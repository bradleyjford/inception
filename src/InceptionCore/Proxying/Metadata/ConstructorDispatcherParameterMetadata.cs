using System;

namespace InceptionCore.Proxying.Metadata
{
    sealed class ConstructorDispatcherParameterMetadata : ConstructorParameterMetadata
    {
        readonly DispatcherFieldMetadata _instanceField;

        public ConstructorDispatcherParameterMetadata(int sequence, DispatcherFieldMetadata instanceField)
            : base(sequence, "dispatcher", typeof(IProxyDispatcher))
        {
            _instanceField = instanceField;
        }

        public FieldMetadata InstanceField => _instanceField;
    }
}