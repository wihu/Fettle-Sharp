using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface IOn<S, E, C> : IWhen<S, E, C>
{
	IWhen<S, E, C> When(ICondition<C> condition);
}
}
