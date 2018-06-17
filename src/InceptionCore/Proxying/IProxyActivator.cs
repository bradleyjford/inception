using System;
using InceptionCore.Reflection;

namespace InceptionCore.Proxying
{
    public interface IProxyActivator
    {
        object CreateInstance(Type type, ArgumentCollection constructorArguments);
    }
}