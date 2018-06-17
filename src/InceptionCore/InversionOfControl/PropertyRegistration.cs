using System;

namespace InceptionCore.InversionOfControl
{
    public sealed class PropertyRegistration
    {
        readonly Type _propertyType;

        public PropertyRegistration(Type propertyType)
        {
            _propertyType = propertyType;
        }
    }
}