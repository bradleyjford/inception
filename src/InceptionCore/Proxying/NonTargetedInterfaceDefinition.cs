using System;

namespace InceptionCore.Proxying
{
    public class NonTargetedInterfaceDefinition : InterfaceDefinition, IEquatable<NonTargetedInterfaceDefinition>
    {
        public NonTargetedInterfaceDefinition(Type type)
            : base(type)
        {
            if (!type.IsInterface)
            {
                throw new ArgumentException("Argument type must be an interface type.", "type");
            }
        }

        public bool Equals(NonTargetedInterfaceDefinition other)
        {
            if (other == null)
            {
                return false;
            }

            return Type == other.Type;
        }

        public override bool Equals(object other)
        {
            var interfaceDefinition = other as NonTargetedInterfaceDefinition;

            return Equals(interfaceDefinition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = HashCodeUtility.Seed;

                hash = HashCodeUtility.Hash(hash, typeof(NonTargetedInterfaceDefinition));
                hash = HashCodeUtility.Hash(hash, Type);

                return hash;
            }
        }
    }
}