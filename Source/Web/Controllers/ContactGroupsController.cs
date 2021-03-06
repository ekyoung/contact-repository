﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using EthanYoung.ContactRepository.ContactGroups;
using EthanYoung.ContactRepository.Contacts;
using EthanYoung.ContactRepository.Web.Models;

namespace EthanYoung.ContactRepository.Web.Controllers
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
        public ContactGroupModel Get(string identifier)
        {
            return Mapper.Map<IContactGroup, ContactGroupModel>(_service.FindByIdentifier(identifier));
        }
        
        // GET api/contactGroups/{guid}/members
        [HttpGet]
        [ActionName("members")]
        public IEnumerable<ContactModel> GetMembers(string identifier)
        {
            return _service.GetMembers(identifier).Select(Mapper.Map<IContact, ContactModel>);
        }
        
        // POST api/contactGroups
        [HttpPost]
        [ActionName("VerbDefault")]
        public void Post([FromBody]ContactGroupModel contactGroupModel)
        {
            var contactGroup = Mapper.Map<ContactGroupModel, ContactGroup>(contactGroupModel);
            if (string.IsNullOrEmpty(contactGroup.Identifier))
            {
                contactGroup.Identifier = Guid.NewGuid().ToString();
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
        public void Delete(string identifier)
        {
            _service.DeleteByIdentifier(identifier);
        }
    }
}