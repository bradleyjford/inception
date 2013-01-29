using System;

namespace Inception.Reflection
{
    public static class TypeExtensionMethods
    {
        public static bool CanBeInstantiated(this Type type)
        {
            return !(type.IsInterface || type.IsAbstract);
        }
    }
}
