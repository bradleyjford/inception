using System;

namespace Inception.Reflection
{
	public interface IPropertySetter
	{
		void SetValue(object target, object value);
	}
}
