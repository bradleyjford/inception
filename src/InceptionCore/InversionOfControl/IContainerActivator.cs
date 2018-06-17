using System;

namespace InceptionCore.InversionOfControl
{
    public interface IContainerActivator
    {
        object CreateInstance(IContainer container);
    }
}