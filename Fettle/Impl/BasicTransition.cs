using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public class BasicTransition<S, E, C> : AbstractTransition<S, E, C> 
{
	public override bool ShouldExecuteEntryAndExitActions { get { return true; } }

	public BasicTransition(S toState, ICondition<C> condition, List<IAction<S, E, C>> actions) 
		: base(toState, condition, actions)
	{
	}
}
}
