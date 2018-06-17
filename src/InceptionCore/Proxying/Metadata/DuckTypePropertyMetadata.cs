using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    class DuckTypePropertyMetadata : PropertyMetadata
    {
        public DuckTypePropertyMetadata(
            PropertyInfo property,
            PropertyInfo targetProperty,
            MethodMetadata getMethod,
            MethodMetadata setMethod)
            : base(property, getMethod, setMethod)
        {
            TargetProperty = targetProperty;
        }

        public PropertyInfo TargetProperty { get; }
    }
}