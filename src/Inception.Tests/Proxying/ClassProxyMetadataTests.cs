using System;
using Inception.Proxying;
using Inception.Proxying.Metadata;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
	[TestFixture]
	public partial class ClassProxyMetadataTests
	{
		private static TypeMetadata BuildProxyMetadata()
		{
			var definition = new ClassProxyDefinition(typeof(Square), null);
			var metadataBuilder = new ClassProxyMetadataBuilder(definition);

			return metadataBuilder.Build();
		}

		[Test]
		public void CorrectBaseTypeIsDetermined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(typeof(Square), pm.BaseType);
		}

		[Test]
		public void TwoConstructorsAreDefined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(2, pm.Constructors.Length);			
		}

		[Test]
		public void DefaultConstructorIsDefined()
		{
			var pm = BuildProxyMetadata();

			var constructor = pm.Constructors[0];
			var dispatcherParameter = constructor.Parameters[0];

			Assert.AreEqual(1, constructor.Parameters.Length);
			Assert.AreEqual(typeof(IProxyDispatcher), dispatcherParameter.ParameterType);
		}

		[Test]
		public void OverloadedConstructorIsDefined()
		{
			var pm = BuildProxyMetadata();

			var constructor = pm.Constructors[1];

			Assert.AreEqual(2, constructor.Parameters.Length);

			var dispatcherParameter = constructor.Parameters[0];
			var widthParameter = constructor.Parameters[1];

			Assert.AreEqual(typeof(IProxyDispatcher), dispatcherParameter.ParameterType);
			Assert.AreEqual(typeof(int), widthParameter.ParameterType);
		}

		[Test]
		public void CorrectPropertyMetadataIsDetermined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(1, pm.Properties.Length);
			Assert.AreEqual("Width", pm.Properties[0].Name);
			Assert.AreEqual("Width", pm.Properties[0].PropertyInfo.Name);
			Assert.AreEqual(typeof(int), pm.Properties[0].PropertyInfo.PropertyType);
		}

		[Test]
		public void CorrectEventMetadataIsDetermined()
		{
			var pm = BuildProxyMetadata();

			Assert.AreEqual(1, pm.Events.Length);

			Assert.AreEqual("WidthChanged", pm.Events[0].Name);
			Assert.AreEqual("WidthChanged", pm.Events[0].EventInfo.Name);
			Assert.AreEqual(typeof(EventHandler), pm.Events[0].EventInfo.EventHandlerType);
		}

		[Test]
		public void CorrectMethodMetadataIsDetermined()
		{
			var pm = BuildProxyMetadata();

			/*
			 * Equals(obj other)
			 * GetHashCode
			 * ToString()
			 * 
			 * get_Width()
			 * set_Width(int value)
			 * 
			 * add_WidthChanged(EventHandler handler)
			 * remove_WidthChanged(EventHandler handler)
			 * 
			 * long CalculateSurfaceArea()
			 * void OnWidthChanged()
			 */

			Assert.AreEqual(9, pm.Methods.Length);
		}
	}
}
