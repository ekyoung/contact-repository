using System.Runtime.Serialization;

namespace EthanYoung.ContactRepository.Web.Models
{
    public class ContactPhoneNumberModel
    {
        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }
    }
}