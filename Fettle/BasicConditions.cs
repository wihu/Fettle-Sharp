using System;
using System.Collections;
using System.Collections.Generic;

namespace Fettle
{
public static class BasicConditions<C> where C : class
{
	public static ICondition<C> Always() 
	{ 
		return new PredicateCondition<C>(context => true); 
	}

	public static ICondition<C> And(ICondition<C> first, ICondition<C> second) 
	{ 
		return new PredicateCondition<C>(context => (first.IsSatisfied(context) && second.IsSatisfied(context))); 
	}

	private class PredicateCondition<T> : ICondition<T>
	{
		private readonly Func<T, bool> m_Predicate;

		public PredicateCondition(Func<T, bool> predicate)
		{
			m_Predicate = predicate;
		}

		public bool IsSatisfied(T context)
		{
			return m_Predicate(context);
		}
	}
}
}
