using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Fettle.Impl;

namespace Fettle.Builder
{
public class StateMachineBuilder<S, E, C> 
	where S : class 
	where E : class
	where C : class
{
	private readonly List<TransitionBuilder<S, E, C>> m_TransitionBuilders = new List<TransitionBuilder<S, E, C>>();
	private readonly List<EntryExitActionBuilder<S, E, C>> m_EntryExitActions = new List<EntryExitActionBuilder<S, E, C>>();
	private C m_DefaultContext;

	private StateMachineBuilder(C defaultContext)
	{
		m_DefaultContext = defaultContext;
	}

	public static StateMachineBuilder<S, E, C> Create()
	{
		return CreateWithDefaultContext(null);
	}

	public static StateMachineBuilder<S, E, C> CreateWithDefaultContext(C defaultContext)
	{
		return new StateMachineBuilder<S, E, C>(defaultContext);
	}

	public ITransition<S, E, C> Transition()
	{
		var transition = new TransitionBuilder<S, E, C>();
		m_TransitionBuilders.Add(transition);
		return transition;
	}

	public IEntryExit<S, E, C> OnEntry(S state)
	{
		var actionBuilder = EntryExitActionBuilder<S, E, C>.EntryTo(state);
		m_EntryExitActions.Add(actionBuilder);
		return actionBuilder;
	}

	public IEntryExit<S, E, C> OnExit(S state)
	{
		var actionBuilder = EntryExitActionBuilder<S, E, C>.ExitFrom(state);
		m_EntryExitActions.Add(actionBuilder);
		return actionBuilder;
	}

	public StateMachineBuilder<S, E, C> WithDefaultContext(C context)
	{
		m_DefaultContext = context;
		return this;
	}

	public IStateMachine<S, E, C> BuildWithInitialState(S state)
	{
		var template = BuildTransitionModel();
		return template.NewStateMachineWithInitialState(state);
	}

	public IStateMachineTemplate<S, E, C> BuildTransitionModel()
	{
		// TODO(WH): replace this with template factory passed as a param or via ctor.
		var model = MutableTransitionModelImpl<S, E, C>.CreateWithDefaultContext(m_DefaultContext);
		foreach (var builder in m_TransitionBuilders)
		{
			builder.AddToTransitionModel(model);
		}
		foreach (var builder in m_EntryExitActions)
		{
			builder.AddToTransitionModel(model);
		}
		return model;
	}
}
}
