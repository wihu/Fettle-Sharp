using System;
using System.Collections;
using System.Collections.Generic;

namespace Fettle.Builder
{
public class TransitionBuilder<S, E, C> 
	: ITransition<S, E, C>
	, IFrom<S, E, C>
	, ITo<S, E, C>
	, IOn<S, E, C>
	, IWhen<S, E, C>
	, IInternal<S, E, C>
	where S : class
	where E : class
	where C : class
{
	private readonly List<IAction<S, E, C>> m_Actions = new List<IAction<S, E, C>>();
	private S m_From;
	private S m_To;
	private E m_Event;
	private ICondition<C> m_Condition = BasicConditions<C>.Always();
	private bool m_RunEntryAndExit = true;

	public IOn<S, E, C> On(E e)
	{
		m_Event = e;
		return this;
	}

	public IFrom<S, E, C> From(S state)
	{
		m_From = state;
		return this;
	}

	public IFrom<S, E, C> FromAll() {
		m_From = null;
		return this;
	}

	public ITo<S, E, C> To(S state)
	{
		m_To = state;
		return this;
	}

	public IInternal<S, E, C> Internal(S state)
	{
		m_From = state;
		m_To = state;
		m_RunEntryAndExit = false;
		return this;
	}

	public IWhen<S, E, C> When(ICondition<C> condition)
	{
		m_Condition = condition;
		return this;
	}

	public void Perform(IAction<S, E, C> action)
	{
		m_Actions.Add(action);
	}

	public void Perform(List<IAction<S, E, C>> actions)
	{
		m_Actions.AddRange(actions);
	}

	public void AddToTransitionModel(IMutableTransitionModel<S, E, C> model)
	{
		if (m_Event == null)
		{
			string fromString = m_From == null ? "AnyState" : m_From.ToString();
			string toString = m_To.ToString();
			string message = "The transition (" + fromString + " -> " + toString + ") has to be performed on an event. " +
			               "Use on() to specify on what event the transition should take place";
			throw new Exception(message);
		}

		if (m_From == null)
		{
			model.AddFromAllTransitions(m_To, m_Event, m_Condition, m_Actions);
		}
		else if (m_RunEntryAndExit)
		{
			model.AddTransition(m_From, m_To, m_Event, m_Condition, m_Actions);
		}
		else
		{
			model.AddInternalTransition(m_From, m_To, m_Event, m_Condition, m_Actions);
		}
	}
}
}
