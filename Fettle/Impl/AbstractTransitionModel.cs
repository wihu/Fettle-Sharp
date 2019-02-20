using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public class AbstractTransitionModel<S, E, C> : ITransitionModel<S, E, C> 
	where S : class 
	where E : class 
	where C : class
{
	public class ActionCollection : List<IAction<S, E, C>> {}
	public class TransitionCollection : List<ITransition<S, E, C>> {}
	public class EventToTransitionsMap : Dictionary<E, TransitionCollection> {}
	public class StateToActionsMap : Dictionary<S, ActionCollection> {}

	protected readonly Dictionary<S, EventToTransitionsMap> m_TransitionMap = new Dictionary<S, EventToTransitionsMap>();
	protected readonly EventToTransitionsMap m_FromAllTransitions = new EventToTransitionsMap();
	protected readonly StateToActionsMap m_ExitActions = new StateToActionsMap();
	protected readonly StateToActionsMap m_EnterActions = new StateToActionsMap();
	protected readonly C m_DefaultContext;

	public C DefaultContext { get { return m_DefaultContext; } }

	public AbstractTransitionModel(C defaultContext)
	{
		m_DefaultContext = defaultContext;
	}

	public bool FireEvent(IStateMachine<S, E, C> machine, E evt, C context)
	{
		var fromState = machine.CurrentState;
		var transitionMap = (EventToTransitionsMap)null;
		m_TransitionMap.TryGetValue(fromState, out transitionMap);
		bool result = FireEvent(machine, evt, transitionMap, fromState, context) || 
			FireEvent(machine, evt, m_FromAllTransitions, fromState, context);
		return result;
	}

	public bool SetStateWithEntryExitActions(IStateMachine<S, E, C> machine, S toState)
	{
		var fromState = machine.CurrentState;
		if (fromState.Equals(toState))
		{
			return false;
		}
		SetStateWithEntryExitActions(machine, fromState, toState, null, null, null);
		return true;
	}

	public Dictionary<E, List<ITransition<S, E, C>>> GetPossibleTransitionsFromState(S state)
	{
		var map = new Dictionary<E, List<ITransition<S, E, C>>>();
		return map;
	}

	private bool ShouldExecuteEntryAndExitActions(ITransition<S, E, C> transition) 
	{
		return (transition == null) || transition.ShouldExecuteEntryAndExitActions;
	}

	private void InvokeActions(ActionCollection actions, S fromState, S toState, E evt, C context, IStateMachine<S, E, C> machine)
	{
		if (actions == null)
		{
			return;
		}
		foreach (var action in actions)
		{
			action.OnTransition(fromState, toState, evt, context, machine);
		}
	}

	private void SetStateWithEntryExitActions(IStateMachine<S, E, C> machine, S fromState, S toState, ITransition<S, E, C> transition, E evt, C context) 
	{
		if (ShouldExecuteEntryAndExitActions(transition))
		{
			var actions = (ActionCollection)null;
			m_ExitActions.TryGetValue(fromState, out actions);
			InvokeActions(actions, fromState, toState, evt, context, machine);
		}
		machine.SetStateNoActions(toState);
		if (transition != null)
		{
			transition.OnTransition(fromState, toState, evt, context, machine);
		}
		if (ShouldExecuteEntryAndExitActions(transition))
		{
			var actions = (ActionCollection)null;
			m_EnterActions.TryGetValue(toState, out actions);
			InvokeActions(actions, fromState, toState, evt, context, machine);
		}
	}

	private bool FireEvent(IStateMachine<S, E, C> machine, E evt, EventToTransitionsMap transitionMap, S fromState, C context)
	{
		if (transitionMap == null)
		{
			return false;
		}
		var transitions = (TransitionCollection)null;
		if (!transitionMap.TryGetValue(evt, out transitions))
		{
			return false;
		}
		foreach (var transition in transitions)
		{
			if (transition.IsSatisfied(context))
			{
				SetStateWithEntryExitActions(machine, fromState, transition.To, transition, evt, context);
				return true;
			}
		}
		return false;
	}
}
}
