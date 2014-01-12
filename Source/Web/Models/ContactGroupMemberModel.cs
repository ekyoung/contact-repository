using System;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class ContactGroupMemberModel
    {
        [DataMember]
        public Guid ContactIdentifier { get; set; }
    }
}