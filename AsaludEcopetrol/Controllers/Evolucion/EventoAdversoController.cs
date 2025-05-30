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
    public class EventoAdversoController : Controller
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
        public ActionResult EventoAdverso(String idConcu)
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaEventoAdverso(Convert.ToInt32(idConcu));
                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        public ActionResult EventoEnSalud(String idConcu)
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
               // Model.ConsultaEventoAdverso(Convert.ToInt32(idConcu));
                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        public ActionResult EventosAnalisis()
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();

           return View(Model);
        }

        [HttpGet]
        public ActionResult EventosAnalisisReporte(String ID, int pagina = 1)
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();


            Model.Lista = Model.GetLista();
            Model.Lista = Model.Lista.Where(x => x.analisis == null).ToList();

            return View(Model);
 
        }

        [HttpGet]
        public ActionResult EventosPlanmejoraReporte(String ID, int pagina = 1)
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();


            Model.Lista = Model.GetLista();
            Model.Lista = Model.Lista.Where(x => x.analisis == true).ToList();
            Model.Lista = Model.Lista.Where(x => x.plan_mejora == null).ToList();

            return View(Model);

        }

        [HttpGet]
        public ActionResult EventosCierreReporte(String ID, int pagina = 1)
        {
            Models.Evolucion.EventoAdverso Model = new Models.Evolucion.EventoAdverso();


            Model.Lista = Model.GetLista();
            Model.Lista = Model.Lista.Where(x => x.analisis == true).ToList();
            Model.Lista = Model.Lista.Where(x => x.plan_mejora == true).ToList();
            Model.Lista = Model.Lista.Where(x => x.cierre_plan_mejora == null).ToList();

            return View(Model);

        }

        [HttpPost]
        public ActionResult EventoAdverso(Models.Evolucion.EventoAdverso Model)
        {

            if (Model.fecha_de_radica_cartaok != null)
            {
                if (Model.fecha_evento_adversook != null)
                {
                    if (Model.id_evento_adverso != 0)
                    {
                        ecop_concurrencia_eventos_adversos ObjEventoAdv = new ecop_concurrencia_eventos_adversos();
                        ObjEventoAdv.id_concurrencia = Model.id_concurrencia;
                        ObjEventoAdv.id_ref_evento_adv = Model.id_evento_adverso;
                        ObjEventoAdv.descripcion_Evento = Model.descripcion_evento;
                        ObjEventoAdv.id_gradoLesion = Convert.ToInt32(Model.id_grado_lesion);
                        ObjEventoAdv.id_plan_de_manejo = Convert.ToInt32(Model.id_plan_de_manejo);
                        ObjEventoAdv.id_barrera_seguiridad = Convert.ToInt32(Model.id_barreras_seguridad);
                        ObjEventoAdv.id_facotres_contri = Convert.ToInt32(Model.id_factores_contribuyentes);
                        ObjEventoAdv.id_acciones_inseg = Convert.ToInt32(Model.id_acciones_inseguras);
                        ObjEventoAdv.fecha_evento_adv = Model.fecha_evento_adversook;
                        ObjEventoAdv.usuario_digita = SesionVar.UserName;
                        ObjEventoAdv.fecha_digita = Model.ManagmentHora();
                        ObjEventoAdv.fecha_de_radica_carta = Model.fecha_de_radica_cartaok;
                        ObjEventoAdv.quien_recibe = Model.quien_recibe;

                        Model.InsertaEventoAdverso(ObjEventoAdv, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);


                        if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ModelState.AddModelError("", "Error... Insertando....");
                        }
                        else
                        {
                            return RedirectToAction("EventoAdverso", "EventoAdverso", new { idConcu = ObjEventoAdv.id_concurrencia });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR: *--- seleccione eventos adversos--- *");
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
        
        [HttpPost]
        public ActionResult EventosAnalisis(Models.Evolucion.EventoAdverso Model)
        {

            if (Model.Items == 1)
            {
                return RedirectToAction("EventosAnalisisReporte", "EventoAdverso");
            }
            else if (Model.Items == 2)
            {
                return RedirectToAction("BuscarCensoD", "Censo");
            }
            else if (Model.Items == 3)
            {
                return RedirectToAction("BuscarCensoD", "Censo");
            }

            return View(Model);
        }

        public JsonResult GetCascadeCategoria(Models.Evolucion.EventoAdverso Model)
        {
            return Json(Model.LstCategoriasAdversos.Select(c => new { id_ref_categorias_eventos_adv = c.id_ref_categorias_eventos_adv, descripcion = c.descripcion }), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetCascadeEventosAdv(string categoria, Models.Evolucion.EventoAdverso Model)
        {
            if (categoria != null)
            {
                Model.ConsultaLista(1, categoria, ref MsgRes);
            }
            return Json(Model.LstEventoAdversos2.Select(p => new { id_ref_evento_adv = p.id_ref_evento_adv, nombre = p.des }), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}