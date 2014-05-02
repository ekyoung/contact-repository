using System;
using System.Diagnostics;
using EthanYoung.ContactRepository.Bootstrapping;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests.Bootstrapping
{
    [Binding]
    public class BootstrappingServiceCanCreateADatabaseWithTheCorrectSchemaSteps
    {
        private string _databaseServerName;
        private string _databaseName;
        private string _retrievedDatabaseVersion;

        private IBootstrappingService _service = DependencyRegistry.Resolve<IBootstrappingService>();

        [BeforeScenario]
        public void BeforeScenario()
        {
            _databaseServerName = null;
            _databaseName = null;
        }

        [BeforeScenario("blankDb")]
        public void BeforeScenarioBlankDb()
        {
            CreateBlankDb();
        }

        [AfterScenario("blankDb")]
        public void AfterScenarioBlankDb()
        {
            RemoveBlankDb();
        }

        [Given(@"database server (.*) and database (.*)")]
        public void GivenDatabaseServerLocalhostAndAUniqueDatabaseName(string databaseServerName, string databaseName)
        {
            _databaseServerName = databaseServerName;
            _databaseName = databaseName;
        }
        
        [Given(@"I ensure the database is setup")]
        public void GivenIEnsureTheDatabaseIsSetup()
        {
            _service.EnsureTheDatabaseIsSetup(_databaseServerName, _databaseName);
        }
        
        [When(@"I retrieve the database version")]
        public void WhenIRetrieveTheDatabaseVersion()
        {
            _retrievedDatabaseVersion = _service.GetDatabaseVersion(_databaseServerName, _databaseName);
        }
        
        [Then(@"the retrieved database version should be the latest version")]
        public void ThenTheRetrievedDatabaseVersionShouldBeTheLatestVersion()
        {
            Assert.IsNotNull(_retrievedDatabaseVersion);
        }

        private void CreateBlankDb()
        {
            RunSqlcmd(@"-S localhost\SQLEXPRESS -i ""C:\PersonalProjects\ContactRepository\Source\Tests\AcceptanceTests\Bootstrapping\CreateBlankDb.sql""");
        }

        private void RemoveBlankDb()
        {
            RunSqlcmd(@"-S localhost\SQLEXPRESS -q ""ALTER DATABASE ContactRepository_Blank SET SINGLE_USER WITH ROLLBACK IMMEDIATE""");
            RunSqlcmd(@"-S localhost\SQLEXPRESS -q ""DROP DATABASE ContactRepository_Blank""");
        }

        private static void RunSqlcmd(string argument)
        {
            const string appName = @"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd.exe";

            var startInfo = new ProcessStartInfo
            {
                FileName = appName,
                Arguments = argument,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            const int timeToWait = 50000;

            RunApplication(startInfo, timeToWait);
        }

        private static void RunApplication(ProcessStartInfo startInfo, int timeToWait)
        {
            using (Process appToRun = new Process())
            {
                appToRun.StartInfo = startInfo;
                appToRun.Start();
                if (!appToRun.WaitForExit(timeToWait))
                {
                    Console.WriteLine("Timeout");
                    return;
                }
                if (appToRun.ExitCode != 0)
                {
                    Console.WriteLine("Failed");
                }
            }
        }
    }
}
