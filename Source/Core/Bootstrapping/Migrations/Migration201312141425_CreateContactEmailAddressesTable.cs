using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201312141425)]
    public class Migration201312141425_CreateContactEmailAddressesTable : Migration
    {
        public override void Up()
        {
            Create.Table("ContactEmailAddresses")
                  .WithColumn("ContactId").AsInt64()
                  .WithColumn("EmailAddress").AsString().NotNullable()
                  .WithColumn("Nickname").AsString().Nullable()
                  .WithColumn("IsPrimary").AsBoolean().NotNullable();

            Create.ForeignKey("FK_ContactEmailAddresses_Contacts")
                 .FromTable("ContactEmailAddresses").ForeignColumn("ContactId")
                 .ToTable("Contacts").PrimaryColumn("ContactId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_ContactEmailAddresses_Contacts");
            Delete.Table("ContactEmailAddresses");
        }
    }
}