using System;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactQueryExecutor
    {
        void Insert(IContact contact);
        void Update(IContact contact);
        IContact SelectByIdentifier(Guid identifier);
    }
}