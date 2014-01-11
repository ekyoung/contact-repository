using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201401102110)]
    public class Migration201401102110_CreateContactGroupMembersTable : Migration
    {
        public override void Up()
        {
            Create.Table("ContactGroupMembers")
                  .WithColumn("ContactGroupId").AsInt64()
                  .WithColumn("ContactIdentifier").AsGuid().NotNullable();

            Create.ForeignKey("FK_ContactGroupMembers_ContactGroups")
                  .FromTable("ContactGroupMembers").ForeignColumn("ContactGroupId")
                  .ToTable("ContactGroups").PrimaryColumn("ContactGroupId");
        }

        public override void Down()
        {
            Delete.Table("ContactGroupMembers");
        }
    }
}