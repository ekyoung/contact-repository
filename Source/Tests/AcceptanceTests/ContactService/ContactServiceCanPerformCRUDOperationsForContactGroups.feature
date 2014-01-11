Feature: Contact Service Can Perform CRUD Operations For Contact Groups
	In order to organize my contacts
	As a user
	I want to be able to save and retrieve groups of contacts

Scenario: Insert and Select
	Given I create a contact group
	And I save the contact group
	When I retrieve the contact group
	Then the name of the retrieved contact group is equal to the name of the contact group
