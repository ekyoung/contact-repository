using System.Runtime.Serialization;

namespace EthanYoung.ContactRepository.Web.Models
{
    [DataContract]
    public class RelationshipModel
    {
        [DataMember]
        public string Name { get; set; } 
    }
}