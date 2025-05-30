using AsaludEcopetrol.Models.CuentasMedicas;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    [SessionExpireFilter]
    public class homologacionController : Controller
    {
        public static List<HomologacionRips> persons = new List<HomologacionRips>();

        static homologacionController()
        {
            persons.Add(new HomologacionRips { PersonID = 1, Name = "John", BirthDate = new DateTime(1968, 6, 26) });
            persons.Add(new HomologacionRips { PersonID = 2, Name = "Sara", BirthDate = new DateTime(1974, 9, 13) });
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult GetPersons([DataSourceRequest] DataSourceRequest dsRequest)
        {
            var result = persons.ToDataSourceResult(dsRequest);
            return Json(result);
        }

        public ActionResult UpdatePerson([DataSourceRequest] DataSourceRequest dsRequest, HomologacionRips person)
        {
            if (person != null && ModelState.IsValid)
            {
                var toUpdate = persons.FirstOrDefault(p => p.PersonID == person.PersonID);
                TryUpdateModel(toUpdate);
            }


            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

    }
}
