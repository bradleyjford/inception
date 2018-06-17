using System;
using InceptionCore.Proxying;
using InceptionCore.Tests.Proxying.Model;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public class ProxyTypeCacheTests
    {
        private ProxyBuilder _builder;
        private ProxyTypeCache _cache;

        public ProxyTypeCacheTests()
        {
            _builder = new ProxyBuilder("ProxyTypeCacheTests", "ProxyTypeCacheTests.dll");
            _cache = new ProxyTypeCache(_builder);
        }

        private ProxyDefinition CreateProxyDefinition1()
        {
            return new ClassProxyDefinition(typeof(Square), null);
        }

        private ProxyDefinition CreateProxyDefinition2()
        {
            return new ClassProxyDefinition(typeof(Shape), null);
        }

        [Fact]
        public void ProxyDefinitionsAreCached()
        {
            var proxyDefinition1 = CreateProxyDefinition1();

            var type1 = _cache[proxyDefinition1];

            var proxyDefinition2 = CreateProxyDefinition1();

            var type2 = _cache[proxyDefinition2];

            Assert.True(_cache.ContainsKey(proxyDefinition2));
            Assert.Same(type1, type2);
        }

        [Fact]
        public void IncorrectProxyTypesAreNotReturnedFromCache()
        {
            var proxyDefinition1 = CreateProxyDefinition1();

            var type1 = _cache[proxyDefinition1];

            var proxyDefinition2 = CreateProxyDefinition2();

            Assert.NotNull(type1);
            Assert.False(_cache.ContainsKey(proxyDefinition2));
        }
    }
}
