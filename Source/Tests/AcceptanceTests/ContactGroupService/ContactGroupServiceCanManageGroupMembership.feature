Feature: Contact Group Service Can Manage Group Membership
	In order to organize my contacts
	As a user
	I want to be able to manage which contacts are members of a group

Background: 
	Given I create a contact
	And I save the contact
	And I create a contact group

Scenario: Add One Member To a Group
	And I add the contact to the contact group
	And I save the contact group
	When I retrieve the contact group
	Then the contact is a member of the retrieved contact group

Scenario: Add One Member To a Group With Relationships
	And I add the contact to the contact group with relationships
	  | Relationship |
	  | Friend       |
	  | Coworker     |
	And I save the contact group
	When I retrieve the contact group
	Then the contact is a member of the retrieved contact group
	And the contact has the following relationships within the retrieved contact group
	  | Relationship |
	  | Friend       |
	  | Coworker     |

Scenario: Retrieve Members Of a Group
	And I add the contact to the contact group
	And I save the contact group
	When I retrieve the members of the contact group
	Then the list of retrieved members contains the contact