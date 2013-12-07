using System;

namespace EthanYoung.ContactRepository.Contacts
{
    public class Contact : IContact
    {
        public long? Id { get; set; }
        public Guid Identifier { get; set; }
        public Name Name { get; set; } 
    }

    public interface IContact
    {
        long? Id { get; set; }
        Guid Identifier { get; set; }
        Name Name { get; set; }
    }
}