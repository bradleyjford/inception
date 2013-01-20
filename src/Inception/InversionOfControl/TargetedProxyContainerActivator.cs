using System;
using Inception.Proxying;

namespace Inception.InversionOfControl
{
	public class TargetedProxyContainerActivator : ContainerActivator
	{
		private readonly ProxyFactory _proxyFactory;
		private readonly IProxyDispatcher _proxyDispatcher;
		private readonly Type _type;
		private readonly IContainerActivator _activator;
		private readonly IInterceptor _interceptor;

		public TargetedProxyContainerActivator(
			ProxyFactory proxyFactory,
			IProxyDispatcher proxyDispatcher,
			Type type,
			IContainerActivator activator,
			IInterceptor interceptor)
		{
			_proxyFactory = proxyFactory;
			_proxyDispatcher = proxyDispatcher;
			_type = type;
			_activator = activator;
			_interceptor = interceptor;
		}

		public override object CreateInstance(IContainer container)
		{
			var dispatcher = _proxyDispatcher ?? new ProxyDispatcher(new[] { _interceptor });

			var instance = _activator.CreateInstance(container);

			//TODO: Fix
			return null; // _proxyFactory.CreateTargetedProxy(instance, dispatcher);
		}
	}
}
