using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public class ContactQueryExecutor : QueryExecutor, IContactQueryExecutor
    {
        public void Insert(IContact contact)
        {
            SqlMapper.Insert("InsertContact", contact);
        }

        public void Update(IContact contact)
        {
            SqlMapper.Update("UpdateContact", contact);
        }

        public List<IContact> SelectAll()
        {
            return (List<IContact>)SqlMapper.QueryForList<IContact>("SelectAllContacts", null);
        }

        public IContact SelectByIdentifier(Guid identifier)
        {
            return SqlMapper.QueryForObject<IContact>("SelectContactByIdentifier", identifier);
        }
    }
}