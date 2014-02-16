using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201402161007)]
    public class Migration201402161007_CreateContactGroupMemberRelationshipsTable : Migration
    {
        public override void Up()
        {
            Create.Table("ContactGroupMemberRelationships")
                .WithColumn("ContactGroupMemberId").AsInt64()
                .WithColumn("Relationship").AsString().NotNullable();

            Create.ForeignKey("FK_ContactGroupMemberRelationships_ContactGroupMembers")
                .FromTable("ContactGroupMemberRelationships").ForeignColumn("ContactGroupMemberId")
                .ToTable("ContactGroupMembers").PrimaryColumn("ContactGroupMemberId");
        }

        public override void Down()
        {
            Delete.Table("ContactGroupMemberRelationships");
        }
    }
}