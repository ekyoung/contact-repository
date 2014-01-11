using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactPhoneNumberQueryExecutor : QueryExecutor, IContactPhoneNumberQueryExecutor
    {
        public void Insert(ContactPhoneNumber contactPhoneNumber)
        {
            SqlMapper.Insert("InsertContactPhoneNumber", contactPhoneNumber);
        }

        public void DeleteByContactId(long contactId)
        {
            SqlMapper.Delete("DeleteContactPhoneNumbersByContactId", contactId);
        }
    }
}