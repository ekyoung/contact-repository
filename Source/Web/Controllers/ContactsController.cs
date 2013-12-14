using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EthanYoung.ContactRepository;
using EthanYoung.ContactRepository.Contacts;
using Web.Models;

namespace Web.Controllers
{
    public class ContactsController : ApiController
    {
        private readonly IContactService _service;

        public ContactsController()
        {
            _service = DependencyRegistry.Resolve<IContactService>();
        }

        // GET api/contacts
        public IEnumerable<ContactModel> Get()
        {
            return _service.FindAll().Select(ToModel);
        }

        // GET api/contacts/{guid}
        public ContactModel Get(Guid identifier)
        {
            return ToModel(_service.FindByIdentifier(identifier));
        }

        // POST api/contacts
        public void Post([FromBody]ContactModel contactModel)
        {
            var contact = new Contact
            {
                Identifier = contactModel.Identifier == Guid.Empty ? Guid.NewGuid() : contactModel.Identifier
            };
            MapFromModel(contact, contactModel);
            _service.Save(contact);
        }

        // PUT api/contacts/{guid}
        public void Put(Guid identifier, [FromBody]ContactModel contactModel)
        {
            IContact contact = _service.FindByIdentifier(contactModel.Identifier);
            MapFromModel(contact, contactModel);
            _service.Save(contact);
        }

        // DELETE api/contacts/{guid}
        public void Delete(Guid identifier)
        {
            _service.DeleteByIdentifier(identifier);
        }

        private ContactModel ToModel(IContact contact)
        {
            return new ContactModel
            {
                Identifier = contact.Identifier,
                FirstName = contact.Name.First,
                LastName = contact.Name.Last
            };
        }

        private void MapFromModel(IContact contact, ContactModel contactModel)
        {
            contact.Name = new Name(contactModel.FirstName, contactModel.LastName);
        }
    }
}