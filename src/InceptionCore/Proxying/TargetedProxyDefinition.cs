using System;

namespace InceptionCore.Proxying
{
    public abstract class TargetedProxyDefinition : ProxyDefinition, IEquatable<TargetedProxyDefinition>
    {
        protected TargetedProxyDefinition(
            Type type,
            Type targetType,
            InterfaceDefinition[] interfaces)
            : base(type, interfaces)
        {
            TargetType = targetType;
        }

        public Type TargetType { get; }

        public bool Equals(TargetedProxyDefinition other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return base.Equals(other) && TargetType == other.TargetType;
        }

        public override bool Equals(object other)
        {
            var definition = other as TargetedProxyDefinition;

            return Equals(definition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode();

                hash = HashCodeUtility.Hash(hash, TargetType);

                return hash;
            }
        }
    }
}