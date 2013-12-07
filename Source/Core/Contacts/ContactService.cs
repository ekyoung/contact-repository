using System;

namespace EthanYoung.ContactRepository.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Save(IContact contact)
        {
            _contactRepository.Save(contact);
        }

        public IContact FindByIdentifier(Guid identifier)
        {
            return _contactRepository.FindByIdentifier(identifier);
        }
    }

    public interface IContactService : IService
    {
        void Save(IContact contact);
        IContact FindByIdentifier(Guid identifier);
    }
}