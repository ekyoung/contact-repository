using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.ContactGroups;

namespace EthanYoung.ContactRepository.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository, IContactGroupRepository contactGroupRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Save(IContact contact)
        {
            _contactRepository.Save(contact);
        }

        public List<IContact> FindAll()
        {
            return _contactRepository.FindAll();
        }

        public IContact FindByIdentifier(Guid identifier)
        {
            return _contactRepository.FindByIdentifier(identifier);
        }

        public void DeleteByIdentifier(Guid identifier)
        {
            _contactRepository.DeleteByIdentifier(identifier);
        }
    }

    public interface IContactService : IService
    {
        void Save(IContact contact);
        IContact FindByIdentifier(Guid identifier);
        List<IContact> FindAll();
        void DeleteByIdentifier(Guid identifier);
    }
}