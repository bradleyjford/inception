using System;
using Inception.Proxying;
using Inception.Reflection;

namespace Inception.InversionOfControl.Configuration
{
	public class FluentForRegistration<TBase> : IRegistration, IFluentForRegistration<TBase>
	{
	    private readonly Registry _registry;
	    private string _name;
		private Type _concreteType;
		private Type _lifecycleType = typeof(TransientContainerLifecycle);
		private IContainerActivator _activator;

		private bool _generateProxy;
		private IProxyDispatcher _proxyDispatcher;
		private IInterceptor _interceptor;

		private readonly ArgumentCollection _constructorArguments = new ArgumentCollection();
		private IConstructorSelector _constructorSelector;

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
			_name = name;

			return this;
		}

		public IFluentForRegistration<TBase> Singleton()
		{
			_lifecycleType = typeof(SingletonContainerLifecycle);

			return this;
		}

		public IFluentForRegistration<TBase> Lifecycle<TLifecycle>()
			where TLifecycle : IContainerLifecycle
		{
			_lifecycleType = typeof(TLifecycle);

			return this;
		}

		public void Use<TConcrete>() 
			where TConcrete : TBase
		{
			_concreteType = typeof(TConcrete);

			if (_constructorSelector == null)
			{
				_constructorSelector = new ContainerConstructorSelector();
			}

			var constructor = _constructorSelector.Select(typeof(TConcrete), _constructorArguments);

			if (constructor == null)
			{
				// TODO: fix
				throw new Exception("No matching constructor located with specified argument types.");
			}

			if (_generateProxy)
			{
				_activator = new ProxyContainerActivator(
					_proxyDispatcher,
					typeof(TConcrete),
					constructor,
					_constructorArguments,
					_interceptor);
			}
			else
			{
				_activator = new DynamicDelegateContainerActivator(
					typeof(TConcrete),
					constructor, 
					_constructorArguments);
			}

		    Build();
		}

        private void Build()
        {
            _registry.Add(this);
        }

		public void Use<TConcrete>(Func<IContainer, TConcrete> activationDelegate)
			where TConcrete : TBase
		{
			_activator = new DelegateContainerActivator<TConcrete>(activationDelegate);

			if (_generateProxy)
			{
				_activator = new TargetedProxyContainerActivator(
					_proxyDispatcher,
					typeof(TConcrete),
					_activator,
					_interceptor);
			}

		    Build();
		}

		public void Use<TConcrete>(TConcrete singletonInstance)
			where TConcrete : TBase
		{
			_lifecycleType = typeof(SingletonContainerLifecycle); 
			_activator = new SpecifiedInstanceContainerActivator(singletonInstance);

			if (_generateProxy)
			{
				_activator = new TargetedProxyContainerActivator(
					_proxyDispatcher,
					typeof(TConcrete),
					_activator,
					_interceptor);
			}

		    Build();
		}

		public IFluentForRegistration<TBase> WithConstructorArgument(string parameterName, object value)
		{
			_constructorArguments.Add(parameterName, value);

			return this;
		}
		
		public Type BaseType
		{
			get { return typeof(TBase); }
		}

		public Type ConcreteType
		{
			get { return _concreteType; }
		}

		public Type LifecycleType
		{
			get { return _lifecycleType; }
		}

		public string Name
		{
			get { return _name; }
		}

		public IContainerActivator Activator
		{
			get { return _activator; }
		}

		public ArgumentCollection ConstructorArguments
		{
			get { return _constructorArguments; }
		}
	}
}
