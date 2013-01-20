using System;
using System.Collections.Generic;
using Inception.Reflection;

namespace Inception.Proxying
{
	public class FluentProxyActivation : FluentProxyDefinition
	{
		private readonly ArgumentCollection _constructorArguments =
			new ArgumentCollection();

		private readonly List<IInterceptor> _interceptors = new List<IInterceptor>();

		private IProxyDispatcher _dispatcher;

		internal FluentProxyActivation(Type type) 
			: base(type)
		{
		}

		public void DispatchInvocationsWith(IProxyDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		public void AddInterceptor(IInterceptor interceptor)
		{
			if (_dispatcher != null)
			{
				throw new InvalidOperationException("Dispatcher already configured.");
			}
				
			_interceptors.Add(interceptor);
		}

		public void WithConstructorArgument(string parameterName, object value)
		{
			_constructorArguments.Add(parameterName, value);
		}

		internal ArgumentCollection ConstructorArguments
		{
			get { return _constructorArguments; }
		}

		internal IProxyDispatcher Dispatcher
		{
			get
			{
				if (_dispatcher == null)
				{
					_dispatcher = new ProxyDispatcher(_interceptors.ToArray());
				}

				return _dispatcher;
			}
		}
	}
}
