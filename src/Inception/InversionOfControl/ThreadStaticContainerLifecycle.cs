using System;
using System.Collections.Generic;
using System.Threading;

namespace Inception.InversionOfControl
{
    public sealed class ThreadStaticContainerLifecycle : ManagedContainerLifecycle
    {
        private readonly Dictionary<int, SingletonContainerLifecycle> _threadStaticLifecycles =
            new Dictionary<int, SingletonContainerLifecycle>();

        private readonly ReaderWriterLockSlim _lock = 
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public override string Name
        {
            get { return "ThreadStatic"; }
        }

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed) throw new ObjectDisposedException("ThreadStaticContainerLifecycle");

            var lifecycle = GetThreadStaticLifecycle();

            return lifecycle.GetInstance(container, registration);
        }

        private SingletonContainerLifecycle GetThreadStaticLifecycle()
        {
            _lock.ExitUpgradeableReadLock();

            try
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;

                if (_threadStaticLifecycles.ContainsKey(threadId))
                {
                    return _threadStaticLifecycles[threadId];
                }

                _lock.EnterWriteLock();

                try
                {
                    var lifecycle = new SingletonContainerLifecycle();

                    _threadStaticLifecycles.Add(threadId, lifecycle);

                    return lifecycle;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var lifecycle in _threadStaticLifecycles.Values)
            {
                lifecycle.Dispose();
            }

            _threadStaticLifecycles.Clear();

            _lock.Dispose();
        }
    }
}
