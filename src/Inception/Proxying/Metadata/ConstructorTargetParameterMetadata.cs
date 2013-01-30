using System;

namespace Inception.Proxying.Metadata
{
    internal class ConstructorTargetParameterMetadata : ConstructorParameterMetadata
    {
        private readonly FieldMetadata _instanceField;

        public ConstructorTargetParameterMetadata(int sequence, string name, FieldMetadata instanceField) 
            : base(sequence, name, instanceField.FieldType)
        {
            _instanceField = instanceField;
        }

        public FieldMetadata InstanceField
        {
            get { return _instanceField; }
        }
    }
}
