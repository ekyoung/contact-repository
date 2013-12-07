using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(1)]
    public class Migration001 : Migration
    {
        public override void Up()
        {
            Create.Table("Contacts")
                  .WithColumn("ContactId").AsInt64().Identity().PrimaryKey()
                  .WithColumn("ContactIdentifier").AsGuid().NotNullable().Unique()
                  .WithColumn("FirstName").AsCustom("varchar(100)").NotNullable()
                  .WithColumn("LastName").AsCustom("varchar(100)").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Contacts");
        }
    }
}