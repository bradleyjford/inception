using System;

namespace InceptionCore.InversionOfControl
{
    public class SpecifiedInstanceContainerActivator : IContainerActivator
    {
        readonly object _instance;

        public SpecifiedInstanceContainerActivator(object instance)
        {
            _instance = instance;
        }

        public Type ConcreteType => _instance.GetType();

        public object CreateInstance(IContainer container)
        {
            return _instance;
        }
    }
}