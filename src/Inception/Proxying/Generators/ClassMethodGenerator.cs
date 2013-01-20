using System;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal class ClassMethodGenerator : MethodGenerator
	{
		private readonly ClassMethodMetadata _methodMetadata;
		private readonly FieldBuilder _dispatcherField;
		private readonly FieldBuilder _methodInfoField;

		public ClassMethodGenerator(
			ClassMethodMetadata methodMetadata,
			FieldBuilder dispatcherField,
			FieldBuilder methodInfoField)
		{
			_methodMetadata = methodMetadata;
			_dispatcherField = dispatcherField;
			_methodInfoField = methodInfoField;
		}

		public override MethodBuilder Generate(TypeBuilder typeBuilder)
		{
			var targetMethodGenerator = new BaseInvocationMethodGenerator(_methodMetadata);

			var targetMethod = targetMethodGenerator.Generate(typeBuilder);

			return GenerateMethod(
				typeBuilder,
				_methodMetadata,
				_dispatcherField,
				_methodInfoField,
				targetMethod);
		}
	}
}
