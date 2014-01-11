using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactEmailAddressQueryExecutor : QueryExecutor, IContactEmailAddressQueryExecutor
    {
        public void Insert(ContactEmailAddress contactEmailAddress)
        {
            SqlMapper.Insert("InsertContactEmailAddress", contactEmailAddress);
        }

        public void DeleteByContactId(long contactId)
        {
            SqlMapper.Delete("DeleteContactEmailAddressesByContactId", contactId);
        }
    }
}