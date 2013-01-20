using System;
using System.Reflection;
using Inception.Proxying;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
	public class ProxyContainerActivator : ContainerActivator
	{
		private readonly ProxyFactory _proxyFactory;
		private readonly IProxyDispatcher _proxyDispatcher;
		private readonly Type _type;
		private readonly ConstructorInfo _constructor;
		private readonly ArgumentCollection _constructorArguments;
		private readonly IInterceptor _interceptor;

		public ProxyContainerActivator(
			ProxyFactory proxyFactory,
			IProxyDispatcher proxyDispatcher,
			Type type,
			ConstructorInfo constructor,
			ArgumentCollection constructorArguments,
			IInterceptor interceptor)
		{
			_proxyFactory = proxyFactory;
			_proxyDispatcher = proxyDispatcher;
			_type = type;
			_constructor = constructor;
			_constructorArguments = constructorArguments;
			_interceptor = interceptor;
		}

		public override object CreateInstance(IContainer container)
		{
			var args = PrepareConstructorArguments(_constructor, _constructorArguments, container);

			var dispatcher = _proxyDispatcher ?? new ProxyDispatcher(new [] { _interceptor });

			// TODO: Fix
			return null; // _proxyFactory.CreateProxy(_type, dispatcher, args);
		}
	}
}
