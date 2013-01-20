using System;

namespace Inception.Reflection
{
	public sealed class PropertyGetter<T> : IPropertyGetter
	{
		private readonly Func<object, T> _delegate;

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
