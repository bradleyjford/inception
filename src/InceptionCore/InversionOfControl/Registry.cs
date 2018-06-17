using System;
using System.Collections;
using System.Collections.Generic;

namespace InceptionCore.InversionOfControl
{
    public class Registry : IEnumerable<IRegistration>
    {
        readonly Dictionary<RegistrationKey, IRegistration> _registrations =
            new Dictionary<RegistrationKey, IRegistration>();

        public IRegistration this[RegistrationKey key] => _registrations[key];

        public IEnumerator<IRegistration> GetEnumerator()
        {
            return _registrations.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IRegistration registration)
        {
            _registrations.Add(RegistrationKey.For(registration.BaseType, registration.Name), registration);
        }

        public bool Contains(RegistrationKey key)
        {
            return _registrations.ContainsKey(key);
        }
    }
}