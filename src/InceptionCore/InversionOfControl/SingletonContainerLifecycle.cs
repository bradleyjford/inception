using System;
using System.Collections.Concurrent;

namespace InceptionCore.InversionOfControl
{
    public sealed class SingletonContainerLifecycle : ManagedContainerLifecycle
    {
        readonly ConcurrentDictionary<IRegistration, object> _instances =
            new ConcurrentDictionary<IRegistration, object>();

        public override string Name => "Singleton";

        public override object GetInstance(IContainer container, IRegistration registration)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(SingletonContainerLifecycle));
            }

            return _instances.GetOrAdd(registration, k => registration.Activator.CreateInstance(container));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
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