using System;

namespace InceptionCore.InversionOfControl
{
    public abstract class ManagedContainerLifecycle : IContainerLifecycle
    {
        protected bool IsDisposed { get; set; }

        public abstract string Name { get; }

        public abstract object GetInstance(IContainer container, IRegistration registration);

        public void Initialize()
        {
        }

        public void Dispose()
        {
            IsDisposed = true;

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources
            }
        }

        ~ManagedContainerLifecycle()
        {
            Dispose(false);
        }
    }
}