using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class ContactModel
    {
        public ContactModel()
        {
            EmailAddresses = new List<ContactEmailAddressModel>();
        }

        [DataMember]
        public Guid Identifier { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public List<ContactEmailAddressModel> EmailAddresses { get; set; }
    }
}