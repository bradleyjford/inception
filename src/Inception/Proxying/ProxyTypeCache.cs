using System;

namespace Inception.Proxying
{
    internal class ProxyTypeCache : LazyCache<ProxyDefinition, Type>
    {
        private readonly ProxyBuilder _proxyBuilder;

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
