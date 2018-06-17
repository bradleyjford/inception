using System;
using System.Reflection;
using InceptionCore.Reflection;

namespace InceptionCore.Proxying
{
    public class ProxyActivator : IProxyActivator
    {
        readonly IConstructorSelector _constructorSelector;

        public ProxyActivator(IConstructorSelector constructorSelector)
        {
            _constructorSelector = constructorSelector;
        }

        public object CreateInstance(Type type, ArgumentCollection constructorArguments)
        {
            var constructor = _constructorSelector.Select(type, constructorArguments);

            if (constructor == null)
            {
                throw new TypeLoadException(
                    "No matching constructor found on type with specified constructorArgument types.");
            }

            var args = PrepareConstructorArguments(constructor, constructorArguments);

            return Activator.CreateInstance(type, args);
        }

        object[] PrepareConstructorArguments(ConstructorInfo constructor, ArgumentCollection arguments)
        {
            var parameters = constructor.GetParameters();
            var parameterCount = parameters.Length;

            var result = new object[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                result[i] = arguments[parameters[i].Name];
            }

            return result;
        }
    }
}