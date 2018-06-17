using System;

namespace InceptionCore.Proxying
{
    public class MixinInterfaceDefinition : InterfaceDefinition, IEquatable<MixinInterfaceDefinition>
    {
        public MixinInterfaceDefinition(Type type, Type mixinType, bool proxyInstantiated)
            : base(type)
        {
            MixinType = mixinType;
            ProxyInstantiated = proxyInstantiated;
        }

        public Type MixinType { get; }

        public bool ProxyInstantiated { get; }

        public bool Equals(MixinInterfaceDefinition other)
        {
            if (other == null)
            {
                return false;
            }

            return Type == other.Type &&
                   MixinType == other.MixinType &&
                   ProxyInstantiated == other.ProxyInstantiated;
        }

        public override bool Equals(object other)
        {
            var interfaceDefinition = other as MixinInterfaceDefinition;

            return Equals(interfaceDefinition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = HashCodeUtility.Seed;

                hash = HashCodeUtility.Hash(hash, typeof(MixinInterfaceDefinition));
                hash = HashCodeUtility.Hash(hash, Type);
                hash = HashCodeUtility.Hash(hash, MixinType);
                hash = HashCodeUtility.Hash(hash, ProxyInstantiated);

                return hash;
            }
        }
    }
}