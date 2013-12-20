using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
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
            return _service.FindAll().Select(Mapper.Map<IContact, ContactModel>);
        }

        // GET api/contacts/{guid}
        public ContactModel Get(Guid identifier)
        {
            return Mapper.Map<IContact, ContactModel>(_service.FindByIdentifier(identifier));
        }

        // POST api/contacts
        public void Post([FromBody]ContactModel contactModel)
        {
            var contact = Mapper.Map<ContactModel, Contact>(contactModel);
            if (contact.Identifier == Guid.Empty)
            {
                contact.Identifier = Guid.NewGuid();
            }

            _service.Save(contact);
        }

        // PUT api/contacts/{guid}
        public void Put(Guid identifier, [FromBody]ContactModel contactModel)
        {
            IContact contact = _service.FindByIdentifier(contactModel.Identifier);
            Mapper.Map(contactModel, (Contact)contact);
            _service.Save(contact);
        }

        // DELETE api/contacts/{guid}
        public void Delete(Guid identifier)
        {
            _service.DeleteByIdentifier(identifier);
        }
    }
}