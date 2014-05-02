using System.Web.Optimization;

namespace EthanYoung.ContactRepository.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-3.0.2/css/bootstrap.css",
                "~/Content/Common/Main.css"));

            bundles.Add(new ScriptBundle("~/bundles/commonApp").Include(
                "~/Content/Alerts/Alerts.js",
                "~/Content/Guids/Guids.js",
                "~/Content/Tasks/Tasks.js"));

            bundles.Add(new ScriptBundle("~/bundles/contactsApp").Include(
                "~/Content/ContactGroups/ContactGroups.js",
                "~/Content/Contacts/Contacts.js",
                "~/Content/Contacts/ContactsApp.js"));
        }
    }
}