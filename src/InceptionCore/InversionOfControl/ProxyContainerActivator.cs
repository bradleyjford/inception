using System;
using System.Reflection;
using InceptionCore.Proxying;
using InceptionCore.Reflection;

namespace InceptionCore.InversionOfControl
{
    public class ProxyContainerActivator : ContainerActivator
    {
        readonly ConstructorInfo _constructor;
        readonly ArgumentCollection _constructorArguments;
        readonly IInterceptor _interceptor;
        readonly IProxyDispatcher _proxyDispatcher;
        readonly Type _type;

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