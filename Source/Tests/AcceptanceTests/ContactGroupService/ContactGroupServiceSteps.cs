using System;
using EthanYoung.ContactRepository.ContactGroups;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactGroupService
{
    [Binding]
    public class ContactGroupServiceSteps
    {
        private readonly IContactGroupService _service = DependencyRegistry.Resolve<IContactGroupService>();

        private IContactGroup _contactGroup;
        private IContactGroup _retrievedContactGroup;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _contactGroup = null;
            _retrievedContactGroup = null;
        }

        [Given(@"I create a contact group")]
        public void GivenICreateAContactGroup()
        {
            _contactGroup = new ContactGroup
            {
                Identifier = Guid.NewGuid(),
                Name = "My Contacts"
            };
        }

        [Given(@"I change the name of the contact group")]
        public void GivenIChangeTheNameOfTheContactGroup()
        {
            _contactGroup.Name += " Updated";
        }

        [Given(@"I save the contact group")]
        public void GivenISaveTheContactGroup()
        {
            _service.Save(_contactGroup);
        }

        [Given(@"I delete the contact group")]
        public void GivenIDeleteTheContactGroup()
        {
            _service.DeleteByIdentifier(_contactGroup.Identifier);
        }

        [When(@"I retrieve the contact group")]
        public void WhenIRetrieveTheContactGroup()
        {
            _retrievedContactGroup = _service.FindByIdentifier(_contactGroup.Identifier);
        }

        [Then(@"the retrieved contact group is null")]
        public void ThenTheRetrievedContactGroupIsNull()
        {
            Assert.IsNull(_retrievedContactGroup);
        }

        [Then(@"the name of the retrieved contact group is equal to the name of the contact group")]
        public void ThenTheNameOfTheRetrievedContactGroupIsEqualToTheNameOfTheContactGroup()
        {
            Assert.AreEqual(_contactGroup.Name, _retrievedContactGroup.Name);
        }

    }
}