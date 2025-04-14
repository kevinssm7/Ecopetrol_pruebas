using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;
using QRCoder;
using Ionic.Zip;
using AsaludEcopetrol.Models.CuentasMedicas;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using System.Text;
using AsaludEcopetrol.Helpers;
using System.Data.OleDb;
using System.Data.Entity.SqlServer;
using Kendo.Mvc.UI;
using QRCode = QRCoder.QRCode;
using System.Globalization;
using System.Runtime.Caching;
using System.Text.RegularExpressions;


namespace AsaludEcopetrol.Controllers.Analistas
{
    public class AnalistasController : Controller
    {
        #region Propiedades

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
        #endregion


        public ActionResult ConfiguracionAnalistas(int? rta)
        {
            ViewBag.regional = BusClass.GetRefRegion();

            return View();
        }

        public JsonResult GuardarConfiguracion(int regional, int unis, String analistas)
        {
            var mensaje = "";
            var conteoExistencia = 0;
            String analistasQueYaExisten = "";
            try
            {
                String[] analistasFinal = new string[0];
                if (analistas != null)
                {
                    analistasFinal = analistas.Split(',');
                }

                var conteo = 0;

                foreach(var item in analistasFinal)
                {
                    var resultado = BusClass.ValidaExisteAnalista(regional, unis, Convert.ToInt32(analistasFinal[conteo]));
                    
                    if(resultado != 0)
                    {
                        conteoExistencia = 1;
                        analistasQueYaExisten += analistasFinal[conteo] + ",";
                    }
                    conteo++;
                }



                mensaje = "ERROR AL GUARDAR LA CONDIGURACIÓN";
                return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL GUARDAR LA CONDIGURACIÓN: " + error;
                return Json(new { success = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult traerAnalista(int regional)
        {
            var miproyecto = "";

            List<vw_analistas_recepcion> analistas = new List<vw_analistas_recepcion>();
            analistas = BusClass.GetListAnalistas().Where(x => x.id_regional == regional).ToList();

            ViewBag.analistas = analistas;

            var listatotal = new object();

            listatotal = (from item in analistas
                          select new
                          {
                              value = item.id_usuario,
                              text = item.nom_analista,
                          });

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        public string ObtenerUnisPrestador(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            Models.Adherencia.Adherencia Model = new Models.Adherencia.Adherencia();

            List<ref_adherencia_unis> Unis = Model.GetUnisByRegional(idregional);

            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_adherencia_unis + "'>" + item.nom_adherencia_unis + "</option>";
            }

            return result;
        }
    }
}