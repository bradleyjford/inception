using System;

namespace InceptionCore.InversionOfControl
{
    public struct RegistrationKey : IEquatable<RegistrationKey>
    {
        public static RegistrationKey For(Type type)
        {
            return new RegistrationKey(type, null);
        }

        public static RegistrationKey For(Type type, string instanceName)
        {
            return new RegistrationKey(type, instanceName);
        }

        RegistrationKey(Type type, string instanceName)
        {
            Type = type;
            InstanceName = instanceName;
        }

        public Type Type { get; }

        public string InstanceName { get; }

        public bool Equals(RegistrationKey other)
        {
            return Equals(other.Type, Type) &&
                   Equals(other.InstanceName, InstanceName);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (other.GetType() != typeof(RegistrationKey))
            {
                return false;
            }

            return Equals((RegistrationKey)other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = HashCodeUtility.Seed;

                hash = HashCodeUtility.Hash(hash, Type);
                hash = HashCodeUtility.Hash(hash, InstanceName);

                return hash;
            }
        }

        public static bool operator ==(RegistrationKey left, RegistrationKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RegistrationKey left, RegistrationKey right)
        {
            return !left.Equals(right);
        }
    }
}