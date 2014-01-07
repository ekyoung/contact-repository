using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactPhoneNumberQueryExecutor
    {
        void Insert(long contactId, ContactPhoneNumber contactPhoneNumber);
        void DeleteByContactId(long contactId);
    }
}