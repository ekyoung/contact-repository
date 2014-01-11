using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactEmailAddressQueryExecutor
    {
        void Insert(ContactEmailAddress contactEmailAddress);
        void DeleteByContactId(long contactId);
    }
}