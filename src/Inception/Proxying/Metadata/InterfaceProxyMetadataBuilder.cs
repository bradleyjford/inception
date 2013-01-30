using System;

namespace Inception.Proxying.Metadata
{
    internal class InterfaceProxyMetadataBuilder : ProxyMetadataBuilder
    {
        public InterfaceProxyMetadataBuilder(ProxyDefinition proxyDefinition) 
            : base(proxyDefinition, typeof(object))
        {
        }
    }
}
