using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;

namespace InceptionCore.Proxying.Metadata
{
    class DuckTypeMethodMetadata : MethodMetadata
    {
        readonly MethodAttributes _methodAttributes;

        public DuckTypeMethodMetadata(MethodInfo method, MethodInfo targetMethod)
            : base(method)
        {
            TargetMethod = targetMethod;
            _methodAttributes = method.Attributes;

            _methodAttributes &= ~MethodAttributes.Abstract;
            _methodAttributes &= ~MethodAttributes.NewSlot;

            _methodAttributes |= MethodAttributes.ReuseSlot | MethodAttributes.Final;
        }

        public override MethodAttributes MethodAttributes => _methodAttributes;

        public MethodInfo TargetMethod { get; }

        public override MethodGenerator CreateGenerator(
            FieldMetadataFieldBuilderMap instanceFieldBuilders,
            MethodMetadataFieldBuilderMap methodMetadataFieldBuilders,
            FieldBuilder dispatcherField)
        {
            return new DuckTypeMethodGenerator(this);
        }
    }
}