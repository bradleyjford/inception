using System;
using System.Reflection;

namespace Inception.Aspects
{
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class AspectAttribute : Attribute
	{
		private int _order = 1;

		public int Order 
		{ 
			get { return _order; }
			set { _order = value; }
		}

		protected internal virtual void OnBeforeInvocation(
			object target, 
			MethodInfo method,
			ParameterInfo[] parameters, 
			object[] arguments)
		{
		}

		protected internal virtual void OnAfterInvocation(
			object target, 
			MethodInfo method,
			ParameterInfo[] parameters, 
			object[] arguments, 
			object returnValue)
		{
		}

		protected internal virtual bool OnInvocationException(
			object target, 
			MethodInfo method,
			ParameterInfo[] parameters, 
			object[] arguments, 
			Exception exception)
		{
			return false;
		}
	}
}
