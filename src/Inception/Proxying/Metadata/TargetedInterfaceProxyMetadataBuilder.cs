using System;
using System.Collections.Generic;

namespace Inception.Proxying.Metadata
{
    internal class TargetedInterfaceProxyMetadataBuilder : ProxyMetadataBuilder
    {
        private readonly TargetedInterfaceProxyDefinition _proxyDefinition;
        
        private TargetFieldMetadata _targetField;

        public TargetedInterfaceProxyMetadataBuilder(TargetedInterfaceProxyDefinition proxyDefinition) 
            : base(proxyDefinition, null)
        {
            _proxyDefinition = proxyDefinition;
        }

        protected override void InitializeFields()
        {
            base.InitializeFields();

            _targetField = new TargetFieldMetadata("_target", _proxyDefinition.TargetType);

            Fields.Add(_targetField);
        }

        protected override void InitializeConstructors()
        {
            var parameters = InitializeConstructorParameters();

            var constructor = new ConstructorMetadata(parameters);

            Constructors.Add(constructor);
        }

        private ConstructorParameterMetadata[] InitializeConstructorParameters()
        {
            var parameters = new List<ConstructorParameterMetadata>();

            var parameterSequence = 1;

            parameters.Add(new ConstructorDispatcherParameterMetadata(parameterSequence++, DispatcherField));
            parameters.Add(new ConstructorTargetParameterMetadata(parameterSequence++, "target", _targetField));

            GetMixinConstructorParameters(parameterSequence, parameters);

            return parameters.ToArray();
        }

        protected override void InitializeMethods()
        {
            InitializeDefinitionMethods(_proxyDefinition, _targetField, false);
        }

        protected override void InitializeProperties()
        {
            InitializeDefinitionProperties(_proxyDefinition, _targetField, false);
        }

        protected override void InitializeEvents()
        {
            InitializeDefinitionEvents(_proxyDefinition, _targetField, false);
        }
    }
}
