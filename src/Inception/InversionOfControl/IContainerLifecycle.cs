using System;

namespace Inception.InversionOfControl
{
    public interface IContainerLifecycle : IDisposable
    {
        object GetInstance(IContainer container, IRegistration registration);

        string Name { get; }
    }
}
