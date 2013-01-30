using System;

namespace Inception.Reflection
{
    public interface IPropertyGetter
    {
        object GetValue(object target);
    }
}
