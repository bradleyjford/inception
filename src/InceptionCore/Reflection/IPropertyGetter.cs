using System;

namespace InceptionCore.Reflection
{
    public interface IPropertyGetter
    {
        object GetValue(object target);
    }
}