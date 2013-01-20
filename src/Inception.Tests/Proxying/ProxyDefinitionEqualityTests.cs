using System;
using System.ComponentModel;
using Inception.Proxying;
using Inception.Tests.Proxying.Model;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
	[TestFixture]
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

		[Test]
		public void ProxyDefinitionsAreEqual()
		{
			var proxyDefinition1 = CreateProxyDefinition1();
			var proxyDefinition2 = CreateProxyDefinition1();

			Assert.That(proxyDefinition1.Equals(proxyDefinition2));
		}

		[Test]
		public void DifferingProxyDefinitionsAreNotEqual()
		{
			var proxyDefinition1 = CreateProxyDefinition1();
			var proxyDefinition2 = CreateProxyDefinition2();

			Assert.That(!proxyDefinition1.Equals(proxyDefinition2));
		}

		[Test]
		public void DifferingProxyDefinitionsWithAdditionalInterfaceAreNotEqual()
		{
			var proxyDefinition1 = CreateProxyDefinitionWithInterface1();
			var proxyDefinition2 = CreateProxyDefinitionWithInterface2();

			Assert.That(!proxyDefinition1.Equals(proxyDefinition2));
		}

		[Test]
		public void ProxyDefinitionsHaveSameHashCodes()
		{
			var proxyDefinition1 = CreateProxyDefinition1();
			var proxyDefinition2 = CreateProxyDefinition1();

			Assert.That(proxyDefinition1.GetHashCode() == proxyDefinition2.GetHashCode());
		}

		[Test]
		public void ProxyDefinitionsWithAdditionalInterfaceHaveSameHashCode()
		{
			var proxyDefinition1 = CreateProxyDefinitionWithInterface1();
			var proxyDefinition2 = CreateProxyDefinitionWithInterface1();

			Assert.That(proxyDefinition1.GetHashCode() == proxyDefinition2.GetHashCode());
		}
	}
}
