using System;
using System.Reflection.Emit;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying.Generators
{
    class ClassMethodGenerator : MethodGenerator
    {
        readonly FieldBuilder _dispatcherField;
        readonly FieldBuilder _methodInfoField;
        readonly ClassMethodMetadata _methodMetadata;

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