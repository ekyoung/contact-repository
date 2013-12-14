using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class PersistedContact : Contact
    {
        public List<ContactEmailAddress> PersistedEmailAddresses
        {
            set { _emailAddresses.AddRange(value); }
        }
    }
}