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

//	public class AlwaysCondition<C> : ICondition<C>
//	{
//		public bool IsSatisfied(C context)
//		{
//			return true;
//		}
//	}
//
//	public class AndCondition<C> : ICondition<C>
//	{
//		private readonly ICondition<C> m_First;
//		private readonly ICondition<C> m_Second;
//
//		public AndCondition(ICondition<C> first, ICondition<C> second)
//		{
//			m_First = first;
//			m_Second = second;
//		}
//
//		public bool IsSatisfied(C context)
//		{
//			return m_First.IsSatisfied(context) && m_Second.IsSatisfied(context);
//		}
//	}
}
}
