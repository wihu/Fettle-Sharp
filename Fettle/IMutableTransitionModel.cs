using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface IMutableTransitionModel<S, E, C> 
	: ITransitionModel<S, E, C>
	, IStateMachineTemplate<S, E, C>
	where S : class
{
	IStateMachineTemplate<S, E, C> CreateImmutableClone();
	void AddTransition(S fromState, S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions);
	void AddFromAllTransitions(S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions);
	void AddInternalTransition(S fromState, S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions);
	void AddEntryAction(S state, IAction<S, E, C> action);
	void AddExitAction(S state, IAction<S, E, C> action);
}
}
