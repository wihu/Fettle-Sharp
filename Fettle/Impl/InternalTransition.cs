using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public class InternalTransition<S, E, C> : AbstractTransition<S, E, C> 
{
	public override bool ShouldExecuteEntryAndExitActions { get { return false; } }

	public InternalTransition(S toState, ICondition<C> condition, List<IAction<S, E, C>> actions) 
		: base(toState, condition, actions)
	{
	}
}
}
