using System;
using Inception.Proxying;
using Inception.Proxying.Metadata;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
	[TestFixture]
	public partial class InterfaceProxyMetadataTests
	{
		private static TypeMetadata BuildProxyMetadata()
		{
			var definition = new InterfaceProxyDefinition(typeof(IShape), null);
			var metadataBuilder = new InterfaceProxyMetadataBuilder(definition);

			return metadataBuilder.Build();
		}

		[Test]
		public void CorrectInterfacesAreDetermined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(typeof(object), pm.BaseType);
			Assert.AreEqual(typeof(IShape), pm.Interfaces[0]);
			Assert.AreEqual(typeof(IProxy), pm.Interfaces[1]);
		}

		[Test]
		public void SingleConstructorIsDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(1, pm.Constructors.Length);
			Assert.AreEqual(1, pm.Constructors[0].Parameters.Length);
			Assert.AreEqual(typeof(IProxyDispatcher), pm.Constructors[0].Parameters[0].ParameterType);
		}

		[Test]
		public void CorrectPropertiesAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(1, pm.Properties.Length);
			Assert.AreEqual("Name", pm.Properties[0].Name);
		}

		[Test]
		public void CorrectEventsAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(1, pm.Events.Length);
			Assert.AreEqual("ShapeChanged", pm.Events[0].Name);
		}

		[Test]
		public void CorrectMethodsAreDefined()
		{
			var pm = BuildProxyMetadata();

			/*
			 * CalculateArea
			 * get_Name
			 * add_ShapeChanged
			 * remove_ShapeChanged;
			 */
			
			Assert.AreEqual(4, pm.Methods.Length);
			Assert.AreEqual(typeof(NonTargetedMethodMetadata), pm.Methods[0].GetType());
		}
	}
}
