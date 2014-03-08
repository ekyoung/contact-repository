using System;
using System.Collections.Generic;
using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.ContactGroups
{
    public class ContactGroupService : IContactGroupService
    {
        private readonly IContactGroupRepository _contactGroupRepository;
        private readonly IContactService _contactService;

        public ContactGroupService(IContactGroupRepository contactGroupRepository, IContactService contactService)
        {
            _contactGroupRepository = contactGroupRepository;
            _contactService = contactService;
        }

        public void Save(IContactGroup contactGroup)
        {
            _contactGroupRepository.Save(contactGroup);
        }

        public List<IContactGroup> FindAll()
        {
            return _contactGroupRepository.FindAll();
        }

        public IContactGroup FindByIdentifier(string contactGroupIdentifier)
        {
            return _contactGroupRepository.FindByIdentifier(contactGroupIdentifier);
        }

        public void DeleteByIdentifier(string contactGroupIdentifier)
        {
            _contactGroupRepository.DeleteByIdentifier(contactGroupIdentifier);
        }

        public List<IContact> GetMembers(string contactGroupIdentifier)
        {
            var contactGroup = FindByIdentifier(contactGroupIdentifier);

            var result = new List<IContact>();

            foreach (var contactGroupMember in contactGroup.Members)
            {
                result.Add(_contactService.FindByIdentifier(contactGroupMember.ContactIdentifier));
            }

            return result;
        }
    }

    public interface IContactGroupService : IService
    {
        void Save(IContactGroup contactGroup);
        List<IContactGroup> FindAll();
        IContactGroup FindByIdentifier(string contactGroupIdentifier);
        void DeleteByIdentifier(string contactGroupIdentifier);
        List<IContact> GetMembers(string contactGroupIdentifier);
    }
}