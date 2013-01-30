using System;
using System.Collections;
using System.Collections.Generic;

namespace Inception.InversionOfControl
{
    public class Registry : IEnumerable<IRegistration>
    {
        private readonly Dictionary<RegistrationKey, IRegistration> _registrations =
            new Dictionary<RegistrationKey, IRegistration>();

        public void Add(IRegistration registration)
        {
            _registrations.Add(RegistrationKey.For(registration.BaseType, registration.Name), registration);
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
