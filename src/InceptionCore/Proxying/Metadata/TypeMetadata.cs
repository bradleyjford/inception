using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    sealed class TypeMetadata
    {
        public static readonly TypeAttributes TypeAttributes =
            TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed;

        public TypeMetadata(
            string name,
            Type baseType,
            Type[] interfaces,
            DispatcherFieldMetadata dispatcherField,
            FieldMetadata[] fields,
            ConstructorMetadata[] constructors,
            MethodMetadata[] methods,
            PropertyMetadata[] properties,
            EventMetadata[] events,
            TargetMetadata[] targets)
        {
            Name = name;
            BaseType = baseType;
            Interfaces = interfaces;
            DispatcherField = dispatcherField;
            Fields = fields;
            Constructors = constructors;
            Methods = methods;
            Properties = properties;
            Events = events;
            Targets = targets;
        }

        public string Name { get; }

        public Type BaseType { get; }

        public Type[] Interfaces { get; }

        public FieldMetadata[] Fields { get; }

        public ConstructorMetadata[] Constructors { get; }

        public MethodMetadata[] Methods { get; }

        public PropertyMetadata[] Properties { get; }

        public EventMetadata[] Events { get; }

        public TargetMetadata[] Targets { get; }

        public DispatcherFieldMetadata DispatcherField { get; }
    }
}