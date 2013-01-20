using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators.ILGeneration;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal static class StaticConstructorGenerator
	{
		public static void Generate(
			TypeBuilder typeBuilder,
			TypeMetadata metadata, 
			MethodMetadataFieldBuilderMap methodMetadataBuilderFields)
		{
			var constructor = typeBuilder.DefineConstructor(
				MethodAttributes.Static,
				CallingConventions.Standard,
				Type.EmptyTypes);

			var il = constructor.GetILGenerator();

			for (var i = 0; i < metadata.Methods.Length; i++)
			{
				var method = metadata.Methods[i];

				if (method.MetadataField == null)
				{
					continue;
				}

				GenerateField(il, method, methodMetadataBuilderFields);
			}

			il.Emit(OpCodes.Ret);
		}

		private static void GenerateField(
			ILGenerator il, 
			MethodMetadata method,
			MethodMetadataFieldBuilderMap methodMetadataBuilderFields)
		{
			var field = methodMetadataBuilderFields[method];

			new StoreFieldStatement(field,
				new GetMethodHandleExpression(method.Method.DeclaringType, method.Method)
			).Emit(il);
		}
	}
}
