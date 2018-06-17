using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;

namespace InceptionCore.Proxying.Metadata
{
    class ClassMethodMetadata : MethodMetadata
    {
        readonly MethodAttributes _methodAttributes;

        public ClassMethodMetadata(MethodInfo method)
            : base(method)
        {
            _methodAttributes = method.Attributes;

            _methodAttributes &= ~MethodAttributes.ReuseSlot;
            _methodAttributes |= MethodAttributes.NewSlot | MethodAttributes.Final;
        }

        public override MethodAttributes MethodAttributes => _methodAttributes;

        public override MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField)
        {
            var methodInfoField = methodMetadataFieldBuilders[this];

            return new ClassMethodGenerator(this, dispatcherField, methodInfoField);
        }
    }
}