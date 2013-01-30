using System;
using Inception.Proxying;
using NUnit.Framework;

namespace Inception.Tests.Proxying.MethodInvocations
{
    [TestFixture]
    public class ClassProxyRefArgumentTests
    {
        private ProxyFactory _factory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _factory = new ProxyFactory(
                "ClassProxyRefArgumentTests.Proxies",
                "ClassProxyRefArgumentTests.Proxies.dll");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //_factory.SaveAssembly();
        }

        private RefParameterTestModel CreateProxy()
        {
            return _factory.CreateProxy<RefParameterTestModel>(p => { });
        }

        [Test]
        public void CanInvokeMethodWithBooleanOutArg()
        {
            var proxy = CreateProxy();

            var arg = false;

            proxy.BooleanRefParameter(ref arg);

            Assert.AreEqual(true, arg);
        }

        [Test]
        public void CanInvokeMethodWithByteOutArg()
        {
            var proxy = CreateProxy();

            var arg = Byte.MinValue;

            proxy.ByteRefParameter(ref arg);

            Assert.AreEqual(Byte.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithCharOutArg()
        {
            var proxy = CreateProxy();

            var arg = Char.MinValue;

            proxy.CharRefParameter(ref arg);

            Assert.AreEqual(Char.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt16OutArg()
        {
            var proxy = CreateProxy();

            var arg = Int16.MinValue;

            proxy.Int16RefParameter(ref arg);

            Assert.AreEqual(Int16.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt32OutArg()
        {
            var proxy = CreateProxy();

            int arg = Int32.MaxValue;

            proxy.Int32RefParameter(ref arg);

            Assert.AreEqual(Int32.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithInt64OutArg()
        {
            var proxy = CreateProxy();

            var arg = Int64.MinValue;

            proxy.Int64RefParameter(ref arg);

            Assert.AreEqual(Int64.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt16OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt16.MinValue;

            proxy.UInt16RefParameter(ref arg);
            
            Assert.AreEqual(UInt16.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt32OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt32.MinValue;

            proxy.UInt32RefParameter(ref arg);

            Assert.AreEqual(UInt32.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithUInt64OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt64.MinValue;

            proxy.UInt64RefParameter(ref arg);

            Assert.AreEqual(UInt64.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithSingleOutArg()
        {
            var proxy = CreateProxy();

            var arg = Single.MinValue;

            proxy.SingleRefParameter(ref arg);

            Assert.AreEqual(Single.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithDoubleOutArg()
        {
            var proxy = CreateProxy();

            var arg = Double.MinValue;

            proxy.DoubleRefParameter(ref arg);

            Assert.AreEqual(Double.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithDecimalOutArg()
        {
            var proxy = CreateProxy();

            var arg = Decimal.MinValue;

            proxy.DecimalRefParameter(ref arg);

            Assert.AreEqual(Decimal.MaxValue, arg);
        }

        [Test]
        public void CanInvokeMethodWithReferenceTypeOutArg()
        {
            var proxy = CreateProxy();

            var arg = "Test";

            proxy.ClassRefParameter(ref arg);

            Assert.AreEqual("Result", arg);
        }
    }
}
