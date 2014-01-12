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
        [HttpGet]
        [ActionName("VerbDefault")]
        public IEnumerable<ContactModel> Get()
        {
            return _service.FindAll().Select(Mapper.Map<IContact, ContactModel>);
        }

        // GET api/contacts/{guid}
        [HttpGet]
        [ActionName("VerbDefault")]
        public ContactModel Get(Guid identifier)
        {
            return Mapper.Map<IContact, ContactModel>(_service.FindByIdentifier(identifier));
        }

        // POST api/contacts
        [HttpPost]
        [ActionName("VerbDefault")]
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
        [HttpPost]
        [ActionName("VerbDefault")]
        public void Put(Guid identifier, [FromBody]ContactModel contactModel)
        {
            IContact contact = _service.FindByIdentifier(contactModel.Identifier);
            Mapper.Map(contactModel, (Contact)contact);
            _service.Save(contact);
        }

        // DELETE api/contacts/{guid}
        [HttpDelete]
        [ActionName("VerbDefault")]
        public void Delete(Guid identifier)
        {
            _service.DeleteByIdentifier(identifier);
        }
    }
}