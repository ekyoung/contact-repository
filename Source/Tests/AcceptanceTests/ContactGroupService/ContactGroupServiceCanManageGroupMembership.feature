Feature: Contact Group Service Can Manage Group Membership
	In order to organize my contacts
	As a user
	I want to be able to manage which contacts are members of a group

Scenario: Add One Member To a Group
	Given I create a contact
	And I save the contact
	And I create a contact group
	And I add the contact to the contact group
	And I save the contact group
	When I retrieve the contact group
	Then the contact is a member of the retrieved contact group
