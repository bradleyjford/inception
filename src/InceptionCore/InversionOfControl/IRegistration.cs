using System;
using InceptionCore.Reflection;

namespace InceptionCore.InversionOfControl
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