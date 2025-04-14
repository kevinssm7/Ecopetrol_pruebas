using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Odontologia
{
    [SessionExpireFilter]
    public partial class TablerosOdontologiaController : Controller
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

        public ActionResult TableroControlOrtodoncia(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
            , String especilista, Int32? tiempotratante,String ortodoncia_primera_vez,String caso_trasferencia, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";
           

            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }
            if (ortodoncia_primera_vez == null)
            {
                ortodoncia_primera_vez = "";
            }
            if (caso_trasferencia == null)
            {
                caso_trasferencia = "";
            }
            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_ortodoncia> listadoOrtodoncia = Model.LIstTableroOrtodoncia().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.fecha_auditoria.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.fecha_auditoria.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
               listadoOrtodoncia = listadoOrtodoncia.Where(l => l.ortodoncista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }
            if (ortodoncia_primera_vez != "")
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.ortodoncia_primera_vez == ortodoncia_primera_vez).ToList();
            }

            if (caso_trasferencia != "")
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.caso_trasferencia == caso_trasferencia).ToList();
            }

            if (documento != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.documento_identidad == documento).ToList();
            }


            ViewBag.rolusuario = RolUsuario;
            ViewData["FechaIncio"] = fecha1;
            ViewData["FechaFinal"] = fecha2;
            ViewData["regional"] = regional;
            ViewData["unis"] = unis;
            ViewData["localidad"] = localidad;
            ViewData["especilista"] = especilista;
            ViewData["tiempotratante"] = tiempotratante;
            ViewData["ortodoncia_primera_vez"] = ortodoncia_primera_vez;
            ViewData["caso_trasferencia"] = caso_trasferencia;
            ViewData["documento"] = documento;
            ViewData["MostrarGestion"] = mostrar;

            Model.ListCheck = listadoOrtodoncia;

            return View(listadoOrtodoncia);
        }

        public ActionResult TableroControlProtesisFija(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
           , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }
            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_ProtesisFija> listadoPT = Model.LIstTableroPT().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPT = listadoPT.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPT = listadoPT.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoPT = listadoPT.Where(l => l.especialista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoPT = listadoPT.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }
          
            if (documento != 0)
            {
                listadoPT = listadoPT.Where(l => l.documento == documento).ToList();
            }


            ViewBag.rolusuario = RolUsuario;
            ViewData["FechaIncio"] = fecha1;
            ViewData["FechaFinal"] = fecha2;
            ViewData["regional"] = regional;
            ViewData["unis"] = unis;
            ViewData["localidad"] = localidad;
            ViewData["especilista"] = especilista;
            ViewData["tiempotratante"] = tiempotratante;
            ViewData["documento"] = documento;
            ViewData["MostrarGestion"] = mostrar;

            Model.ListCheck2 = listadoPT;

            return View(listadoPT);
        }

        public ActionResult TableroControlProtesisRemovible(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
           , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }
           
            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_ProtesisRemov> listadoPR = Model.LIstTableroPR().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPR = listadoPR.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPR = listadoPR.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoPR = listadoPR.Where(l => l.especialista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoPR = listadoPR.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }
           

            if (documento != 0)
            {
                listadoPR = listadoPR.Where(l => l.documento == documento).ToList();
            }


            ViewBag.rolusuario = RolUsuario;
            ViewData["FechaIncio"] = fecha1;
            ViewData["FechaFinal"] = fecha2;
            ViewData["regional"] = regional;
            ViewData["unis"] = unis;
            ViewData["localidad"] = localidad;
            ViewData["especilista"] = especilista;
            ViewData["tiempotratante"] = tiempotratante;
            ViewData["documento"] = documento;
            ViewData["MostrarGestion"] = mostrar;

            Model.ListCheck3 = listadoPR;

            return View(listadoPR);
        }

        public ActionResult TableroControlEndodoncia(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
           , String especilista, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
           
            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_endodoncia> listadoEndodoncia = Model.LIstTableroEndodoncia().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoEndodoncia = listadoEndodoncia.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoEndodoncia = listadoEndodoncia.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.endodoncista_tratante == especilista).ToList();
            }

            if (documento != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.documento == documento).ToList();
            }


            ViewBag.rolusuario = RolUsuario;
            ViewData["FechaIncio"] = fecha1;
            ViewData["FechaFinal"] = fecha2;
            ViewData["regional"] = regional;
            ViewData["unis"] = unis;
            ViewData["localidad"] = localidad;
            ViewData["especilista"] = especilista;
           
            ViewData["documento"] = documento;
            ViewData["MostrarGestion"] = mostrar;

            Model.ListCheck4 = listadoEndodoncia;

            return View(listadoEndodoncia);
        }


        public ActionResult TableroControlPlanMejora(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, String especilista)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
           if (especilista == "")
            {
                especilista = null;
            }


            List<vw_odont_tableros_plan_mejora> listadoPM = Model.GetOdontogTablerosPlanMejora().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPM = listadoPM.Where(l => l.Fecha_definicion.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPM = listadoPM.Where(l => l.Fecha_definicion.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPM = listadoPM.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPM = listadoPM.Where(l => l.id_unis == unis).ToList();
            }
            if (especilista != null)
            {
                listadoPM = listadoPM.Where(l => l.especialista == especilista).ToList();
            }

            ViewBag.rolusuario = RolUsuario;
            ViewData["FechaIncio"] = fecha1;
            ViewData["FechaFinal"] = fecha2;
            ViewData["regional"] = regional;
            ViewData["unis"] = unis;
            ViewData["especilista"] = especilista;
                     
            Model.ListCheck5 = listadoPM;

            return View(listadoPM);
        }

        public ActionResult DescargarReporte(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
            , String especilista, Int32? tiempotratante, String ortodoncia_primera_vez, String caso_trasferencia, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            ExportaConsultaOrtodoncia(FechaIncio,FechaFinal,regional,unis,localidad,especilista,tiempotratante,ortodoncia_primera_vez
                                  ,caso_trasferencia,documento);

            return View(Model);
        }

        public ActionResult DescargarReporteProtesisFija(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
        , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            ExportaConsultaProtesisFija(FechaIncio, FechaFinal, regional, unis, localidad, especilista, tiempotratante, documento);

            return View(Model);
        }

        public ActionResult DescargarReporteProtesisRemovible(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
          , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            ExportaConsultaProtesisRemovible(FechaIncio, FechaFinal, regional, unis, localidad, especilista, tiempotratante, documento);

            return View(Model);
        }

        public ActionResult DescargarReporteOrtodoncia(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
       , String especilista, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            ExportaConsultaEndodoncia(FechaIncio, FechaFinal, regional, unis, localidad, especilista,documento);

            return View(Model);
        }

        public ActionResult DescargarReportePlanMejora(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, String especilista)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            ExportaConsultaPlanMejora(FechaIncio, FechaFinal, regional, unis, especilista);

            return View(Model);
        }

        private void ExportaConsultaOrtodoncia(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
            , String especilista, Int32? tiempotratante, String ortodoncia_primera_vez, String caso_trasferencia, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }
            if (ortodoncia_primera_vez == null)
            {
                ortodoncia_primera_vez = "";
            }
            if (caso_trasferencia == null)
            {
                caso_trasferencia = "";
            }
            if (documento == null)
            {
                documento = 0;
            }

            List<vw_odont_tableros_ortodoncia> listadoOrtodoncia = Model.LIstTableroOrtodoncia().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.fecha_auditoria.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.fecha_auditoria.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.ortodoncista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }
            if (ortodoncia_primera_vez != "")
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.ortodoncia_primera_vez == ortodoncia_primera_vez).ToList();
            }

            if (caso_trasferencia != "")
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.caso_trasferencia == caso_trasferencia).ToList();
            }

            if (documento != 0)
            {
                listadoOrtodoncia = listadoOrtodoncia.Where(l => l.documento_identidad == documento).ToList();
            }

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Id Ortodoncia\";\"Regional\";\"Unis\";\"Localidad\";\"Documento Paciente\";\"Nombre Paciente\";\"edad\";\"Tiempo Tratamiento\";\"Ortodoncista Tratante\";\"Colaboracion Paciente\";\"PPE Quien Realiza\";\"Paciente Satisfecho\";\"Ortodoncia Primera Vez\";\"Verifica Calidad tto hc\";\"Caso Trasferencia\";\"Donde Trasferencia\";\"Observaciones\";\"Fecha Auditoria\";\"Porcentaje Total\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteOrtodoncia" + DateTime.Now + ".csv");
            Response.ContentType = "text/csv";
            foreach (var line in listadoOrtodoncia)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\"",
                          line.id_tratamiento_ortodoncia,
                          line.regional_descripcion,
                          line.unis_descripcion,
                          line.localidad_descripcion,
                          line.documento_identidad,
                          line.nombre,
                          line.edad,
                          line.tiempo_tratamiento,
                          line.ortodoncista_tratante,
                          line.colaboracion_paciente,
                          line.ppe_quien_realiza,
                          line.paciente_satisfecho,
                          line.ortodoncia_primera_vez,
                          line.verifica_calidad_tto_hc,
                          line.caso_trasferencia,
                          line.donde_trasferencia,
                          line.observaciones,
                          line.fecha_auditoria,
                          line.Promedio));

            }

            Response.Write(sw.ToString());

            Response.End();

        }

        public void ExportaConsultaProtesisFija(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
          , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }
            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_ProtesisFija> listadoPT = Model.LIstTableroPT().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPT = listadoPT.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPT = listadoPT.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoPT = listadoPT.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoPT = listadoPT.Where(l => l.especialista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoPT = listadoPT.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }

            if (documento != 0)
            {
                listadoPT = listadoPT.Where(l => l.documento == documento).ToList();
            }

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Id Protesis Fija\";\"Regional\";\"Unis\";\"Localidad\";\"Documento Paciente\";\"Nombre Paciente\";\"edad\";\"Tiempo Tratamiento\";\"Especialista Tratante\";\"Colaboracion Paciente\";\"PPE Quien Realiza\";\"Paciente Satisfecho\";\"Observaciones\";\"Fecha Auditoria\";\"TotalDT1\";\"TotalDT2\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteProtesisFija" + DateTime.Now + ".csv");
            Response.ContentType = "text/csv";
            foreach (var line in listadoPT)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\"",
                          line.id_rehabilitacion_oral_protesis_fija,
                          line.regional_descripcion,
                          line.unis_descripcion,
                          line.localidad_descripcion,
                          line.documento,
                          line.nombre,
                          line.edad,
                          line.tiempo_tratamiento,
                          line.especialista_tratante,
                          line.colaboracion_paciente,
                          line.ppe_quien_realiza,
                          line.paciente_satisfecho,
                          line.observaciones,
                          line.fecha_digita,
                          line.TotalDT1,
                          line.TotalDT2));

            }

            Response.Write(sw.ToString());

            Response.End();

        }

        public void ExportaConsultaProtesisRemovible(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
          , String especilista, Int32? tiempotratante, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }
            if (tiempotratante == null)
            {
                tiempotratante = 0;
            }

            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_ProtesisRemov> listadoPR = Model.LIstTableroPR().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPR = listadoPR.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPR = listadoPR.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoPR = listadoPR.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoPR = listadoPR.Where(l => l.especialista_tratante == especilista).ToList();
            }

            if (tiempotratante != 0)
            {
                listadoPR = listadoPR.Where(l => l.tiempo_tratamiento == tiempotratante).ToList();
            }


            if (documento != 0)
            {
                listadoPR = listadoPR.Where(l => l.documento == documento).ToList();
            }


            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Id Protesis Removible\";\"Regional\";\"Unis\";\"Localidad\";\"Documento Paciente\";\"Nombre Paciente\";\"edad\";\"Tiempo Tratamiento\";\"Especialista Tratante\";\"Colaboracion Paciente\";\"PPE Quien Realiza\";\"Paciente Satisfecho\";\"Observaciones\";\"Fecha Auditoria\";\"Total Porcentaje\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteProtesisRemovible" + DateTime.Now + ".csv");
            Response.ContentType = "text/csv";
            foreach (var line in listadoPR)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\"",
                          line.id_rehabilitacion_oral_protesis_removibles,
                          line.regional_descripcion,
                          line.unis_descripcion,
                          line.localidad_descripcion,
                          line.documento,
                          line.nombre,
                          line.edad,
                          line.tiempo_tratamiento,
                          line.especialista_tratante,
                          line.colaboracion_paciente,
                          line.ppe_quien_realiza,
                          line.paciente_satisfecho,
                          line.observaciones,
                          line.fecha_digita,
                          line.PromedioTotal));

            }

            Response.Write(sw.ToString());

            Response.End();
        }

        public void ExportaConsultaEndodoncia(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, Int32? localidad
        , String especilista, Int32? documento)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (localidad == null)
            {
                localidad = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }

            if (documento == null)
            {
                documento = 0;
            }
            bool mostrar = true;

            List<vw_odont_tableros_endodoncia> listadoEndodoncia = Model.LIstTableroEndodoncia().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoEndodoncia = listadoEndodoncia.Where(l => l.fecha_digita.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoEndodoncia = listadoEndodoncia.Where(l => l.fecha_digita.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_unis == unis).ToList();
            }

            if (localidad != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.id_localidad == localidad).ToList();
            }

            if (especilista != null)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.endodoncista_tratante == especilista).ToList();
            }

            if (documento != 0)
            {
                listadoEndodoncia = listadoEndodoncia.Where(l => l.documento == documento).ToList();
            }

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Id Endodoncia\";\"Regional\";\"Unis\";\"Localidad\";\"Documento Paciente\";\"Nombre Paciente\";\"Observaciones\";\"Fecha Auditoria\";\"Total Porcentaje\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteProtesisRemovible" + DateTime.Now + ".csv");
            Response.ContentType = "text/csv";
            foreach (var line in listadoEndodoncia)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\"",
                          line.id_tratamiento_endodoncia,
                          line.regional_descripcion,
                          line.unis_descripcion,
                          line.localidad_descripcion,
                          line.documento,
                          line.nombre,
                          line.observaciones,
                          line.fecha_digita,
                          line.PromedioTotal));

            }

            Response.Write(sw.ToString());

            Response.End();

        }

        public void ExportaConsultaPlanMejora(DateTime? FechaIncio, DateTime? FechaFinal, Int32? regional, Int32? unis, String especilista)
        {
            Models.Odontologia.TableroControlOdontologia Model = new Models.Odontologia.TableroControlOdontologia();

            string RolUsuario = SesionVar.ROL;
            string fecha1 = "";
            string fecha2 = "";


            if (regional == null)
            {
                regional = 0;
            }
            if (unis == null)
            {
                unis = 0;
            }
            if (especilista == "")
            {
                especilista = null;
            }


            List<vw_odont_tableros_plan_mejora> listadoPM = Model.GetOdontogTablerosPlanMejora().ToList();

            if (FechaIncio != null)
            {
                fecha1 = FechaIncio.Value.ToString("dd/MM/yyyy");
                listadoPM = listadoPM.Where(l => l.Fecha_definicion.Value.Date >= FechaIncio.Value.Date).ToList();
            }

            if (FechaFinal != null)
            {
                fecha2 = FechaFinal.Value.ToString("dd/MM/yyyy");
                listadoPM = listadoPM.Where(l => l.Fecha_definicion.Value.Date <= FechaFinal.Value.Date).ToList();
            }

            if (regional != 0)
            {
                listadoPM = listadoPM.Where(l => l.id_regional == regional).ToList();
            }

            if (unis != 0)
            {
                listadoPM = listadoPM.Where(l => l.id_unis == unis).ToList();
            }
            if (especilista != null)
            {
                listadoPM = listadoPM.Where(l => l.especialista == especilista).ToList();
            }

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"ID\";\"REGIONAL\";\"UNIS\";\"PRESTADOR\";\"FECHA DE DEFINICION DE ACCION DE MEJORA\";\"ACCIONES\";\"FECHA DE SEGUIMIENTO DE ACCION DE MEJORA\";\"ESTADO DE LAS ACCIONES DEL  PLAN DE MEJORA\";\"ESTADO DEL  PLAN DE MEJORA\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteProtesisRemovible" + DateTime.Now + ".csv");
            Response.ContentType = "text/csv";
            foreach (var line in listadoPM)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\"",
                          line.id_odont_plan_mejora,
                          line.regional_descripcion,
                          line.unis_descripcion,
                          line.especialista,
                          line.Fecha_definicion,
                          line.accion_mejora,
                          line.fecha_seguimiento,
                          line.estado,
                          line.estado_plan ));

            }

            Response.Write(sw.ToString());

            Response.End();

        }

    }
}