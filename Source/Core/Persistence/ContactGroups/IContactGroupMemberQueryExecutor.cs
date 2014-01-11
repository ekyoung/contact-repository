using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public interface IContactGroupMemberQueryExecutor
    {
        void Insert(ContactGroupMember contactGroupMember);
        void DeleteByContactGroupId(long contactGroupId);
    }
}