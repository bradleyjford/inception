using System;

namespace Inception.Proxying
{
    public abstract class TargetedProxyDefinition : ProxyDefinition, IEquatable<TargetedProxyDefinition>
    {
        private readonly Type _targetType;

        protected TargetedProxyDefinition(
            Type type, 
            Type targetType, 
            InterfaceDefinition[] interfaces) 
            : base(type, interfaces)
        {
            _targetType = targetType;
        }

        public Type TargetType
        {
            get { return _targetType; }
        }

        public bool Equals(TargetedProxyDefinition other)
        {
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;

            return base.Equals(other) && _targetType == other._targetType;
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
