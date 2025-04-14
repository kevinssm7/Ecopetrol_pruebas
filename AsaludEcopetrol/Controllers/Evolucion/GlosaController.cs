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
    public class GlosaController : Controller
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

        private Facede.Facade _BusClass;
        public Facede.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new Facede.Facade();
                }

            }
            set { _BusClass = value; }
        }


        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        #region METODOS
        public ActionResult Glosa(String idConcu)
        {
            Models.Evolucion.Glosa Model = new Models.Evolucion.Glosa();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                ViewBag.cups = BusClass.GetCups().ToList();
                List<Ref_cuentas_glosa> lista1 = BusClass.GetCuentaGlosa().ToList();
                lista1 = lista1.Where(l => l.estado == "A").ToList();
                ViewBag.codigoglosa = lista1;
                ViewBag.responsableglosa = BusClass.GetResGlosa().ToList();

                List<Ref_causal_glosa> listacausal1 = BusClass.GetCausalGlosa().ToList();
                List<Ref_causal_glosa> listacausal2 = BusClass.GetCausalGlosa().ToList();
                List<Ref_causal_glosa> listacausal4 = BusClass.GetCausalGlosa().ToList();
                List<Ref_causal_glosa> listacausal5 = BusClass.GetCausalGlosa().ToList();

                listacausal1 = listacausal1.Where(l => l.responsable_glosa_id == 1).ToList();
                listacausal2 = listacausal2.Where(l => l.responsable_glosa_id == 2).ToList();
                listacausal4 = listacausal4.Where(l => l.responsable_glosa_id == 4).ToList();
                listacausal5 = listacausal5.Where(l => l.responsable_glosa_id == 5).ToList();

                ViewBag.causal1 = listacausal1;
                ViewBag.causal2 = listacausal2;
                ViewBag.causal4 = listacausal4;
                ViewBag.causal5 = listacausal5;


                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaGlosa(Convert.ToInt32(idConcu));
                Model.ConsultaIdConcurrenia(Convert.ToInt32(idConcu));
                Model.causal_glosa = "";

                ViewBag.usuario = SesionVar.ROL;

                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }


        public ActionResult ConsultaGlosa(String idConcu)
        {
            Models.Evolucion.Glosa Model = new Models.Evolucion.Glosa();

            if (!(String.IsNullOrEmpty(idConcu)))
            {
                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaGlosa(Convert.ToInt32(idConcu));
                return View(Model);
            }
            else
            {
                return RedirectToAction("Inicio", "Usuario");
            }
        }

        public ActionResult calcular(Models.Evolucion.Glosa Model, string id, string ips, int cantidad, int id_concurrencia)
        {


            ecop_concurrencia_glosa ObjGlosa = new ecop_concurrencia_glosa();
            ObjGlosa.id_concurrencia = Model.id_concurrencia;
            ObjGlosa.id_cups = Model.id_cups;
            ObjGlosa.id_codigo_glosa = Convert.ToInt32(Model.id_codigo_glosa);
            ObjGlosa.cantidad_glosa = Convert.ToInt32(Model.cantidad_glosa);
            ObjGlosa.Valor_Glosa = Model.valor_glosa;
            ObjGlosa.usuario_digita = SesionVar.UserName;
            ObjGlosa.observaciones_auditoria = Model.observacion_auditoria;
            ObjGlosa.digita_fecha = Model.ManagmentHora();
            ObjGlosa.responsable_glosa = Model.responsable_glosa;

            Model.InsertaGlosa(ObjGlosa, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
            if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                ModelState.AddModelError("", "Error... Insertando....");
            }
            else
            {
                return RedirectToAction("Glosa", "Glosa", new { idConcu = ObjGlosa.id_concurrencia });
                //return View("Glosa", Model);
            }

            return View("Glosa", Model);
            // Glosa(Model);

        }


        [HttpPost]
        public ActionResult Glosa(Models.Evolucion.Glosa Model, Int32? idconcurrencia, String codigosglosa, String valorglosa, String valorglosaoculto,  Int32? codigoglosa, Int32? cantidad, String responsableglosa, Int32? causalglosa1,
           Int32? causalglosa2, Int32? causalglosa4, Int32? causalglosa5, String otrocausalglosa, String observacion)
        {


            ecop_concurrencia_glosa ObjGlosa = new ecop_concurrencia_glosa();
            ObjGlosa.id_concurrencia = idconcurrencia;
            ObjGlosa.id_cups = codigosglosa;
            ObjGlosa.id_codigo_glosa = Convert.ToInt32(codigoglosa);
            ObjGlosa.cantidad_glosa = Convert.ToInt32(cantidad);
            ObjGlosa.Valor_Glosa = valorglosaoculto;
            ObjGlosa.usuario_digita = SesionVar.UserName;

            observacion = observacion.Replace("\r", "");
            observacion = observacion.Replace("\n", "");

            ObjGlosa.observaciones_auditoria = observacion;
            ObjGlosa.digita_fecha = Model.ManagmentHora();
            ObjGlosa.responsable_glosa = responsableglosa;
            if (responsableglosa == "1")
            {
                ObjGlosa.causal_glosa = causalglosa1;
                if (causalglosa1 == 4)
                {
                    ObjGlosa.otro_causal_glosa = otrocausalglosa;
                }
                else
                {
                    ObjGlosa.otro_causal_glosa = "NA";
                }

            }
            else if (responsableglosa == "2")
            {
                ObjGlosa.causal_glosa = causalglosa2;
            }
            else if (responsableglosa == "4")
            {
                ObjGlosa.causal_glosa = causalglosa4;
            }
            else
            {
                ObjGlosa.causal_glosa = causalglosa5;
            }

            Model.InsertaGlosa(ObjGlosa, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);

            ViewBag.cups = BusClass.GetCups().ToList();

            if (MsgRes.ResponseType != BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                ModelState.AddModelError("", "Error... Insertando....");
            }
            else
            {
                return RedirectToAction("Glosa", "Glosa", new { idConcu = idconcurrencia });
                //return View("Glosa", Model);
            }



            return View(Model);
        }


        public JsonResult GetCups(Models.Evolucion.Glosa Model)
        {
            return Json(Model.LstCups.ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetrefCuentaMaestroIps(Models.Evolucion.Glosa Model)
        {
            return Json(Model.lstIPS.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo utilizado para obtener las causales de una glosa, dependiendo de su responsable.
        /// </summary>
        /// <param name="idresponsable"></param>
        /// <returns></returns>
        public JsonResult GetCausalGlosa(int idresponsable)
        {
            Models.Evolucion.Glosa Model = new Models.Evolucion.Glosa();
            List<Ref_causal_glosa> result = Model.ColsultaCausalGlosa(idresponsable);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Traervalor(String id, Models.Evolucion.Glosa Model)
        {
            if (id != null)
            {
                List<vw_ref_cups> listCups = new List<vw_ref_cups>();

                listCups = Model.LstCups.ToList();
                listCups = listCups.Where(x => x.id_cups == id).ToList();

                foreach (var item in listCups)
                {
                    if (item.valor_cups != null)
                    {
                        String mensaje = "";
                        mensaje = item.valor_cups;
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult valorglosa(String opcionrealizar, String id)
        {
            var valor = "";

            Models.General General = new Models.General();
            Models.Evolucion.Glosa Model = new Models.Evolucion.Glosa();

            List<vw_ref_cups> listglosa = BusClass.GetCups().ToList();
            listglosa = listglosa.Where(l => l.id_cups == id).ToList();

            foreach (var item in listglosa)
            {
                valor = item.valor_cups;
            }

            if (valor != null)
            {
                opcionrealizar = "1";
            }
            else
            {
                opcionrealizar = "2";
            }


            return Json(new { id, opcionrealizar, valor }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}