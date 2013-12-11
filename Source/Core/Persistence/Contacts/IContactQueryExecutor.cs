using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Persistence.Contacts
{
    public interface IContactQueryExecutor
    {
        void Insert(IContact contact);
        void Update(IContact contact);
        List<IContact> SelectAll();
        IContact SelectByIdentifier(Guid identifier);
    }
}