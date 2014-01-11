using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

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
            SqlMapper.Update("UpdateContactGroup", contactGroup);
        }

        public List<IContactGroup> SelectAll()
        {
            return (List<IContactGroup>)SqlMapper.QueryForList<IContactGroup>("SelectAllContactGroups", null);
        }

        public IContactGroup SelectByIdentifier(Guid identifier)
        {
            return SqlMapper.QueryForObject<IContactGroup>("SelectContactGroupByIdentifier", identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            SqlMapper.Delete("DeleteContactGroupByIdentifier", identifier);
        }
    }
}