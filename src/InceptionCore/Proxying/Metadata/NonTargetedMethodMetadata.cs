using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;

namespace InceptionCore.Proxying.Metadata
{
    class NonTargetedMethodMetadata : MethodMetadata
    {
        readonly MethodAttributes _methodAttributes;

        public NonTargetedMethodMetadata(MethodInfo method)
            : base(method)
        {
            _methodAttributes = method.Attributes;
            _methodAttributes &= ~MethodAttributes.Abstract;
            _methodAttributes &= ~MethodAttributes.NewSlot;
        }

        public override MethodAttributes MethodAttributes => _methodAttributes;

        public override MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField)
        {
            var methodInfoField = methodMetadataFieldBuilders[this];

            return new NonTargetedMethodGenerator(this, dispatcherField, methodInfoField);
        }
    }
}