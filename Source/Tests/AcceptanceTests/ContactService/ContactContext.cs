using EthanYoung.ContactRepository.Contacts;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactService
{
    public class ContactContext
    {
        public IContact Contact { get; set; }
        public IContact RetrievedContact { get; set; }
    }
}