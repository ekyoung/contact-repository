using System.Collections;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactEmailAddressQueryExecutor : QueryExecutor, IContactEmailAddressQueryExecutor
    {
        public void Insert(long contactId, ContactEmailAddress contactEmailAddress)
        {
            var parameterObject = new Hashtable
            {
                {"ContactId", contactId},
                {"EmailAddress", contactEmailAddress.EmailAddress},
                {"Nickname", contactEmailAddress.Nickname},
                {"IsPrimary", contactEmailAddress.IsPrimary}
            };
            SqlMapper.Insert("InsertContactEmailAddress", parameterObject);
        }

        public void DeleteByContactId(long contactId)
        {
            SqlMapper.Delete("DeleteContactEmailAddressesByContactId", contactId);
        }
    }
}