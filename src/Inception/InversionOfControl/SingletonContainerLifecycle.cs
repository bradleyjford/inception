using System;
using System.Collections.Generic;
using System.Threading;

namespace Inception.InversionOfControl
{
    public sealed class SingletonContainerLifecycle : ManagedContainerLifecycle
    {
        private readonly Dictionary<IRegistration, object> _instances =
            new Dictionary<IRegistration, object>();

        private readonly ReaderWriterLockSlim _lock = 
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public override string Name
        {
            get { return "Singleton"; }
        }

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed) throw new ObjectDisposedException("SingletonContainerLifecycle");

            object instance;

            _lock.EnterUpgradeableReadLock();

            try
            {
                if (!_instances.ContainsKey(registration))
                {
                    _lock.EnterWriteLock();

                    try
                    {
                        instance = registration.Activator.CreateInstance(container);

                        _instances.Add(registration, instance);
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }
                else
                {
                    instance = _instances[registration];
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }

            return instance;
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _lock.Dispose();

                foreach (var instance in _instances)
                {
                    var disposable = instance.Value as IDisposable;

                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }

            _instances.Clear();
        }
    }
}
