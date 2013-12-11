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

            bundles.Add(new ScriptBundle("~/bundles/contacts").Include(
                "~/Content/Contacts/ContactsApp.js",
                "~/Content/Contacts/ContactsControllers.js"));
        }
    }
}