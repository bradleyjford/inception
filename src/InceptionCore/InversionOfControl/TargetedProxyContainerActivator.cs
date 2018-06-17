using System;
using InceptionCore.Proxying;

namespace InceptionCore.InversionOfControl
{
    public class TargetedProxyContainerActivator : ContainerActivator
    {
        readonly IContainerActivator _activator;
        readonly IInterceptor _interceptor;
        readonly IProxyDispatcher _proxyDispatcher;
        readonly Type _type;

        public TargetedProxyContainerActivator(
            IProxyDispatcher proxyDispatcher,
            Type type,
            IContainerActivator activator,
            IInterceptor interceptor)
        {
            _proxyDispatcher = proxyDispatcher;
            _type = type;
            _activator = activator;
            _interceptor = interceptor;
        }

        public override object CreateInstance(IContainer container)
        {
            var dispatcher = _proxyDispatcher ?? new ProxyDispatcher(new[] {_interceptor});

            var instance = _activator.CreateInstance(container);

            //TODO: Fix
            return null; // _proxyFactory.CreateTargetedProxy(instance, dispatcher);
        }
    }
}