using System;
using System.ComponentModel;
using InceptionCore.Proxying;
using InceptionCore.Tests.Proxying.Model;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public partial class ProxyBuilderTests
    {
        readonly ProxyBuilder _builder;

        public ProxyBuilderTests()
        {
            _builder = new ProxyBuilder(
                GetType().FullName, 
                GetType().FullName + ".dll");    
        }

        [Fact]
        public void CanBuildSubClassProxy()
        {
            var proxyDefinition = new ClassProxyDefinition(typeof(Square), null);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(Square).IsAssignableFrom(type));
        }

        [Fact]
        public void CanBuildSubClassProxyWithDuckType()
        {
            var duckTypeDefinition = new DuckTypeInterfaceDefinition(typeof(IShape));
            var interfaces = new InterfaceDefinition[] { duckTypeDefinition };
            var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(Square).IsAssignableFrom(type));
            Assert.True(typeof(IShape).IsAssignableFrom(type));
        }

        [Fact]
        public void CanBuildSubClassProxyWithInstantiatedMixin()
        {
            var mixinDefinition = new MixinInterfaceDefinition(
                typeof(INotifyPropertyChanged),
                typeof(NotifyPropertyChangedMixin), 
                true);

            var interfaces = new InterfaceDefinition[] { mixinDefinition };
            var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(Square).IsAssignableFrom(type));
            Assert.True(typeof(INotifyPropertyChanged).IsAssignableFrom(type));
        }

        [Fact]
        public void CanBuildSubClassProxyWithTargetedMixin()
        {
            var mixinDefinition = new MixinInterfaceDefinition(
                typeof(INotifyPropertyChanged),
                typeof(NotifyPropertyChangedMixin),
                false);

            var interfaces = new InterfaceDefinition[] { mixinDefinition };
            var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(Square).IsAssignableFrom(type));
            Assert.True(typeof(INotifyPropertyChanged).IsAssignableFrom(type));
        }

        [Fact]
        public void CanBuildInterfaceProxy()
        {
            var proxyDefinition = new InterfaceProxyDefinition(typeof(IShape), null);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(IShape).IsAssignableFrom(type));
        }

        [Fact]
        public void CanBuildTargetedClassProxy()
        {
            var proxyDefinition = new TargetedClassProxyDefinition(typeof(Square), null);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(Square).IsAssignableFrom(type));

        }

        [Fact]
        public void CanBuildTargetedInterfaceProxy()
        {
            var proxyDefinition = new TargetedInterfaceProxyDefinition(typeof(ISquare), typeof(Square), null);

            var type = _builder.Build(proxyDefinition);

            Assert.NotNull(type);
            Assert.True(typeof(ISquare).IsAssignableFrom(type));
        }

        [Fact]
        public void ProxyGeneratedWithDefaultConstructorOnInterfaceTypes()
        {
            var proxyDefinition = new InterfaceProxyDefinition(typeof(IShape), null);

            var type = _builder.Build(proxyDefinition);

            Assert.Single(type.GetConstructors());
        }
    }
}
