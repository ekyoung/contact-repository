using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupRepository : IContactGroupRepository
    {
        private readonly IContactGroupQueryExecutor _contactGroupQueryExecutor;

        public ContactGroupRepository(IContactGroupQueryExecutor contactGroupQueryExecutor)
        {
            _contactGroupQueryExecutor = contactGroupQueryExecutor;
        }

        public void Save(IContactGroup contactGroup)
        {
            if (contactGroup.Id == null)
            {
                _contactGroupQueryExecutor.Insert(contactGroup);
            }
            else
            {
                _contactGroupQueryExecutor.Update(contactGroup);
            }
        }

        public List<IContactGroup> FindAll()
        {
            return _contactGroupQueryExecutor.SelectAll();
        }

        public IContactGroup FindByIdentifier(Guid identifier)
        {
            return _contactGroupQueryExecutor.SelectByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            _contactGroupQueryExecutor.DeleteByIdentifier(identifier);
        }
    }
}