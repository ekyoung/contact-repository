using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactPhoneNumberQueryExecutor
    {
        void Insert(ContactPhoneNumber contactPhoneNumber);
        void DeleteByContactId(long contactId);
    }
}