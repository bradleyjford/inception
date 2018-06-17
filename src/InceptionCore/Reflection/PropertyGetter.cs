using System;

namespace InceptionCore.Reflection
{
    public sealed class PropertyGetter<T> : IPropertyGetter
    {
        readonly Func<object, T> _delegate;

        internal PropertyGetter(Func<object, T> @delegate)
        {
            _delegate = @delegate;
        }

        public object GetValue(object target)
        {
            return _delegate(target);
        }
    }
}