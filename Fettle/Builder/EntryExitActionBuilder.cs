using System;
using System.Collections.Generic;

namespace Fettle.Builder
{
public class EntryExitActionBuilder<S, E, C> : IEntryExit<S, E, C> where S : class
{
	private enum Mode
	{
		Entry,
		Exit,
	}

	private readonly Mode m_Mode;
	private readonly S m_State;
	private readonly List<IAction<S, E, C>> m_Actions = new List<IAction<S, E, C>>();

	private EntryExitActionBuilder(Mode mode, S state)
	{
		m_Mode = mode;
		m_State = state;
	}

	public static EntryExitActionBuilder<S, E, C> EntryTo(S state)
	{
		return new EntryExitActionBuilder<S, E, C>(Mode.Entry, state);
	}

	public static EntryExitActionBuilder<S, E, C> ExitFrom(S state)
	{
		return new EntryExitActionBuilder<S, E, C>(Mode.Exit, state);
	}

	public IEntryExit<S, E, C> Perform(IAction<S, E, C> action)
	{
		m_Actions.Add(action);
		return this;
	}

	public IEntryExit<S, E, C> Perform(List<IAction<S, E, C>> actions)
	{
		m_Actions.AddRange(actions);
		return this;
	}

	public void AddToTransitionModel(IMutableTransitionModel<S, E, C> model)
	{
		foreach (var action in m_Actions)
		{
			Add(model, action);
		}
	}

	private void Add(IMutableTransitionModel<S, E, C> model, IAction<S, E, C> action)
	{
		switch (m_Mode)
		{
		case Mode.Entry:
			model.AddEntryAction(m_State, action);
			break;
		case Mode.Exit:
			model.AddExitAction(m_State, action);
			break;
		default:
            throw new Exception("Invalid mode = " + m_Mode);
            break;
		}
	}
}
}
