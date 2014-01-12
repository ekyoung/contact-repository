using System;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class ContactGroupMemberModel
    {
        public Guid ContactIdentifier { get; set; }
    }
}