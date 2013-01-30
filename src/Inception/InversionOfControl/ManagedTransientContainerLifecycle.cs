using System;
using System.Collections.Generic;

namespace Inception.InversionOfControl
{
    public sealed class ManagedTransientContainerLifecycle : ManagedContainerLifecycle
    {
        private readonly List<object> _instances = new List<object>();
        private readonly object _instancesLock = new object();

        public override string Name
        {
            get { return "ManagedTransient"; }
        }

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed) throw new ObjectDisposedException("ManagedTransientContainerLifecycle");

            var instance = registration.Activator.CreateInstance(container);

            lock (_instancesLock)
            {
                _instances.Add(instance);
            }

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
