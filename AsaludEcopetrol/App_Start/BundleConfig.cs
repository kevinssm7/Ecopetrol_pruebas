﻿using System.Web;
using System.Web.Optimization;

namespace AsaludEcopetrol
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.2.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                  "~/Scripts/kendo/kendo.web.min.js",
                  "~/Scripts/kendo/kendo.aspnetmvc.min.js",
                  "~/Scripts/kendo/kendo.jszip.min.js",
                    "~/Scripts/kendo/kendo.all.min.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                     "~/Content/themes/base/all.css"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
                 "~/Content/kendo.common.min.css",
                 "~/Content/kendo.flat.min.css",
                 "~/Content/kendo.default.min.css"
                 ));


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                         "~/Content/themes/base/jquery.ui.css",
                         "~/Content/themes/base/jquery.ui.core.css",
                         "~/Content/themes/base/jquery.ui.resizable.css",
                         "~/Content/themes/base/jquery.ui.selectable.css",
                         "~/Content/themes/base/jquery.ui.accordion.css",
                         "~/Content/themes/base/jquery.ui.autocomplete.css",
                         "~/Content/themes/base/jquery.ui.button.css",
                         "~/Content/themes/base/jquery.ui.dialog.css",
                         "~/Content/themes/base/jquery.ui.slider.css",
                         "~/Content/themes/base/jquery.ui.tabs.css",
                         "~/Content/themes/base/jquery.ui.datepicker.css",
                         "~/Content/themes/base/jquery.ui.progressbar.css",
                         "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
