using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactEmailAddressQueryExecutor
    {
        void Insert(long contactId, ContactEmailAddress contactEmailAddress);
        void DeleteByContactId(long contactId);
    }
}