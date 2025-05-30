using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AsaludEcopetrol
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string ruta = "";

            DateTime fechaInicio = new DateTime(2025, 01, 31, 22, 00, 00);
            DateTime fechaFin = new DateTime(2025, 02, 02, 22, 00, 00);
            
            //if(DateTime.Now >= fechaInicio && DateTime.Now <= fechaFin)
            //{
            //    ruta = "LoginCerrado";
            //}
            //else
            //{
            //    ruta = "Login";
            //}

            ruta = "Login";

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = ruta, id = UrlParameter.Optional }
            );
        }
    }
}