using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal sealed class TypeMetadata
	{
		public static readonly TypeAttributes TypeAttributes =
			TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed;

		private readonly string _name;
		private readonly Type _baseType;
		private readonly Type[] _interfaces;
		private readonly DispatcherFieldMetadata _dispatcherField;
		private readonly FieldMetadata[] _fields;

		private readonly ConstructorMetadata[] _constructors;

		private readonly MethodMetadata[] _methods;
		
		private readonly PropertyMetadata[] _properties;
		private readonly EventMetadata[] _events;

		private readonly TargetMetadata[] _targets;

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
			_name = name;
			_baseType = baseType;
			_interfaces = interfaces;
			_dispatcherField = dispatcherField;
			_fields = fields;
			_constructors = constructors;
			_methods = methods;
			_properties = properties;
			_events = events;
			_targets = targets;
		}

		public string Name
		{
			get { return _name; }
		}

		public Type BaseType
		{
			get { return _baseType; }
		}

		public Type[] Interfaces
		{
			get { return _interfaces; }
		}

		public FieldMetadata[] Fields
		{
			get { return _fields; }
		}

		public ConstructorMetadata[] Constructors
		{
			get { return _constructors; }
		}

		public MethodMetadata[] Methods
		{
			get { return _methods; }
		}

		public PropertyMetadata[] Properties
		{
			get { return _properties; }
		}

		public EventMetadata[] Events
		{
			get { return _events; }
		}

		public TargetMetadata[] Targets
		{
			get { return _targets; }
		}

		public DispatcherFieldMetadata DispatcherField
		{
			get { return _dispatcherField; }
		}
	}
}