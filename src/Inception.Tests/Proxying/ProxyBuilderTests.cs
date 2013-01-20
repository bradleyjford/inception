using System;
using System.ComponentModel;
using Inception.Proxying;
using Inception.Tests.Proxying.Model;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
	[TestFixture]
	public partial class ProxyBuilderTests
	{
		private ProxyBuilder _builder;

		[SetUp]
		public void SetUp()
		{
			_builder = new ProxyBuilder(
				GetType().FullName, 
				GetType().FullName + ".dll");	
		}

		[Test]
		public void CanBuildSubClassProxy()
		{
			var proxyDefinition = new ClassProxyDefinition(typeof(Square), null);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(Square).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildSubClassProxyWithDuckType()
		{
			var duckTypeDefinition = new DuckTypeInterfaceDefinition(typeof(IShape));
			var interfaces = new InterfaceDefinition[] { duckTypeDefinition };
			var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(Square).IsAssignableFrom(type));
			Assert.That(typeof(IShape).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildSubClassProxyWithInstantiatedMixin()
		{
			var mixinDefinition = new MixinInterfaceDefinition(
				typeof(INotifyPropertyChanged),
				typeof(NotifyPropertyChangedMixin), 
				true);

			var interfaces = new InterfaceDefinition[] { mixinDefinition };
			var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(Square).IsAssignableFrom(type));
			Assert.That(typeof(INotifyPropertyChanged).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildSubClassProxyWithTargetedMixin()
		{
			var mixinDefinition = new MixinInterfaceDefinition(
				typeof(INotifyPropertyChanged),
				typeof(NotifyPropertyChangedMixin),
				false);

			var interfaces = new InterfaceDefinition[] { mixinDefinition };
			var proxyDefinition = new ClassProxyDefinition(typeof(Square), interfaces);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(Square).IsAssignableFrom(type));
			Assert.That(typeof(INotifyPropertyChanged).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildInterfaceProxy()
		{
			var proxyDefinition = new InterfaceProxyDefinition(typeof(IShape), null);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(IShape).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildTargetedClassProxy()
		{
			var proxyDefinition = new TargetedClassProxyDefinition(typeof(Square), null);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(Square).IsAssignableFrom(type));
		}

		[Test]
		public void CanBuildTargetedInterfaceProxy()
		{
			var proxyDefinition = new TargetedInterfaceProxyDefinition(typeof(ISquare), typeof(Square), null);

			var type = _builder.Build(proxyDefinition);

			Assert.IsNotNull(type);
			Assert.That(typeof(ISquare).IsAssignableFrom(type));
		}

		[Test]
		public void ProxyGeneratedWithDefaultConstructorOnInterfaceTypes()
		{
			var proxyDefinition = new InterfaceProxyDefinition(typeof(IShape), null);

			var type = _builder.Build(proxyDefinition);

			Assert.That(type.GetConstructors().Length == 1);
		}
	}
}
