using System;
using Inception.Proxying;
using Inception.Reflection;
using NUnit.Framework;

namespace Inception.Tests.Proxying
{
    [TestFixture]
    public partial class ProxyConstructorSelectorTests
    {
        private readonly ProxyConstructorSelector _selector = new ProxyConstructorSelector();

        [Test]
        public void CanLocateDefaultConstructor()
        {
            var arguments = new ArgumentCollection();

            var constructor = _selector.Select(typeof(Square), arguments);

            Assert.IsNotNull(constructor);
            Assert.That(constructor.GetParameters().Length == 0);
        }

        [Test]
        public void CanLocateConstructorWithExactArgumentTypes()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("width", 5);

            var constructor = _selector.Select(typeof(Square), arguments);

            Assert.IsNotNull(constructor);
            Assert.That(constructor.GetParameters().Length == 1);
        }

        [Test]
        public void CanLocateConstructorWithImplementedInterfaceArgument()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("square", new Square());

            var constructor = _selector.Select(typeof(ConstructorSelectorTestModel), arguments);

            Assert.IsNotNull(constructor);
            Assert.That(constructor.GetParameters()[0].ParameterType == typeof(ISquare));
        }

        [Test]
        public void CanLocateConstructorWithBaseClassArgument()
        {
            var arguments = new ArgumentCollection();

            arguments.Add("shape", new Circle());

            var constructor = _selector.Select(typeof(ConstructorSelectorTestModel), arguments);

            Assert.IsNotNull(constructor);
            Assert.That(constructor.GetParameters()[0].ParameterType == typeof(Shape));
        }
    }
}
