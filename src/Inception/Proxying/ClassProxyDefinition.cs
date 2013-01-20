﻿using System;

namespace Inception.Proxying
{
	internal class ClassProxyDefinition : ProxyDefinition
	{
		public ClassProxyDefinition(Type type, InterfaceDefinition[] interfaces) 
			: base(type, interfaces)
		{
			if (type.IsInterface)
			{
				throw new ArgumentException("Specified type cannot be an interface type.", "type");
			}
		}
	}
}
