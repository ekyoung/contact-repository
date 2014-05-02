using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201402160952)]
    public class Migration201402160952_AddPrimaryKeyToContactGroupMembersTable : Migration
    {
        public override void Up()
        {
            Alter.Table("ContactGroupMembers")
                .AddColumn("ContactGroupMemberId").AsInt64().Identity();

            Create.PrimaryKey("PK_ContactGroupMembers").OnTable("ContactGroupMembers").Column("ContactGroupMemberId");
        }

        public override void Down()
        {
            Delete.PrimaryKey("PK_ContactGroupMembers").FromTable("ContactGroupMembers");

            Delete.Column("ContactGroupMemberId").FromTable("ContactGroupMembers");
        }
    }
}