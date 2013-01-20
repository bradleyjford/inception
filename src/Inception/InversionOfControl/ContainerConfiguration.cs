using System;
using System.Collections.Generic;

namespace Inception.InversionOfControl
{
	public class ContainerConfiguration
	{
		private readonly List<IRegistration> _registrations = new List<IRegistration>(); 

		public void AddRegistrationConfiguration(IRegistration configuration)
		{
			_registrations.Add(configuration);
		}

		public IEnumerable<IRegistration> Registrations
		{
			get { return _registrations; }
		}
	}
}
