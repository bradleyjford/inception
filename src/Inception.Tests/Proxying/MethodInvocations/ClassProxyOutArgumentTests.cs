using System;
using Inception.Proxying;
using NUnit.Framework;

namespace Inception.Tests.Proxying.MethodInvocations
{
    [TestFixture]
    public class ClassProxyOutArgumentTests
    {
        private ProxyFactory _factory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _factory = new ProxyFactory(
                "ClassProxyOutArgumentTests.Proxies",
                "ClassProxyOutArgumentTests.dll");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _factory.SaveAssembly();
        }

        private OutParameterTestModel CreateProxy()
        {
            return _factory.CreateProxy<OutParameterTestModel>(p => { });
        }

        [Test]
        public void CanInvokeMethodWithBooleanOutArg()
        {
            var proxy = CreateProxy();

            bool arg;

            proxy.BooleanOutParameter(out arg);

            Assert.AreEqual(true, arg);
        }

        [Test]
        public void CanInvokeMethodWithByteOutArg()
        {
            var proxy = CreateProxy();

            byte arg;

            proxy.ByteOutParameter(out arg);

            Assert.AreEqual(Byte.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithCharOutArg()
        {
            var proxy = CreateProxy();

            char arg;

            proxy.CharOutParameter(out arg);

            Assert.AreEqual(Char.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt16OutArg()
        {
            var proxy = CreateProxy();

            short arg;

            proxy.Int16OutParameter(out arg);

            Assert.AreEqual(Int16.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt32OutArg()
        {
            var proxy = CreateProxy();

            int arg;

            proxy.Int32OutParameter(out arg);

            Assert.AreEqual(Int32.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt64OutArg()
        {
            var proxy = CreateProxy();

            long arg;

            proxy.Int64OutParameter(out arg);

            Assert.AreEqual(Int64.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt16OutArg()
        {
            var proxy = CreateProxy();

            ushort arg;

            proxy.UInt16OutParameter(out arg);
            
            Assert.AreEqual(UInt16.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt32OutArg()
        {
            var proxy = CreateProxy();

            uint arg;

            proxy.UInt32OutParameter(out arg);

            Assert.AreEqual(UInt32.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt64OutArg()
        {
            var proxy = CreateProxy();

            ulong arg;

            proxy.UInt64OutParameter(out arg);

            Assert.AreEqual(UInt64.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithSingleOutArg()
        {
            var proxy = CreateProxy();

            float arg;

            proxy.SingleOutParameter(out arg);

            Assert.AreEqual(Single.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithDoubleOutArg()
        {
            var proxy = CreateProxy();

            double arg;

            proxy.DoubleOutParameter(out arg);

            Assert.AreEqual(Double.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithDecimalOutArg()
        {
            var proxy = CreateProxy();

            decimal arg;

            proxy.DecimalOutParameter(out arg);

            Assert.AreEqual(Decimal.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithReferenceTypeOutArg()
        {
            var proxy = CreateProxy();

            string arg;

            proxy.ClassOutParameter(out arg);

            Assert.AreEqual("Result", arg);
        }
    }
}
