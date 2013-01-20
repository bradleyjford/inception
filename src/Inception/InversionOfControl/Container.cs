using System;
using System.Collections.Generic;
using System.Linq;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
	public class Container : IContainer, IDisposable
	{
		private readonly Registry _registry;

		private readonly RegistrationDictionary _adhocRegistrations =
			new RegistrationDictionary();

		private readonly IConstructorSelector _adhocConstructorSelector = new ContainerConstructorSelector();

		private readonly LifecycleCache _lifecycles = new LifecycleCache();

		private readonly Container _parentContainer;
		private List<WeakReference> _childContainers;

		private bool _disposed;

		public Container(Action<ContainerConfiguration> configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			_registry = new Registry(configuration);

			_registry.Build(this);
		}

		private Container(Container parentContainer, Registry registry)
		{
			_parentContainer = parentContainer;
			_registry = registry;

			registry.Build(this);
		}

		public IContainer ParentContainer
		{
			get { return _parentContainer; }
		}

		public IContainer CreateChildContainer(Action<ContainerConfiguration> config)
		{
			if (config == null) throw new ArgumentNullException("config");

			if (_disposed) throw new ObjectDisposedException("Container");

			if (_childContainers == null)
			{
				_childContainers = new List<WeakReference>();
			}

			var registry = new Registry(config);

			var childContainer = new Container(this, registry);

			_childContainers.Add(new WeakReference(childContainer));

			return childContainer;
		}

		public T GetInstance<T>(string name) where T : class
		{
			return (T)GetInstance(typeof(T), name);
		}

		public T GetInstance<T>() where T : class
		{
			return (T)GetInstance(typeof(T), null);
		}

		public object GetInstance(Type type)
		{
			return GetInstance(type, null);
		}

		public object GetInstance(Type type, string name)
		{
			if (type == null) throw new ArgumentNullException("type");

			var registrationKey = RegistrationKey.For(type, name);

			var result = ResolveInstance(registrationKey);

			if (result == null)
			{
				throw new NullReferenceException("Could not get instance of type " + type.FullName);
			}

			return result;
		}

		public IEnumerable<T> GetAllInstances<T>()
		{
			var registrations = _registry.Where(r => r.BaseType == typeof(T));

			foreach (var registration in registrations)
			{
				yield return (T)ResolveInstance(RegistrationKey.For(typeof(T), registration.Name));
			}
		}

		public IEnumerable<object> GetAllInstances(Type type)
		{
			if (type == null) throw new ArgumentNullException("type");

			var registrations = _registry.Where(r => r.BaseType == type);

			foreach (var registration in registrations)
			{
				yield return ResolveInstance(RegistrationKey.For(type, registration.Name));
			}
		}

		protected object ResolveInstance(Type type)
		{
			return ResolveInstance(RegistrationKey.For(type));
		}

		protected virtual object ResolveInstance(RegistrationKey registrationKey)
		{
			if (_disposed) throw new ObjectDisposedException("Container");

			object result = null;

			if (_registry.Contains(registrationKey))
			{
				var registration = _registry[registrationKey];

				result = _lifecycles[registration.LifecycleType].GetInstance(this, registration);
			}
			else if (_parentContainer != null)
			{
				result = _parentContainer.ResolveInstance(registrationKey);
			}

			if (result == null)
			{
				result = ResolveAdhocInstance(registrationKey);
			}

			return result;
		}

		protected virtual object ResolveAdhocInstance(RegistrationKey registrationKey)
		{
			var type = registrationKey.Type;

			object instance = null;

			Registration registration;

			if (_adhocRegistrations.ContainsKey(registrationKey))
			{
				registration = (Registration)_adhocRegistrations[registrationKey];

				instance = _lifecycles[registration.LifecycleType].GetInstance(this, registration);
			}
			else if (!type.IsAbstract && !type.IsInterface)
			{
				registration = new Registration(type, null)
				{
					ConcreteType = type,
					LifecycleType = typeof(TransientContainerLifecycle)
				};

				registration.Activator = new DynamicDelegateContainerActivator(
					type,
					_adhocConstructorSelector.Select(type, new ArgumentCollection()),
					null);

				_adhocRegistrations.Add(registrationKey, registration);

				instance = _lifecycles[registration.LifecycleType].GetInstance(this, registration);
			}

			return instance;
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!_disposed)
				{
					_disposed = true;

					if (_childContainers != null)
					{
						foreach (var childContainerReference in _childContainers.Where(wr => wr.IsAlive))
						{
							((IDisposable)childContainerReference.Target).Dispose();
						}
					}

					_lifecycles.Dispose();
				}
			}
		}

	}
}
