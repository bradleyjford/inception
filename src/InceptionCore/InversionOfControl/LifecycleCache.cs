using System;

namespace InceptionCore.InversionOfControl
{
    sealed class LifecycleCache : LazyCache<Type, IContainerLifecycle>
    {
        protected override IContainerLifecycle CreateItem(Type type)
        {
            return (IContainerLifecycle)Activator.CreateInstance(type);
        }
    }
}