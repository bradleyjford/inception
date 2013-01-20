using System;
using System.Linq;
using Inception.Proxying;

namespace Inception.Aspects
{
	public class AspectInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			var aspectAttributes = 
				Attribute.GetCustomAttributes(invocation.Method, typeof(AspectAttribute), true)
				.Cast<AspectAttribute>()
				.OrderBy(aspect => aspect.Order);

			var parameters = invocation.Method.GetParameters();

			foreach (var aspect in aspectAttributes)
			{
				aspect.OnBeforeInvocation(
					invocation.Target, 
					invocation.Method, 
					parameters, 
					invocation.Arguments);
			}

			try
			{
				invocation.Proceed();

				foreach (var aspect in aspectAttributes)
				{
					aspect.OnAfterInvocation(
						invocation.Target,
						invocation.Method, 
						parameters, 
						invocation.Arguments, 
						invocation.ReturnValue);
				}
			}
			catch (Exception invocationException)
			{
				var handled = false;

				foreach (var aspect in aspectAttributes)
				{
					var aspectHandled = aspect.OnInvocationException(
						invocation.Target, 
						invocation.Method, 
						parameters, 
						invocation.Arguments, 
						invocationException);

					if (aspectHandled)
					{
						handled = true;
						break;
					}
				}

				if (!handled)
				{
					throw;
				}
			}
		}
	}
}
