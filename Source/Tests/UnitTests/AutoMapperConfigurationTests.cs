using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EthanYoung.ContactRepository.ContactGroups;
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

        [Test]
        public void GivenAContact_Map_CreatesAContactModelWithTheRightPhoneNumbers()
        {
            var primaryPhoneNumber = new PhoneNumber("1111111111");
            const string primaryNickName = "Home";
            var otherPhoneNumber = new PhoneNumber("2222222222");
            const string otherNickName = "Other";

            var contact = new Contact();
            contact.SetPhoneNumber(otherPhoneNumber, otherNickName);
            contact.SetPhoneNumber(primaryPhoneNumber, primaryNickName);
            contact.PrimaryPhoneNumber = primaryPhoneNumber;

            AutoMapperConfiguration.Configure();
            var contactModel = Mapper.Map<Contact, ContactModel>(contact);

            Assert.AreEqual(otherPhoneNumber.ToString(), contactModel.PhoneNumbers[0].PhoneNumber);
            Assert.AreEqual(otherNickName, contactModel.PhoneNumbers[0].NickName);
            Assert.IsFalse(contactModel.PhoneNumbers[0].IsPrimary);

            Assert.AreEqual(primaryPhoneNumber.ToString(), contactModel.PhoneNumbers[1].PhoneNumber);
            Assert.AreEqual(primaryNickName, contactModel.PhoneNumbers[1].NickName);
            Assert.IsTrue(contactModel.PhoneNumbers[1].IsPrimary);
        }

        [Test]
        public void GivenAContactModel_Map_CreatesAContactWithTheRightPhoneNumbers()
        {
            var primaryPhoneNumber = new PhoneNumber("1111111111");
            const string primaryNickName = "Home";
            var otherPhoneNumber = new PhoneNumber("2222222222");
            const string otherNickName = "Other";

            var contactModel = new ContactModel
            {
                FirstName = "Joe",
                LastName = "Contact",
                PhoneNumbers = new List<ContactPhoneNumberModel>
                {
                    new ContactPhoneNumberModel {PhoneNumber = otherPhoneNumber.Value, NickName = otherNickName, IsPrimary = false},
                    new ContactPhoneNumberModel {PhoneNumber = primaryPhoneNumber.Value, NickName = primaryNickName, IsPrimary = true},
                }
            };

            AutoMapperConfiguration.Configure();
            var contact = Mapper.Map<ContactModel, Contact>(contactModel);

            Assert.IsTrue(contact.PhoneNumbers.Any(x => x.PhoneNumber == otherPhoneNumber && x.Nickname == otherNickName && !x.IsPrimary));
            Assert.IsTrue(contact.PhoneNumbers.Any(x => x.PhoneNumber == primaryPhoneNumber && x.Nickname == primaryNickName && x.IsPrimary));
        }

        [Test]
        public void GivenAContactModelWithNoPhoneNumbersAndContactWithPhoneNumbers_Map_RemovesThePhoneNumbers()
        {
            var contactModel = new ContactModel
            {
                FirstName = "Joe",
                LastName = "Contact"
            };

            var contact = new Contact();
            contact.SetPhoneNumber(new PhoneNumber("1111111111"), null);

            AutoMapperConfiguration.Configure();
            Mapper.Map(contactModel, contact);

            Assert.AreEqual(0, contact.PhoneNumbers.Count);
        }

        [Test]
        public void GivenAContactGroup_Map_CreatesAContactGroupModelWithTheRightMembers()
        {
            var contactGroup = new ContactGroup
            {
                Identifier = Guid.NewGuid(),
                Name = "My Contacts"
            };
            contactGroup.AddMember(Guid.NewGuid());
            contactGroup.AddMember(Guid.NewGuid());

            AutoMapperConfiguration.Configure();

            var contactGroupModel = Mapper.Map<IContactGroup, ContactGroupModel>(contactGroup);
            
            Assert.AreEqual(2, contactGroupModel.Members.Count);
            Assert.IsTrue(contactGroupModel.Members.Any(x => x.ContactIdentifier == contactGroup.Members[0].ContactIdentifier));
            Assert.IsTrue(contactGroupModel.Members.Any(x => x.ContactIdentifier == contactGroup.Members[1].ContactIdentifier));
        }

        [Test]
        public void GivenAContactGroupModel_Map_CreatesAContactGroupWithTheRightMembers()
        {
            var contactGroupModel = new ContactGroupModel
            {
                Name = "My Contacts",
                Members = new List<ContactGroupMemberModel>
                {
                    new ContactGroupMemberModel {ContactIdentifier = Guid.NewGuid()},
                    new ContactGroupMemberModel {ContactIdentifier = Guid.NewGuid()}
                }
            };

            AutoMapperConfiguration.Configure();

            var contactGroup = Mapper.Map<ContactGroupModel, ContactGroup>(contactGroupModel);

            Assert.AreEqual(2, contactGroup.Members.Count);
            Assert.IsTrue(contactGroup.IsMember(contactGroupModel.Members[0].ContactIdentifier));
            Assert.IsTrue(contactGroup.IsMember(contactGroupModel.Members[1].ContactIdentifier));
        }

        [Test]
        public void GivenAContactGroupModelWithNoMembersAndAContactGroupWithMembers_Map_RemovesTheMembers()
        {
            var contactGroupModel = new ContactGroupModel
            {
                Name = "My Contacts",
            };

            var contactGroup = new ContactGroup();
            contactGroup.AddMember(Guid.NewGuid());

            AutoMapperConfiguration.Configure();

            Mapper.Map<ContactGroupModel, ContactGroup>(contactGroupModel, contactGroup);

            Assert.AreEqual(0, contactGroup.Members.Count);
        }
    }
}