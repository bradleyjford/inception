using System;
using System.Collections.Concurrent;
using System.Threading;

namespace InceptionCore.InversionOfControl
{
    public sealed class ThreadStaticContainerLifecycle : ManagedContainerLifecycle
    {
        readonly ConcurrentDictionary<int, SingletonContainerLifecycle> _threadStaticLifecycles =
            new ConcurrentDictionary<int, SingletonContainerLifecycle>();

        public override string Name => "ThreadStatic";

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("ThreadStaticContainerLifecycle");
            }

            var lifecycle = GetThreadStaticLifecycle();

            return lifecycle.GetInstance(container, registration);
        }

        SingletonContainerLifecycle GetThreadStaticLifecycle()
        {
            return _threadStaticLifecycles.GetOrAdd(Thread.CurrentThread.ManagedThreadId,
                k => new SingletonContainerLifecycle());
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var lifecycle in _threadStaticLifecycles.Values)
            {
                lifecycle.Dispose();
            }

            _threadStaticLifecycles.Clear();
        }
    }
}