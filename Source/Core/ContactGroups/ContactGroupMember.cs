using System;
using System.Collections.Generic;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroupMember
    {
        public long? Id { get; set; }
        public long? ContactGroupId { get; set; }
        public Guid ContactIdentifier { get; set; }

        protected readonly List<string> _relationships = new List<string>();
        public IReadOnlyList<string> Relationships
        {
            get { return _relationships.AsReadOnly(); }
        }

        public void AddRelationship(string relationship)
        {
            _relationships.Add(relationship);
        }
    }
}