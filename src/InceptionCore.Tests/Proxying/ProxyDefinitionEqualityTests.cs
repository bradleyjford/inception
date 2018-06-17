using System;
using System.ComponentModel;
using InceptionCore.Proxying;
using InceptionCore.Tests.Proxying.Model;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public class ProxyDefinitionEqualityTests
    {
        private ProxyDefinition CreateProxyDefinition1()
        {
            return new ClassProxyDefinition(typeof(Square), null);
        }

        private ProxyDefinition CreateProxyDefinitionWithInterface1()
        {
            var interfaceDefinition = new MixinInterfaceDefinition(
                typeof(INotifyPropertyChanged),
                typeof(NotifyPropertyChangedMixin),
                false);

            var proxyDefinition = new ClassProxyDefinition(
                typeof(Square),
                new InterfaceDefinition[] { interfaceDefinition });

            return proxyDefinition;
        }

        private ProxyDefinition CreateProxyDefinition2()
        {
            return new ClassProxyDefinition(typeof(Shape), null);
        }

        private ProxyDefinition CreateProxyDefinitionWithInterface2()
        {
            var interfaceDefinition = new NonTargetedInterfaceDefinition(typeof(ISquare));

            var proxyDefinition = new ClassProxyDefinition(
                typeof(Shape),
                new InterfaceDefinition[] { interfaceDefinition });

            return proxyDefinition;
        }

        [Fact]
        public void ProxyDefinitionsAreEqual()
        {
            var proxyDefinition1 = CreateProxyDefinition1();
            var proxyDefinition2 = CreateProxyDefinition1();

            Assert.True(proxyDefinition1.Equals(proxyDefinition2));
        }

        [Fact]
        public void DifferingProxyDefinitionsAreNotEqual()
        {
            var proxyDefinition1 = CreateProxyDefinition1();
            var proxyDefinition2 = CreateProxyDefinition2();

            Assert.True(!proxyDefinition1.Equals(proxyDefinition2));
        }

        [Fact]
        public void DifferingProxyDefinitionsWithAdditionalInterfaceAreNotEqual()
        {
            var proxyDefinition1 = CreateProxyDefinitionWithInterface1();
            var proxyDefinition2 = CreateProxyDefinitionWithInterface2();

            Assert.True(!proxyDefinition1.Equals(proxyDefinition2));
        }

        [Fact]
        public void ProxyDefinitionsHaveSameHashCodes()
        {
            var proxyDefinition1 = CreateProxyDefinition1();
            var proxyDefinition2 = CreateProxyDefinition1();

            Assert.True(proxyDefinition1.GetHashCode() == proxyDefinition2.GetHashCode());
        }

        [Fact]
        public void ProxyDefinitionsWithAdditionalInterfaceHaveSameHashCode()
        {
            var proxyDefinition1 = CreateProxyDefinitionWithInterface1();
            var proxyDefinition2 = CreateProxyDefinitionWithInterface1();

            Assert.True(proxyDefinition1.GetHashCode() == proxyDefinition2.GetHashCode());
        }
    }
}
