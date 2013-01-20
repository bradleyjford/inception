using System;
using System.Reflection;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
	public sealed class DynamicDelegateContainerActivator : ContainerActivator
	{
		private readonly ConstructorInfo _constructor;
		private readonly DynamicDelegateActivator _activator;

		private readonly ArgumentCollection _constructorArguments;

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
