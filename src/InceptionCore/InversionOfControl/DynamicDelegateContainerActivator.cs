using System;
using System.Reflection;
using InceptionCore.Reflection;

namespace InceptionCore.InversionOfControl
{
    public sealed class DynamicDelegateContainerActivator : ContainerActivator
    {
        readonly DynamicDelegateActivator _activator;
        readonly ConstructorInfo _constructor;

        readonly ArgumentCollection _constructorArguments;

        public DynamicDelegateContainerActivator(
            Type type,
            ConstructorInfo constructor,
            ArgumentCollection constructorArguments)
        {
            _constructor = constructor;
            _constructorArguments = constructorArguments;

            _activator = new DynamicDelegateActivator(type, constructor);
        }

        public override object CreateInstance(IContainer container)
        {
            var args = PrepareConstructorArguments(_constructor, _constructorArguments, container);

            return _activator.CreateInstance(args);
        }
    }
}