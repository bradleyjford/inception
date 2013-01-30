using System;

namespace Inception.Proxying
{
    public interface IInterceptor
    {
        void Intercept(IInvocation invocation);
    }
}