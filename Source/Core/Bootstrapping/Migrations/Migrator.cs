using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace EthanYoung.ContactRepository.Bootstrapping.Migrations
{
    public class Migrator
    {
        public void Migrate(string databaseServerName, string databaseName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //using (var announcer = new NullAnnouncer())
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));

            var migrationContext = new RunnerContext(announcer);

            string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", databaseServerName, databaseName);
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory();
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
            var processor = factory.Create(connectionString, announcer, options);

            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runner.MigrateUp();
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; set; }
        }
    }
}