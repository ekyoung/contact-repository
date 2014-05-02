Feature: Contact Service Can Manage Multiple Phone Numbers For A Contact
	In order to keep track of all the phone numbers one of my contacts uses
	As a user
	I want to be able to store and retrieve multiple phone numbers for a contact

Scenario: Set a Nickname For An Existing Phone Number
	Given I create a contact
	And I set phone number (111) 111-1111 on the contact
	And I set phone number (111) 111-1111 with nickname Home on the contact
	Then the contact has 1 phone number
	And the contact has phone number (111) 111-1111 with nickname Home

Scenario: First Phone Number Added Is Primary
	Given I create a contact
	And I set phone number (111) 111-1111 on the contact
	Then the primary phone number of the contact is (111) 111-1111

Scenario: Set Primary Phone Number to a New Phone Number
	Given I create a contact
	And I set phone number (111) 111-1111 with nickname Home on the contact
	And I set phone number (222) 222-2222 with nickname Work on the contact
	And I set phone number (333) 333-3333 as the primary phone number of the contact
	Then the contact has 3 phone numbers
	And the contact has phone number (333) 333-3333 with null nickname
	And the primary phone number of the contact is (333) 333-3333

Scenario: Set Primary Phone Number to an Existing Phone Number
	Given I create a contact
	And I set phone number (111) 111-1111 with nickname Home on the contact
	And I set phone number (222) 222-2222 with nickname Work on the contact
	And I set phone number (222) 222-2222 as the primary phone number of the contact
	Then the contact has 2 phone numbers
	And the primary phone number of the contact is (222) 222-2222

Scenario: Clear Phone Numbers
	Given I create a contact
	And I set phone number (111) 111-1111 on the contact
	And I set phone number (222) 222-2222 on the contact
	And I clear the phone numbers of the contact
	Then the contact has 0 phone numbers
