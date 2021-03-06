﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EthanYoung.ContactRepository.Web.Models
{
    [DataContract]
    public class ContactGroupModel
    {
        public ContactGroupModel()
        {
            Members = new List<ContactGroupMemberModel>();
        }

        [DataMember]
        public string Identifier { get; set; } 
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<ContactGroupMemberModel> Members { get; set; }
    }
}