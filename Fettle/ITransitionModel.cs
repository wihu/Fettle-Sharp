using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface ITransitionModel<S, E, C>
{
	C DefaultContext { get; }

	/// <summary>
	/// Fires the event at the state machine. This can result in a state change and trigger actions to be run.
	/// </summary>
	/// <returns><c>true</c>, if a state change was triggered, <c>false</c> otherwise.</returns>
	/// Note that this will return true if a transition to the same state occurs.
	/// <param name="machine">State machine.</param>
	/// <param name="e">Event.</param>
	/// <param name="context">Context.</param>
	bool FireEvent(IStateMachine<S, E, C> machine, E e, C context);

	/// <summary>
	/// Forces the state machine to enter a state, even if there are no transitions to that state.
	/// No transition actions are run but exit actions on the current state and entry actions on the new state are run.
	/// </summary>
	/// <returns><c>true</c>, if the state was changed, <c>false</c> otherwise.</returns>
	bool SetStateWithEntryExitActions(IStateMachine<S, E, C> machine, S state);

	/// <summary>
	/// Gets possible transitions from a state.
	/// </summary>
	/// <returns>Possible transitions from the specified state.</returns>
	/// <param name="state">State.</param>
	Dictionary<E, List<ITransition<S, E, C>>> GetPossibleTransitionsFromState(S state);
}
}
