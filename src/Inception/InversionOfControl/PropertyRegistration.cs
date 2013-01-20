using System;

namespace Inception.InversionOfControl
{
	public sealed class PropertyRegistration
	{
		private readonly Type _propertyType;

		public PropertyRegistration(Type propertyType)
		{
			_propertyType = propertyType;
		}
	}
}
