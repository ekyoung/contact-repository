using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactGroupRepository _contactGroupRepository;

        public ContactService(IContactRepository contactRepository, IContactGroupRepository contactGroupRepository)
        {
            _contactRepository = contactRepository;
            _contactGroupRepository = contactGroupRepository;
        }

        public void Save(IContact contact)
        {
            _contactRepository.Save(contact);
        }

        public void Save(IContactGroup contactGroup)
        {
            _contactGroupRepository.Save(contactGroup);
        }

        public List<IContact> FindAll()
        {
            return _contactRepository.FindAll();
        }

        public IContact FindByIdentifier(Guid identifier)
        {
            return _contactRepository.FindByIdentifier(identifier);
        }

        public IContactGroup FindContactGroupByIdentifier(Guid identifier)
        {
            return _contactGroupRepository.FindByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            _contactRepository.DeleteByIdentifier(identifier);
        }
    }

    public interface IContactService : IService
    {
        void Save(IContact contact);
        void Save(IContactGroup contactGroup);
        IContact FindByIdentifier(Guid identifier);
        IContactGroup FindContactGroupByIdentifier(Guid identifier);
        List<IContact> FindAll();
        void DeleteByIdentifier(Guid identifier);
    }
}