using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface IAction<S, E, C> 
{
	/// <summary>
	/// Called when a transition occurs.
	/// </summary>
	/// <param name="fromState">From state.</param>
	/// <param name="toState">To state.</param>
	/// <param name="causedBy">Caused by.</param>
	/// <param name="context">Context.</param>
	/// <param name="stateMachine">State machine.</param>
	void OnTransition(S fromState, S toState, E causedBy, C context, IStateMachine<S, E, C> stateMachine);
}
}
