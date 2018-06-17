using System;
using InceptionCore.Proxying;
using InceptionCore.Reflection;
using Xunit;

namespace InceptionCore.Tests.Proxying
{
    
    public partial class ProxyConstructorSelectorTests
    {
        private readonly ProxyConstructorSelector _selector = new ProxyConstructorSelector();

        [Fact]
        public void CanLocateDefaultConstructor()
        {
            var arguments = new ArgumentCollection();

            var constructor = _selector.Select(typeof(Square), arguments);

            Assert.NotNull(constructor);
            Assert.True(constructor.GetParameters().Length == 0);
        }

        [Fact]
        public void CanLocateConstructorWithExactArgumentTypes()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("width", 5);

            var constructor = _selector.Select(typeof(Square), arguments);

            Assert.NotNull(constructor);
            Assert.True(constructor.GetParameters().Length == 1);
        }

        [Fact]
        public void CanLocateConstructorWithImplementedInterfaceArgument()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("square", new Square());

            var constructor = _selector.Select(typeof(ConstructorSelectorTestModel), arguments);

            Assert.NotNull(constructor);
            Assert.True(constructor.GetParameters()[0].ParameterType == typeof(ISquare));
        }

        [Fact]
        public void CanLocateConstructorWithBaseClassArgument()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("shape", new Circle());

            var constructor = _selector.Select(typeof(ConstructorSelectorTestModel), arguments);

            Assert.NotNull(constructor);
            Assert.True(constructor.GetParameters()[0].ParameterType == typeof(Shape));
        }
    }
}
