using System;
using System.Reflection;
using Inception.Reflection;

namespace Inception.Proxying
{
	public class ProxyConstructorSelector : ConstructorSelector
	{
		public override ConstructorInfo Select(Type type, ArgumentCollection arguments)
		{
			ConstructorInfo result = null;

			var argumentTypes = arguments.GetArgumentTypes();

			var constructors = type.GetConstructors(ConstructorBindingFlags);

			for (var i = 0; i < constructors.Length; i++)
			{
				var constructor = constructors[i];

				var matched = IsMatch(constructor, argumentTypes);

				if (matched)
				{
					result = constructor;
					break;
				}
			}

			return result;
		}

		private bool IsMatch(ConstructorInfo constructor, Type[] argumentTypes)
		{
			var parameters = constructor.GetParameters();

			if (parameters.Length != argumentTypes.Length)
			{
				return false;
			}

			for (var i = 0; i < parameters.Length; i++)
			{
				var parameter = parameters[i];
				var argumentType = argumentTypes[i];

				if (!parameter.ParameterType.IsAssignableFrom(argumentType))
				{
					return false;
				}
			}

			return true;
		}
	}
}
