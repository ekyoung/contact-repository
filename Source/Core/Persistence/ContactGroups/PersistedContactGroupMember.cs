using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public class PersistedContactGroupMember : ContactGroupMember
    {
        public List<string> PersistedRelationships
        {
            set { _relationships.AddRange(value); }
        }
    }
}