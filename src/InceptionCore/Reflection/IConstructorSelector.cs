using System;
using System.Reflection;

namespace InceptionCore.Reflection
{
    public interface IConstructorSelector
    {
        ConstructorInfo Select(Type type, ArgumentCollection arguments);
    }
}