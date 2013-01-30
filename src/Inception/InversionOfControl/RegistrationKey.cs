using System;

namespace Inception.InversionOfControl
{
    public struct RegistrationKey : IEquatable<RegistrationKey>
    {
        private readonly Type _type;
        private readonly string _instanceName;

        public static RegistrationKey For(Type type)
        {
            return new RegistrationKey(type, null);
        }

        public static RegistrationKey For(Type type, string instanceName)
        {
            return new RegistrationKey(type, instanceName);
        }

        private RegistrationKey(Type type, string instanceName)
        {
            _type = type;
            _instanceName = instanceName;
        }

        public Type Type
        {
            get { return _type; }
        }

        public string InstanceName
        {
            get { return _instanceName; }
        }

        public bool Equals(RegistrationKey other)
        {
            return Equals(other._type, _type) && 
                Equals(other._instanceName, _instanceName);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (other.GetType() != typeof(RegistrationKey)) return false;
            
            return Equals((RegistrationKey)other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = HashCodeUtility.Seed;

                hash = HashCodeUtility.Hash(hash, _type);
                hash = HashCodeUtility.Hash(hash, _instanceName);

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
