using System.Web;
using System.Web.Optimization;

namespace MyComicStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            
            //ComicStore/Index
            bundles.Add(new StyleBundle("~/bundles/comicstoreindex/ss").Include(
                      "~/Content/ComicStore/Index.css",
                      "~/Content/ComicStore/Browse.css"));

            bundles.Add(new ScriptBundle("~/bundles/comicstorescript/js").Include(
                      "~/Scripts/ComicStore/Index.js",
                      "~/Scripts/ComicStore/Browse.js"));

            bundles.Add(new ScriptBundle("~/bundles/DropdownList/js").Include(
                      "~/Scripts/ComicStore/DropdownList.js"));

            bundles.Add(new ScriptBundle("~/bundles/ComicList/js").Include(
                      "~/Scripts/ComicStore/ComicList.js"));

            bundles.Add(new ScriptBundle("~/bundles/Details/js").Include(
                      "~/Scripts/ComicStore/Details.js"));

            bundles.Add(new ScriptBundle("~/bundles/Searchbar/js").Include(
                      "~/Scripts/ComicStore/SearchBar.js"));

            //BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;

        }
    }
}
