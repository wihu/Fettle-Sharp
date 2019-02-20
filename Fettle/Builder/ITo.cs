using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface ITo<S, E, C> 
{
	IOn<S, E, C> On(E e);
}
}
