Feature: Contact Service Can Manage Multiple Email Addresses For A Contact
	In order to keep track of all the email addresses one of my contacts uses
	As a user
	I want to be able to store and retrieve multiple email addresses for a contact

Scenario: Set a Nickname For An Existing Email Address
	Given I create a contact
	And I set email address user@home.com on the contact
	And I set email address user@home.com with nickname Home on the contact
	Then the contact has 1 email address
	And the contact has email address user@home.com with nickname Home

Scenario: First Email Address Added Is Primary
	Given I create a contact
	And I set email address user@home.com on the contact
	Then the primary email address of the contact is user@home.com

Scenario: Set Primary Email Address to a New Email Address
	Given I create a contact
	And I set email address user@home.com with nickname Home on the contact
	And I set email address user@work.com with nickname Work on the contact
	And I set email address new@home.com as the primary email address of the contact
	Then the contact has 3 email addresses
	And the contact has email address new@home.com with null nickname
	And the primary email address of the contact is new@home.com

Scenario: Set Primary Email Address to an Existing Email Address
	Given I create a contact
	And I set email address user@home.com with nickname Home on the contact
	And I set email address user@work.com with nickname Work on the contact
	And I set email address user@work.com as the primary email address of the contact
	Then the contact has 2 email addresses
	And the primary email address of the contact is user@work.com

Scenario: Clear Email Addresses
	Given I create a contact
	And I set email address user@home.com on the contact
	And I set email address user@work.com on the contact
	And I clear the email addresses of the contact
	Then the contact has 0 email addresses
