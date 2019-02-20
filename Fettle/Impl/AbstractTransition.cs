using System.Collections;
using System.Collections.Generic;

namespace Fettle.Impl
{
public abstract class AbstractTransition<S, E, C> : ITransition<S, E, C>
{
	protected readonly S m_ToState;
	protected readonly ICondition<C> m_Condition;
	protected readonly List<IAction<S, E, C>> m_Actions = new List<IAction<S, E, C>>();

	/* ITransition */
	public S To { get { return m_ToState; } }
	/* ITransition */
	public virtual bool ShouldExecuteEntryAndExitActions { get { return false; } }

	public ICondition<C> Condition { get { return m_Condition; } }

	public AbstractTransition(S toState, ICondition<C> condition, List<IAction<S, E, C>> actions)
	{
		m_ToState = toState;
		m_Condition = condition;
		m_Actions.AddRange(actions);
	}

	/* ITransition */
	public bool IsSatisfied(C context)
	{
		return m_Condition.IsSatisfied(context);
	}

	/* ITransition */
	public void OnTransition(S fromState, S toState, E evt, C context, IStateMachine<S, E, C> machine)
	{
		foreach (var action in m_Actions)
		{
			action.OnTransition(fromState, toState, evt, context, machine);
		}
	}
}
}
