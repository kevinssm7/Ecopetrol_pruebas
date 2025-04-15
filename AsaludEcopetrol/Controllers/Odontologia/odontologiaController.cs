using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Odontologia;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Odontologia
{
    [SessionExpireFilter]
    public class odontologiaController : Controller
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

        public ActionResult IngresoOrtodoncia()
        {
            Models.Odontologia.ortodoncia Model = new Models.Odontologia.ortodoncia();

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.sino = BusClass.Ref_sino();
            ViewBag.Porcentajes = BusClass.getcheckPorcentaje();
            return View(Model);
        }

        public ActionResult IngresoProtesisFija()
        {
            Models.Odontologia.ProtesisFija Model = new Models.Odontologia.ProtesisFija();

            List<ref_odontologia_protesisfija_dientes> uno = new List<ref_odontologia_protesisfija_dientes>();
            List<ref_odontologia_protesisfija_dientes> dos = new List<ref_odontologia_protesisfija_dientes>();
            List<int> diente1 = new List<int>();
            List<int> diente2 = new List<int>();

            try
            {

                Model.alerta = "NO";
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.meses = BusClass.meses();
                ViewBag.sino = BusClass.Ref_sino();
                ViewBag.Porcentajes = BusClass.getcheckPorcentaje();
                ViewBag.alerta = "NO";

                uno = BusClass.OdontoProtesisFijaDientes(1);
                dos = BusClass.OdontoProtesisFijaDientes(2);


                //ViewBag.dientesLineaUno = BusClass.OdontoProtesisFijaDientes(1);
                //ViewBag.dientesLineaDos = BusClass.OdontoProtesisFijaDientes(2);

                foreach (var d1 in uno)
                {
                    var diente = d1.diente;
                    if (diente != null)
                    {
                        diente1.Add((int)diente);
                    }
                }

                foreach (var d2 in dos)
                {
                    var diente = d2.diente;
                    if (diente != null)
                    {
                        diente2.Add((int)diente);
                    }
                }

                ViewBag.dientesLineaUno = diente1;
                ViewBag.dientesLineaDos = diente1;

                //ViewBag.dientesListaUno = diente1;
                //ViewBag.dientesListaDos = diente1;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Model);
        }

        public JsonResult traerDientes(int? tipo)
        {
            var listatotal = new object();

            try
            {
                List<ref_odontologia_protesisfija_dientes> dientes = new List<ref_odontologia_protesisfija_dientes>();
                dientes = BusClass.OdontoProtesisFijaDientes(tipo);

                ViewBag.Lista = dientes;
                listatotal = (from item in dientes
                              select new
                              {
                                  value = item.diente,
                                  text = item.diente,
                              });
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AuditoriaDientesFija(string dientesUno, string dientesDos)
        {

            Models.Odontologia.ProtesisFija Model = new Models.Odontologia.ProtesisFija();
            List<int> diente1 = new List<int>();
            List<int> diente2 = new List<int>();

            try
            {
                Model.alerta = "NO";

                String[] dientesUnoLista = new string[0];
                if (dientesUno != null)
                {
                    dientesUnoLista = dientesUno.Split(',');
                }


                String[] dientesDosLista = new string[0];
                if (dientesDos != null)
                {
                    dientesDosLista = dientesDos.Split(',');
                }

                if (dientesUnoLista.Count() > 0)
                {
                    foreach (var item in dientesUnoLista)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            diente1.Add(Convert.ToInt32(item));
                        }
                    }
                }

                if (dientesDosLista.Count() > 0)
                {
                    foreach (var item in dientesDosLista)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            diente2.Add(Convert.ToInt32(item));
                        }
                    }
                }

                ViewBag.listaDientesUno = diente1;
                ViewBag.listaDientesDos = diente2;

                ViewBag.listaDientesUnoConteo = dientesUnoLista.Count();
                ViewBag.listaDientesDosConteo = dientesDosLista.Count();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView(Model);
        }

        public ActionResult ProtesisRemovibles()
        {
            Models.Odontologia.ProtesisRemovible Model = new Models.Odontologia.ProtesisRemovible();

            return View(Model);
        }

        public ActionResult IngresoProtesisRemovibles(Int32 valor)
        {
            Models.Odontologia.ProtesisRemovible Model = new Models.Odontologia.ProtesisRemovible();

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.sino = BusClass.Ref_sino();
            ViewBag.Porcentajes = BusClass.getcheckPorcentaje();
            ViewBag.numero_protesis = valor;

            return View(Model);
        }

        public ActionResult IngresoEndodoncia()
        {
            Models.Odontologia.Endodoncia Model = new Models.Odontologia.Endodoncia();

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.sino = BusClass.Ref_sino();
            ViewBag.Porcentajes = BusClass.getcheckPorcentaje();
            ViewBag.tipoEndodoncia = BusClass.getListTipoEndodoncia();
            ViewBag.dientes = BusClass.OdontoProtesisFijaDientesTotal();

            return View(Model);
        }

        public ActionResult TableroPlandeMejora(Models.Odontologia.PlanMejora Model)
        {
            return View(Model);
        }

        public ActionResult PlandeMejora()
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.sino = BusClass.Ref_sino();

            return View(Model);
        }

        public ActionResult ImpresionReportesTratamientos()
        {
            Models.Odontologia.ortodoncia Model = new Models.Odontologia.ortodoncia();

            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.historico = Model.ListReporteTratUnif;
            ViewBag.tratamiento = from OdontTipoTratamiento d in Enum.GetValues(typeof(OdontTipoTratamiento))
                                  select new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = DescriptionEnums.DescriptionAttr(d)
                                  };


            return View(Model);
        }


        public ActionResult IngresoPlanMejoraBen()
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            return View(Model);
        }

        public ActionResult IngresoPrestador()
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            ViewBag.regionales = BusClass.GetRefRegion();
            return View(Model);
        }

        public ActionResult PlandeMejoraBeneficiario(int id)
        {

            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Model.id_odont_plan_mejora_beneficiario = id;


            List<odont_plan_mejora_beneficiario> list = new List<odont_plan_mejora_beneficiario>();

            list = Model.GetPlanMejoraBen();
            list = list.Where(x => x.id_odont_plan_mejora_beneficiario == id).ToList();

            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    Model.Observacion = item.observacion_tratamiento;
                }

            }


            return View(Model);
        }

        public ActionResult PlandeMejoraDetalle(int id)
        {
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.id_odont_plan_mejora = id;
            ViewBag.plan_mejora = BusClass.GetListAccionesMejora();

            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Model.id_odont_plan_mejora = id;

            List<odont_plan_mejora> list = new List<odont_plan_mejora>();

            list = Model.GetPlanMejoraTra();
            list = list.Where(x => x.id_odont_plan_mejora == id).ToList();

            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    Model.estado = Convert.ToString(item.estado);
                }

            }


            return View(Model);
        }

        public ActionResult TableroPlandeMejoraBeneficiario(Models.Odontologia.PlanMejora Model)
        {
            return View(Model);
        }

        public ActionResult IngresoHistoriaClinica()
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.sino = BusClass.Ref_sino();

            return View(Model);
        }

        public ActionResult IngresoDetalleHistoriaClinica(Int32 id)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();

            Model.id_odont_historia_clinica = id;

            return View(Model);
        }

        public ActionResult IngresoPacienteHC(Int32 id)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();

            ViewBag.id_odont_historia_clinica = id;
            Model.id_odont_historia_clinica = id;

            return View(Model);
        }

        public ActionResult IngresoPacienteHCDetalle(Int32 id, Int32 id2)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();

            Model.id_odont_historia_clinica = id;
            Model.id_odont_historia_clinica_paciente = id2;

            return View(Model);
        }

        [HttpPost]
        public ActionResult IngresoPacienteHC(Models.Odontologia.HistoriaClinica Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_atencionok != null)
            {

            }
            else
            {
                variable2 = "FECHA ATENCION";
                Conteo = Conteo + 1;
            }

            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                List<odont_historia_clinica_paciente> list = new List<odont_historia_clinica_paciente>();

                list = Model.GetHistoriaClinicaPaciente(Model.id_odont_historia_clinica, ref MsgRes);

                if (list.Count > 3)
                {
                    ModelState.AddModelError("", "ERROR AL INGRESO SOLO PUEDE INGRESAR 3 PACIENTES POR  PERIODO !");
                }
                else
                {
                    odont_historia_clinica_paciente OBJ = new odont_historia_clinica_paciente();

                    OBJ.id_odont_historia_clinica = Model.id_odont_historia_clinica;
                    OBJ.numero_hc = Model.numero_hc;
                    OBJ.paciente = Model.paciente;
                    OBJ.fecha_atencion = Model.fecha_atencionok;
                    OBJ.estado = 1;

                    Model.id_odont_historia_clinica_paciente = Model.InsertarHistoriaClinicaPaciente(OBJ, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<Ref_odont_hc_calidad_formal> List1 = new List<Ref_odont_hc_calidad_formal>();

                        List1 = Model.GetHistoriaClinicaRefCalidadFormal();

                        odont_historia_clinica_detalle_calidad obj1 = new odont_historia_clinica_detalle_calidad();
                        foreach (var item in List1)
                        {
                            obj1.id_odont_historia_clinica_paciente = Model.id_odont_historia_clinica_paciente;
                            obj1.id_ref_odont_hc_calidad_formal = item.id_ref_odont_hc_calidad_formal;
                            obj1.calificacionf = 0;
                            obj1.ponderacionf = item.Ponderacion;
                            obj1.calificacion_ponderadaf = 0;

                            Model.InsertarHistoriaClinicaDetalle(obj1, ref MsgRes);
                        }


                        List<Ref_odont_hc_calidad_contenido> List2 = new List<Ref_odont_hc_calidad_contenido>();

                        List2 = Model.GetHistoriaClinicaRefContenido();

                        odont_historia_clinica_detalle_contenido obj2 = new odont_historia_clinica_detalle_contenido();
                        foreach (var item in List2)
                        {
                            obj2.id_odont_historia_clinica_paciente = Model.id_odont_historia_clinica_paciente;
                            obj2.id_ref_odont_hc_calidad_contenido = item.id_ref_odont_hc_calidad_contenido;
                            obj2.calificacionc = 0;
                            obj2.ponderacionc = item.Ponderacion;
                            obj2.calificacion_ponderadac = 0;

                            Model.InsertarHistoriaClinicaDetalleConte(obj2, ref MsgRes);
                        }


                        return RedirectToAction("IngresoPacienteHCDetalle", "odontologia", new { id = Model.id_odont_historia_clinica, id2 = Model.id_odont_historia_clinica_paciente });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }


            return View(Model);
        }
        [HttpPost]
        public ActionResult PlandeMejora(Models.Odontologia.PlanMejora Model)
        {

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_regional != 0)
            {

            }
            else
            {
                variable2 = "REGIONAL";
                Conteo = Conteo + 1;
            }


            if (Model.id_unis != 0)
            {

            }
            else
            {
                variable2 = "UNIS";
                Conteo = Conteo + 1;
            }

            if (Model.especialista != null)
            {

            }
            else
            {
                variable2 = "ESPECIALISTA";
                Conteo = Conteo + 1;
            }


            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                odont_plan_mejora obj = new odont_plan_mejora();

                obj.id_regional = Model.id_regional;
                obj.id_unis = Model.id_unis;
                obj.especialista = Model.especialista;
                obj.estado = 0;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.id_odont_plan_mejora = Model.InsertarPlanMejora(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("PlandeMejoraDetalle", "odontologia", new { id = Model.id_odont_plan_mejora });
                }
                else
                {
                    ModelState.AddModelError("", "ERROR AL INGRESO!");
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult IngresoPlanMejoraBen(Models.Odontologia.PlanMejora Model)
        {
            odont_plan_mejora_beneficiario obj = new odont_plan_mejora_beneficiario();

            obj.documento_beneficiario = Convert.ToInt32(Model.documento_identidad);
            obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
            obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

            Model.id_odont_plan_mejora_beneficiario = Model.InsertarPlanMejoraBeneficiario(obj, ref MsgRes);


            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("PlandeMejoraBeneficiario", "odontologia", new { id = Model.id_odont_plan_mejora_beneficiario });
            }
            else
            {
                ModelState.AddModelError("", "ERROR AL INGRESO!");
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult ProtesisRemovibles(Models.Odontologia.ProtesisRemovible Model)
        {
            return RedirectToAction("IngresoProtesisRemovibles", "odontologia", new { valor = Model.numero_protesis });
        }

        [HttpPost]
        public ActionResult IngresoProtesisRemovibles(Models.Odontologia.ProtesisRemovible Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_regional != 0)
            {

            }
            else
            {
                variable2 = "REGIONAL";
                Conteo = Conteo + 1;
            }


            if (Model.id_unis != 0)
            {

            }
            else
            {
                variable2 = "UNIS";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                odont_rehabilitacion_oral_protesis_removibles OBJ = new odont_rehabilitacion_oral_protesis_removibles();

                OBJ.id_regional = Model.id_regional;
                OBJ.id_unis = Model.id_unis;
                OBJ.id_localidad = Model.id_localidad;
                OBJ.documento = Model.documento;
                OBJ.nombre = Model.nombre;
                OBJ.edad = Model.edad;
                OBJ.tiempo_tratamiento = Model.tiempo_tratamiento;
                OBJ.paciente_satisfecho = Model.paciente_satisfecho;
                OBJ.especialista_tratante = Model.especialista_tratante;
                OBJ.colaboracion_paciente = Model.colaboracion_paciente;
                OBJ.ppe_quien_realiza = Model.ppe_quien_realiza;
                OBJ.se_realizo_en_los_tiempos = Model.se_realizo_en_los_tiempos;
                OBJ.numero_protesis = Model.numero_protesis;
                OBJ.tiempo_protesis_aterior = Model.tiempo_protesis_aterior.ToString();
                OBJ.protesis = Model.protesis;
                OBJ.hc_tratamiento_realizado = Model.hc_tratamiento_realizado;
                OBJ.sobredentadura = Model.sobredentadura;
                OBJ.observaciones = Model.observaciones;
                OBJ.fecha_digita = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_digita = Convert.ToString(SesionVar.UserName);

                OBJ.id_rehabilitacion_oral_protesis_removibles = Model.InsertarOdontRemovible(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {


                    if (!string.IsNullOrEmpty(Model.detalle))
                    {
                        string[] detalle = Model.detalle.Split(',');
                        for (int i = 0; i < detalle.Count(); i++)
                        {
                            ProtesisRemovibleDtll obj2 = new ProtesisRemovibleDtll();

                            obj2.id_ref_tratamiento_rem = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.id_ref_paramatros_rem = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.estabilidad = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.funcion_oclusal = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.terminado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.diseño = Convert.ToInt32(detalle[i]);

                            Model.lista_detalle.Add(obj2);
                        }
                    }


                    foreach (Models.Odontologia.ProtesisRemovibleDtll item in Model.lista_detalle)
                    {
                        odont_rehabilitacion_oral_protesis_removibles_dtll dtll = new odont_rehabilitacion_oral_protesis_removibles_dtll();

                        dtll.id_rehabilitacion_oral_protesis_removibles = OBJ.id_rehabilitacion_oral_protesis_removibles;
                        dtll.id_ref_tratamiento_rem = item.id_ref_tratamiento_rem;
                        dtll.id_ref_paramatros_rem = item.id_ref_paramatros_rem;
                        dtll.estabilidad = item.estabilidad;
                        dtll.funcion_oclusal = item.funcion_oclusal;
                        dtll.terminado = item.terminado;
                        dtll.diseño = item.diseño;

                        Model.InsertarOdontRemovibledtll(dtll, ref MsgRes);

                    }
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        // CrearPdfOdontRemovible("CartaProtesisRemovibles" + OBJ.id_rehabilitacion_oral_protesis_removibles, OBJ.id_rehabilitacion_oral_protesis_removibles);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);


            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult IngresoEndodoncia(Models.Odontologia.Endodoncia Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_regional != 0)
            {

            }
            else
            {
                variable2 = "REGIONAL";
                Conteo = Conteo + 1;
            }


            if (Model.id_unis != 0)
            {

            }
            else
            {
                variable2 = "UNIS";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                odont_tratamiento_endodoncia OBJ = new odont_tratamiento_endodoncia();

                OBJ.id_regional = Model.id_regional;
                OBJ.id_unis = Model.id_unis;
                OBJ.id_localidad = Model.id_localidad;
                OBJ.documento = Model.documento;
                OBJ.nombre = Model.nombre;
                OBJ.edad = Model.edad;
                OBJ.diente_evaluado = Model.diente_evaluado;
                OBJ.endodoncista_tratante = Model.endodoncista_tratante;
                OBJ.tipo_endodoncia = Model.tipo_endodoncia;
                OBJ.odontologo_ppe = Model.odontologo_ppe;
                OBJ.calidad_tto_hc = Model.calidad_tto_hc;
                OBJ.rehabilitacion_oral = Model.rehabilitacion_oral;
                OBJ.retratamiento_antes_12_meses = Model.retratamiento_antes_12_meses;
                OBJ.observaciones = Model.observaciones;
                OBJ.fecha_digita = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_digita = Convert.ToString(SesionVar.UserName);

                OBJ.id_tratamiento_endodoncia = Model.InsertarOdontEndodoncia(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    odont_tratamiento_endodoncia_dtll obj2 = new odont_tratamiento_endodoncia_dtll();

                    obj2.id_tratamiento_endodoncia = OBJ.id_tratamiento_endodoncia;
                    obj2.check1 = Model.check1;
                    obj2.check2 = Model.check2;
                    obj2.check3 = Model.check3;
                    obj2.check4 = Model.check4;
                    obj2.check5 = Model.check5;

                    Model.InsertarOdontEndodonciadtll(obj2, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        CrearPdfOdontEndodoncia("CartaEndodoncia" + OBJ.id_tratamiento_endodoncia, OBJ.id_tratamiento_endodoncia);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult ListaBeneficiario(String id, Models.Odontologia.ortodoncia Model)
        {
            var List = Model.ListaBeneficiarios(id);
            VW_base_beneficiarios OBJ = new VW_base_beneficiarios();
            if (List.Count() > 0)
            {

                foreach (var item in List)
                {
                    OBJ.Nombre = Model.nombre;
                    OBJ.edad = item.edad;
                }

                return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);

            }
            else
            {

                OBJ.Nombre = "";
                OBJ.edad = "";

                return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult IngresoHistoriaClinica(Models.Odontologia.HistoriaClinica Model)
        {
            List<odont_historia_clinica> List = new List<odont_historia_clinica>();

            List = Model.GetHistoriaClinica();
            List = List.Where(x => x.id_unis == Model.id_unis && x.mes == Model.mes && x.año == Model.año).ToList();
            List = List.Where(x => x.nom_odontologo_auditado == Model.nom_odontologo_auditado).ToList();
            if (List.Count == 0)
            {
                odont_historia_clinica obj = new odont_historia_clinica();

                obj.año = Model.año;
                obj.mes = Model.mes;
                obj.id_regional = Model.id_regional;
                obj.id_unis = Model.id_unis;
                obj.nom_odontologo_auditado = Model.nom_odontologo_auditado;
                obj.nom_odontologo_auditor = Convert.ToString(SesionVar.UserName);
                obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                obj.usuario_digita = Convert.ToString(SesionVar.UserName);

                Model.id_odont_historia_clinica = Model.InsertarHistoriaClinica(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return RedirectToAction("IngresoPacienteHC", "odontologia", new { id = Model.id_odont_historia_clinica });
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                }
            }
            else
            {
                foreach (var item in List)
                {
                    Model.id_odont_historia_clinica = item.id_odont_historia_clinica;

                    return RedirectToAction("IngresoDetalleHistoriaClinica", "odontologia", new { id = Model.id_odont_historia_clinica });
                }


            }

            return View(Model);
        }

        public JsonResult SaveOrtodoncia(Models.Odontologia.ortodoncia Model)
        {
            String mensaje = "";

            try
            {
                odont_tratamiento_ortodoncia OBJ = new odont_tratamiento_ortodoncia();

                OBJ.año = Model.ano;
                OBJ.mesIngresado = Model.mesIngresado;
                OBJ.id_regional = Model.id_regional;
                OBJ.id_unis = Model.id_unis;
                OBJ.id_localidad = Model.id_localidad;
                OBJ.documento_identidad = Model.documento_identidad;
                OBJ.nombre = Model.nombre;
                OBJ.edad = Model.edad;
                OBJ.tiempo_tratamiento = Model.tiempo_tratamiento;
                OBJ.ortodoncista_tratante = Model.ortodoncista_tratante;
                if (Model.colaboracion_paciente == "1")
                {
                    OBJ.colaboracion_paciente = "SI";
                }
                else
                {
                    OBJ.colaboracion_paciente = "NO";

                }

                OBJ.ppe_quien_realiza = Model.ppe_quien_realiza;

                if (Model.paciente_satisfecho == "1")
                {
                    OBJ.paciente_satisfecho = "SI";
                }
                else
                {
                    OBJ.paciente_satisfecho = "NO";

                }

                if (Model.ortodoncia_primera_vez == "1")
                {
                    OBJ.ortodoncia_primera_vez = "SI";
                }
                else
                {
                    OBJ.ortodoncia_primera_vez = "NO";

                }

                if (Model.verifica_calidad_tto_hc == "1")
                {
                    OBJ.verifica_calidad_tto_hc = "SI";
                }
                else
                {
                    OBJ.verifica_calidad_tto_hc = "NO";

                }

                if (Model.caso_trasferencia == "1")
                {
                    OBJ.caso_trasferencia = "SI";
                    OBJ.donde_trasferencia = Model.donde_trasferencia;
                }
                else
                {
                    OBJ.caso_trasferencia = "NO";
                    OBJ.donde_trasferencia = "NA";

                }


                OBJ.observaciones = Model.observaciones;
                OBJ.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_digita = Convert.ToString(SesionVar.UserName);

                OBJ.id_tratamiento_ortodoncia = Model.InsertarOdontOrtodoncia(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    odont_tratamiento_ortodoncia_detalle obj2 = new odont_tratamiento_ortodoncia_detalle();

                    obj2.id_tratamiento_ortodoncia = OBJ.id_tratamiento_ortodoncia;
                    obj2.check1 = Model.check1;
                    obj2.check2 = Model.check2;
                    obj2.check3 = Model.check3;
                    obj2.check4 = Model.check4;
                    obj2.check5 = Model.check5;
                    obj2.check6 = Model.check6;
                    obj2.check7 = Model.check7;
                    obj2.check8 = Model.check8;
                    obj2.check9 = Model.check9;

                    Model.InsertarOdontOrtodonciaDetalle(obj2, ref MsgRes);

                    //if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    //{
                    // CrearPdfOdontOrtodoncia("CartaOrtodoncia" + OBJ.id_tratamiento_ortodoncia, OBJ.id_tratamiento_ortodoncia);
                    //}
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = OBJ.id_tratamiento_ortodoncia }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;

                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {

                    mensaje = "ERROR EL INGRESO DEL DETALLE." + MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                return Json(false);
            }

        }

        public JsonResult SaveFija(Models.Odontologia.ProtesisFija Model)
        {
            String mensaje = "";
            try
            {
                //if (Model.alerta == "NO")
                //{
                if (!string.IsNullOrEmpty(Model.detalle_dientes_uno))
                {
                    string[] detalle = Model.detalle_dientes_uno.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (detalle.Count() > 0)
                    {
                        for (int i = 0; i < detalle.Count(); i++)
                        {
                            Models.Odontologia.ProtesisFijaDtll dtll = new Models.Odontologia.ProtesisFijaDtll();
                            dtll.num_diente = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.parametro_auditado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.terminado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.funcion = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.pertinencia_del_tto_realizado = Convert.ToInt32(detalle[i]);

                            Model.lista_detalle_dientes.Add(dtll);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(Model.detalle_dientes_dos))
                {
                    string[] detalle = Model.detalle_dientes_dos.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


                    if (detalle.Count() > 0)
                    {
                        for (int i = 0; i < detalle.Count(); i++)
                        {
                            Models.Odontologia.ProtesisFijaDtll dtll = new Models.Odontologia.ProtesisFijaDtll();
                            dtll.num_diente = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.parametro_auditado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.terminado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.funcion = Convert.ToInt32(detalle[i]);
                            i += 1;
                            dtll.pertinencia_del_tto_realizado = Convert.ToInt32(detalle[i]);
                            Model.lista_detalle_dientes.Add(dtll);
                        }
                    }
                }


                odont_rehabilitacion_oral_protesis_fija obj = new odont_rehabilitacion_oral_protesis_fija();
                obj.año = Model.ano;
                obj.mesIngresado = Model.mesIngresado;
                obj.id_regional = Model.id_regional;
                obj.id_unis = Model.id_unis;
                obj.id_localidad = Model.id_localidad;
                obj.documento = Model.documento;
                obj.nombre = Model.nombre;
                obj.edad = Model.edad;
                obj.tiempo_tratamiento = Model.tiempo_tratamiento;
                if (Model.paciente_satisfecho == "1")
                {
                    obj.paciente_satisfecho = "SI";
                }
                else
                {
                    obj.paciente_satisfecho = "NO";
                }

                obj.especialista_tratante = Model.especialista_tratante;

                if (Model.colaboracion_paciente == "1")
                {
                    obj.colaboracion_paciente = "SI";
                }
                else
                {
                    obj.colaboracion_paciente = "NO";
                }

                obj.ppe_quien_realiza = Model.ppe_quien_realiza;

                if (Model.se_realizo_en_los_tiempos == "1")
                {
                    obj.se_realizo_en_los_tiempos = "SI";
                }
                else
                {
                    obj.se_realizo_en_los_tiempos = "NO";
                }

                obj.protesis = Model.protesis;
                obj.tiempo_protesis_aterior = Model.tiempo_protesis_aterior;

                if (Model.hc_tratamiento_realizado == "1")
                {
                    obj.hc_tratamiento_realizado = "SI";
                }
                else
                {
                    obj.hc_tratamiento_realizado = "NO";
                }

                if (Model.terminado_protesis_fija_implante == "1")
                {
                    obj.terminado_protesis_fija_implante = "SI";
                }
                else
                {
                    obj.terminado_protesis_fija_implante = "NO";
                }

                obj.observaciones = Model.observaciones;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                obj.id_rehabilitacion_oral_protesis_fija = Model.InsertarOdontFija(obj, ref MsgRes);
                Model.id_rehabilitacion_oral_protesis_fija = obj.id_rehabilitacion_oral_protesis_fija;

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    List<odont_rehabilitacion_oral_protesis_fija_dtll> detalle = new List<odont_rehabilitacion_oral_protesis_fija_dtll>();

                    if (Model.lista_detalle_dientes.Count() > 0)
                    {
                        foreach (Models.Odontologia.ProtesisFijaDtll item in Model.lista_detalle_dientes)
                        {
                            odont_rehabilitacion_oral_protesis_fija_dtll dtll = new odont_rehabilitacion_oral_protesis_fija_dtll();
                            dtll.id_odont_rehabilitacion_oral_protesis_fija = obj.id_rehabilitacion_oral_protesis_fija;
                            dtll.num_diente = item.num_diente;
                            dtll.parametro_auditado = item.parametro_auditado;
                            dtll.terminado = item.terminado;
                            dtll.funcion = item.funcion;
                            dtll.pertinenca_del_tto_realizado = item.pertinencia_del_tto_realizado;
                            detalle.Add(dtll);
                        }

                        Model.InsertarOdontFijaDtll(detalle, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "SE INGRESÓ CORRECTAMENTE";
                            return Json(new { success = true, message = mensaje, ID = obj.id_rehabilitacion_oral_protesis_fija }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mensaje = "ERROR EL INGRESO DEL DETALLE.";
                            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL TRATAMIENTO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                //}
                //else
                //{
                //    mensaje = "ERROR: LOS PORCENTAJES DE CALIFICACIÓN NO PUEDEN IR EN CERO CUANDO SELECCIONA UN PARÁMETRO PARA AUDITAR";
                //    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(false);
            }

        }

        public JsonResult SaveRemovible(Models.Odontologia.ProtesisRemovible Model)
        {
            String mensaje = "";

            try
            {
                odont_rehabilitacion_oral_protesis_removibles OBJ = new odont_rehabilitacion_oral_protesis_removibles();

                OBJ.año = Model.ano;
                OBJ.mesIngresado = Model.mesIngresado;
                OBJ.id_regional = Model.id_regional;
                OBJ.id_unis = Model.id_unis;
                OBJ.id_localidad = Model.id_localidad;
                OBJ.documento = Model.documento;
                OBJ.nombre = Model.nombre;
                OBJ.edad = Model.edad;
                OBJ.tiempo_tratamiento = Model.tiempo_tratamiento;

                if (Model.paciente_satisfecho == "1")
                {
                    OBJ.paciente_satisfecho = "SI";
                }
                else
                {
                    OBJ.paciente_satisfecho = "NO";
                }


                OBJ.especialista_tratante = Model.especialista_tratante;
                if (Model.colaboracion_paciente == "1")
                {
                    OBJ.colaboracion_paciente = "SI";
                }
                else
                {
                    OBJ.colaboracion_paciente = "NO";
                }

                OBJ.ppe_quien_realiza = Model.ppe_quien_realiza;

                if (Model.se_realizo_en_los_tiempos == "1")
                {
                    OBJ.se_realizo_en_los_tiempos = "SI";
                }
                else
                {
                    OBJ.se_realizo_en_los_tiempos = "NO";
                }

                OBJ.numero_protesis = Model.numero_protesis;
                OBJ.protesis = Model.protesis;
                OBJ.tiempo_protesis_aterior = Model.tiempo_protesis_aterior.ToString();
                if (Model.hc_tratamiento_realizado == "1")
                {
                    OBJ.hc_tratamiento_realizado = "SI";
                }

                else
                {
                    OBJ.hc_tratamiento_realizado = "NO";
                }

                if (Model.sobredentadura == "1")
                {
                    OBJ.sobredentadura = "SI";
                }
                else
                {
                    OBJ.sobredentadura = "NO";
                }

                OBJ.observaciones = Model.observaciones;
                OBJ.fecha_digita = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_digita = Convert.ToString(SesionVar.UserName);

                OBJ.id_rehabilitacion_oral_protesis_removibles = Model.InsertarOdontRemovible(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    if (!string.IsNullOrEmpty(Model.detalle))
                    {
                        string[] detalle = Model.detalle.Split(',');
                        for (int i = 0; i < detalle.Count(); i++)
                        {
                            ProtesisRemovibleDtll obj2 = new ProtesisRemovibleDtll();

                            obj2.id_ref_tratamiento_rem = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.id_ref_paramatros_rem = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.estabilidad = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.funcion_oclusal = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.terminado = Convert.ToInt32(detalle[i]);
                            i += 1;
                            obj2.diseño = Convert.ToInt32(detalle[i]);

                            Model.lista_detalle.Add(obj2);
                        }
                    }

                    foreach (Models.Odontologia.ProtesisRemovibleDtll item in Model.lista_detalle)
                    {
                        odont_rehabilitacion_oral_protesis_removibles_dtll dtll = new odont_rehabilitacion_oral_protesis_removibles_dtll();

                        dtll.id_rehabilitacion_oral_protesis_removibles = OBJ.id_rehabilitacion_oral_protesis_removibles;
                        dtll.id_ref_tratamiento_rem = item.id_ref_tratamiento_rem;
                        dtll.id_ref_paramatros_rem = item.id_ref_paramatros_rem;
                        dtll.estabilidad = item.estabilidad;
                        dtll.funcion_oclusal = item.funcion_oclusal;
                        dtll.terminado = item.terminado;
                        dtll.diseño = item.diseño;

                        Model.InsertarOdontRemovibledtll(dtll, ref MsgRes);

                    }
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = OBJ.id_rehabilitacion_oral_protesis_removibles }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(false);
            }

        }

        public JsonResult SaveEndodoncia(Models.Odontologia.Endodoncia Model)
        {

            String mensaje = "";
            ref_odontologia_protesisfija_dientes dienteDato = new ref_odontologia_protesisfija_dientes();

            try
            {
                odont_tratamiento_endodoncia OBJ = new odont_tratamiento_endodoncia();
                var diente = Model.diente_evaluado;

                dienteDato = BusClass.TraerDienteId(diente);

                if (dienteDato == null)
                {
                    throw new Exception("ELIJA UN DIENTE VALIDO.");
                }

                OBJ.año = Model.ano;
                OBJ.mesIngresado = Model.mesIngresado;
                OBJ.id_regional = Model.id_regional;
                OBJ.id_unis = Model.id_unis;
                OBJ.id_localidad = Model.id_localidad;
                OBJ.documento = Model.documento;
                OBJ.nombre = Model.nombre;
                OBJ.edad = Model.edad;
                OBJ.diente_evaluado = Model.diente_evaluado;
                OBJ.endodoncista_tratante = Model.endodoncista_tratante;
                OBJ.tipo_endodoncia = Model.tipo_endodoncia;
                OBJ.odontologo_ppe = Model.odontologo_ppe;
                if (Model.calidad_tto_hc == "1")
                {
                    OBJ.calidad_tto_hc = "SI";
                }
                else
                {
                    OBJ.calidad_tto_hc = "NO";

                }

                if (Model.rehabilitacion_oral == "1")
                {
                    OBJ.rehabilitacion_oral = "SI";
                }
                else
                {
                    OBJ.rehabilitacion_oral = "NO";
                }


                if (Model.retratamiento_antes_12_meses == "1")
                {
                    OBJ.retratamiento_antes_12_meses = "SI";
                }
                else
                {
                    OBJ.retratamiento_antes_12_meses = "NO";
                }


                if (Model.retratamiento == "1")
                {
                    OBJ.retratamiento = "SI";
                }
                else
                {
                    OBJ.retratamiento = "NO";
                }






                OBJ.fecha_retratamiento = Model.fecha_retratamiento;

                if (Model.urgencia == "1")
                {
                    OBJ.urgencia = "SI";
                }
                else
                {
                    OBJ.urgencia = "NO";
                }




                OBJ.fecha_atencion = Model.fecha_atencion;
                OBJ.fecha_urgencia = Model.fecha_urgencia;
                OBJ.observaciones = Model.observaciones;
              
             
                OBJ.fecha_digita = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_digita = Convert.ToString(SesionVar.UserName);

                OBJ.id_tratamiento_endodoncia = Model.InsertarOdontEndodoncia(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    odont_tratamiento_endodoncia_dtll obj2 = new odont_tratamiento_endodoncia_dtll();

                    obj2.id_tratamiento_endodoncia = OBJ.id_tratamiento_endodoncia;
                    obj2.check1 = Model.check1;
                    obj2.check2 = Model.check2;
                    obj2.check3 = Model.check3;
                    obj2.check4 = Model.check4;
                    obj2.check5 = Model.check5;

                    Model.InsertarOdontEndodonciadtll(obj2, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje, ID = OBJ.id_tratamiento_endodoncia }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO: " + error;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SavePrestadorPlanM(Models.Odontologia.PlanMejora Model)
        {

            String mensaje = "";

            try
            {
                odont_plan_mejora obj = new odont_plan_mejora();

                obj.año = Model.ano;
                obj.mesIngresado = Model.mesIngresado;
                obj.id_regional = Model.id_regional;
                obj.id_unis = Model.id_unis;
                obj.especialista = Model.especialista;
                obj.estado = 0;
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.id_odont_plan_mejora = Model.InsertarPlanMejora(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, ID = Model.id_odont_plan_mejora }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                return Json(false);
            }
        }

        public JsonResult SavePrestador(Models.Odontologia.ProtesisFija Model)
        {
            String mensaje = "";

            try
            {
                List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

                List1 = Model.GetPrestadoresOdont();
                List1 = List1.Where(x => x.ID_Prestador == Convert.ToDecimal(Model.documento)).ToList();
                List1 = List1.Where(x => x.Especialidad == Model.especialista_tratante).ToList();

                if (List1.Count != 0)
                {
                    mensaje = "ERROR! PRESTADOR Y ESPECIALIDAD YA EXISTE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Ref_odont_prestadores obj = new Ref_odont_prestadores();

                    obj.regional = Model.id_regional;
                    obj.ID_Prestador = Convert.ToDecimal(Model.documento);
                    obj.Razón_Social = Model.nombre;
                    obj.Nombre_Municipio = Model.Nombre_Municipio;
                    obj.Especialidad = Model.especialista_tratante;
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Model.InsertarprestadorOdontologia(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, message = error }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveIngresoHistoriaClinica(Models.Odontologia.HistoriaClinica Model)
        {
            String mensaje = "";


            List<odont_historia_clinica> List = new List<odont_historia_clinica>();

            List = Model.GetHistoriaClinica();
            List = List.Where(x => x.id_unis == Model.id_unis && x.mes == Model.mes && x.año == Model.año).ToList();
            List = List.Where(x => x.nom_odontologo_auditado == Model.nom_odontologo_auditado).ToList();

            if (List.Count == 0)
            {

                try
                {
                    odont_historia_clinica obj = new odont_historia_clinica();

                    obj.año = Model.año;
                    obj.mes = Model.mes;
                    obj.id_regional = Model.id_regional;
                    obj.id_unis = Model.id_unis;
                    obj.nom_odontologo_auditado = Model.nom_odontologo_auditado;
                    obj.nom_odontologo_auditor = Convert.ToString(SesionVar.UserName);
                    obj.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_digita = Convert.ToString(SesionVar.UserName);

                    Model.id_odont_historia_clinica = Model.InsertarHistoriaClinica(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje, ID = Model.id_odont_historia_clinica }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception)
                {

                    return Json(false);
                }

            }
            else
            {
                String Validacion = "";
                mensaje = "";
                Validacion = "SI";
                foreach (var item in List)
                {
                    Model.id_odont_historia_clinica = item.id_odont_historia_clinica;
                }
                return Json(new { success = true, message = mensaje, valida = Validacion, ID = Model.id_odont_historia_clinica }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult SaveIngresoPacienteHC(Models.Odontologia.HistoriaClinica Model)
        {
            String mensaje = "";
            try
            {
                odont_historia_clinica_paciente OBJ = new odont_historia_clinica_paciente();

                OBJ.id_odont_historia_clinica = Model.id_odont_historia_clinica;
                OBJ.numero_hc = Model.numero_hc;
                OBJ.paciente = Model.paciente;
                OBJ.fecha_atencion = Model.fecha_atencion;
                OBJ.estado = 1;

                Model.id_odont_historia_clinica_paciente = Model.InsertarHistoriaClinicaPaciente(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    List<Ref_odont_hc_calidad_formal> List1 = new List<Ref_odont_hc_calidad_formal>();

                    List1 = Model.GetHistoriaClinicaRefCalidadFormal();

                    odont_historia_clinica_detalle_calidad obj1 = new odont_historia_clinica_detalle_calidad();
                    foreach (var item in List1)
                    {
                        obj1.id_odont_historia_clinica_paciente = Model.id_odont_historia_clinica_paciente;
                        obj1.id_ref_odont_hc_calidad_formal = item.id_ref_odont_hc_calidad_formal;
                        obj1.calificacionf = 0;
                        obj1.ponderacionf = item.Ponderacion;
                        obj1.calificacion_ponderadaf = 0;

                        Model.InsertarHistoriaClinicaDetalle(obj1, ref MsgRes);
                    }

                    List<Ref_odont_hc_calidad_contenido> List2 = new List<Ref_odont_hc_calidad_contenido>();

                    List2 = Model.GetHistoriaClinicaRefContenido();

                    odont_historia_clinica_detalle_contenido obj2 = new odont_historia_clinica_detalle_contenido();
                    foreach (var item in List2)
                    {
                        obj2.id_odont_historia_clinica_paciente = Model.id_odont_historia_clinica_paciente;
                        obj2.id_ref_odont_hc_calidad_contenido = item.id_ref_odont_hc_calidad_contenido;
                        obj2.calificacionc = 0;
                        obj2.ponderacionc = item.Ponderacion;
                        obj2.calificacion_ponderadac = 0;

                        Model.InsertarHistoriaClinicaDetalleConte(obj2, ref MsgRes);
                    }

                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    return Json(new { success = true, message = mensaje, ID = Model.id_odont_historia_clinica, ID2 = Model.id_odont_historia_clinica_paciente }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR EL INGRESO DEL DETALLE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                return Json(false);
            }
        }

        public JsonResult GetCascadeUnis(string regional, Models.Odontologia.ortodoncia Model)
        {
            Model.ConsultaLista(1, regional, ref MsgRes);

            return Json(Model.Odont_unis.Select(p => new { id_ref_unis = p.id_ref_unis, nombre = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeLocalidades(string unis, Models.Odontologia.ortodoncia Model)
        {
            Model.ConsultaLocalidades(1, unis, ref MsgRes);
            return Json(Model.Ciudades.Select(p => new { id_ref_ciudades = p.id_ref_ciudades, nombre_localidad = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEspecialistasTotal(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

            List1 = Model.GetPrestadoresOdont();
            List1 = List1.Where(x => x.regional == Convert.ToInt32(regional)).ToList();

            return Json(List1.Select(p => new { id_ref_odont_prestadores = p.id_ref_odont_prestadores, Razón_Social = p.Razón_Social }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEspecialistasOdont(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

            List1 = Model.GetPrestadoresOdont();
            List1 = List1.Where(x => x.regional == Convert.ToInt32(regional)).ToList();

            return Json(List1.Select(p => new { id_ref_odont_prestadores = p.id_ref_odont_prestadores, Razón_Social = p.Razón_Social }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeOrtodoncista(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

            List1 = Model.GetPrestadoresOdont();
            List1 = List1.Where(x => x.regional == Convert.ToInt32(regional) && x.Especialidad == "ORTODONCIA").ToList();

            return Json(List1.Select(p => new { id_ref_odont_prestadores = p.id_ref_odont_prestadores, Razón_Social = p.Razón_Social }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeendodoncista(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

            List1 = Model.GetPrestadoresOdont();
            List1 = List1.Where(x => x.regional == Convert.ToInt32(regional) && x.Especialidad == "ENDODONCIA").ToList();

            return Json(List1.Select(p => new { id_ref_odont_prestadores = p.id_ref_odont_prestadores, Razón_Social = p.Razón_Social }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeOdontologiaG(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

            List1 = Model.GetPrestadoresOdont();
            List1 = List1.Where(x => x.regional == Convert.ToInt32(regional) && x.Especialidad == "ODONTOLOGÍA GENERAL").ToList();

            return Json(List1.Select(p => new { id_ref_odont_prestadores = p.id_ref_odont_prestadores, Razón_Social = p.Razón_Social }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEspecialidadesTotal(string regional, Models.Odontologia.ortodoncia Model)
        {
            List<vw_prestadores_odontologiaUnificado> List1 = new List<vw_prestadores_odontologiaUnificado>();

            List1 = Model.GetPrestadoresOdonUnificados();

            return Json(List1.Select(p => new { Especialidad = p.Especialidad }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarReporteTratamientos(Int32? id, Int32 tipo, Models.Odontologia.ortodoncia Model)
        {
            string result = "";

            List<vw_reportesTratamientosUnificados> List1 = new List<vw_reportesTratamientosUnificados>();

            Model.ConsultaReportes();
            List1 = Model.ListReporteTratUnif;
            List1 = List1.Where(x => x.documento == tipo).ToList();
            List1 = List1.Where(x => x.tipo == Convert.ToInt32(id)).ToList();

            foreach (var item in List1)
            {
                result += "<tr>";
                result += "<td>" + item.id + "</td>";
                result += "<td>" + item.regional + "</td>";
                result += "<td>" + item.unis + "</td>";
                result += "<td>" + item.documento + "</td>";
                result += "<td>" + item.nombre + "</td>";
                if (item.tipo == 1)
                {
                    result += "<td><a href='" + Url.Action("CrearPdfOrtodoncia2", "odontologia", new { id = item.id }) + "'>Generar Informe</a></td>";
                }
                else if (item.tipo == 2)
                {
                    result += "<td><a href='" + Url.Action("CrearPdfOdontFija", "odontologia", new { id = item.id }) + "'>Generar Informe</a></td>";
                }
                else if (item.tipo == 3)
                {
                    result += "<td><a href='" + Url.Action("CrearPdfOdontRemovible", "odontologia", new { id = item.id }) + "'>Generar Informe</a></td>";
                }
                else if (item.tipo == 4)
                {
                    result += "<td><a href='" + Url.Action("CrearPdfOdontEndodoncia2", "odontologia", new { id = item.id }) + "'>Generar Informe</a></td>";
                }


                result += "</tr>";
            }

            return Json(result);
        }

        public JsonResult GetCascadeCiudades(string regional, Models.Facturacion.FacturaDevolucion Model)
        {
            if (regional != null)
            {
                Model.ConsultaLista(1, regional, ref MsgRes);
            }
            return Json(Model.ListCiudades.Select(p => new { id_ref_ciudades = p.id_ref_ciudades, nombre = p.nombre }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Buscar_Beneficiario()
        {


            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {

                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<VW_base_beneficiarios> beneficiarios = BusClass.GetBeneficiarios(term, ref MsgRes);
                    var lista = (from reg in beneficiarios
                                 select new
                                 {
                                     id = reg.id_base_beneficiarios,
                                     nit = reg.Numero_identificacion,
                                     nombre = reg.Nombre,
                                     edad = reg.edad,
                                     label = reg.Numero_identificacion,
                                 }).Distinct().OrderBy(f => f.label).Take(15);
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public void CrearPdfOrtodoncia2(Int32 id)
        {
            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "Rptortodoncia.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_ortodoncia_report> lst = new List<vw_odont_ortodoncia_report>();
            lst = Model.ConsultaIdReporteOrtodoncia(Convert.ToInt32(id), ref MsgRes);

            string filename = "CartaOrtodoncia" + lst.FirstOrDefault().id_tratamiento_ortodoncia;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetOdontOrtod", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        public void CrearPdfOdontFija(Int32 id)
        {
            //RUTA REPORTE
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptProtesisFija.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_fija_report> lst = new List<vw_odont_fija_report>();
            List<odont_rehabilitacion_oral_protesis_fija_dtll> lstdtll = new List<odont_rehabilitacion_oral_protesis_fija_dtll>();
            List<vw_odont_porcentaje_d1_fija> LstTotal1 = new List<vw_odont_porcentaje_d1_fija>();
            List<vw_odont_porcentaje_d2_fija> LstTotal2 = new List<vw_odont_porcentaje_d2_fija>();

            lst = Model.ConsultaIdReporteProtesisFija(Convert.ToInt32(id), ref MsgRes);
            lstdtll = Model.ConsultaIdReporteProtesisFijaDtll(Convert.ToInt32(id), ref MsgRes);
            lstdtll = lstdtll.OrderBy(x => x.id_detalle).ToList();
            LstTotal1 = Model.Getporcentaje_d1_fija(Convert.ToInt32(id), ref MsgRes);
            LstTotal2 = Model.Getporcentaje_d2_fija(Convert.ToInt32(id), ref MsgRes);


            string filename = "ReporteOdontFija" + lst.FirstOrDefault().id_rehabilitacion_oral_protesis_fija;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDataSet", lst);
            Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDtllDataSet", lstdtll.Where(l => l.num_diente >= 11 && l.num_diente <= 28).ToList());
            Microsoft.Reporting.WebForms.ReportDataSource rds3 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDtll2DataSet", lstdtll.Where(l => l.num_diente >= 31 && l.num_diente <= 48).ToList());
            Microsoft.Reporting.WebForms.ReportDataSource rds4 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaTotal1", LstTotal1);
            Microsoft.Reporting.WebForms.ReportDataSource rds5 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaTotal2", LstTotal2);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);
            viewer.LocalReport.DataSources.Add(rds2);
            viewer.LocalReport.DataSources.Add(rds3);
            viewer.LocalReport.DataSources.Add(rds4);
            viewer.LocalReport.DataSources.Add(rds5);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        public void CrearPdfOdontRemovible(Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptProtesisRemovible2.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_removible_report> lst = new List<vw_odont_removible_report>();
            List<vw_odont_reporte_removible_dtll> lstdtll = new List<vw_odont_reporte_removible_dtll>();


            lst = Model.ConsultaIdReporteRemovible(Convert.ToInt32(id), ref MsgRes);
            lstdtll = Model.ConsultaIdReporteProtesisRemovibleDtll(Convert.ToInt32(id), ref MsgRes);

            //ASIGNAICON  DATASET A REPORT
            string filename = "ReporteOdontRemovible" + lst.FirstOrDefault().id_rehabilitacion_oral_protesis_removibles;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRemovible", lst);
            Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisRemovibleDtllDataSet", lstdtll.ToList());


            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;


                }
            }

        }

        public void CrearPdfOdontEndodoncia2(Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptEndodoncia.rdlc");

            //CONEXION BD  PARA CARGAR DATASET


            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_endodoncia_report> lst = new List<vw_odont_endodoncia_report>();

            lst = Model.ConsultaIdReporteEndodoncia(Convert.ToInt32(id), ref MsgRes);

            //ASIGNAICON  DATASET A REPORT
            string filename = "CartaEndodoncia" + lst.FirstOrDefault().id_tratamiento_endodoncia;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetOdontEndodoncia", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();





                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;


                }
            }

        }

        private void CrearPdfOdontOrtodoncia(Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "Rptortodoncia.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_ortodoncia_report> lst = new List<vw_odont_ortodoncia_report>();

            lst = Model.ConsultaIdReporteOrtodoncia(Convert.ToInt32(id), ref MsgRes);
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetOdontOrtod", lst);

            string filename = "CartaOrtodoncia" + lst.FirstOrDefault().id_tratamiento_ortodoncia;

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        private void CrearPdfOdontEndodoncia(string fileName, Int32 id)
        {
            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptEndodoncia.rdlc");

            //CONEXION BD  PARA CARGAR DATASET
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_endodoncia_report> lst = new List<vw_odont_endodoncia_report>();

            lst = Model.ConsultaIdReporteEndodoncia(Convert.ToInt32(id), ref MsgRes);
            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetOdontEndodoncia", lst);

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    this.Response.Clear();
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("Content-disposition", "attachment; filename=" + fileName + ".pdf");
                    this.Response.BinaryWrite(pdfContent);
                    this.Response.End();


                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }

        public void CrearPdfOdontFijaok(Int32? id)
        {

            //RUTA REPORTE

            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptProtesisFija.rdlc");

            //CONEXION BD  PARA CARGAR DATASET

            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();

            List<vw_odont_fija_report> lst = new List<vw_odont_fija_report>();
            List<odont_rehabilitacion_oral_protesis_fija_dtll> lstdtll = new List<odont_rehabilitacion_oral_protesis_fija_dtll>();

            lst = Model.ConsultaIdReporteProtesisFija(Convert.ToInt32(id), ref MsgRes);

            string filename = "ReporteOdontFija" + lst.FirstOrDefault().id_rehabilitacion_oral_protesis_fija;

            //ASIGNAICON  DATASET A REPORT
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDataSet", lst);
            Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDtllDataSet", lstdtll.Where(l => l.num_diente >= 11 && l.num_diente <= 28).ToList());
            Microsoft.Reporting.WebForms.ReportDataSource rds3 = new Microsoft.Reporting.WebForms.ReportDataSource("OdontProtesisFijaDtll2DataSet", lstdtll.Where(l => l.num_diente >= 31 && l.num_diente <= 48).ToList());

            // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();





                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;


                }
            }



        }

        /*Alexis Quiñones*/
        public JsonResult GetCascadeAccionesMejoras(string accion_mejora, Models.Odontologia.PlanMejora Model)
        {
            Model.ConsultaAccionesmejoras();
            return Json(Model.LIstAccionesMejora.Select(p => new { id_ref_acciones_mejora = p.id_ref_acciones_mejora, descripcion = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeEstadoAccionesMejoras(string accion_mejora, Models.Odontologia.PlanMejora Model)
        {
            Model.ConsultaEstadoAccionesmejoras();
            return Json(Model.LIstEstadosAccionesMejora.Select(p => new { id_estado_accion_mejora = p.id_estado_accion_mejora, descripcion = p.descripcion }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get2(Int32? id, int? page, int? limit)
        {


            Models.Odontologia.PlanMejora Model = new PlanMejora();

            List<vw_odont_planes_mejora> Lista = new List<vw_odont_planes_mejora>();

            Lista = Model.GetPlanMejoraTradtll(id.Value, ref MsgRes);

            var query = Lista;
            List<vw_odont_planes_mejora> records = new List<vw_odont_planes_mejora>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get3(Int32? id, int? page, int? limit)
        {

            Models.Odontologia.PlanMejora Model = new PlanMejora();
            Model.id_tratamiento = id.Value;

            List<vw_odont_planes_mejora_ben> Lista = new List<vw_odont_planes_mejora_ben>();

            //Model.LlenaLista();
            Lista = Model.GetPlanMejoraBendtll(id.Value, ref MsgRes);

            var query = Lista;
            List<vw_odont_planes_mejora_ben> records = new List<vw_odont_planes_mejora_ben>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlanMejora(Int32? id, int? page, int? limit)
        {

            Models.Odontologia.PlanMejora Model = new PlanMejora();
            Model.id_tratamiento = id.Value;

            List<vw_odont_planes_mejora> Lista = new List<vw_odont_planes_mejora>();

            //Model.LlenaLista();
            Lista = Model.GetPlanMejoraTradtll(id.Value, ref MsgRes);

            var query = Lista;
            List<vw_odont_planes_mejora> records = new List<vw_odont_planes_mejora>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHCPaciente(Int32? id, int? page, int? limit)
        {

            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            List<odont_historia_clinica_paciente> Lista = new List<odont_historia_clinica_paciente>();

            Lista = Model.GetHistoriaClinicaPaciente(id.Value, ref MsgRes);
            Lista = Lista.Where(X => X.estado == 2).ToList();

            var query = Lista;
            List<odont_historia_clinica_paciente> records = new List<odont_historia_clinica_paciente>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHCPacienteTotalesPaciente(Int32? id, int? page, int? limit)
        {

            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            List<vw_totales_odont> Lista = new List<vw_totales_odont>();

            Lista = Model.GetTotalPaciente(id.Value, ref MsgRes);



            var query = Lista;
            List<vw_totales_odont> records = new List<vw_totales_odont>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHCPacienteTotales(Int32? id, int? page, int? limit)
        {

            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            List<vw_odont_totales_hc> Lista = new List<vw_odont_totales_hc>();

            Lista = Model.GetOdontogTotales(id.Value, ref MsgRes);


            var query = Lista;
            List<vw_odont_totales_hc> records = new List<vw_odont_totales_hc>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHCDetalle(int Id, int? page, int? limit)
        {
            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            List<vw_odont_historia_clinica_detalle> Lista = new List<vw_odont_historia_clinica_detalle>();

            Lista = Model.GetVWHistoriaClinicaDetalle(Id, ref MsgRes);

            var query = Lista;
            List<vw_odont_historia_clinica_detalle> records = new List<vw_odont_historia_clinica_detalle>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }


            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHCDetalle2(int Id, int? page, int? limit)
        {
            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            List<vw_odont_historia_clinica_detalle_contenido> Lista = new List<vw_odont_historia_clinica_detalle_contenido>();

            Lista = Model.GetVWHistoriaClinicaDetalleConten(Id, ref MsgRes);

            var query = Lista;
            List<vw_odont_historia_clinica_detalle_contenido> records = new List<vw_odont_historia_clinica_detalle_contenido>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }


            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save2(Int32 id_odont_plan_mejora_tratamiento, DateTime fecha_seguimiento, Int32 accion_mejora, String estado)
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (fecha_seguimiento != null)
            {

            }
            else
            {
                variable2 = "FECHA SEGUIMIENTO ";
                Conteo = Conteo + 1;
            }


            if (accion_mejora != 0)
            {

            }
            else
            {
                variable2 = "ACCION MEJORA";
                Conteo = Conteo + 1;
            }

            if (estado != "")
            {

            }
            else
            {
                variable2 = "ESTADO";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {

                odont_plan_mejora_tratamiento_dtll OBJ = new odont_plan_mejora_tratamiento_dtll();


                OBJ.id_odont_plan_mejora_tratamiento = id_odont_plan_mejora_tratamiento;
                OBJ.fecha_seguimiento = fecha_seguimiento;
                OBJ.accion_mejora = Convert.ToInt32(accion_mejora);
                OBJ.estado = Convert.ToString(estado);
                OBJ.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.InsertarPlanMejoraTratamientodetalle(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);

                return Json(new { result = false });
            }




        }

        public JsonResult Save4(Int32 id_odont_plan_mejora_beneficiario, DateTime fecha_seguimiento, Int32 accion_mejora, String estado)
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (fecha_seguimiento != null)
            {

            }
            else
            {
                variable2 = "FECHA SEGUIMIENTO ";
                Conteo = Conteo + 1;
            }


            if (accion_mejora != 0)
            {

            }
            else
            {
                variable2 = "ACCION MEJORA";
                Conteo = Conteo + 1;
            }

            if (estado != "")
            {

            }
            else
            {
                variable2 = "ESTADO";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {

                odont_plan_mejora_beneficiario_dtll OBJ = new odont_plan_mejora_beneficiario_dtll();


                OBJ.id_odont_plan_mejora_beneficiario = id_odont_plan_mejora_beneficiario;
                OBJ.fecha_seguimiento = fecha_seguimiento;
                OBJ.accion_mejora = Convert.ToInt32(accion_mejora);
                OBJ.estado = Convert.ToString(estado);
                OBJ.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.InsertarPlanMejoraBeneficiariodetalle(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);

                return Json(new { result = false });
            }




        }

        public JsonResult SavePlan(Int32 id_odont_plan_mejora, DateTime fecha_seguimiento, Int32 accion_mejora, String estado, String Observacion)
        {
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (fecha_seguimiento != null)
            {

            }
            else
            {
                variable2 = "FECHA SEGUIMIENTO ";
                Conteo = Conteo + 1;
            }


            if (accion_mejora != 0)
            {

            }
            else
            {
                variable2 = "ACCION MEJORA";
                Conteo = Conteo + 1;
            }

            if (estado != "")
            {

            }
            else
            {
                variable2 = "ESTADO";
                Conteo = Conteo + 1;
            }



            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {

                odont_plan_mejora_dtll OBJ = new odont_plan_mejora_dtll();


                OBJ.id_odont_plan_mejora = id_odont_plan_mejora;
                OBJ.fecha_seguimiento = fecha_seguimiento;
                OBJ.accion_mejora = Convert.ToInt32(accion_mejora);
                OBJ.estado = "NO CUMPLIDA";
                OBJ.observacion = Convert.ToString(Observacion);
                OBJ.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                OBJ.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                Model.InsertarPlanMejoradetalle(OBJ, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);

                return Json(new { result = false });
            }




        }

        [HttpPost]
        public JsonResult Save5(Int32 id_odont_plan_mejora_beneficiario, String Observacion)
        {
            Models.Odontologia.PlanMejora Model = new PlanMejora();

            Model.id_odont_plan_mejora_beneficiario = Convert.ToInt32(id_odont_plan_mejora_beneficiario);

            odont_plan_mejora_beneficiario objact = new odont_plan_mejora_beneficiario();

            objact.id_odont_plan_mejora_beneficiario = id_odont_plan_mejora_beneficiario;
            objact.estado = 1;
            objact.observacion_tratamiento = Observacion;
            objact.usuario_ingreso = Convert.ToString(SesionVar.UserName);

            Model.ActualizarOdontPlanMejoraObsBeneficiario(objact, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        [HttpPost]
        public JsonResult SaveDefPLan(Int32 id_odont_plan_mejora)
        {
            Models.Odontologia.PlanMejora Model = new PlanMejora();

            Model.id_odont_plan_mejora = Convert.ToInt32(id_odont_plan_mejora);

            odont_plan_mejora objact = new odont_plan_mejora();

            objact.id_odont_plan_mejora = id_odont_plan_mejora;
            objact.estado = 1;
            objact.usuario_ingreso = Convert.ToString(SesionVar.UserName);

            Model.ActualizarOdontPlanMejoraObsTratamiento(objact, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
        }


        [HttpPost]
        public JsonResult Update1(Models.Odontologia.PlanMejora record)
        {

            record.OBJDetallle.id_odont_plan_mejora_dtll = record.id_odont_plan_mejora_dtll;
            record.OBJDetallle.estado = record.estado;
            record.OBJDetallle.fecha_ingreso = Convert.ToDateTime(DateTime.Now);


            record.ActualizarOdontPlanMejora(ref MsgRes);


            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        [HttpPost]
        public JsonResult Update2(Models.Odontologia.PlanMejora record)
        {

            record.OBJDetallleB.id_odont_plan_mejora_beneficiario_dtll = record.id_odont_plan_mejora_beneficiario_dtll;
            record.OBJDetallleB.estado = record.estado;

            record.ActualizarOdontPlanMejoraBeneficiario(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        public JsonResult cierre1(Int32 id_odont_plan_mejora)
        {
            String mensaje = "";
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Model.habilita = "SI";
            List<vw_odont_planes_mejora> Lista = new List<vw_odont_planes_mejora>();
            Lista = Model.GetPlanMejoraTradtll(id_odont_plan_mejora, ref MsgRes);
            if (Lista.Count != 0)
            {
                foreach (var item in Lista)
                {
                    if (item.estado == "NO CUMPLIDA")
                    {
                        Model.habilita = "NO";
                    }
                }

                if (Model.habilita == "SI")
                {
                    odont_plan_mejora obj = new odont_plan_mejora();

                    obj.id_odont_plan_mejora = id_odont_plan_mejora;
                    obj.estado = 2;
                    obj.fecha_cierre = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_cierre = Convert.ToString(SesionVar.UserName);

                    Model.ActualizarOdontPlanMejoraCierreTrat(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR CERRANDO PLAN DE MEJORA. POR FAVOR INTÉNTELO DE NUEVO.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR... HAY ITEMS DEL PLAN DE MEJORA SIN CUMPLIR.";

                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                mensaje = "ERROR... DEBE SELECCIONAR  MÍNIMO UN PLAN DE MEJORA.";

                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult cierre2(Int32 id_odont_plan_mejora_beneficiario)
        {
            String mensaje = "";
            Models.Odontologia.PlanMejora Model = new Models.Odontologia.PlanMejora();

            Model.habilita = "SI";
            List<vw_odont_planes_mejora_ben> Lista = new List<vw_odont_planes_mejora_ben>();
            Lista = Model.GetPlanMejoraBendtll(id_odont_plan_mejora_beneficiario, ref MsgRes);

            if (Lista.Count != 0)
            {
                foreach (var item in Lista)
                {
                    if (item.estado == "NO CUMPLIDA")
                    {
                        Model.habilita = "NO";
                    }
                }

                if (Model.habilita == "SI")
                {
                    odont_plan_mejora_beneficiario obj = new odont_plan_mejora_beneficiario();

                    obj.id_odont_plan_mejora_beneficiario = id_odont_plan_mejora_beneficiario;
                    obj.estado = 2;
                    obj.fecha_cierre = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_cierre = Convert.ToString(SesionVar.UserName);

                    Model.ActualizarOdontPlanMejoraCierreBen(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESO CORRECTAMENTE....";

                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR CERRANDO PLAN DE MEJORA. POR FAVOR INTÉNTELO DE NUEVO.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensaje = "ERROR... HAY ITEMS DEL PLAN DE MEJORA SIN CUMPLIR.";

                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                mensaje = "ERROR... DEBE SELECCIONAR  MÍNIMO UN PLAN DE MEJORA.";

                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {

            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult Savedtll1(Models.Odontologia.HistoriaClinica record)
        {

            odont_historia_clinica_detalle_calidad obj = new odont_historia_clinica_detalle_calidad();

            obj.id_odont_historia_clinica_detalle = record.id_odont_historia_clinica_detalle;
            obj.calificacionf = record.calificacionf;
            obj.calificacion_ponderadaf = obj.calificacionf * record.ponderacionf;

            record.ActualizarOdontHCdtll1(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }

        }

        [HttpPost]

        public JsonResult Savedtll2(Models.Odontologia.HistoriaClinica record)
        {

            odont_historia_clinica_detalle_contenido obj = new odont_historia_clinica_detalle_contenido();

            obj.id_odont_historia_clinica_detalle_contenido = record.id_odont_historia_clinica_detalle_contenido;
            obj.calificacionc = record.calificacionc;
            obj.calificacion_ponderadac = obj.calificacionc * record.ponderacionc;

            record.ActualizarOdontHCdtll2(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }

        }

        [HttpPost]
        public JsonResult SaveHCPaciente(Int32 id_odont_historia_clinica, Int32 id_odont_historia_clinica_paciente, String observaciones)
        {
            String mensaje = "";

            Models.Odontologia.HistoriaClinica Model = new HistoriaClinica();

            odont_historia_clinica_paciente OBJ = new odont_historia_clinica_paciente();

            OBJ.id_odont_historia_clinica_paciente = id_odont_historia_clinica_paciente;
            OBJ.observaciones = observaciones;


            Model.ActualizarOdontHCdtllFinal(OBJ, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {

                mensaje = "SE INGRESO CORRECTAMENTE....";

                return Json(new { success = true, message = mensaje, ID = OBJ.id_odont_historia_clinica_paciente }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR INGRESE TODOS LOS CAMPOS OBLIGATORIOS.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }


        }


        /*Alexis Quiñones Castillo*/

        public ActionResult Consolidados()
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();

            ViewBag.auditores = BusClass.GetSisUsuarioOdont();
            ViewBag.tratamiento = from OdontTipoTratamiento d in Enum.GetValues(typeof(OdontTipoTratamiento))
                                  select new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = DescriptionEnums.DescriptionAttr(d)
                                  };

            return View(Model);
        }

        public ActionResult IngresoReferenciaOdontologica()
        {
            HistoriaClinica modelo = new HistoriaClinica();
            Session["remisiones"] = new List<odont_remisiones_especialidades>();
            ViewBag.regionales = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.historico = modelo.GetRemisiones(ref MsgRes).OrderByDescending(l => l.fecha_digita).ToList();
            ViewBag.historico2 = modelo.GetRemisionesVerificadas(ref MsgRes).OrderByDescending(l => l.fecha_digita).ToList();
            return View();
        }

        public void CrearPdfConsolidadoHC(int tipoconsolidado, int? regional, int? mes, int? año, int? idauditor)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            //RUTA REPORTE
            string rPath = "", FileName = "", rta = "";
            int count = 0;
            Microsoft.Reporting.WebForms.ReportDataSource rds = null;

            switch (tipoconsolidado)
            {
                case 1:
                    rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoHC.rdlc");
                    List<vw_odont_consolidado_hc> lst = Model.GetConsolidadoHc(regional, mes, año, ref MsgRes).OrderBy(l => l.Mes).ToList();
                    count = lst.Count;
                    FileName = "Consolidado Historia Clinica";

                    //CONEXION BD  PARA CARGAR DATASET
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("RptConsolidadoHCDataSet", lst);
                    FileName = NomConsolidado("ConsolidadoHC", regional, idauditor, mes, año);

                    break;
                case 2:
                    rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoHCporProfesional.rdlc");
                    List<vw_odont_consolidado_hc_prestador> lst2 = Model.GetConsolidadoHcporprestador(idauditor, regional, mes, año, ref MsgRes).OrderBy(l => l.Mes).ToList();
                    count = lst2.Count;

                    FileName = "Consolidado HC por Profesional";

                    //CONEXION BD  PARA CARGAR DATASET
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("RptConsolidadoHCProfesionalDataSet", lst2);
                    FileName = NomConsolidado("ConsolidadoHCporProfesional", regional, idauditor, mes, año);
                    break;
            }


            if (count > 0)
            {
                //ASIGNAICON  DATASET A REPORT

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);

                if (count != 0)
                {
                    //CONTROL EXCEPCION
                    try
                    {
                        viewer.LocalReport.Refresh();
                        //EXPORTAR PDF

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;
                        string filename;

                        byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        filename = string.Format("{0}.{1}", FileName, "xls");
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                        Response.ContentType = mimeType;
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                    }
                }
            }
            else
            {
                rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "window.location.href = '" + Url.Action("Consolidados", "odontologia") + "';" +
                "</script> ";

                Response.Write(rta);
            }

        }

        public void CrearPdfConsolidadottos(int tipotratamiento, int chktipoconsolidado, DateTime? fechainicial, DateTime? fechafinal,
         int? mesinicial_g, int? añoinicial_g, int? mesfinal_g, int? añofinal_g, int? mesinicial_p, int? añoinicial_p, int? mesfinal_p, int? añofinal_p,
         int? regional, int? regional_g, int? unis)
        {
            Models.Reportes.Reportes Model = new Models.Reportes.Reportes();
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            int count = 0;
            string nomregional = "", nomunis = "", año = "";
            String tratamiento = "";
            string rPath = "";
            if (chktipoconsolidado == 1)
            {
                #region 


                List<vw_odont_tableros_ortodoncia> TblOrtodoncia = new List<vw_odont_tableros_ortodoncia>();
                List<vw_odont_tableros_ProtesisFija> TblProtesisFija = new List<vw_odont_tableros_ProtesisFija>();
                List<vw_odont_tableros_ProtesisRemov> TblProtesisRemov = new List<vw_odont_tableros_ProtesisRemov>();
                List<vw_odont_tableros_endodoncia> TblEndodoncia = new List<vw_odont_tableros_endodoncia>();

                switch (tipotratamiento)
                {
                    case 1:
                        tratamiento = "Ortodoncia_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientos_V2.rdlc");
                        TblOrtodoncia = Model.ConsultaListadoTTOsOrodoncia(mesinicial_g, añoinicial_g, mesfinal_g, añofinal_g, regional_g, ref MsgRes);
                        count += TblOrtodoncia.Count;
                        break;
                    case 2:
                        tratamiento = "Protesis Fija_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientos_V3.rdlc");
                        TblProtesisFija = Model.ConsultaListadoTTOsPPF(mesinicial_g, añoinicial_g, mesfinal_g, añofinal_g, regional_g, ref MsgRes);
                        count += TblProtesisFija.Count;
                        break;
                    case 3:
                        tratamiento = "Protesis Removible_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientos_V4.rdlc");
                        TblProtesisRemov = Model.ConsultaListadoTTOsRemovible(mesinicial_g, añoinicial_g, mesfinal_g, añofinal_g, regional_g, ref MsgRes);
                        count += TblProtesisRemov.Count;
                        break;

                    case 4:
                        tratamiento = "Endodoncia_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientos_V5.rdlc");
                        TblEndodoncia = Model.ConsultaListadoTTOsEndodoncia(mesinicial_g, añoinicial_g, mesfinal_g, añofinal_g, regional_g, ref MsgRes);
                        count += TblEndodoncia.Count;
                        break;
                }

                Microsoft.Reporting.WebForms.ReportDataSource lstortodoncia = new Microsoft.Reporting.WebForms.ReportDataSource("TableroGeneralOrtodonciaDataSet", TblOrtodoncia);
                Microsoft.Reporting.WebForms.ReportDataSource lstppfija = new Microsoft.Reporting.WebForms.ReportDataSource("TableroGeneralPPFDataSet", TblProtesisFija);
                Microsoft.Reporting.WebForms.ReportDataSource lstpremovible = new Microsoft.Reporting.WebForms.ReportDataSource("TableroGeneralPRemovibleDataSet", TblProtesisRemov);
                Microsoft.Reporting.WebForms.ReportDataSource lstendodoncia = new Microsoft.Reporting.WebForms.ReportDataSource("TableroGeneralEndodonciaDataSet", TblEndodoncia);

                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                //viewer.LocalReport.SetParameters(p1);
                //viewer.LocalReport.SetParameters(p2);

                viewer.LocalReport.DataSources.Add(lstortodoncia);
                viewer.LocalReport.DataSources.Add(lstppfija);
                viewer.LocalReport.DataSources.Add(lstpremovible);
                viewer.LocalReport.DataSources.Add(lstendodoncia);

                #endregion
            }
            else
            {
                #region

                List<vw_odont_tableros_ortodoncia_prof> TblOrtodoncia = new List<vw_odont_tableros_ortodoncia_prof>();
                List<vw_odont_tableros_ProtesisFija_prof> TblProtesisFija = new List<vw_odont_tableros_ProtesisFija_prof>();
                List<vw_odont_tableros_ProtesisRemov_prof> TblProtesisRemov = new List<vw_odont_tableros_ProtesisRemov_prof>();
                List<vw_odont_tableros_endodoncia_prof> TblEndodoncia = new List<vw_odont_tableros_endodoncia_prof>();


                if (regional != null)
                {
                    nomregional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).FirstOrDefault().indice;
                }
                if (unis != null)
                {
                    nomunis = BusClass.Odont_unis().Where(l => l.id_ref_unis == unis).FirstOrDefault().descripcion;
                }

                //ReportParameter p1 = new ReportParameter("Nomregional", nomregional);
                //ReportParameter p2 = new ReportParameter("Nomunis", nomunis);
                //ReportParameter p3 = new ReportParameter("TipoTratamiento", tipotratamiento.ToString());
                //ReportParameter p4 = new ReportParameter("Año", año);

                //viewer.LocalReport.SetParameters(p1);
                //viewer.LocalReport.SetParameters(p2);
                //viewer.LocalReport.SetParameters(p3);
                //viewer.LocalReport.SetParameters(p4);

                switch (tipotratamiento)
                {
                    case 1:
                        tratamiento = "Ortodoncia_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientosPPFV2.rdlc");
                        TblOrtodoncia = Model.ConsultaListadoTTOsOrodonciaProf(regional, unis, mesinicial_p, añoinicial_p, mesfinal_p, añofinal_p, ref MsgRes);
                        count += TblOrtodoncia.Count;
                        break;
                    case 2:
                        tratamiento = "Protesis Fija_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientosPPFV3.rdlc");
                        TblProtesisFija = Model.ConsultaListadoTTOsProf(regional, unis, mesinicial_p, añoinicial_p, mesfinal_p, añofinal_p, ref MsgRes);
                        count += TblProtesisFija.Count;
                        break;
                    case 3:
                        tratamiento = "Protesis Removible_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientosPPFV4.rdlc");
                        TblProtesisRemov = Model.ConsultaListadoTTOsRemoviblesProf(regional, unis, mesinicial_p, añoinicial_p, mesfinal_p, añofinal_p, ref MsgRes);
                        count += TblProtesisRemov.Count;
                        break;
                    case 4:
                        tratamiento = "Endodoncia_" + DateTime.Now.ToString("dd-MM-yyyy");
                        rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptConsolidadoTratamientosPPFV5.rdlc");
                        TblEndodoncia = Model.ConsultaListadoEndodnciaProf(regional, unis, mesinicial_p, añoinicial_p, mesfinal_p, añofinal_p, ref MsgRes);
                        count += TblEndodoncia.Count;
                        break;
                }

                Microsoft.Reporting.WebForms.ReportDataSource lstortodoncia = new Microsoft.Reporting.WebForms.ReportDataSource("TableroGeneralOrtodPPFDataSet", TblOrtodoncia);
                Microsoft.Reporting.WebForms.ReportDataSource lstppfija2 = new Microsoft.Reporting.WebForms.ReportDataSource("TablerolProtesisFijaPPFDataSet", TblProtesisFija);
                Microsoft.Reporting.WebForms.ReportDataSource lstpremovible = new Microsoft.Reporting.WebForms.ReportDataSource("TablerolProtesisRemovPPFDataSet", TblProtesisRemov);
                Microsoft.Reporting.WebForms.ReportDataSource lstendodoncia = new Microsoft.Reporting.WebForms.ReportDataSource("TableroEndodonciaPPFDataSet", TblEndodoncia);

                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();

                viewer.LocalReport.DataSources.Add(lstortodoncia);
                viewer.LocalReport.DataSources.Add(lstppfija2);
                viewer.LocalReport.DataSources.Add(lstpremovible);
                viewer.LocalReport.DataSources.Add(lstendodoncia);
                #endregion

            }

            if (count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    string filename;

                    byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                    filename = string.Format("{0}.{1}", tratamiento, "xls");
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.ContentType = mimeType;
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;

                }

            }

            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "window.location.href = '" + Url.Action("Consolidados", "odontologia") + "';" +
                "</script> ";

                Response.Write(rta);
            }

        }

        public void CrearConsolidadoPlanMejora(int idregional)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            //RUTA REPORTE
            string rPath = "", FileName = "", rta = "";
            int count = 0;

            String regional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault().indice;

            rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptPlanMejora.rdlc");
            List<vw_odont_count_acciones_mejora> lst = Model.GetListCountAccionesMejora(idregional).ToList();
            List<vw_odont_count_planes_mejora> lst2 = Model.GetListCountPlanesMejora(idregional).ToList();
            count += lst.Count;
            count += lst2.Count;

            //CONEXION BD  PARA CARGAR DATASET
            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("ContadorAccionesMejoraDataSet", lst);
            Microsoft.Reporting.WebForms.ReportDataSource rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("ContadorPlanesMejoraDataSet", lst2);
            ReportParameter p1 = new ReportParameter("NomRegional", regional.ToString());

            if (count > 0)
            {
                //ASIGNAICON  DATASET A REPORT

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.DataSources.Add(rds2);
                viewer.LocalReport.SetParameters(p1);

                if (count != 0)
                {
                    //CONTROL EXCEPCION
                    try
                    {
                        viewer.LocalReport.Refresh();
                        //EXPORTAR PDF

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;

                        byte[] excelContent = viewer.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        // DEVOLVER EL ARCHIVO EXCEL COMO DESCARGA
                        Response.Clear();
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Consolidado_Planesdemejora.xls");
                        Response.BinaryWrite(excelContent);
                        Response.End();

                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                    }
                }
            }
            else
            {
                rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "window.location.href = '" + Url.Action("Consolidados", "odontologia") + "';" +
                "</script> ";

                Response.Write(rta);
            }

        }

        public string NomConsolidado(string nombre, int? idregional, int? idauditor, int? mes, int? año)
        {
            string result = nombre;

            if (idregional != null)
                result += "-" + idregional;
            if (idauditor != null)
                result += "-" + idauditor;
            if (mes != null)
                result += "-" + mes;
            if (año != null)
                result += "-" + año;

            return result;
        }

        public JsonResult GuardarRemisionesEspecialidades(int regional, int unis, int localidad, int profesional, int ortodoncia_una_vez, int o_otras_cuentas, int rehabilitacion_oral
            , int periodoncia, int endodoncia, int cirugia_oral, int ayudas_diagnosticas, int odontopediatria, int fonoaudiologia, int mes, int año)
        {
            HistoriaClinica modelo = new HistoriaClinica();
            odont_remisiones_especialidades obj = new odont_remisiones_especialidades();
            obj.id_regional = regional;
            obj.id_odontologo_ppe = profesional;
            obj.ortodoncia_primera_vez = ortodoncia_una_vez;
            obj.ortodoncia_otras_cuentas = o_otras_cuentas;
            obj.id_unis = unis;
            obj.id_localidad = localidad;
            obj.rehabilitacion_oral = rehabilitacion_oral;
            obj.periodoncia = periodoncia;
            obj.endodoncia = endodoncia;
            obj.cirugia_oral_maxilofacial = cirugia_oral;
            obj.ayudas_diagnosticas = ayudas_diagnosticas;
            obj.odontopediatria = odontopediatria;
            obj.fonoaudiologia = fonoaudiologia;
            obj.mes_ingresado = mes;
            obj.año = año;
            obj.fecha_digita = DateTime.Now;
            obj.usuario_digita = SesionVar.UserName;
            modelo.InsertarRemisionesEspecialidades(obj, ref MsgRes);

            string result = "";
            List<vw_odont_remisiones_especialidades> remisiones = modelo.GetRemisiones(ref MsgRes).OrderByDescending(l => l.fecha_digita).ToList();

            foreach (var item in remisiones)
            {
                result += "<tr>";
                result += "<td>" + item.Razón_Social + "</td>";
                result += "<td>" + item.nom_localidad + "</td>";
                result += "<td>" + item.ortodoncia_primera_vez + "</td>";
                result += "<td>" + item.ortodoncia_otras_cuentas + "</td>";
                result += "<td>" + item.rehabilitacion_oral + "</td>";
                result += "<td>" + item.periodoncia + "</td>";
                result += "<td>" + item.endodoncia + "</td>";
                result += "<td>" + item.cirugia_oral_maxilofacial + "</td>";
                result += "<td>" + item.ayudas_diagnosticas + "</td>";
                result += "<td>" + item.odontopediatria + "</td>";
                result += "<td>" + item.fonoaudiologia + "</td>";
                result += "<td>" + item.mes_ingresado + "/" + item.año + "</td>";
                result += "</tr>";
            }

            return Json(result);
        }

        public void CrearConsolidadoRemisiones(int? regional, int? unis, int? localidad, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "RptRemisionesEspecialidades.rdlc");
            List<vw_odont_remisiones_especialidades> lst = Model.GetRemisiones(ref MsgRes);
            if (regional != null)
                lst = lst.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                lst = lst.Where(l => l.id_unis == unis).ToList();
            if (localidad != null)
                lst = lst.Where(l => l.id_localidad == localidad).ToList();
            if (mesinicial != null && añoinicial != null)
                lst = lst.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();
            if (mesfinal != null && mesfinal != null)
                lst = lst.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();


            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("RptResimionesEspecialidadesDataSet", lst);
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("EXCELOPENXML", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.AddHeader("Content-disposition", "attachment; filename=Consolidado_remisiones.xlsx");
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "window.location.href = '" + Url.Action("IngresoReferenciaOdontologica", "odontologia") + "';" +
                "</script> ";

                Response.Write(rta);
            }
        }

        public JsonResult GuardarRemisionesVerificacion(int mes, int año, int regional, int unis, int localidad, int odontologo,
            double porManuales, double porEsalud)
        {
            HistoriaClinica modelo = new HistoriaClinica();
            odont_remisiones_verificadas obj = new odont_remisiones_verificadas();
            obj.mes = mes;
            obj.año = año;
            obj.id_regional = regional;
            obj.id_unis = unis;
            obj.id_localidad = localidad;
            obj.id_odontologo_ppe = odontologo;
            obj.Porcentaje_remisiones_Manuales = porManuales;
            obj.Porcentaje_remisiones_esalud = porEsalud;
            obj.fecha_digita = DateTime.Now;
            obj.usuario_digita = SesionVar.UserName;
            modelo.InsertarRemisionesVerificadas(obj, ref MsgRes);

            string result = "";
            List<vw_odont_remisiones_verificadas> remisiones = modelo.GetRemisionesVerificadas(ref MsgRes).OrderByDescending(l => l.fecha_digita).ToList();

            foreach (var item in remisiones)
            {
                result += "<tr>";
                result += "<td>" + item.nom_regional + "</td>";
                result += "<td>" + item.nom_unis + "</td>";
                result += "<td>" + item.nom_localidad + "</td>";
                result += "<td>" + item.nom_odontologo + "</td>";
                result += "<td>" + item.Porcentaje_remisiones_Manuales + "%</td>";
                result += "<td>" + item.Porcentaje_remisiones_esalud + "%</td>";
                result += "</tr>";
            }

            return Json(result);
        }

        public void CrearConsolidadoRemisionesVerificadas(int? regional, int? unis, int? localidad, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal)
        {
            Models.Odontologia.HistoriaClinica Model = new Models.Odontologia.HistoriaClinica();
            string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "Rptremisionesverificadas.rdlc");
            List<vw_odont_remisiones_verificadas> lst = Model.GetRemisionesVerificadas(ref MsgRes);
            if (regional != null)
                lst = lst.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                lst = lst.Where(l => l.id_unis == unis).ToList();
            if (localidad != null)
                lst = lst.Where(l => l.id_localidad == localidad).ToList();
            if (mesinicial != null && añoinicial != null)
                lst = lst.Where(l => l.mes >= mesinicial && l.año == añoinicial).ToList();
            if (mesfinal != null && mesfinal != null)
                lst = lst.Where(l => l.mes <= mesfinal && l.año == añofinal).ToList();

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("RptRemisionesVerifiDataSet", lst);
            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rPath;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(rds);

            if (lst.Count != 0)
            {
                //CONTROL EXCEPCION
                try
                {
                    viewer.LocalReport.Refresh();
                    //EXPORTAR PDF

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WebForms.Warning[] warnings;
                    byte[] pdfContent = viewer.LocalReport.Render("EXCELOPENXML", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    //RETORNO PDF

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-length", pdfContent.Length.ToString());
                    Response.AddHeader("Content-disposition", "attachment; filename=Consolidado_remisiones.xlsx");
                    Response.BinaryWrite(pdfContent);
                    Response.Flush();

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "window.location.href = '" + Url.Action("IngresoReferenciaOdontologica", "odontologia") + "';" +
                "</script> ";

                Response.Write(rta);
            }
        }

        public FileContentResult DownloadHistoriaClinica()
        {
            List<vw_odontologia_detallado_historia_clinica> list = new List<vw_odontologia_detallado_historia_clinica>();

            //Model.fecha_inicial = Convert.ToDateTime("01/01/1900");
            //Model.fecha_final = Convert.ToDateTime("31/12/2020");

            list = BusClass.getdetallehistoriaclinica();


            var fileDownloadName = String.Format("Consolidado detallado historia clinica.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFile(list.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFile(List<vw_odontologia_detallado_historia_clinica> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Consulta");

            // Sets Headers
            ws.Cells[1, 1].Value = "ID Odont Historia Clinica";
            ws.Cells[1, 2].Value = "ID Odont Historia Clinica Paciente";
            ws.Cells[1, 3].Value = "Año";
            ws.Cells[1, 4].Value = "Mes";
            ws.Cells[1, 5].Value = "Descripcion Mes";
            ws.Cells[1, 6].Value = "Id Regional";
            ws.Cells[1, 7].Value = "Nombre Regional";
            ws.Cells[1, 8].Value = "Id Unis";
            ws.Cells[1, 9].Value = "Descripcion Unis";
            ws.Cells[1, 10].Value = "Nombre Odontologo Auditado";
            ws.Cells[1, 11].Value = "Usuario Odontologo Auditor";
            ws.Cells[1, 12].Value = "Nombre Odontologo Auditor";
            ws.Cells[1, 13].Value = "Fecha Digita";
            ws.Cells[1, 14].Value = "Usuario Digita";
            ws.Cells[1, 15].Value = "Numero Historia Clinica";
            ws.Cells[1, 16].Value = "Paciente";
            ws.Cells[1, 17].Value = "Fecha Atencion";
            ws.Cells[1, 18].Value = "Observaciones";
            ws.Cells[1, 19].Value = "TCalidad";
            ws.Cells[1, 20].Value = "TContenido";
            ws.Cells[1, 21].Value = "Porcentaje Calidad";
            ws.Cells[1, 22].Value = "Porcentaje Contenido";
            ws.Cells[1, 23].Value = "Total Calculado Calidad";
            ws.Cells[1, 24].Value = "Total Calculado Contenido";
            ws.Cells[1, 25].Value = "Suma Total";



            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_odont_historia_clinica;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).id_odont_historia_clinica_paciente;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).año;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).mes;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).des_mes;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).id_regional;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).des_regional;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).id_unis;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).des_unis;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).nom_odontologo_auditado;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).nom_odontologo_auditor;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).des_odontologo_auditor;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).fecha_digita;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).usuario_digita;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).numero_hc;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).paciente;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).fecha_atencion;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).observaciones;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).tcalidad;
                ws.Cells[i + 2, 20].Value = datasource.ElementAt(i).tcontenido;
                ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).porcentajecalidad;
                ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).porcentajecontenido;
                ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).total_calculadocalidad;
                ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).total_calculadocontenido;
                ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).sumatotal;





            }

            // Format Header of Table
            using (ExcelRange rng = ws.Cells["A1:Y1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                rng.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue); //Set color to DarkGray 
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        public ActionResult CrearPrestadorOdontologia()
        {
            Models.Odontologia.ortodoncia Model = new Models.Odontologia.ortodoncia();
            try
            {
                ViewBag.regionales = BusClass.GetRefRegion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public JsonResult SavePrestadorOdonto(Models.Odontologia.ProtesisFija Model)
        {
            String mensaje = "";

            try
            {
                List<Ref_odont_prestadores> List1 = new List<Ref_odont_prestadores>();

                List1 = Model.GetPrestadoresOdont();
                List1 = List1.Where(x => x.ID_Prestador == Convert.ToDecimal(Model.documento)).ToList();
                List1 = List1.Where(x => x.Especialidad == Model.especialista_tratante).ToList();

                if (List1.Count != 0)
                {
                    mensaje = "ERROR! PRESTADOR Y ESPECIALIDAD YA EXISTE.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Ref_odont_prestadores obj = new Ref_odont_prestadores();

                    obj.regional = Model.id_regional;
                    obj.ID_Prestador = Convert.ToDecimal(Model.documento);
                    obj.Razón_Social = Model.nombre;
                    obj.Nombre_Municipio = Model.Nombre_Municipio;
                    obj.Especialidad = Model.especialista_tratante;
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Model.InsertarprestadorOdontologia(obj, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE....";
                        return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensaje = "ERROR EL INGRESO DEL DETALLE.";
                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { success = false, message = error }, JsonRequestBehavior.AllowGet);
            }
        }

        // leo 26/05/2022
    }

}