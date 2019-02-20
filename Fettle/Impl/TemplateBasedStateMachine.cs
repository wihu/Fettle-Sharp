using System;
using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public class TemplateBasedStateMachine<S, E, C> : IStateMachine<S, E, C>
{
	private readonly ITransitionModel<S, E, C> m_Model;

	public S CurrentState { get; private set; }

	public TemplateBasedStateMachine(ITransitionModel<S, E, C> model, S initialState)
	{
		if (initialState == null)
		{
			throw new Exception("Initial state must not be null.");
		}
		m_Model = model;
		this.CurrentState = initialState;
	}

	public bool FireEvent(E evt)
	{
		return FireEvent(evt, m_Model.DefaultContext);
	}

	public bool FireEvent(E evt, C context)
	{
		m_Model.FireEvent(this, evt, context);
		return false;
	}

	public void SetStateNoActions(S state)
	{
		this.CurrentState = state;
	}

	public bool SetStateWithEntryExitActions(S state)
	{
		bool result = m_Model.SetStateWithEntryExitActions(this, state);
		return result;
	}
	public Dictionary<E, List<ITransition<S, E, C>>> GetPossibleTransitionsFromState(S state)
	{
		return m_Model.GetPossibleTransitionsFromState(state);
	}
}
}
