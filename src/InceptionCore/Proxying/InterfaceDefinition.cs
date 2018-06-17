using System;

namespace InceptionCore.Proxying
{
    public abstract class InterfaceDefinition : IProxyDefinitionElement
    {
        protected InterfaceDefinition(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public abstract override bool Equals(object other);

        public abstract override int GetHashCode();
    }
}