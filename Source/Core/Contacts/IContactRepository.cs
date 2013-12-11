using System;
using System.Collections.Generic;

namespace EthanYoung.ContactRepository.Contacts
{
    public interface IContactRepository : IRepository
    {
        void Save(IContact contact);
        List<IContact> FindAll();
        IContact FindByIdentifier(Guid identifier);
    }
}