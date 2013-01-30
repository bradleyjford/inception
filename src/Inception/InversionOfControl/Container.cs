using System;
using System.Collections.Generic;
using System.Linq;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
    public class Container : IContainer
    {
        private readonly Registry _registry;

        private readonly RegistrationDictionary _onDemandRegistrations =
            new RegistrationDictionary();

        private readonly IConstructorSelector _defaultConstructorSelector = new ContainerConstructorSelector();

        private readonly LifecycleCache _lifecycles = new LifecycleCache();

        private readonly Container _parentContainer;
        private List<WeakReference> _childContainers;

        private bool _disposed;

        public Container(Action<Registry> configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            _registry = new Registry();

            configuration(_registry);
        }

        private Container(Container parentContainer, Registry registry)
        {
            _parentContainer = parentContainer;
            _registry = registry;
        }

        public IContainer ParentContainer
        {
            get { return _parentContainer; }
        }

        public IContainer CreateChildContainer(Action<Registry> configuration)
        {
            if (_disposed) throw new ObjectDisposedException("Container");
            if (configuration == null) throw new ArgumentNullException("configuration");

            var registry = new Registry();

            configuration(registry);

            var childContainer = new Container(this, registry);

            AddChildContainer(childContainer);

            return childContainer;
        }

        private void AddChildContainer(Container childContainer)
        {
            if (_childContainers == null)
            {
                _childContainers = new List<WeakReference>();
            }

            _childContainers.Add(new WeakReference(childContainer));
        }

        public T GetInstance<T>(string name) where T : class
        {
            return (T)GetInstanceCore(typeof(T), name);
        }

        public T GetInstance<T>() where T : class
        {
            return (T)GetInstanceCore(typeof(T), null);
        }

        public object GetInstance(Type type)
        {
            return GetInstanceCore(type, null);
        }

        public object GetInstance(Type type, string name)
        {
            return GetInstanceCore(type, name);
        }

        protected virtual object GetInstanceCore(Type type, string name)
        {
            if (_disposed) throw new ObjectDisposedException("Container");
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
            return GetAllInstances(typeof(T)).Cast<T>();
        }

        public IEnumerable<object> GetAllInstances(Type type)
        {
            if (_disposed) throw new ObjectDisposedException("Container");
            if (type == null) throw new ArgumentNullException("type");

            var registrations = _registry.Where(r => r.BaseType == type);

            foreach (var registration in registrations)
            {
                yield return ResolveInstance(RegistrationKey.For(type, registration.Name));
            }
        }

        protected virtual object ResolveInstance(RegistrationKey registrationKey)
        {
            if (_disposed) throw new ObjectDisposedException("Container");

            object result = null;

            if (_registry.Contains(registrationKey))
            {
                var registration = _registry[registrationKey];

                result = GetRegistrationInstance(registration);
            }
            else if (_parentContainer != null)
            {
                result = _parentContainer.ResolveInstance(registrationKey);
            }

            if (result == null)
            {
                result = ResolveNonRegisteredInstance(registrationKey);
            }

            return result;
        }

        protected virtual object ResolveNonRegisteredInstance(RegistrationKey registrationKey)
        {
            object instance = null;

            var type = registrationKey.Type;

            if (_onDemandRegistrations.ContainsKey(registrationKey))
            {
                var registration = (Registration)_onDemandRegistrations[registrationKey];

                instance = GetRegistrationInstance(registration);
            }
            else if (type.CanBeInstantiated())
            {
                var registration = CreateOnDemandRegistration(type);

                _onDemandRegistrations.Add(registrationKey, registration);

                instance = GetRegistrationInstance(registration);
            }

            return instance;
        }

        private Registration CreateOnDemandRegistration(Type type)
        {
            var registration = new Registration(type, null)
            {
                ConcreteType = type,
                LifecycleType = typeof(TransientContainerLifecycle)
            };

            registration.Activator = new DynamicDelegateContainerActivator(
                type,
                _defaultConstructorSelector.Select(type, new ArgumentCollection()),
                null);

            return registration;
        }

        private object GetRegistrationInstance(IRegistration registration)
        {
            return _lifecycles[registration.LifecycleType].GetInstance(this, registration);
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
