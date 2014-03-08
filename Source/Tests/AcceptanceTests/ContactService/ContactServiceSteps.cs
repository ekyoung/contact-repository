using System;
using System.Linq;
using EthanYoung.ContactRepository.Contacts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactService
{
    [Binding]
    public class ContactServiceSteps
    {
        private readonly IContactService _service = DependencyRegistry.Resolve<IContactService>();

        private readonly ContactContext _contactContext;

        public ContactServiceSteps(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _contactContext.Contact = null;
            _contactContext.RetrievedContact = null;
        }

        [Given(@"I create a contact")]
        public void GivenICreateAContact()
        {
            _contactContext.Contact = new Contact
            {
                Identifier = Guid.NewGuid().ToString(),
                Name = new Name("Joe", "Contact")
            };
        }

        [Given(@"I change the name of the contact")]
        public void GivenIChangeTheNameOfTheContact()
        {
            _contactContext.Contact.Name = new Name("Joe", "Updated");
        }

        [Given(@"I set email address (.*\.com) on the contact")]
        public void GivenISetEmailAddressOnTheContact(EmailAddress emailAddress)
        {
            _contactContext.Contact.SetEmailAddress(emailAddress, null);
        }

        [Given(@"I set email address (.*\.com) with nickname (.*) on the contact")]
        public void GivenISetEmailAddressWithNicknameOnTheContact(EmailAddress emailAddress, string nickname)
        {
            _contactContext.Contact.SetEmailAddress(emailAddress, nickname);
        }

        [Given(@"I set email address (.*\.com) as the primary email address of the contact")]
        public void GivenISetEmailAddressAsThePrimaryEmailAddressOfTheContact(EmailAddress emailAddress)
        {
            _contactContext.Contact.PrimaryEmailAddress = emailAddress;
        }

        [Given(@"I clear the email addresses of the contact")]
        public void GivenIClearTheEmailAddressesOfTheContact()
        {
            _contactContext.Contact.ClearEmailAddresses();
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) on the contact")]
        public void GivenISetPhoneNumberOnTheContact(PhoneNumber phoneNumber)
        {
            _contactContext.Contact.SetPhoneNumber(phoneNumber, null);
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*) on the contact")]
        public void GivenISetPhoneNumberWithNicknameOnTheContact(PhoneNumber phoneNumber, string nickname)
        {
            _contactContext.Contact.SetPhoneNumber(phoneNumber, nickname);
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) as the primary phone number of the contact")]
        public void GivenISetPhoneNumberAsThePrimaryPhoneNumberOfTheContact(PhoneNumber phoneNumber)
        {
            _contactContext.Contact.PrimaryPhoneNumber = phoneNumber;
        }

        [Given(@"I clear the phone numbers of the contact")]
        public void GivenIClearThePhoneNumbersOfTheContact()
        {
            _contactContext.Contact.ClearPhoneNumbers();
        }

        [Given(@"I save the contact")]
        public void GivenISaveTheContact()
        {
            _service.Save(_contactContext.Contact);
        }

        [Given(@"I delete the contact")]
        public void GivenIDeleteTheContact()
        {
            _service.DeleteByIdentifier(_contactContext.Contact.Identifier);
        }

        [When(@"I retrieve the contact")]
        public void WhenIRetrieveTheContact()
        {
            _contactContext.RetrievedContact = _service.FindByIdentifier(_contactContext.Contact.Identifier);
        }

        [Then(@"the retrieved contact is null")]
        public void ThenTheRetrievedContactIsNull()
        {
            Assert.IsNull(_contactContext.RetrievedContact);
        }

        [Then(@"the name of the retrieved contact is equal to the name of the contact")]
        public void ThenTheNameOfTheRetrievedContactIsEqualToTheNameOfTheContact()
        {
            Assert.AreEqual(_contactContext.Contact.Name, _contactContext.RetrievedContact.Name);
        }

        [Then(@"the contact has (.*) email address")]
        [Then(@"the contact has (.*) email addresses")]
        public void ThenTheContactHasCountEmailAddress(int count)
        {
            Assert.AreEqual(count, _contactContext.Contact.EmailAddresses.Count);
        }

        [Then(@"the retrieved contact has (.*) email address")]
        [Then(@"the retrieved contact has (.*) email addresses")]
        public void ThenTheRetrievedContactHasCountEmailAddress(int count)
        {
            Assert.AreEqual(count, _contactContext.RetrievedContact.EmailAddresses.Count);
        }

        [Then(@"the contact has email address (.*\.com) with nickname (.*)")]
        public void ThenTheContactHasEmailAddressWithNickname(EmailAddress emailAddress, string nickname)
        {
            AssertContactHasEmailAddressWithNickname(_contactContext.Contact, emailAddress, nickname);
        }

        [Then(@"the contact has email address (.*\.com) with null nickname")]
        public void ThenTheContactHasEmailAddressWithNullNickname(EmailAddress emailAddress)
        {
            AssertContactHasEmailAddressWithNickname(_contactContext.Contact, emailAddress, null);
        }

        [Then(@"the retrieved contact has email address (.*\.com) with nickname (.*)")]
        public void ThenTheRetrievedContactHasEmailAddressWithNickname(EmailAddress emailAddress, string nickname)
        {
            AssertContactHasEmailAddressWithNickname(_contactContext.RetrievedContact, emailAddress, nickname);
        }

        private void AssertContactHasEmailAddressWithNickname(IContact contact, EmailAddress emailAddress, string nickname)
        {
            var contactEmailAddress = contact.EmailAddresses.FirstOrDefault(x => x.EmailAddress == emailAddress);
            Assert.IsNotNull(contactEmailAddress);
            Assert.AreEqual(nickname, contactEmailAddress.Nickname);
        }

        [Then(@"the primary email address of the contact is (.*\.com)")]
        public void ThenThePrimaryEmailAddressOfTheContactIs(EmailAddress emailAddress)
        {
            Assert.AreEqual(emailAddress, _contactContext.Contact.PrimaryEmailAddress);
        }

        [Then(@"the primary email address of the retrieved contact is (.*\.com)")]
        public void ThenThePrimaryEmailAddressOfTheRetrievedContactIs(EmailAddress emailAddress)
        {
            Assert.AreEqual(emailAddress, _contactContext.RetrievedContact.PrimaryEmailAddress);
        }


        [Then(@"the contact has (.*) phone number")]
        [Then(@"the contact has (.*) phone numbers")]
        public void ThenTheContactHasCountPhoneNumbers(int count)
        {
            Assert.AreEqual(count, _contactContext.Contact.PhoneNumbers.Count);
        }

        [Then(@"the retrieved contact has (.*) phone number")]
        [Then(@"the retrieved contact has (.*) phone numbers")]
        public void ThenTheRetrievedContactHasCountPhoneNumber(int count)
        {
            Assert.AreEqual(count, _contactContext.RetrievedContact.PhoneNumbers.Count);
        }

        [Then(@"the contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*)")]
        public void ThenTheContactHasPhoneNumberWithNickname(PhoneNumber phoneNumber, string nickname)
        {
            AssertContactHasPhoneNumberWithNickname(_contactContext.Contact, phoneNumber, nickname);
        }

        [Then(@"the contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with null nickname")]
        public void ThenTheContactHasPhoneNumberWithNullNickname(PhoneNumber phoneNumber)
        {
            AssertContactHasPhoneNumberWithNickname(_contactContext.Contact, phoneNumber, null);
        }

        [Then(@"the retrieved contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*)")]
        public void ThenTheRetrievedContactHasPhoneNumberWithNickname(PhoneNumber phoneNumber, string nickname)
        {
            AssertContactHasPhoneNumberWithNickname(_contactContext.RetrievedContact, phoneNumber, nickname);
        }

        private void AssertContactHasPhoneNumberWithNickname(IContact contact, PhoneNumber phoneNumber, string nickname)
        {
            var contactPhoneNumber = contact.PhoneNumbers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            Assert.IsNotNull(contactPhoneNumber);
            Assert.AreEqual(nickname, contactPhoneNumber.Nickname);
        }

        [Then(@"the primary phone number of the contact is (\(\d{3}\) \d{3}\-\d{4})")]
        public void ThenThePrimaryPhoneNumberOfTheContactIs(PhoneNumber phoneNumber)
        {
            Assert.AreEqual(phoneNumber, _contactContext.Contact.PrimaryPhoneNumber);
        }

        [Then(@"the primary phone number of the retrieved contact is (\(\d{3}\) \d{3}\-\d{4})")]
        public void ThenThePrimaryPhoneNumberOfTheRetrievedContactIs(PhoneNumber phoneNumber)
        {
            Assert.AreEqual(phoneNumber, _contactContext.RetrievedContact.PrimaryPhoneNumber);
        }
    }
}
