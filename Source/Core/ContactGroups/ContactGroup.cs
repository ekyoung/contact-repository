using System;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroup : IContactGroup
    {
        public long? Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; } 
    }

    public interface IContactGroup
    {
        long? Id { get; set; }
        Guid Identifier { get; set; }
        string Name { get; set; }
    }
}