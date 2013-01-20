using System;
using System.Reflection;

namespace Inception.Proxying
{
	public class ProxyDispatcher : IProxyDispatcher
	{
		private readonly IInterceptor[] _interceptors;

		public ProxyDispatcher()
		{
			_interceptors = new IInterceptor[0];
		}

		public ProxyDispatcher(IInterceptor[] interceptors)
		{
			_interceptors = interceptors ?? new IInterceptor[0];
		}

		public object MethodInvocation(
			object target,
			MethodInfo method, 
			object[] arguments, 
			ProxyTargetInvocation proxyTargetInvocation)
		{
			if (_interceptors.Length == 0)
			{
				return proxyTargetInvocation(arguments);
			}

			var invocation = new Invocation(
				target,
				(IProxy)target, // TODO: Fix
				method,
				arguments,
				proxyTargetInvocation,
				_interceptors);

			invocation.ExecuteInterceptors();
			
			return invocation.ReturnValue;
		}
	}
}