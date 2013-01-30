using System;
using Inception.Proxying;
using Inception.Tests.Proxying.Model;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
    [TestFixture]
    public class ProxyTypeCacheTests
    {
        private ProxyBuilder _builder;
        private ProxyTypeCache _cache;

        [SetUp]
        public void SetUp()
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

        [Test]
        public void ProxyDefinitionsAreCached()
        {
            var proxyDefinition1 = CreateProxyDefinition1();

            var type1 = _cache[proxyDefinition1];

            var proxyDefinition2 = CreateProxyDefinition1();

            var type2 = _cache[proxyDefinition2];

            Assert.That(_cache.ContainsKey(proxyDefinition2));
            Assert.AreSame(type1, type2);
        }

        [Test]
        public void IncorrectProxyTypesAreNotReturnedFromCache()
        {
            var proxyDefinition1 = CreateProxyDefinition1();

            var type1 = _cache[proxyDefinition1];

            var proxyDefinition2 = CreateProxyDefinition2();

            Assert.IsNotNull(type1);
            Assert.That(!_cache.ContainsKey(proxyDefinition2));
        }
    }
}
