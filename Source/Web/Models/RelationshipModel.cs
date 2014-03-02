using System.Runtime.Serialization;


namespace Web.Models
{
    [DataContract]
    public class RelationshipModel
    {
        [DataMember]
        public string Name { get; set; } 
    }
}