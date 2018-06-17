using System;

namespace InceptionCore.InversionOfControl.Configuration
{
    public static class FluentConfigurationExtensionMethods
    {
        public static IFluentForRegistration<T> For<T>(this Registry registry)
        {
            return new FluentForRegistration<T>(registry);
        }

        //public static IFluentPropertyForRegistration<T> ForAllPropertiesOfType<T>(this ContainerConfiguration configuration)
        //{
        //    var result = new IFluentPropertyForRegistration<T>();

        //    configuration.

        //    return result;
        //}
    }
}