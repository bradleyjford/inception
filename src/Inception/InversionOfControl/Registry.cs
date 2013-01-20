using System;
using System.Collections;
using System.Collections.Generic;

namespace Inception.InversionOfControl
{
	public class Registry : IEnumerable<IRegistration>
	{
		private readonly Action<ContainerConfiguration> _configuration;

		private readonly Dictionary<RegistrationKey, IRegistration> _registrations =
			new Dictionary<RegistrationKey, IRegistration>();

		public Registry(Action<ContainerConfiguration> configuration)
		{
			_configuration = configuration;
		}

		public void Build(IContainer container)
		{
			var config = new ContainerConfiguration();

			_configuration.Invoke(config);

			foreach (var registration in config.Registrations)
			{
				var key = RegistrationKey.For(registration.BaseType, registration.Name);

				_registrations.Add(key, registration);
			}
		}

		public bool Contains(RegistrationKey key)
		{
			return _registrations.ContainsKey(key);
		}

		public IRegistration this[RegistrationKey key]
		{
			get { return _registrations[key]; }
		}

		public IEnumerator<IRegistration> GetEnumerator()
		{
			return _registrations.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
