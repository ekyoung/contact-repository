using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EthanYoung.ContactRepository.Web.Models
{
    [DataContract]
    public class ContactGroupMemberModel
    {
        [DataMember]
        public string ContactIdentifier { get; set; }

        [DataMember]
        public List<RelationshipModel> Relationships { get; set; }
    }
}