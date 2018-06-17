using System;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class NonTargetedMethodGenerator : MethodGenerator
    {
        readonly FieldBuilder _dispatcherField;
        readonly FieldBuilder _methodInfoField;
        readonly MethodMetadata _methodMetadata;

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