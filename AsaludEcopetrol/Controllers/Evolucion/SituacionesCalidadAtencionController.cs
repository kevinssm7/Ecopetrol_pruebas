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
    public class SituacionesCalidadAtencionController : Controller
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
        public ActionResult SituacionesCalidadAtencion(String idConcu)
        {
            Models.Evolucion.SituacionesCalidadAtencion Model = new Models.Evolucion.SituacionesCalidadAtencion();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaSituacionesDeCalidad(Convert.ToInt32(idConcu));

                ViewBag.usuario = SesionVar.ROL;

                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult SituacionesCalidadAtencion(Models.Evolucion.SituacionesCalidadAtencion Model)
        {

            if (Model.fecha_situacion_calidok != null)
            {
                if (Model.fecha_de_radica_cartaok != null)
                {
                    if (Model.id_situacion_calidad != null)
                    {
                        ecop_concurrencia_situaciones_de_calidad ObjSitCalid = new ecop_concurrencia_situaciones_de_calidad();
                        ObjSitCalid.id_concurrencia = Model.id_concurrencia;
                        ObjSitCalid.id_situacion = Convert.ToInt32(Model.id_situacion_calidad);
                        ObjSitCalid.descripcion = Model.descripcion_situacion_calidad;
                        ObjSitCalid.fecha_situcion = Model.fecha_situacion_calidok;
                        ObjSitCalid.usuario_digita = SesionVar.UserName;
                        ObjSitCalid.fecha_digita = Model.ManagmentHora();
                        ObjSitCalid.fecha_de_radica_carta = Model.fecha_de_radica_cartaok;
                        ObjSitCalid.quien_recibe = Model.quien_recibe;

                        Model.InsertaSituacionesCalidad(ObjSitCalid, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
                        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ModelState.AddModelError("", "Error... Insertando....");
                        }
                        else
                        {
                            return RedirectToAction("SituacionesCalidadAtencion", "SituacionesCalidadAtencion", new { idConcu = ObjSitCalid.id_concurrencia });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR: *---Debe ingresar situacion de calidad--- *");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
                }
               
            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }


            return View(Model);
        }


        public JsonResult GetCascadeCategoria(Models.Evolucion.SituacionesCalidadAtencion Model)
        {
            return Json(Model.LstCategoriasSituacion.Select(c => new { id_ref_categorias_situaciones_de_calidad = c.id_ref_categorias_situaciones_de_calidad, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSituacion(string categoria, Models.Evolucion.SituacionesCalidadAtencion Model)
        {
            if (categoria != null)
            {
                Model.ConsultaLista(1, categoria, ref MsgRes);
            }
            return Json(Model.LstSituacionesCa.Select(p => new { id_calidad = p.id_calidad, descripcion = p.descripcion }), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}