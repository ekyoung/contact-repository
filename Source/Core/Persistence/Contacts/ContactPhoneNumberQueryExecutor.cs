using System.Collections;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactPhoneNumberQueryExecutor : QueryExecutor, IContactPhoneNumberQueryExecutor
    {
        public void Insert(long contactId, ContactPhoneNumber contactPhoneNumber)
        {
            var parameterObject = new Hashtable
            {
                {"ContactId", contactId},
                {"PhoneNumber", contactPhoneNumber.PhoneNumber},
                {"Nickname", contactPhoneNumber.Nickname},
                {"IsPrimary", contactPhoneNumber.IsPrimary}
            };
            SqlMapper.Insert("InsertContactPhoneNumber", parameterObject);
        }

        public void DeleteByContactId(long contactId)
        {
            SqlMapper.Delete("DeleteContactPhoneNumbersByContactId", contactId);
        }
    }
}