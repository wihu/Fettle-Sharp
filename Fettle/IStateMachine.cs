using System.Collections;
using System.Collections.Generic;

namespace Fettle 
{
public interface IStateMachine<S, E, C> 
{
	S CurrentState { get; }
	bool FireEvent(E e);
	bool FireEvent(E e, C context);
	void SetStateNoActions(S state);
	bool SetStateWithEntryExitActions(S state);
	Dictionary<E, List<ITransition<S, E, C>>> GetPossibleTransitionsFromState(S state);
}
}
