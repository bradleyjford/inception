using System;

namespace InceptionCore.InversionOfControl
{
    public sealed class DelegateContainerActivator<T> : IContainerActivator
    {
        readonly Func<IContainer, T> _activationDelegate;

        public DelegateContainerActivator(Func<IContainer, T> activationDelegate)
        {
            _activationDelegate = activationDelegate;
        }

        public object CreateInstance(IContainer container)
        {
            return _activationDelegate(container);
        }
    }
}