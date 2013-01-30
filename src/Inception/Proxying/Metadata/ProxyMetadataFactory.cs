using System;

namespace Inception.Proxying.Metadata
{
    internal static class ProxyMetadataFactory
    {
        public static TypeMetadata BuildTypeMetadata(ProxyDefinition proxyDefinition)
        {
            var classProxyDefinition = proxyDefinition as ClassProxyDefinition;

            if (classProxyDefinition != null)
            {
                return new ClassProxyMetadataBuilder(classProxyDefinition).Build();
            }

            var interfaceProxyDefinition = proxyDefinition as InterfaceProxyDefinition;

            if (interfaceProxyDefinition != null)
            {
                return new InterfaceProxyMetadataBuilder(interfaceProxyDefinition).Build();
            }

            var targetedClassProxyDefinition = proxyDefinition as TargetedClassProxyDefinition;

            if (targetedClassProxyDefinition != null)
            {
                return new TargetedClassProxyMetadataBuilder(targetedClassProxyDefinition).Build();
            }

            var targetedInterfaceProxyDefinition = proxyDefinition as TargetedInterfaceProxyDefinition;

            if (targetedInterfaceProxyDefinition != null)
            {
                return new TargetedInterfaceProxyMetadataBuilder(targetedInterfaceProxyDefinition).Build();
            }

            throw new NotSupportedException();
        }
    }
}
