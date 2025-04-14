using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Analitica
{
    [SessionExpireFilter]
    public class AnaliticaController : Controller
    {
        public PartialViewResult CargueAnalitica()
        {
            return PartialView();
        }
    }
}