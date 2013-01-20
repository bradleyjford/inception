using System;

namespace Inception.InversionOfControl.Configuration
{
	public static class FluentConfigurationExtensionMethods
	{
		public static IFluentForRegistration<T> For<T>(this ContainerConfiguration configuration)
		{
			var result = new FluentForRegistration<T>();

			configuration.AddRegistrationConfiguration(result);

			return result;
		}

		//public static IFluentPropertyForRegistration<T> ForAllPropertiesOfType<T>(this ContainerConfiguration configuration)
		//{
		//    var result = new IFluentPropertyForRegistration<T>();

		//    configuration.

		//    return result;
		//}
	}
}
