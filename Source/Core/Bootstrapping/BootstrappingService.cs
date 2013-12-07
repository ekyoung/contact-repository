using EthanYoung.ContactRepository.Bootstrapping.Migrations;

namespace EthanYoung.ContactRepository.Bootstrapping
{
    public class BootstrappingService : IBootstrappingService
    {
        public void EnsureTheDatabaseIsSetup(string databaseServerName, string databaseName)
        {
            var migrator = new Migrator();
            migrator.Migrate(databaseServerName, databaseName);
        }

        public string GetDatabaseVersion(string databaseServerName, string databaseName)
        {
            return null;
        }
    }

    public interface IBootstrappingService : IService
    {
        void EnsureTheDatabaseIsSetup(string databaseServerName, string databaseName);
        string GetDatabaseVersion(string databaseServerName, string databaseName);
    }
}