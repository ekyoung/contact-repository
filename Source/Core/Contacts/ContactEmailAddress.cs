namespace EthanYoung.ContactRepository.Contacts
{
    public class ContactEmailAddress
    {
        public long? ContactId { get; set; }
        public EmailAddress EmailAddress { get; set; }
        public string Nickname { get; set; }
        public bool IsPrimary { get; set; }
    }
}