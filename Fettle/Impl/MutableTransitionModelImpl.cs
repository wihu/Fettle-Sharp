using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public class MutableTransitionModelImpl<S, E, C> 
	: AbstractTransitionModel<S, E, C>
	, IMutableTransitionModel<S, E, C>
	where S : class
	where E : class
	where C : class
{
	private MutableTransitionModelImpl(C defaultContext) : base(defaultContext)
	{
	}

	public static MutableTransitionModelImpl<S, E, C> Create()
	{
		return CreateWithDefaultContext(null);
	}

	public static MutableTransitionModelImpl<S, E, C> CreateWithDefaultContext(C defaultContext)
	{
		return new MutableTransitionModelImpl<S, E, C>(defaultContext);
	}

	public IStateMachine<S, E, C> NewStateMachineWithInitialState(S state)
	{
		return new TemplateBasedStateMachine<S, E, C>(this, state);
	}

	public IStateMachineTemplate<S, E, C> CreateImmutableClone()
	{
		return null;
	}

	public void AddTransition(S fromState, S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions)
	{
		AddTransition(fromState, evt, new BasicTransition<S, E, C>(toState, condition, actions));
	}

	public void AddFromAllTransitions(S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions)
	{
		var transitions = (TransitionCollection)null;
		if (!m_FromAllTransitions.TryGetValue(evt, out transitions))
		{
			transitions = new TransitionCollection();
			m_FromAllTransitions.Add(evt, transitions);
		}
		transitions.Add(new BasicTransition<S, E, C>(toState, condition, actions));
	}

	public void AddInternalTransition(S fromState, S toState, E evt, ICondition<C> condition, List<IAction<S, E, C>> actions)
	{
		AddTransition(fromState, evt, new InternalTransition<S, E, C>(toState, condition, actions));
	}

	public void AddEntryAction(S state, IAction<S, E, C> action)
	{
	}

	public void AddExitAction(S state, IAction<S, E, C> action)
	{
	}

	private void AddTransition(S fromState, E evt, ITransition<S, E, C> transition)
	{
		//TODO(WH): create an extension for IDictionary<T>.ComputeIfAbsent(key, k => new T()).
		var map = (EventToTransitionsMap)null;
		if (!m_TransitionMap.TryGetValue(fromState, out map))
		{
			map = new EventToTransitionsMap();
			m_TransitionMap.Add(fromState, map);
		}
		var transitions = (TransitionCollection)null;
		if (!map.TryGetValue(evt, out transitions))
		{
			transitions = new TransitionCollection();
			map.Add(evt, transitions);
		}
		transitions.Add(transition);
	}
}
}
