using System;
using System.Collections.Generic;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroupService : IContactGroupService
    {
        private readonly IContactGroupRepository _contactGroupRepository;

        public ContactGroupService(IContactGroupRepository contactGroupRepository)
        {
            _contactGroupRepository = contactGroupRepository;
        }

        public void Save(IContactGroup contactGroup)
        {
            _contactGroupRepository.Save(contactGroup);
        }

        public List<IContactGroup> FindAll()
        {
            return _contactGroupRepository.FindAll();
        }

        public IContactGroup FindByIdentifier(Guid identifier)
        {
            return _contactGroupRepository.FindByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            _contactGroupRepository.DeleteByIdentifier(identifier);
        }
    }

    public interface IContactGroupService : IService
    {
        void Save(IContactGroup contactGroup);
        List<IContactGroup> FindAll();
        IContactGroup FindByIdentifier(Guid identifier);
        void DeleteByIdentifier(Guid identifier);
    }
}