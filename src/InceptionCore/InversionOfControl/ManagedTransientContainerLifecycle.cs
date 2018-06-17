using System;
using System.Collections.Concurrent;

namespace InceptionCore.InversionOfControl
{
    public sealed class ManagedTransientContainerLifecycle : ManagedContainerLifecycle
    {
        readonly ConcurrentBag<object> _instances = new ConcurrentBag<object>();
        readonly object _instancesLock = new object();

        public override string Name => "ManagedTransient";

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(ManagedTransientContainerLifecycle));
            }

            var instance = registration.Activator.CreateInstance(container);

            _instances.Add(instance);

            return instance;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var instance in _instances)
                {
                    var disposable = instance as IDisposable;

                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                lock (_instancesLock)
                {
                    _instances.Clear();
                }
            }
        }
    }
}