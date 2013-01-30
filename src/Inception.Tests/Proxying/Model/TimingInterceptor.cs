using System;
using Inception.Proxying;

namespace Inception.Tests.Proxying.Model
{
    public class TimingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
