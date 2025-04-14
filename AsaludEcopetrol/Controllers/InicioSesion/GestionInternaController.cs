using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.Models;
using ECOPETROL_COMMON.ENTIDADES;

namespace AsaludEcopetrol.Controllers.InicioSesion
{
    public class GestionInternaController : Controller
    {
        
        public ActionResult gestioninterna()
        {
            Models.InicioSesion.GestionInterna Model = new Models.InicioSesion.GestionInterna();
            ViewBag.gestioninterna = Model.GetGestionInternaList();
            return View();
        }

        public JsonResult GetDatosGestion(int idgestioninterna)
        {
            Models.InicioSesion.GestionInterna Model = new Models.InicioSesion.GestionInterna();
            ref_gestion_interna obj = Model.GetGestionInternaById(idgestioninterna);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
}