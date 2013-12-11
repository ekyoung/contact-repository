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

        private ContactModel ToModel(IContact contact)
        {
            return new ContactModel
            {
                Identifier = contact.Identifier,
                FirstName = contact.Name.First,
                LastName = contact.Name.Last
            };
        }
        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}