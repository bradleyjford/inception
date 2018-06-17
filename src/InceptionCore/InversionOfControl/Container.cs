using System;
using System.Collections.Generic;
using System.Linq;

namespace InceptionCore.InversionOfControl
{
    public class Container : IContainer
    {
        readonly LifecycleCache _lifecycles = new LifecycleCache();

        readonly Container _parentContainer;
        readonly Registry _registry;

        List<WeakReference> _childContainers;

        bool _disposed;

        public Container(Action<Registry> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _registry = new Registry();

            configuration(_registry);
        }

        Container(Container parentContainer, Registry registry)
        {
            _parentContainer = parentContainer;
            _registry = registry;
        }

        public IContainer ParentContainer => _parentContainer;

        public T GetInstance<T>(string name) where T : class
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name == String.Empty)
            {
                throw new ArgumentException("Name must be provided.", nameof(name));
            }

            return (T)GetInstanceCore(typeof(T), name);
        }

        public T GetInstance<T>() where T : class
        {
            return (T)GetInstanceCore(typeof(T), null);
        }

        public object GetInstance(Type type)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Container));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetInstanceCore(type, null);
        }

        public object GetInstance(Type type, string name)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Container));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name == String.Empty)
            {
                throw new ArgumentException("Name must be provided.", nameof(name));
            }

            return GetInstanceCore(type, name);
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return GetAllInstances(typeof(T)).Cast<T>();
        }

        public IEnumerable<object> GetAllInstances(Type type)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Container));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var registrations = _registry.Where(r => r.BaseType == type);

            foreach (var registration in registrations)
            {
                yield return ResolveInstance(RegistrationKey.For(type, registration.Name));
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public IContainer CreateChildContainer(Action<Registry> configuration)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Container");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var registry = new Registry();

            configuration(registry);

            var childContainer = new Container(this, registry);

            AddChildContainer(childContainer);

            return childContainer;
        }

        void AddChildContainer(Container childContainer)
        {
            if (_childContainers == null)
            {
                _childContainers = new List<WeakReference>();
            }

            _childContainers.Add(new WeakReference(childContainer));
        }

        protected object GetInstanceCore(Type type, string name)
        {
            var registrationKey = RegistrationKey.For(type, name);

            var result = ResolveInstance(registrationKey);

            if (result == null)
            {
                throw new NullReferenceException("Could not get instance of type " + type.FullName);
            }

            return result;
        }

        protected object ResolveInstance(RegistrationKey registrationKey)
        {
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
                throw new NullReferenceException($"No registration for {registrationKey.Type} with name {registrationKey.InstanceName}.");
            }

            return result;
        }

        protected T ResolveInstance<T>(RegistrationKey registrationKey)
            where T : class 
        {
            T result = null;

            if (_registry.Contains(registrationKey))
            {
                var registration = _registry[registrationKey];

                result = GetRegistrationInstance<T>(registration);
            }
            else if (_parentContainer != null)
            {
                result = _parentContainer.ResolveInstance<T>(registrationKey);
            }

            if (result == null)
            {
                throw new NullReferenceException($"No registration for {registrationKey.Type} with name {registrationKey.InstanceName}.");
            }

            return result;
        }

        T GetRegistrationInstance<T>(IRegistration registration)
        {
            return (T)_lifecycles[registration.LifecycleType].GetInstance(this, registration);
        }

        object GetRegistrationInstance(IRegistration registration)
        {
            return _lifecycles[registration.LifecycleType].GetInstance(this, registration);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
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