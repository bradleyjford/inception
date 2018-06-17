using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;

namespace InceptionCore.Proxying.Metadata
{
    class TargetedMethodMetadata : MethodMetadata
    {
        readonly MethodAttributes _methodAttributes;
        readonly FieldMetadata _targetField;

        public TargetedMethodMetadata(MethodInfo method, FieldMetadata targetField, MethodInfo targetMethod)
            : base(method)
        {
            _targetField = targetField;
            TargetMethod = targetMethod;

            _methodAttributes = method.Attributes;

            _methodAttributes &= ~MethodAttributes.NewSlot;
            _methodAttributes &= ~MethodAttributes.Abstract;
            _methodAttributes |= MethodAttributes.ReuseSlot | MethodAttributes.Final;
        }

        public MethodInfo TargetMethod { get; }

        public override MethodAttributes MethodAttributes => _methodAttributes;

        public override MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField)
        {
            var methodInfoField = methodMetadataFieldBuilders[this];
            var targetField = instanceFieldBuilders[_targetField];

            return new TargetedMethodGenerator(this, dispatcherField, methodInfoField, targetField);
        }
    }
}