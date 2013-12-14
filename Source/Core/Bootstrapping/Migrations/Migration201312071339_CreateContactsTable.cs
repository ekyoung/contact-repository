using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201312071339)]
    public class Migration201312071339_CreateContactsTable : Migration
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