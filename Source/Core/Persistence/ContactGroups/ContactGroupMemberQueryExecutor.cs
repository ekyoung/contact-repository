using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupMemberQueryExecutor : QueryExecutor, IContactGroupMemberQueryExecutor
    {
        public void Insert(ContactGroupMember contactGroupMember)
        {
            SqlMapper.Insert("InsertContactGroupMember", contactGroupMember);
        }

        public void DeleteByContactGroupId(long contactGroupId)
        {
            SqlMapper.Delete("DeleteContactGroupMembersByContactGroupId", contactGroupId);
        }
    }
}