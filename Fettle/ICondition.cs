using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface ICondition<C> 
{
	bool IsSatisfied(C context);
}
}
