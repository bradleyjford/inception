using System;

namespace InceptionCore.Proxying
{
    class ProxyTypeCache : LazyCache<ProxyDefinition, Type>
    {
        readonly ProxyBuilder _proxyBuilder;

        public ProxyTypeCache(ProxyBuilder proxyBuilder)
        {
            _proxyBuilder = proxyBuilder;
        }

        protected override Type CreateItem(ProxyDefinition proxyDefinition)
        {
            return _proxyBuilder.Build(proxyDefinition);
        }
    }
}