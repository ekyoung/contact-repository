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

        public void DeleteByIdentifier(Guid identifier)
        {
            SqlMapper.Delete("DeleteContactByIdentifier", identifier);
        }
    }

    public interface IContactQueryExecutor
    {
        void Insert(IContact contact);
        void Update(IContact contact);
        List<IContact> SelectAll();
        IContact SelectByIdentifier(Guid identifier);
        void DeleteByIdentifier(Guid identifier);
    }
}