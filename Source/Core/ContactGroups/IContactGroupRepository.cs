using System.Collections.Generic;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public interface IContactGroupRepository : IRepository
    {
        void Save(IContactGroup contactGroup);
        List<IContactGroup> FindAll();
        IContactGroup FindByIdentifier(string identifier);
        void DeleteByIdentifier(string identifier); 
    }
}