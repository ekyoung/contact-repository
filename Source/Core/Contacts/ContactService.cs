using System.Collections.Generic;

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

        public List<IContact> FindAll()
        {
            return _contactRepository.FindAll();
        }

        public IContact FindByIdentifier(string identifier)
        {
            return _contactRepository.FindByIdentifier(identifier);
        }

        public void DeleteByIdentifier(string identifier)
        {
            _contactRepository.DeleteByIdentifier(identifier);
        }
    }

    public interface IContactService : IService
    {
        void Save(IContact contact);
        IContact FindByIdentifier(string identifier);
        List<IContact> FindAll();
        void DeleteByIdentifier(string identifier);
    }
}