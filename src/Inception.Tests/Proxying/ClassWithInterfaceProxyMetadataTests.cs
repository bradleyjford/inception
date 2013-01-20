using System;
using Inception.Proxying;
using Inception.Proxying.Metadata;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
	[TestFixture]
	public partial class ClassWithInterfaceProxyMetadataTests
	{
		private static TypeMetadata BuildProxyMetadata()
		{
			var interfaceDefinition = new NonTargetedInterfaceDefinition(typeof(IShape));

			var definition = new ClassProxyDefinition(typeof(Square), new [] { interfaceDefinition });
			var metadataBuilder = new ClassProxyMetadataBuilder(definition);

			return metadataBuilder.Build();
		}

		[Test]
		public void CorrectInterfacesAreDetermined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(typeof(Square), pm.BaseType);
			Assert.AreEqual(typeof(IShape), pm.Interfaces[0]);
			Assert.AreEqual(typeof(IProxy), pm.Interfaces[1]);
		}

		[Test]
		public void CorrectPropertiesAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(2, pm.Properties.Length);
			Assert.AreEqual("Name", pm.Properties[0].Name);
			Assert.AreEqual("Width", pm.Properties[1].Name);
		}

		[Test]
		public void CorrectEventsAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(2, pm.Events.Length);
			Assert.AreEqual("ShapeChanged", pm.Events[0].Name);
			Assert.AreEqual("WidthChanged", pm.Events[1].Name);
		}

		[Test]
		public void CorrectMethodsAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(13, pm.Methods.Length);
		}
	}
}
