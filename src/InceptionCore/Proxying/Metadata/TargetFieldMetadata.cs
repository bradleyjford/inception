using System;

namespace InceptionCore.Proxying.Metadata
{
    class TargetFieldMetadata : FieldMetadata
    {
        public TargetFieldMetadata(string name, Type fieldType)
            : base(name, fieldType, PrivateReadonlyFieldAttributes)
        {
        }
    }
}