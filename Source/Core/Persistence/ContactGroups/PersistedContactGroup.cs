using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class PersistedContactGroup : ContactGroup
    {
        public List<ContactGroupMember> PersistedMembers
        {
            set { _members.AddRange(value); }
        }
    }
}