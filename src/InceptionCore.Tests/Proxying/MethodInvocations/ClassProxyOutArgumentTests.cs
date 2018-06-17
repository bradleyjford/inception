using System;
using InceptionCore.Proxying;
using Xunit;

namespace InceptionCore.Tests.Proxying.MethodInvocations
{
    
    public class ClassProxyOutArgumentTests : IDisposable
    {
        private ProxyFactory _factory;

        public ClassProxyOutArgumentTests()
        {
            _factory = new ProxyFactory(
                "ClassProxyOutArgumentTests.Proxies",
                "ClassProxyOutArgumentTests.dll");
        }

        public void Dispose()
        {
            //_factory.SaveAssembly();
        }

        private OutParameterTestModel CreateProxy()
        {
            return _factory.CreateProxy<OutParameterTestModel>(p => { });
        }

        [Fact]
        public void CanInvokeMethodWithBooleanOutArg()
        {
            var proxy = CreateProxy();

            bool arg;

            proxy.BooleanOutParameter(out arg);

            Assert.Equal(true, arg);
        }

        [Fact]
        public void CanInvokeMethodWithByteOutArg()
        {
            var proxy = CreateProxy();

            byte arg;

            proxy.ByteOutParameter(out arg);

            Assert.Equal(Byte.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithCharOutArg()
        {
            var proxy = CreateProxy();

            char arg;

            proxy.CharOutParameter(out arg);

            Assert.Equal(Char.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt16OutArg()
        {
            var proxy = CreateProxy();

            short arg;

            proxy.Int16OutParameter(out arg);

            Assert.Equal(Int16.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt32OutArg()
        {
            var proxy = CreateProxy();

            int arg;

            proxy.Int32OutParameter(out arg);

            Assert.Equal(Int32.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithInt64OutArg()
        {
            var proxy = CreateProxy();

            long arg;

            proxy.Int64OutParameter(out arg);

            Assert.Equal(Int64.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt16OutArg()
        {
            var proxy = CreateProxy();

            ushort arg;

            proxy.UInt16OutParameter(out arg);
            
            Assert.Equal(UInt16.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt32OutArg()
        {
            var proxy = CreateProxy();

            uint arg;

            proxy.UInt32OutParameter(out arg);

            Assert.Equal(UInt32.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithUInt64OutArg()
        {
            var proxy = CreateProxy();

            ulong arg;

            proxy.UInt64OutParameter(out arg);

            Assert.Equal(UInt64.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithSingleOutArg()
        {
            var proxy = CreateProxy();

            float arg;

            proxy.SingleOutParameter(out arg);

            Assert.Equal(Single.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithDoubleOutArg()
        {
            var proxy = CreateProxy();

            double arg;

            proxy.DoubleOutParameter(out arg);

            Assert.Equal(Double.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithDecimalOutArg()
        {
            var proxy = CreateProxy();

            decimal arg;

            proxy.DecimalOutParameter(out arg);

            Assert.Equal(Decimal.MaxValue, arg);
        }

        [Fact]
        public void CanInvokeMethodWithReferenceTypeOutArg()
        {
            var proxy = CreateProxy();

            string arg;

            proxy.ClassOutParameter(out arg);

            Assert.Equal("Result", arg);
        }
    }
}
