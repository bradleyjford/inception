using System;
using System.Reflection.Emit;
using Inception.Proxying.Metadata;

namespace Inception.Proxying.Generators
{
	internal class TargetedMethodGenerator : MethodGenerator
	{
		private readonly TargetedMethodMetadata _methodMetadata;
		private readonly FieldBuilder _dispatcherField;
		private readonly FieldBuilder _methodInfoField;
		private readonly FieldBuilder _targetField;

		public TargetedMethodGenerator(
			TargetedMethodMetadata methodMetadata,
			FieldBuilder dispatcherField,
			FieldBuilder methodInfoField,
			FieldBuilder targetField)
		{
			_methodMetadata = methodMetadata;
			_dispatcherField = dispatcherField;
			_methodInfoField = methodInfoField;
			_targetField = targetField;
		}

		public override MethodBuilder Generate(TypeBuilder typeBuilder)
		{
			var targetMethodGenerator = new TargetedInvocationMethodGenerator(
				_methodMetadata, 
				_targetField);

			var targetMethod = targetMethodGenerator.Generate(typeBuilder);

			var methodBuilder = GenerateMethod(
				typeBuilder,
				_methodMetadata,
				_dispatcherField,
				_methodInfoField,
				targetMethod);

			if (_methodMetadata.IsExplicitInterfaceImplementation)
			{
				typeBuilder.DefineMethodOverride(methodBuilder, _methodMetadata.Method);
			}

			return methodBuilder;
		}
	}
}
