using System;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactQueryExecutor _queryExecutor;

        public ContactRepository(IContactQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public void Save(IContact contact)
        {
            if (contact.Id == null)
            {
                _queryExecutor.Insert(contact);
            }
            else
            {
                _queryExecutor.Update(contact);
            }
        }

        public IContact FindByIdentifier(Guid identifier)
        {
            return _queryExecutor.SelectByIdentifier(identifier);
        }
    }
}