using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupRepository : IContactGroupRepository
    {
        private readonly IContactGroupQueryExecutor _contactGroupQueryExecutor;
        private readonly IContactGroupMemberQueryExecutor _contactGroupMemberQueryExecutor;

        public ContactGroupRepository(IContactGroupQueryExecutor contactGroupQueryExecutor, IContactGroupMemberQueryExecutor contactGroupMemberQueryExecutor)
        {
            _contactGroupQueryExecutor = contactGroupQueryExecutor;
            _contactGroupMemberQueryExecutor = contactGroupMemberQueryExecutor;
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
                _contactGroupMemberQueryExecutor.DeleteByContactGroupId(contactGroup.Id.Value);
            }

            foreach (var member in contactGroup.Members)
            {
                member.ContactGroupId = contactGroup.Id.Value;
                _contactGroupMemberQueryExecutor.Insert(member);
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