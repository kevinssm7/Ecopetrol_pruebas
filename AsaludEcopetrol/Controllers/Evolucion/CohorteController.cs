using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Evolucion
{
    [SessionExpireFilter]

    public class CohorteController : Controller
    {
        #region  PROPIEDADES

        private SessionState _SesionVar;
        public SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion



        // GET: Cohorte
        public ActionResult ConcurrenciaCohorte(String idConcu)
        {
            Models.Evolucion.Cohorte Model = new Models.Evolucion.Cohorte();
            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaCohorte(Convert.ToInt32(idConcu));


                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");

            }

        }

        [HttpPost]
        public ActionResult ConcurrenciaCohorte(Models.Evolucion.Cohorte Model)
        {

            Model.OBJCohorte.id_concurrencia = Model.id_concurrencia;
            Model.OBJCohorte.id_ref_ingreso_cohorte = Model.id_ref_ingreso_cohorte;
            Model.OBJCohorte.observacion = Model.Observacion;
            Model.OBJCohorte.usuario_digita = SesionVar.UserName;
            Model.OBJCohorte.fecha_digita = DateTime.Now;

            Model.InsertarConcurrenciaCohorte(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("ConcurrenciaCohorte", "Cohorte", new { idConcu = Model.id_concurrencia });
            }
            else
            {
                ModelState.AddModelError("", "ERROR... ACTUALIZANDO....");
            }


            return View(Model);
           
          

        }


    }
}