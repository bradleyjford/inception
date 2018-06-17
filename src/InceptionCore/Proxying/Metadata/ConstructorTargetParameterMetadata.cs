using System;

namespace InceptionCore.Proxying.Metadata
{
    class ConstructorTargetParameterMetadata : ConstructorParameterMetadata
    {
        public ConstructorTargetParameterMetadata(int sequence, string name, FieldMetadata instanceField)
            : base(sequence, name, instanceField.FieldType)
        {
            InstanceField = instanceField;
        }

        public FieldMetadata InstanceField { get; }
    }
}