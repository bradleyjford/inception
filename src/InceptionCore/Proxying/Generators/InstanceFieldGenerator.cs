using System;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    static class InstanceFieldGenerator
    {
        public static FieldMetadataFieldBuilderMap Generate(TypeBuilder typeBuilder, TypeMetadata typeMetadata)
        {
            var fieldCount = typeMetadata.Fields.Length;

            var fieldMap = new FieldMetadataFieldBuilderMap(fieldCount);

            for (var i = 0; i < fieldCount; i++)
            {
                var fieldMetadata = typeMetadata.Fields[i];

                var field = GenerateField(typeBuilder, fieldMetadata);

                fieldMap.Add(fieldMetadata, field);
            }

            return fieldMap;
        }

        static FieldBuilder GenerateField(TypeBuilder typeBuilder, FieldMetadata fieldMetadata)
        {
            var field = typeBuilder.DefineField(
                fieldMetadata.Name,
                fieldMetadata.FieldType,
                fieldMetadata.FieldAttributes);

            return field;
        }
    }
}