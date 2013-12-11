using System;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class ContactModel
    {
        [DataMember]
        public Guid Identifier { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}