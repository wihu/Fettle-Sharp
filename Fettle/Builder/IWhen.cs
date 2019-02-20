using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface IWhen<S, E, C>
{
	void Perform(IAction<S, E, C> action);
	void Perform(List<IAction<S, E, C>> actions);
}
}
