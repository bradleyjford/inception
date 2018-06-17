using System;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    static class PropertyGenerator
    {
        public static void Generate(
            TypeBuilder typeBuilder,
            PropertyMetadata propertyMetadata,
            MethodBuilder getMethod,
            MethodBuilder setMethod)
        {
            var property = typeBuilder.DefineProperty(
                propertyMetadata.Name,
                propertyMetadata.PropertyAttributes,
                propertyMetadata.PropertyType,
                propertyMetadata.IndexerTypes);

            if (getMethod != null)
            {
                property.SetGetMethod(getMethod);
            }

            if (setMethod != null)
            {
                property.SetSetMethod(setMethod);
            }
        }
    }
}