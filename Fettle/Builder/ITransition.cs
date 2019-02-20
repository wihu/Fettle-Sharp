using System.Collections;
using System.Collections.Generic;

namespace Fettle.Builder
{
public interface ITransition<S, E, C> 
{
	IFrom<S, E, C> From(S state);
	IFrom<S, E, C> FromAll();

}
}
