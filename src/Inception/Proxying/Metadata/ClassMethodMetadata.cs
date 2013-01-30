using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;

namespace Inception.Proxying.Metadata
{
    internal class ClassMethodMetadata : MethodMetadata
    {
        private readonly MethodAttributes _methodAttributes;

        public ClassMethodMetadata(MethodInfo method) 
            : base(method)
        {
            _methodAttributes = method.Attributes;

            _methodAttributes &= ~MethodAttributes.ReuseSlot;
            _methodAttributes |= MethodAttributes.NewSlot | MethodAttributes.Final;
        }

        public override MethodAttributes MethodAttributes
        {
            get { return _methodAttributes; }
        }

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
