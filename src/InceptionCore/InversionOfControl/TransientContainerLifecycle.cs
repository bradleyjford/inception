using System;

namespace InceptionCore.InversionOfControl
{
    public sealed class TransientContainerLifecycle : IContainerLifecycle
    {
        public string Name => "Transient";

        public object GetInstance(IContainer container, IRegistration registration)
        {
            return registration.Activator.CreateInstance(container);
        }

        public void Dispose()
        {
        }
    }
}