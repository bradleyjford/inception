using System;
using InceptionCore.Proxying;

namespace InceptionCore.Tests.Proxying.Model
{
    public class TimingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
