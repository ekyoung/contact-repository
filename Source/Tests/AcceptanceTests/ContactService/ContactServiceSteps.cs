using System;
using EthanYoung.ContactRepository.Contacts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactService
{
    [Binding]
    public class ContactServiceSteps
    {
        private readonly IContactService _service = DependencyRegistry.Resolve<IContactService>();

        private IContact _savedContact;
        private IContact _retrievedContact;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _savedContact = null;
            _retrievedContact = null;
        }

        [Given(@"I save a new contact")]
        public void GivenISaveANewContact()
        {
            _savedContact = new Contact
            {
                Identifier = Guid.NewGuid(),
                Name = new Name("Joe", "Contact")
            };

            _service.Save(_savedContact);
        }

        [Given(@"I change the name of the saved contact")]
        public void GivenIChangeTheNameOfTheSavedContact()
        {
            _savedContact.Name = new Name("Joe", "Updated");
        }

        [Given(@"I save the contact again")]
        public void GivenISaveTheContactAgain()
        {
            _service.Save(_savedContact);
        }

        [Given(@"I delete the contact")]
        public void GivenIDeleteTheContact()
        {
            _service.DeleteByIdentifier(_savedContact.Identifier);
        }

        [When(@"I retrieve the contact")]
        public void WhenIRetrieveTheContact()
        {
            _retrievedContact = _service.FindByIdentifier(_savedContact.Identifier);
        }
        
        [Then(@"the name of the retrieved contact is equal to the name of the contact I saved")]
        public void ThenTheNameOfTheRetrievedContactIsEqualToTheNameOfTheContactISaved()
        {
            Assert.AreEqual(_savedContact.Name, _retrievedContact.Name);
        }

        [Then(@"the retrieved contact is null")]
        public void ThenTheRetrievedContactIsNull()
        {
            Assert.IsNull(_retrievedContact);
        }
    }
}
