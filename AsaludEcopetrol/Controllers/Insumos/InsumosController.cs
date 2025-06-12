using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ECOPETROL_COMMON.ENTIDADES;
using LinqToExcel;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using System.Net.Http.Headers;

namespace AsaludEcopetrol.Controllers.Insumos
{
    [SessionExpireFilter]
    public class InsumosController : Controller
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

        #region Quejas Validas

        public ActionResult CargueQuejasValidas()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueQuejasValidas(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();
            if (Model.ValidarExistenciaQuejasValidas(mes, año))
            {
                string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(ruta, fileName);

                try
                {
                    file.SaveAs(path);

                    calidad_quejas_validas obj = new calidad_quejas_validas();
                    obj.mes = mes;
                    obj.año = año;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    List<calidad_quejas_validas_dtll> list = EntidadQuejasValidas(path, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        Model.InsertarDetallesQuejasValidas(list, obj, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["rta"] = 1;
                            ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                            System.IO.File.Delete(path);
                        }
                        else
                        {
                            ViewData["rta"] = 2;
                            ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception ex)
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                    System.IO.File.Delete(path);
                }
            }
            else
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Lo sentimos, ya ha sido cargada una base con el mismo mes y año ";
            }

            return View();
        }

        private List<calidad_quejas_validas_dtll> EntidadQuejasValidas(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_quejas_validas_dtll> result = new List<calidad_quejas_validas_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "H999999", "Quejas Validas") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_quejas_validas_dtll obj = new calidad_quejas_validas_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Año";
                    obj.año = Convert.ToInt32(item[0]);
                    columna = "Periodo";
                    obj.periodo = Convert.ToDateTime(item[1]);
                    columna = "Mes";
                    obj.mes = Convert.ToString(item[2]).ToUpper();
                    columna = "Prestador";
                    obj.prestador = Convert.ToString(item[3]).ToUpper();
                    columna = "Regional";
                    obj.regional = Convert.ToString(item[4]).ToUpper();
                    columna = "UNIS";
                    obj.unis = Convert.ToString(item[5]).ToUpper();
                    columna = "Localidad";
                    obj.localidad = Convert.ToString(item[6]).ToUpper();
                    columna = "Total";
                    obj.total = Convert.ToInt32(item[7]);

                    result.Add(obj);
                    obj = new calidad_quejas_validas_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroControlQuejasValidas(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Prestador, string regional, string unis, string ciudad)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            List<vw_calidad_quejas_validas> List = new List<vw_calidad_quejas_validas>();

