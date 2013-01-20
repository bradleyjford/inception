using System;

namespace Inception.Proxying
{
	internal class TargetedClassProxyDefinition : TargetedProxyDefinition
	{
		public TargetedClassProxyDefinition(
			Type type, 
			InterfaceDefinition[] interfaces) 
			: base(type, type, interfaces)
		{
			if (type.IsInterface)
			{
				throw new ArgumentException("Specified type cannot be an interace type.", "type");
			}
		}
	}
}
