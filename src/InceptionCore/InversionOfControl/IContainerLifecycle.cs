using System;

namespace InceptionCore.InversionOfControl
{
    public interface IContainerLifecycle : IDisposable
    {
        string Name { get; }
        object GetInstance(IContainer container, IRegistration registration);
    }
}