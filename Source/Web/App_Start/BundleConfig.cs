using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-3.0.2/css/bootstrap.css",
                "~/Content/Common/Main.css"));

            bundles.Add(new ScriptBundle("~/bundles/alerts").Include(
                "~/Content/Alerts/Alerts.js"));

            bundles.Add(new ScriptBundle("~/bundles/contacts").Include(
                "~/Content/Contacts/Contacts.js",
                "~/Content/Contacts/ContactsApp.js"));
        }
    }
}