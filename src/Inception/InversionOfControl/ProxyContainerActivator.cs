using System;
using System.Reflection;
using Inception.Proxying;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
    public class ProxyContainerActivator : ContainerActivator
    {
        private readonly IProxyDispatcher _proxyDispatcher;
        private readonly Type _type;
        private readonly ConstructorInfo _constructor;
        private readonly ArgumentCollection _constructorArguments;
        private readonly IInterceptor _interceptor;

        public ProxyContainerActivator(
            IProxyDispatcher proxyDispatcher,
            Type type,
            ConstructorInfo constructor,
            ArgumentCollection constructorArguments,
            IInterceptor interceptor)
        {
            _proxyDispatcher = proxyDispatcher;
            _type = type;
            _constructor = constructor;
            _constructorArguments = constructorArguments;
            _interceptor = interceptor;
        }

        public override object CreateInstance(IContainer container)
        {
            //var proxyFactory = container.GetInstance<ProxyFactory>();

            //var args = PrepareConstructorArguments(_constructor, _constructorArguments, container);

            //var dispatcher = _proxyDispatcher ?? new ProxyDispatcher(new [] { _interceptor });

            //return proxyFactory.DefineProxy(_type);
            throw new NotImplementedException();
        }
    }
}
