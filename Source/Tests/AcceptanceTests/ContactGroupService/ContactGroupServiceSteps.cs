﻿using System;
using System.Collections.Generic;
using System.Linq;
using EthanYoung.ContactRepository.ContactGroups;
using EthanYoung.ContactRepository.Contacts;
using EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactService;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.ContactGroupService
{
    [Binding]
    public class ContactGroupServiceSteps
    {
        private readonly IContactGroupService _service = DependencyRegistry.Resolve<IContactGroupService>();

        private readonly ContactContext _contactContext;

        private IContactGroup _contactGroup;
        private IContactGroup _retrievedContactGroup;
        private List<IContact> _retrievedContactGroupMembers;

        public ContactGroupServiceSteps(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }

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
                Identifier = Guid.NewGuid().ToString(),
                Name = "My Contacts"
            };
        }

        [Given(@"I change the name of the contact group")]
        public void GivenIChangeTheNameOfTheContactGroup()
        {
            _contactGroup.Name += " Updated";
        }

        [Given(@"I add the contact to the contact group")]
        public void GivenIAddTheContactToTheContactGroup()
        {
            _contactGroup.AddMember(_contactContext.Contact.Identifier);
        }

        [Given(@"I add the contact to the contact group with relationships")]
        public void GivenIAddTheContactToTheContactGroupWithRelationships(Table relationships)
        {
            _contactGroup.AddMember(_contactContext.Contact.Identifier, relationships.Rows.Select(x => x["Relationship"]));
        }

        [Given(@"I add relationship ""(.*)"" to the contact within the contact group")]
        public void GivenIAddRelationshipToTheContactWithinTheContactGroup(string relationship)
        {
            _contactGroup.GetMember(_contactContext.Contact.Identifier).AddRelationship(relationship);
        }

        [Given(@"I remove relationship ""(.*)"" from the contact within the contact group")]
        public void GivenIRemoveRelationshipFromTheContactWithinTheContactGroup(string relationship)
        {
            _contactGroup.GetMember(_contactContext.Contact.Identifier).RemoveRelationship(relationship);
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

        [When(@"I retrieve the members of the contact group")]
        public void WhenIRetrieveTheMembersOfTheContactGroup()
        {
            _retrievedContactGroupMembers = _service.GetMembers(_contactGroup.Identifier);
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

        [Then(@"the contact is a member of the retrieved contact group")]
        public void ThenTheContactIsAMemberOfTheRetrievedContactGroup()
        {
            Assert.IsTrue(_retrievedContactGroup.IsMember(_contactContext.Contact.Identifier));
        }

        [Then(@"the list of retrieved members contains the contact")]
        public void ThenTheListOfRetrievedMembersContainsTheContact()
        {
            Assert.IsTrue(_retrievedContactGroupMembers.Any(x => x.Identifier == _contactContext.Contact.Identifier));
        }

        [Then(@"the contact has the following relationships within the retrieved contact group")]
        public void ThenTheContactHasTheFollowingRelationshipsWithinTheRetrievedContactGroup(Table relationships)
        {
            var member = _retrievedContactGroup.GetMember(_contactContext.Contact.Identifier);
            
            Assert.AreEqual(relationships.Rows.Count, member.Relationships.Count);
            foreach (var relationship in relationships.Rows.Select(x => x["Relationship"]))
            {
                Assert.IsTrue(member.Relationships.Contains(relationship));
            }
        }
    }
}