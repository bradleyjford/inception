using System;
using System.Collections.Generic;

namespace InceptionCore.InversionOfControl
{
    public sealed class RegistrationDictionary :
        Dictionary<RegistrationKey, IRegistration>
    {
        public void Merge(RegistrationDictionary registrations)
        {
            foreach (var registration in registrations)
            {
                Add(registration.Key, registration.Value);
            }
        }
    }
}