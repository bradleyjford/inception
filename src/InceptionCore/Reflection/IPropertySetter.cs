﻿using System;

namespace InceptionCore.Reflection
{
    public interface IPropertySetter
    {
        void SetValue(object target, object value);
    }
}