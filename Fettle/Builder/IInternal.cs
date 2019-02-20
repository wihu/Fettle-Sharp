using System.Collections;
using System.Collections.Generic;

namespace Fettle.Builder
{
public interface IInternal<S, E, C> 
{
	IOn<S, E, C> On(E evt);
}
}
