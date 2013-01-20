using System;

namespace Inception.InversionOfControl
{
	public sealed class TransientContainerLifecycle : IContainerLifecycle
	{
		public string Name
		{
			get { return "Transient"; }
		}

		public object GetInstance(IContainer container, IRegistration registration)
		{
			return registration.Activator.CreateInstance(container);
		}

		public void Dispose()
		{
			
		}
	}
}
