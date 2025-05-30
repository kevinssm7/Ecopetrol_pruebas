using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Consulta;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;
using System.Data;
using Aspose.Cells;
using ClosedXML.Excel;

namespace AsaludEcopetrol.Controllers.Consultas
{
    [SessionExpireFilter]
    public class ConsultasController : Controller
    {
        #region  PROPIEDADES

        Consulta Model = new Consulta();

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
        readonly String Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;

        #endregion

        #region EVENTOS PRIVADOS
        public ActionResult ExportaConsultaCensoFechas(Models.Consulta.Consulta Model)
        {

            if (Model.ListaCensoFechas.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"fecha_recepcion_censo\";\"nit_ips\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"edad\";\"fecha_nacimiento\";\"genero\";\"fecha_ingreso\";\"fecha_egreso\";\"Tipo_Habitacion\";\"dias_estancia\";\"Tipo_ingreso\";\"dx_actual\";\"Descripcion_Cie10\";\"Origen_evento\";\"Medico_Auditor\";\"tipo_salud\";\"regional\";\"unis\"");



                foreach (var line in Model.ListaCensoFechas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\"",
                                               line.id_censo,
                                               line.fecha_recepcion_censo,
                                               line.nit_ips,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.edad,
                                               line.fecha_nacimiento,
                                               line.genero,
                                               line.fecha_ingreso,
                                               line.fecha_egreso,
                                               line.Tipo_Habitacion,
                                               line.dias_estancia,
                                               line.Tipo_ingreso,
                                               line.dx_actual,
                                               line.Descripcion_Cie10,
                                               line.Origen_evento,
                                               line.Medico_Auditor,
                                               line.tipo_salud,
                                               line.regional,
                                               line.unis));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaCenso" + DateTime.Now + ".csv");

