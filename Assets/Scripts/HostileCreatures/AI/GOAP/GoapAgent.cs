using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoapAgent : MonoBehaviour
{
	private FSM stateMachine;
	private FSM.FSMState idleState;
	private FSM.FSMState moveToState;
	private FSM.FSMState performActionState;

    private HashSet<GoapAction> availableActions;
    private Queue<GoapAction> currentActions;

    public IGoap dataProvider;
    public GoapPlanner planner;

	void Start () {
		stateMachine = new FSM();
		availableActions = new HashSet<GoapAction>();
		currentActions = new Queue<GoapAction>();
		planner = new GoapPlanner();
		FindDataProvider();
		CreateIdleState();
		CreateMoveToState();
		CreatePerformActionState();
		stateMachine.pushState(idleState);
	    LoadActions();
	}
	void Update()
	{
		stateMachine.Update(this.gameObject);
	}
	public void AddAction(GoapAction action)
	{
		availableActions.Add(action);
	}
	public GoapAction GetAction(Type action)
	{
		foreach (GoapAction availableAction in availableActions)
		{
			if (availableAction.GetType().Equals(action))
				return availableAction;
		}
		return null;
	}
	public void RemoveAction(GoapAction action)
	{
		availableActions.Remove(action);
	}
	private bool HasActionPlan()
	{
		return currentActions.Count > 0;
	}
	private void CreateIdleState()
	{
		idleState = (fsm, obj) => {

			HashSet<KeyValuePair<string, object>> worldState = dataProvider.GetWorldState();
			HashSet<KeyValuePair<string, object>> goal = dataProvider.CreateGoalState();

			Queue<GoapAction> plan = planner.Plan(gameObject, availableActions, worldState, goal);

			if (plan != null)
			{
				currentActions = plan;
				dataProvider.PlanFound(goal, plan);

				fsm.popState();
				fsm.pushState(performActionState);
			}
			else
			{
				dataProvider.PlanFailed(goal);
				fsm.popState();
				fsm.pushState(idleState);
			}
		};
	}
	private void CreateMoveToState()
	{
		moveToState = (fsm, gameObject) => {

			GoapAction action = currentActions.Peek();
			if (action.RequiresInRange() && action.target == null)
			{
				fsm.popState();
				fsm.popState();
				fsm.pushState(idleState);
				return;
			}

			if (dataProvider.MoveAgent(action))
			{
				fsm.popState();
			}
		};
	}
	private void CreatePerformActionState()
	{

		performActionState = (fsm, obj) => {

			if (!HasActionPlan())
			{
				fsm.popState();
				fsm.pushState(idleState);
				dataProvider.ActionsFinished();
				return;
			}

			GoapAction action = currentActions.Peek();
			if (action.IsDone())
			{
				currentActions.Dequeue();
			}

			if (HasActionPlan())
			{
				action = currentActions.Peek();
				bool inRange = action.RequiresInRange() ? action.IsInRange() : true;

				if (inRange)
				{
					bool success = action.Perform(obj);
					if (!success)
					{
						fsm.popState();
						fsm.pushState(idleState);
						CreateIdleState();
						dataProvider.PlanAborted(action);
					}
				}
				else
				{
					fsm.pushState(moveToState);
				}
			}
			else
			{
				fsm.popState();
				fsm.pushState(idleState);
				dataProvider.ActionsFinished();
			}
		};
	}
	private void FindDataProvider()
	{
		foreach (Component comp in gameObject.GetComponents(typeof(Component)))
		{
			if (typeof(IGoap).IsAssignableFrom(comp.GetType()))
			{
				dataProvider = (IGoap)comp;
				Debug.Log(dataProvider);
				return;
			}
		}
	}
	private void LoadActions()
	{
		GoapAction[] actions = gameObject.GetComponents<GoapAction>();
		foreach (GoapAction action in actions)
		{
			availableActions.Add(action);
		}
	}
}
