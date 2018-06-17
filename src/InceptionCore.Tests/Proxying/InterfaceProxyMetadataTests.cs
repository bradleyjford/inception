using System;
using InceptionCore.Proxying;
using InceptionCore.Proxying.Metadata;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public partial class InterfaceProxyMetadataTests
    {
        private static TypeMetadata BuildProxyMetadata()
        {
            var definition = new InterfaceProxyDefinition(typeof(IShape), null);
            var metadataBuilder = new InterfaceProxyMetadataBuilder(definition);

            return metadataBuilder.Build();
        }

        [Fact]
        public void CorrectInterfacesAreDetermined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(typeof(object), pm.BaseType);
            Assert.Equal(typeof(IShape), pm.Interfaces[0]);
            Assert.Equal(typeof(IProxy), pm.Interfaces[1]);
        }

        [Fact]
        public void SingleConstructorIsDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(1, pm.Constructors.Length);
            Assert.Equal(1, pm.Constructors[0].Parameters.Length);
            Assert.Equal(typeof(IProxyDispatcher), pm.Constructors[0].Parameters[0].ParameterType);
        }

        [Fact]
        public void CorrectPropertiesAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(1, pm.Properties.Length);
            Assert.Equal("Name", pm.Properties[0].Name);
        }

        [Fact]
        public void CorrectEventsAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(1, pm.Events.Length);
            Assert.Equal("ShapeChanged", pm.Events[0].Name);
        }

        [Fact]
        public void CorrectMethodsAreDefined()
        {
            var pm = BuildProxyMetadata();

            /*
             * CalculateArea
             * get_Name
             * add_ShapeChanged
             * remove_ShapeChanged;
             */
            
            Assert.Equal(4, pm.Methods.Length);
            Assert.Equal(typeof(NonTargetedMethodMetadata), pm.Methods[0].GetType());
        }
    }
}