                //Response.ClearContent();
                //Response.AddHeader("content-disposition", "attachment;filename=ConsultaCenso" + DateTime.Now + ".csv");
                //Response.ContentType = "text/csv";
                //Response.Write(sw.ToString());
                //Response.End();
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaConsultaCensoDocumentos(Models.Consulta.Consulta Model)
        {
            if (Model.ListaCensoDocumento.Count != 0)
            {
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"id_censo\";\"fecha_recepcion_censo\";\"nit_ips\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"edad\";\"fecha_nacimiento\";\"genero\";\"fecha_ingreso\";\"fecha_egreso\";\"Tipo_Habitacion\";\"dias_estancia\";\"Tipo_ingreso\";\"dx_actual\";\"Descripcion_Cie10\";\"Origen_evento\";\"Medico_Auditor\";\"tipo_salud\";\"regional\";\"unis\"");

                foreach (var line in Model.ListaCensoDocumento)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\"",
                                               line.id_censo,
                                               line.fecha_recepcion_censo,
                                               line.nit_ips,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.edad,
                                               line.fecha_nacimiento,
                                               line.genero,
                                               line.fecha_ingreso,
                                               line.fecha_egreso,
                                               line.Tipo_Habitacion,
                                               line.dias_estancia,
                                               line.Tipo_ingreso,
                                               line.dx_actual,
                                               line.Descripcion_Cie10,
                                               line.Origen_evento,
                                               line.Medico_Auditor,
                                               line.tipo_salud,
                                               line.regional,
                                               line.unis));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaCenso" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
            }
        }

        private ActionResult ExportaConsultaCensoRegional(Models.Consulta.Consulta Model)
        {

            if (Model.ListaCensoRegional.Count != 0)
            {
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"id_censo\";\"fecha_recepcion_censo\";\"nit_ips\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"edad\";\"fecha_nacimiento\";\"genero\";\"fecha_ingreso\";\"fecha_egreso\";\"Tipo_Habitacion\";\"dias_estancia\";\"Tipo_ingreso\";\"dx_actual\";\"Descripcion_Cie10\";\"Origen_evento\";\"Medico_Auditor\";\"tipo_salud\";\"regional\";\"unis\"");

                foreach (var line in Model.ListaCensoRegional)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\"",
                                               line.id_censo,
                                               line.fecha_recepcion_censo,
                                               line.nit_ips,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.edad,
                                               line.fecha_nacimiento,
                                               line.genero,
                                               line.fecha_ingreso,
                                               line.fecha_egreso,
                                               line.Tipo_Habitacion,
                                               line.dias_estancia,
                                               line.Tipo_ingreso,
                                               line.dx_actual,
                                               line.Descripcion_Cie10,
                                               line.Origen_evento,
                                               line.Medico_Auditor,
                                               line.tipo_salud,
                                               line.regional,
                                               line.unis));
                }


                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaCenso" + DateTime.Now + ".csv");

            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaCensoConcurrenciaFecha(Models.Consulta.Consulta Model, int tipo)
        {

            Model.CargarListas2(tipo);

            if (Model.ListaCensoConcuFechas.Count != 0)
            {
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"id_censo\";\"fecha_recepcion_censo\";\"nit_ips\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"edad\";\"fecha_nacimiento\";\"genero\";\"fecha_ingreso\";\"fecha_egreso\";\"Tipo_Habitacion\";\"dias_estancia\";\"Tipo_ingreso\";\"dx_actual\";\"Descripcion_Cie10\";\"Origen_evento\";\"Medico_Auditor\";\"tipo_salud\";\"regional\";\"unis\"");


                //foreach (var line in Model.ListaCensoConcuFechas)
                //{
                //    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\"",
                //                 line.id_censo,
                //                line.id_concurrencia,
                //                line.id_evolucion,
                //                line.tipo_identifi_afiliado,
                //                line.num_identifi_afil,
                //                line.tipo_salud,
                //                line.Ciudad_Serv_Medicos,
                //                line.regional,
                //                line.Regional_Beneficiario,
                //                line.unis,
                //                line.fecha_recepcion_censo,
                //                line.primer_apellido,
                //                line.segundo_apellido,
                //                line.primer_nombre,
                //                line.segundo_nombre,
                //                line.fecha_nacimiento,
                //                line.edad,
                //                line.genero,
                //                line.fecha_ingreso,
                //                line.Tipo_ingresoo,
                //                line.Origen_evento,
                //                line.Tipo_Habitacion,
                //                line.Medico_Auditor,
                //                line.Nit_Ips,
                //                line.Documento_SAP_Ips,
                //                line.NIT_SUIS,
                //                line.Nombre_Ips,
                //                line.RAZON_SOCIAL_SUIS,
                //                line.CiudadIPs,
                //                line.dx_actual,
                //                line.Descripcion_Cie10,
                //                line.ALTO_COSTO,
                //                line.DESCRIPCION_ALTO_COSTO,
                //                line.salud_publica,
                //                line.nombre_salud_publica,
                //                line.Codigo_CIE10_concurrencia,
                //                line.NOMBRE_CIE10_concurrencia,
                //                line.triage,
                //                line.reingreso,
                //                line.gestantes,
                //                line.fecha_egreso,
                //                line.DxprincipalEgreso,
                //                line.nombre_cie10_EGRESO,
                //                line.CondicionAlta,
                //                line.hospitalizacion_prevenible,
                //                line.descripcion_prevenible,
                //                line.NumeroDefuncion,
                //                line.HoraDefuncion,
                //                line.tipo_instancia,
                //                line.medico_tratante,
                //                line.MEGA,
                //                line.especialidad,
                //                line.procedimientoqx,
                //                line.id_cups_qx,
                //                line.nombre_cups,
                //                line.incapacidades,
                //                line.Descripcion_evolucion,
                //                line.Fecha_evolucion,
                //                line.TIPO_HABITACION_evolucion,
                //                line.TIENE_ESTANCIA_PERTINENTE,
                //                line.EPOC,
                //                line.PAD,
                //                line.RCV,
                //                line.cohortes_GESTANTES,


                //                               ));
                //}


                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaConcurrencia" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaConcurrenciaDocumentos(Models.Consulta.Consulta Model)
        {

            //if (Model.ListaConcurrenciaDocumento.Count != 0)
            if (Model.ListaConcurrenciaDocumentoReporte.Count != 0)
            {
                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"fecha_nacimiento\";\"edad\";\"genero\";\"fecha_ingreso\";\"Tipo_ingreso\";\"Origen_evento\";\"Tipo_Habitacion_censo\";\"Medico_Auditor\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"regional\";\"dx_actual\";\"Descripcion_Cie10\";\"dias_h\";\"ALTO_COSTO\";\"DESCRIPCION_ALTO_COSTO\";\"salud_publica\";\"nombre_salud_publica\";\"Codigo_CIE10_concurrencia\";\"NOMBRE_CIE10_concurrencia\";\"triage\";\"reingreso\";\"gestantes\";\"fecha_evolucion\";\"NOMBRE_CIE101\";\"Nombre_CIE102\";\"NOMBRE_CIE103\";\"NOMBRE_CIE104\";\"descripcion_evolucion\";\"TIPO_HABITACION\";\"TIENE_GLOSA\";\"Descripcion_Cups\";\"cantidad_glosa\";\"Valor_Glosa\";\"id_codigo_glosa\";\"RESPONSABLE_GLOSA\";\"observaciones_auditoria\";\"fecha_evento_adv\";\"descripcion_Evento\";\"fecha_situcion\";\"descripcion_situacion\";\"fecha_egreso\";\"DxprincipalEgreso\";\"nombre_cie10_EGRESO\";\"CondicionAlta\";\"NumeroDefuncion\";\"HoraDefuncion\";\"tipo_instancia\";\"especialidad\";\"procedimientoqx\";\"id_cups_qx\";\"nombre_cups\";\"incapacidades\";\"tiene_estancia_pertinente\"");

                //foreach (var line in Model.ListaConcurrenciaDocumento)
                foreach (var line in Model.ListaConcurrenciaDocumentoReporte)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\";\"{43}\";\"{44}\";\"{45}\";\"{46}\";\"{47}\";\"{48}\";\"{49}\";\"{50}\";\"{51}\";\"{52}\";\"{53}\";\"{54}\";\"{55}\";\"{56}\";\"{57}\";\"{58}\";\"{59}\";\"{60}\";\"{61}\";\"{62}\";\"{63}\"",
                                               line.id_censo,
                                               line.id_concurrencia,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.fecha_recepcion_censo,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.fecha_nacimiento,
                                               line.edad,
                                               line.genero,
                                               line.fecha_ingreso,
                                               line.Tipo_ingreso,
                                               line.Origen_evento,
                                               line.Tipo_Habitacion_censo,
                                               line.Medico_Auditor,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.regional,
                                               line.dx_actual,
                                               line.Descripcion_Cie10,
                                               line.dias_h,
                                               line.ALTO_COSTO,
                                               line.DESCRIPCION_ALTO_COSTO,
                                               line.salud_publica,
                                               line.nombre_salud_publica,
                                               line.Codigo_CIE10_concurrencia,
                                               line.NOMBRE_CIE10_concurrencia,
                                               line.triage,
                                               line.reingreso,
                                               line.gestantes,
                                               line.fecha_evolucion,
                                               line.NOMBRE_CIE101,
                                               line.Nombre_CIE102,
                                               line.NOMBRE_CIE103,
                                               line.NOMBRE_CIE104,
                                               line.descripcion_evolucion,
                                               line.TIPO_HABITACION,
                                               line.TIENE_GLOSA,
                                               line.Descripcion_Cups,
                                               line.cantidad_glosa,
                                               line.Valor_Glosa,
                                               line.id_codigo_glosa,
                                               line.RESPONSABLE_GLOSA,
                                               line.observaciones_auditoria,
                                               line.fecha_evento_adv,
                                               line.descripcion_Evento,
                                               line.fecha_situcion,
                                               line.descripcion,
                                               line.fecha_egreso,
                                               line.DxprincipalEgreso,
                                               line.nombre_cie10_EGRESO,
                                               line.CondicionAlta,
                                               line.NumeroDefuncion,
                                               line.HoraDefuncion,
                                               line.tipo_instancia,
                                               line.especialidad,
                                               line.procedimientoqx,
                                               line.id_cups_qx,
                                               line.nombre_cups,
                                               line.incapacidades,
                                               line.tiene_estancia_pertinente));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaConcurrencia" + DateTime.Now + ".csv");
            }
            else
            {

                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaConcurrenciaFechasVisita(Models.Consulta.Consulta Model)
        {

            StringWriter sw = new StringWriter();
            sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"fecha_nacimiento\";\"edad\";\"genero\";\"fecha_ingreso\";\"Tipo_ingreso\";\"Origen_evento\";\"Tipo_Habitacion_censo\";\"Medico_Auditor\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"regional\";\"dx_actual\";\"Descripcion_Cie10\";\"dias_h\";\"ALTO_COSTO\";\"DESCRIPCION_ALTO_COSTO\";\"salud_publica\";\"nombre_salud_publica\";\"Codigo_CIE10_concurrencia\";\"NOMBRE_CIE10_concurrencia\";\"triage\";\"reingreso\";\"gestantes\";\"fecha_evolucion\";\"NOMBRE_CIE101\";\"Nombre_CIE102\";\"NOMBRE_CIE103\";\"NOMBRE_CIE104\";\"descripcion_evolucion\";\"TIPO_HABITACION\";\"TIENE_GLOSA\";\"Descripcion_Cups\";\"cantidad_glosa\";\"Valor_Glosa\";\"id_codigo_glosa\";\"RESPONSABLE_GLOSA\";\"observaciones_auditoria\";\"fecha_evento_adv\";\"descripcion_Evento\";\"fecha_situcion\";\"descripcion_situacion\";\"fecha_egreso\";\"DxprincipalEgreso\";\"nombre_cie10_EGRESO\";\"CondicionAlta\";\"NumeroDefuncion\";\"HoraDefuncion\";\"tipo_instancia\";\"especialidad\";\"procedimientoqx\";\"id_cups_qx\";\"nombre_cups\";\"incapacidades\";\"tiene_estancia_pertinente\"");

            try
            {
                //if (Model.ListaConcurrenciaFechas.Count != 0)
                if (Model.ListaConcurrenciaFechasReporte.Count != 0)
                {
                    //foreach (var line in Model.ListaConcurrenciaFechas)
                    foreach (var line in Model.ListaConcurrenciaFechasReporte)
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\";\"{43}\";\"{44}\";\"{45}\";\"{46}\";\"{47}\";\"{48}\";\"{49}\";\"{50}\";\"{51}\";\"{52}\";\"{53}\";\"{54}\";\"{55}\";\"{56}\";\"{57}\";\"{58}\";\"{59}\";\"{60}\";\"{61}\";\"{62}\";\"{63}\"",
                                                  line.id_censo,
                                                  line.id_concurrencia,
                                                  line.tipo_identifi_afiliado,
                                                  line.num_identifi_afil,
                                                  line.fecha_recepcion_censo,
                                                  line.primer_apellido,
                                                  line.segundo_apellido,
                                                  line.primer_nombre,
                                                  line.segundo_nombre,
                                                  line.fecha_nacimiento,
                                                  line.edad,
                                                  line.genero,
                                                  line.fecha_ingreso,
                                                  line.Tipo_ingreso,
                                                  line.Origen_evento,
                                                  line.Tipo_Habitacion_censo,
                                                  line.Medico_Auditor,
                                                  line.ips_primaria,
                                                  line.Nombre_Ips,
                                                  line.CiudadIPs,
                                                  line.regional,
                                                  line.dx_actual,
                                                  line.Descripcion_Cie10,
                                                  line.dias_h,
                                                  line.ALTO_COSTO,
                                                  line.DESCRIPCION_ALTO_COSTO,
                                                  line.salud_publica,
                                                  line.nombre_salud_publica,
                                                  line.Codigo_CIE10_concurrencia,
                                                  line.NOMBRE_CIE10_concurrencia,
                                                  line.triage,
                                                  line.reingreso,
                                                  line.gestantes,
                                                  line.fecha_evolucion,
                                                  line.NOMBRE_CIE101,
                                                  line.Nombre_CIE102,
                                                  line.NOMBRE_CIE103,
                                                  line.NOMBRE_CIE104,
                                                  line.descripcion_evolucion,
                                                  line.TIPO_HABITACION,
                                                  line.TIENE_GLOSA,
                                                  line.Descripcion_Cups,
                                                  line.cantidad_glosa,
                                                  line.Valor_Glosa,
                                                  line.id_codigo_glosa,
                                                  line.RESPONSABLE_GLOSA,
                                                  line.observaciones_auditoria,
                                                  line.fecha_evento_adv,
                                                  line.descripcion_Evento,
                                                  line.fecha_situcion,
                                                  line.descripcion,
                                                  line.fecha_egreso,
                                                  line.DxprincipalEgreso,
                                                  line.nombre_cie10_EGRESO,
                                                  line.CondicionAlta,
                                                  line.NumeroDefuncion,
                                                  line.HoraDefuncion,
                                                  line.tipo_instancia,
                                                  line.especialidad,
                                                  line.procedimientoqx,
                                                  line.id_cups_qx,
                                                  line.nombre_cups,
                                                  line.incapacidades,
                                                  line.tiene_estancia_pertinente));
                    }
                    byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "ConsultaConcurrencia" + DateTime.Now + ".csv");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });

            }
        }

        private ActionResult ExportaConsultaConcurrenciaFechasDigitacion(Models.Consulta.Consulta Model)
        {
            if (Model.ListaConcurrenciaFechas2.Count != 0)
            {
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"fecha_nacimiento\";\"edad\";\"genero\";\"fecha_ingreso\";\"Tipo_ingreso\";\"Origen_evento\";\"Tipo_Habitacion_censo\";\"Medico_Auditor\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"regional\";\"dx_actual\";\"Descripcion_Cie10\";\"dias_h\";\"ALTO_COSTO\";\"DESCRIPCION_ALTO_COSTO\";\"salud_publica\";\"nombre_salud_publica\";\"Codigo_CIE10_concurrencia\";\"NOMBRE_CIE10_concurrencia\";\"triage\";\"reingreso\";\"gestantes\";\"fecha_evolucion\";\"NOMBRE_CIE101\";\"Nombre_CIE102\";\"NOMBRE_CIE103\";\"NOMBRE_CIE104\";\"descripcion_evolucion\";\"TIPO_HABITACION\";\"TIENE_GLOSA\";\"Descripcion_Cups\";\"cantidad_glosa\";\"Valor_Glosa\";\"id_codigo_glosa\";\"RESPONSABLE_GLOSA\";\"observaciones_auditoria\";\"fecha_evento_adv\";\"descripcion_Evento\";\"fecha_situcion\";\"descripcion_situacion\";\"fecha_egreso\";\"DxprincipalEgreso\";\"nombre_cie10_EGRESO\";\"CondicionAlta\";\"NumeroDefuncion\";\"HoraDefuncion\";\"tipo_instancia\";\"especialidad\";\"procedimientoqx\";\"id_cups_qx\";\"nombre_cups\";\"incapacidades\";\"tiene_estancia_pertinente\"");


                foreach (var line in Model.ListaConcurrenciaFechas2)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\";\"{43}\";\"{44}\";\"{45}\";\"{46}\";\"{47}\";\"{48}\";\"{49}\";\"{50}\";\"{51}\";\"{52}\";\"{53}\";\"{54}\";\"{55}\";\"{56}\";\"{57}\";\"{58}\";\"{59}\";\"{60}\";\"{61}\";\"{62}\";\"{63}\"",

                                           line.id_censo,
                                              line.id_concurrencia,
                                              line.tipo_identifi_afiliado,
                                              line.num_identifi_afil,
                                              line.fecha_recepcion_censo,
                                              line.primer_apellido,
                                              line.segundo_apellido,
                                              line.primer_nombre,
                                              line.segundo_nombre,
                                              line.fecha_nacimiento,
                                              line.edad,
                                              line.genero,
                                              line.fecha_ingreso,
                                              line.Tipo_ingreso,
                                              line.Origen_evento,
                                              line.Tipo_Habitacion_censo,
                                              line.Medico_Auditor,
                                              line.ips_primaria,
                                              line.Nombre_Ips,
                                              line.CiudadIPs,
                                              line.regional,
                                              line.dx_actual,
                                              line.Descripcion_Cie10,
                                              line.dias_h,
                                              line.ALTO_COSTO,
                                              line.DESCRIPCION_ALTO_COSTO,
                                              line.salud_publica,
                                              line.nombre_salud_publica,
                                              line.Codigo_CIE10_concurrencia,
                                              line.NOMBRE_CIE10_concurrencia,
                                              line.triage,
                                              line.reingreso,
                                              line.gestantes,
                                              line.fecha_evolucion,
                                              line.NOMBRE_CIE101,
                                              line.Nombre_CIE102,
                                              line.NOMBRE_CIE103,
                                              line.NOMBRE_CIE104,
                                              line.descripcion_evolucion,
                                              line.TIPO_HABITACION,
                                              line.TIENE_GLOSA,
                                              line.Descripcion_Cups,
                                              line.cantidad_glosa,
                                              line.Valor_Glosa,
                                              line.id_codigo_glosa,
                                              line.RESPONSABLE_GLOSA,
                                              line.observaciones_auditoria,
                                              line.fecha_evento_adv,
                                              line.descripcion_Evento,
                                              line.fecha_situcion,
                                              line.descripcion,
                                              line.fecha_egreso,
                                              line.DxprincipalEgreso,
                                              line.nombre_cie10_EGRESO,
                                              line.CondicionAlta,
                                              line.NumeroDefuncion,
                                              line.HoraDefuncion,
                                              line.tipo_instancia,
                                              line.especialidad,
                                              line.procedimientoqx,
                                              line.id_cups_qx,
                                              line.nombre_cups,
                                              line.incapacidades,
                                              line.tiene_estancia_pertinente));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaConcurrencia" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        public ActionResult ExportaConsultaConcurrenciaRegional(Models.Consulta.Consulta Model)
        {

            //if (Model.ListaConcurrenciaRegional.Count != 0)
            if (Model.ListaConcurrenciaRegionalReporte.Count != 0)
            {
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"fecha_nacimiento\";\"edad\";\"genero\";\"fecha_ingreso\";\"Tipo_ingreso\";\"Origen_evento\";\"Tipo_Habitacion_censo\";\"Medico_Auditor\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"regional\";\"dx_actual\";\"Descripcion_Cie10\";\"dias_h\";\"ALTO_COSTO\";\"DESCRIPCION_ALTO_COSTO\";\"salud_publica\";\"nombre_salud_publica\";\"Codigo_CIE10_concurrencia\";\"NOMBRE_CIE10_concurrencia\";\"triage\";\"reingreso\";\"gestantes\";\"fecha_evolucion\";\"NOMBRE_CIE101\";\"Nombre_CIE102\";\"NOMBRE_CIE103\";\"NOMBRE_CIE104\";\"descripcion_evolucion\";\"TIPO_HABITACION\";\"TIENE_GLOSA\";\"Descripcion_Cups\";\"cantidad_glosa\";\"Valor_Glosa\";\"id_codigo_glosa\";\"RESPONSABLE_GLOSA\";\"observaciones_auditoria\";\"fecha_evento_adv\";\"descripcion_Evento\";\"fecha_situcion\";\"descripcion_situacion\";\"fecha_egreso\";\"DxprincipalEgreso\";\"nombre_cie10_EGRESO\";\"CondicionAlta\";\"NumeroDefuncion\";\"HoraDefuncion\";\"tipo_instancia\";\"especialidad\";\"procedimientoqx\";\"id_cups_qx\";\"nombre_cups\";\"incapacidades\";\"tiene_estancia_pertinente\"");

                //foreach (var line in Model.ListaConcurrenciaRegional)
                foreach (var line in Model.ListaConcurrenciaRegionalReporte)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\";\"{43}\";\"{44}\";\"{45}\";\"{46}\";\"{47}\";\"{48}\";\"{49}\";\"{50}\";\"{51}\";\"{52}\";\"{53}\";\"{54}\";\"{55}\";\"{56}\";\"{57}\";\"{58}\";\"{59}\";\"{60}\";\"{61}\";\"{62}\";\"{63}\"",

                                         line.id_censo,
                                            line.id_concurrencia,
                                            line.tipo_identifi_afiliado,
                                            line.num_identifi_afil,
                                            line.fecha_recepcion_censo,
                                            line.primer_apellido,
                                            line.segundo_apellido,
                                            line.primer_nombre,
                                            line.segundo_nombre,
                                            line.fecha_nacimiento,
                                            line.edad,
                                            line.genero,
                                            line.fecha_ingreso,
                                            line.Tipo_ingreso,
                                            line.Origen_evento,
                                            line.Tipo_Habitacion_censo,
                                            line.Medico_Auditor,
                                            line.ips_primaria,
                                            line.Nombre_Ips,
                                            line.CiudadIPs,
                                            line.regional,
                                            line.dx_actual,
                                            line.Descripcion_Cie10,
                                            line.dias_h,
                                            line.ALTO_COSTO,
                                            line.DESCRIPCION_ALTO_COSTO,
                                            line.salud_publica,
                                            line.nombre_salud_publica,
                                            line.Codigo_CIE10_concurrencia,
                                            line.NOMBRE_CIE10_concurrencia,
                                            line.triage,
                                            line.reingreso,
                                            line.gestantes,
                                            line.fecha_evolucion,
                                            line.NOMBRE_CIE101,
                                            line.Nombre_CIE102,
                                            line.NOMBRE_CIE103,
                                            line.NOMBRE_CIE104,
                                            line.descripcion_evolucion,
                                            line.TIPO_HABITACION,
                                            line.TIENE_GLOSA,
                                            line.Descripcion_Cups,
                                            line.cantidad_glosa,
                                            line.Valor_Glosa,
                                            line.id_codigo_glosa,
                                            line.RESPONSABLE_GLOSA,
                                            line.observaciones_auditoria,
                                            line.fecha_evento_adv,
                                            line.descripcion_Evento,
                                            line.fecha_situcion,
                                            line.descripcion,
                                            line.fecha_egreso,
                                            line.DxprincipalEgreso,
                                            line.nombre_cie10_EGRESO,
                                            line.CondicionAlta,
                                            line.NumeroDefuncion,
                                            line.HoraDefuncion,
                                            line.tipo_instancia,
                                            line.especialidad,
                                            line.procedimientoqx,
                                            line.id_cups_qx,
                                            line.nombre_cups,
                                            line.incapacidades,
                                            line.tiene_estancia_pertinente));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaConcurrencia" + DateTime.Now + ".csv");
            }
            else
            {

                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaConsultaEgresoFechas(Models.Consulta.Consulta Model)
        {
            if (Model.ListaegresoFechas2.Count != 0)
            {
                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Mes\";\"Fecha egreso\";\"Regional\";\"Unis\";\"Tipo documento\";\"Documento\";\"Nombre\";\"Edad\";\"IPS\";\"CIE 10\";\"Diagnostico\";\"Telefono\";\"Celular\";\"DirecciOn\";\"Auditor\";\"Mega\";\"Regional Beneficiario\"");

                foreach (var line in Model.ListaegresoFechas2)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\"",
                                               line.mes,
                                               line.fecha_egreso_for,
                                               line.regional,
                                               line.unis,
                                               line.afi_tipo_doc,
                                               line.documento,
                                               line.afi_nom,
                                               line.afi_edad,
                                               line.Nombre_Ips,
                                               line.DxprincipalEgreso,
                                               line.nombre_cie10,
                                               line.afi_tel,
                                               line.afi_cel,
                                               line.Direccion_pac,
                                               line.Auditor,
                                               line.MEGA,
                                               line.Regional_Beneficiario)
                        );
                }
                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaEgreso" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaEgresoDocumentos(Models.Consulta.Consulta Model)
        {

            if (Model.ListaegresoDocumentos2.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Mes\";\"Fecha egreso\";\"Regional\";\"Unis\";\"Tipo documento\";\"Documento\";\"Nombre\";\"Edad\";\"IPS\";\"CIE 10\";\"Diagnostico\";\"Telefono\";\"Celular\";\"DirecciOn\";\"Auditor\";\"Mega\";\"Regional Beneficiario\"");

                foreach (var line in Model.ListaegresoDocumentos2)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\"",
                                               line.mes,
                                               line.fecha_egreso_for,
                                               line.regional,
                                               line.unis,
                                               line.afi_tipo_doc,
                                               line.documento,
                                               line.afi_nom,
                                               line.afi_edad,
                                               line.Nombre_Ips,
                                               line.DxprincipalEgreso,
                                               line.nombre_cie10,
                                               line.afi_tel,
                                               line.afi_cel,
                                               line.Direccion_pac,
                                               line.Auditor,
                                               line.MEGA,
                                               line.Regional_Beneficiario)
                        );
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaEgreso" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaEgresoRegional(Models.Consulta.Consulta Model)
        {
            if (Model.ListaegresoRegional2.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Mes\";\"Fecha egreso\";\"Regional\";\"Unis\";\"Tipo documento\";\"Documento\";\"Nombre\";\"Edad\";\"IPS\";\"CIE 10\";\"Diagnostico\";\"Telefono\";\"Celular\";\"DirecciOn\";\"Auditor\";\"Mega\";\"Regional Beneficiario\"");

                foreach (var line in Model.ListaegresoRegional2)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\"",
                                               line.mes,
                                               line.fecha_egreso_for,
                                               line.regional,
                                               line.unis,
                                               line.afi_tipo_doc,
                                               line.documento,
                                               line.afi_nom,
                                               line.afi_edad,
                                               line.Nombre_Ips,
                                               line.DxprincipalEgreso,
                                               line.nombre_cie10,
                                               line.afi_tel,
                                               line.afi_cel,
                                               line.Direccion_pac,
                                               line.Auditor,
                                               line.MEGA,
                                               line.Regional_Beneficiario)
                        );
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaEgreso" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private static ExcelPackage GenerateExcelFileFacturasGestionadas2(List<vw_consulta_egreso> datasource)
        {

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "Mes";
            ws.Cells[1, 2].Value = "Fecha egraso";
            ws.Cells[1, 3].Value = "Regional";
            ws.Cells[1, 4].Value = "Unis";
            ws.Cells[1, 5].Value = "Tipo documento";
            ws.Cells[1, 6].Value = "Documento";
            ws.Cells[1, 7].Value = "Num factura";
            ws.Cells[1, 8].Value = "Valor neto";
            ws.Cells[1, 9].Value = "Ciudad";
            ws.Cells[1, 10].Value = "Regional";
            ws.Cells[1, 11].Value = "Fecha RecepciOn";
            ws.Cells[1, 12].Value = "Estado Actual";
            ws.Cells[1, 13].Value = "Fecha AprobaciOn";
            ws.Cells[1, 14].Value = "Analista";
            ws.Cells[1, 15].Value = "Auditor";
            ws.Cells[1, 16].Value = "Tipos gasto aplicado";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).mes;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).fecha_egreso;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).regional;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).unis;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).afi_tipo_doc;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).documento;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).afi_nom;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).afi_edad;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).Nombre_Ips;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).DxprincipalEgreso;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).nombre_cie10;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).afi_tel;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).afi_cel;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).Direccion_pac;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).Auditor;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).MEGA;
            }

            ws.Cells["A:Z"].AutoFitColumns();
            using (ExcelRange rng = ws.Cells["A1:Z1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        private ActionResult ExportaConsultaEventosFechas(Models.Consulta.Consulta Model)
        {

            if (Model.ListaEventosFechas.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"Medico_Auditor\";\"regional\";\"unis\";\"fecha_evento_adv\";\"Evento_adverso\";\"descripcion_Evento\";\"GradoLesion\";\"PlanManejo\";\"BarreraSeguridad\";\"FactoresContribuyentes\";\"AccionesInseguras\";\"fecha_de_radica_carta\";\"quien_recibe\"");

                foreach (var line in Model.ListaEventosFechas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"24\"",
                                               line.id_censo,
                                               line.id_concurrencia,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.fecha_recepcion_censo,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.Medico_Auditor,
                                               line.regional,
                                               line.unis,
                                               line.fecha_evento_adv,
                                               line.Evento_adverso,
                                               line.descripcion_Evento,
                                               line.GradoLesion,
                                               line.PlanManejo,
                                               line.BarreraSeguridad,
                                               line.FactoresContribuyentes,
                                               line.AccionesInseguras,
                                               line.fecha_de_radica_carta,
                                               line.quien_recibe));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaEventosAdversos" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaEventosDocumento(Models.Consulta.Consulta Model)
        {

            if (Model.ListaEventosDocumento.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"Medico_Auditor\";\"regional\";\"unis\";\"fecha_evento_adv\";\"Evento_adverso\";\"descripcion_Evento\";\"GradoLesion\";\"PlanManejo\";\"BarreraSeguridad\";\"FactoresContribuyentes\";\"AccionesInseguras\";\"fecha_de_radica_carta\";\"quien_recibe\"");

                foreach (var line in Model.ListaEventosDocumento)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"24\"",
                                               line.id_censo,
                                               line.id_concurrencia,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.fecha_recepcion_censo,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.Medico_Auditor,
                                               line.regional,
                                               line.unis,
                                               line.fecha_evento_adv,
                                               line.Evento_adverso,
                                               line.descripcion_Evento,
                                               line.GradoLesion,
                                               line.PlanManejo,
                                               line.BarreraSeguridad,
                                               line.FactoresContribuyentes,
                                               line.AccionesInseguras,
                                               line.fecha_de_radica_carta,
                                               line.quien_recibe));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaEventosAdversos" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaSituacionCalFechas(Models.Consulta.Consulta Model)
        {

            if (Model.ListaSituacionCalidadFechas.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"Medico_Auditor\";\"regional\";\"unis\";\"fecha_situcion\";\"SituacionCalidad\";\"descripcion_situacion\";\"fecha_de_radica_carta\";\"quien_recibe\"");

                foreach (var line in Model.ListaSituacionCalidadFechas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\"",
                                               line.id_censo,
                                               line.id_concurrencia,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.fecha_recepcion_censo,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.Medico_Auditor,
                                               line.regional,
                                               line.unis,
                                               line.fecha_situcion,
                                               line.SituacionCalidad,
                                               line.descripcion_situacion,
                                               line.fecha_de_radica_carta,
                                               line.quien_recibe));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaSituacionCalidad" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaSituacionCalDocumento(Models.Consulta.Consulta Model)
        {

            if (Model.ListaSituacionCalidadDocumento.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"id_concurrencia\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"fecha_recepcion_censo\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"ips_primaria\";\"Nombre_Ips\";\"CiudadIPs\";\"Medico_Auditor\";\"regional\";\"unis\";\"fecha_situcion\";\"SituacionCalidad\";\"descripcion_situacion\";\"fecha_de_radica_carta\";\"quien_recibe\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaSituacionCalidad" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaSituacionCalidadDocumento)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\"",
                                               line.id_censo,
                                               line.id_concurrencia,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.fecha_recepcion_censo,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.ips_primaria,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.Medico_Auditor,
                                               line.regional,
                                               line.unis,
                                               line.fecha_situcion,
                                               line.SituacionCalidad,
                                               line.descripcion_situacion,
                                               line.fecha_de_radica_carta,
                                               line.quien_recibe));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaSituacionCalidad" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaGestantesFechas(Models.Consulta.Consulta Model)
        {
            if (Model.ListaGestantesFechas.Count != 0)
            {
                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Numero_identificacion\";\"Clase_de_identificacion\";\"Apellidos\";\"Nombre\";\"edad_gestacional\";\"fecha_nacimiento\";\"peso\";\"via_parto\";\"talla\";\"sexo\";\"apgar\";\"control_prenatal\";\"Nombre_Ips\";\"CiudadIPs\";\"regional_asalud\";\"fecha_digita\";\"usuario_digita\";\"fecha_BCG\";\"fecha_vitaminaK\";\"fecha_consenjeria_lactancia\";\"resultadoTCH\";\"fechaTCH\";\"fecha_consulta_pediatria\";\"fecha_hepatitisB\";\"acompañamiento_parto\"");

                foreach (var line in Model.ListaGestantesFechas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{21}\";\"{23}\";\"{24}\"",
                                               line.Numero_identificacion,
                                               line.Clase_de_identificacion,
                                               line.Apellidos,
                                               line.Nombre,
                                               line.edad_gestacional,
                                               line.fecha_nacimiento,
                                               line.peso,
                                               line.via_parto,
                                               line.talla,
                                               line.sexo,
                                               line.apgar,
                                               line.control_prenatal,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.regional_asalud,
                                               line.fecha_digita,
                                               line.usuario_digita,
                                               line.fecha_BCG,
                                               line.fecha_vitaminaK,
                                               line.fecha_consenjeria_lactancia,
                                               line.resultadoTCH,
                                               line.fechaTCH,
                                               line.fecha_consulta_pediatria,
                                               line.fecha_hepatitisB,
                                               line.acompañamiento_parto));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaGestantes" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        private ActionResult ExportaConsultaGestantesFechasCompleto(Models.Consulta.Consulta Model)
        {
            try
            {
                if (Model.ListaGestantesFechasNuevo.Count != 0)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("\"Regional Pertenece\";\"Identificacion de la Madre\";\"Apellidos de la Madre\";\"Nombres de la Madre\";\"IDENTIFICACION RC\";\"NOMBRE DEL MENOR\";\"TSH\";\"BCG\";\"HEPATITIS B\";\"Mes\";\"Trimestre\";\"Ano\";\"Localidad Evento\";\"Descripcion Ciudad SMed\";\"Unis\";\"Fecha de Nacimiento\";\"Edad Gestacional\";\"Peso\";\"Via del Parto\";\"Talla\";\"Sexo\";\"Apgar\";\"Causa Egreso\";\"Control Prenatal 0=SI - 1=NO\";\"Fecha\";\"Observaciones\";\" Nombre de quien reporta\"");

                    foreach (var line in Model.ListaGestantesFechasNuevo)
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\"",
                                                   line.regional,
                                                   line.identificacion_madre,
                                                   line.apellidos_madre,
                                                   line.nombre_madre,
                                                   line.identificacion_rc,
                                                   line.nombre_menor,
                                                   line.resultadoTCH,
                                                   line.bcg,
                                                   line.hepatitis,
                                                   line.mes,
                                                   line.trimestre,
                                                   line.año,
                                                   line.localidad_evento,
                                                   line.descripcion_ciudad,
                                                   line.unis,
                                                   line.fecha_nacimiento,
                                                   line.edad_gestacional,
                                                   line.peso,
                                                   line.via_parto,
                                                   line.talla,
                                                   line.sexo,
                                                   line.apgar,
                                                   line.causal_egreso,
                                                   line.control_prenatal,
                                                   line.fecha,
                                                   line.observaciones,
                                                   line.nombre_reporta
                                                   ));
                    }

                    byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "ConsultaGestantes" + DateTime.Now + ".csv");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de procesar la solicitud" });
            }
        }

        private ActionResult ExportaConsultaGestantesRegionalCompleto(Models.Consulta.Consulta Model)
        {
            try
            {
                if (Model.ListaGestantesRegionalNueva.Count != 0)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("\"Regional Pertenece\";\"Identificacion de la Madre\";\"Apellidos de la Madre\";\"Nombres de la Madre\";\"IDENTIFICACION RC\";\"NOMBRE DEL MENOR\";\"TSH\";\"BCG\";\"HEPATITIS B\";\"Mes\";\"Trimestre\";\"Ano\";\"Localidad Evento\";\"Descripcion Ciudad SMed\";\"Unis\";\"Fecha de Nacimiento\";\"Edad Gestacional\";\"Peso\";\"Via del Parto\";\"Talla\";\"Sexo\";\"Apgar\";\"Causa Egreso\";\"Control Prenatal 0=SI - 1=NO\";\"Fecha\";\"Observaciones\";\" Nombre de quien reporta\"");

                    foreach (var line in Model.ListaGestantesRegionalNueva)
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{24}\";\"{25}\";\"{26}\"",
                                                   line.regional,
                                                   line.identificacion_madre,
                                                   line.apellidos_madre,
                                                   line.nombre_madre,
                                                   line.identificacion_rc,
                                                   line.nombre_menor,
                                                   line.resultadoTCH,
                                                   line.bcg,
                                                   line.hepatitis,
                                                   line.mes,
                                                   line.trimestre,
                                                   line.año,
                                                   line.localidad_evento,
                                                   line.descripcion_ciudad,
                                                   line.unis,
                                                   line.fecha_nacimiento,
                                                   line.edad_gestacional,
                                                   line.peso,
                                                   line.via_parto,
                                                   line.talla,
                                                   line.sexo,
                                                   line.apgar,
                                                   line.causal_egreso,
                                                   line.control_prenatal,
                                                   line.fecha,
                                                   line.observaciones,
                                                   line.nombre_reporta

                                                   ));
                    }

                    byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "ConsultaGestantes" + DateTime.Now + ".csv");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de procesar la solicitud " });
            }
        }

        private ActionResult ExportaConsultaGestantesRegional(Models.Consulta.Consulta Model)
        {
            try
            {
                if (Model.ListaGestantesRegional.Count != 0)
                {

                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"Numero_identificacion\";\"Clase_de_identificacion\";\"Apellidos\";\"Nombre\";\"edad_gestacional\";\"fecha_nacimiento\";\"peso\";\"via_parto\";\"talla\";\"sexo\";\"apgar\";\"control_prenatal\";\"Nombre_Ips\";\"CiudadIPs\";\"regional_asalud\";\"fecha_digita\";\"usuario_digita\";\"fecha_BCG\";\"fecha_vitaminaK\";\"fecha_consenjeria_lactancia\";\"resultadoTCH\";\"fechaTCH\";\"fecha_consulta_pediatria\";\"fecha_hepatitisB\";\"acompañamiento_parto\"");


                    foreach (var line in Model.ListaGestantesRegional)
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{21}\";\"{23}\";\"{24}\"",
                                                line.Numero_identificacion,
                                                line.Clase_de_identificacion,
                                                line.Apellidos,
                                                line.Nombre,
                                                line.edad_gestacional,
                                                line.fecha_nacimiento,
                                                line.peso,
                                                line.via_parto,
                                                line.talla,
                                                line.sexo,
                                                line.apgar,
                                                line.control_prenatal,
                                                line.Nombre_Ips,
                                                line.CiudadIPs,
                                                line.regional_asalud,
                                                line.fecha_digita,
                                                line.usuario_digita,
                                                line.fecha_BCG,
                                                line.fecha_vitaminaK,
                                                line.fecha_consenjeria_lactancia,
                                                line.resultadoTCH,
                                                line.fechaTCH,
                                                line.fecha_consulta_pediatria,
                                                line.fecha_hepatitisB,
                                                line.acompañamiento_parto));
                    }

                    byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "ConsultaGestantes" + DateTime.Now + ".csv");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de procesar la solicitud " });
            }
        }

        private ActionResult ExportaConsultaGestantesSinFechas(Models.Consulta.Consulta Model)
        {

            if (Model.ListaGestantesSinFechas.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Numero_identificacion\";\"Clase_de_identificacion\";\"Apellidos\";\"Nombre\";\"edad_gestacional\";\"fecha_nacimiento\";\"peso\";\"via_parto\";\"talla\";\"sexo\";\"apgar\";\"control_prenatal\";\"Nombre_Ips\";\"CiudadIPs\";\"fecha_digita\";\"usuario_digita\";\"fecha_BCG\";\"fecha_vitaminaK\";\"fecha_consenjeria_lactancia\";\"resultadoTCH\";\"fechaTCH\";\"fecha_consulta_pediatria\";\"fecha_hepatitisB\"");

                foreach (var line in Model.ListaGestantesSinFechas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\"",
                                               line.Numero_identificacion,
                                               line.Clase_de_identificacion,
                                               line.Apellidos,
                                               line.Nombre,
                                               line.edad_gestacional,
                                               line.fecha_nacimiento,
                                               line.peso,
                                               line.via_parto,
                                               line.talla,
                                               line.sexo,
                                               line.apgar,
                                               line.control_prenatal,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.fecha_digita,
                                               line.usuario_digita,
                                               line.fecha_BCG,
                                               line.fecha_vitaminaK,
                                               line.fecha_consenjeria_lactancia,
                                               line.resultadoTCH,
                                               line.fechaTCH,
                                               line.fecha_consulta_pediatria,
                                               line.fecha_hepatitisB));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaGestantesSinConcurrencia" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaConsultaMortalidadFechas(Models.Consulta.Consulta Model)
        {

            List<ManagementConsultaConcuMortalidadCon_SinResult> List = BusClass.ConsultaMortalidadV2(1, ref MsgRes);
            List = List.Where(x => x.fecha_digita >= Model.fecha_inicio && x.fecha_digita <= Model.fecha_fin).ToList();

            if (List.Count != 0)
            {

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consulta mortalidad");

                Sheet.Cells["A1"].Value = "TRIMESTRE DEL AÑO";
                Sheet.Cells["B1"].Value = "MES EN NUMERO";
                Sheet.Cells["C1"].Value = "MES EN DESCRIPCION";
                Sheet.Cells["D1"].Value = "AÑO";
                Sheet.Cells["E1"].Value = "ID LOCALIDAD O CIUDAD DE OCURRENCIA DEL FALLECIMIENTO";
                Sheet.Cells["F1"].Value = "UNIS - DE LA IPS DONDE FALLECE";
                Sheet.Cells["G1"].Value = "REGIONAL -DE LA IPS DONDE FALLECE";
                Sheet.Cells["H1"].Value = "NO IDENTIFICACION";
                Sheet.Cells["I1"].Value = "APELLIDOS";
                Sheet.Cells["J1"].Value = "NOMBRES";
                Sheet.Cells["K1"].Value = "EDAD";
                Sheet.Cells["L1"].Value = "GENERO";
                Sheet.Cells["M1"].Value = "NO. CERTIFICADO DEFUNCION";
                Sheet.Cells["N1"].Value = "FECHA FALLECIMIENTO";
                Sheet.Cells["O1"].Value = "CIE 10 (4 CARACTERES)";
                Sheet.Cells["P1"].Value = "DESCRIPCION DEL CIE10";
                Sheet.Cells["Q1"].Value = "CIUDAD RADICACION SERV. DE SALUD";
                Sheet.Cells["R1"].Value = "TIPO-TRABAJADOR O BENEFICIARIO";
                Sheet.Cells["S1"].Value = "NOMBRE PERSONA QUE REPORTA";
                Sheet.Cells["T1"].Value = "IPS EN DONDE OCURRE EL FALLECIMIENTO";
                Sheet.Cells["U1"].Value = "OBSERVACIONES/CAUSA DETALLADA DE LA MUERTE (NOTA AUDITORÍA)";

                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.trimestre_del_año;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_mes;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.localidad_ciudad_ocurrencia_falecimiento;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.unis_ips_fallecimiento;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.regional_ips_fallecimiento;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.no_identificacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.apellido;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.nombre;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.edad;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.genero;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.num_certificado_defuncion;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_fallecimiento;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.cie10;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.descripcion_cie10;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.ciudad_radicacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.tipo;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.persona_reportante;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.ips_fallecimiento;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.observaciones_fallecimiento;

                    row++;
                }

                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A1:U1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:U1"].Style.Font.Color.SetColor(Color.White);

                return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsolidadoMortalidad" + DateTime.Now + ".xls");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        public ActionResult ExportaConsultaMortalidadRegional(Models.Consulta.Consulta Model)
        {
            List<ManagementConsultaConcuMortalidadCon_SinResult> List = BusClass.ConsultaMortalidadV2(1, ref MsgRes);
            List = List.Where(x => x.regional_id == Model.regional).ToList();
            List = List.Where(x => x.fecha_digita >= Model.fecha_inicio && x.fecha_digita <= Model.fecha_fin).ToList();

            if (List.Count != 0)
            {

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consulta mortalidad");

                Sheet.Cells["A1"].Value = "TRIMESTRE DEL AÑO";
                Sheet.Cells["B1"].Value = "MES EN NUMERO";
                Sheet.Cells["C1"].Value = "MES EN DESCRIPCION";
                Sheet.Cells["D1"].Value = "AÑO";
                Sheet.Cells["E1"].Value = "ID LOCALIDAD O CIUDAD DE OCURRENCIA DEL FALLECIMIENTO";
                Sheet.Cells["F1"].Value = "UNIS - DE LA IPS DONDE FALLECE";
                Sheet.Cells["G1"].Value = "REGIONAL -DE LA IPS DONDE FALLECE";
                Sheet.Cells["H1"].Value = "NO IDENTIFICACION";
                Sheet.Cells["I1"].Value = "APELLIDOS";
                Sheet.Cells["J1"].Value = "NOMBRES";
                Sheet.Cells["K1"].Value = "EDAD";
                Sheet.Cells["L1"].Value = "GENERO";
                Sheet.Cells["M1"].Value = "NO. CERTIFICADO DEFUNCION";
                Sheet.Cells["N1"].Value = "FECHA FALLECIMIENTO";
                Sheet.Cells["O1"].Value = "CIE 10 (4 CARACTERES)";
                Sheet.Cells["P1"].Value = "DESCRIPCION DEL CIE10";
                Sheet.Cells["Q1"].Value = "CIUDAD RADICACION SERV. DE SALUD";
                Sheet.Cells["R1"].Value = "TIPO-TRABAJADOR O BENEFICIARIO";
                Sheet.Cells["S1"].Value = "NOMBRE PERSONA QUE REPORTA";
                Sheet.Cells["T1"].Value = "IPS EN DONDE OCURRE EL FALLECIMIENTO";
                Sheet.Cells["U1"].Value = "OBSERVACIONES/CAUSA DETALLADA DE LA MUERTE (NOTA AUDITORÍA)";

                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.trimestre_del_año;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_mes;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.localidad_ciudad_ocurrencia_falecimiento;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.unis_ips_fallecimiento;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.regional_ips_fallecimiento;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.no_identificacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.apellido;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.nombre;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.edad;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.genero;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.num_certificado_defuncion;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_fallecimiento;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.cie10;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.descripcion_cie10;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.ciudad_radicacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.tipo;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.persona_reportante;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.ips_fallecimiento;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.observaciones_fallecimiento;

                    row++;
                }

                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A1:U1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:U1"].Style.Font.Color.SetColor(Color.White);

                return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsolidadoMortalidad" + DateTime.Now + ".xls");

            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }
        }

        public ActionResult ExportaConsultaMortalidadSinFechas(Models.Consulta.Consulta Model)
        {
            List<ManagementConsultaConcuMortalidadCon_SinResult> List = BusClass.ConsultaMortalidadV2(2, ref MsgRes);
            List = List.Where(x => x.fecha_digita >= Model.fecha_inicio && x.fecha_digita <= Model.fecha_fin).ToList();

            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consulta mortalidad sin concurrencia");

                Sheet.Cells["A1"].Value = "TRIMESTRE DEL AÑO";
                Sheet.Cells["B1"].Value = "MES EN NUMERO";
                Sheet.Cells["C1"].Value = "MES EN DESCRIPCION";
                Sheet.Cells["D1"].Value = "AÑO";
                Sheet.Cells["E1"].Value = "ID LOCALIDAD O CIUDAD DE OCURRENCIA DEL FALLECIMIENTO";
                Sheet.Cells["F1"].Value = "UNIS - DE LA IPS DONDE FALLECE";
                Sheet.Cells["G1"].Value = "REGIONAL -DE LA IPS DONDE FALLECE";
                Sheet.Cells["H1"].Value = "NO IDENTIFICACION";
                Sheet.Cells["I1"].Value = "APELLIDOS";
                Sheet.Cells["J1"].Value = "NOMBRES";
                Sheet.Cells["K1"].Value = "EDAD";
                Sheet.Cells["L1"].Value = "GENERO";
                Sheet.Cells["M1"].Value = "NO. CERTIFICADO DEFUNCION";
                Sheet.Cells["N1"].Value = "FECHA FALLECIMIENTO";
                Sheet.Cells["O1"].Value = "CIE 10 (4 CARACTERES)";
                Sheet.Cells["P1"].Value = "DESCRIPCION DEL CIE10";
                Sheet.Cells["Q1"].Value = "CIUDAD RADICACION SERV. DE SALUD";
                Sheet.Cells["R1"].Value = "TIPO-TRABAJADOR O BENEFICIARIO";
                Sheet.Cells["S1"].Value = "NOMBRE PERSONA QUE REPORTA";
                Sheet.Cells["T1"].Value = "IPS EN DONDE OCURRE EL FALLECIMIENTO";
                Sheet.Cells["U1"].Value = "OBSERVACIONES/CAUSA DETALLADA DE LA MUERTE (NOTA AUDITORÍA)";

                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.trimestre_del_año;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.id_mes;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.localidad_ciudad_ocurrencia_falecimiento;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.unis_ips_fallecimiento;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.regional_ips_fallecimiento;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.no_identificacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.apellido;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.nombre;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.edad;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.genero;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.num_certificado_defuncion;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.fecha_fallecimiento;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.cie10;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.descripcion_cie10;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = line.ciudad_radicacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = line.tipo;
                    Sheet.Cells[string.Format("S{0}", row)].Value = line.persona_reportante;
                    Sheet.Cells[string.Format("T{0}", row)].Value = line.ips_fallecimiento;
                    Sheet.Cells[string.Format("U{0}", row)].Value = line.observaciones_fallecimiento;

                    row++;
                }

                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A:U"].AutoFitColumns();
                Sheet.Cells["A1:U1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:U1"].Style.Font.Color.SetColor(Color.White);

                return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsolidadoMortalidadSinConcurrencia" + DateTime.Now + ".xls");

            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaConsulta1(Models.Consulta.Consulta Model)
        {
            if (Model.ListaAlertas.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_concurrencia\";\"id_censo\";\"Documento_Afiliado\";\"afi_nom\";\"edad\";\"Diagnostico_Censo\";\"Nombre_Diagnostico_Censo\";\"Nombre_auditor\";\"CiudadIPs\";\"Nombre_Ips\";\"Diagnostico_1_Evolu\";\"Nombre_Diagnostico_1_Evolu\";\"Diagnostico_2_Evolu\";\"Nombre_Diagnostico_2_Evolu\";\"Diagnostico_3_Evolu\";\"Nombre_Diagnostico_3_Evolu\";\"Diagnostico_4_Evolu\";\"Nombre_Diagnostico_4_Evolu\";\"fecha_digita\";\"fecha_ingreso\";\"fecha_egreso\";\"Diagnostico_Egreso\";\"Nombre_Diagnostico_Egreso\";\"Incapacidad\";\"Fecha_Inicial_Incapacidad\";\"Fecha_final_Incapacidad\"");



                foreach (var line in Model.ListaAlertas)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\"",
                                               line.id_concurrencia,
                                               line.id_censo,
                                               line.Documento_Afiliado,
                                               line.afi_nom,
                                               line.edad,
                                               line.Diagnostico_Censo,
                                               line.Nombre_Diagnostico_Censo,
                                               line.Nombre_auditor,
                                               line.CiudadIPs,
                                               line.Nombre_Ips,
                                               line.Diagnostico_1_Evolu,
                                               line.Nombre_Diagnostico_1_Evolu,
                                               line.Diagnostico_2_Evolu,
                                               line.Nombre_Diagnostico_2_Evolu,
                                               line.Diagnostico_3_Evolu,
                                               line.Nombre_Diagnostico_3_Evolu,
                                               line.Diagnostico_4_Evolu,
                                               line.Nombre_Diagnostico_4_Evolu,
                                               line.fecha_digita,
                                               line.fecha_ingreso,
                                               line.fecha_egreso,
                                               line.Diagnostico_Egreso,
                                               line.Nombre_Diagnostico_Egreso,
                                               line.Incapacidad,
                                               line.Fecha_Inicial_Incapacidad,
                                               line.Fecha_final_Incapacidad));



                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaAlertas" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaDevolucionFechas(Models.Consulta.Consulta Model)
        {
            if (Model.LstDevoluciones.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_devolucion_factura\";\"NumeroFactura\";\"Fecha_Recepcion\";\"Fecha_Devolucion\";\"Nombre_Ips\";\"Ciudad\";\"Nombre_Regional\";\"valor_factura\";\"ValorGlosa\";\"Motivo_Devolucion\";\"Observacion\";\"nombre\";\"Numero_Factura_2\";\"Valor_Factura_2\";\"Fecha_Factura_2\";\"Fecha_Recepcion_2\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Devoluciones" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.LstDevoluciones)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\"",
                                               line.id_devolucion_factura,
                                               line.NumeroFactura,
                                               line.Fecha_Recepcion,
                                               line.Fecha_Devolucion,
                                               line.Nombre_Ips,
                                               line.Ciudad,
                                               line.Nombre_Regional,
                                               line.valor_factura,
                                               line.ValorGlosa,
                                               line.Motivo_Devolucion,
                                               line.Observacion,
                                               line.nombre,
                                               line.Numero_Factura_2,
                                               line.Valor_Factura_2,
                                               line.Fecha_Factura_2,
                                               line.Fecha_Recepcion_2));

                }
                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "Devoluciones" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaHallazgosRIPSFechas(Models.Consulta.Consulta Model)
        {
            if (Model.LstHallazgos.Count != 0)
            {
                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_hallazgo_RIPS\";\"numero_factura\";\"Fecha_reporte_hallazgo\";\"Nombre_Regional\";\"Ciudad\";\"Nombre_Proovedor\";\"Tipo_Hallazgo\";\"Descripcion_Hallazgo\";\"Fecha_recepcion\";\"fecha_digita\";\"usuario_digita\"");

                foreach (var line in Model.LstHallazgos)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\"",
                            line.id_hallazgo_RIPS,
                            line.numero_factura,
                            line.Fecha_reporte_hallazgo,
                            line.Nombre_Regional,
                            line.Ciudad,
                            line.Nombre_Proovedor,
                            line.Tipo_Hallazgo,
                            line.Descripcion_Hallazgo,
                            line.Fecha_recepcion,
                            line.fecha_digita,
                            line.usuario_digita));
                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "HallazgosRIPS" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaRecepcionFacturas(Models.Consulta.Consulta Model)
        {
            if (Model.ListRecepcion.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_factura_sin_censo\";\"numero_factura\";\"fecha_factura\";\"fecha_recepcion\";\"Nombre_Ips\";\"Ciudad\";\"valor_factura\";\"tipo_factura\";\"valor_factura_definitiva\";\"digita_fecha\";\"auditada\"");


                foreach (var line in Model.ListRecepcion)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\"",
                                               line.id_factura_sin_censo,
                                               line.numero_factura,
                                               line.fecha_factura,
                                               line.fecha_recepcion,
                                               line.Nombre_Ips,
                                               line.Ciudad,
                                               line.valor_factura,
                                               line.tipo_factura,
                                               line.valor_factura_definitiva,
                                               line.digita_fecha,
                                               line.auditada));

                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaRecepcionFacturas" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        private ActionResult ExportaRecepcionFacturasSin(Models.Consulta.Consulta Model)
        {
            if (Model.ListRecepcionSin.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_factura_sin_censo\";\"numero_factura\";\"fecha_factura\";\"fecha_recepcion\";\"Nombre_Ips\";\"Ciudad\";\"valor_factura\";\"tipo_factura\";\"valor_factura_definitiva\";\"digita_fecha\";\"auditada\"");

                foreach (var line in Model.ListRecepcionSin)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\"",
                                               line.id_factura_sin_censo,
                                               line.numero_factura,
                                               line.fecha_factura,
                                               line.fecha_recepcion,
                                               line.Nombre_Ips,
                                               line.Ciudad,
                                               line.valor_factura,
                                               line.tipo_factura,
                                               line.valor_factura_definitiva,
                                               line.digita_fecha,
                                               line.auditada));

                }

                byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
                return File(bytes, "text/csv", "ConsultaRecepcionFacturas" + DateTime.Now + ".csv");
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
            }

        }

        /// <summary>
        /// Exportar Excel reporte fecha consulta
        /// </summary>
        /// <param name="Model"></param>
        public ActionResult ExportaRIPSFechaConsulta(Models.Consulta.Consulta Model)
        {
            if (DateTime.Compare(Model.fecha_inicio.Value, Model.fecha_fin.Value) < 0)
            {
                var rslt = Model.GetListRipsFechaCosnsulta(Model.fecha_inicio.Value, Model.fecha_fin.Value);

                if (rslt.Count() > 0)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:V1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:V1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:V1"].Style.Font.Size = 14;
                    Sheet.Cells["A1:V1"].Style.Font.Bold = true;

                    Sheet.Cells["A1"].Value = "Codigo_Prestador";
                    Sheet.Cells["B1"].Value = "Nombre del prestador";
                    Sheet.Cells["C1"].Value = "Tipo IdentificaciOn usuario";
                    Sheet.Cells["D1"].Value = "Numero Identificacion usuario";
                    Sheet.Cells["E1"].Value = "Nombres";
                    Sheet.Cells["F1"].Value = "Fecha Consulta";
                    Sheet.Cells["G1"].Value = "Codigo Consulta";
                    Sheet.Cells["H1"].Value = "Finalidad Consulta";
                    Sheet.Cells["I1"].Value = "Codigo dx principal";
                    Sheet.Cells["J1"].Value = "Descripcion dx principal";
                    Sheet.Cells["K1"].Value = "COdigo del diagnOstico relacionado N° 1";
                    Sheet.Cells["L1"].Value = "Descripcion Diagnostico";
                    Sheet.Cells["M1"].Value = "COdigo del diagnOstico relacionado N° 2";
                    Sheet.Cells["N1"].Value = "Descripcion Diagnostico";
                    Sheet.Cells["O1"].Value = "COdigo del diagnOstico relacionado N° 3";
                    Sheet.Cells["P1"].Value = "Descripcion Diagnostico";
                    Sheet.Cells["Q1"].Value = "Tipo Diagnostico Principal";
                    Sheet.Cells["R1"].Value = "Causa Externa";
                    Sheet.Cells["S1"].Value = "Numero Factura";
                    Sheet.Cells["T1"].Value = "Valor Consulta";
                    Sheet.Cells["U1"].Value = "Regional";
                    Sheet.Cells["V1"].Value = "Ciudad Prestador";
                    int row = 2;
                    int count = 0;
                    foreach (var item in rslt)
                    {
                        if (count <= 1000000)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = item.codigo_prestador;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.Razon_social;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.tipo_id_usuario;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.num_id_usuario;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.nombre;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_consulta.Value.ToString("dd/MM/yyyy");
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.cod_consulta;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.finalidad_consulta;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.cod_dx_ppal;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.descripcion_dx;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.cod_dx_rel_1;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcion_dx_1;
                            Sheet.Cells[string.Format("M{0}", row)].Value = item.cod_dx_rel_2;
                            Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcion_dx_2;
                            Sheet.Cells[string.Format("O{0}", row)].Value = item.cod_dx_rel_3;
                            Sheet.Cells[string.Format("P{0}", row)].Value = item.descripcion_dx_3;
                            Sheet.Cells[string.Format("Q{0}", row)].Value = item.tipo_dx_ppal;
                            Sheet.Cells[string.Format("R{0}", row)].Value = item.causa_externa;
                            Sheet.Cells[string.Format("S{0}", row)].Value = item.num_factura;
                            Sheet.Cells[string.Format("T{0}", row)].Value = item.valor_consulta;
                            Sheet.Cells[string.Format("U{0}", row)].Value = item.regional;
                            Sheet.Cells[string.Format("V{0}", row)].Value = item.ciudad;
                            row++;
                        }
                        count += 1;
                        if (count > 1000000)
                        {
                            break;
                        }

                    }

                    Sheet.Cells["A:AZ"].AutoFitColumns();

                    return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsultaRipsFechaConsulta" + DateTime.Now + ".xls");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "La fecha final no puede ser menor a la fecha inicial" });
            }
        }

        /// <summary>
        /// Exportar excel reporte fecha de procedimiento
        /// </summary>
        /// <param name="Model"></param>
        public ActionResult ExportaRIPSFechaProcedimiento(Models.Consulta.Consulta Model)
        {
            if (DateTime.Compare(Model.fecha_inicio.Value, Model.fecha_fin.Value) < 0)
            {
                var rslt = Model.GetListRipsFechaProcedimiento(Model.regional, Model.fecha_inicio.Value, Model.fecha_fin.Value);

                if (rslt.Count() > 0)
                {
                    Session["consultaprocedimiento"] = rslt;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:S1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:S1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:S1"].Style.Font.Size = 14;
                    Sheet.Cells["A1:S1"].Style.Font.Bold = true;

                    Sheet.Cells["A1"].Value = "Codigo_Prestador";
                    Sheet.Cells["B1"].Value = "Nombre del prestador";
                    Sheet.Cells["C1"].Value = "Tipo identificacion usuario";
                    Sheet.Cells["D1"].Value = "Numero Identificacion usuario";
                    Sheet.Cells["E1"].Value = "Nombres";
                    Sheet.Cells["F1"].Value = "Fecha Procedimiento";
                    Sheet.Cells["G1"].Value = "Codigo Procedimiento";
                    Sheet.Cells["H1"].Value = "Nombre Procedimiento";
                    Sheet.Cells["I1"].Value = "Ambito Procedimiento";
                    Sheet.Cells["J1"].Value = "Finalidad Procedimiento";
                    Sheet.Cells["K1"].Value = "Acto Quirurjico";
                    Sheet.Cells["L1"].Value = "Numero Factura";
                    Sheet.Cells["M1"].Value = "Codigo dx principal";
                    Sheet.Cells["N1"].Value = "Descripcion dx principal";
                    Sheet.Cells["O1"].Value = "COdigo del diagnOstico relacionado N° 1";
                    Sheet.Cells["P1"].Value = "Descripcion Diagnostico";
                    Sheet.Cells["Q1"].Value = "Valor Procedimiento";
                    Sheet.Cells["R1"].Value = "Regional";
                    Sheet.Cells["S1"].Value = "Ciudad Prestador";
                    int row = 2;

                    foreach (var item in rslt)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.cod_prestador;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.tipo_id_usuario;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.num_id_usuario;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nombre_completo;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_procedimiento.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.cod_procedimiento;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_procedimiento;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.ambito;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.Finalidad_procedimiento;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.forma_acto_quirurjico;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.cod_dx_ppal;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.des_dx_ppal;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.cod_dx_rel;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.des_dx_rel;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.valor_procedimiento;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.regional;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.ciudad_prestador;
                        row++;
                    }

                    Sheet.Cells["A:AZ"].AutoFitColumns();

                    return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsultaRipsFechaProcedimiento" + DateTime.Now + ".xls");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "La fecha final no puede ser menor a la fecha inicial" });
            }
        }

        /// <summary>
        /// Exportar excel reporte fecha nacimiento
        /// </summary>
        /// <param name="Model"></param>
        public ActionResult ExportaRIPSFechaNacimiento(Models.Consulta.Consulta Model)
        {
            if (DateTime.Compare(Model.fecha_inicio.Value, Model.fecha_fin.Value) < 0)
            {
                var rslt = Model.GetListRipsFechaNacimiento(Model.fecha_inicio.Value, Model.fecha_fin.Value);

                if (rslt.Count() > 0)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:T1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:T1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:T1"].Style.Font.Size = 14;
                    Sheet.Cells["A1:T1"].Style.Font.Bold = true;

                    Sheet.Cells["A1"].Value = "Numero Factura";
                    Sheet.Cells["B1"].Value = "Valor Factura";
                    Sheet.Cells["C1"].Value = "Codigo Prestador";
                    Sheet.Cells["D1"].Value = "Nombre Prestador";
                    Sheet.Cells["E1"].Value = "Tipo identificacion de la madre";
                    Sheet.Cells["F1"].Value = "Numero identificacion de la madre";
                    Sheet.Cells["G1"].Value = "Nombre de madre";
                    Sheet.Cells["H1"].Value = "Fecha Nacimiento";
                    Sheet.Cells["I1"].Value = "Hora Nacimiento";
                    Sheet.Cells["J1"].Value = "Edad Gestacional";
                    Sheet.Cells["K1"].Value = "Control Prenatal";
                    Sheet.Cells["L1"].Value = "Sexo";
                    Sheet.Cells["M1"].Value = "Peso";
                    Sheet.Cells["N1"].Value = "Cod DX recien nacido";
                    Sheet.Cells["O1"].Value = "Descripcion Diagnostico";
                    Sheet.Cells["P1"].Value = "Causa Basica de muerte";
                    Sheet.Cells["Q1"].Value = "Fecha Muerte recien nacido";
                    Sheet.Cells["R1"].Value = "Hora Muerte recien nacido";
                    Sheet.Cells["S1"].Value = "Regional";
                    Sheet.Cells["T1"].Value = "Ciudad Prestador";
                    int row = 2;

                    foreach (var item in rslt)
                    {

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.num_factura;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.valor_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.cod_prestador;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.tipo_id_madre;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.num_id_madre;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.fecha_nacimiento_rn.Value.ToString("dd/MM/yyyy");
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.hora_nacimiento;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.edad_gestacional;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.control_prenatal;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.sexo;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.peso;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.dx_recien_nacido;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.descripcion_dx_recien_nacido;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.causa_muerte;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.fecha_muerte;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.hora_muerte;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.regional;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.ciudad_prestador;
                        row++;
                    }


                    Sheet.Cells["A:AZ"].AutoFitColumns();

                    return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsultaRipsFechaNacimiento" + DateTime.Now + ".xls");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                }
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "La fecha final no puede ser menor a la fecha inicial" });
            }
        }

        /// <summary>
        /// exportar excel reporte fecha mortalidad
        /// </summary>
        /// <param name="Model"></param>
        public ActionResult ExportaRIPSFechaMortandad(Models.Consulta.Consulta Model)
        {
            if (DateTime.Compare(Model.fecha_inicio.Value, Model.fecha_fin.Value) < 0)
            {
                var tipoarchivo = Model.tipo_archivo;

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:W1"].Style.Font.Size = 14;
                Sheet.Cells["A1:W1"].Style.Font.Bold = true;

                Sheet.Cells["A1"].Value = "Numero Factura";
                Sheet.Cells["B1"].Value = "Valor Factura";
                Sheet.Cells["C1"].Value = "Codigo Prestador";
                Sheet.Cells["D1"].Value = "Nombre Prestador";
                Sheet.Cells["E1"].Value = "Tipo Identificacion usuario";
                Sheet.Cells["F1"].Value = "Numero Identificacion usuario";
                Sheet.Cells["G1"].Value = "Nombre Usuario";
                Sheet.Cells["H1"].Value = "Estado Salida";
                Sheet.Cells["I1"].Value = "DX Causa basica de muerte";
                Sheet.Cells["J1"].Value = "Descripcion causa muerte";
                Sheet.Cells["K1"].Value = "DX egreso";
                Sheet.Cells["L1"].Value = "Descripcion Diagnostico";
                Sheet.Cells["M1"].Value = "DX Rel egreso 1";
                Sheet.Cells["N1"].Value = "Descripcion Diagnostico";
                Sheet.Cells["O1"].Value = "DX Rel egreso 2";
                Sheet.Cells["P1"].Value = "Descripcion Diagnostico";
                Sheet.Cells["Q1"].Value = "DX Rel egreso 3";
                Sheet.Cells["R1"].Value = "Descripcion Diagnostico";
                Sheet.Cells["S1"].Value = "Fecha Ingreso";
                Sheet.Cells["T1"].Value = "Hora Ingreso";
                Sheet.Cells["U1"].Value = "Fecha Egreso";
                Sheet.Cells["V1"].Value = "Hora Egreso";
                Sheet.Cells["W1"].Value = "Regional";
                int row = 2;

                if (Model.tipo_archivo == "AH")
                {
                    var rslt1 = Model.GetListRipsMortalidadAH(Model.fecha_inicio, Model.fecha_fin).ToList();
                    if (rslt1.Count() > 0)
                    {
                        foreach (var item in rslt1)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_factura;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.valor_factura;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_prestador;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.tipo_id_usuario;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.num_id_usuario;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre;
                            if (item.estado_saida == "2")
                            {
                                Sheet.Cells[string.Format("H{0}", row)].Value = "MUERTO";
                            }
                            else
                            {
                                Sheet.Cells[string.Format("H{0}", row)].Value = "VIVO";
                            }
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.dx_causa_basica_muerte;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.descripcion_dx_causa_basica_muerte;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.dx_salida;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcion_dx_salida;
                            Sheet.Cells[string.Format("M{0}", row)].Value = item.dx_rel_1_egreso;
                            Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcion_dx_egreso1;
                            Sheet.Cells[string.Format("O{0}", row)].Value = item.dx_rel_2_egreso;
                            Sheet.Cells[string.Format("P{0}", row)].Value = item.descripcion_dx_egreso2;
                            Sheet.Cells[string.Format("Q{0}", row)].Value = item.dx_rel_3_egreso;
                            Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcion_dx_egreso3;
                            Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                            Sheet.Cells[string.Format("T{0}", row)].Value = item.hora_ingreso;
                            Sheet.Cells[string.Format("U{0}", row)].Value = item.fecha_salida.Value.ToString("dd/MM/yyyy");
                            Sheet.Cells[string.Format("V{0}", row)].Value = item.hora_salida;
                            Sheet.Cells[string.Format("W{0}", row)].Value = item.regional;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();


                        return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsultaRipsFechaMortandad" + DateTime.Now + ".xls");
                    }
                    else
                    {
                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                    }

                }
                else if (Model.tipo_archivo == "AU")
                {
                    var rslt2 = Model.GetListRipsMortalidadAU(Model.fecha_inicio, Model.fecha_fin).ToList();
                    if (rslt2.Count() > 0)
                    {
                        foreach (var item in rslt2)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = item.numero_factura;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.valor_factura;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.codigo_prestador;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.tipo_id_usuario;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.num_id_usuario;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre;
                            if (item.estado_saida == "2")
                            {
                                Sheet.Cells[string.Format("H{0}", row)].Value = "MUERTO";
                            }
                            else
                            {
                                Sheet.Cells[string.Format("H{0}", row)].Value = "VIVO";
                            }
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.dx_causa_basica_muerte;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.descripcion_dx_causa_basica_muerte;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.dx_salida;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.descripcion_dx_salida;
                            Sheet.Cells[string.Format("M{0}", row)].Value = item.dx_rel_salida_1;
                            Sheet.Cells[string.Format("N{0}", row)].Value = item.descripcion_dx_salida1;
                            Sheet.Cells[string.Format("O{0}", row)].Value = item.dx_rel_salida_2;
                            Sheet.Cells[string.Format("P{0}", row)].Value = item.descripcion_dx_salida2;
                            Sheet.Cells[string.Format("Q{0}", row)].Value = item.dx_rel_salida_3;
                            Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcion_dx_salida3;
                            Sheet.Cells[string.Format("S{0}", row)].Value = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                            Sheet.Cells[string.Format("T{0}", row)].Value = item.hora_ingreso;
                            Sheet.Cells[string.Format("U{0}", row)].Value = item.fecha_salida.Value.ToString("dd/MM/yyyy");
                            Sheet.Cells[string.Format("V{0}", row)].Value = item.hora_salida;
                            Sheet.Cells[string.Format("W{0}", row)].Value = item.regional;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();

                        return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsultaRipsFechaMortandad" + DateTime.Now + ".xls");
                    }
                    else
                    {
                        return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Lo sentimos. No se ha encontrado ningún registro que coincida con los parámetros de búsqueda" });
                    }
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Error" });
                }
            }
            else
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "La fecha final no puede ser menor a la fecha inicial" });
            }
        }

        private void ExportaConsultaDes(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.valor = valor;
            if (valor == 1)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_fin = fechafin;


            }
            else if (valor == 2)
            {
                Model.num_identifi_afil = documento;
            }
            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_fin = fechafin;
                Model.regional = Convert.ToInt32(regional);
            }

            Model.CargarListas2(valor);

            if (Model.ListaNuevasConsultas2.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"ID CENSO\";\"ID CONCURRENCIA\";\"ID EVOLUCION\";\"TIPO IDENTIFICACION USUARIO\";\"IDENTIFICACION USUARIO\";\"TIPO SALUD\";\"CIUDAD SERVICIOS MÉDICOS\";\"REGIONAL\";\"REGIONAL BENEFICIARIO\";\"UNIS\";\"FECHA RECEPCION CENSO\";\"PRIMER APELLIDO\";\"SEGUNDO APELLIDO\";\"PRIMER NOMBRE\";\"SEGUNDO NOMBRE\";\"FECHA NACIMIENTO\";\"EDAD\";\"GÉNERO\";\"FECHA INGRESO\";\"TIPO INGRESO\";\"ORIGEN EVENTO\";\"TIPO HABITACION\";\"MÉDICO AUDITOR\";\"NIT IPS\";\"SAP IPS\";\"NIT SUIS\";\"NOMBRE IPS\";\"RAZON SOCIAL IPS\";\"CIUDAD IPS\";\"DX ACTUAL\";\"DESCRIPCION CIE10\";\"ALTO COSTO\";\"NOMBRE ALTO COSTO\";\"SALUD PUBLICA\";\"NOMBRE SALUD PUBLICA\";\"CODIGO CIE10 CONCURRENCIA\";\"NOMBRE CIE10 CONCURRENCIA\";\"TRIAGE\";\"REINGRESO\";\"GESTANTES\";\"FECHA EGRESO\";\"DX PRINCIPAL EGRESO\";\"CIE10 EGRESO\";\"CONDICION ALTA\";\"HOSPITALIZACION PREVENIBLE\";\"NOMBRE HOSPITALIZACION PREVENIBLE\";\"NÚMERO DEFUNCION\";\"HORA DEFUNCION\";\"TIPO INSTANCIA\";\"MEDIO TRATANTE\";\"MEGA\";\"ESPECIALIDAD\";\"PROCEDIMIENTO QX\";\"CUPS QX\";\"NOMBRE CUPS\";\"INCAPACIDADES\";\"DESCRIPCION EVOLUCION\";\"FECHA EVOLUCION\";\"TIPO HABITACION EVOLUCION\";\"TIENE ESTANCIA PERTINENTE\";\"EPOC\";\"PAD\";\"RCV\";\"COHORTES GESTANTES\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaCenso" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaNuevasConsultas2)
                {
                    //sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\"",
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\";\"{43}\";\"{44}\";\"{45}\";\"{46}\";\"{47}\";\"{48}\";\"{49}\";\"{50}\";\"{51}\";\"{52}\";\"{53}\";\"{54}\";\"{55}\";\"{56}\";\"{57}\";\"{58}\";\"{59}\";\"{60}\";\"{61}\";\"{62}\";\"{63}\"",

                    line.id_censo,
                    line.id_concurrencia,
                    line.id_evolucion,
                    line.tipo_identifi_afiliado,
                    line.num_identifi_afil,
                    line.tipo_salud,
                    line.Ciudad_Serv_Medicos,
                    line.regional,
                    line.Regional_Beneficiario,
                    line.unis,
                    line.fecha_recepcion_censo,
                    line.primer_apellido,
                    line.segundo_apellido,
                    line.primer_nombre,
                    line.segundo_nombre,
                    line.fecha_nacimiento,
                    line.edad,
                    line.genero,
                    line.fecha_ingreso,
                    line.Tipo_ingresoo,
                    line.Origen_evento,
                    line.Tipo_Habitacion,
                    line.Medico_Auditor,
                    line.Nit_Ips,
                    line.Documento_SAP_Ips,
                    line.NIT_SUIS,
                    line.Nombre_Ips,
                    line.RAZON_SOCIAL_SUIS,
                    line.CiudadIPs,
                    line.dx_actual,
                    line.Descripcion_Cie10,
                    line.ALTO_COSTO,
                    line.DESCRIPCION_ALTO_COSTO,
                    line.salud_publica,
                    line.nombre_salud_publica,
                    line.Codigo_CIE10_concurrencia,
                    line.NOMBRE_CIE10_concurrencia,
                    line.triage,
                    line.reingreso,
                    line.gestantes,
                    line.fecha_egreso,
                    line.DxprincipalEgreso,
                    line.nombre_cie10_EGRESO,
                    line.CondicionAlta,
                    line.hospitalizacion_prevenible,
                    line.descripcion_prevenible,
                    line.NumeroDefuncion,
                    line.HoraDefuncion,
                    line.tipo_instancia,
                    line.medico_tratante,
                    line.MEGA,
                    line.especialidad,
                    line.procedimientoqx,
                    line.id_cups_qx,
                    line.nombre_cups,
                    line.incapacidades,
                    line.Descripcion_evolucion,
                    line.Fecha_evolucion,
                    line.TIPO_HABITACION_evolucion,
                    line.TIENE_ESTANCIA_PERTINENTE,
                    line.EPOC,
                    line.PAD,
                    line.RCV,
                    line.cohortes_GESTANTES

                    ));
                }
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        private void ExportaConsultaPacientesActivos()
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            if (Model.ListaPacientesActivos.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"id_censo\";\"fecha_recepcion_censo\";\"Nombre_Ips\";\"CiudadIPs\";\"tipo_identifi_afiliado\";\"num_identifi_afil\";\"primer_apellido\";\"segundo_apellido\";\"primer_nombre\";\"segundo_nombre\";\"edad\";\"genero\";\"fecha_ingreso\";\"Tipo_Habitacion\";\"dias_estancia\";\"Tipo_ingreso\";\"dx_actual\";\"Descripcion_Cie10\";\"Origen_evento\";\"Medico_Auditor\";\"tipo_salud\";\"Regional_Beneficiario\";\"Regional_Asalud\";\"unis\";\"UltEvolucionCompleta\";\"Fecha_ultima_evolucion\";\"fecha_egreso_censo\";\"fecha_egreso\";\"CondicionAlta\";\"Diagnostico_Egreso\";\"Nombre_Diagnostico_Egreso\";\"Gestantes\";\"justificacionEstancia\";\"gestion_auditor\";\"incapacidades\";\"fecha_inicial\";\"fecha_final\";\"med_ser_trata\";\"NumeroDefuncion\";\"Mega\";\"Especialidad\";\"Prestador\";\"Ciudad S/Med\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaCenso" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaPacientesActivos)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\";\"{24}\";\"{25}\";\"{26}\";\"{27}\";\"{28}\";\"{29}\";\"{30}\";\"{31}\";\"{32}\";\"{33}\";\"{34}\";\"{35}\";\"{36}\";\"{37}\";\"{38}\";\"{39}\";\"{40}\";\"{41}\";\"{42}\"",
                                               line.id_censo,
                                               line.fecha_recepcion_censo,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.tipo_identifi_afiliado,
                                               line.num_identifi_afil,
                                               line.primer_apellido,
                                               line.segundo_apellido,
                                               line.primer_nombre,
                                               line.segundo_nombre,
                                               line.edad,
                                               line.genero,
                                               line.fecha_ingreso,
                                               line.Tipo_Habitacion,
                                               line.dias_estancia,
                                               line.Tipo_ingreso,
                                               line.dx_actual,
                                               line.Descripcion_Cie10,
                                               line.Origen_evento,
                                               line.Medico_Auditor,
                                               line.tipo_salud,
                                               line.Regional_Beneficiario,
                                               line.Regional_Asalud,
                                               line.unis,
                                               line.UltEvolucionCompleta,
                                               line.Fecha_ultima_evolucion,
                                               line.fecha_egreso_censo,
                                               line.fecha_egreso,
                                               line.CondicionAlta,
                                               line.Diagnostico_Egreso,
                                               line.Nombre_Diagnostico_Egreso,
                                               line.Gestantes,
                                               line.justificacionEstancia,
                                               line.gestion_auditor,
                                               line.incapacidades,
                                               line.fecha_inicial,
                                               line.fecha_final,
                                               line.med_ser_trata,
                                               line.NumeroDefuncion,
                                               line.Mega,
                                               line.Especialidad,
                                               line.Prestador,
                                               line.ciudad

                                               ));

                }

                Response.Write(sw.ToString());

                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");

            }

        }

        private void ExportaConsultaGestantesNuevo(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.valor = valor;
            if (valor == 1)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;


            }

            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;
                Model.regional = Convert.ToInt32(regional);
            }

            Model.CargarListasGestantes(valor);

            if (Model.ListaGestantesNuevo.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Numero_identificacion\";\"Clase_de_identificacion\";\"Apellidos\";\"Nombre\";\"edad_gestacional\";\"fecha_nacimiento\";\"peso\";\"via_parto\";\"talla\";\"sexo\";\"apgar\";\"control_prenatal\";\"Nombre_Ips\";\"CiudadIPs\";\"regional_asalud\";\"fecha_digita\";\"usuario_digita\";\"fecha_BCG\";\"fecha_vitaminaK\";\"fecha_consenjeria_lactancia\";\"resultadoTCH\";\"fechaTCH\";\"fecha_consulta_pediatria\";\"fecha_hepatitisB\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaGestantes" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaGestantesNuevo)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{21}\";\"{23}\"",
                                               line.Numero_identificacion,
                                               line.Clase_de_identificacion,
                                               line.Apellidos,
                                               line.Nombre,
                                               line.edad_gestacional,
                                               line.fecha_nacimiento,
                                               line.peso,
                                               line.via_parto,
                                               line.talla,
                                               line.sexo,
                                               line.apgar,
                                               line.control_prenatal,
                                               line.Nombre_Ips,
                                               line.CiudadIPs,
                                               line.regional_asalud,
                                               line.fecha_digita,
                                               line.usuario_digita,
                                               line.fecha_BCG,
                                               line.fecha_vitaminaK,
                                               line.fecha_consenjeria_lactancia,
                                               line.resultadoTCH,
                                               line.fechaTCH,
                                               line.fecha_consulta_pediatria,
                                               line.fecha_hepatitisB));
                }

                Response.Write(sw.ToString());

                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");

            }

        }

        private void ExportaConsultaMortalidadNuevo(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {

            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.valor = valor;
            if (valor == 1)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;


            }

            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;
                Model.regional = Convert.ToInt32(regional);
            }
            Model.CargarListasMortalidad(valor);

            if (Model.ListaMortalidadNuevo.Count != 0)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Numero_identificacion\";\"Clase_de_identificacion\";\"Apellidos\";\"Nombre\";\"regional\";\"unis\";\"genero\";\"edad\";\"NumeroDefuncion\";\"HoraDefuncion\";\"fecha_fallecimiento\";\"observacion_fallecimiento\";\"DxprincipalEgreso\";\"nombre_cie10\";\"Nombre_Ips\";\"CiudadIPs\";\"fecha_digita\";\"usuario_digita\";\"tipo_trabajador\";\"regional_asalud\";\"diag_causa_directa_muerte\";\"diag_causa_basica_muerte\";\"diag_causa_antecedente_muerte\";\"fecha_exp_cedula_fallecido\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ConsultaMortalidad" + DateTime.Now + ".csv");
                Response.ContentType = "text/csv";

                foreach (var line in Model.ListaMortalidadNuevo)
                {
                    sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\";\"{13}\";\"{14}\";\"{15}\";\"{16}\";\"{17}\";\"{18}\";\"{19}\";\"{20}\";\"{21}\";\"{22}\";\"{23}\"",
                                                 line.Numero_identificacion,
                                                 line.Clase_de_identificacion,
                                                 line.Apellidos,
                                                 line.Nombre,
                                                 line.regional,
                                                 line.unis,
                                                 line.genero,
                                                 line.edad,
                                                 line.NumeroDefuncion,
                                                 line.HoraDefuncion,
                                                 line.fecha_fallecimiento,
                                                 line.observacion_fallecimiento,
                                                 line.DxprincipalEgreso,
                                                 line.nombre_cie10,
                                                 line.Nombre_Ips,
                                                 line.CiudadIPs,
                                                 line.fecha_digita,
                                                 line.usuario_digita,
                                                 line.tipo_trabajador,
                                                 line.regional_asalud,
                                                 line.diag_causa_directa_muerte,
                                                 line.diag_causa_basica_muerte,
                                                 line.diag_causa_antecedente_muerte,
                                                 line.fecha_exp_cedula_fallecido));
                }

                Response.Write(sw.ToString());

                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }



        }
        #endregion

        public ActionResult ConsultasGenerales()
        {
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            return View(Model);
        }

        public ActionResult ConsultasEco()
        {
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            return View(Model);
        }

        public ActionResult ConsultasEcopetrol()
        {
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            ViewBag.lista_consultas = Model.Ref_ConsultasEcopetrol();

            return View(Model);
        }

        public ActionResult ConsultasAlertas()
        {
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            return View(Model);
        }

        public ActionResult ConsultasCuentas()
        {
            ViewData["Fechaconsulta"] = "";
            AsaludEcopetrol.Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            AsaludEcopetrol.Models.CuentasMedicas.Rips ModelRips = new Models.CuentasMedicas.Rips();
            Models.Facturacion.FacturaDevolucion ModelFD = new Models.Facturacion.FacturaDevolucion();
            ViewBag.tiposRipsforMortalidad = ModelRips.ConsultaTipoRips().Where(f => f.aplica_mortlidad == true).ToList();
            ViewBag.tiposRipsforEpidemiologia = ModelRips.ConsultaTipoRips().Where(l => l.aplica_epidemiologia == true).ToList();
            List<ECOPETROL_COMMON.ENTIDADES.Ref_regional> lista = ModelFD.RefRegional;
            ECOPETROL_COMMON.ENTIDADES.Ref_regional obj = new ECOPETROL_COMMON.ENTIDADES.Ref_regional();
            obj.id_ref_regional = 7;
            obj.nombre_regional = "Todas..";
            lista.Add(obj);

            ViewBag.regional = lista;

            return View(Model);
        }

        /// <summary>
        /// Vista de procesos internos
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcesosInternos()
        {
            return View();
        }

        public ActionResult NuevaConsul1(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.valor = valor;
            if (valor == 1 || valor == 4)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_fin = fechafin;
            }
            else if (valor == 2)
            {
                Model.num_identifi_afil = documento;
            }
            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_fin = fechafin;
                Model.regional = Convert.ToInt32(regional);
            }

            Model.CargarListas2(valor);

            return View(Model);
        }

        public ActionResult NuevaConsul2(DateTime? fechainicio, DateTime? fechafin, Int32? regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.fecha_inicio = fechainicio;
            Model.fecha_fin = fechafin;
            Model.regional = regional;
            Model.valor = valor;


            if (valor == 1)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;


            }
            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;
                Model.regional = regional;
            }

            Model.CargarListasGestantes(valor);


            return View(Model);
        }

        public ActionResult NuevaConsul3(DateTime? fechainicio, DateTime? fechafin, Int32? regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            Model.fecha_inicio = fechainicio;
            Model.fecha_fin = fechafin;
            Model.regional = regional;
            Model.valor = valor;

            Model.valor = valor;
            if (valor == 1)
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;


            }
            else
            {
                Model.fecha_inicio = fechainicio;
                Model.fecha_final = fechafin;
                Model.regional = regional;
            }

            Model.CargarListasMortalidad(valor);

            return View(Model);
        }

        public ActionResult Descargar1(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            ExportaConsultaDes(fechainicio, fechafin, regional, documento, valor);


            return View(Model);
        }

        public ActionResult Descargar2(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            ExportaConsultaGestantesNuevo(fechainicio, fechafin, regional, documento, valor);


            return View(Model);
        }

        public ActionResult Descargar3(DateTime? fechainicio, DateTime? fechafin, String regional, string documento, Int32 valor)
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();

            ExportaConsultaMortalidadNuevo(fechainicio, fechafin, regional, documento, valor);


            return View(Model);
        }

        public ActionResult CargueMegas()
        {
            // ToEntidadHojaExcelList("C:/Users/User/Desktop/requerimiento.xlsx");
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueMegas(HttpPostedFileBase file)
        {
            Models.General General = new Models.General();

            /* Agrega el archivo y lo guarda */
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Files"), fileName);
            file.SaveAs(path);
            try
            {
                /*Luego que guarda el archivo, setea los valores y agrega a base de datos */
                List<megas_cargue_base> ListaMega = Excelaentidad(path);

                Model.InsertarMega(ListaMega);
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", "Megas Cargado s Exitosamente");
            }
            catch (Exception ex)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Transaccion Fallida!", ex.Message);
            }

            ViewBag.idrole = SesionVar.ROL;
            return View();

        }

        public List<megas_cargue_base> Excelaentidad(string pathDelFicheroExcel)
        {

            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("ANEXO 1 BASE MEGA")
                             let item = new ECOPETROL_COMMON.ENTIDADES.megas_cargue_base
                             {
                                 numero_identificacion = row["Número Identif/"],
                                 cod_personal = row["COd/ Personal"],
                                 clase_identificacion = row["Clase de IdentificaciOn"],
                                 dependencia = Convert.ToInt32(row["Dependencia"]),
                                 descripcion_dependencia = row["COd/ Personal"],
                                 registro = row["Registro"],
                                 apellidos = row["Apellidos"],
                                 nombres = row["Nombres"],
                                 genero = row["GENERO"],
                                 fecha_nacimiento = row["Fecha Nacto"],
                                 edad = row["Edad"],
                                 ciudad_s_med = row["Ciudad S/Med"],
                                 descripcion_ciudad_s_med = row["DescripciOn Ciudad S/Med"],
                                 REGIONAL = row["REGIONAL"],
                                 UNIS = row["UNIS"],
                                 cod_funcion_o_miembro = row["COd FunciOn o miembro"],
                                 descripcion_cod_funcion_o_miembro = row["DescripciOn FunciOn O denominaciOn"],
                                 inicio_s_medico = row["Inicio S-Médico"],
                                 term_s_med = row["Term S- Med"],
                                 ingreso_a_ECP = row["Ingreso a ECP"],
                                 TIPO_SALUD = row["TIPO SALUD"],
                                 MEDICO_GENERAL = row["MEDICO GENERAL"],
                                 ESPECIALIDAD_MD = row["ESPECIALIDAD"],
                                 PRESTADOR_MD = row["PRESTADOR"],
                                 FECHA_ASIGNACION_MD = row["FECHA ASIGNACION"],
                                 ODONTOLOGO = row["ODONTOLOGO"],
                                 ESPECIALIDAD_OD = row["ESPECIALIDAD_OD"],
                                 PRESTADOR_OD = row["PRESTADOR_OD"],
                                 FECHA_ASIGNACION_OD = row["FECHA ASIGNACION_OD"],
                                 usuario_digita = SesionVar.UserName,
                                 fecha_digita = DateTime.Now

                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        [HttpPost]
        public ActionResult ConsultasGenerales(Models.Consulta.Consulta Model, string Command)
        {
            if (Model.id_tipo_busqueda == "1")// Censo
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaCensoFechas(Model);

                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return ExportaConsultaCensoDocumentos(Model);
                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaCensoRegional(Model);
                }
            }
            else if (Model.id_tipo_busqueda == "2") // Concurrencia
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaConcurrenciaFechasVisita(Model);
                }
                else if (Model.id_filtro_busqueda == "4")
                {
                    return ExportaConsultaConcurrenciaFechasDigitacion(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return ExportaConsultaConcurrenciaDocumentos(Model);
                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaConcurrenciaRegional(Model);
                }
            }
            else if (Model.id_tipo_busqueda == "3") // Egreso
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaEgresoFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return ExportaConsultaEgresoDocumentos(Model);
                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaEgresoRegional(Model);
                }
            }
            else if (Model.id_tipo_busqueda == "4") // EVE Adversos
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaEventosFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return ExportaConsultaEventosDocumento(Model);
                }

            }
            else if (Model.id_tipo_busqueda == "5") // SITUACION DE CALIDAD
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaSituacionCalFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    ExportaConsultaSituacionCalDocumento(Model);
                }

            }
            else if (Model.id_tipo_busqueda == "6") // NATALIDAD
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaGestantesFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {

                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaGestantesRegional(Model);
                }

            }
            else if (Model.id_tipo_busqueda == "7") // MORTALIDAD
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaMortalidadFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {

                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaMortalidadRegional(Model);
                }
            }
            else if (Model.id_tipo_busqueda == "8") // NATALIDAD SIN CONC
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaGestantesSinFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {

                }
                else if (Model.id_filtro_busqueda == "3")
                {

                }
            }
            else if (Model.id_tipo_busqueda == "9") // MORTALIDAD SIN CONC
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaMortalidadSinFechas(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {

                }
                else if (Model.id_filtro_busqueda == "3")
                {

                }
            }

            else if (Model.id_tipo_busqueda == "10") // CENSO CONCURRENCIA
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaCensoConcurrenciaFecha(Model, 1);
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return ExportaConsultaCensoConcurrenciaFecha(Model, 2);
                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaCensoConcurrenciaFecha(Model, 3);
                }
            }

            else if (Model.id_tipo_busqueda == "11") // NATALIDAD CON/SIN CONCURRENCIA
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return ExportaConsultaGestantesFechasCompleto(Model);
                }
                else if (Model.id_filtro_busqueda == "2")
                {

                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return ExportaConsultaGestantesRegionalCompleto(Model);
                }
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsultasEco(Models.Consulta.Consulta Model, string Command)
        {
            if (Model.id_tipo_busqueda == "10")// CENSO-CONCURRENCIA
            {
                if (Model.id_filtro_busqueda == "1")
                {
                    return RedirectToAction("NuevaConsul1", "Consultas", new { fechainicio = Model.fecha_inicio, fechafin = Model.fecha_fin, regional = Model.NomRegional, documento = Model.num_identifi_afil, valor = Model.id_filtro_busqueda });
                }
                else if (Model.id_filtro_busqueda == "2")
                {
                    return RedirectToAction("NuevaConsul1", "Consultas", new { documento = Model.num_identifi_afil, valor = Model.id_filtro_busqueda });
                }
                else if (Model.id_filtro_busqueda == "3")
                {
                    return RedirectToAction("NuevaConsul1", "Consultas", new { fechainicio = Model.fecha_inicial, fechafin = Model.fecha_final, regional = Model.regional, valor = Model.id_filtro_busqueda });
                }

                else if (Model.id_filtro_busqueda == "4")
                {
                    return RedirectToAction("NuevaConsul1", "Consultas", new { fechainicio = Model.fecha_inicio, fechafin = Model.fecha_fin, regional = Model.NomRegional, documento = Model.num_identifi_afil, valor = Model.id_filtro_busqueda });
                }
            }
            else if (Model.id_tipo_busqueda == "6") // NATALIDAD
            {
                if (Model.id_filtro_busqueda == "3")
                {
                    return RedirectToAction("NuevaConsul2", "Consultas", new { fechainicio = Model.fecha_inicial, fechafin = Model.fecha_final, regional = Model.regional, valor = Model.id_filtro_busqueda });
                }
                else
                {
                    return RedirectToAction("NuevaConsul2", "Consultas", new { fechainicio = Model.fecha_inicio, fechafin = Model.fecha_fin, regional = Model.regional, valor = Model.id_filtro_busqueda });
                }

            }
            else if (Model.id_tipo_busqueda == "7") // MORTALIDAD
            {
                if (Model.id_filtro_busqueda == "3")
                {
                    return RedirectToAction("NuevaConsul3", "Consultas", new { fechainicio = Model.fecha_inicial, fechafin = Model.fecha_final, regional = Model.regional, valor = Model.id_filtro_busqueda });
                }
                else
                {
                    return RedirectToAction("NuevaConsul3", "Consultas", new { fechainicio = Model.fecha_inicio, fechafin = Model.fecha_fin, regional = Model.regional, valor = Model.id_filtro_busqueda });

                }



            }
            else if (Model.id_tipo_busqueda == "1")
            {
                ExportaConsultaPacientesActivos();
            }
            return View(Model);
        }


        public JsonResult GetConsultaAlert(Models.Consulta.Consulta Model)
        {
            return Json(Model.GetConsultaAlertas.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ConsultasAlertas(Models.Consulta.Consulta Model, string Command)
        {
            Int32 Opc = Convert.ToInt32(Model.id_tipo_busqueda2);
            Model.CuentaFechaCargue(Opc);
            return ExportaConsulta1(Model);
        }

        [HttpPost]
        public ActionResult ConsultasCuentas(Models.Consulta.Consulta Model, string Command)
        {
            AsaludEcopetrol.Models.CuentasMedicas.Rips ModelRips = new Models.CuentasMedicas.Rips();
            ViewBag.tiposRipsforMortalidad = ModelRips.ConsultaTipoRips().Where(f => f.aplica_mortlidad == true).ToList();
            ViewBag.tiposRipsforEpidemiologia = ModelRips.ConsultaTipoRips().Where(l => l.aplica_epidemiologia == true).ToList();
            Models.Facturacion.FacturaDevolucion ModelFD = new Models.Facturacion.FacturaDevolucion();
            List<ECOPETROL_COMMON.ENTIDADES.Ref_regional> lista = ModelFD.RefRegional;
            ECOPETROL_COMMON.ENTIDADES.Ref_regional obj = new ECOPETROL_COMMON.ENTIDADES.Ref_regional();
            obj.id_ref_regional = 7;
            obj.nombre_regional = "Todas..";
            lista.Add(obj);

            ViewBag.Regional = lista;
            if (Model.id_tipo_busqueda == "1")// Devolucion
            {
                return ExportaDevolucionFechas(Model);
            }

            if (Model.id_tipo_busqueda == "2") //RIPS
            {
                if (Model.id_filtro_busqueda == "1")
                    return ExportaHallazgosRIPSFechas(Model);

                if (Model.id_filtro_busqueda == "2")
                    return ExportaRIPSFechaConsulta(Model);

                if (Model.id_filtro_busqueda == "3")
                    return ExportaRIPSFechaProcedimiento(Model);

                if (Model.id_filtro_busqueda == "4")
                    return ExportaRIPSFechaNacimiento(Model);

                if (Model.id_filtro_busqueda == "5")
                {
                    return ExportaRIPSFechaMortandad(Model);
                }

                if (Model.id_filtro_busqueda == "6")
                {

                }
            }

            if (Model.id_tipo_busqueda == "3")
            {
                return ExportaRecepcionFacturas(Model);
            }

            if (Model.id_tipo_busqueda == "4")
            {
                return ExportaRecepcionFacturasSin(Model);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult ProcesosInternos(int proceso)
        {
            if (proceso == 1)
            {
                return RedirectToAction("Regionalprestadores", "ProcesosInternos");

            }
            else if (proceso == 2)
            {
                return RedirectToAction("EvaluacionCalidad", "ProcesosInternos");
            }
            else if (proceso == 3)
            {
                return RedirectToAction("ResultadosRanking", "ProcesosInternos");
            }
            else if (proceso == 4)
            {
                return RedirectToAction("AgregarVisita", "ProcesosInternos");
            }
            else if (proceso == 5)
            {
                return RedirectToAction("AdminVisitas", "ProcesosInternos");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ExportarLstEcopetrol(DateTime? fechainicial, DateTime? fechafin, Int32 lista_consultas, String numDoc)
        {

            var FI = Convert.ToDateTime(fechainicial);
            var FF = Convert.ToDateTime(fechafin);

            var fileDownloadName = String.Format("Consolidado" + Convert.ToDateTime(DateTime.Now) + ".xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            if (lista_consultas == 1)
            {
                List<ManagmentClinicosCensoConConcurrenciaResult> lista1 = new List<ManagmentClinicosCensoConConcurrenciaResult>();

                lista1 = Model.CensoConcurrenciaEcopetrol(FI, FF, ref MsgRes);
                ExcelPackage package = GenerateExcelcensoConcurrenciaEcopetrol(lista1.ToList());
                var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
                fsr.FileDownloadName = fileDownloadName;

                return fsr;
            }
            else if (lista_consultas == 2)
            {
                List<ManagmentClinicosCensoResult> lista2 = new List<ManagmentClinicosCensoResult>();

                lista2 = Model.CensoEcopetrol(FI, FF, ref MsgRes);

                //if (SesionVar.ROL == "28")
                //{
                //    lista2 = lista2.Where(X => X.Id_Regional == 5).ToList();
                //}


                ExcelPackage package = GenerateExcelcensoEcopetrol(lista2.ToList());
                var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
                fsr.FileDownloadName = fileDownloadName;
                return fsr;
            }
            else if (lista_consultas == 3)
            {
                List<ManagmentClinicosConsultasAlertasResult> lista3 = new List<ManagmentClinicosConsultasAlertasResult>();

                lista3 = Model.AlertasEcopetrol(FI, FF, ref MsgRes);

                //if (SesionVar.ROL == "28")
                //{
                //    lista3 = lista3.Where(X => X.Id_regional == 5).ToList();
                //}


                ExcelPackage package = GenerateExcelAlertasoEcopetrol(lista3.ToList());
                var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
                fsr.FileDownloadName = fileDownloadName;
                return fsr;
            }
            else
            {

                ExcelPackage package = null;
                var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
                fsr.FileDownloadName = fileDownloadName;
                return fsr;
            }
        }

        public ActionResult DownloadExcelEPPlus(DateTime? fechainicial, DateTime? fechafin, Int32 lista_consultas, String numDoc)
        {


            JsonResult result = new JsonResult();
            String mensaje = "";
            var FI = Convert.ToDateTime(fechainicial);
            var FF = Convert.ToDateTime(fechafin);


            if (lista_consultas == 1)
            {
                List<ManagmentClinicosCensoConConcurrenciaResult> lista1 = new List<ManagmentClinicosCensoConConcurrenciaResult>();
                try
                {
                    DataTable datatable = Model.CensoConcurrenciaEcopetrolII(FI, FF, Conexion, ref MsgRes);
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage pck = new ExcelPackage(stream))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Consolidado");
                        ws.Cells.LoadFromDataTable(datatable, true);
                        pck.Save();
                    }
                    stream.Position = 0;
                    string excelName = $"Censo-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    return File(stream, "application/octet-stream", excelName);

                }
                catch (Exception ex)
                {
                    string rta = "<script LANGUAGE='JavaScript'>window.alert('Ha ocurrido un error al momento de visualizar el archivo. Por favor comuniquese con el administrador');</script>";
                    Response.Write(rta);
                    Response.End();
                }
            }
            else if (lista_consultas == 2)
            {
                List<ManagmentClinicosCensoResult> lista2 = new List<ManagmentClinicosCensoResult>();

                lista2 = Model.CensoEcopetrol(FI, FF, ref MsgRes);

                //if (SesionVar.ROL == "28")
                //{
                //    lista2 = lista2.Where(X => X.Id_regional == 5).ToList();
                //}
                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista2, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Censo-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);

            }
            else if (lista_consultas == 3)
            {
                List<ManagmentClinicosConsultasAlertasResult> lista3 = new List<ManagmentClinicosConsultasAlertasResult>();

                lista3 = Model.AlertasEcopetrol(FI, FF, ref MsgRes);

                //if (SesionVar.ROL == "28")
                //{
                //    lista3 = lista3.Where(X => X.Id_regional == 5).ToList();
                //}

                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Consolidado");
                    workSheet.Cells.LoadFromCollection(lista3, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"Alertas-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/octet-stream", excelName);
            }

            return View();
        }

        private static ExcelPackage GenerateExcelcensoConcurrenciaEcopetrol(List<ManagmentClinicosCensoConConcurrenciaResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "id_censo";
            ws.Cells[1, 2].Value = "id_concurrencia";
            ws.Cells[1, 3].Value = "id_evolucion";
            ws.Cells[1, 4].Value = "tipo_identifi_afiliado";
            ws.Cells[1, 5].Value = "num_identifi_afil";
            ws.Cells[1, 6].Value = "fecha_recepcion_censo";
            ws.Cells[1, 7].Value = "primer_apellido";
            ws.Cells[1, 8].Value = "segundo_apellido";
            ws.Cells[1, 9].Value = "primer_nombre";
            ws.Cells[1, 10].Value = "segundo_nombre";
            ws.Cells[1, 11].Value = "fecha_nacimiento";
            ws.Cells[1, 12].Value = "edad";
            ws.Cells[1, 13].Value = "genero";
            ws.Cells[1, 14].Value = "fecha_ingreso";
            ws.Cells[1, 15].Value = "Tipo_ingresoo";
            ws.Cells[1, 16].Value = "Origen_evento";
            ws.Cells[1, 17].Value = "Tipo_Habitacion";
            ws.Cells[1, 18].Value = "Medico_Auditor";
            ws.Cells[1, 19].Value = "Nit_Ips";
            ws.Cells[1, 20].Value = "Documento SAP Ips";
            ws.Cells[1, 21].Value = "Nombre_Ips";
            ws.Cells[1, 22].Value = "CiudadIPs";
            ws.Cells[1, 23].Value = "regional";
            ws.Cells[1, 24].Value = "Regional del beneficiario";
            ws.Cells[1, 25].Value = "dx_actual";
            ws.Cells[1, 26].Value = "Descripcion_Cie10";
            ws.Cells[1, 27].Value = "ALTO_COSTO";
            ws.Cells[1, 28].Value = "DESCRIPCION_ALTO_COSTO";
            ws.Cells[1, 29].Value = "salud_publica";
            ws.Cells[1, 30].Value = "nombre_salud_publica";
            ws.Cells[1, 31].Value = "Codigo_CIE10_concurrencia";
            ws.Cells[1, 32].Value = "NOMBRE_CIE10_concurrencia";
            ws.Cells[1, 33].Value = "triage";
            ws.Cells[1, 34].Value = "reingreso";
            ws.Cells[1, 35].Value = "gestantes";
            ws.Cells[1, 36].Value = "fecha_egreso";
            ws.Cells[1, 37].Value = "DxprincipalEgreso";
            ws.Cells[1, 38].Value = "nombre_cie10_EGRESO";
            ws.Cells[1, 39].Value = "CondicionAlta";
            ws.Cells[1, 40].Value = "hospitalizacion_prevenible";
            ws.Cells[1, 41].Value = "descripcion_prevenible";
            ws.Cells[1, 42].Value = "NumeroDefuncion";
            ws.Cells[1, 43].Value = "HoraDefuncion";
            ws.Cells[1, 44].Value = "tipo_instancia";
            ws.Cells[1, 45].Value = "medico_tratante";
            ws.Cells[1, 46].Value = "MEGA";
            ws.Cells[1, 47].Value = "especialidad";
            ws.Cells[1, 48].Value = "procedimientoqx";
            ws.Cells[1, 49].Value = "id_cups_qx";
            ws.Cells[1, 50].Value = "nombre_cups";
            ws.Cells[1, 51].Value = "incapacidades";
            ws.Cells[1, 52].Value = "tipo_salud";
            ws.Cells[1, 53].Value = "unis";
            ws.Cells[1, 54].Value = "Descripcion_evolucion";
            ws.Cells[1, 55].Value = "Fecha_evolucion";
            ws.Cells[1, 56].Value = "TIPO_HABITACION_evolucion";
            ws.Cells[1, 57].Value = "TIENE_ESTANCIA_PERTINENTE";
            ws.Cells[1, 58].Value = "RCV";
            ws.Cells[1, 59].Value = "EPOC";
            ws.Cells[1, 60].Value = "cohortes_GESTANTES";
            ws.Cells[1, 61].Value = "PAD";



            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_censo;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).id_concurrencia;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).id_evolucion;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).tipo_identifi_afiliado;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).num_identifi_afil;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).fecha_recepcion_censo;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).primer_apellido;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).segundo_apellido;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).primer_nombre;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).segundo_nombre;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).fecha_nacimiento;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).edad;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).genero;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).fecha_ingreso;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).Tipo_ingresoo;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).Origen_evento;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).Tipo_Habitacion;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).Medico_Auditor;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).Nit_Ips;
                ws.Cells[i + 2, 20].Value = datasource.ElementAt(i).Documento_SAP_Ips;
                ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).Nombre_Ips;
                ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).CiudadIPs;
                ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).regional;
                ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).Regional_Beneficiario;
                ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).dx_actual;
                ws.Cells[i + 2, 26].Value = datasource.ElementAt(i).Descripcion_Cie10;
                ws.Cells[i + 2, 27].Value = datasource.ElementAt(i).ALTO_COSTO;
                ws.Cells[i + 2, 28].Value = datasource.ElementAt(i).DESCRIPCION_ALTO_COSTO;
                ws.Cells[i + 2, 29].Value = datasource.ElementAt(i).salud_publica;
                ws.Cells[i + 2, 30].Value = datasource.ElementAt(i).nombre_salud_publica;
                ws.Cells[i + 2, 31].Value = datasource.ElementAt(i).Codigo_CIE10_concurrencia;
                ws.Cells[i + 2, 32].Value = datasource.ElementAt(i).NOMBRE_CIE10_concurrencia;
                ws.Cells[i + 2, 33].Value = datasource.ElementAt(i).triage;
                ws.Cells[i + 2, 34].Value = datasource.ElementAt(i).reingreso;
                ws.Cells[i + 2, 35].Value = datasource.ElementAt(i).gestantes;
                ws.Cells[i + 2, 36].Value = datasource.ElementAt(i).fecha_egreso;
                ws.Cells[i + 2, 37].Value = datasource.ElementAt(i).DxprincipalEgreso;
                ws.Cells[i + 2, 38].Value = datasource.ElementAt(i).nombre_cie10_EGRESO;
                ws.Cells[i + 2, 39].Value = datasource.ElementAt(i).CondicionAlta;
                ws.Cells[i + 2, 40].Value = datasource.ElementAt(i).hospitalizacion_prevenible;
                ws.Cells[i + 2, 41].Value = datasource.ElementAt(i).descripcion_prevenible;
                ws.Cells[i + 2, 42].Value = datasource.ElementAt(i).NumeroDefuncion;
                ws.Cells[i + 2, 43].Value = datasource.ElementAt(i).HoraDefuncion;
                ws.Cells[i + 2, 44].Value = datasource.ElementAt(i).tipo_instancia;
                ws.Cells[i + 2, 45].Value = datasource.ElementAt(i).medico_tratante;
                ws.Cells[i + 2, 46].Value = datasource.ElementAt(i).MEGA;
                ws.Cells[i + 2, 47].Value = datasource.ElementAt(i).especialidad;
                ws.Cells[i + 2, 48].Value = datasource.ElementAt(i).procedimientoqx;
                ws.Cells[i + 2, 49].Value = datasource.ElementAt(i).id_cups_qx;
                ws.Cells[i + 2, 50].Value = datasource.ElementAt(i).nombre_cups;
                ws.Cells[i + 2, 51].Value = datasource.ElementAt(i).incapacidades;
                ws.Cells[i + 2, 52].Value = datasource.ElementAt(i).tipo_salud;
                ws.Cells[i + 2, 53].Value = datasource.ElementAt(i).unis;
                ws.Cells[i + 2, 54].Value = datasource.ElementAt(i).Descripcion_evolucion;
                ws.Cells[i + 2, 55].Value = datasource.ElementAt(i).Fecha_evolucion;
                ws.Cells[i + 2, 56].Value = datasource.ElementAt(i).TIPO_HABITACION_evolucion;
                ws.Cells[i + 2, 57].Value = datasource.ElementAt(i).TIENE_ESTANCIA_PERTINENTE;
                ws.Cells[i + 2, 58].Value = datasource.ElementAt(i).RCV;
                ws.Cells[i + 2, 59].Value = datasource.ElementAt(i).EPOC;
                ws.Cells[i + 2, 60].Value = datasource.ElementAt(i).cohortes_GESTANTES;
                ws.Cells[i + 2, 61].Value = datasource.ElementAt(i).PAD;

            }
            using (ExcelRange rng = ws.Cells["A1:BX1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        private static ExcelPackage GenerateExcelcensoEcopetrol(List<ManagmentClinicosCensoResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            // Sets Headers
            ws.Cells[1, 1].Value = "id_censo";
            ws.Cells[1, 2].Value = "fecha_recepcion_censo";
            ws.Cells[1, 3].Value = "Nit_Ips";
            ws.Cells[1, 3].Value = "NIT_SUIS";
            ws.Cells[1, 4].Value = "Documento SAP Ips";
            ws.Cells[1, 4].Value = "ips_primaria";
            ws.Cells[1, 5].Value = "Nombre_Ips";
            ws.Cells[1, 5].Value = "Nobre_IPS_SUIS";
            ws.Cells[1, 6].Value = "CiudadIPs";
            ws.Cells[1, 6].Value = "Id_regional";
            ws.Cells[1, 6].Value = "RegionalIps";
            ws.Cells[1, 7].Value = "tipo_identifi_afiliado";
            ws.Cells[1, 8].Value = "num_identifi_afil";
            ws.Cells[1, 9].Value = "primer_apellido";
            ws.Cells[1, 10].Value = "segundo_apellido";
            ws.Cells[1, 11].Value = "primer_nombre";
            ws.Cells[1, 12].Value = "segundo_nombre";
            ws.Cells[1, 13].Value = "edad";
            ws.Cells[1, 14].Value = "fecha_nacimiento";
            ws.Cells[1, 15].Value = "genero";
            ws.Cells[1, 16].Value = "fecha_ingreso";
            ws.Cells[1, 17].Value = "fecha_egreso";
            ws.Cells[1, 18].Value = "fecha_egreso_censo";
            ws.Cells[1, 19].Value = "Tipo_Habitacion";
            ws.Cells[1, 20].Value = "Tipo_ingreso";
            ws.Cells[1, 21].Value = "dx_actual";
            ws.Cells[1, 22].Value = "Descripcion_Cie10";
            ws.Cells[1, 23].Value = "Origen_evento";
            ws.Cells[1, 24].Value = "Medico_Auditor";
            ws.Cells[1, 25].Value = "digita_fecha";
            ws.Cells[1, 26].Value = "Usuario_Digita";
            ws.Cells[1, 27].Value = "tipo_salud";
            ws.Cells[1, 28].Value = "regional";
            ws.Cells[1, 29].Value = "unis";
            ws.Cells[1, 30].Value = "RCV";
            ws.Cells[1, 31].Value = "EPOC";
            ws.Cells[1, 32].Value = "GESTANTES";
            ws.Cells[1, 33].Value = "PAD";


            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_censo;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).fecha_recepcion_censo;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).Nit_Ips;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).Documento_SAP;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).Nombre_Ips;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).CiudadIPs;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).tipo_identifi_afiliado;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).num_identifi_afil;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).primer_apellido;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).segundo_apellido;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).primer_nombre;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).segundo_nombre;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).edad;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).fecha_nacimiento;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).genero;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).fecha_ingreso;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).fecha_egreso;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).fecha_egreso_censo;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).Tipo_Habitacion;
                ws.Cells[i + 2, 20].Value = datasource.ElementAt(i).Tipo_ingreso;
                ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).dx_actual;
                ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).Descripcion_Cie10;
                ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).Origen_evento;
                ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).Medico_Auditor;
                ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).digita_fecha;
                ws.Cells[i + 2, 26].Value = datasource.ElementAt(i).Usuario_Digita;
                ws.Cells[i + 2, 27].Value = datasource.ElementAt(i).tipo_salud;
                ws.Cells[i + 2, 28].Value = datasource.ElementAt(i).regional;
                ws.Cells[i + 2, 29].Value = datasource.ElementAt(i).unis;
                ws.Cells[i + 2, 30].Value = datasource.ElementAt(i).RCV;
                ws.Cells[i + 2, 31].Value = datasource.ElementAt(i).EPOC;
                ws.Cells[i + 2, 32].Value = datasource.ElementAt(i).GESTANTES;
                ws.Cells[i + 2, 33].Value = datasource.ElementAt(i).PAD;


            }
            using (ExcelRange rng = ws.Cells["A1:AJ1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

        private static ExcelPackage GenerateExcelAlertasoEcopetrol(List<ManagmentClinicosConsultasAlertasResult> datasource)
        {

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            ws.Cells[1, 1].Value = "id_concurrencia";
            ws.Cells[1, 2].Value = "id_censo";
            ws.Cells[1, 3].Value = "afi_tipo_doc";
            ws.Cells[1, 4].Value = "Documento_Afiliado";
            ws.Cells[1, 5].Value = "afi_nom";
            ws.Cells[1, 6].Value = "edad";
            ws.Cells[1, 7].Value = "Diagnostico_Censo";
            ws.Cells[1, 8].Value = "Nombre_Diagnostico_Censo";
            ws.Cells[1, 9].Value = "Nombre_auditor";
            ws.Cells[1, 10].Value = "CiudadIPs";
            ws.Cells[1, 11].Value = "Nit_Ips";
            ws.Cells[1, 12].Value = "Documento SAP Ips";
            ws.Cells[1, 13].Value = "Nombre_Ips";
            ws.Cells[1, 14].Value = "Diagnostico_1_Evolu";
            ws.Cells[1, 15].Value = "Nombre_Diagnostico_1_Evolu";
            ws.Cells[1, 16].Value = "Diagnostico_2_Evolu";
            ws.Cells[1, 17].Value = "Nombre_Diagnostico_2_Evolu";
            ws.Cells[1, 18].Value = "Diagnostico_3_Evolu";
            ws.Cells[1, 19].Value = "Nombre_Diagnostico_3_Evolu";
            ws.Cells[1, 20].Value = "Diagnostico_4_Evolu";
            ws.Cells[1, 21].Value = "Nombre_Diagnostico_4_Evolu";
            ws.Cells[1, 22].Value = "fecha_digita";
            ws.Cells[1, 23].Value = "fecha_ingreso";
            ws.Cells[1, 24].Value = "fecha_egreso";
            ws.Cells[1, 25].Value = "Diagnostico_Egreso";
            ws.Cells[1, 26].Value = "Nombre_Diagnostico_Egreso";
            ws.Cells[1, 27].Value = "Incapacidad";
            ws.Cells[1, 28].Value = "Fecha_Inicial_Incapacidad";
            ws.Cells[1, 29].Value = "Fecha_final_Incapacidad";
            ws.Cells[1, 30].Value = "Alerta_Confirmada";
            ws.Cells[1, 31].Value = "Tipo_Evento";
            ws.Cells[1, 32].Value = "Nombre_De_Alerta";
            ws.Cells[1, 33].Value = "Descripcion_Alerta";
            ws.Cells[1, 34].Value = "RCV";
            ws.Cells[1, 35].Value = "EPOC";
            ws.Cells[1, 36].Value = "GESTANTES";
            ws.Cells[1, 37].Value = "PAD";


            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id_concurrencia;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).id_censo;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).afi_tipo_doc;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).Documento_Afiliado;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).afi_nom;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).edad;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).Diagnostico_Censo;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).Nombre_Diagnostico_Censo;
                ws.Cells[i + 2, 9].Value = datasource.ElementAt(i).Nombre_auditor;
                ws.Cells[i + 2, 10].Value = datasource.ElementAt(i).CiudadIPs;
                ws.Cells[i + 2, 11].Value = datasource.ElementAt(i).Nit_Ips;
                ws.Cells[i + 2, 12].Value = datasource.ElementAt(i).Documento_SAP_Ips;
                ws.Cells[i + 2, 13].Value = datasource.ElementAt(i).Nombre_Ips;
                ws.Cells[i + 2, 14].Value = datasource.ElementAt(i).Diagnostico_1_Evolu;
                ws.Cells[i + 2, 15].Value = datasource.ElementAt(i).Nombre_Diagnostico_1_Evolu;
                ws.Cells[i + 2, 16].Value = datasource.ElementAt(i).Diagnostico_2_Evolu;
                ws.Cells[i + 2, 17].Value = datasource.ElementAt(i).Nombre_Diagnostico_2_Evolu;
                ws.Cells[i + 2, 18].Value = datasource.ElementAt(i).Diagnostico_3_Evolu;
                ws.Cells[i + 2, 19].Value = datasource.ElementAt(i).Nombre_Diagnostico_3_Evolu;
                ws.Cells[i + 2, 20].Value = datasource.ElementAt(i).Diagnostico_4_Evolu;
                ws.Cells[i + 2, 21].Value = datasource.ElementAt(i).Nombre_Diagnostico_4_Evolu;
                ws.Cells[i + 2, 22].Value = datasource.ElementAt(i).fecha_digita;
                ws.Cells[i + 2, 23].Value = datasource.ElementAt(i).fecha_ingreso;
                ws.Cells[i + 2, 24].Value = datasource.ElementAt(i).fecha_egreso;
                ws.Cells[i + 2, 25].Value = datasource.ElementAt(i).Diagnostico_Egreso;
                ws.Cells[i + 2, 26].Value = datasource.ElementAt(i).Nombre_Diagnostico_Egreso;
                ws.Cells[i + 2, 27].Value = datasource.ElementAt(i).Incapacidad;
                ws.Cells[i + 2, 28].Value = datasource.ElementAt(i).Fecha_Inicial_Incapacidad;
                ws.Cells[i + 2, 29].Value = datasource.ElementAt(i).Fecha_final_Incapacidad;
                ws.Cells[i + 2, 30].Value = datasource.ElementAt(i).Alerta_Confirmada;
                ws.Cells[i + 2, 31].Value = datasource.ElementAt(i).Tipo_Evento;
                ws.Cells[i + 2, 32].Value = datasource.ElementAt(i).Nombre_De_Alerta;
                ws.Cells[i + 2, 33].Value = datasource.ElementAt(i).Descripcion_Alerta;
                ws.Cells[i + 2, 34].Value = datasource.ElementAt(i).RCV;
                ws.Cells[i + 2, 35].Value = datasource.ElementAt(i).EPOC;
                ws.Cells[i + 2, 36].Value = datasource.ElementAt(i).GESTANTES;
                ws.Cells[i + 2, 37].Value = datasource.ElementAt(i).PAD;

            }
            using (ExcelRange rng = ws.Cells["A1:AK1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                rng.Style.Font.Color.SetColor(Color.White);
            }
            return pck;
        }

    }
}