using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using EthanYoung.ContactRepository;
using EthanYoung.ContactRepository.ContactGroups;
using EthanYoung.ContactRepository.Contacts;
using Web.Models;

namespace Web.Controllers
{
    public class ContactGroupsController : ApiController
    {
        private readonly IContactGroupService _service;

        public ContactGroupsController()
        {
            _service = DependencyRegistry.Resolve<IContactGroupService>();
        }

        // GET api/contactGroups
        [HttpGet]
        [ActionName("VerbDefault")]
        public IEnumerable<ContactGroupModel> Get()
        {
            return _service.FindAll().Select(Mapper.Map<IContactGroup, ContactGroupModel>);
        }
        
        // GET api/contactGroups/{guid}
        [HttpGet]
        [ActionName("VerbDefault")]
        public ContactGroupModel Get(Guid identifier)
        {
            return Mapper.Map<IContactGroup, ContactGroupModel>(_service.FindByIdentifier(identifier));
        }
        
        // GET api/contactGroups/{guid}/members
        [HttpGet]
        [ActionName("members")]
        public IEnumerable<ContactModel> GetMembers(Guid identifier)
        {
            return _service.GetMembers(identifier).Select(Mapper.Map<IContact, ContactModel>);
        }
        
        // POST api/contactGroups
        [HttpPost]
        [ActionName("VerbDefault")]
        public void Post([FromBody]ContactGroupModel contactGroupModel)
        {
            var contactGroup = Mapper.Map<ContactGroupModel, ContactGroup>(contactGroupModel);
            if (contactGroup.Identifier == Guid.Empty)
            {
                contactGroup.Identifier = Guid.NewGuid();
            }

            _service.Save(contactGroup);
        }

        // PUT api/contactGroups/{guid}
        [HttpPut]
        [ActionName("VerbDefault")]
        public void Put(Guid identifier, [FromBody]ContactGroupModel contactGroupModel)
        {
            IContactGroup contactGroup = _service.FindByIdentifier(contactGroupModel.Identifier);
            Mapper.Map(contactGroupModel, (ContactGroup)contactGroup);
            _service.Save(contactGroup);
        }

        // DELETE api/contactGroups/{guid}
        [HttpDelete]
        [ActionName("VerbDefault")]
        public void Delete(Guid identifier)
        {
            _service.DeleteByIdentifier(identifier);
        }
    }
}