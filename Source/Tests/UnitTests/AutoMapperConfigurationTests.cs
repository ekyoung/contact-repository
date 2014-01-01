using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EthanYoung.ContactRepository.Contacts;
using NUnit.Framework;
using Web.Models;

namespace EthanYoung.ContactRepository.Tests.UnitTests
{
    [TestFixture]
    public class AutoMapperConfigurationTests
    {
        [Test]
        public void InAllCases_Configure_CreatesValidConfiguration()
        {
            AutoMapperConfiguration.Configure();

            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void GivenAContact_Map_CreatesAContactModelWithTheRightNames()
        {
            const string firstName = "Joe";
            const string lastName = "Last";

            var contact = new Contact
            {
                Name = new Name(firstName, lastName)
            };

            AutoMapperConfiguration.Configure();
            var contactModel = Mapper.Map<Contact, ContactModel>(contact);

            Assert.AreEqual(firstName, contactModel.FirstName);
            Assert.AreEqual(lastName, contactModel.LastName);
        }

        [Test]
        public void GivenAContactModel_Map_CreatesAContactWithTheRightName()
        {
            const string firstName = "Joe";
            const string lastName = "Last";

            var contactModel = new ContactModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            AutoMapperConfiguration.Configure();
            var contact = Mapper.Map<ContactModel, Contact>(contactModel);

            Assert.AreEqual(new Name(firstName, lastName), contact.Name);
        }

        [Test]
        public void GivenAContact_Map_CreatesAContactModelWithTheRightEmailAddresses()
        {
            var primaryEmailAddress = new EmailAddress("primary@email.com");
            const string primaryNickName = "Home";
            var otherEmailAddress = new EmailAddress("otheraddress@email.com");
            const string otherNickName = "Other";

            var contact = new Contact();
            contact.SetEmailAddress(otherEmailAddress, otherNickName);
            contact.SetEmailAddress(primaryEmailAddress, primaryNickName);
            contact.PrimaryEmailAddress = primaryEmailAddress;

            AutoMapperConfiguration.Configure();
            var contactModel = Mapper.Map<Contact, ContactModel>(contact);

            Assert.AreEqual(otherEmailAddress.Value, contactModel.EmailAddresses[0].EmailAddress);
            Assert.AreEqual(otherNickName, contactModel.EmailAddresses[0].NickName);
            Assert.IsFalse(contactModel.EmailAddresses[0].IsPrimary);

            Assert.AreEqual(primaryEmailAddress.Value, contactModel.EmailAddresses[1].EmailAddress);
            Assert.AreEqual(primaryNickName, contactModel.EmailAddresses[1].NickName);
            Assert.IsTrue(contactModel.EmailAddresses[1].IsPrimary);
        }

        [Test]
        public void GivenAContactModel_Map_CreatesAContactWithTheRightEmailAddresses()
        {
            var primaryEmailAddress = new EmailAddress("primary@email.com");
            const string primaryNickName = "Home";
            var otherEmailAddress = new EmailAddress("otheraddress@email.com");
            const string otherNickName = "Other";

            var contactModel = new ContactModel
            {
                FirstName = "Joe",
                LastName = "Contact",
                EmailAddresses = new List<ContactEmailAddressModel>
                {
                    new ContactEmailAddressModel {EmailAddress = otherEmailAddress.Value, NickName = otherNickName, IsPrimary = false},
                    new ContactEmailAddressModel {EmailAddress = primaryEmailAddress.Value, NickName = primaryNickName, IsPrimary = true},
                }
            };

            AutoMapperConfiguration.Configure();
            var contact = Mapper.Map<ContactModel, Contact>(contactModel);

            Assert.IsTrue(contact.EmailAddresses.Any(x => x.EmailAddress == otherEmailAddress && x.Nickname == otherNickName && !x.IsPrimary));
            Assert.IsTrue(contact.EmailAddresses.Any(x => x.EmailAddress == primaryEmailAddress && x.Nickname == primaryNickName && x.IsPrimary));
        }

        [Test]
        public void GivenAContactModelWithNoEmailAddressesAndContactWithEmailAddresses_Map_RemovesTheEmailAddresses()
        {
            var contactModel = new ContactModel
            {
                FirstName = "Joe",
                LastName = "Contact"
            };

            var contact = new Contact();
            contact.SetEmailAddress(new EmailAddress("contact@email.com"), null);

            AutoMapperConfiguration.Configure();
            Mapper.Map(contactModel, contact);

            Assert.AreEqual(0, contact.EmailAddresses.Count);
        }
    }
}