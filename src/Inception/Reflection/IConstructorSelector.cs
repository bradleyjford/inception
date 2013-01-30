using System;
using System.Reflection;

namespace Inception.Reflection
{
    public interface IConstructorSelector
    {
        ConstructorInfo Select(Type type, ArgumentCollection arguments);
    }
}
