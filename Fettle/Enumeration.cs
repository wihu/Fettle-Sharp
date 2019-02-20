using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fettle
{
public abstract class Enumeration : IComparable
{
	public string Name { get; private set; }

	public int Id { get; private set; }

	protected Enumeration()
	{ }

	protected Enumeration(int id, string name) 
	{
		Id = id; 
		Name = name; 
	}

	public override string ToString() 
	{
		return this.Name;
	}

	public static IEnumerable<T> GetAll<T>() where T : Enumeration
	{
		var fields = typeof(T).GetFields(BindingFlags.Public | 
			BindingFlags.Static | 
			BindingFlags.DeclaredOnly); 

		return fields.Select(f => f.GetValue(null)).Cast<T>();
	}
	
	public override int GetHashCode()
	{
		return this.Id.GetHashCode();
	}

	public override bool Equals(object obj) 
	{
		var otherValue = obj as Enumeration; 

		if (object.ReferenceEquals(otherValue, null)) 
			return false;

		bool typeMatches = GetType().Equals(obj.GetType());
		bool valueMatches = Id.Equals(otherValue.Id);
		return typeMatches && valueMatches;
	}

	public int CompareTo(object other) 
	{
		return this.Id.CompareTo(((Enumeration)other).Id);
	}

	public static bool operator ==(Enumeration a, Enumeration b)
	{
		if (object.ReferenceEquals(a, null)) {
			return object.ReferenceEquals(b, null);
		}
		return a.Equals(b);
	}

	public static bool operator !=(Enumeration a, Enumeration b)
	{
		return !(a == b);
	}
}
}
