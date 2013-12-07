using System;

namespace EthanYoung.ContactRepository.Contacts
{
    public interface IContactRepository : IRepository
    {
        void Save(IContact contact);
        IContact FindByIdentifier(Guid identifier);
    }
}