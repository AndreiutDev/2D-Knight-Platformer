using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    private Stack<FSMState> stateStack = new Stack<FSMState>();

	public delegate void FSMState(FSM fsm, GameObject obj);
	public void Update(GameObject obj)
	{
		if (stateStack.Peek() != null)
		{
			stateStack.Peek().Invoke(this, obj);
		}
	}
	public void pushState(FSMState state)
	{
		stateStack.Push(state);
	}
	public void popState()
	{
		stateStack.Pop();
	}
}
