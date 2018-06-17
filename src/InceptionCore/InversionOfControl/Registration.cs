using System;
using InceptionCore.Reflection;

namespace InceptionCore.InversionOfControl
{
    public class Registration : IRegistration
    {
        public Registration(Type baseType, string name)
        {
            Name = name;
            BaseType = baseType;
        }

        public string Name { get; }

        public Type BaseType { get; }

        public Type ConcreteType { get; set; }

        public Type LifecycleType { get; set; }

        public IContainerActivator Activator { get; set; }

        public ArgumentCollection ConstructorArguments { get; } = new ArgumentCollection();
    }
}