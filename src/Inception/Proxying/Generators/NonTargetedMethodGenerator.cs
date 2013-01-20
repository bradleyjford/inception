using System;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal class NonTargetedMethodGenerator : MethodGenerator
	{
		private readonly MethodMetadata _methodMetadata;
		private readonly FieldBuilder _dispatcherField;
		private readonly FieldBuilder _methodInfoField;

		public NonTargetedMethodGenerator(
			MethodMetadata methodMetadata,
			FieldBuilder dispatcherField,
			FieldBuilder methodInfoField)
		{
			_methodMetadata = methodMetadata;
			_dispatcherField = dispatcherField;
			_methodInfoField = methodInfoField;
		}

		public override MethodBuilder Generate(TypeBuilder typeBuilder)
		{
			return GenerateMethod(
				typeBuilder,
				_methodMetadata,
				_dispatcherField,
				_methodInfoField,
				null);
		} 
	}
}
