using System.Runtime.Serialization;

namespace EthanYoung.ContactRepository.Web.Models
{
    public class ContactEmailAddressModel
    {
        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }
    }
}