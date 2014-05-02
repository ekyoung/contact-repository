Feature: Bootstrapping Service Can Create The Schema In A Blank Database
	In order to install this service as easily as possible
	As a systems administrator
	I want it to create the schema in a database that I've already created when it starts up

@Ignore
@blankDb
Scenario: Create the Schema in a Blank Database
	Given database server localhost\SQLEXPRESS and database ContactRepository_Blank
	And I ensure the database is setup
	When I retrieve the database version
	Then the retrieved database version should be the latest version
