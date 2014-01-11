using System;
using System.Collections.Generic;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public interface IContactGroupRepository : IRepository
    {
        void Save(IContactGroup contactGroup);
        List<IContactGroup> FindAll();
        IContactGroup FindByIdentifier(Guid identifier);
        void DeleteByIdentifier(Guid identifier); 
    }
}