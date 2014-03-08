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

        public IContactGroup SelectByIdentifier(string identifier)
        {
            return SqlMapper.QueryForObject<IContactGroup>("SelectContactGroupByIdentifier", identifier);
        }

        public void DeleteByIdentifier(string identifier)
        {
            SqlMapper.Delete("DeleteContactGroupByIdentifier", identifier);
        }
    }

    public interface IContactGroupQueryExecutor
    {
        void Insert(IContactGroup contactGroup);
        void Update(IContactGroup contactGroup);
        List<IContactGroup> SelectAll();
        IContactGroup SelectByIdentifier(string identifier);
        void DeleteByIdentifier(string identifier);
    }
}