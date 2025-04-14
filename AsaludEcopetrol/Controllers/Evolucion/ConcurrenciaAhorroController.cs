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
    public class ConcurrenciaAhorroController : Controller
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
        public ActionResult CostoEvitadoAhorro(String idConcu)
        {
            Models.Evolucion.ConcurrenciaAhorro Model = new Models.Evolucion.ConcurrenciaAhorro();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaAhorro(Convert.ToInt32(idConcu));

                ViewBag.usuario = SesionVar.ROL;

                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult CostoEvitadoAhorro(Models.Evolucion.ConcurrenciaAhorro Model)
        {

            Model.OBJAhorro.id_concurrencia = Model.id_concurrencia;
            Model.OBJAhorro.id_ref_tipo_ahorro = Model.id_ref_tipo_ahorro;
            Model.OBJAhorro.otro_tipo_ahorro = Model.otro_tipo_ahorro;
            Model.OBJAhorro.cantidad = Model.cantidad;
            Model.OBJAhorro.ahorro = Model.id_ref_valor_ahorro;
            Model.OBJAhorro.valor_ahorro = Convert.ToString(Model.valor_ahorro);
            Model.OBJAhorro.valor_ahorro_otro = Convert.ToString(Model.valor_ahorro2);
            Model.OBJAhorro.observacion = Model.observacion;
            Model.OBJAhorro.fecha_digita = Convert.ToDateTime(DateTime.Now);
            Model.OBJAhorro.usuario_digita = SesionVar.UserName;

            Model.InsertarConcurrenciaAhorro(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("CostoEvitadoAhorro", "ConcurrenciaAhorro", new { idConcu = Model.id_concurrencia });
            }
            else
            {
                ModelState.AddModelError("", "ERROR... ACTUALIZANDO....");
            }

            return View(Model);
        }


        [HttpPost]
        public ActionResult ListaValorAhorro(Int32 id, Models.Evolucion.ConcurrenciaAhorro Model)
        {
            var List = Model.ListaValorAhorro(id);
            if (List.Count() > 0)
            {
                Ref_valor_ahorro OBJ = new Ref_valor_ahorro();
                foreach (var item in List)
                {
                    OBJ.costo_dia_de_estancia = item.costo_dia_de_estancia;
                }


                return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion
    }
}