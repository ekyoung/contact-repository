using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Persistence.ContactGroups
{
    public interface IContactGroupQueryExecutor
    {
        void Insert(IContactGroup contactGroup);
        void Update(IContactGroup contactGroup);
        List<IContactGroup> SelectAll();
        IContactGroup SelectByIdentifier(Guid identifier);
        void DeleteByIdentifier(Guid identifier); 
    }
}