using System;
using InceptionCore.Proxying;
using InceptionCore.Proxying.Metadata;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public partial class ClassWithInterfaceProxyMetadataTests
    {
        private static TypeMetadata BuildProxyMetadata()
        {
            var interfaceDefinition = new NonTargetedInterfaceDefinition(typeof(IShape));

            var definition = new ClassProxyDefinition(typeof(Square), new [] { interfaceDefinition });
            var metadataBuilder = new ClassProxyMetadataBuilder(definition);

            return metadataBuilder.Build();
        }

        [Fact]
        public void CorrectInterfacesAreDetermined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(typeof(Square), pm.BaseType);
            Assert.Equal(typeof(IShape), pm.Interfaces[0]);
            Assert.Equal(typeof(IProxy), pm.Interfaces[1]);
        }

        [Fact]
        public void CorrectPropertiesAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(2, pm.Properties.Length);
            Assert.Equal("Name", pm.Properties[0].Name);
            Assert.Equal("Width", pm.Properties[1].Name);
        }

        [Fact]
        public void CorrectEventsAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(2, pm.Events.Length);
            Assert.Equal("ShapeChanged", pm.Events[0].Name);
            Assert.Equal("WidthChanged", pm.Events[1].Name);
        }

        [Fact]
        public void CorrectMethodsAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(13, pm.Methods.Length);
        }
    }
}
