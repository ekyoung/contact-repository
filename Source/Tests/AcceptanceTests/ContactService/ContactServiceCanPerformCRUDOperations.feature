﻿Feature: Contact Service Can Perform CRUD Operations
	In order to remember my contacts
	As a user
	I want to be able to save and retrieve a contact

Scenario: Insert and Select
	Given I create a contact
	And I save the contact
	When I retrieve the contact
	Then the name of the retrieved contact is equal to the name of the contact

Scenario: Insert Update and Select
	Given I create a contact
	And I save the contact
	And I change the name of the contact
	And I save the contact
	When I retrieve the contact
	Then the name of the retrieved contact is equal to the name of the contact

Scenario: Insert Select and Add Contact Info
	Given I create a contact
	And I save the contact
	And I set email address user@home.com with nickname Home on the contact
	And I set phone number (111) 111-1111 with nickname Home on the contact
	And I save the contact
	When I retrieve the contact
	Then the retrieved contact has email address user@home.com with nickname Home
	And the retrieved contact has phone number (111) 111-1111 with nickname Home

Scenario: Insert and Delete
	Given I create a contact
	And I set email address user@home.com on the contact
	And I set phone number (111) 111-1111 on the contact
	And I save the contact
	And I delete the contact
	When I retrieve the contact
	Then the retrieved contact is null

Scenario: Contact With Multiple Email Addresses
	Given I create a contact
	And I set email address user@home.com with nickname Home on the contact
	And I set email address user@work.com with nickname Work on the contact
	And I save the contact
	When I retrieve the contact
	Then the retrieved contact has email address user@home.com with nickname Home
	And the retrieved contact has email address user@work.com with nickname Work
	And the primary email address of the retrieved contact is user@home.com

Scenario: Clear Stored Email Addresses
	Given I create a contact
	And I set email address user@home.com on the contact
	And I set email address user@work.com on the contact
	And I save the contact
	And I clear the email addresses of the contact
	And I save the contact
	When I retrieve the contact
	Then the retrieved contact has 0 email addresses

Scenario: Contact With Multiple Phone Numbers
	Given I create a contact
	And I set phone number (111) 111-1111 with nickname Home on the contact
	And I set phone number (222) 222-2222 with nickname Work on the contact
	And I save the contact
	When I retrieve the contact
	Then the retrieved contact has phone number (111) 111-1111 with nickname Home
	And the retrieved contact has phone number (222) 222-2222 with nickname Work
	And the primary phone number of the retrieved contact is (111) 111-1111

Scenario: Clear Stored Phone Numbers
	Given I create a contact
	And I set phone number (111) 111-1111 on the contact
	And I set phone number (222) 222-2222 on the contact
	And I save the contact
	And I clear the phone numbers of the contact
	And I save the contact
	When I retrieve the contact
	Then the retrieved contact has 0 phone numbers
