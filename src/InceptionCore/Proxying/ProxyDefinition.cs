using System;
using System.Linq;

namespace InceptionCore.Proxying
{
    public abstract class ProxyDefinition : IProxyDefinitionElement, IEquatable<ProxyDefinition>
    {
        const string ProxyTypeNameFormat = @"<Proxy{0}>{1}";

        protected ProxyDefinition(Type primaryType, InterfaceDefinition[] interfaces)
        {
            Type = primaryType;
            Interfaces = interfaces ?? new InterfaceDefinition[0];
        }

        public InterfaceDefinition[] Interfaces { get; }

        public string ProxyTypeName => string.Format(ProxyTypeNameFormat, GetHashCode(), Type.Name);

        public bool Equals(ProxyDefinition other)
        {
            if (other == null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            var interfacesEqual = Interfaces.SequenceEqual(other.Interfaces);

            return Type == other.Type && interfacesEqual;
        }

        public Type Type { get; }

        public override bool Equals(object other)
        {
            var definition = other as ProxyDefinition;

            return Equals(definition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = HashCodeUtility.Seed;

                hash = HashCodeUtility.Hash(hash, GetType());
                hash = HashCodeUtility.Hash(hash, Type);
                hash = HashCodeUtility.Hash(hash, Interfaces);

                return hash;
            }
        }
    }
}