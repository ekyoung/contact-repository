using System;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroupMember
    {
        public long? ContactGroupId { get; set; }
        public Guid ContactIdentifier { get; set; }
    }
}