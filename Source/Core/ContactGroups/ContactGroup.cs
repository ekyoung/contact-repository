using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroup : IContactGroup
    {
        public long? Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; } 

        protected readonly List<ContactGroupMember> _members = new List<ContactGroupMember>();
        public ReadOnlyCollection<ContactGroupMember> Members
        {
            get { return _members.AsReadOnly(); }
        }

        public void AddMember(string contactIdentifier)
        {
            _members.Add(new ContactGroupMember {ContactIdentifier = contactIdentifier});
        }

        public void AddMember(string contactIdentifier, IEnumerable<string> relationships)
        {
            var member = new ContactGroupMember
            {
                ContactIdentifier = contactIdentifier
            };

            foreach (var relationship in relationships)
            {
                member.AddRelationship(relationship);
            }

            _members.Add(member);
        }

        public ContactGroupMember GetMember(string contactIdentifier)
        {
            return _members.FirstOrDefault(x => x.ContactIdentifier == contactIdentifier);
        }

        public bool IsMember(string contactIdentifier)
        {
            return GetMember(contactIdentifier) != null;
        }

        public void ClearMembers()
        {
            _members.Clear();
        }
    }

    public interface IContactGroup
    {
        long? Id { get; set; }
        string Identifier { get; set; }
        string Name { get; set; }
        ReadOnlyCollection<ContactGroupMember> Members { get; }
        void AddMember(string contactIdentifier);
        void AddMember(string contactIdentifier, IEnumerable<string> relationships);
        ContactGroupMember GetMember(string contactIdentifier);
        bool IsMember(string contactIdentifier);
        void ClearMembers();
    }
}