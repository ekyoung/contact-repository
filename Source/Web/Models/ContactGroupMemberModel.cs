using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Web.Models
{
    [DataContract]
    public class ContactGroupMemberModel
    {
        [DataMember]
        public Guid ContactIdentifier { get; set; }

        [DataMember]
        public List<RelationshipModel> Relationships { get; set; }
    }
}