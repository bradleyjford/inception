using System;
using Inception.Proxying;
using Inception.Reflection;

namespace Inception.InversionOfControl.Configuration
{
	public class FluentForRegistration<TBase> : IRegistration, IFluentForRegistration<TBase>
	{
		private string _name;
		private Type _concreteType;
		private Type _lifecycleType = typeof(TransientContainerLifecycle);
		private IContainerActivator _activator;
		

		private bool _generateProxy;
		private ProxyFactory _proxyFactory;
		private IProxyDispatcher _proxyDispatcher;
		private IInterceptor _interceptor;

		private ArgumentCollection _constructorArguments = new ArgumentCollection();
		private IConstructorSelector _constructorSelector;

		internal FluentForRegistration()
		{
			
		}

		public IFluentForRegistration<TBase> Proxy(ProxyFactory proxyFactory, IInterceptor interceptor)
		{
			_generateProxy = true;
			_proxyFactory = proxyFactory;
			_interceptor = interceptor;

			return this;
		}

		public IFluentForRegistration<TBase> Proxy(ProxyFactory proxyFactory, IProxyDispatcher proxyDispatcher)
		{
			_generateProxy = true;
			_proxyFactory = proxyFactory;
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
					_proxyFactory,
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
		}

		public void Use<TConcrete>(Func<IContainer, TConcrete> activationDelegate)
			where TConcrete : TBase
		{
			_activator = new DelegateContainerActivator<TConcrete>(activationDelegate);

			if (_generateProxy)
			{
				_activator = new TargetedProxyContainerActivator(
					_proxyFactory,
					_proxyDispatcher,
					typeof(TConcrete),
					_activator,
					_interceptor);
			}
		}

		public void Use<TConcrete>(TConcrete singletonInstance)
			where TConcrete : TBase
		{
			_lifecycleType = typeof(SingletonContainerLifecycle); 
			_activator = new SpecifiedInstanceContainerActivator(singletonInstance);

			if (_generateProxy)
			{
				_activator = new TargetedProxyContainerActivator(
					_proxyFactory,
					_proxyDispatcher,
					typeof(TConcrete),
					_activator,
					_interceptor);
			}
		}

		public IFluentForRegistration<TBase> WithCtorArgument(string parameterName, object value)
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
