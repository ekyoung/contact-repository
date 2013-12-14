using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactQueryExecutor _contactQueryExecutor;
        private readonly IContactEmailAddressQueryExecutor _contactEmailAddressQueryExecutor;

        public ContactRepository(IContactQueryExecutor contactQueryExecutor, IContactEmailAddressQueryExecutor contactEmailAddressQueryExecutor)
        {
            _contactQueryExecutor = contactQueryExecutor;
            _contactEmailAddressQueryExecutor = contactEmailAddressQueryExecutor;
        }

        public void Save(IContact contact)
        {
            if (contact.Id == null)
            {
                _contactQueryExecutor.Insert(contact);
            }
            else
            {
                _contactQueryExecutor.Update(contact);
                _contactEmailAddressQueryExecutor.DeleteByContactId(contact.Id.Value);
            }

            foreach (var contactEmailAddress in contact.EmailAddresses)
            {
                _contactEmailAddressQueryExecutor.Insert(contact.Id.Value, contactEmailAddress);
            }
        }

        public List<IContact> FindAll()
        {
            return _contactQueryExecutor.SelectAll();
        }

        public IContact FindByIdentifier(Guid identifier)
        {
            return _contactQueryExecutor.SelectByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            _contactQueryExecutor.DeleteByIdentifier(identifier);
        }
    }
}