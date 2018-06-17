using System;
using System.Reflection;

namespace InceptionCore.Reflection
{
    public abstract class ConstructorSelector : IConstructorSelector
    {
        protected const BindingFlags ConstructorBindingFlags = BindingFlags.Public | BindingFlags.Instance;

        public abstract ConstructorInfo Select(Type type, ArgumentCollection arguments);

        protected virtual bool ArgumentSuppliedForParameter(ParameterInfo parameter, ArgumentCollection arguments)
        {
            if (!arguments.TryGetValue(parameter.Name, out var argument))
            {
                return false;
            }

            var argumentType = argument.GetType();

            if (!parameter.ParameterType.IsAssignableFrom(argumentType))
            {
                return false;
            }

            return true;
        }
    }
}