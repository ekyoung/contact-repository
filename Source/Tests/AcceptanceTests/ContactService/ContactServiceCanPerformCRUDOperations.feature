Feature: Contact Service Can Perform CRUD Operations
	In order to remember my contacts
	As a user
	I want to be able to save and retrieve a contact

Scenario: Insert and Select
	Given I save a new contact
	When I retrieve the contact
	Then the name of the retrieved contact is equal to the name of the contact I saved

Scenario: Insert Update and Select
	Given I save a new contact
	And I change the name of the saved contact
	And I save the contact again
	When I retrieve the contact
	Then the name of the retrieved contact is equal to the name of the contact I saved

Scenario: Insert and Delete
	Given I save a new contact
	And I delete the contact
	When I retrieve the contact
	Then the retrieved contact is null