using System;
using InceptionCore.Proxying;
using InceptionCore.Reflection;

namespace InceptionCore.InversionOfControl.Configuration
{
    public class FluentForRegistration<TBase> : IRegistration, IFluentForRegistration<TBase>
    {
        readonly Registry _registry;
        IConstructorSelector _constructorSelector;

        bool _generateProxy;
        IInterceptor _interceptor;
        IProxyDispatcher _proxyDispatcher;

        internal FluentForRegistration(Registry registry)
        {
            _registry = registry;
        }

        public IFluentForRegistration<TBase> Proxy(IInterceptor interceptor)
        {
            _generateProxy = true;
            _interceptor = interceptor;

            return this;
        }

        public IFluentForRegistration<TBase> Proxy(IProxyDispatcher proxyDispatcher)
        {
            _generateProxy = true;
            _proxyDispatcher = proxyDispatcher;

            return this;
        }

        public IFluentForRegistration<TBase> Named(string name)
        {
            Name = name;

            return this;
        }

        public IFluentForRegistration<TBase> Singleton()
        {
            LifecycleType = typeof(SingletonContainerLifecycle);

            return this;
        }

        public IFluentForRegistration<TBase> Lifecycle<TLifecycle>()
            where TLifecycle : IContainerLifecycle
        {
            LifecycleType = typeof(TLifecycle);

            return this;
        }

        public void Use<TConcrete>()
            where TConcrete : TBase
        {
            ConcreteType = typeof(TConcrete);

            if (_constructorSelector == null)
            {
                _constructorSelector = new ContainerConstructorSelector();
            }

            var constructor = _constructorSelector.Select(typeof(TConcrete), ConstructorArguments);

            if (constructor == null)
            {
                throw new Exception("No matching constructor located with specified argument types.");
            }

            if (_generateProxy)
            {
                Activator = new ProxyContainerActivator(
                    _proxyDispatcher,
                    typeof(TConcrete),
                    constructor,
                    ConstructorArguments,
                    _interceptor);
            }
            else
            {
                Activator = new DynamicDelegateContainerActivator(
                    typeof(TConcrete),
                    constructor,
                    ConstructorArguments);
            }

            Build();
        }

        public void Use<TConcrete>(Func<IContainer, TConcrete> activationDelegate)
            where TConcrete : TBase
        {
            Activator = new DelegateContainerActivator<TConcrete>(activationDelegate);

            if (_generateProxy)
            {
                Activator = new TargetedProxyContainerActivator(
                    _proxyDispatcher,
                    typeof(TConcrete),
                    Activator,
                    _interceptor);
            }

            Build();
        }

        public void Use<TConcrete>(TConcrete singletonInstance)
            where TConcrete : TBase
        {
            LifecycleType = typeof(SingletonContainerLifecycle);
            Activator = new SpecifiedInstanceContainerActivator(singletonInstance);

            if (_generateProxy)
            {
                Activator = new TargetedProxyContainerActivator(
                    _proxyDispatcher,
                    typeof(TConcrete),
                    Activator,
                    _interceptor);
            }

            Build();
        }

        public IFluentForRegistration<TBase> WithConstructorArgument(string parameterName, object value)
        {
            ConstructorArguments.Add(parameterName, value);

            return this;
        }

        public Type BaseType => typeof(TBase);

        public Type ConcreteType { get; set; }

        public Type LifecycleType { get; set; } = typeof(TransientContainerLifecycle);

        public string Name { get; set; }

        public IContainerActivator Activator { get; set; }

        public ArgumentCollection ConstructorArguments { get; } = new ArgumentCollection();

        void Build()
        {
            _registry.Add(this);
        }
    }
}