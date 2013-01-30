using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
    internal class DuckTypePropertyMetadata : PropertyMetadata
    {
        private readonly PropertyInfo _targetProperty;

        public DuckTypePropertyMetadata(
            PropertyInfo property, 
            PropertyInfo targetProperty,
            MethodMetadata getMethod, 
            MethodMetadata setMethod) 
            : base(property, getMethod, setMethod)
        {
            _targetProperty = targetProperty;
        }

        public PropertyInfo TargetProperty
        {
            get { return _targetProperty; }
        }
    }
}
