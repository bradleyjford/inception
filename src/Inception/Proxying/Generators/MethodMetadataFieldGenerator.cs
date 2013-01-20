using System;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal static class MethodMetadataFieldGenerator
	{
		public static MethodMetadataFieldBuilderMap Generate(TypeBuilder typeBuilder, TypeMetadata metadata)
		{
			var methodCount = metadata.Methods.Length;

			var methodMetadataFields = new MethodMetadataFieldBuilderMap(methodCount);

			for (var i = 0; i < methodCount; i++)
			{
				var method = metadata.Methods[i];
				var metadataField = method.MetadataField;

				var field = typeBuilder.DefineField(
					metadataField.Name, 
					metadataField.FieldType,
					Type.EmptyTypes, 
					Type.EmptyTypes, 
					metadataField.FieldAttributes);

				methodMetadataFields.Add(method, field);
			}

			return methodMetadataFields;
		}
	}
}
