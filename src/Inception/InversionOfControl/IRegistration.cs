using System;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
    public interface IRegistration
    {
        string Name { get; }
        Type BaseType { get; }
        Type ConcreteType { get; }
        Type LifecycleType { get; }
        IContainerActivator Activator { get; }
        ArgumentCollection ConstructorArguments { get; }
    }
}
