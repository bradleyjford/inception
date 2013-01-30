using System;
using System.Reflection;
using Inception.Reflection;

namespace Inception.InversionOfControl
{
    public class ContainerConstructorSelector : ConstructorSelector
    {
        public override ConstructorInfo Select(Type type, ArgumentCollection arguments)
        {
            ConstructorInfo result = null;
            var parameterCount = -1;

            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();

                var hasUnresolvedPrimitiveParameter = false;
                var matchedParameters = 0;

                foreach (var parameter in parameters)
                {
                    if (IsPrimitiveType(parameter.ParameterType))
                    {
                        if (ArgumentSuppliedForParameter(parameter, arguments))
                        {
                            matchedParameters++;
                        }
                        else
                        {
                            hasUnresolvedPrimitiveParameter = true;
                            break;
                        }
                    }
                }

                if (!hasUnresolvedPrimitiveParameter &&
                    parameters.Length > parameterCount &&
                    arguments.Count == matchedParameters)
                {
                    parameterCount = parameters.Length;

                    result = constructor;
                }
            }

            return result;
        }

        private bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type == typeof(string);
        }
    }
}
