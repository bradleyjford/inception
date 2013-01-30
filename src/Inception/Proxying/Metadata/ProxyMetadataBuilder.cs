using System;
using System.Collections.Generic;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
    /// <summary>
    /// Given a ProxyDefinition, build a TypeMetadata object graph.
    /// </summary>
    internal abstract class ProxyMetadataBuilder
    {
        private const BindingFlags ProxyBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private readonly ProxyDefinition _proxyDefinition;
        private readonly Type _baseType;

        private readonly List<Type> _interfaces = new List<Type>(); 

        private readonly List<FieldMetadata> _fields = new List<FieldMetadata>(); 
        private readonly List<ConstructorMetadata> _constructors = new List<ConstructorMetadata>(); 
        private readonly List<MethodMetadata> _methods = new List<MethodMetadata>(); 
        private readonly List<PropertyMetadata> _properties = new List<PropertyMetadata>();
        private readonly List<EventMetadata> _events = new List<EventMetadata>();

        private readonly List<TargetMetadata> _mixins = new List<TargetMetadata>();

        private DispatcherFieldMetadata _dispatcherField;

        protected ProxyMetadataBuilder(ProxyDefinition proxyDefinition, Type baseType)
        {
            _proxyDefinition = proxyDefinition;
            _baseType = baseType;

            _dispatcherField = new DispatcherFieldMetadata();
        }

        protected DispatcherFieldMetadata DispatcherField
        {
            get { return _dispatcherField; }
        }

        protected IList<Type> Interfaces
        {
            get { return _interfaces; }
        }

        protected IList<FieldMetadata> Fields
        {
            get { return _fields; }
        }

        protected IList<TargetMetadata> Mixins
        {
            get { return _mixins; }
        }

        protected IList<ConstructorMetadata> Constructors
        {
            get { return _constructors; }
        }

        protected IList<MethodMetadata> Methods
        {
            get { return _methods; }
        }

        protected IList<PropertyMetadata> Properties
        {
            get { return _properties; }
        }

        protected IList<EventMetadata> Events
        {
            get { return _events; }
        }

        public TypeMetadata Build()
        {
            InitializeInterfaceTypes();

            InitializeFields();
            InitializeMixins();
            InitializeNonTargetedInterfaces();
            InitializeConstructors();

            if (_constructors.Count == 0)
            {
                InitializeDefaultConstructor();
            }

            InitializeMethods();
            InitializeProperties();
            InitializeEvents();

            InitializeDuckTypes();

            return new TypeMetadata(
                _proxyDefinition.ProxyTypeName,
                _baseType, 
                _interfaces.ToArray(), 
                _dispatcherField,
                _fields.ToArray(),
                _constructors.ToArray(), 
                _methods.ToArray(), 
                _properties.ToArray(), 
                _events.ToArray(), 
                _mixins.ToArray());
        }

        protected virtual void InitializeInterfaceTypes()
        {
            if (_proxyDefinition.Type.IsInterface)
            {
                _interfaces.Add(_proxyDefinition.Type);
            }

            for (var i = 0; i < _proxyDefinition.Interfaces.Length; i++)
            {
                var interfaceDefinition = _proxyDefinition.Interfaces[i];

                _interfaces.Add(interfaceDefinition.Type);
            }

            _interfaces.Add(typeof(IProxy));
        }

        protected virtual void InitializeFields()
        {
            _fields.Add(_dispatcherField);
        }

        protected virtual void InitializeNonTargetedInterfaces()
        {
            for (var i = 0; i < _proxyDefinition.Interfaces.Length; i++)
            {
                var definition = _proxyDefinition.Interfaces[i] as NonTargetedInterfaceDefinition;

                if (definition == null)
                {
                    continue;
                }

                InitializeDefinitionMethods(definition, null, true);
                InitializeDefinitionProperties(definition, null, true);
                InitializeDefinitionEvents(definition, null, true);
            }
        }

        protected virtual void InitializeMixins()
        {
            var currentMixinPosition = 1;

            for (var i = 0; i < _proxyDefinition.Interfaces.Length; i++)
            {
                var mixinInterface = _proxyDefinition.Interfaces[i] as MixinInterfaceDefinition;

                if (mixinInterface == null)
                {
                    break;
                }

                var name = String.Format("mixin{0}", currentMixinPosition++);

                var mixin = new TargetMetadata(
                    name,
                    mixinInterface.Type,
                    mixinInterface.MixinType,
                    mixinInterface.ProxyInstantiated);

                _mixins.Add(mixin);

                _fields.Add(mixin.InstanceField);

                InitializeDefinitionMethods(mixinInterface, mixin.InstanceField, true);
                InitializeDefinitionProperties(mixinInterface, mixin.InstanceField, true);
                InitializeDefinitionEvents(mixinInterface, mixin.InstanceField, true);
            }
        }

        protected virtual void InitializeConstructors()
        {
        }

        protected ConstructorParameterMetadata[] InitializeConstructorParameters(ConstructorInfo constructor)
        {
            var parameters = new List<ConstructorParameterMetadata>();

            var parameterSequence = 1;

            parameters.Add(new ConstructorDispatcherParameterMetadata(parameterSequence++, _dispatcherField));

            var constructorParameters = constructor.GetParameters();

            for (var i = 0; i < constructorParameters.Length; i++)
            {
                var constructorParameter = constructorParameters[i];

                parameters.Add(new ConstructorBaseParameterMetadata(
                    parameterSequence++,
                    constructorParameter.Name,
                    constructorParameter.ParameterType));
            }

            GetMixinConstructorParameters(parameterSequence, parameters);

            return parameters.ToArray();
        }

        protected void GetMixinConstructorParameters(int parameterSequence, List<ConstructorParameterMetadata> parameters)
        {
            for (var i = 0; i < _mixins.Count; i++)
            {
                var mixin = _mixins[i];

                if (!mixin.IsProxyInstantiated)
                {
                    parameters.Add(new ConstructorTargetParameterMetadata(parameterSequence++, mixin.ParameterName, mixin.InstanceField));
                }
            }
        }

        protected virtual void InitializeDefaultConstructor()
        {
            var dispatcherParameter = new ConstructorDispatcherParameterMetadata(1, _dispatcherField);
            var parameters = new [] { dispatcherParameter };

            var constructor = new ConstructorMetadata(parameters);

            _constructors.Add(constructor);
        }

        protected virtual void InitializeMethods()
        {
            InitializeDefinitionMethods(_proxyDefinition, null, false);
        }

        protected void InitializeDefinitionMethods(
            IProxyDefinitionElement definition, 
            FieldMetadata targetField, 
            bool implementExplicitly)
        {
            var definitionMethods = definition.Type.GetMethods(ProxyBindingFlags);

            for (var i = 0; i < definitionMethods.Length; i++)
            {
                var method = definitionMethods[i];

                if (ShouldBuildMethodMetadata(method))
                {
                    CreateMethodMetadata(definition, targetField, method, implementExplicitly);
                }
            }
        }

        protected bool ShouldBuildMethodMetadata(MethodInfo method)
        {
            return !method.IsSpecialName && method.IsVirtual && method.Name != "Finalize";
        }

        private MethodMetadata CreateMethodMetadata(
            IProxyDefinitionElement definition, 
            FieldMetadata targetField,
            MethodInfo method, 
            bool implementExplicitly)
        {
            var result = MethodMetadataFactory.Create(definition, targetField, method);

            if (implementExplicitly)
            {
                result.UseExplicitInterfaceImplementation();
            }

            _methods.Add(result);

            return result;
        }

        protected virtual void InitializeProperties()
        {
            InitializeDefinitionProperties(_proxyDefinition, null, false);
        }

        protected void InitializeDefinitionProperties(
            IProxyDefinitionElement definition, 
            FieldMetadata targetField,
            bool implementExplicitly)
        {
            var properties = definition.Type.GetProperties(ProxyBindingFlags);

            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                var getMethod = property.GetGetMethod();
                var setMethod = property.GetSetMethod();

                MethodMetadata getMethodMetadata = null;
                MethodMetadata setMethodMetadata = null;

                if (getMethod != null && getMethod.IsVirtual)
                {
                    getMethodMetadata = CreateMethodMetadata(definition, targetField, getMethod, implementExplicitly);
                }

                if (setMethod != null && setMethod.IsVirtual)
                {
                    setMethodMetadata = CreateMethodMetadata(definition, targetField, setMethod, implementExplicitly);
                }

                if (getMethodMetadata != null || setMethodMetadata != null)
                {
                    _properties.Add(new PropertyMetadata(properties[i], getMethodMetadata, setMethodMetadata));
                }
            }
        }

        protected virtual void InitializeEvents()
        {
            InitializeDefinitionEvents(_proxyDefinition, null, false);
        }

        protected virtual void InitializeDefinitionEvents(
            IProxyDefinitionElement definition, 
            FieldMetadata targetField, 
            bool implementExplicitly)
        {
            var events = definition.Type.GetEvents(ProxyBindingFlags);

            for (var i = 0; i < events.Length; i++)
            {
                var @event = events[i];

                var addMethod = @event.GetAddMethod();
                var removeMethod = @event.GetRemoveMethod();
                var raiseMethod = @event.GetRaiseMethod();

                MethodMetadata addMethodMetadata = null;
                MethodMetadata removeMethodMetadata = null;
                MethodMetadata raiseMethodMetadata = null;

                if (addMethod.IsVirtual)
                {
                    addMethodMetadata = CreateMethodMetadata(definition, targetField, addMethod, implementExplicitly);
                }

                if (removeMethod.IsVirtual)
                {
                    removeMethodMetadata = CreateMethodMetadata(definition, targetField, removeMethod, implementExplicitly);
                }

                if (raiseMethod != null && raiseMethod.IsVirtual)
                {
                    raiseMethodMetadata = CreateMethodMetadata(definition, targetField, raiseMethod, implementExplicitly);
                }

                if (addMethodMetadata != null ||
                    removeMethodMetadata != null ||
                    raiseMethodMetadata != null)
                {
                    _events.Add(new EventMetadata(events[i], addMethodMetadata, removeMethodMetadata, raiseMethodMetadata));
                }
            }
        }

        protected virtual void InitializeDuckTypes()
        {
            for (var i = 0; i < _proxyDefinition.Interfaces.Length; i++)
            {
                var duckTypeDefinition = _proxyDefinition.Interfaces[i] as DuckTypeInterfaceDefinition;

                if (duckTypeDefinition == null)
                {
                    continue;
                }

                InitializeDuckTypeMethods(duckTypeDefinition);
                InitializeDuckTypeProperties(duckTypeDefinition);
                InitializeDuckTypeEvents(duckTypeDefinition);
            }
        }

        private void InitializeDuckTypeMethods(DuckTypeInterfaceDefinition duckTypeDefinition)
        {
            var methods = duckTypeDefinition.Type.GetMethods(ProxyBindingFlags);
            var methodCount = methods.Length;

            for (var i = 0; i < methodCount; i++)
            {
                var method = methods[i];

                if (ShouldBuildMethodMetadata(method))
                {
                    InitializeDuckTypeMethod(method);
                }
            }
        }

        private MethodMetadata InitializeDuckTypeMethod(MethodInfo method)
        {
            var targetMethod = MemberLocator.LocateMatchingMethod(method, _proxyDefinition.Type);

            if (targetMethod == null)
            {
                // TODO: Fix
                throw new Exception();
            }

            var methodMetadata = new DuckTypeMethodMetadata(method, targetMethod);

            methodMetadata.UseExplicitInterfaceImplementation();

            _methods.Add(methodMetadata);

            return methodMetadata;
        }

        private void InitializeDuckTypeProperties(DuckTypeInterfaceDefinition duckTypeDefinition)
        {
            var properties = duckTypeDefinition.Type.GetProperties(ProxyBindingFlags);
            var propertyCount = properties.Length;

            for (var i = 0; i < propertyCount; i++)
            {
                var property = properties[i];

                var targetProperty = MemberLocator.LocateMatchingProperty(property, _proxyDefinition.Type);

                if (targetProperty == null)
                {
                    // TODO: Fix
                    throw new Exception();
                }

                var getMethod = property.GetGetMethod();
                var setMethod = property.GetSetMethod();

                MethodMetadata getMethodMetadata = null;
                MethodMetadata setMethodMetadata = null;

                if (getMethod != null)
                {
                    getMethodMetadata = InitializeDuckTypeMethod(getMethod);
                }

                if (setMethod != null)
                {
                    setMethodMetadata = InitializeDuckTypeMethod(setMethod);
                }

                var propertyMetadata = 
                    new DuckTypePropertyMetadata(property, targetProperty, getMethodMetadata, setMethodMetadata);

                propertyMetadata.UseExplicitInterfaceImplementation();

                _properties.Add(propertyMetadata);
            }
        }

        private void InitializeDuckTypeEvents(DuckTypeInterfaceDefinition definition)
        {
            var events = definition.Type.GetEvents(ProxyBindingFlags);

            for (var i = 0; i < events.Length; i++)
            {
                var @event = events[i];

                var targetEvent = MemberLocator.LocateMatchingEvent(@event, _proxyDefinition.Type);

                if (targetEvent == null)
                {
                    // TODO: Fix
                    throw new Exception();
                }

                var addMethod = @event.GetAddMethod();
                var removeMethod = @event.GetRemoveMethod();
                var raiseMethod = @event.GetRaiseMethod();

                MethodMetadata raiseMethodMetadata = null;

                var addMethodMetadata = InitializeDuckTypeMethod(addMethod);
                var removeMetodMetadata = InitializeDuckTypeMethod(removeMethod);

                if (raiseMethod != null)
                {
                    raiseMethodMetadata = InitializeDuckTypeMethod(raiseMethod);
                }

                var eventMetadata =
                    new DuckTypeEventMetadata(@event, targetEvent, addMethodMetadata, removeMetodMetadata, raiseMethodMetadata);

                eventMetadata.UseExplicitInterfaceImplementation();

                _events.Add(eventMetadata);
            }
        }
    }
}