            var conteo = 0;
            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Prestador) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(unis) ||
                !string.IsNullOrEmpty(ciudad))
            {
                List = Model.GetListCalidadQuejasValidas();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                List = List.Where(l => l.prestador.ToUpper().Contains(Prestador.ToUpper())).ToList();
            }


            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.regional.ToUpper().Contains(regional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(unis))
            {
                List = List.Where(l => l.unis.ToUpper().Contains(unis.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                List = List.Where(l => l.localidad.ToUpper().Contains(ciudad.ToUpper())).ToList();
            }

            ViewBag.reg = regional;

            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;
            ViewBag.cantidadresultados = 0;

            if (conteo == 1)
            {
                if (List.Count() > 0)
                {
                    ViewBag.cantidadresultados = List.Count;
                    ViewBag.countresultados = List.Select(l => l.total).Sum();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }
            Session["ListQuejasValidas"] = List;
            return View(List);
        }

        public ActionResult CargueBaseQuejasValidas(int? rta)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewData["rta"] = 0;
            if (rta != null)
            {
                ViewData["rta"] = rta;
            }
            ViewData["Msg"] = "";
            ViewBag.rol = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;
            return View(Model.GetListBasesCargadasQuejasValidas());
        }

        [HttpPost]
        public ActionResult CargueBaseQuejasValidas(HttpPostedFileBase file, DateTime fechacargue, string observaciones)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            try
            {
                Guid al = Guid.NewGuid();
                string guid = al.ToString().Substring(0, 8);
                string ext = Path.GetExtension(file.FileName);
                string filename = guid + ext;
                string path = Model.ObtenerPath(filename);
                file.SaveAs(path);

                calidad_quejas_validas_base_zip obj = new calidad_quejas_validas_base_zip();
                obj.fecha = fechacargue;
                obj.observaciones = observaciones;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.ruta = path;
                obj.content_type = file.ContentType;
                obj.ext = ext;
                Model.InsertarArchivoQuejasValidas(obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewData["rta"] = 1;
                    ViewData["Msg"] = "El documento se ha guardado correctamente";
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = "Ah ocurrido un error al guardar el documento: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Ha ocurrido un error al guardar el documento: " + ex.Message;
            }

            return View(Model.GetListBasesCargadasQuejasValidas());
        }

        public void DescargarBaseQuejasValidas(int id)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            calidad_quejas_validas_base_zip documento = Model.GetArchivoById(id);

            if (!string.IsNullOrEmpty(documento.ruta))
            {
                byte[] bytes;
                using (FileStream file = new FileStream(documento.ruta, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                }
                byte[] array = bytes.ToArray();

                if (array != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.ContentType = documento.content_type;
                    Response.AddHeader("content-length", array.Length.ToString());
                    Response.BinaryWrite(array);
                    Response.Flush();
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('No contiene documento');</script>");
            }
        }

        public ActionResult EliminarArchivoQuejas(int id)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            calidad_quejas_validas_base_zip documento = Model.GetArchivoById(id);
            if (documento != null)
            {
                Model.EliminarDocumentoRutaFisica(documento.ruta, ref MsgRes);
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.EliminarArchivoZipQuejasValidas(documento);
                }
            }

            return RedirectToAction("CargueBaseQuejasValidas", "Insumos", new { rta = 3 });
        }

        public JsonResult Buscar_Prestador_Quejas_Validas()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_quejas_validas_prestadores> Prestadores = BusClass.GetPrestadoresQuejasValidas(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.prestador,
                                     label = reg.prestador,
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

        public void DescargarResultadosQuejasValidas()
        {
            List<vw_calidad_quejas_validas> List = (List<vw_calidad_quejas_validas>)Session["ListQuejasValidas"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Quejas Validas");

                Sheet.Cells["A1"].Value = "Prestador";
                Sheet.Cells["B1"].Value = "Periodo";
                Sheet.Cells["C1"].Value = "Mes";
                Sheet.Cells["D1"].Value = "Año";
                Sheet.Cells["E1"].Value = "Regional";
                Sheet.Cells["F1"].Value = "Unis";
                Sheet.Cells["G1"].Value = "Localidad";
                Sheet.Cells["H1"].Value = "Total";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.periodo;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.regional;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.unis;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.localidad;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.total;
                    row++;
                }

                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoQuejasValidas" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        #endregion

        #region Oportunidad Rips
        public ActionResult CargueOportunidadRips()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueOportunidadRips(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();

            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos2"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                calidad_oportunidad_rips obj = new calidad_oportunidad_rips();
                obj.mes = mes;
                obj.año = año;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<calidad_oportunidad_rips_dtll> result = EntidadOportunidadRips(path, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarDetallesOportunidadRIPS(result, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }

            return View();
        }

        private List<calidad_oportunidad_rips_dtll> EntidadOportunidadRips(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_oportunidad_rips_dtll> result = new List<calidad_oportunidad_rips_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "I999999", "Oportunidad Rips") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_oportunidad_rips_dtll obj = new calidad_oportunidad_rips_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Periodo";
                    obj.periodo = Convert.ToDateTime(item[0]);

                    columna = "Año";
                    obj.año = Convert.ToInt32(item[1]);

                    columna = "Mes";
                    obj.mes = Convert.ToString(item[2]).ToUpper();

                    columna = "Regional";
                    obj.regional = Convert.ToString(item[3]).ToUpper();

                    columna = "Código del Prestador";
                    obj.codigo_prestador = Convert.ToString(item[4]).ToUpper();

                    columna = "Nombre del prestador";
                    obj.nom_prestador = Convert.ToString(item[5]).ToUpper();

                    columna = "Total de registros con oportunidad";
                    obj.total_registros_con_oportunidad = Convert.ToInt32(item[6]);

                    columna = "Total de Registros Evaluados";
                    obj.total_registros_evaluados = Convert.ToInt32(item[7]);

                    columna = "Indicador";
                    obj.indicador = Convert.ToDecimal(item[8]);

                    result.Add(obj);
                    obj = new calidad_oportunidad_rips_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroControlOportunidadRips(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Prestador, string regional)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            List<vw_calidad_oportunidad_rips_indicador> List = new List<vw_calidad_oportunidad_rips_indicador>();

            var conteo = 0;
            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Prestador) || !string.IsNullOrEmpty(regional))
            {
                List = Model.GetListCalidadOportunidadRips();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                List = List.Where(l => l.nom_prestador.ToUpper().Contains(Prestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.regional.ToUpper().Contains(regional)).ToList();
            }

            ViewBag.reg = regional;

            ViewBag.registrocontportunidad = 0;
            ViewBag.registrosevaluados = 0;
            ViewBag.oportunidad = 0;
            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;
            if (conteo == 1)
            {
                if (List.Count() > 0)
                {
                    double? a = List.Select(l => l.total_registros_con_oportunidad).Sum();
                    double? b = List.Select(l => l.total_registros_evaluados).Sum();
                    double? c = ((a / b) * 100);

                    ViewBag.registrocontportunidad = a;
                    ViewBag.registrosevaluados = b;
                    ViewBag.oportunidad = Math.Round(c.Value, 2);
                    ViewBag.countresultados = List.Count();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }

            Session["ListOportunidadRips"] = List;
            return View(List);
        }

        public JsonResult Buscar_Prestador_Oportunidad_Rips()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> Prestadores = BusClass.GetPrestadoresOportunidadRips(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.nom_prestador,
                                     label = reg.nom_prestador,
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

        public JsonResult Buscar_codPrestador_Oportunidad_Rips()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> Prestadores = BusClass.GetCodPrestadoresOportunidadRips(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.nom_prestador,
                                     codigo = reg.codigo_prestador,
                                     label = reg.codigo_prestador,
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

        public void DescargarResultadosOportunidadRips()
        {
            List<vw_calidad_oportunidad_rips_indicador> List = (List<vw_calidad_oportunidad_rips_indicador>)Session["ListOportunidadRips"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Oportunidad Rips");

                Sheet.Cells["A1"].Value = "Prestador";
                Sheet.Cells["B1"].Value = "Regional";
                Sheet.Cells["C1"].Value = "Periodo";
                Sheet.Cells["D1"].Value = "Mes";
                Sheet.Cells["E1"].Value = "Año";
                Sheet.Cells["F1"].Value = "Total registros con oportunidad";
                Sheet.Cells["G1"].Value = "Total registros evaluados";
                Sheet.Cells["H1"].Value = "Indicador";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.nom_prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.regional;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.periodo;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.total_registros_con_oportunidad;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.total_registros_evaluados;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.indicador_oportunidad;
                    row++;
                }

                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoOportunidadRips" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        #endregion

        #region Calidad Rips
        public ActionResult CargueCalidadRips()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult CargueCalidadRips(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();
            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos2"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                calidad_de_rips obj = new calidad_de_rips();
                obj.mes = mes;
                obj.año = año;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<calidad_de_rips_dtll> result = EntidadCalidadRips(path, ref MsgRes);
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarDetallesCalidadRIPS(result, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }


            return View();
        }

        private List<calidad_de_rips_dtll> EntidadCalidadRips(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_de_rips_dtll> result = new List<calidad_de_rips_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "I999999", "Calidad Rips") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_de_rips_dtll obj = new calidad_de_rips_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Periodo";
                    obj.periodo = Convert.ToDateTime(item[0]);

                    columna = "Año";
                    obj.año = Convert.ToInt32(item[1]);

                    columna = "Mes";
                    obj.mes = Convert.ToString(item[2]).ToUpper();

                    columna = "Regional";
                    obj.regional = Convert.ToString(item[3]).ToUpper();

                    columna = "Código del Prestador";
                    obj.codigo_prestador = Convert.ToString(item[4]).ToUpper();

                    columna = "Nombre del prestador";
                    obj.nom_prestador = Convert.ToString(item[5]).ToUpper();

                    columna = "Cantidad de registros únicos sin errores";
                    obj.cantidad_registros_sin_errores = Convert.ToInt32(item[6]);

                    columna = "Total de Registros Evaluados";
                    obj.total_registros_evaluados = Convert.ToInt32(item[7]);

                    columna = "Indicador";
                    obj.indicador = Convert.ToDecimal(item[8]);

                    result.Add(obj);
                    obj = new calidad_de_rips_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroControlCalidadRips(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Prestador, string regional)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            List<vw_calidad_de_rips_indicador> List = new List<vw_calidad_de_rips_indicador>();

            var conteo = 0;

            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Prestador) || !string.IsNullOrEmpty(regional))
            {
                List = Model.GetListCalidadCalidadRips();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                List = List.Where(l => l.nom_prestador.ToUpper().Contains(Prestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.regional.ToUpper().Contains(regional)).ToList();
            }

            ViewBag.reg = regional;

            ViewBag.registrossinerrores = 0;
            ViewBag.registrosevaluados = 0;
            ViewBag.oportunidad = 0;
            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;


            if (conteo == 1)
            {
                if (List.Count() > 0)
                {

                    double? a = List.Select(l => l.cantidad_registros_sin_errores).Sum();
                    double? b = List.Select(l => l.total_registros_evaluados).Sum();
                    double? c = ((a / b) * 100);

                    ViewBag.registrossinerrores = a;
                    ViewBag.registrosevaluados = b;
                    ViewBag.oportunidad = Math.Round(c.Value, 2);
                    ViewBag.countresultados = List.Count();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }
            Session["ListCalidadRips"] = List;
            return View(List);
        }

        public void DescargarResultadosCalidadRips()
        {
            List<vw_calidad_de_rips_indicador> List = (List<vw_calidad_de_rips_indicador>)Session["ListCalidadRips"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Calidad Rips");

                Sheet.Cells["A1"].Value = "Prestador";
                Sheet.Cells["B1"].Value = "Regional";
                Sheet.Cells["C1"].Value = "Periodo";
                Sheet.Cells["D1"].Value = "Mes";
                Sheet.Cells["E1"].Value = "Año";
                Sheet.Cells["F1"].Value = "Cantidad de registros unicos sin errores";
                Sheet.Cells["G1"].Value = "Total registros evaluados";
                Sheet.Cells["H1"].Value = "Indicador";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.nom_prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.regional;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.periodo;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.cantidad_registros_sin_errores;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.total_registros_evaluados;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.indicador_calidad;
                    row++;
                }

                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A:H"].AutoFitColumns();
                Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoCalidadRips" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        #endregion

        public JsonResult Buscar_Prestador_citasmed_odontologia()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_opor_citas_y_odont_prestadores> Prestadores = BusClass.GetPrestadoresCitasmedicasyodontologia(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.Prestador,
                                     Nit = reg.Nit_prestador,
                                     label = reg.Prestador,
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

        public JsonResult Buscar_CodPrestador_citasmed_odontologia()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_opor_citas_y_odont_prestadores> Prestadores = BusClass.GetPrestadoresCitasmedicasyodontologia(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.Prestador,
                                     Nit = reg.Nit_prestador,
                                     label = reg.Nit_prestador,
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

        public JsonResult Buscar_Profesional_citasmed_odontologia()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_opor_citas_y_odon_profesionales> Prestadores = BusClass.GetProfesionalesCitasmedicasyodontologia(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.profesional,
                                     documento = reg.documento_profesional,
                                     label = reg.profesional,
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

        public JsonResult Buscar_CodProfesional_citasmed_odontologia()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_opor_citas_y_odon_profesionales> Prestadores = BusClass.GetProfesionalesCitasmedicasyodontologia(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.profesional,
                                     documento = reg.documento_profesional,
                                     label = reg.documento_profesional,
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


        #region Oportunidad Citas Medicas Generales

        public ActionResult CargueOportunidadCitasMedicas()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueOportunidadCitasMedicas(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();
            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                calidad_oportunidad_citas_medicina_gnral obj = new calidad_oportunidad_citas_medicina_gnral();
                obj.mes = mes;
                obj.año = año;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<calidad_oportunidad_citas_medicina_gnral_dtll> result = EntidadOportinidadCitasMedicas(path, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarOportunidadCitasMedicas(result, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }

            return View();
        }

        private List<calidad_oportunidad_citas_medicina_gnral_dtll> EntidadOportinidadCitasMedicas(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_oportunidad_citas_medicina_gnral_dtll> result = new List<calidad_oportunidad_citas_medicina_gnral_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "O999999", "Oportunidad citas medicas gnrls") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_oportunidad_citas_medicina_gnral_dtll obj = new calidad_oportunidad_citas_medicina_gnral_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Periodo";
                    obj.Periodo = Convert.ToDateTime(item[0]);

                    columna = "Año";
                    obj.año = Convert.ToInt32(item[1]);

                    columna = "REGIONAL";
                    obj.Regional = Convert.ToString(item[2]).ToUpper();

                    columna = "UNIS";
                    obj.unis = Convert.ToString(item[3]).ToUpper();

                    columna = "CIUDAD";
                    obj.ciudad = Convert.ToString(item[4]).ToUpper();

                    columna = "NIT PRESTADOR";
                    obj.nit_prestador = Convert.ToString(item[5]).ToUpper();

                    columna = "PRESTADOR";
                    obj.prestador = Convert.ToString(item[6]).ToUpper();

                    columna = "DOCUMENTO PROFESIONAL";
                    obj.documento_profesional = Convert.ToString(item[7]).ToUpper();

                    columna = "PROFESIONAL";
                    obj.profesional = Convert.ToString(item[8]).ToUpper();

                    columna = "ESPECIALIDAD";
                    obj.especialidad = Convert.ToString(item[9]).ToUpper();

                    columna = "Mes";
                    obj.mes = Convert.ToString(item[10]).ToUpper();

                    columna = "FECHA CITA";
                    obj.fecha_cita = Convert.ToDateTime(item[11]);

                    columna = "Citas cumplidas";
                    obj.citas_cumplidas = Convert.ToInt32(item[12]);

                    columna = " Citas asignadas";
                    obj.citas_asignadas = Convert.ToInt32(item[13]);

                    columna = "Indicador";
                    obj.indicador = Convert.ToDecimal(item[14]);

                    result.Add(obj);
                    obj = new calidad_oportunidad_citas_medicina_gnral_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroOportunidadCitasMedicas(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Codprestador, string Prestador, string Docprofesional, string Profesional, string regional, string unis, string ciudad)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            List<vw_calidad_oportunidad_citas_medicina_gnral_indicador> List = new List<vw_calidad_oportunidad_citas_medicina_gnral_indicador>();
            var conteo = 0;

            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Codprestador) || !string.IsNullOrEmpty(Prestador) || !string.IsNullOrEmpty(Docprofesional) || !string.IsNullOrEmpty(Profesional) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(unis) ||
                !string.IsNullOrEmpty(ciudad))
            {
                List = Model.GetListCalidadOporCitasMedicas();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.Periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.Periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Codprestador))
            {
                List = List.Where(l => l.Nit_prestador.ToUpper().Contains(Codprestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                List = List.Where(l => l.Prestador.ToUpper().Contains(Prestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Docprofesional))
            {
                List = List.Where(l => l.doc_profesional.ToUpper().Contains(Docprofesional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Profesional))
            {
                List = List.Where(l => l.profesional.ToUpper().Contains(Profesional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.Regional.ToUpper().Contains(regional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(unis))
            {
                List = List.Where(l => l.unis.ToUpper().Contains(unis.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                List = List.Where(l => l.ciudad.ToUpper().Contains(ciudad.ToUpper())).ToList();
            }

            ViewBag.reg = regional;

            ViewBag.citascumplidas = 0;
            ViewBag.citasasignadas = 0;
            ViewBag.indicador = 0;
            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;

            if (conteo == 1)
            {
                if (List.Count() > 0)
                {
                    double? a = List.Select(l => l.citas_cumplidas).Sum();
                    double? b = List.Select(l => l.citas_asignadas).Sum();
                    double? c = ((a / b) * 100);

                    ViewBag.citascumplidas = a;
                    ViewBag.citasasignadas = b;
                    ViewBag.indicador = Math.Round(c.Value, 2);
                    ViewBag.countresultados = List.Count();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }
            Session["ListCalidadOporCitasMedicas"] = List;
            return View(List);
        }

        public void DescargarResultadosOportunidadCitasMedicas()
        {
            List<vw_calidad_oportunidad_citas_medicina_gnral_indicador> List = (List<vw_calidad_oportunidad_citas_medicina_gnral_indicador>)Session["ListCalidadOporCitasMedicas"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Oportunidad citas medicas");

                Sheet.Cells["A1"].Value = "Nit prestador";
                Sheet.Cells["B1"].Value = "Prestador";
                Sheet.Cells["C1"].Value = "Documento profesional";
                Sheet.Cells["D1"].Value = "Profesional";
                Sheet.Cells["E1"].Value = "Periodo";
                Sheet.Cells["F1"].Value = "Mes";
                Sheet.Cells["G1"].Value = "Año";
                Sheet.Cells["H1"].Value = "Regional";
                Sheet.Cells["I1"].Value = "Citas cumplidas";
                Sheet.Cells["J1"].Value = "Citas asignadas";
                Sheet.Cells["K1"].Value = "Indicador";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.Nit_prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.Prestador;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.doc_profesional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.profesional;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.Periodo;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.Regional;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.citas_cumplidas;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.citas_asignadas;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.indicador_final;
                    row++;
                }

                Sheet.Cells["A:K"].AutoFitColumns();
                Sheet.Cells["A:K"].AutoFitColumns();
                Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoOportunidadCitasMedicas" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        #endregion

        #region Oportunidad odontologia general
        public ActionResult CargueOportunidadOdontologia()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueOportunidadOdontologia(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();
            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                calidad_oportunidad_odontologia_gnral obj = new calidad_oportunidad_odontologia_gnral();
                obj.mes = mes;
                obj.año = año;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<calidad_oportunidad_odontologia_gnral_dtll> result = EntidadOportinidadOdontologia(path, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarOportunidadOdontologia(result, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    System.IO.File.Delete(path);
                }

            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }


            return View();
        }

        private List<calidad_oportunidad_odontologia_gnral_dtll> EntidadOportinidadOdontologia(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_oportunidad_odontologia_gnral_dtll> result = new List<calidad_oportunidad_odontologia_gnral_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "N999999", "Oportunidad odontologia") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_oportunidad_odontologia_gnral_dtll obj = new calidad_oportunidad_odontologia_gnral_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Periodo";
                    obj.Periodo = Convert.ToDateTime(item[0]);

                    columna = "Año";
                    obj.año = Convert.ToInt32(item[1]);

                    columna = "MES";
                    obj.mes = Convert.ToString(item[2]).ToUpper();

                    columna = "REGIONAL";
                    obj.Regional = Convert.ToString(item[3]).ToUpper();

                    columna = "UNIS";
                    obj.unis = Convert.ToString(item[4]).ToUpper();

                    columna = "LOCALIDAD";
                    obj.localidad = Convert.ToString(item[5]).ToUpper();

                    columna = "NIT IPS";
                    obj.nit_ips = Convert.ToString(item[6]).ToUpper();

                    columna = "NOMBRE IPS";
                    obj.nom_ips = Convert.ToString(item[7]).ToUpper();

                    columna = "CEDULA PROFESIONAL";
                    obj.documento_profesional = Convert.ToString(item[8]).ToUpper();

                    columna = "NOMBRE PROFESIONAL";
                    obj.profesional = Convert.ToString(item[9]).ToUpper();

                    columna = "ESPECIALIDAD ODONTOLOGO SEGUN E-SALUD";
                    obj.especialidad = Convert.ToString(item[10]).ToUpper();

                    columna = "Citas cumplidas";
                    obj.citas_cumplidas = Convert.ToInt32(item[11]);

                    columna = " Citas asignadas";
                    obj.citas_asignadas = Convert.ToInt32(item[12]);

                    columna = "Indicador";
                    obj.indicador = Convert.ToDecimal(item[13]);

                    result.Add(obj);
                    obj = new calidad_oportunidad_odontologia_gnral_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroOportunidadOdontologia(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Codprestador, string Prestador, string Docprofesional, string Profesional, string regional, string unis, string ciudad)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            List<vw_calidad_oportunidad_odontologia_gnral_indicador> List = new List<vw_calidad_oportunidad_odontologia_gnral_indicador>();
            var conteo = 0;

            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Codprestador) || !string.IsNullOrEmpty(Prestador) || !string.IsNullOrEmpty(Docprofesional) || !string.IsNullOrEmpty(Profesional) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(unis) ||
                !string.IsNullOrEmpty(ciudad))
            {
                List = Model.GetListCalidadOporOdontologia();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.Periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.Periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Codprestador))
            {
                List = List.Where(l => l.Nit_prestador.ToUpper().Contains(Codprestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Prestador))
            {
                List = List.Where(l => l.Prestador.ToUpper().Contains(Prestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Docprofesional))
            {
                List = List.Where(l => l.doc_profesional.ToUpper().Contains(Docprofesional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Profesional))
            {
                List = List.Where(l => l.profesional.ToUpper().Contains(Profesional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.Regional.ToUpper().Contains(regional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(unis))
            {
                List = List.Where(l => l.unis.ToUpper().Contains(unis.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                List = List.Where(l => l.localidad.ToUpper().Contains(ciudad.ToUpper())).ToList();
            }

            ViewBag.reg = regional;
            ViewBag.citascumplidas = 0;
            ViewBag.citasasignadas = 0;
            ViewBag.indicador = 0;
            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;

            if (conteo == 1)
            {
                if (List.Count() > 0)
                {
                    double? a = List.Select(l => l.citas_cumplidas).Sum();
                    double? b = List.Select(l => l.citas_asignadas).Sum();
                    double? c = ((a / b) * 100);

                    ViewBag.citascumplidas = a;
                    ViewBag.citasasignadas = b;
                    ViewBag.indicador = Math.Round(c.Value, 2);
                    ViewBag.countresultados = List.Count();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }
            Session["ListCalidadOdontologia"] = List;
            return View(List);
        }

        public void DescargarResultadosOportunidadOdontologia()
        {
            List<vw_calidad_oportunidad_odontologia_gnral_indicador> List = (List<vw_calidad_oportunidad_odontologia_gnral_indicador>)Session["ListCalidadOdontologia"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Oportunidad Odontología");

                Sheet.Cells["A1"].Value = "Nit prestador";
                Sheet.Cells["B1"].Value = "Prestador";
                Sheet.Cells["C1"].Value = "Documento profesional";
                Sheet.Cells["D1"].Value = "Profesional";
                Sheet.Cells["E1"].Value = "Periodo";
                Sheet.Cells["F1"].Value = "Mes";
                Sheet.Cells["G1"].Value = "Año";
                Sheet.Cells["H1"].Value = "Regional";
                Sheet.Cells["I1"].Value = "Citas cumplidas";
                Sheet.Cells["J1"].Value = "Citas asignadas";
                Sheet.Cells["K1"].Value = "Indicador";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.Nit_prestador;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.Prestador;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.doc_profesional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.profesional;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.Periodo;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.año;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.Regional;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.citas_cumplidas;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.citas_asignadas;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.indicador_final;
                    row++;
                }

                Sheet.Cells["A:K"].AutoFitColumns();
                Sheet.Cells["A:K"].AutoFitColumns();
                Sheet.Cells["A1:K1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:K1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoOportunidadOdontología" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }
        #endregion

        #region citas cumplidas
        public ActionResult CargueCalidadCitasCumplidas()
        {
            ViewBag.meses = BusClass.meses();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueCalidadCitasCumplidas(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.meses = BusClass.meses();
            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                calidad_citas_cumplidas obj = new calidad_citas_cumplidas();
                obj.mes = mes;
                obj.año = año;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;

                List<calidad_citas_cumplidas_dtll> result = EntidadCitasCumplidas(path, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarCalidadCitasCumplidas(result, obj, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        if (MsgRes.CodeError.Contains("No se puede convertir un objeto DBNull en otros tipos"))
                        {
                            ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: El formato de la columna es incorrecto";
                        }
                        else
                        {
                            ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        }

                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    System.IO.File.Delete(path);
                }

            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                System.IO.File.Delete(path);
            }

            return View();
        }

        private List<calidad_citas_cumplidas_dtll> EntidadCitasCumplidas(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<calidad_citas_cumplidas_dtll> result = new List<calidad_citas_cumplidas_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "G999999", "Citas Cumplidas") select c).ToList();
            string columna = "";
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {
                    calidad_citas_cumplidas_dtll obj = new calidad_citas_cumplidas_dtll();
                    var item = EData1[i];
                    fila++;

                    columna = "Periodo";
                    obj.periodo = Convert.ToDateTime(item[0]);

                    columna = "Mes";
                    obj.mes = Convert.ToString(item[1]);

                    columna = "Regional";
                    obj.regional = Convert.ToString(item[2]).ToUpper();

                    columna = "UNIS";
                    obj.unis = Convert.ToString(item[3]).ToUpper();

                    columna = "Ciudad";
                    obj.ciudad = Convert.ToString(item[4]).ToUpper();

                    columna = "Profesional";
                    obj.profesional = Convert.ToString(item[5]).ToUpper();

                    columna = "Número de citas cumplidas";
                    obj.num_citas_cumplidas = Convert.ToInt32(item[6]);

                    result.Add(obj);
                    obj = new calidad_citas_cumplidas_dtll();
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            book.Dispose();
            return result;
        }

        public ActionResult TableroCalidadCitasCumplidas(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string Profesional, string regional, string unis, string ciudad)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.meses = BusClass.meses();
            List<vw_calidad_citas_cumplidas_indicador> List = new List<vw_calidad_citas_cumplidas_indicador>();
            var conteo = 0;


            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(Profesional) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(unis) || !string.IsNullOrEmpty(ciudad))
            {
                List = Model.GetListCalidadCitasCumplidas();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(Profesional))
            {
                List = List.Where(l => l.profesional.ToUpper().Contains(Profesional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.regional.ToUpper().Contains(regional.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(unis))
            {
                List = List.Where(l => l.unis.ToUpper().Contains(unis.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                List = List.Where(l => l.ciudad.ToUpper().Contains(ciudad.ToUpper())).ToList();
            }

            ViewBag.reg = regional;
            ViewBag.countresultados = 0;
            ViewBag.cantidadresultados = 0;
            ViewBag.promedio = 0;
            ViewBag.resultado = 0;

            if (conteo == 1)
            {
                if (List.Count() > 0)
                {
                    int a = List.Count;
                    int b = List.Select(l => l.num_citas_cumplidas).Sum().Value;
                    int c = (b / a);
                    ViewBag.cantidadresultados = a;
                    ViewBag.countresultados = b;
                    ViewBag.promedio = c;
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;
                }
            }
            Session["ListCalidadCitasCumplidas"] = List;
            return View(List);
        }

        public void DescargarResultadosCitasCumplidas()
        {
            List<vw_calidad_citas_cumplidas_indicador> List = (List<vw_calidad_citas_cumplidas_indicador>)Session["ListCalidadCitasCumplidas"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Consolidado Citas Cumplidas");

                Sheet.Cells["A1"].Value = "Periodo";
                Sheet.Cells["B1"].Value = "Profesional";
                Sheet.Cells["C1"].Value = "Regional";
                Sheet.Cells["D1"].Value = "Unis";
                Sheet.Cells["E1"].Value = "Ciudad";
                Sheet.Cells["F1"].Value = "Promedio";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.periodo;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.profesional;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.regional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.unis;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.ciudad;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.num_citas_cumplidas;
                    row++;
                }

                Sheet.Cells["A:F"].AutoFitColumns();
                Sheet.Cells["A:F"].AutoFitColumns();
                Sheet.Cells["A1:F1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoCitasCumplidas" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }
        #endregion


        public JsonResult Buscar_Profesional_citasmed_cumplidas()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_citas_cumplidas_profesionales> Profesionales = BusClass.GetProfesionalesCitasCumplidas(term, ref MsgRes);
                    var lista = (from reg in Profesionales
                                 select new
                                 {
                                     nombre = reg.profesional,
                                     label = reg.profesional,
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

        public JsonResult Buscar_prestador_eventos_adversos()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_eventos_adversos_prestadores> Prestadores = BusClass.GetprestadoresEventosAdversos(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.prestador,
                                     documento = reg.documento_prestador,
                                     label = reg.prestador,
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

        public JsonResult Buscar_docprestador_eventos_adversos()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    List<vw_calidad_eventos_adversos_prestadores> Prestadores = BusClass.GetprestadoresEventosAdversos(term, ref MsgRes);
                    var lista = (from reg in Prestadores
                                 select new
                                 {
                                     nombre = reg.prestador,
                                     documento = reg.documento_prestador,
                                     label = reg.documento_prestador,
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

        public ActionResult CargueEventos()
        {
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult CargueEventos(HttpPostedFileBase file)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
                file.SaveAs(path);

                OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder(); cb.DataSource = path;
                if (Path.GetExtension(path).ToUpper() == ".XLS")
                {
                    cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                    cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");

                }
                else if (Path.GetExtension(path).ToUpper() == ".XLSX")
                {
                    cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                    cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
                }

                DataTable dt = new DataTable("Eventos Adversos");

                using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
                {
                    //Abrimos la conexión
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [Eventos Adversos$]";
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                        conn.Dispose();
                    }

                    Model.InsertarEventosAdversos(dt, ref MsgRes);

                }

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewData["rta"] = 1;
                    ViewData["Msg"] = "Los datos se insertaron correctamente!";
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = "Ah ocurrido un error al insertar los datos: " + MsgRes.DescriptionResponse;
                }

            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Ah ocurrido un error al insertar los datos: " + ex.Message;

            }

            return View();
        }

        public ActionResult TableroControlEventos(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string regional, string nitprestador,
            string nomprestador)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.regional = BusClass.GetRefRegion();
            List<calidad_evento_adverso> List = new List<calidad_evento_adverso>();

            var conteo = 0;

            if ((FechaFinalfiltro != null && FechaFinalfiltro != null) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(nitprestador) || !string.IsNullOrEmpty(nomprestador))
            {
                List = Model.GetListCalidadEventoAdverso();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date >= FechaInicialfiltro.Value.Date).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                List = List.Where(l => l.periodo.Value.Date <= FechaFinalfiltro.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(regional))
            {
                List = List.Where(l => l.regional_evento.ToUpper() == regional.ToUpper()).ToList();
            }

            if (!string.IsNullOrEmpty(nitprestador))
            {
                List = List.Where(l => l.nit_prestador_ocurrencia_evento.ToUpper().Contains(nitprestador.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(nomprestador))
            {
                List = List.Where(l => l.nom_prestador_ocurrencia_evento.ToUpper().Contains(nomprestador.ToUpper())).ToList();
            }



            ViewBag.reg = regional;
            ViewBag.nit = nitprestador;
            ViewBag.prestador = nomprestador;

            ViewBag.countresultados = 0;
            ViewBag.registros = 0;


            if (conteo == 1)
            {
                if (List.Count() != 0)
                {
                    conteo = 1;
                    ViewBag.countresultados = conteo;
                }
                else
                {
                    conteo = 2;
                    ViewBag.countresultados = conteo;
                }
                ViewBag.registros = List.Count();
            }

            Session["ListEventos"] = List;
            return View(List);
        }

        public void DescargarResultados()
        {
            List<calidad_evento_adverso> listareporte = (List<calidad_evento_adverso>)Session["ListEventos"];

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Eventos Adversos");

            Sheet.Cells["A1:AJ2"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:AJ2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:AJ2"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:AJ2"].Style.Font.Color.SetColor(Color.White);
            Sheet.Cells["A1:AJ2"].Style.Font.Name = "Century Gothic";

            Sheet.Cells["A1"].Value = "AÑO";
            Sheet.Cells["B1"].Value = "ID MES";
            Sheet.Cells["C1"].Value = "PERIODO";
            Sheet.Cells["D1"].Value = "MES";
            Sheet.Cells["E1"].Value = "CONSECUTIVO";
            Sheet.Cells["F1"].Value = "FECHA DE REPORTE";
            Sheet.Cells["G1"].Value = "FECHA DE OCURRENCIA DEL EVENTO";
            Sheet.Cells["H1"].Value = "LOCALIDAD DE SERVICIOS DE SALUD";
            Sheet.Cells["I1"].Value = "DEPENDENCIA DE SALUD";
            Sheet.Cells["J1"].Value = "FUENTE DEL REPORTE";
            Sheet.Cells["K1"].Value = "REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE";
            Sheet.Cells["L1"].Value = "REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE";
            Sheet.Cells["M1"].Value = "LUGAR DONDE OCURRIO EL INCIDENTE O EVENTO ";
            Sheet.Cells["N1"].Value = "AMBITO DE OCURRENCIA DEL EVENTO";
            Sheet.Cells["O1"].Value = "NIT DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO";
            Sheet.Cells["P1"].Value = "NOMBRE DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO";
            Sheet.Cells["Q1"].Value = "NUMERO DE IDENTIFICACION DEL PRESTADOR (CODIGO SAP)";
            Sheet.Cells["R1"].Value = "TIPO DE IDENTIFICACION DEL BENEFICIARIO";
            Sheet.Cells["S1"].Value = "NUMERO DE IDENTIFICACION DEL BENEFICIARIO";
            Sheet.Cells["T1"].Value = "NOMBRE DEL BENEFICIARIO";
            Sheet.Cells["U1"].Value = "EDAD DEL BENEFICIARIO ";
            Sheet.Cells["V1"].Value = "DESCRIPCION DEL EVENTO";
            Sheet.Cells["W1"].Value = "CATEGORIA DEL EVENTO";
            Sheet.Cells["X1"].Value = "SUBCATEGORIA DEL EVENTO";
            Sheet.Cells["Y1"].Value = "CLASIFICACION DEL EVENTO DE LA ATENCIÓN EN SALUD";
            Sheet.Cells["Z1"].Value = "CONFIRMACION EVENTO (PREVENIBLE NO PREVENIBLE)";
            Sheet.Cells["AA1"].Value = "CONCEPTO AUDITORIA";
            Sheet.Cells["AB1"].Value = "SEVERIDAD DEL DESENLACE";
            Sheet.Cells["AC1"].Value = "PROBABILIDAD DE REPETICION";
            Sheet.Cells["AD1"].Value = "GESTION DE LA GESTORÍA INTEGRAL DE LA CALIDAD";
            Sheet.Cells["AE1"].Value = "PLAN DE MEJORA AL PRESTADOR (SI O NO)";
            Sheet.Cells["AF1"].Value = "COSTO DE NO CALIDAD";
            Sheet.Cells["AG1"].Value = "SEGUIMIENTO";
            Sheet.Cells["AH1"].Value = "REGIONAL DEL EVENTO";
            Sheet.Cells["AI1"].Value = "FECHA RADICACION DEL PLAN DE MEJORA";
            Sheet.Cells["AJ1"].Value = "FECHA PROGRAMADA PARA REVISION DE PLAN DE MEJORA";


            int row = 3;
            foreach (calidad_evento_adverso item in listareporte)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.año;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.id_mes;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.periodo;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.mes;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.consecutivo;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_reporte;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.fecha_ocurrencia_evento;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.localidad_servicios_salud;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.dependencia_de_salud;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.fuente_del_reporte;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.nom_reportante;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.id_reportante;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.lugar_ocurrencia_evento;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.ambito_ocurrencia_evento;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.nit_prestador_ocurrencia_evento;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.nom_prestador_ocurrencia_evento;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.num_id_prestador_codigo_sap;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.tipo_ident_beneficiario_ocurrencia_evento;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.num_id_beneficiario;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.nom_beneficiario;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.edad_beneficiario;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.descripcion_evento;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.categoria_evento;
                Sheet.Cells[string.Format("X{0}", row)].Value = item.subcategoria_evento;
                Sheet.Cells[string.Format("Y{0}", row)].Value = item.clasificacion_evento_atencion_en_salud;
                Sheet.Cells[string.Format("Z{0}", row)].Value = item.confirmacion_evento;
                Sheet.Cells[string.Format("AA{0}", row)].Value = item.concepto_auditoria;
                Sheet.Cells[string.Format("AB{0}", row)].Value = item.severidad_del_desenlace;
                Sheet.Cells[string.Format("AC{0}", row)].Value = item.probabilidad_de_repeticion;
                Sheet.Cells[string.Format("AD{0}", row)].Value = item.gestion_de_la_gestoria_integral_calidad;
                Sheet.Cells[string.Format("AE{0}", row)].Value = item.plan_de_mejora_al_prestador;
                Sheet.Cells[string.Format("AF{0}", row)].Value = item.costo_de_no_calidad;
                Sheet.Cells[string.Format("AG{0}", row)].Value = item.seguimiento;
                Sheet.Cells[string.Format("AH{0}", row)].Value = item.regional_evento;
                Sheet.Cells[string.Format("AI{0}", row)].Value = item.fecha_radicacion_plan_mejora;
                Sheet.Cells[string.Format("AJ{0}", row)].Value = item.fecha_programada_revision_plan_mejora;
                //Sheet.Cells["A{0}:AJ{0}"].Style.Font.Name = "Century Gothic";
                row++;
            }
            Sheet.Cells["A:AJ"].AutoFitColumns();


            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteventosAdversos.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult GestorDocumentalInsumos(int? rta)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            List<ref_calidad_insumos_tipo_documental> lista = new List<ref_calidad_insumos_tipo_documental>();
            lista = Model.GetListInsumoTipoDocumental();
            ViewBag.tipodoc = lista;

            ViewData["rta"] = 0;
            if (rta != null)
            {
                ViewData["rta"] = rta;
            }
            ViewData["Msg"] = "";
            ViewBag.idrol = SesionVar.ROL;
            return View(Model.GetListGestorDocumentalInsumos());
        }

        [HttpPost]
        public ActionResult GestorDocumentalInsumos(HttpPostedFileBase file, int tipodoc, string observaciones)
        {
            ViewBag.idrol = SesionVar.ROL;
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();
            ViewBag.tipodoc = Model.GetListInsumoTipoDocumental();
            try
            {
                Guid al = Guid.NewGuid();
                string guid = al.ToString().Substring(0, 8);
                string ext = Path.GetExtension(file.FileName);
                string filename = guid + ext;
                string path = Model.ObtenerPath(filename);
                file.SaveAs(path);

                var tipoDato = "";

                if (file.FileName.Contains(".zip"))
                {
                    tipoDato = "application/zip";
                }
                else if (file.FileName.Contains(".xls") || file.FileName.Contains(".xlsx"))
                {
                    tipoDato = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }

                else if (file.FileName.Contains(".rar"))
                {
                    tipoDato = "application/vnd.rar"; // Tipo MIME para archivos .rar
                }

                calidad_gestor_documental_insumos obj = new calidad_gestor_documental_insumos();
                obj.observaciones = observaciones;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.ruta_documento = path;
                obj.nom_denerado_doc = filename;
                obj.content_type = tipoDato;
                obj.ext = ext;
                obj.id_ref_calidad_tipo_documento_insumos = tipodoc;
                Model.InsertarDocumentoInsumo(obj, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewData["rta"] = 1;
                    ViewData["Msg"] = "El documento se ha guardado correctamente";
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = "Ah ocurrido un error al guardar el documento: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Ha ocurrido un error al guardar el documento: " + ex.Message;
            }

            return View(Model.GetListGestorDocumentalInsumos());
        }

        public ActionResult EliminarDocumentGestor(int id)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            calidad_gestor_documental_insumos documento = Model.GetDocumentoById(id);
            if (documento != null)
            {
                Model.EliminarDocumentoRutaFisica(documento.ruta_documento, ref MsgRes);
                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.EliminarDocumento(documento);
                }
            }

            return RedirectToAction("GestorDocumentalInsumos", "Insumos", new { rta = 3 });
        }

        public void VerDocumentoCargado(int id)
        {
            Models.Insumos.Insumos Model = new Models.Insumos.Insumos();

            vw_calidad_gestor_documental_insumos documento = Model.VwGetDocumentoById(id);

            var fileName = documento.descripcion;

            var tipo = documento.content_type;


            if (tipo.Equals("application/octet-stream"))
            {
                tipo = "application/x-zip-compressed";
            }

            try
            {
                if (!string.IsNullOrEmpty(documento.ruta_documento))
                {
                    byte[] bytes;
                    using (FileStream file = new FileStream(documento.ruta_documento, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                    }
                    byte[] array = bytes.ToArray();

                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.ContentType = tipo;
                        //Response.AddHeader("content-length", "attachment;filename=" + fileName + ".xlsx");
                        Response.BinaryWrite(array);
                        Response.AddHeader("content-length", array.Length.ToString());

                        Response.Flush();
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No contiene documento');</script>");
                }
            }
            catch (Exception ex)
            {
                 Response.Write("<script language=javascript>alert('Error');</script>");
            }
            
        }

        public string ObtenerUnis(string idregional)
        {
            var regional = BusClass.GetRefRegion().Where(l => l.indice == idregional).FirstOrDefault();
            string result = "<option value=''>- Seleccionar -</option>";
            List<Ref_odont_unis> Unis = BusClass.Odont_unis().Where(l => l.id_regional == regional.id_ref_regional).ToList();
            foreach (var item in Unis)
            {
                result += "<option value='" + item.descripcion + "'>" + item.descripcion + "</option>";
            }
            return result;
        }

        public string ObtenerCiudades(string idunis)
        {
            Ref_odont_unis unis = BusClass.Odont_unis().Where(l => l.descripcion == idunis).FirstOrDefault();
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.GetCiudades().Where(l => l.id_ref_odont_unis == unis.id_ref_unis).ToList();
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.nombre + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        public ActionResult CapacidadResolutiva()
        {
            Models.Insumos.Indicador_calidad Model = new Models.Insumos.Indicador_calidad();
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";
            ViewBag.meses = BusClass.meses();


            return View(Model);
        }

        [HttpPost]
        public ActionResult CapacidadResolutiva(HttpPostedFileBase file, int mes, int año)
        {
            Models.Insumos.Indicador_calidad Model = new Models.Insumos.Indicador_calidad();

            ViewBag.meses = BusClass.meses();
            if (Model.ValidarExistenciaIndicadorCalidad(mes, año))
            {
                string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(ruta, fileName);

                try
                {
                    file.SaveAs(path);

                    insumos_capacidad_resolutiva obj = new insumos_capacidad_resolutiva();
                    obj.mes = mes;
                    obj.año = año;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    List<insumos_capacidad_resolutiva_dtll> list = EntidadIndicadoresCalidad(path, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        Model.InsertarIndicadoresCalidadDtll(list, obj, ref MsgRes);

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            ViewData["rta"] = 1;
                            ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                            System.IO.File.Delete(path);
                        }
                        else
                        {
                            ViewData["rta"] = 2;
                            ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["exitoso"] = 0;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception ex)
                {
                    ViewData["exitoso"] = 0;
                    ViewData["rta"] = 2;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
                    System.IO.File.Delete(path);
                }
            }
            else
            {
                ViewData["rta"] = 2;
                ViewData["Msg"] = "Lo sentimos, ya ha sido cargada una base con el mismo mes y año ";
            }

            return View();

        }
        private List<insumos_capacidad_resolutiva_dtll> EntidadIndicadoresCalidad(string rutafichero, ref MessageResponseOBJ MsgRes)
        {
            List<insumos_capacidad_resolutiva_dtll> result = new List<insumos_capacidad_resolutiva_dtll>();
            var book = new ExcelQueryFactory(rutafichero);
            var EData1 = (from c in book.WorksheetRange("A1", "P999999", "Indicadores") select c).ToList();
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {

                    insumos_capacidad_resolutiva_dtll obj = new insumos_capacidad_resolutiva_dtll();
                    var item = EData1[i];
                    fila++;
                    if (item[0] != null && item[0] != "")
                    {
                        var texto = "";

                        columna = "REGIONAL";
                        texto = item[0];
                        if (texto.Length <= 50)
                        {
                            obj.regional = Convert.ToString(item[0]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "UNIS";
                        texto = item[1];
                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item[1]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD";
                        texto = item[2];
                        if (texto.Length <= 50)
                        {
                            obj.ciudad = Convert.ToString(item[2]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NIT PRESTADOR";
                        obj.nit_prestador = int.Parse(item[3]);

                        columna = "PRESTADOR";
                        texto = item[4];
                        if (texto.Length <= 200)
                        {
                            obj.prestador = Convert.ToString(item[4]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Prestador Homologado";
                        texto = item[5];
                        if (texto.Length <= 200)
                        {
                            obj.prestador_homologado = Convert.ToString(item[5]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DOCUMENTO PROFESIONAL";


                        //var documento = int.Parse(item[6]);

                        obj.documento_profesional = Convert.ToInt64(item[6]);

                        columna = "PROFESIONAL";
                        texto = item[7];
                        if (texto.Length <= 100)
                        {
                            obj.profesional = Convert.ToString(item[7]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ESPECIALIDAD";
                        texto = item[8];
                        if (texto.Length <= 100)
                        {
                            obj.especialidad = Convert.ToString(item[8]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Periodo";
                        obj.periodo = Convert.ToDateTime(item[9]);

                        columna = "Mes";
                        texto = item[10];
                        if (texto.Length <= 50)
                        {
                            obj.mes = Convert.ToString(item[10]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Trimestre";
                        texto = item[11];
                        if (texto.Length <= 50)
                        {
                            obj.trimestre = Convert.ToString(item[11]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CLAVE (CIUDAD+PRESTADOR + ID PROFESIONAL + ESPECIALIDAD+MES)";
                        texto = item[12];
                        if (texto.Length <= 500)
                        {
                            obj.clave = Convert.ToString(item[12]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AUTORIZACIONES";
                        obj.autorizaciones = int.Parse(item[13]);

                        columna = "CITAS";
                        obj.citas = int.Parse(item[14]);

                        columna = "CAPACIDAD RESOLUTIVA";
                        if (item[15] == null || item[15] == "")
                        {
                            obj.capacidad_resolutiva = 0;
                        }
                        else
                        {
                            obj.capacidad_resolutiva = Convert.ToDecimal(item[15]);
                        }

                        result.Add(obj);
                        obj = new insumos_capacidad_resolutiva_dtll();
                    }
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
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

        public ActionResult TableroIndicadoresCalidad(DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, string regional, string unis, string ciudad, string especialidad, string Codprestador, string Prestador)
        {
            Models.Insumos.Indicador_calidad Model = new Models.Insumos.Indicador_calidad();
            ViewBag.regional = BusClass.GetRefRegion();
            List<calidad_ref_especialidad> especialidades = BusClass.GetEspecialidades().ToList();
            ViewBag.especialidad = especialidades;

            List<management_insumos_capacidad_resolutiva_listaResult> listaIndicadores = new List<management_insumos_capacidad_resolutiva_listaResult>();

            var conteo = 0;
            if (FechaInicialfiltro != null || FechaFinalfiltro != null || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(unis) || !string.IsNullOrEmpty(ciudad) ||
                !string.IsNullOrEmpty(especialidad) || !string.IsNullOrEmpty(Codprestador) || !string.IsNullOrEmpty(Prestador))
            {
                listaIndicadores = BusClass.ListaInsumosCapacidadResolutiva();
                conteo = 1;
            }

            if (FechaInicialfiltro != null)
            {
                listaIndicadores = listaIndicadores.Where(x => x.periodo >= FechaInicialfiltro).ToList();
            }

            if (FechaFinalfiltro != null)
            {
                listaIndicadores = listaIndicadores.Where(x => x.periodo <= FechaFinalfiltro).ToList();
            }

            if (regional != null && regional != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.regional.Contains(regional)).ToList();
            }

            if (unis != null && unis != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.unis == unis).ToList();
            }

            if (ciudad != null && ciudad != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.ciudad == ciudad).ToList();
            }
            if (especialidad != null && especialidad != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.especialidad == especialidad).ToList();
            }

            if (Codprestador != null && Codprestador != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.nit_prestador == int.Parse(Codprestador)).ToList();
            }

            if (Prestador != null && Prestador != "")
            {
                listaIndicadores = listaIndicadores.Where(x => x.prestador.Contains(Prestador)).ToList();
            }

            ViewBag.countresultados = 0;
            ViewBag.resultado = 0;
            double? autorizaciones = 0;
            double? citas = 0;
            double? c = 0;


            if (conteo == 1)
            {

                if (listaIndicadores.Count() != 0)
                {
                    autorizaciones = listaIndicadores.Select(l => l.autorizaciones).Sum();
                    citas = listaIndicadores.Select(l => l.citas).Sum();
                    c = ((autorizaciones / citas) * 100);

                    Session["ListaIndicadoresGlobal"] = listaIndicadores;
                    ViewBag.countresultados = listaIndicadores.Count();
                    ViewBag.resultado = 1;
                }
                else
                {
                    ViewBag.resultado = 2;

                }
            }
            ViewBag.reg = regional;

            ViewBag.autorizaciones = autorizaciones;
            ViewBag.citas = citas;
            ViewBag.indicador = Math.Round(c.Value, 2);

            Model.IndicadorLista = new List<management_insumos_capacidad_resolutiva_listaResult>();
            Model.IndicadorLista = listaIndicadores;

            ViewBag.conteo = listaIndicadores.Count();
            ViewBag.IndicadoresLista = listaIndicadores;

            return View(Model);
        }

        public void DescargarResultadosIndicadoresCalidad()
        {
            List<management_insumos_capacidad_resolutiva_listaResult> List = (List<management_insumos_capacidad_resolutiva_listaResult>)Session["ListaIndicadoresGlobal"];
            if (List.Count != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Indicadores");

                Sheet.Cells["A1"].Value = "REGIONAL";
                Sheet.Cells["B1"].Value = "UNIS";
                Sheet.Cells["C1"].Value = "CIUDAD";
                Sheet.Cells["D1"].Value = "NIT_PRESTADOR";
                Sheet.Cells["E1"].Value = "PRESTADOR";
                Sheet.Cells["F1"].Value = "Prestador Homologado";
                Sheet.Cells["G1"].Value = "DOCUMENTO PROFESIONAL";
                Sheet.Cells["H1"].Value = "PROFESIONAL";
                Sheet.Cells["I1"].Value = "ESPECIALIDAD";
                Sheet.Cells["J1"].Value = "Periodo";
                Sheet.Cells["K1"].Value = "Mes";
                Sheet.Cells["L1"].Value = "Trimestre";
                Sheet.Cells["M1"].Value = "CLAVE (CIUDAD+PRESTADOR + ID PROFESIONAL + ESPECIALIDAD+MES)";
                Sheet.Cells["N1"].Value = "AUTORIZACIONES";
                Sheet.Cells["O1"].Value = "CITAS";
                Sheet.Cells["P1"].Value = "CAPACIDAD RESOLUTIVA";
                int row = 2;

                foreach (var line in List)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = line.regional;
                    Sheet.Cells[string.Format("B{0}", row)].Value = line.unis;
                    Sheet.Cells[string.Format("C{0}", row)].Value = line.ciudad;
                    Sheet.Cells[string.Format("D{0}", row)].Value = line.nit_prestador;
                    Sheet.Cells[string.Format("E{0}", row)].Value = line.prestador;
                    Sheet.Cells[string.Format("F{0}", row)].Value = line.prestador_homologado;
                    Sheet.Cells[string.Format("G{0}", row)].Value = line.documento_profesional;
                    Sheet.Cells[string.Format("H{0}", row)].Value = line.profesional;
                    Sheet.Cells[string.Format("I{0}", row)].Value = line.especialidad;
                    Sheet.Cells[string.Format("J{0}", row)].Value = line.periodo;
                    Sheet.Cells[string.Format("K{0}", row)].Value = line.mes;
                    Sheet.Cells[string.Format("L{0}", row)].Value = line.trimestre;
                    Sheet.Cells[string.Format("M{0}", row)].Value = line.clave;
                    Sheet.Cells[string.Format("N{0}", row)].Value = line.autorizaciones;
                    Sheet.Cells[string.Format("O{0}", row)].Value = line.citas;
                    Sheet.Cells[string.Format("P{0}", row)].Value = line.capacidad_resolutiva;
                    row++;
                }

                Sheet.Cells["A:P"].AutoFitColumns();
                Sheet.Cells["A:P"].AutoFitColumns();
                Sheet.Cells["A1:P1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(54, 96, 146);
                Sheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:P1"].Style.Font.Color.SetColor(Color.White);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoIndicadoresCalidad" + ".xlsX");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
            }
        }

        public string ObtenerEspecialidad()
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<calidad_ref_especialidad> especialidades = BusClass.GetEspecialidades().ToList();
            foreach (var item in especialidades)
            {
                result += "<option value='" + item.descripcion + "'>" + item.descripcion + "</option>";
            }

            return result;
        }

        public JsonResult Buscar_CodPrestador_IndicadoresCalidad()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {

                    List<management_insumos_capacidad_resolutiva_listaResult> prestadores = BusClass.ListaInsumosCapacidadResolutiva();
                    prestadores = prestadores.Where(x => x.prestador.ToUpper().Contains(term.ToUpper()) || Convert.ToString(x.nit_prestador).ToUpper().Contains(term.ToUpper())).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nit = reg.nit_prestador,
                                     label = reg.nit_prestador,
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

        public JsonResult Buscar_Prestador_IndicadoresCalidad()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {

                    List<management_insumos_capacidad_resolutiva_listaResult> prestadores = BusClass.ListaInsumosCapacidadResolutiva();
                    prestadores = prestadores.Where(x => x.prestador.ToUpper().Contains(term.ToUpper()) || Convert.ToString(x.nit_prestador).ToUpper().Contains(term.ToUpper()) ||
                    x.prestador_homologado.ToUpper().Contains(term)).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nombre = reg.prestador,
                                     label = reg.prestador,
                                 }).Distinct().OrderBy(f => f.nombre).Take(15);
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
    }
}
