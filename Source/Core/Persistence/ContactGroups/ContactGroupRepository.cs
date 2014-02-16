using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupRepository : IContactGroupRepository
    {
        private readonly IContactGroupQueryExecutor _contactGroupQueryExecutor;
        private readonly IContactGroupMemberQueryExecutor _contactGroupMemberQueryExecutor;
        private readonly IContactGroupMemberRelationshipQueryExecutor _contactGroupMemberRelationshipQueryExecutor;

        public ContactGroupRepository(IContactGroupQueryExecutor contactGroupQueryExecutor, IContactGroupMemberQueryExecutor contactGroupMemberQueryExecutor, IContactGroupMemberRelationshipQueryExecutor contactGroupMemberRelationshipQueryExecutor)
        {
            _contactGroupQueryExecutor = contactGroupQueryExecutor;
            _contactGroupMemberQueryExecutor = contactGroupMemberQueryExecutor;
            _contactGroupMemberRelationshipQueryExecutor = contactGroupMemberRelationshipQueryExecutor;
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

                foreach (var relationship in member.Relationships)
                {
                    _contactGroupMemberRelationshipQueryExecutor.Insert(member.Id.Value, relationship);
                }
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