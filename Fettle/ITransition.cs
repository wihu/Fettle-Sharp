using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface ITransition<S, E, C>
{
	S To { get; }
	bool ShouldExecuteEntryAndExitActions { get; }

	bool IsSatisfied(C context);
	void OnTransition(S fromState, S toState, E e, C context, IStateMachine<S, E, C> machine);
}
}
