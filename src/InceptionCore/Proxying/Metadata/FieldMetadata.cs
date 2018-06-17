using System;
using System.Diagnostics;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    [DebuggerDisplay("{Name}")]
    class FieldMetadata : MemberMetadata
    {
        protected const FieldAttributes PrivateReadonlyFieldAttributes =
            FieldAttributes.Private | FieldAttributes.InitOnly;

        protected const FieldAttributes PrivateStaticReadonlyFieldAttributes =
            FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly;

        public FieldMetadata(
            string name,
            Type fieldType,
            FieldAttributes fieldAttributes)
            : base(name)
        {
            FieldType = fieldType;
            FieldAttributes = fieldAttributes;
        }

        public Type FieldType { get; }

        public FieldAttributes FieldAttributes { get; }
    }
}