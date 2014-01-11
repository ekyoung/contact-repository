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
            _contactGroupQueryExecutor.Insert(contactGroup);
        }

        public List<IContactGroup> FindAll()
        {
            throw new NotImplementedException();
        }

        public IContactGroup FindByIdentifier(Guid identifier)
        {
            return _contactGroupQueryExecutor.SelectByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            throw new NotImplementedException();
        }
    }
}