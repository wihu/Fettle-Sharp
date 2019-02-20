using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public interface IStateMachineTemplate<S, E, C> where S : class
{
	IStateMachine<S, E, C> NewStateMachineWithInitialState(S state);

}
}
