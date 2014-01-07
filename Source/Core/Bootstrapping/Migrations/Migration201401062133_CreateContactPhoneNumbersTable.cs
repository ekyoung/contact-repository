using FluentMigrator;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    [Migration(201401062133)]
    public class Migration201401062133_CreateContactPhoneNumbersTable : Migration
    {
        public override void Up()
        {
            Create.Table("ContactPhoneNumbers")
                  .WithColumn("ContactId").AsInt64()
                  .WithColumn("PhoneNumber").AsString().NotNullable()
                  .WithColumn("Nickname").AsString().Nullable()
                  .WithColumn("IsPrimary").AsBoolean().NotNullable();

            Create.ForeignKey("FK_ContactPhoneNumbers_Contacts")
                 .FromTable("ContactPhoneNumbers").ForeignColumn("ContactId")
                 .ToTable("Contacts").PrimaryColumn("ContactId");
        }

        public override void Down()
        {
            Delete.Table("ContactPhoneNumbers");
        }
    }
}