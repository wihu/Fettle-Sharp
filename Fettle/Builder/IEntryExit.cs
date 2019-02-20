using System.Collections;
using System.Collections.Generic;

namespace Fettle.Builder
{
public interface IEntryExit<S, E, C> 
{
	IEntryExit<S, E, C> Perform(IAction<S, E, C> action);
	IEntryExit<S, E, C> Perform(List<IAction<S, E, C>> actions);
}
}
