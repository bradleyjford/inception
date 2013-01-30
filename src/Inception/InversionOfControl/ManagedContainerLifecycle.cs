using System;

namespace Inception.InversionOfControl
{
    public abstract class ManagedContainerLifecycle : IContainerLifecycle
    {
        private bool _isDisposed;

        public void Initialize()
        {
        }

        public abstract string Name { get; }

        public abstract object GetInstance(IContainer container, IRegistration registration);

        protected bool IsDisposed
        {
            get { return _isDisposed; }
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public void Dispose()
        {
            _isDisposed = true;

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~ManagedContainerLifecycle()
        {
            Dispose(false);
        }
    }
}