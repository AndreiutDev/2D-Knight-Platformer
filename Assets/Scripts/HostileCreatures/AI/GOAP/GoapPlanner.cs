using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapPlanner
{
	public Queue<GoapAction> Plan(GameObject agent,
								  HashSet<GoapAction> availableActions,
								  HashSet<KeyValuePair<string, object>> worldState,
								  HashSet<KeyValuePair<string, object>> goal)
	{
		foreach (GoapAction action in availableActions)
		{
			action.DoResetAction();
		}

		HashSet<GoapAction> usableActions = new HashSet<GoapAction>();
		foreach (GoapAction action in availableActions)
		{
			if (action.CheckProceduralPreconditions(agent))
				usableActions.Add(action);
		}

		List<Node> leaves = new List<Node>();

		Node start = new Node(null, 0, worldState, null);
		bool success = BuildGraph(start, leaves, usableActions, goal);

		if (!success)
		{
			return null;
		}

		Node cheapest = null;
		foreach (Node leaf in leaves)
		{
			if (cheapest == null)
				cheapest = leaf;
			else
			{
				if (leaf.runningCost < cheapest.runningCost)
					cheapest = leaf;
			}
		}

		List<GoapAction> result = new List<GoapAction>();
		Node n = cheapest;
		while (n != null)
		{
			if (n.action != null)
			{
				result.Insert(0, n.action); 
			}
			n = n.parent;
		}

		Queue<GoapAction> queue = new Queue<GoapAction>();
		foreach (GoapAction a in result)
		{
			queue.Enqueue(a);
		}
		return queue;
	}

	protected bool BuildGraph(Node parent, List<Node> leaves, HashSet<GoapAction> usableActions, HashSet<KeyValuePair<string, object>> goal)
	{
		bool foundOne = false;

		foreach (GoapAction action in usableActions)
		{
			if (inState(action.preconditions, parent.state))
			{
				HashSet<KeyValuePair<string, object>> currentState = populateState(parent.state, action.effects);

				Node node = new Node(parent, parent.runningCost + action.cost, currentState, action);

				if (goalInState(goal, currentState))
				{
					leaves.Add(node);
					foundOne = true;
				}
				else
				{
					HashSet<GoapAction> subset = actionSubset(usableActions, action);
					bool found = BuildGraph(node, leaves, subset, goal);
					if (found)
						foundOne = true;
				}


			}
		}

		return foundOne;
	}

	protected HashSet<GoapAction> actionSubset(HashSet<GoapAction> actions, GoapAction removeMe)
	{
		HashSet<GoapAction> subset = new HashSet<GoapAction>();
		foreach (GoapAction a in actions)
		{
			if (!a.Equals(removeMe))
				subset.Add(a);
		}
		return subset;
	}

	protected bool goalInState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
	{
		bool match = false;
		foreach (KeyValuePair<string, object> t in test)
		{
			foreach (KeyValuePair<string, object> s in state)
			{
				if (s.Equals(t))
				{
					match = true;
					break;
				}
			}
		}
		return match;
	}

	protected bool inState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
	{
		bool allMatch = true;
		foreach (KeyValuePair<string, object> t in test)
		{
			bool match = false;
			foreach (KeyValuePair<string, object> s in state)
			{
				if (s.Equals(t))
				{
					match = true;
					break;
				}
			}
			if (!match)
				allMatch = false;
		}
		return allMatch;
	}
	protected HashSet<KeyValuePair<string, object>> populateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
	{
		HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();

		foreach (KeyValuePair<string, object> s in currentState)
		{
			state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
		}

		foreach (KeyValuePair<string, object> change in stateChange)
		{

			bool exists = false;

			foreach (KeyValuePair<string, object> s in state)
			{
				if (s.Key.Equals(change.Key))
				{
					exists = true;
					break;
				}
			}
			if (exists)
			{
				state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
				KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
				state.Add(updated);
			}
			else
			{
				state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
			}
		}
		return state;
	}
	protected class Node
	{
		public Node parent;
		public float runningCost;
		public HashSet<KeyValuePair<string, object>> state;
		public GoapAction action;

		public Node(Node parent, float runningCost, HashSet<KeyValuePair<string, object>> state, GoapAction action)
		{
			this.parent = parent;
			this.runningCost = runningCost;
			this.state = state;
			this.action = action;
		}
	}
}
