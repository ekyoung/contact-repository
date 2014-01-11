using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201401101942)]
    public class Migration201401101942_CreateContactGroupsTable : Migration
    {
        public override void Up()
        {
            Create.Table("ContactGroups")
                  .WithColumn("ContactGroupId").AsInt64().Identity().PrimaryKey()
                  .WithColumn("ContactGroupIdentifier").AsGuid().NotNullable().Unique()
                  .WithColumn("Name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ContactGroups");
        }
    }
}