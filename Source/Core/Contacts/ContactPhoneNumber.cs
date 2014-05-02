namespace EthanYoung.ContactRepository.Contacts
{
    public class ContactPhoneNumber
    {
        public long? ContactId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string Nickname { get; set; }
        public bool IsPrimary { get; set; } 
    }
}