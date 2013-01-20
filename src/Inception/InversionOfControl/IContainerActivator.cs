using System;

namespace Inception.InversionOfControl
{
	public interface IContainerActivator
	{
		object CreateInstance(IContainer container);
	}
}
