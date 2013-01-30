using System;

namespace Inception.Proxying.Metadata
{
    internal sealed class ConstructorDispatcherParameterMetadata : ConstructorParameterMetadata
    {
        private readonly DispatcherFieldMetadata _instanceField;

        public ConstructorDispatcherParameterMetadata(int sequence, DispatcherFieldMetadata instanceField) 
            : base(sequence, "dispatcher", typeof(IProxyDispatcher))
        {
            _instanceField = instanceField;
        }

        public FieldMetadata InstanceField
        {
            get { return _instanceField; }
        }
    }
}
