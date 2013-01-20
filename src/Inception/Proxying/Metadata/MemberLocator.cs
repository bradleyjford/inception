using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal static class MemberLocator
	{
		public static MethodInfo LocateMatchingMethod(MethodInfo method, Type inType)
		{
			var parameters = method.GetParameters();
			var parameterTypes = new Type[parameters.Length];

			for (var i = 0; i < parameters.Length; i++)
			{
				parameterTypes[i] = parameters[i].ParameterType;
			}

			return inType.GetMethod(
				method.Name,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				CallingConventions.Standard | CallingConventions.HasThis,
				parameterTypes,
				null);
		}

		public static PropertyInfo LocateMatchingProperty(PropertyInfo property, Type inType)
		{
			var indexParameters = property.GetIndexParameters();
			var indexParameterTypes = new Type[indexParameters.Length];

			for (var i = 0; i < indexParameters.Length; i++)
			{
				indexParameterTypes[i] = indexParameters[i].ParameterType;
			}

			return inType.GetProperty(
				property.Name,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				property.PropertyType,
				indexParameterTypes,
				null);
		}

		public static EventInfo LocateMatchingEvent(EventInfo @event, Type inType)
		{
			return inType.GetEvent(
				@event.Name,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		}
	}
}
