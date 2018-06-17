using System;
using InceptionCore.Proxying;
using InceptionCore.Proxying.Metadata;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public partial class ClassProxyMetadataTests
    {
        private static TypeMetadata BuildProxyMetadata()
        {
            var definition = new ClassProxyDefinition(typeof(Square), null);
            var metadataBuilder = new ClassProxyMetadataBuilder(definition);

            return metadataBuilder.Build();
        }

        [Fact]
        public void CorrectBaseTypeIsDetermined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(typeof(Square), pm.BaseType);
        }

        [Fact]
        public void TwoConstructorsAreDefined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(2, pm.Constructors.Length);            
        }

        [Fact]
        public void DefaultConstructorIsDefined()
        {
            var pm = BuildProxyMetadata();

            var constructor = pm.Constructors[0];
            var dispatcherParameter = constructor.Parameters[0];

            Assert.Equal(1, constructor.Parameters.Length);
            Assert.Equal(typeof(IProxyDispatcher), dispatcherParameter.ParameterType);
        }

        [Fact]
        public void OverloadedConstructorIsDefined()
        {
            var pm = BuildProxyMetadata();

            var constructor = pm.Constructors[1];

            Assert.Equal(2, constructor.Parameters.Length);

            var dispatcherParameter = constructor.Parameters[0];
            var widthParameter = constructor.Parameters[1];

            Assert.Equal(typeof(IProxyDispatcher), dispatcherParameter.ParameterType);
            Assert.Equal(typeof(int), widthParameter.ParameterType);
        }

        [Fact]
        public void CorrectPropertyMetadataIsDetermined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(1, pm.Properties.Length);
            Assert.Equal("Width", pm.Properties[0].Name);
            Assert.Equal("Width", pm.Properties[0].PropertyInfo.Name);
            Assert.Equal(typeof(int), pm.Properties[0].PropertyInfo.PropertyType);
        }

        [Fact]
        public void CorrectEventMetadataIsDetermined()
        {
            var pm = BuildProxyMetadata();

            Assert.Equal(1, pm.Events.Length);

            Assert.Equal("WidthChanged", pm.Events[0].Name);
            Assert.Equal("WidthChanged", pm.Events[0].EventInfo.Name);
            Assert.Equal(typeof(EventHandler), pm.Events[0].EventInfo.EventHandlerType);
        }

        [Fact]
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

            Assert.Equal(9, pm.Methods.Length);
        }
    }
}
