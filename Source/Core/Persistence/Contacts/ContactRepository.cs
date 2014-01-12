using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactQueryExecutor _contactQueryExecutor;
        private readonly IContactEmailAddressQueryExecutor _contactEmailAddressQueryExecutor;
        private readonly IContactPhoneNumberQueryExecutor _contactPhoneNumberQueryExecutor;

        public ContactRepository(IContactQueryExecutor contactQueryExecutor, IContactEmailAddressQueryExecutor contactEmailAddressQueryExecutor, IContactPhoneNumberQueryExecutor contactPhoneNumberQueryExecutor)
        {
            _contactQueryExecutor = contactQueryExecutor;
            _contactEmailAddressQueryExecutor = contactEmailAddressQueryExecutor;
            _contactPhoneNumberQueryExecutor = contactPhoneNumberQueryExecutor;
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
                _contactPhoneNumberQueryExecutor.DeleteByContactId(contact.Id.Value);
            }

            foreach (var contactEmailAddress in contact.EmailAddresses)
            {
                contactEmailAddress.ContactId = contact.Id.Value;
                _contactEmailAddressQueryExecutor.Insert(contactEmailAddress);
            }

            foreach (var contactPhoneNumber in contact.PhoneNumbers)
            {
                contactPhoneNumber.ContactId = contact.Id.Value;
                _contactPhoneNumberQueryExecutor.Insert(contactPhoneNumber);
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