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
    }
}