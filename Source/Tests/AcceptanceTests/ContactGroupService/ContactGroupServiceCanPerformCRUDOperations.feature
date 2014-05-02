Feature: Contact Group Service Can Perform CRUD Operations
	In order to organize my contacts
	As a user
	I want to be able to save and retrieve groups of contacts

Background: 
	Given I create a contact
	And I save the contact
	And I create a contact group
	And I save the contact group

Scenario: Insert and Select
	When I retrieve the contact group
	Then the name of the retrieved contact group is equal to the name of the contact group

Scenario: Insert Update and Select
	And I change the name of the contact group
	And I save the contact group
	When I retrieve the contact group
	Then the name of the retrieved contact group is equal to the name of the contact group

Scenario: Insert Select and Add Member
	And I add the contact to the contact group
	And I save the contact group
	When I retrieve the contact group
	Then the contact is a member of the retrieved contact group

Scenario: Insert and Delete
	And I delete the contact group
	When I retrieve the contact group
	Then the retrieved contact group is null
