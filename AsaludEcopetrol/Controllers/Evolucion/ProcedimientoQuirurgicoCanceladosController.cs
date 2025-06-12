using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Evolucion
{
    [SessionExpireFilter]
    public class ProcedimientoQuirurgicoCanceladosController : Controller
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

        #region METODOS
        public ActionResult ProcedimientoQuirurgicoCancelado(String idConcu)
        {
            Models.Evolucion.ProcedimientoQuirurgicoCancelados Model = new Models.Evolucion.ProcedimientoQuirurgicoCancelados();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaProcedimientoQuirurgicoCancelado(Convert.ToInt32(idConcu));
                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult ProcedimientoQuirurgicoCancelado(Models.Evolucion.ProcedimientoQuirurgicoCancelados Model)
        {

            if (ModelState.IsValid)
            {
                ecop_concurrencia_procedimientos_quirurgicos_cancelados ObjProcedi = new ecop_concurrencia_procedimientos_quirurgicos_cancelados();
                ObjProcedi.id_concurrencia = Model.id_concurrencia;
                ObjProcedi.procedimiento = Model.id_cups;
                ObjProcedi.fecha_cancelacion = Model.fecha_cancelacion;
                ObjProcedi.id_motivo_cancelacion = Convert.ToInt32(Model.id_motivo_cancelacion);
                ObjProcedi.usuario_digita = SesionVar.UserName;
                ObjProcedi.fecha_digita = Model.ManagmentHora();
                Model.InsertaProcedimientoQuirugicoCancelado(ObjProcedi, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
                if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ModelState.AddModelError("", "Error... Insertando....");
                }
                else
                {
                    return RedirectToAction("ProcedimientoQuirurgicoCancelado", "ProcedimientoQuirurgicoCancelados", new { idConcu = ObjProcedi.id_concurrencia });
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }


            return View(Model);
        }

        public JsonResult GetCups(Models.Evolucion.ProcedimientoQuirurgicoCancelados Model)
        {
            return Json(Model.LstCups.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}