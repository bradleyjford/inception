using System;
using InceptionCore.Proxying;
using Xunit;

namespace InceptionCore.Tests.Proxying.MethodInvocations
{
    
    public class ClassProxyRefArgumentTests : IDisposable
    {
        private ProxyFactory _factory;

        public ClassProxyRefArgumentTests()
        {
            _factory = new ProxyFactory(
                "ClassProxyRefArgumentTests.Proxies",
                "ClassProxyRefArgumentTests.Proxies.dll");
        }

        public void Dispose()
        {
            //_factory.SaveAssembly();
        }

        private RefParameterTestModel CreateProxy()
        {
            return _factory.CreateProxy<RefParameterTestModel>(p => { });
        }

        [Fact]
        public void CanInvokeMethodWithBooleanOutArg()
        {
            var proxy = CreateProxy();

            var arg = false;

            proxy.BooleanRefParameter(ref arg);

            Assert.Equal(true, arg);
        }

        [Fact]
        public void CanInvokeMethodWithByteOutArg()
        {
            var proxy = CreateProxy();

            var arg = Byte.MinValue;

            proxy.ByteRefParameter(ref arg);

            Assert.Equal(Byte.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithCharOutArg()
        {
            var proxy = CreateProxy();

            var arg = Char.MinValue;

            proxy.CharRefParameter(ref arg);

            Assert.Equal(Char.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt16OutArg()
        {
            var proxy = CreateProxy();

            var arg = Int16.MinValue;

            proxy.Int16RefParameter(ref arg);

            Assert.Equal(Int16.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt32OutArg()
        {
            var proxy = CreateProxy();

            int arg = Int32.MaxValue;

            proxy.Int32RefParameter(ref arg);

            Assert.Equal(Int32.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt64OutArg()
        {
            var proxy = CreateProxy();

            var arg = Int64.MinValue;

            proxy.Int64RefParameter(ref arg);

            Assert.Equal(Int64.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt16OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt16.MinValue;

            proxy.UInt16RefParameter(ref arg);
            
            Assert.Equal(UInt16.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt32OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt32.MinValue;

            proxy.UInt32RefParameter(ref arg);

            Assert.Equal(UInt32.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt64OutArg()
        {
            var proxy = CreateProxy();

            var arg = UInt64.MinValue;

            proxy.UInt64RefParameter(ref arg);

            Assert.Equal(UInt64.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithSingleOutArg()
        {
            var proxy = CreateProxy();

            var arg = Single.MinValue;

            proxy.SingleRefParameter(ref arg);

            Assert.Equal(Single.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithDoubleOutArg()
        {
            var proxy = CreateProxy();

            var arg = Double.MinValue;

            proxy.DoubleRefParameter(ref arg);

            Assert.Equal(Double.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithDecimalOutArg()
        {
            var proxy = CreateProxy();

            var arg = Decimal.MinValue;

            proxy.DecimalRefParameter(ref arg);

            Assert.Equal(Decimal.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithReferenceTypeOutArg()
        {
            var proxy = CreateProxy();

            var arg = "Test";

            proxy.ClassRefParameter(ref arg);

            Assert.Equal("Result", arg);
        }
    }
}
