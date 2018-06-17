using System;

namespace InceptionCore.Proxying
{
    public interface IInterceptor
    {
        void Intercept(IInvocation invocation);
    }
}