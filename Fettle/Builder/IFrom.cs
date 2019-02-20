using System.Collections;
using System.Collections.Generic;

namespace Fettle.Builder
{
public interface IFrom<S, E, C> 
{
	ITo<S, E, C> To(S state);
}
}
