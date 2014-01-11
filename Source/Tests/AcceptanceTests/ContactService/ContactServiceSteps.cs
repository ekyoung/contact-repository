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

        private IContact _contact;
        private IContact _retrievedContact;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _contact = null;
            _retrievedContact = null;
        }

        [Given(@"I create a contact")]
        public void GivenICreateAContact()
        {
            _contact = new Contact
            {
                Identifier = Guid.NewGuid(),
                Name = new Name("Joe", "Contact")
            };
        }

        [Given(@"I change the name of the contact")]
        public void GivenIChangeTheNameOfTheContact()
        {
            _contact.Name = new Name("Joe", "Updated");
        }

        [Given(@"I set email address (.*\.com) on the contact")]
        public void GivenISetEmailAddressOnTheContact(EmailAddress emailAddress)
        {
            _contact.SetEmailAddress(emailAddress, null);
        }

        [Given(@"I set email address (.*\.com) with nickname (.*) on the contact")]
        public void GivenISetEmailAddressWithNicknameOnTheContact(EmailAddress emailAddress, string nickname)
        {
            _contact.SetEmailAddress(emailAddress, nickname);
        }

        [Given(@"I set email address (.*\.com) as the primary email address of the contact")]
        public void GivenISetEmailAddressAsThePrimaryEmailAddressOfTheContact(EmailAddress emailAddress)
        {
            _contact.PrimaryEmailAddress = emailAddress;
        }

        [Given(@"I clear the email addresses of the contact")]
        public void GivenIClearTheEmailAddressesOfTheContact()
        {
            _contact.ClearEmailAddresses();
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) on the contact")]
        public void GivenISetPhoneNumberOnTheContact(PhoneNumber phoneNumber)
        {
            _contact.SetPhoneNumber(phoneNumber, null);
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*) on the contact")]
        public void GivenISetPhoneNumberWithNicknameOnTheContact(PhoneNumber phoneNumber, string nickname)
        {
            _contact.SetPhoneNumber(phoneNumber, nickname);
        }

        [Given(@"I set phone number (\(\d{3}\) \d{3}\-\d{4}) as the primary phone number of the contact")]
        public void GivenISetPhoneNumberAsThePrimaryPhoneNumberOfTheContact(PhoneNumber phoneNumber)
        {
            _contact.PrimaryPhoneNumber = phoneNumber;
        }

        [Given(@"I clear the phone numbers of the contact")]
        public void GivenIClearThePhoneNumbersOfTheContact()
        {
            _contact.ClearPhoneNumbers();
        }

        [Given(@"I save the contact")]
        public void GivenISaveTheContact()
        {
            _service.Save(_contact);
        }

        [Given(@"I delete the contact")]
        public void GivenIDeleteTheContact()
        {
            _service.DeleteByIdentifier(_contact.Identifier);
        }

        [When(@"I retrieve the contact")]
        public void WhenIRetrieveTheContact()
        {
            _retrievedContact = _service.FindByIdentifier(_contact.Identifier);
        }

        [Then(@"the retrieved contact is null")]
        public void ThenTheRetrievedContactIsNull()
        {
            Assert.IsNull(_retrievedContact);
        }

        [Then(@"the name of the retrieved contact is equal to the name of the contact")]
        public void ThenTheNameOfTheRetrievedContactIsEqualToTheNameOfTheContact()
        {
            Assert.AreEqual(_contact.Name, _retrievedContact.Name);
        }

        [Then(@"the contact has (.*) email address")]
        [Then(@"the contact has (.*) email addresses")]
        public void ThenTheContactHasCountEmailAddress(int count)
        {
            Assert.AreEqual(count, _contact.EmailAddresses.Count);
        }

        [Then(@"the retrieved contact has (.*) email address")]
        [Then(@"the retrieved contact has (.*) email addresses")]
        public void ThenTheRetrievedContactHasCountEmailAddress(int count)
        {
            Assert.AreEqual(count, _retrievedContact.EmailAddresses.Count);
        }

        [Then(@"the contact has email address (.*\.com) with nickname (.*)")]
        public void ThenTheContactHasEmailAddressWithNickname(EmailAddress emailAddress, string nickname)
        {
            AssertContactHasEmailAddressWithNickname(_contact, emailAddress, nickname);
        }

        [Then(@"the contact has email address (.*\.com) with null nickname")]
        public void ThenTheContactHasEmailAddressWithNullNickname(EmailAddress emailAddress)
        {
            AssertContactHasEmailAddressWithNickname(_contact, emailAddress, null);
        }

        [Then(@"the retrieved contact has email address (.*\.com) with nickname (.*)")]
        public void ThenTheRetrievedContactHasEmailAddressWithNickname(EmailAddress emailAddress, string nickname)
        {
            AssertContactHasEmailAddressWithNickname(_retrievedContact, emailAddress, nickname);
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
            Assert.AreEqual(emailAddress, _contact.PrimaryEmailAddress);
        }

        [Then(@"the primary email address of the retrieved contact is (.*\.com)")]
        public void ThenThePrimaryEmailAddressOfTheRetrievedContactIs(EmailAddress emailAddress)
        {
            Assert.AreEqual(emailAddress, _retrievedContact.PrimaryEmailAddress);
        }


        [Then(@"the contact has (.*) phone number")]
        [Then(@"the contact has (.*) phone numbers")]
        public void ThenTheContactHasCountPhoneNumbers(int count)
        {
            Assert.AreEqual(count, _contact.PhoneNumbers.Count);
        }

        [Then(@"the retrieved contact has (.*) phone number")]
        [Then(@"the retrieved contact has (.*) phone numbers")]
        public void ThenTheRetrievedContactHasCountPhoneNumber(int count)
        {
            Assert.AreEqual(count, _retrievedContact.PhoneNumbers.Count);
        }

        [Then(@"the contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*)")]
        public void ThenTheContactHasPhoneNumberWithNickname(PhoneNumber phoneNumber, string nickname)
        {
            AssertContactHasPhoneNumberWithNickname(_contact, phoneNumber, nickname);
        }

        [Then(@"the contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with null nickname")]
        public void ThenTheContactHasPhoneNumberWithNullNickname(PhoneNumber phoneNumber)
        {
            AssertContactHasPhoneNumberWithNickname(_contact, phoneNumber, null);
        }

        [Then(@"the retrieved contact has phone number (\(\d{3}\) \d{3}\-\d{4}) with nickname (.*)")]
        public void ThenTheRetrievedContactHasPhoneNumberWithNickname(PhoneNumber phoneNumber, string nickname)
        {
            AssertContactHasPhoneNumberWithNickname(_retrievedContact, phoneNumber, nickname);
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
            Assert.AreEqual(phoneNumber, _contact.PrimaryPhoneNumber);
        }

        [Then(@"the primary phone number of the retrieved contact is (\(\d{3}\) \d{3}\-\d{4})")]
        public void ThenThePrimaryPhoneNumberOfTheRetrievedContactIs(PhoneNumber phoneNumber)
        {
            Assert.AreEqual(phoneNumber, _retrievedContact.PrimaryPhoneNumber);
        }
    }
}
