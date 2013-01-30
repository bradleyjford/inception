using System;

namespace Inception.Reflection
{
    public sealed class PropertySetter<T> : IPropertySetter
    {
        private readonly Action<object, T> _delegate;

        internal PropertySetter(Action<object, T> @delegate)
        {
            _delegate = @delegate;
        }

        public void SetValue(object target, object value)
        {
            _delegate(target, (T)value);
        }
    }
}
