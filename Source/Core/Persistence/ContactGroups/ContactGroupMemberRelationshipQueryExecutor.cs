using System.Collections;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class ContactGroupMemberRelationshipQueryExecutor : QueryExecutor, IContactGroupMemberRelationshipQueryExecutor
    {
        public void Insert(long contactGroupMemberId, string relationship)
        {
            var parameters = new Hashtable
            {
                {"ContactGroupMemberId", contactGroupMemberId},
                {"Relationship", relationship}
            };

            SqlMapper.Insert("InsertContactGroupMemberRelationship", parameters);
        }

        public void DeleteByContactGroupId(long contactGroupMemberId)
        {
            SqlMapper.Delete("DeleteContactGroupMemberRelationshipsByContactGroupMemberId", contactGroupMemberId);
        }
    }

    public interface IContactGroupMemberRelationshipQueryExecutor
    {
        void Insert(long contactGroupMemberId, string relationship);
        void DeleteByContactGroupId(long contactGroupMemberId);
    }
}