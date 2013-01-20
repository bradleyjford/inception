using System;

namespace Inception.InversionOfControl
{
	public class SpecifiedInstanceContainerActivator : IContainerActivator
	{
		private readonly object _instance;

		public SpecifiedInstanceContainerActivator(object instance)
		{
			_instance = instance;
		}

		public object CreateInstance(IContainer container)
		{
			return _instance;
		}

		public Type ConcreteType
		{
			get { return _instance.GetType(); }
		}
	}
}
