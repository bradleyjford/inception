using System;
using System.Linq;
using System.Reflection;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
	public abstract class ContainerActivator : IContainerActivator
	{
		private static readonly ArrayCastMethodCache ArrayCastDelegates =
			new ArrayCastMethodCache();

		public abstract object CreateInstance(IContainer container);

		protected object[] PrepareConstructorArguments(
			ConstructorInfo constructor, 
			ArgumentCollection constructorArguments, 
			IContainer container)
		{
			if (constructorArguments == null)
			{
				constructorArguments = new ArgumentCollection();
			}

			var parameters = constructor.GetParameters();

			var args = new object[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				args[i] = GetArgumentValue(parameters[i], constructorArguments, container);
			}

			return args;
		}

		private object GetArgumentValue(
			ParameterInfo parameter, 
			ArgumentCollection constructorArguments, 
			IContainer container)
		{
			object value;

			if (constructorArguments.Contains(parameter.Name))
			{
				value = constructorArguments[parameter.Name];
			}
			else if (parameter.ParameterType.IsArray)
			{
				var elementType = parameter.ParameterType.GetElementType();

				var arrayCastDelegate = ArrayCastDelegates[elementType];
				var instances = container.GetAllInstances(elementType).ToArray();

				value = arrayCastDelegate(instances);
			}
			else
			{
				value = container.GetInstance(parameter.ParameterType);
			}

			return value;
		}
	}
}
