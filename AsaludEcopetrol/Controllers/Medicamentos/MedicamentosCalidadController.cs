
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Medicamentos;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using Facede;

using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Data;
using AsaludEcopetrol.Helpers;
using System.Web.UI.WebControls;
using System.Web.UI;
using iTextSharp.text;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Runtime.Caching;
using System.Globalization;
using Aspose.Cells;
using AsaludEcopetrol.Models;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace AsaludEcopetrol.Controllers.Medicamentos
{
    public class MedicamentosCalidadController : Controller
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
        Facade BusClass = new Facade();
        #endregion

        public ActionResult CargueBaseDispensacion(int? rta, string msj)
        {
            ViewBag.meses = BusClass.meses();
            Models.General General = new Models.General();

            if (rta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", msj);
            }
            else if (rta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", msj);
            }
            else
            {
                ViewData["alerta"] = "";
            }

            return View();
        }

        //public JsonResult SaveMedicamentoDispensacion(List<HttpPostedFileBase> files)
        public JsonResult SaveMedicamentoDispensacion(int? año, int? mes)
        {
            string mensaje = "";
            string mensajeFinal = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            var validado = false;
            var nombreArchivo = "";
            List<string> archivosDañad0s = new List<string>();
            var idLote = 0;
            var ruta = "";
            try
            {
                IList<HttpPostedFileBase> files = Request.Files.GetMultiple("file");

                if (files.Count() > 0)
                {
                    medicamentos_dispen_lote lote = new medicamentos_dispen_lote();
                    lote.fecha_lote = DateTime.Now;
                    lote.mes = mes;
                    lote.año = año;
                    lote.usuario_lote = SesionVar.UserName;
                    idLote = BusClass.CargarLoteMedicamentosDispensacion(lote);

                    HttpClient httpClient = new HttpClient();
                    try
                    {
                        for (var i = 0; i < files.Count() - 1; i++)
                        {
                            httpClient.Timeout = TimeSpan.FromHours(1);

                            HttpPostedFileBase file = files[i];
                            nombreArchivo = file.FileName;

                            ruta = DevolverRutaArchivo(file);

                            string dirpath = Path.Combine(ruta);
                            WebClient User = new WebClient();
                            ruta = dirpath;
                            string filename = ruta;

                            var tipo = Path.GetExtension(file.FileName);

                            Byte[] FileBuffer = User.DownloadData(filename);

                            byte[] array = new byte[0];
                            array = FileBuffer;
                            array = array.ToArray();

                            HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, file.ContentType, file.FileName + "." + tipo);

                            CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                            var asposeOptions = new Aspose.Cells.LoadOptions
                            {
                                MemorySetting = MemorySetting.MemoryPreference
                            };

                            Workbook wb = new Workbook(sigFile.InputStream, asposeOptions);
                            Worksheet worksheet = wb.Worksheets[0];
                            Cells cells = worksheet.Cells;
                            int MaximaFila = cells.MaxDataRow;

                            var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                            {
                                CheckMixedValueType = false,
                                ExportColumnName = true,
                                ExportAsString = true
                            };

                            DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                            Int32 idCargue = Model.DispensacionMedicamentosCalidad(lote, dataTable, file.FileName, ref MsgRes);

                            if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                mensaje = "SE INGRESÓ CORRECTAMENTE.";
                                validado = true;
                                //return Json(new { success = true, message = mensaje, idMed = lote }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mensaje = MsgRes.DescriptionResponse + " - Archivo: " + nombreArchivo;
                                archivosDañad0s.Add(mensaje);
                                //return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        var alerta = e.Message;
                        if (alerta.Contains("System.OutOfMemoryException"))
                        {
                            if (archivosDañad0s.Count > 0)
                            {
                                foreach (var item in archivosDañad0s)
                                {
                                    mensaje = "ERROR AL INGRESAR LOS REGISTROS - MEMORIA.  Archivo: " + item;
                                    mensajeFinal += mensaje + " \n";
                                }
                            }
                            else
                            {
                                mensaje = "ERROR AL INGRESAR LOS REGISTROS - MEMORIA.";
                                mensajeFinal = mensaje;
                            }
                        }
                        else
                        {
                            if (archivosDañad0s.Count > 0)
                            {
                                foreach (var item in archivosDañad0s)
                                {
                                    mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + alerta;
                                    mensajeFinal += mensaje + " \n";
                                }
                            }
                            else
                            {
                                mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + alerta;
                                mensajeFinal = mensaje;
                            }
                        }
                        var eliminarLote = BusClass.eliminarLoteMedicamentosDispensacion(idLote);
                    }
                }
                else
                {
                    mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
                    mensajeFinal = mensaje;
                }
            }
            catch (Exception ex)
            {
                if (archivosDañad0s.Count > 0)
                {
                    foreach (var item in archivosDañad0s)
                    {
                        mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + ex.Message;
                        mensajeFinal += mensaje + " \n";
                    }
                }
                else
                {
                    mensaje = "*---ERROR -- - *" + ex.Message;
                    mensajeFinal = mensaje;
                }
                var eliminarLote = BusClass.eliminarLoteMedicamentosDispensacion(idLote);
            }

            if (string.IsNullOrEmpty(mensajeFinal))
            {
                mensajeFinal = "";

                if (archivosDañad0s.Count > 0)
                {
                    foreach (var item in archivosDañad0s)
                    {
                        mensaje = "ERROR AL INGRESAR LOS REGISTROS Archivo: " + item;
                        mensajeFinal += mensaje;
                        mensajeFinal += " \n";
                    }
                }
                else
                {
                    mensajeFinal = "SE INGRESÓ CORRECTAMENTE LOS REGISTROS.";
                }
            }

            FileInfo fileDelete = new FileInfo(ruta);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            return Json(new { success = validado, message = mensajeFinal, idLote = idLote }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult CargueBaseDispensacion(HttpPostedFileBase file)
        //{
        //    string mensaje = "";
        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    try
        //    {
        //        if (this.Request.Files["file"].ContentLength > 0)
        //        {
        //            try
        //            {
        //                var ruta = DevolverRutaArchivo(file);

        //                string dirpath = Path.Combine(ruta);
        //                WebClient User = new WebClient();
        //                ruta = dirpath;
        //                string filename = ruta;

        //                var tipo = Path.GetExtension(file.FileName);

        //                Byte[] FileBuffer = User.DownloadData(filename);

        //                byte[] array = new byte[0];
        //                array = FileBuffer;
        //                array = array.ToArray();

        //                HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, file.ContentType, file.FileName + "." + tipo);

        //                CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
        //                var asposeOptions = new Aspose.Cells.LoadOptions
        //                {
        //                    MemorySetting = MemorySetting.MemoryPreference
        //                };

        //                Workbook wb = new Workbook(sigFile.InputStream, asposeOptions);
        //                Worksheet worksheet = wb.Worksheets[0];
        //                Cells cells = worksheet.Cells;
        //                int MaximaFila = cells.MaxDataRow;

        //                var ExportTableOptions = new Aspose.Cells.ExportTableOptions
        //                {
        //                    CheckMixedValueType = false,
        //                    ExportColumnName = true,
        //                    ExportAsString = true
        //                };

        //                DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

        //                Int32 lote = Model.DispensacionMedicamentosCalidad(dataTable, ref MsgRes);

        //                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
        //                {
        //                    mensaje = "SE INGRESO CORRECTAMENTE.";
        //                    return Json(new { success = true, message = mensaje, idMed = lote }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    mensaje = MsgRes.DescriptionResponse;
        //                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                var alerta = e.Message;
        //                if (alerta.Contains("System.OutOfMemoryException"))
        //                {
        //                    mensaje = "ERROR AL INGRESAR LOS REGISTROS - MEMORIA.";
        //                }
        //                else
        //                {
        //                    mensaje = "ERROR AL INGRESAR LOS REGISTROS.";
        //                }

        //                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
        //            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "*---ERROR -- - *" + ex.Message;
        //        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public string DevolverRutaArchivo(HttpPostedFileBase file)
        {

            string archivo = "";
            string rutaFinal = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<Models.CuentasMedicas.SoportesClinicos> listasoportes = new List<Models.CuentasMedicas.SoportesClinicos>();

            ViewBag.af = false;
            try
            {

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                strRutaDefinitiva = ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                //string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                var extension = Path.GetExtension(file.FileName);
                string nombreSintilde = file.FileName.Replace(extension, "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde + extension);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;
                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "MedicamentosDispen";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "MedicamentosDispenPruebas";
                }
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

                Models.CuentasMedicas.SoportesClinicos objGD = new Models.CuentasMedicas.SoportesClinicos();

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                byte[] ExcelData = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    ExcelData = binaryReader.ReadBytes(file.ContentLength);
                }

                rutaFinal = archivo;
                return rutaFinal;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return rutaFinal;
            }
        }

        public class HttpPostedFileBaseCustom : HttpPostedFileBase
        {
            private byte[] _Bytes;
            private String _ContentType;
            private String _FileName;
            private MemoryStream _Stream;

            public override Int32 ContentLength { get { return this._Bytes.Length; } }
            public override String ContentType { get { return this._ContentType; } }
            public override String FileName { get { return this._FileName; } }

            public override Stream InputStream
            {
                get
                {
                    if (this._Stream == null)
                    {
                        this._Stream = new MemoryStream(this._Bytes);
                    }
                    return this._Stream;
                }
            }

            public HttpPostedFileBaseCustom(byte[] contentData, String contentType, String fileName)
            {
                this._ContentType = contentType;
                this._FileName = fileName;
                this._Bytes = contentData ?? new byte[0];
            }

            public override void SaveAs(String filename)
            {
                System.IO.File.WriteAllBytes(filename, this._Bytes);
            }
        }

        //public JsonResult SaveMedicamentoDispensacion(HttpPostedFileBase file)
        //{
        //    string mensaje = "";
        //    Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();

        //    try
        //    {
        //        if (this.Request.Files["file"].ContentLength > 0)
        //        {
        //            try
        //            {
        //                //leo

        //                using (var excel = new ExcelPackage(file.InputStream))
        //                {
        //                    var tbl = new DataTable();
        //                    var ws = excel.Workbook.Worksheets.First();
        //                    var hasHeader = true;
        //                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
        //                            : String.Format("Column {0}", firstRowCell.Start.Column));

        //                    int startRow = hasHeader ? 2 : 1;
        //                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //                    {
        //                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //                        DataRow row = tbl.NewRow();
        //                        foreach (var cell in wsRow)
        //                            row[cell.Start.Column - 1] = cell.Text;
        //                        tbl.Rows.Add(row);
        //                    }






        //                    Int32 lote = Model.DispensacionMedicamentosCalidad(tbl, ref MsgRes);

        //                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
        //                    {
        //                        mensaje = "SE INGRESO CORRECTAMENTE....";
        //                        return Json(new { success = true, message = mensaje, idMed = lote }, JsonRequestBehavior.AllowGet);
        //                    }
        //                    else
        //                    {
        //                        mensaje = MsgRes.DescriptionResponse;
        //                        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                var alerta = e.Message;
        //                if (alerta.Contains("System.OutOfMemoryException"))
        //                {
        //                    mensaje = "ERROR AL INGRESAR LOS REGISTROS - MEMORIA.";
        //                }
        //                else
        //                {
        //                    mensaje = "ERROR AL INGRESAR LOS REGISTROS.";
        //                }

        //                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
        //            return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "*---ERROR -- - *" + ex.Message;
        //        return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public List<medicamentos_dispen_calidad> CargarMedDispensacion(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContador = 1;
            List<medicamentos_dispen_calidad> result = new List<medicamentos_dispen_calidad>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "BT999999", "Cargue") select c).ToList();
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    medicamentos_dispen_calidad obj = new medicamentos_dispen_calidad();

                    var item = EData1[i];
                    fila++;
                    if (item[0] != null && item[0] != "")
                    {
                        var texto = "";

                        columna = "DEPENDENCIA DE SALUD";
                        texto = item[0];
                        if (texto.Length <= 200)
                        {
                            obj.dependencia_salud = Convert.ToString(item[0]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE LA FACTURA";
                        try
                        {

                            texto = item[1];
                            if (texto.Length <= 200)
                            {
                                obj.fecha_factura = Convert.ToDateTime(item[1]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "NÚMERO DE FACTURA";
                        texto = item[2];
                        if (texto.Length <= 200)
                        {
                            obj.numero_factura = Convert.ToString(item[2]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "TIPO DE PAGO";
                        try
                        {
                            texto = item[3];
                            if (texto.Length != 0)
                            {
                                obj.tipo_pago = Convert.ToInt32(item[3]);
                            }
                            else
                            {
                                obj.tipo_pago = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO";
                        texto = item[4];
                        if (texto.Length <= 200)
                        {
                            obj.nit = Convert.ToString(item[4]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "FECHA DE PRESCRIPCIÓN (FÓRMULA)";
                        try
                        {
                            texto = item[5];
                            if (texto.Length != 0)
                            {
                                obj.fecha_prescripcion = Convert.ToDateTime(item[5]);
                            }
                            else
                            {
                                obj.fecha_prescripcion = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }



                        columna = "NÚMERO DE FÓRMULA";
                        texto = item[6];
                        if (texto.Length <= 200)
                        {
                            obj.numero_formula = Convert.ToString(item[6]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "ORIGEN DE LA FORMULA";
                        texto = item[7];
                        if (texto.Length <= 200)
                        {
                            obj.origen_formula = Convert.ToString(item[7]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE FORMULA";
                        texto = item[8];
                        if (texto.Length <= 200)
                        {
                            obj.tipo_formula = Convert.ToString(item[8]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                        texto = item[9];
                        if (texto.Length <= 200)
                        {
                            obj.tipo_ident_beneficiario = Convert.ToString(item[9]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                        texto = item[10];
                        if (texto.Length <= 200)
                        {
                            obj.num_documento_beneficiario = Convert.ToString(item[11]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE DEL BENEFICIARIO";
                        texto = item[11];
                        if (texto.Length <= 200)
                        {
                            obj.nom_beneficiario = Convert.ToString(item[11]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DE BENEFICIARIO";
                        texto = item[12];
                        if (texto.Length <= 200)
                        {
                            obj.ciudad_beneficiario = Convert.ToString(item[12]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CIUDAD DE DESPACHO.";
                        texto = item[13];
                        if (texto.Length <= 200)
                        {
                            obj.ciudad_despacho = Convert.ToString(item[13]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "UNIS";
                        texto = item[14];
                        if (texto.Length <= 200)
                        {
                            obj.unis = Convert.ToString(item[14]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "ID IPS";
                        texto = item[15];
                        if (texto.Length <= 200)
                        {
                            obj.id_ips = Convert.ToString(item[15]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "NOMPRE IPS DE ATENCIÓN";
                        texto = item[16];
                        if (texto.Length <= 200)
                        {
                            obj.nom_ips_atencion = Convert.ToString(item[16]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "ID. PRESCRIPTOR";
                        texto = item[17];
                        if (texto.Length <= 200)
                        {
                            obj.id_prescriptor = Convert.ToString(item[17]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PRESCRIPTOR";
                        texto = item[18];
                        if (texto.Length <= 200)
                        {
                            obj.prescriptor = Convert.ToString(item[18]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "ESPECIALIDAD";
                        texto = item[19];
                        if (texto.Length <= 200)
                        {
                            obj.especialidad = Convert.ToString(item[19]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)";
                        texto = item[20];
                        if (texto.Length <= 200)
                        {
                            obj.cod_producto_ecop = Convert.ToString(item[20]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CODIGO COMERCIAL";
                        texto = item[21];
                        if (texto.Length <= 200)
                        {
                            obj.cod_comercial = Convert.ToString(item[21]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)";
                        texto = item[22];
                        if (texto.Length <= 200)
                        {
                            obj.cod_interno_medicamento = Convert.ToString(item[22]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)";
                        texto = item[23];
                        if (texto.Length <= 200)
                        {
                            obj.cod_barras_medicamento = Convert.ToString(item[23]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CUM";
                        texto = item[24];
                        if (texto.Length <= 200)
                        {
                            obj.cum = Convert.ToString(item[24]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "REGISTRO SANITARIO";
                        texto = item[25];
                        if (texto.Length <= 200)
                        {
                            obj.registro_sanitario = Convert.ToString(item[25]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CLASIFICACIÓN INVIMA";
                        texto = item[26];
                        if (texto.Length <= 200)
                        {
                            obj.clasificacion_invima = Convert.ToString(item[26]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "CÓDIGO ATC";
                        texto = item[27];
                        if (texto.Length <= 200)
                        {
                            obj.cod_atc = Convert.ToString(item[27]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)";
                        texto = item[28];
                        if (texto.Length <= 200)
                        {
                            obj.grupo_farmacologico = Convert.ToString(item[28]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "NOMBRE DEL MEDICAMENTO EN D.C.I.";
                        texto = item[29];
                        if (texto.Length <= 200)
                        {
                            obj.nom_medicamento_DCI = Convert.ToString(item[29]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "FORMA FARMACÉUTICA";
                        texto = item[30];
                        if (texto.Length <= 200)
                        {
                            obj.forma_farmaceutica = Convert.ToString(item[30]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CONCENTRACIÓN";
                        texto = item[31];
                        if (texto.Length <= 200)
                        {
                            obj.concentracion = Convert.ToString(item[31]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "PRESENTACIÓN";
                        texto = item[32];
                        if (texto.Length <= 200)
                        {
                            obj.presentacion = Convert.ToString(item[32]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                        texto = item[33];
                        if (texto.Length <= 200)
                        {
                            obj.descripcion_producto = Convert.ToString(item[33]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                        texto = item[34];
                        if (texto.Length <= 200)
                        {
                            obj.nom_comercial_medicamento = Convert.ToString(item[34]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "¿COMERCIAL O GENÉRICO?";
                        texto = item[35];
                        if (texto.Length <= 200)
                        {
                            obj.comercial_o_generico = Convert.ToString(item[35]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "ERROR DE PRESCRIPCIÓN";
                        texto = item[36];
                        if (texto.Length <= 200)
                        {
                            obj.error_prescripcion = Convert.ToString(item[36]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "LABORATORIO FABRICANTE";
                        texto = item[37];
                        if (texto.Length <= 200)
                        {
                            obj.laboratorio_fabricante = Convert.ToString(item[37]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR";
                        texto = item[38];
                        if (texto.Length <= 200)
                        {
                            obj.laboratorio_distribuidor = Convert.ToString(item[38]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                        texto = item[39];
                        if (texto.Length <= 200)
                        {
                            obj.laboratorio_regis_sanitario = Convert.ToString(item[39]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "FECHA DE DESPACHO DE LA FÓRMULA";
                        try
                        {
                            texto = item[40];
                            if (texto.Length != null)
                            {
                                obj.fecha_despacho_formula = Convert.ToDateTime(item[40]);
                            }
                            else
                            {
                                obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "UNIDAD MÍNIMA DE ENTREGA";
                        texto = item[41];
                        if (texto.Length <= 100)
                        {
                            obj.unidad_medica_entrega = Convert.ToString(item[41]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE UNIDADES PRESCRITAS";
                        try
                        {
                            texto = item[42];
                            if (texto.Length != null)
                            {
                                obj.numero_unidades_prescritas = Convert.ToInt32(item[42]);
                            }
                            else
                            {
                                obj.numero_unidades_prescritas = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE UNIDADES ENTREGADAS";
                        try
                        {
                            texto = item[43];
                            if (texto.Length != null)
                            {
                                obj.numero_unidades_entregadas = Convert.ToInt32(item[43]);
                            }
                            else
                            {
                                obj.numero_unidades_entregadas = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                        try
                        {
                            texto = item[44];
                            if (texto.Length <= 100)
                            {
                                obj.valor_unitario = Convert.ToInt32(item[44]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "IVA";
                        try
                        {
                            texto = item[45];
                            if (texto.Length <= 100)
                            {
                                obj.iva = Convert.ToInt32(item[45]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "VALOR TOTAL DE LA ENTREGA";
                        try
                        {
                            texto = item[46];
                            if (texto.Length <= 200)
                            {
                                obj.valor_total_entrega = Convert.ToInt32(item[46]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        try
                        {
                            columna = "CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA";
                            texto = item[47];
                            if (texto.Length <= 200)
                            {
                                obj.consecutivo_tiquete = Convert.ToString(item[47]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "PRODUCTO ABE S/N";
                        texto = item[48];
                        if (texto.Length <= 200)
                        {
                            obj.producto_ABE = Convert.ToString(item[48]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "MEDICAMENTO REGULADO S/N";
                        texto = item[49];
                        if (texto.Length <= 200)
                        {
                            obj.medicamento_regulado = Convert.ToString(item[49]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO";
                        texto = item[50];
                        if (texto.Length <= 200)
                        {
                            obj.clasif_producto_gest_riesgo = Convert.ToString(item[50]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)";
                        texto = item[51];
                        if (texto.Length <= 200)
                        {
                            obj.centro_dispensacion = Convert.ToString(item[51]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AMBITO";
                        texto = item[52];
                        if (texto.Length <= 200)
                        {
                            obj.ambito = Convert.ToString(item[52]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "MODALIDAD DE ENTREGA";
                        texto = item[53];
                        if (texto.Length <= 200)
                        {
                            obj.modalidad_entrega = Convert.ToString(item[53]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "DOSIS DIARIA DEFINIDA";
                        try
                        {
                            texto = item[54];
                            if (texto.Length != null)
                            {
                                obj.dosis_diaria_definida = Convert.ToDecimal(item[54]);
                            }
                            else
                            {
                                obj.dosis_diaria_definida = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "DOSIS";
                        try
                        {
                            texto = item[55];
                            if (texto.Length != 0)
                            {
                                obj.dosis = Convert.ToInt32(item[55]);
                            }
                            else
                            {
                                obj.dosis = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "DIAG P";
                        texto = item[56];
                        if (texto.Length <= 200)
                        {
                            obj.diag_p = Convert.ToString(item[56]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DIAG S";
                        texto = item[57];
                        if (texto.Length <= 200)
                        {
                            obj.diag_s = Convert.ToString(item[57]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AÑO DE REPORTE";
                        try
                        {
                            texto = item[58];
                            if (texto.Length != null)
                            {
                                obj.año_reporte = Convert.ToInt32(item[58]);
                            }
                            else
                            {
                                obj.año_reporte = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "MES DE REPORTE";
                        texto = item[59];
                        if (texto.Length <= 200)
                        {
                            obj.mes_reporte = Convert.ToString(item[59]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CODIGO FAMILIAR";
                        try
                        {
                            texto = item[60];
                            if (texto.Length != null)
                            {
                                obj.codigo_familiar = Convert.ToInt32(item[60]);
                            }
                            else
                            {
                                obj.codigo_familiar = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE SALUD";
                        texto = item[61];
                        if (texto.Length <= 200)
                        {
                            obj.tipo_salud = Convert.ToString(item[61]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION";
                        texto = item[62];
                        if (texto.Length <= 200)
                        {
                            obj.medico_general_asignado_periodoDispensacion = Convert.ToString(item[62]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "EDAD";
                        try
                        {
                            texto = item[63];
                            if (texto.Length != null)
                            {
                                obj.edad = Convert.ToInt32(item[63]);
                            }
                            else
                            {
                                obj.edad = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "GRUPO ETAREO";
                        texto = item[64];
                        if (texto.Length <= 200)
                        {
                            obj.grupo_etareo = Convert.ToString(item[64]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "GENERO";
                        texto = item[65];
                        if (texto.Length <= 200)
                        {
                            obj.genero = Convert.ToString(item[65]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DE DESPACHO HOMOLOGADA";
                        texto = item[66];
                        if (texto.Length <= 200)
                        {
                            obj.ciudad_despacho_homologada = Convert.ToString(item[66]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LOCALIDAD DE DESPACHO HOMOLOGADA";
                        texto = item[67];
                        if (texto.Length <= 200)
                        {
                            obj.localidad_despacho_homologada = Convert.ToString(item[67]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL DE DESPACHO HOMOLOGADA";
                        texto = item[68];
                        if (texto.Length <= 200)
                        {
                            obj.regional_despacho_homologada = Convert.ToString(item[68]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DEL BENEFICIARIO VALIDADA";
                        texto = item[69];
                        if (texto.Length <= 200)
                        {
                            obj.ciudad_beneficiario_oValidada = Convert.ToString(item[69]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LOCALIDAD DE BENEFICIARIO VALIDADA";
                        texto = item[70];
                        if (texto.Length <= 200)
                        {
                            obj.localidad_beneficiario_oValidada = Convert.ToString(item[70]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL DE BENEFECIARIO VALIDADA";
                        texto = item[71];
                        if (texto.Length <= 200)
                        {
                            obj.regional_beneficiario_oValidada = Convert.ToString(item[71]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = Convert.ToString(SesionVar.UserName);

                        result.Add(obj);
                        obj = new medicamentos_dispen_calidad();
                    }
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                if (textError != "" && textError != null)
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
                }
                else
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                }
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        //public ActionResult TableroMedicamentosDispen(int? año, int? mes, int? regional, int? prestador)
        public ActionResult TableroMedicamentosDispen(DateTime? fechaIni, DateTime? fechaFin)
        {
            var idUser = SesionVar.IDUser;

            List<management_medicamentosDispen_listResult> list = new List<management_medicamentosDispen_listResult>();
            var opera = 0;

            try
            {
                list = BusClass.ListaMedicamentosDispensacion(fechaIni, fechaFin);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            Session["listaMedicamentos"] = list;

            ViewBag.conteo = list.Count();
            ViewBag.list = list;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.meses = BusClass.meses();

            return View();
        }

        public PartialViewResult ConsolidadoMedicamentosDispen(int idCargueBase)
        {
            List<management_medicamentosDispen_consolidadoIdResult> modelo = BusClass.ListaMedicamentosDispensacionConsolidado(idCargueBase).OrderBy(l => l.nom_medicamento_DCI).ToList();
            Session["ConsolidadoMedicamentosDispen"] = modelo;
            return PartialView(modelo);
        }

        public JsonResult EliminarCargueBaseMedicamentosDispen(int? idCargue)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                BusClass.EliminarCargueDispen((int)idCargue, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    mensaje = "CARGUE ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DEL CARGUE";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DEL CARGUE: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }


        public void ExcelConsolidadoMedicamentosDispensacion()
        {
            try
            {
                List<management_medicamentosDispen_consolidadoIdResult> listareporte = new List<management_medicamentosDispen_consolidadoIdResult>();
                listareporte = (List<management_medicamentosDispen_consolidadoIdResult>)Session["ConsolidadoMedicamentosDispen"];

                if (listareporte.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte Consolidado Med Dispensacion");

                    Sheet.Cells["A1:G1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:G1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:G1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:G1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Dependencia salud";
                    Sheet.Cells["B1"].Value = "Ciudad beneficiario";
                    Sheet.Cells["C1"].Value = "Ciudad despacho";
                    Sheet.Cells["D1"].Value = "Unis";
                    Sheet.Cells["E1"].Value = "Código comercial";
                    Sheet.Cells["F1"].Value = "Nombre medicamento DCI";
                    Sheet.Cells["G1"].Value = "Valor total";

                    int row = 2;
                    foreach (management_medicamentosDispen_consolidadoIdResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.dependencia_salud;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.ciudad_beneficiario;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ciudad_despacho;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.unis;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.cod_comercial;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nom_medicamento_DCI;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.valor_total_entrega;
                        row++;
                    }
                    Sheet.Cells["A:G"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsolidadoMedicamentos_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                             "window.alert('NO HAY DATOS POR MOSTRAR');" +
                             "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Error: " + ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                               "window.alert('ERROR EN LA DESCARGA');" +
                               "</script> ";
                Response.Write(rta);
                Response.End();
                //throw new Exception(mensaje);
            }
        }
        //public void ExcelExportar()
        //{
        //    List<management_medicamentosDispen_listResult> listareporte = (List<management_medicamentosDispen_listResult>)Session["listaMedicamentos"];
        //    ExcelPackage Ep = new ExcelPackage();
        //    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

        //    Sheet.Cells["A1:BT1"].Style.Font.Bold = true;
        //    Color colFromHex = Color.FromArgb(22, 54, 92);
        //    Sheet.Cells["A1:BT1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    Sheet.Cells["A1:BT1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
        //    Sheet.Cells["A1:BT1"].Style.Font.Color.SetColor(Color.White);
        //    Sheet.Cells["A1:BT1"].Style.Font.Name = "Century Gothic";

        //    Sheet.Cells["A1"].Value = "DEPENDENCIA DE SALUD";
        //    Sheet.Cells["B1"].Value = "FECHA DE LA FACTURA";
        //    Sheet.Cells["C1"].Value = "NÚMERO DE FACTURA";
        //    Sheet.Cells["D1"].Value = "TIPO DE PAGO";
        //    Sheet.Cells["E1"].Value = "CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO";
        //    Sheet.Cells["F1"].Value = "FECHA DE PRESCRIPCIÓN (FÓRMULA)";
        //    Sheet.Cells["G1"].Value = "NUMERO FACTURA";
        //    Sheet.Cells["H1"].Value = "ORIGEN DE LA FORMULA";
        //    Sheet.Cells["I1"].Value = "TIPO DE FORMULA";
        //    Sheet.Cells["J1"].Value = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
        //    Sheet.Cells["K1"].Value = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
        //    Sheet.Cells["L1"].Value = "NOMBRE DEL BENEFICIARIO";
        //    Sheet.Cells["M1"].Value = "CIUDAD DE BENEFICIARIO";
        //    Sheet.Cells["N1"].Value = "CIUDAD DE DESPACHO.";
        //    Sheet.Cells["O1"].Value = "UNIS";
        //    Sheet.Cells["P1"].Value = "ID IPS";
        //    Sheet.Cells["Q1"].Value = "NOMPRE IPS DE ATENCIÓN";
        //    Sheet.Cells["R1"].Value = "ID. PRESCRIPTOR";
        //    Sheet.Cells["S1"].Value = "PRESCRIPTOR";
        //    Sheet.Cells["T1"].Value = "ESPECIALIDAD";
        //    Sheet.Cells["U1"].Value = "CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)";
        //    Sheet.Cells["V1"].Value = "CODIGO COMERCIAL";
        //    Sheet.Cells["W1"].Value = "CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)";
        //    Sheet.Cells["X1"].Value = "CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)";
        //    Sheet.Cells["Y1"].Value = "CUM";
        //    Sheet.Cells["Z1"].Value = "REGISTRO SANITARIO";
        //    Sheet.Cells["AA1"].Value = "CLASIFICACIÓN INVIMA";
        //    Sheet.Cells["AB1"].Value = "CÓDIGO ATC";
        //    Sheet.Cells["AC1"].Value = "GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)";
        //    Sheet.Cells["AD1"].Value = "NOMBRE DEL MEDICAMENTO EN D.C.I.";
        //    Sheet.Cells["AE1"].Value = "FORMA FARMACÉUTICA";
        //    Sheet.Cells["AF1"].Value = "CONCENTRACIÓN";
        //    Sheet.Cells["AG1"].Value = "PRESENTACIÓN";
        //    Sheet.Cells["AH1"].Value = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
        //    Sheet.Cells["AI1"].Value = "NOMBRE COMERCIAL DEL MEDICAMENTO";
        //    Sheet.Cells["AJ1"].Value = "¿COMERCIAL O GENÉRICO?";
        //    Sheet.Cells["AK1"].Value = "ERROR DE PRESCRIPCIÓN";
        //    Sheet.Cells["AL1"].Value = "LABORATORIO FABRICANTE";
        //    Sheet.Cells["AM1"].Value = "LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR";
        //    Sheet.Cells["AN1"].Value = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
        //    Sheet.Cells["AO1"].Value = "FECHA DE DESPACHO DE LA FÓRMULA";
        //    Sheet.Cells["AP1"].Value = "UNIDAD MÍNIMA DE ENTREGA";
        //    Sheet.Cells["AQ1"].Value = "NÚMERO DE UNIDADES PRESCRITAS";
        //    Sheet.Cells["AR1"].Value = "NÚMERO DE UNIDADES ENTREGADAS";
        //    Sheet.Cells["AS1"].Value = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
        //    Sheet.Cells["AT1"].Value = "IVA";
        //    Sheet.Cells["AU1"].Value = "VALOR TOTAL DE LA ENTREGA";
        //    Sheet.Cells["AV1"].Value = "CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA";
        //    Sheet.Cells["AW1"].Value = "PRODUCTO ABE S/N";
        //    Sheet.Cells["AX1"].Value = "MEDICAMENTO REGULADO S/N";
        //    Sheet.Cells["AY1"].Value = "CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO";
        //    Sheet.Cells["AZ1"].Value = "CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)";
        //    Sheet.Cells["BA1"].Value = "AMBITO";
        //    Sheet.Cells["BB1"].Value = "MODALIDAD DE ENTREGA";
        //    Sheet.Cells["BC1"].Value = "DOSIS DIARIA DEFINIDA";
        //    Sheet.Cells["BD1"].Value = "DOSIS";
        //    Sheet.Cells["BE1"].Value = "DIAG P";
        //    Sheet.Cells["BF1"].Value = "DIAG S";
        //    Sheet.Cells["BG1"].Value = "AÑO DE REPORTE";
        //    Sheet.Cells["BH1"].Value = "MES DE REPORTE";
        //    Sheet.Cells["BI1"].Value = "CODIGO FAMILIAR";
        //    Sheet.Cells["BJ1"].Value = "TIPO DE SALUD";
        //    Sheet.Cells["BK1"].Value = "MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION";
        //    Sheet.Cells["BL1"].Value = "EDAD";
        //    Sheet.Cells["BM1"].Value = "GRUPO ETAREO";
        //    Sheet.Cells["BN1"].Value = "GENERO";
        //    Sheet.Cells["BO1"].Value = "CIUDAD DE DESPACHO HOMOLOGADA";
        //    Sheet.Cells["BP1"].Value = "LOCALIDAD DE DESPACHO HOMOLOGADA";
        //    Sheet.Cells["BQ1"].Value = "REGIONAL DE DESPACHO HOMOLOGADA";
        //    Sheet.Cells["BR1"].Value = "CIUDAD DEL BENEFICIARIO VALIDADA";
        //    Sheet.Cells["BS1"].Value = "LOCALIDAD DE BENEFICIARIO VALIDADA";
        //    Sheet.Cells["BT1"].Value = "REGIONAL DE BENEFECIARIO VALIDADA";

        //    int row = 2;
        //    foreach (management_medicamentosDispen_listResult item in listareporte)
        //    {
        //        Sheet.Cells[string.Format("A{0}", row)].Value = item.dependencia_salud;
        //        Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_factura;
        //        Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura;
        //        Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_pago;
        //        Sheet.Cells[string.Format("E{0}", row)].Value = item.nit;
        //        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_prescripcion;
        //        Sheet.Cells[string.Format("G{0}", row)].Value = item.numero_formula;
        //        Sheet.Cells[string.Format("H{0}", row)].Value = item.origen_formula;
        //        Sheet.Cells[string.Format("I{0}", row)].Value = item.tipo_formula;
        //        Sheet.Cells[string.Format("J{0}", row)].Value = item.tipo_ident_beneficiario;
        //        Sheet.Cells[string.Format("K{0}", row)].Value = item.num_documento_beneficiario;
        //        Sheet.Cells[string.Format("L{0}", row)].Value = item.nom_beneficiario;
        //        Sheet.Cells[string.Format("M{0}", row)].Value = item.ciudad_beneficiario;
        //        Sheet.Cells[string.Format("N{0}", row)].Value = item.ciudad_despacho;
        //        Sheet.Cells[string.Format("O{0}", row)].Value = item.unis;
        //        Sheet.Cells[string.Format("P{0}", row)].Value = item.id_ips;
        //        Sheet.Cells[string.Format("Q{0}", row)].Value = item.nom_ips_atencion;
        //        Sheet.Cells[string.Format("R{0}", row)].Value = item.id_prescriptor;
        //        Sheet.Cells[string.Format("S{0}", row)].Value = item.prescriptor;
        //        Sheet.Cells[string.Format("T{0}", row)].Value = item.especialidad;
        //        Sheet.Cells[string.Format("U{0}", row)].Value = item.cod_producto_ecop;
        //        Sheet.Cells[string.Format("V{0}", row)].Value = item.cod_comercial;
        //        Sheet.Cells[string.Format("W{0}", row)].Value = item.cod_interno_medicamento;
        //        Sheet.Cells[string.Format("X{0}", row)].Value = item.cod_barras_medicamento;
        //        Sheet.Cells[string.Format("Y{0}", row)].Value = item.cum;
        //        Sheet.Cells[string.Format("Z{0}", row)].Value = item.registro_sanitario;
        //        Sheet.Cells[string.Format("AA{0}", row)].Value = item.clasificacion_invima;
        //        Sheet.Cells[string.Format("AB{0}", row)].Value = item.cod_atc;
        //        Sheet.Cells[string.Format("AC{0}", row)].Value = item.grupo_farmacologico;
        //        Sheet.Cells[string.Format("AD{0}", row)].Value = item.nom_medicamento_DCI;
        //        Sheet.Cells[string.Format("AE{0}", row)].Value = item.forma_farmaceutica;
        //        Sheet.Cells[string.Format("AF{0}", row)].Value = item.concentracion;
        //        Sheet.Cells[string.Format("AG{0}", row)].Value = item.presentacion;
        //        Sheet.Cells[string.Format("AH{0}", row)].Value = item.descripcion_producto;
        //        Sheet.Cells[string.Format("AI{0}", row)].Value = item.nom_comercial_medicamento;
        //        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.comercial_o_generico;
        //        Sheet.Cells[string.Format("AK{0}", row)].Value = item.error_prescripcion;
        //        Sheet.Cells[string.Format("AL{0}", row)].Value = item.laboratorio_fabricante;
        //        Sheet.Cells[string.Format("AM{0}", row)].Value = item.laboratorio_distribuidor;
        //        Sheet.Cells[string.Format("AN{0}", row)].Value = item.laboratorio_regis_sanitario;
        //        Sheet.Cells[string.Format("AO{0}", row)].Value = item.fecha_despacho_formula;
        //        Sheet.Cells[string.Format("AP{0}", row)].Value = item.unidad_medica_entrega;
        //        Sheet.Cells[string.Format("AQ{0}", row)].Value = item.numero_unidades_prescritas;
        //        Sheet.Cells[string.Format("AR{0}", row)].Value = item.numero_unidades_entregadas;
        //        Sheet.Cells[string.Format("AS{0}", row)].Value = item.valor_unitario;
        //        Sheet.Cells[string.Format("AT{0}", row)].Value = item.iva;
        //        Sheet.Cells[string.Format("AU{0}", row)].Value = item.valor_total_entrega;
        //        Sheet.Cells[string.Format("AV{0}", row)].Value = item.consecutivo_tiquete;
        //        Sheet.Cells[string.Format("AW{0}", row)].Value = item.producto_ABE;
        //        Sheet.Cells[string.Format("AX{0}", row)].Value = item.medicamento_regulado;
        //        Sheet.Cells[string.Format("AY{0}", row)].Value = item.clasif_producto_gest_riesgo;
        //        Sheet.Cells[string.Format("AZ{0}", row)].Value = item.centro_dispensacion;
        //        Sheet.Cells[string.Format("BA{0}", row)].Value = item.ambito;
        //        Sheet.Cells[string.Format("BB{0}", row)].Value = item.modalidad_entrega;
        //        Sheet.Cells[string.Format("BC{0}", row)].Value = item.dosis_diaria_definida;
        //        Sheet.Cells[string.Format("BD{0}", row)].Value = item.dosis;
        //        Sheet.Cells[string.Format("BE{0}", row)].Value = item.diag_p;
        //        Sheet.Cells[string.Format("BF{0}", row)].Value = item.diag_s;
        //        Sheet.Cells[string.Format("BG{0}", row)].Value = item.año_reporte;
        //        Sheet.Cells[string.Format("BH{0}", row)].Value = item.mes_reporte;
        //        Sheet.Cells[string.Format("BI{0}", row)].Value = item.codigo_familiar;
        //        Sheet.Cells[string.Format("BJ{0}", row)].Value = item.tipo_salud;
        //        Sheet.Cells[string.Format("BK{0}", row)].Value = item.medico_general_asignado_periodoDispensacion;
        //        Sheet.Cells[string.Format("BL{0}", row)].Value = item.edad;
        //        Sheet.Cells[string.Format("BM{0}", row)].Value = item.grupo_etareo;
        //        Sheet.Cells[string.Format("BN{0}", row)].Value = item.genero;
        //        Sheet.Cells[string.Format("BO{0}", row)].Value = item.ciudad_despacho_homologada;
        //        Sheet.Cells[string.Format("BP{0}", row)].Value = item.localidad_despacho_homologada;
        //        Sheet.Cells[string.Format("BQ{0}", row)].Value = item.regional_despacho_homologada;
        //        Sheet.Cells[string.Format("BR{0}", row)].Value = item.ciudad_beneficiario_oValidada;
        //        Sheet.Cells[string.Format("BS{0}", row)].Value = item.localidad_beneficiario_oValidada;
        //        Sheet.Cells[string.Format("BT{0}", row)].Value = item.regional_beneficiario_oValidada;


        //        Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        //        Sheet.Cells[string.Format("AO{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

        //        row++;
        //    }

        //    Sheet.Cells["A" + row + ":BT1" + row].Style.Font.Name = "Century Gothic";

        //    Sheet.Cells["A:BT"].AutoFitColumns();

        //    Response.Clear();
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosDispensacion.xlsx");
        //    Response.BinaryWrite(Ep.GetAsByteArray());
        //    Response.End();
        //}

        public void pdfMedicamentos(int idLote)
        {
            try
            {
                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteMedicamentosDispensacion.rdlc");

                List<management_medicamentosDispen_reporteResult> lista = new List<management_medicamentosDispen_reporteResult>();

                string filename = "MedicamentosDispensacion_" + idLote;

                lista = BusClass.ListaMedicamentosDispensacionReporte(idLote);

                var conteo = lista.Count();

                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetMedDispena", lista);

                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);

                if (lista.Count != 0)
                {
                    try
                    {
                        viewer.LocalReport.Refresh();

                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string[] streams;
                        Microsoft.Reporting.WebForms.Warning[] warnings;
                        byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

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
            catch (Exception ex)
            {
                var mensajeError = ex.Message;
            }
        }

        public ActionResult TableroMedicamentosDispenPrestadores(int? año, int? mes)
        {
            var idUser = SesionVar.IDUser;
            List<management_listaMedicDspensacion_agrupacionResult> list = new List<management_listaMedicDspensacion_agrupacionResult>();
            List<management_listaMedicDspensacion_agrupacionResult> consolidado = new List<management_listaMedicDspensacion_agrupacionResult>();
            var opera = 0;

            try
            {
                if ((año != null && año != 0) && (mes != null && mes != 0))
                {
                    list = BusClass.ListaMedicamentosDispensacionPrestadoresAgrupacion((int)mes, (int)año);
                    opera = 1;
                }

                ViewBag.conteo = 0;
                ViewBag.resultado = 0;

                Session["listaMedicamentosPrestadores"] = list;
                Session["mesIndicadores"] = mes;
                Session["añoIndicadores"] = año;

                var conteo = list.Count();

                if (conteo != 0)
                {
                    ViewBag.conteo = conteo;

                    if (opera == 1)
                    {
                        ViewBag.resultado = 1;
                    }
                }
                else
                {
                    if (opera == 1)
                    {
                        ViewBag.resultado = 2;
                    }
                }

                if (conteo != 0)
                {
                    consolidado = list.Take(1).ToList();
                }

                ViewBag.list = list;
                ViewBag.meses = BusClass.meses();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public void ExcelExportarPrestadores()
        {
            List<management_listaMedicDspensacionResult> listareporte = new List<management_listaMedicDspensacionResult>();

            try
            {
                var mes = (int)Session["mesIndicadores"];
                var año = (int)Session["añoIndicadores"];

                listareporte = BusClass.ListaMedicamentosDispensacionPrestadores(mes, año);

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('SIN DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

                Sheet.Cells["A1:BF1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:BF1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:BF1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:BF1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:BF1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "DEPENDENCIA DE SALUD";
                Sheet.Cells["B1"].Value = "FECHA DE LA FACTURA";
                Sheet.Cells["C1"].Value = "NÚMERO DE FACTURA";
                Sheet.Cells["D1"].Value = "TIPO DE PAGO";
                Sheet.Cells["E1"].Value = "CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO";
                Sheet.Cells["F1"].Value = "FECHA DE PRESCRIPCIÓN (FÓRMULA)";
                Sheet.Cells["G1"].Value = "NUMERO FACTURA";
                Sheet.Cells["H1"].Value = "ORIGEN DE LA FORMULA";
                Sheet.Cells["I1"].Value = "TIPO DE FORMULA";
                Sheet.Cells["J1"].Value = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["K1"].Value = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["L1"].Value = "NOMBRE DEL BENEFICIARIO";
                Sheet.Cells["M1"].Value = "CIUDAD DE BENEFICIARIO";
                Sheet.Cells["N1"].Value = "CIUDAD DE DESPACHO.";
                Sheet.Cells["O1"].Value = "UNIS";
                Sheet.Cells["P1"].Value = "ID IPS";
                Sheet.Cells["Q1"].Value = "NOMPRE IPS DE ATENCIÓN";
                Sheet.Cells["R1"].Value = "ID. PRESCRIPTOR";
                Sheet.Cells["S1"].Value = "PRESCRIPTOR";
                Sheet.Cells["T1"].Value = "ESPECIALIDAD";
                Sheet.Cells["U1"].Value = "CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)";
                Sheet.Cells["V1"].Value = "CODIGO COMERCIAL";
                Sheet.Cells["W1"].Value = "CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)";
                Sheet.Cells["X1"].Value = "CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)";
                Sheet.Cells["Y1"].Value = "CUM";
                Sheet.Cells["Z1"].Value = "REGISTRO SANITARIO";
                Sheet.Cells["AA1"].Value = "CLASIFICACIÓN INVIMA";
                Sheet.Cells["AB1"].Value = "CÓDIGO ATC";
                Sheet.Cells["AC1"].Value = "GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)";
                Sheet.Cells["AD1"].Value = "NOMBRE DEL MEDICAMENTO EN D.C.I.";
                Sheet.Cells["AE1"].Value = "FORMA FARMACÉUTICA";
                Sheet.Cells["AF1"].Value = "CONCENTRACIÓN";
                Sheet.Cells["AG1"].Value = "PRESENTACIÓN";
                Sheet.Cells["AH1"].Value = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                Sheet.Cells["AI1"].Value = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                Sheet.Cells["AJ1"].Value = "¿COMERCIAL O GENÉRICO?";
                Sheet.Cells["AK1"].Value = "ERROR DE PRESCRIPCIÓN";
                Sheet.Cells["AL1"].Value = "LABORATORIO FABRICANTE";
                Sheet.Cells["AM1"].Value = "LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR";
                Sheet.Cells["AN1"].Value = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                Sheet.Cells["AO1"].Value = "FECHA DE DESPACHO DE LA FÓRMULA";
                Sheet.Cells["AP1"].Value = "UNIDAD MÍNIMA DE ENTREGA";
                Sheet.Cells["AQ1"].Value = "NÚMERO DE UNIDADES PRESCRITAS";
                Sheet.Cells["AR1"].Value = "NÚMERO DE UNIDADES ENTREGADAS";
                Sheet.Cells["AS1"].Value = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                Sheet.Cells["AT1"].Value = "IVA";
                Sheet.Cells["AU1"].Value = "VALOR TOTAL DE LA ENTREGA";
                Sheet.Cells["AV1"].Value = "CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA";
                Sheet.Cells["AW1"].Value = "PRODUCTO ABE S/N";
                Sheet.Cells["AX1"].Value = "MEDICAMENTO REGULADO S/N";
                Sheet.Cells["AY1"].Value = "CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO";
                Sheet.Cells["AZ1"].Value = "CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)";
                Sheet.Cells["BA1"].Value = "AMBITO";
                Sheet.Cells["BB1"].Value = "MODALIDAD DE ENTREGA";
                Sheet.Cells["BC1"].Value = "DOSIS DIARIA DEFINIDA";
                Sheet.Cells["BD1"].Value = "DOSIS";
                Sheet.Cells["BE1"].Value = "DIAG P";
                Sheet.Cells["BF1"].Value = "DIAG S";

                int row = 2;
                foreach (management_listaMedicDspensacionResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.dependencia_salud;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_factura;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_pago;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.nit;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_prescripcion;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.numero_formula;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.origen_formula;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.tipo_formula;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.tipo_ident_beneficiario;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.num_documento_beneficiario;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.nom_beneficiario;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.ciudad_beneficiario;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.ciudad_despacho;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.id_ips;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.nom_ips_atencion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.id_prescriptor;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.prescriptor;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.especialidad;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.cod_producto_ecop;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.cod_comercial;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.cod_interno_medicamento;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.cod_barras_medicamento;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.cum;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.registro_sanitario;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.clasificacion_invima;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.cod_atc;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.grupo_farmacologico;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.nom_medicamento_DCI;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.forma_farmaceutica;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.concentracion;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.presentacion;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = item.descripcion_producto;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.nom_comercial_medicamento;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.comercial_o_generico;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = item.error_prescripcion;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = item.laboratorio_fabricante;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = item.laboratorio_distribuidor;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = item.laboratorio_regis_sanitario;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = item.fecha_despacho_formula;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = item.unidad_medica_entrega;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = item.numero_unidades_prescritas;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = item.numero_unidades_entregadas;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = item.valor_unitario;
                    Sheet.Cells[string.Format("AT{0}", row)].Value = item.iva;
                    Sheet.Cells[string.Format("AU{0}", row)].Value = item.valor_total_entrega;
                    Sheet.Cells[string.Format("AV{0}", row)].Value = item.consecutivo_tiquete;
                    Sheet.Cells[string.Format("AW{0}", row)].Value = item.producto_ABE;
                    Sheet.Cells[string.Format("AX{0}", row)].Value = item.medicamento_regulado;
                    Sheet.Cells[string.Format("AY{0}", row)].Value = item.clasif_producto_gest_riesgo;
                    Sheet.Cells[string.Format("AZ{0}", row)].Value = item.centro_dispensacion;
                    Sheet.Cells[string.Format("BA{0}", row)].Value = item.ambito;
                    Sheet.Cells[string.Format("BB{0}", row)].Value = item.modalidad_entrega;
                    Sheet.Cells[string.Format("BC{0}", row)].Value = item.dosis_diaria_definida;
                    Sheet.Cells[string.Format("BD{0}", row)].Value = item.dosis;
                    Sheet.Cells[string.Format("BE{0}", row)].Value = item.diag_p;
                    Sheet.Cells[string.Format("BF{0}", row)].Value = item.diag_s;

                    Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AO{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A" + row + ":BF" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:BF"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosDispensacionPrestadores.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR EN LA DESCARGA DEL REPORTE.');" +
                  "</script> ";
                Response.Write(rta);
            }
        }

        public ActionResult TableroConsultaMedicamentos()
        {
            return View();
        }

        public void ReporteMedicamentosDispensacion(string documento, string familiar, string formula, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                List<management_medicamentosDispen_consulta_armadaResult> listareporte = new List<management_medicamentosDispen_consulta_armadaResult>();
                listareporte = BusClass.ListaMedicamentosDispenConsultaArmada((DateTime)fechaInicio, (DateTime)fechaFin, documento, familiar, formula);

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('NO SE HAN ENCONTRADO RESULTADOS.');" +
                      "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                //--kevin
                else
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("MedicamentosDispensacion");

                    Sheet.Cells["A1:BT1"].Style.Font.Bold = true;
                    Color colFromHex = Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:BT1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:BT1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:BT1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:BT1"].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A1"].Value = "DEPENDENCIA DE SALUD";
                    Sheet.Cells["B1"].Value = "FECHA DE LA FACTURA";
                    Sheet.Cells["C1"].Value = "NÚMERO DE FACTURA";
                    Sheet.Cells["D1"].Value = "TIPO DE PAGO";
                    Sheet.Cells["E1"].Value = "CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO";
                    Sheet.Cells["F1"].Value = "FECHA DE PRESCRIPCIÓN (FÓRMULA)";
                    Sheet.Cells["G1"].Value = "NUMERO FACTURA";
                    Sheet.Cells["H1"].Value = "ORIGEN DE LA FORMULA";
                    Sheet.Cells["I1"].Value = "TIPO DE FORMULA";
                    Sheet.Cells["J1"].Value = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                    Sheet.Cells["K1"].Value = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                    Sheet.Cells["L1"].Value = "NOMBRE DEL BENEFICIARIO";
                    Sheet.Cells["M1"].Value = "CIUDAD DE BENEFICIARIO";
                    Sheet.Cells["N1"].Value = "CIUDAD DE DESPACHO.";
                    Sheet.Cells["O1"].Value = "UNIS";
                    Sheet.Cells["P1"].Value = "ID IPS";
                    Sheet.Cells["Q1"].Value = "NOMPRE IPS DE ATENCIÓN";
                    Sheet.Cells["R1"].Value = "ID. PRESCRIPTOR";
                    Sheet.Cells["S1"].Value = "PRESCRIPTOR";
                    Sheet.Cells["T1"].Value = "ESPECIALIDAD";
                    Sheet.Cells["U1"].Value = "CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)";
                    Sheet.Cells["V1"].Value = "CODIGO COMERCIAL";
                    Sheet.Cells["W1"].Value = "CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)";
                    Sheet.Cells["X1"].Value = "CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)";
                    Sheet.Cells["Y1"].Value = "CUM";
                    Sheet.Cells["Z1"].Value = "REGISTRO SANITARIO";
                    Sheet.Cells["AA1"].Value = "CLASIFICACIÓN INVIMA";
                    Sheet.Cells["AB1"].Value = "CÓDIGO ATC";
                    Sheet.Cells["AC1"].Value = "GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)";
                    Sheet.Cells["AD1"].Value = "NOMBRE DEL MEDICAMENTO EN D.C.I.";
                    Sheet.Cells["AE1"].Value = "FORMA FARMACÉUTICA";
                    Sheet.Cells["AF1"].Value = "CONCENTRACIÓN";
                    Sheet.Cells["AG1"].Value = "PRESENTACIÓN";
                    Sheet.Cells["AH1"].Value = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                    Sheet.Cells["AI1"].Value = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                    Sheet.Cells["AJ1"].Value = "¿COMERCIAL O GENÉRICO?";
                    Sheet.Cells["AK1"].Value = "ERROR DE PRESCRIPCIÓN";
                    Sheet.Cells["AL1"].Value = "LABORATORIO FABRICANTE";
                    Sheet.Cells["AM1"].Value = "LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR";
                    Sheet.Cells["AN1"].Value = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                    Sheet.Cells["AO1"].Value = "FECHA DE DESPACHO DE LA FÓRMULA";
                    Sheet.Cells["AP1"].Value = "UNIDAD MÍNIMA DE ENTREGA";
                    Sheet.Cells["AQ1"].Value = "NÚMERO DE UNIDADES PRESCRITAS";
                    Sheet.Cells["AR1"].Value = "NÚMERO DE UNIDADES ENTREGADAS";
                    Sheet.Cells["AS1"].Value = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                    Sheet.Cells["AT1"].Value = "IVA";
                    Sheet.Cells["AU1"].Value = "VALOR TOTAL DE LA ENTREGA";
                    Sheet.Cells["AV1"].Value = "CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA";
                    Sheet.Cells["AW1"].Value = "PRODUCTO ABE S/N";
                    Sheet.Cells["AX1"].Value = "MEDICAMENTO REGULADO S/N";
                    Sheet.Cells["AY1"].Value = "CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO";
                    Sheet.Cells["AZ1"].Value = "CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)";
                    Sheet.Cells["BA1"].Value = "AMBITO";
                    Sheet.Cells["BB1"].Value = "MODALIDAD DE ENTREGA";
                    Sheet.Cells["BC1"].Value = "DOSIS DIARIA DEFINIDA";
                    Sheet.Cells["BD1"].Value = "DOSIS";
                    Sheet.Cells["BE1"].Value = "DIAG P";
                    Sheet.Cells["BF1"].Value = "DIAG S";
                    Sheet.Cells["BG1"].Value = "AÑO DE REPORTE";
                    Sheet.Cells["BH1"].Value = "MES DE REPORTE";
                    Sheet.Cells["BI1"].Value = "CODIGO FAMILIAR";
                    Sheet.Cells["BJ1"].Value = "TIPO DE SALUD";
                    Sheet.Cells["BK1"].Value = "MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION";
                    Sheet.Cells["BL1"].Value = "EDAD";
                    Sheet.Cells["BM1"].Value = "GRUPO ETAREO";
                    Sheet.Cells["BN1"].Value = "GENERO";
                    Sheet.Cells["BO1"].Value = "CIUDAD DE DESPACHO HOMOLOGADA";
                    Sheet.Cells["BP1"].Value = "LOCALIDAD DE DESPACHO HOMOLOGADA";
                    Sheet.Cells["BQ1"].Value = "REGIONAL DE DESPACHO HOMOLOGADA";
                    Sheet.Cells["BR1"].Value = "CIUDAD DEL BENEFICIARIO VALIDADA";
                    Sheet.Cells["BS1"].Value = "LOCALIDAD DE BENEFICIARIO VALIDADA";
                    Sheet.Cells["BT1"].Value = "REGIONAL DE BENEFECIARIO VALIDADA";

                    int row = 2;
                    foreach (management_medicamentosDispen_consulta_armadaResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.dependencia_salud;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.fecha_factura;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.numero_factura;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.tipo_pago;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nit;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_prescripcion;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.numero_formula;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.origen_formula;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.tipo_formula;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.tipo_ident_beneficiario;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.num_documento_beneficiario;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.nom_beneficiario;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.ciudad_beneficiario;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.ciudad_despacho;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.unis;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.id_ips;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.nom_ips_atencion;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.id_prescriptor;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.prescriptor;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.especialidad;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.cod_producto_ecop;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.cod_comercial;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.cod_interno_medicamento;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.cod_barras_medicamento;
                        Sheet.Cells[string.Format("Y{0}", row)].Value = item.cum;
                        Sheet.Cells[string.Format("Z{0}", row)].Value = item.registro_sanitario;
                        Sheet.Cells[string.Format("AA{0}", row)].Value = item.clasificacion_invima;
                        Sheet.Cells[string.Format("AB{0}", row)].Value = item.cod_atc;
                        Sheet.Cells[string.Format("AC{0}", row)].Value = item.grupo_farmacologico;
                        Sheet.Cells[string.Format("AD{0}", row)].Value = item.nom_medicamento_DCI;
                        Sheet.Cells[string.Format("AE{0}", row)].Value = item.forma_farmaceutica;
                        Sheet.Cells[string.Format("AF{0}", row)].Value = item.concentracion;
                        Sheet.Cells[string.Format("AG{0}", row)].Value = item.presentacion;
                        Sheet.Cells[string.Format("AH{0}", row)].Value = item.descripcion_producto;
                        Sheet.Cells[string.Format("AI{0}", row)].Value = item.nom_comercial_medicamento;
                        Sheet.Cells[string.Format("AJ{0}", row)].Value = item.comercial_o_generico;
                        Sheet.Cells[string.Format("AK{0}", row)].Value = item.error_prescripcion;
                        Sheet.Cells[string.Format("AL{0}", row)].Value = item.laboratorio_fabricante;
                        Sheet.Cells[string.Format("AM{0}", row)].Value = item.laboratorio_distribuidor;
                        Sheet.Cells[string.Format("AN{0}", row)].Value = item.laboratorio_regis_sanitario;
                        Sheet.Cells[string.Format("AO{0}", row)].Value = item.fecha_despacho_formula;
                        Sheet.Cells[string.Format("AP{0}", row)].Value = item.unidad_medica_entrega;
                        Sheet.Cells[string.Format("AQ{0}", row)].Value = item.numero_unidades_prescritas;
                        Sheet.Cells[string.Format("AR{0}", row)].Value = item.numero_unidades_entregadas;
                        Sheet.Cells[string.Format("AS{0}", row)].Value = item.valor_unitario;
                        Sheet.Cells[string.Format("AT{0}", row)].Value = item.iva;
                        Sheet.Cells[string.Format("AU{0}", row)].Value = item.valor_total_entrega;
                        Sheet.Cells[string.Format("AV{0}", row)].Value = item.consecutivo_tiquete;
                        Sheet.Cells[string.Format("AW{0}", row)].Value = item.producto_ABE;
                        Sheet.Cells[string.Format("AX{0}", row)].Value = item.medicamento_regulado;
                        Sheet.Cells[string.Format("AY{0}", row)].Value = item.clasif_producto_gest_riesgo;
                        Sheet.Cells[string.Format("AZ{0}", row)].Value = item.centro_dispensacion;
                        Sheet.Cells[string.Format("BA{0}", row)].Value = item.ambito;
                        Sheet.Cells[string.Format("BB{0}", row)].Value = item.modalidad_entrega;
                        Sheet.Cells[string.Format("BC{0}", row)].Value = item.dosis_diaria_definida;
                        Sheet.Cells[string.Format("BD{0}", row)].Value = item.dosis;
                        Sheet.Cells[string.Format("BE{0}", row)].Value = item.diag_p;
                        Sheet.Cells[string.Format("BF{0}", row)].Value = item.diag_s;
                        Sheet.Cells[string.Format("BG{0}", row)].Value = item.año_reporte;
                        Sheet.Cells[string.Format("BH{0}", row)].Value = item.mes_reporte;
                        Sheet.Cells[string.Format("BI{0}", row)].Value = item.codigo_familiar;
                        Sheet.Cells[string.Format("BJ{0}", row)].Value = item.tipo_salud;
                        Sheet.Cells[string.Format("BK{0}", row)].Value = item.medico_general_asignado_periodoDispensacion;
                        Sheet.Cells[string.Format("BL{0}", row)].Value = item.edad;
                        Sheet.Cells[string.Format("BM{0}", row)].Value = item.grupo_etareo;
                        Sheet.Cells[string.Format("BN{0}", row)].Value = item.genero;
                        Sheet.Cells[string.Format("BO{0}", row)].Value = item.ciudad_despacho_homologada;
                        Sheet.Cells[string.Format("BP{0}", row)].Value = item.localidad_despacho_homologada;
                        Sheet.Cells[string.Format("BQ{0}", row)].Value = item.regional_despacho_homologada;
                        Sheet.Cells[string.Format("BR{0}", row)].Value = item.ciudad_beneficiario_oValidada;
                        Sheet.Cells[string.Format("BS{0}", row)].Value = item.localidad_beneficiario_oValidada;
                        Sheet.Cells[string.Format("BT{0}", row)].Value = item.regional_beneficiario_oValidada;

                        Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("AO{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    Sheet.Cells["A" + row + ":BT1" + row].Style.Font.Name = "Century Gothic";

                    Sheet.Cells["A:BT"].AutoFitColumns();

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosDispensacion.xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('PROBLEMAS AL GENERAR EL REPORTE - CONTACTE CON EL ÁREA SISTEMAS.');" +
                  "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public JsonResult BuscarDocumento_medicamentosDispen()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<management_medicamentosDispen_consulta_filtros_docResult> med = BusClass.ListaMedicamentosDispenConsultaFiltroDoc(term);

                    var lista = (from m in med
                                 select new
                                 {
                                     documento = m.num_documento_beneficiario,
                                     label = m.num_documento_beneficiario
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
                var error = e.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarFamiliar_medicamentosDispen()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<management_medicamentosDispen_consulta_filtros_familiarResult> med = BusClass.ListaMedicamentosDispenConsultaFiltroFamiliar(term);

                    var lista = (from m in med
                                 select new
                                 {
                                     familiar = m.codigo_familiar,
                                     label = m.codigo_familiar
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
                var error = e.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarFormula_medicamentosDispen()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<management_medicamentosDispen_consulta_filtros_formulaResult> med = BusClass.ListaMedicamentosDispenConsultaFiltroFormu(term);

                    var lista = (from m in med
                                 select new
                                 {
                                     formula = m.numero_formula,
                                     label = m.numero_formula
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
                var error = e.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargueAlertasDispensacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargueAlertasDispensacion(List<HttpPostedFileBase> files)
        {
            var mensaje = "";
            var rta = 0;

            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<int> respuesta = new List<int>();
            var pasa = 0;

            try
            {
                if (files != null)
                {
                    HttpPostedFileBase file = files[0];

                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(file.InputStream, asposeOptions);
                    Worksheet worksheet = wb.Worksheets[0];
                    Cells cells = worksheet.Cells;
                    int MaximaFila = cells.MaxDataRow;

                    var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                    {
                        CheckMixedValueType = false,
                        ExportColumnName = true,
                        ExportAsString = true
                    };

                    DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                    alertas_dispensacion obj = new alertas_dispensacion();
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    respuesta = Model.ExcelMasivoAlertasDispe(dataTable, obj, ref MsgRes);

                    pasa = respuesta[0];
                    if (pasa == 1)
                    {
                        var nroLote = respuesta[1];
                        var nroDatos = respuesta[2];

                        var resultado = MsgRes.ResponseType;
                        var mensajeSalida = MsgRes.DescriptionResponse;
                        var idUsuario = SesionVar.IDUser;

                        if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "SE INGRESÓ CORRECTAMENTE CARGUE MASIVO ALERTAS #" + nroLote + " con: " + nroDatos + " registros";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ERROR AL INGRESAR CARGUE ALERTAS: " + MsgRes.DescriptionResponse;
                            rta = 2;
                        }
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE ALERTAS: " + MsgRes.DescriptionResponse;
                        rta = 2;
                    }
                }
                else
                {
                    mensaje = "SELECCIONE UN ARCHIVO VALIDO.";
                    rta = 2;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE CARGARON LOS REGISTROS:" + error;
                rta = 2;
            }

            ViewBag.msg = mensaje;
            ViewBag.rta = rta;

            return View();
        }

        public ActionResult TableroControlAlertasMedicamentos(int? rta, string msg, string nombre_comercial, DateTime? fecha_prescripcion, string numero_formula,
              string documento_beneficiario, string id_prescriptor)
        {
            List<management_alertasDispensacion_tableroControlResult> listado = new List<management_alertasDispensacion_tableroControlResult>();
            List<management_alertasDispensacion_tableroControlResult> listadoFinal = new List<management_alertasDispensacion_tableroControlResult>();
            List<management_alertasDispensacion_tableroControlResult> listadoSin = new List<management_alertasDispensacion_tableroControlResult>();
            List<management_alertasDispensacion_tableroControlResult> listadoCon = new List<management_alertasDispensacion_tableroControlResult>();
            List<management_regional_usuarioResult> regionales = new List<management_regional_usuarioResult>();

            var usuario = SesionVar.IDUser;

            try
            {
                regionales = BusClass.ListadoRegionalesUsuarioId(usuario);

                if (regionales.Count() > 0)
                {
                    listado = BusClass.ListadoAlertasDispensacion(fecha_prescripcion, numero_formula, documento_beneficiario, id_prescriptor, nombre_comercial);
                    foreach (var item in regionales)
                    {
                        List<management_alertasDispensacion_tableroControlResult> lista = new List<management_alertasDispensacion_tableroControlResult>();
                        lista = listado.Where(x => x.dependencia_salid == item.indice).ToList();

                        if (lista.Count() > 0)
                        {
                            listadoFinal.AddRange(lista);
                        }
                    }
                }

                listadoSin = listadoFinal.Where(x => x.id_detalle == 0).ToList();
                listadoCon = listadoFinal.Where(x => x.id_detalle != 0).ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoSin = listadoSin;
            ViewBag.listadoCon = listadoCon;

            ViewBag.conteoSin = listadoSin.Count();
            ViewBag.conteoCon = listadoCon.Count();

            ViewBag.msg = msg;
            ViewBag.rta = rta;

            Session["ListadoAlertasSin"] = listadoFinal;
            return View();
        }

        public JsonResult Buscar_nombre_comercial()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<management_alertasDispensacion_buscarNombreComercialResult> nombres = new List<management_alertasDispensacion_buscarNombreComercialResult>();
                    nombres = BusClass.TraerNombreComercial(term);

                    var lista = (from reg in nombres
                                 select new
                                 {
                                     nombre = reg.nombre_comercial_medicamento,
                                     label = reg.nombre_comercial_medicamento,
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

        public ActionResult GestionarCasoAlerta(int? idRegistro, int? idAlerta, int? idDetalle)
        {
            //idGestion = para editar el registro

            List<management_alertasDispensacion_tableroControl_idResult> listadoGestion = new List<management_alertasDispensacion_tableroControl_idResult>();

            try
            {
                listadoGestion = BusClass.TraerRegistroAlerta(idRegistro);
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idRegistro = idRegistro;
            ViewBag.idAlerta = idAlerta;
            ViewBag.listado = listadoGestion;
            ViewBag.idDetalle = idDetalle;

            ViewBag.refTipotramite = BusClass.ListadoTipotramite();

            alertas_dispensacion_detalle detalle = idDetalle != 0 ? BusClass.TraerDatosgestionTramite(idDetalle) : new alertas_dispensacion_detalle();
            List<management_alertasDispensacion_datosTramiteResult> listaTramite = BusClass.TraerDatosAMTramite(idDetalle);

            ViewBag.listaTramite = listaTramite;
            var listaDet = "";
            var listaFechas = "";

            if (listaTramite.Count() > 0)
            {
                foreach (var item in listaTramite)
                {
                    listaDet += listaDet == "" ? Convert.ToString(item.id_gestionTramite) : "|" + Convert.ToString(item.id_gestionTramite);
                    listaFechas += listaFechas == "" ? Convert.ToString(item.fecha_tipo) : "|" + Convert.ToString(item.fecha_tipo);
                }
            }

            ViewBag.listaIdTramite = listaDet;
            ViewBag.listaFechasTramite = listaFechas;

            return View(detalle);
        }

        public JsonResult traerTipoTramite()
        {
            var miproyecto = "";
            var listatotal = new object();
            List<ref_alertas_dispensacion_GestionTramite> tipoTramite = new List<ref_alertas_dispensacion_GestionTramite>();
            try
            {
                tipoTramite = BusClass.ListadoTipotramite();

                ViewBag.Lista = tipoTramite;
                listatotal = (from item in tipoTramite
                              select new
                              {
                                  value = item.id_tipo,
                                  text = item.descripcion,
                              });
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(listatotal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GestionarCasoAlerta(Models.Medicamentos.AlertasMedicamentos Model, List<HttpPostedFileBase> soportes, string ArrayTipoTramite,
            string ArrayTipoTramiteRespuestas)
        {
            var mensaje = "";
            var rta = 2;
            alertas_dispensacion_detalle obj = new alertas_dispensacion_detalle();
            ViewBag.refTipotramite = BusClass.ListadoTipotramite();
            var gestiona = 0;

            try
            {
                obj.id_alerta = Model.id_alerta;
                obj.id_registro = Model.id_registro;
                obj.factura = Model.tienefactura;
                obj.numero_factura = Model.numero_factura;
                obj.valor_facturado = Model.valor_facturado;
                obj.diagnostico_asociaco_medicamento_hc = Model.diagnosticoHC;
                obj.es_medicamento_pertinente = Model.medicamento_pertinente;
                obj.cantidad_dispensada_corresponde_prescrita = Model.cantCorrespondePres;
                obj.cantidades_pertinentes = Model.cantPertinente;
                obj.es_desviacion = Model.desviacion;
                obj.causa_desviacion = Model.causa_desviacion;
                obj.responsable_desviacion = Model.responsable_desviacion;
                obj.plan_accion = Model.plan_accion;
                obj.observacion = Model.observaciones;
                obj.confirmacion_alerta_dispensacion = Model.confirmacionAlerta;
                obj.tipoDato = Model.tipoDato;

                obj.usuario_digita = SesionVar.UserName;
                obj.fecha_digita = DateTime.Now;

                if (Model.id_detalle == null || Model.id_detalle == 0)
                {
                    if (Model.confirmacionAlerta != "Tramite")
                    {
                        obj.fecha_terminada = DateTime.Now;
                    }
                    gestiona = BusClass.InsertarRespuestaAlertaDiepnsacion(obj);
                }
                else
                {
                    obj.id_detalle = (int)Model.id_detalle;
                    obj.fecha_terminada = DateTime.Now;
                    obj.tiene_soporte = Model.tiene_soporte;
                    obj.observacion_soporte = Model.observacion_soporte;
                    obj.fecha_recepcion_soporte = Model.fecha_recepcion_soporte;

                    gestiona = BusClass.ActualizarAlertaDispensacionDetalle(obj);
                }

                if (gestiona != 0)
                {
                    log_alertas_dispensacion_detalle deta = new log_alertas_dispensacion_detalle();
                    deta.id_detalle = (int)Model.id_detalle;
                    deta.id_alerta = Model.id_alerta;
                    deta.id_registro = Model.id_registro;
                    deta.factura = Model.tienefactura;
                    deta.numero_factura = Model.numero_factura;
                    deta.valor_facturado = Model.valor_facturado;
                    deta.diagnostico_asociaco_medicamento_hc = Model.diagnosticoHC;
                    deta.es_medicamento_pertinente = Model.medicamento_pertinente;
                    deta.cantidad_dispensada_corresponde_prescrita = Model.cantCorrespondePres;
                    deta.cantidades_pertinentes = Model.cantPertinente;
                    deta.es_desviacion = Model.desviacion;
                    deta.causa_desviacion = Model.causa_desviacion;
                    deta.responsable_desviacion = Model.responsable_desviacion;
                    deta.plan_accion = Model.plan_accion;
                    deta.observacion = Model.observaciones;
                    deta.confirmacion_alerta_dispensacion = Model.confirmacionAlerta;
                    deta.tipoDato = Model.tipoDato;
                    deta.fecha_terminada = DateTime.Now;
                    deta.tiene_soporte = Model.tiene_soporte;
                    deta.observacion_soporte = Model.observacion_soporte;
                    deta.fecha_recepcion_soporte = Model.fecha_recepcion_soporte;
                    deta.usuario_digita = SesionVar.UserName;
                    deta.fecha_digita = DateTime.Now;

                    var insertaLog = BusClass.InsertarLogRespuestaAlertaDiepnsacion(deta);

                    if (Model.id_detalle == null || Model.id_detalle == 0)
                    {
                        if (Model.confirmacionAlerta == "Tramite")
                        {
                            List<alertas_dispensacion_detalle_entramite> listadoTramite = ArmadoTramite(ArrayTipoTramite, gestiona);
                            if (listadoTramite.Count() > 0)
                            {
                                var insertaMasivotramite = BusClass.InsertarRespuestaAlertaDiepnsacionTramite(listadoTramite);
                            }
                        }
                    }
                    else
                    {
                        List<alertas_dispensacion_detalle_entramite_respuestas> listaRepsuestas = armadoArrayTramiteRespuestas(ArrayTipoTramiteRespuestas, gestiona);
                        if (listaRepsuestas.Count() > 0)
                        {
                            var insertaMasivotramite = BusClass.InsertarRespuestaAlertaDiepnsacionTramiteRespuestas(listaRepsuestas);
                        }
                    }

                    if (soportes.Count() > 0)
                    {
                        for (var i = 0; i < soportes.Count(); i++)
                        {
                            HttpPostedFileBase file = soportes[i];
                            if (file != null)
                            {
                                if (file.ContentLength > 0)
                                {
                                    GuardarArchivosAlertaDispen(file, gestiona);
                                }
                            }
                        }
                    }

                    mensaje = "REGISTRO INGRESADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA INSERCIÓN DE INFORMACIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN DE INFORMACIÓN: " + error;
            }

            return RedirectToAction("TableroControlAlertasMedicamentos", "MedicamentosCalidad", new { rta = rta, msg = mensaje });
        }
        public List<alertas_dispensacion_detalle_entramite> ArmadoTramite(string ArrayTipoTramite, int? idDetalle)
        {
            List<alertas_dispensacion_detalle_entramite> listado = new List<alertas_dispensacion_detalle_entramite>();
            try
            {
                var CamposCompletos = ArrayTipoTramite.Split('|');
                if (CamposCompletos.Count() > 0)
                {
                    foreach (var item in CamposCompletos)
                    {
                        alertas_dispensacion_detalle_entramite dato = new alertas_dispensacion_detalle_entramite();

                        var camposParciales = item.Split('_');
                        if (camposParciales.Count() > 0)
                        {
                            var tipo = camposParciales[0];
                            var fecha = camposParciales[1];

                            dato.id_detalle = idDetalle;
                            dato.id_tipo = !string.IsNullOrEmpty(tipo) ? Convert.ToInt32(tipo) : 0;
                            dato.fecha_tipo = !string.IsNullOrEmpty(fecha) ? Convert.ToDateTime(fecha) : (DateTime?)null;
                            dato.fecha_digita = DateTime.Now;
                            dato.usuario_digita = SesionVar.UserName;
                        }

                        if (dato != null)
                        {
                            listado.Add(dato);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<alertas_dispensacion_detalle_entramite_respuestas> armadoArrayTramiteRespuestas(string ArrayTipoTramiteRespuestas, int? idDetalle)
        {
            List<alertas_dispensacion_detalle_entramite_respuestas> listado = new List<alertas_dispensacion_detalle_entramite_respuestas>();
            try
            {
                var camposCompletos = ArrayTipoTramiteRespuestas.Split('|');
                if (camposCompletos.Count() > 0)
                {
                    foreach (var item in camposCompletos)
                    {
                        alertas_dispensacion_detalle_entramite_respuestas dato = new alertas_dispensacion_detalle_entramite_respuestas();

                        var camposParciales = item.Split('_');
                        if (camposParciales.Count() > 0)
                        {
                            var id = camposParciales[0];
                            var tieneSoporte = camposParciales[1];
                            var fecha = camposParciales[2];
                            var observacion = camposParciales[3];

                            dato.id_detalle = idDetalle;
                            dato.tiene_soporte = tieneSoporte;
                            dato.id_gestionTramite = !string.IsNullOrEmpty(id) ? Convert.ToInt32(id) : 0;

                            if (tieneSoporte == "SI")
                            {
                                dato.fecha_soporte = !string.IsNullOrEmpty(fecha) ? Convert.ToDateTime(fecha) : (DateTime?)null;
                            }
                            else
                            {
                                dato.observacion_soporte = observacion;
                            }

                            dato.fecha_digita = DateTime.Now;
                            dato.usario_digita = SesionVar.UserName;
                        }

                        if (dato != null)
                        {
                            listado.Add(dato);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        private int GuardarArchivosAlertaDispen(HttpPostedFileBase file, int idDetalle)
        {
            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            var respuesta = 0;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaAlertasDispensacionArchivos"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivosAlertasDispen";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivosAlertasDispenPruebas";
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + idDetalle);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                alertas_dispensacion_detalle_archivos OBJ = new alertas_dispensacion_detalle_archivos();
                OBJ.id_detalle = idDetalle;
                OBJ.ruta = Convert.ToString(archivo);
                OBJ.extension = file.ContentType;
                OBJ.nombre = file.FileName;
                OBJ.fecha_digita = DateTime.Now;
                OBJ.usuario_digita = SesionVar.UserName;

                respuesta = BusClass.InsertarArchivoAlertasDispen(OBJ);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        public ActionResult TableroAlertasGestionadas()
        {
            List<management_alertasDispensacion_tableroControl_gestionadasResult> listado = new List<management_alertasDispensacion_tableroControl_gestionadasResult>();
            List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult> listadoDetalle = new List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult>();
            var conteo = 0;
            try
            {
                listado = BusClass.ListadoAlertasDispensacionGestionadas();
                listadoDetalle = BusClass.ListadoAlertasDispensacionGestionadasDetallada();
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.conteo = conteo;
            ViewBag.rol = SesionVar.ROL;

            Session["ListadoAlertasCon"] = listadoDetalle;

            return View();
        }

        public PartialViewResult MostrarArchivosAlertas(int? idDetalle)
        {
            List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult> listaArchivos = new List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult>();
            List<management_alertasDispensacion_tableroControl_respuestasResult> listaGestiones = new List<management_alertasDispensacion_tableroControl_respuestasResult>();

            var conteo = 0;
            var conteoGestiones = 0;
            try
            {
                listaArchivos = BusClass.ListadoAlertasDispensacionGestionadasArchivos(idDetalle);
                conteo = listaArchivos.Count();

                listaGestiones = BusClass.ListadoAlertasDispensacionGestiones(idDetalle);
                conteoGestiones = listaGestiones.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listaArchivos = listaArchivos;
            ViewBag.conteoArchivos = conteo;


            ViewBag.listaGestiones = listaGestiones;
            ViewBag.conteoGestiones = conteoGestiones;

            ViewBag.idDetalle = idDetalle;
            return PartialView();
        }

        public ActionResult VerArchivoAlertaGestion(Int32 idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                alertas_dispensacion_detalle_archivos dato = new alertas_dispensacion_detalle_archivos();
                dato = BusClass.TraerArchivoAlertasDispen(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extensionTipo = obj.extension;

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];

                    return File(dirpath, extensionTipo, nombreFinal);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public void ExcelExportarAlertasSinConfirmar()
        {
            List<management_alertasDispensacion_tableroControlResult> listareporte = new List<management_alertasDispensacion_tableroControlResult>();

            try
            {
                listareporte = (List<management_alertasDispensacion_tableroControlResult>)Session["ListadoAlertasSin"];

                if (listareporte.Count() == 0)

                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('SIN DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

                Sheet.Cells["A1:BF1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AT1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AT1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AT1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AT1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "ID ALERTA";
                Sheet.Cells["B1"].Value = "ID REGISTRO";
                Sheet.Cells["C1"].Value = "DEPENDENCIA DE SALUD";
                Sheet.Cells["D1"].Value = "FECHA DE PRESCRIPCIÓN FÓRMULA";
                Sheet.Cells["E1"].Value = "NÚMERO DE FÓRMULA";
                Sheet.Cells["F1"].Value = "TIPO DE FORMULA";
                Sheet.Cells["G1"].Value = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["H1"].Value = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["I1"].Value = "NOMBRE DEL BENEFICIARIO";
                Sheet.Cells["J1"].Value = "CIUDAD DE DESPACHO";
                Sheet.Cells["K1"].Value = "UNIS";
                Sheet.Cells["L1"].Value = "ID IPS";
                Sheet.Cells["M1"].Value = "NOMPRE IPS DE ATENCIÓN";
                Sheet.Cells["N1"].Value = "ID PRESCRIPTOR";
                Sheet.Cells["O1"].Value = "PRESCRIPTOR";
                Sheet.Cells["P1"].Value = "ESPECIALIDAD";
                Sheet.Cells["Q1"].Value = "CODIFICACIÓN EAN 13";
                Sheet.Cells["R1"].Value = "CÓDIGO HIJO";
                Sheet.Cells["S1"].Value = "CÓDIGO INTERNO DE MEDICAMENTO";
                Sheet.Cells["T1"].Value = "CÓDIGO COMERCIAL";
                Sheet.Cells["U1"].Value = "CUM";
                Sheet.Cells["V1"].Value = "REGISTRO SANITARIO";
                Sheet.Cells["W1"].Value = "CLASIFICACIÓN INVIMA";
                Sheet.Cells["X1"].Value = "CÓDIGO ATC";
                Sheet.Cells["Y1"].Value = "GRUPO FARMACOLOGICO";
                Sheet.Cells["Z1"].Value = "NOMBRE DEL MEDICAMENTO EN D C I";
                Sheet.Cells["AA1"].Value = "FORMA FARMACÉUTICA";
                Sheet.Cells["AB1"].Value = "CONCENTRACIÓN";
                Sheet.Cells["AC1"].Value = "PRESENTACIÓN";
                Sheet.Cells["AD1"].Value = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                Sheet.Cells["AE1"].Value = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                Sheet.Cells["AF1"].Value = "COMERCIAL O GENÉRICO";
                Sheet.Cells["AG1"].Value = "LABORATORIO FABRICANTE";
                Sheet.Cells["AH1"].Value = "LABORATORIO COMERCIALIZADOR O DISTRIBUIDOR";
                Sheet.Cells["AI1"].Value = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                Sheet.Cells["AJ1"].Value = "DOCUMENTO FECHA DISPENSACION";
                Sheet.Cells["AK1"].Value = "NÚMERO DE UNIDADES PRESCRITAS";
                Sheet.Cells["AL1"].Value = "NÚMERO DE UNIDADES ENTREGADAS";
                Sheet.Cells["AM1"].Value = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                Sheet.Cells["AN1"].Value = "IVA";
                Sheet.Cells["AO1"].Value = "VALOR";
                Sheet.Cells["AP1"].Value = "CONSECUTIVO TIQUETE";
                Sheet.Cells["AQ1"].Value = "PRESTADOR";
                Sheet.Cells["AR1"].Value = "FECHA ALERTA";
                Sheet.Cells["AS1"].Value = "CONSECUTIVO";
                Sheet.Cells["AT1"].Value = "NUMERO DE ALERTA"; // Puedes seguir con más letras según sea necesario

                int row = 2;
                foreach (management_alertasDispensacion_tableroControlResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_alerta;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_registro;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.dependencia_salid;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_prescripcion_formula;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.numero_formula;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.tipo_formula;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.tipo_identificacion_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.numero_documento_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_beneficiario;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.ciudad_despacho;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.id_ips;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nombre_ips_atencion;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.id_prescriptor;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.prescriptor;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.especialidad;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.codificacion_ean13;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.codigo_hijo;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.codigo_interno_medicamento;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.codigo_comercial;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.cum;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.registro_sanitario;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.clasificacion_invima;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.codigo_atc;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.grupo_farmacologico;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.nombre_medicamento_DCI;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.formula_farmaceutica;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.concentracion;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.presentacion;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.descripcion_completa_producto;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.nombre_comercial_medicamento;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.comercial_o_generico;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.laboratorio_fabricante;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = item.laboratorio_comercializador_distribuidor;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.laboratorio_titulo_registro_sanitario;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.documento_fecha_dispensacion;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = item.numero_unidades_prescritas;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = item.numero_unidades_entregadas;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = item.valor_unitario_unidad_entregada;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = item.iva;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = item.valor;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = item.concutivo_tiquete;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = item.prestador;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = item.fecha_alerta;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = item.consecutivo;
                    Sheet.Cells[string.Format("AT{0}", row)].Value = item.numero_alerta; // Agregar nuevas letras según sea necesario

                    Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AR{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A" + row + ":AT" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:AT"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosDispensacionPrestadores.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR EN LA DESCARGA DEL REPORTE.');" +
                  "</script> ";
                Response.Write(rta);
            }
        }

        public void ExcelExportarAlertasConfirmadas()
        {
            List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult> listareporte = new List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult>();

            try
            {
                listareporte = (List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult>)Session["ListadoAlertasCon"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                      "window.alert('SIN DATOS POR MOSTRAR.');" +
                      "</script> ";
                    Response.Write(rta);
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

                Sheet.Cells["A1:BK1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:BK1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:BK1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:BK1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:BK1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "ID ALERTA";
                Sheet.Cells["B1"].Value = "ID REGISTRO";
                Sheet.Cells["C1"].Value = "DEPENDENCIA DE SALUD";
                Sheet.Cells["D1"].Value = "FECHA DE PRESCRIPCIÓN FÓRMULA";
                Sheet.Cells["E1"].Value = "NÚMERO DE FÓRMULA";
                Sheet.Cells["F1"].Value = "TIPO DE FORMULA";
                Sheet.Cells["G1"].Value = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["H1"].Value = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                Sheet.Cells["I1"].Value = "NOMBRE DEL BENEFICIARIO";
                Sheet.Cells["J1"].Value = "CIUDAD DE DESPACHO";
                Sheet.Cells["K1"].Value = "UNIS";
                Sheet.Cells["L1"].Value = "ID IPS";
                Sheet.Cells["M1"].Value = "NOMPRE IPS DE ATENCIÓN";
                Sheet.Cells["N1"].Value = "ID PRESCRIPTOR";
                Sheet.Cells["O1"].Value = "PRESCRIPTOR";
                Sheet.Cells["P1"].Value = "ESPECIALIDAD";
                Sheet.Cells["Q1"].Value = "CODIFICACIÓN EAN 13";
                Sheet.Cells["R1"].Value = "CÓDIGO HIJO";
                Sheet.Cells["S1"].Value = "CÓDIGO INTERNO DE MEDICAMENTO";
                Sheet.Cells["T1"].Value = "CÓDIGO COMERCIAL";
                Sheet.Cells["U1"].Value = "CUM";
                Sheet.Cells["V1"].Value = "REGISTRO SANITARIO";
                Sheet.Cells["W1"].Value = "CLASIFICACIÓN INVIMA";
                Sheet.Cells["X1"].Value = "CÓDIGO ATC";
                Sheet.Cells["Y1"].Value = "GRUPO FARMACOLOGICO";
                Sheet.Cells["Z1"].Value = "NOMBRE DEL MEDICAMENTO EN D C I";
                Sheet.Cells["AA1"].Value = "FORMA FARMACÉUTICA";
                Sheet.Cells["AB1"].Value = "CONCENTRACIÓN";
                Sheet.Cells["AC1"].Value = "PRESENTACIÓN";
                Sheet.Cells["AD1"].Value = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                Sheet.Cells["AE1"].Value = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                Sheet.Cells["AF1"].Value = "COMERCIAL O GENÉRICO";
                Sheet.Cells["AG1"].Value = "LABORATORIO FABRICANTE";
                Sheet.Cells["AH1"].Value = "LABORATORIO COMERCIALIZADOR O DISTRIBUIDOR";
                Sheet.Cells["AI1"].Value = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                Sheet.Cells["AJ1"].Value = "DOCUMENTO FECHA DISPENSACION";
                Sheet.Cells["AK1"].Value = "NÚMERO DE UNIDADES PRESCRITAS";
                Sheet.Cells["AL1"].Value = "NÚMERO DE UNIDADES ENTREGADAS";
                Sheet.Cells["AM1"].Value = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                Sheet.Cells["AN1"].Value = "IVA";
                Sheet.Cells["AO1"].Value = "VALOR";
                Sheet.Cells["AP1"].Value = "CONSECUTIVO TIQUETE";
                Sheet.Cells["AQ1"].Value = "PRESTADOR";
                Sheet.Cells["AR1"].Value = "FECHA ALERTA";
                Sheet.Cells["AS1"].Value = "CONSECUTIVO";
                Sheet.Cells["AT1"].Value = "NUMERO DE ALERTA";

                Sheet.Cells["AU1"].Value = "ID DETALLE";
                Sheet.Cells["AV1"].Value = "FACTURA";
                Sheet.Cells["AW1"].Value = "NUMERO DE FACTURA";
                Sheet.Cells["AX1"].Value = "VALOR FACTURADO";
                Sheet.Cells["AY1"].Value = "DIAGNOSTICO ASOCIADO MEDICAMENTO HC";
                Sheet.Cells["AZ1"].Value = "ES MEDICAMENTO PERTINENTE";
                Sheet.Cells["BA1"].Value = "CANTIDAD DISPENSADA CORRESPONDE PRESCRITA";
                Sheet.Cells["BB1"].Value = "CANTIDADES PERTINENTES";
                Sheet.Cells["BC1"].Value = "ES DESVIACIÓN";
                Sheet.Cells["BD1"].Value = "CAUSA DESVIACIÓN";
                Sheet.Cells["BE1"].Value = "RESPONSABLE DESVIACIÓN";
                Sheet.Cells["BF1"].Value = "PLAN ACCIÓN";
                Sheet.Cells["BG1"].Value = "OBSERVACIÓN";
                Sheet.Cells["BH1"].Value = "CONFIRMACIÓN ALERTA DISPENSACIÓN";
                Sheet.Cells["BI1"].Value = "TIPO DATO";
                Sheet.Cells["BJ1"].Value = "FECHA GESTIÓN";
                Sheet.Cells["BK1"].Value = "USUARIO GESTIÓN";


                int row = 2;
                foreach (management_alertasDispensacion_tableroControl_gestionadasDetalladaResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_alerta;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_registro;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.dependencia_salid;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_prescripcion_formula;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.numero_formula;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.tipo_formula;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.tipo_identificacion_beneficiario;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.numero_documento_beneficiario;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_beneficiario;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.ciudad_despacho;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.id_ips;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nombre_ips_atencion;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.id_prescriptor;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.prescriptor;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.especialidad;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.codificacion_ean13;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.codigo_hijo;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.codigo_interno_medicamento;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.codigo_comercial;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.cum;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.registro_sanitario;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.clasificacion_invima;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.codigo_atc;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.grupo_farmacologico;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.nombre_medicamento_DCI;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.formula_farmaceutica;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.concentracion;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.presentacion;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.descripcion_completa_producto;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.nombre_comercial_medicamento;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.comercial_o_generico;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.laboratorio_fabricante;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = item.laboratorio_comercializador_distribuidor;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.laboratorio_titulo_registro_sanitario;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.documento_fecha_dispensacion;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = item.numero_unidades_prescritas;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = item.numero_unidades_entregadas;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = item.valor_unitario_unidad_entregada;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = item.iva;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = item.valor;
                    Sheet.Cells[string.Format("AP{0}", row)].Value = item.concutivo_tiquete;
                    Sheet.Cells[string.Format("AQ{0}", row)].Value = item.prestador;
                    Sheet.Cells[string.Format("AR{0}", row)].Value = item.fecha_alerta;
                    Sheet.Cells[string.Format("AS{0}", row)].Value = item.consecutivo;
                    Sheet.Cells[string.Format("AT{0}", row)].Value = item.numero_alerta;

                    Sheet.Cells[string.Format("AU{0}", row)].Value = item.id_detalle;
                    Sheet.Cells[string.Format("AV{0}", row)].Value = item.factura;
                    Sheet.Cells[string.Format("AW{0}", row)].Value = item.numero_factura;
                    Sheet.Cells[string.Format("AX{0}", row)].Value = item.valor_facturado;
                    Sheet.Cells[string.Format("AY{0}", row)].Value = item.diagnostico_asociaco_medicamento_hc;
                    Sheet.Cells[string.Format("AZ{0}", row)].Value = item.es_medicamento_pertinente;
                    Sheet.Cells[string.Format("BA{0}", row)].Value = item.cantidad_dispensada_corresponde_prescrita;
                    Sheet.Cells[string.Format("BB{0}", row)].Value = item.cantidades_pertinentes;
                    Sheet.Cells[string.Format("BC{0}", row)].Value = item.es_desviacion;
                    Sheet.Cells[string.Format("BD{0}", row)].Value = item.causa_desviacion;
                    Sheet.Cells[string.Format("BE{0}", row)].Value = item.responsable_desviacion;
                    Sheet.Cells[string.Format("BF{0}", row)].Value = item.plan_accion;
                    Sheet.Cells[string.Format("BG{0}", row)].Value = item.observacion;
                    Sheet.Cells[string.Format("BH{0}", row)].Value = item.confirmacion_alerta_dispensacion;
                    Sheet.Cells[string.Format("BI{0}", row)].Value = item.tipoDato;
                    Sheet.Cells[string.Format("BJ{0}", row)].Value = item.fecha_digita1;
                    Sheet.Cells[string.Format("BK{0}", row)].Value = item.nombreDigita1;


                    Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AR{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A" + row + ":BK" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:BK"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptMedicamentosDispensacionPrestadores.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                  "window.alert('ERROR EN LA DESCARGA DEL REPORTE.');" +
                  "</script> ";
                Response.Write(rta);
            }
        }
    }
}

