﻿using System;

namespace InceptionCore.Proxying
{
    public abstract class Interceptor : IInterceptor
    {
        public virtual void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}