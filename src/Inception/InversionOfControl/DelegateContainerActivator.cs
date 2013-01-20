using System;

namespace Inception.InversionOfControl
{
	public sealed class DelegateContainerActivator<T> : IContainerActivator
	{
		private readonly Func<IContainer, T> _activationDelegate;

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
