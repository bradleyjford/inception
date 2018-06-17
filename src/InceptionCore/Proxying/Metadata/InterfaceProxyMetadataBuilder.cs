using System;

namespace InceptionCore.Proxying.Metadata
{
    class InterfaceProxyMetadataBuilder : ProxyMetadataBuilder
    {
        public InterfaceProxyMetadataBuilder(ProxyDefinition proxyDefinition)
            : base(proxyDefinition, typeof(object))
        {
        }
    }
}