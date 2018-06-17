using System;

namespace InceptionCore.Proxying.Metadata
{
    sealed class TargetMetadata : MemberMetadata
    {
        readonly TargetFieldMetadata _instanceField;

        public TargetMetadata(
            string parameterName,
            Type type,
            Type targetType,
            bool isProxyInstantiated)
            : base(parameterName)
        {
            ParameterName = parameterName;
            Type = type;
            TargetType = targetType;
            IsProxyInstantiated = isProxyInstantiated;

            _instanceField = new TargetFieldMetadata("_" + parameterName, type);
        }

        public string ParameterName { get; }

        public Type Type { get; }

        public Type TargetType { get; }

        public bool IsProxyInstantiated { get; }

        public FieldMetadata InstanceField => _instanceField;
    }
}