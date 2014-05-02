using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(2014030081505)]
    public class Migration2014030081505_ChangeIdentifiersFromGuidsToStrings : Migration
    {
        public override void Up()
        {
            Delete.Index("IX_Contacts_ContactIdentifier").OnTable("Contacts");
            Alter.Column("ContactIdentifier").OnTable("Contacts").AsString().Unique();

            Delete.Index("IX_ContactGroups_ContactGroupIdentifier").OnTable("ContactGroups");
            Alter.Column("ContactGroupIdentifier").OnTable("ContactGroups").AsString().Unique();

            Alter.Column("ContactIdentifier").OnTable("ContactGroupMembers").AsString();
        }

        public override void Down()
        {
            Delete.Index("IX_Contacts_ContactIdentifier").OnTable("Contacts");
            Alter.Column("ContactIdentifier").OnTable("Contacts").AsGuid();

            Delete.Index("IX_ContactGroups_ContactGroupIdentifier").OnTable("ContactGroups");
            Alter.Column("ContactGroupIdentifier").OnTable("ContactGroups").AsGuid();

            Alter.Column("ContactIdentifier").OnTable("ContactGroupMembers").AsGuid();
        }
    }
}