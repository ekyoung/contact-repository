using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupQueryExecutor : QueryExecutor, IContactGroupQueryExecutor
    {
        public void Insert(IContactGroup contactGroup)
        {
            SqlMapper.Insert("InsertContactGroup", contactGroup);
        }

        public void Update(IContactGroup contactGroup)
        {
            throw new NotImplementedException();
        }

        public List<IContact> SelectAll()
        {
            throw new NotImplementedException();
        }

        public IContactGroup SelectByIdentifier(Guid identifier)
        {
            return SqlMapper.QueryForObject<IContactGroup>("SelectContactGroupByIdentifier", identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            throw new NotImplementedException();
        }
    }
}