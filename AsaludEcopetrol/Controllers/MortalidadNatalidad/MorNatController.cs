using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using Facede;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.MortalidadNatalidad
{
    [SessionExpireFilter]
    public class MorNatController : Controller
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

        public class CustomPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;

            public CustomPostedFile(byte[] fileBytes, string filename = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = filename;
                this.ContentType = "application/octet-stream";
                this.stream = new MemoryStream(fileBytes);
            }

            public override int ContentLength => fileBytes.Length;
            public override string FileName { get; }
            public override Stream InputStream
            {
                get { return stream; }
            }
            public override string ContentType { get; }
        }

        public ActionResult ReporteMortalidad(int? idMortalidad)
        {
            mortalidad_registros reg = new mortalidad_registros();

            try
            {
                if (idMortalidad != null)
                {
                    reg = BusClass.TraerDatosMortalidad(idMortalidad);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.rta = 0;
            ViewBag.msg = "";

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.tipoDoc = BusClass.GetTipoIdentificacion(ref MsgRes);
            ViewBag.tipoBeneficiario = BusClass.TraerTiposBeneficiario();

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 6);
            años.Add(DateTime.Now.Year - 5);
            años.Add(DateTime.Now.Year - 4);
            años.Add(DateTime.Now.Year - 3);
            años.Add(DateTime.Now.Year - 2);
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);

            ViewBag.años = años;

            ViewBag.idMortalidad = idMortalidad;
            ViewBag.reg = reg;


            return View(reg);
        }

        public JsonResult BuscarBeneficiarios()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length > 0)
                {
                    term = term.ToUpper();

                    List<base_beneficiarios_analitica> bb = new List<base_beneficiarios_analitica>();
                    bb = BusClass.TraerBeneficiarioDocumento(term);

                    var lista = (from ti in bb
                                 select new
                                 {
                                     id = ti.id_base_beneficiarios,
                                     label = ti.Numero_identificacion + "-" + ti.Nombre + " " + ti.Apellidos
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

        public string LlenarDatosBB(int? idBB)
        {
            mortalidad_registros mor = new mortalidad_registros();
            var resultado = "";
            string identificacion = "";
            var existeRegistro = 0;

            try
            {
                management_buscarBeneficiarios_MorNatResult bb = BusClass.TraerDatosBeneficiario(idBB);
                identificacion = bb.numero_identificacion;

                mor = BusClass.TraerDatosMortalidadIdentificacion(identificacion);

                if (mor != null)
                {
                    existeRegistro = 1;
                }

                resultado = $"{existeRegistro}|{bb.numero_identificacion}|{bb.cod_personal}|{bb.clase_de_identificacion}|{bb.apellidos}|{bb.nombre}|{bb.genero}|{bb.fecha_nacimiento.Value.ToString("MM/dd/yyyy")}|{bb.id_tipoSalud}";

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }


            return resultado;
        }

        public string buscarIdentificacion(string identificacion)
        {
            mortalidad_registros mor = new mortalidad_registros();
            var respuesta = "";
            var existeRegistro = 0;

            try
            {
                mor = BusClass.TraerDatosMortalidadIdentificacion(identificacion);

                if (mor != null)
                {
                    existeRegistro = 1;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return respuesta;

        }

        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_odont_unis> Unis = BusClass.Odont_unisIdRegional(idregional);
            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_unis + "'>" + item.descripcion + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.GetCiudadesXUnis(idunis);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReporteMortalidad(Models.MorNat.Mortalidad Model)
        {
            mortalidad_registros reg = new mortalidad_registros();
            var mensaje = "";
            var rta = 2;
            var gestiona = 0;
            var id = 0;

            try
            {
                reg.año = Model.Año;

                var mes = Model.Mes;
                var trimestre = 0;

                if (mes >= 1 && mes <= 3)
                {
                    trimestre = 1;
                }
                else if (mes >= 4 && mes <= 6)
                {
                    trimestre = 2;
                }
                else if (mes >= 7 && mes <= 9)
                {
                    trimestre = 3;
                }
                else
                {
                    trimestre = 4;
                }

                var causaFallecimiento = Model.CausaFallecimiento;

                List<ref_cie10_mortNat> mortalidades = new List<ref_cie10_mortNat>();
                mortalidades = BusClass.GetCie10MorNatBycodigo(causaFallecimiento);

                if (mortalidades.Count() == 0)
                {
                    throw new Exception("NO EXISTE ESTA CAUSA FALLECIMIENTO");
                }

                reg.trimestre = trimestre;
                reg.mes = Model.Mes;
                reg.regional = Model.Regional;
                reg.unis = Model.Unis;
                reg.ciudad_smed = Model.CiudadSmed;
                reg.tipo_documento = Model.TipoDocumento;
                reg.numero_documento = Model.NumeroDocumento;
                reg.apellidos = Model.Apellidos;
                reg.nombres = Model.Nombres;
                reg.genero = Model.Genero;
                reg.fecha_nacimiento = Model.FechaNacimiento;
                reg.edad = Model.Edad;
                reg.tipo_beneficiario = Model.TipoBeneficiario;
                reg.nro_certificado = Model.NroCertificado;
                reg.fecha_fallecimiento = Model.FechaFallecimiento;
                reg.causa_fallecimiento = Model.CausaFallecimiento;
                reg.confirmado_descartado = Model.ConfirmadoDescartado;
                reg.fecha_notificacion = Model.FechaNotificacion;
                reg.fuente = Model.Fuente;
                reg.version = Model.Version != null ? Model.Version + 1 : 1;
                reg.observacion = Model.observacion;
                reg.fecha_digita = DateTime.Now;
                reg.usuario_digita = SesionVar.UserName;

                if (Model.IdMortalidad == null || Model.IdMortalidad == 0)
                {
                    gestiona = BusClass.InsercionMortalidadRegistro(reg);
                }
                else
                {
                    reg.id_mortalidad = Model.IdMortalidad;
                    id = Model.IdMortalidad;
                    gestiona = BusClass.ActualizarRegistroMortalidad(reg);
                }

                if (gestiona != 0)
                {
                    mensaje = "MORTALIDAD INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR LA MORTALIDAD";
                }

                Model.IdMortalidad = 0;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INGRESAR LA MORTALIDAD: " + error;

            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.rta = rta;
            ViewBag.msg = mensaje;

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.tipoDoc = BusClass.GetTipoIdentificacion(ref MsgRes);
            ViewBag.tipoBeneficiario = BusClass.TraerTiposBeneficiario();

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 6);
            años.Add(DateTime.Now.Year - 5);
            años.Add(DateTime.Now.Year - 4);
            años.Add(DateTime.Now.Year - 3);
            años.Add(DateTime.Now.Year - 2);
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);

            ViewBag.años = años;

            mortalidad_registros Nuereg = new mortalidad_registros();
            //Nuereg = BusClass.TraerDatosMortalidad(gestiona);

            ViewBag.idMortalidad = 0;
            ViewBag.reg = Nuereg;


            if (id == null || id == 0)
            {
                return View(Nuereg);
            }
            else
            {
                return RedirectToAction("TableroMortalidad", "MorNat", new { rta = rta, msg = mensaje });
            }

        }

        public JsonResult BuscarCIE10()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    term = term.ToUpper();

                    List<ref_cie10_mortNat> CIELOS = new List<ref_cie10_mortNat>();
                    CIELOS = BusClass.GetCie10MorNatBycodigo(term);

                    var lista = (from reg in CIELOS
                                 select new
                                 {
                                     id = reg.codigo,
                                     label = reg.codigo + "-" + reg.descripcion,
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

        public ActionResult TableroMortalidad(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro, int? rta, string msg)
        {
            List<management_TableroMortalidadResult> listadoMortalidad = new List<management_TableroMortalidadResult>();
            var conteo = 0;
            try
            {
                listadoMortalidad = BusClass.TraerMortalidadesTablero(año, trimestre, mes, unis, regional, documento, fechaFiltro);
                conteo = listadoMortalidad.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            ViewBag.listadoMortalidad = listadoMortalidad;
            ViewBag.conteo = conteo;
            ViewBag.rol = SesionVar.ROL;

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);
            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();

            Session["ListadoMortalidad"] = listadoMortalidad;

            ViewBag.rta = rta;
            ViewBag.msg = msg;

            return View();
        }

        public JsonResult EliminarMortalidadId(int? id)
        {
            var rta = 0;
            var mensaje = "";
            try
            {
                var elimina = BusClass.EliminarMOrtalidadId(id, SesionVar.UserName);
                if (elimina != 0)
                {
                    mensaje = "Mortalidad eliminada correctamente";
                    rta = 1;
                }
                else
                {
                    mensaje = "Error al eliminar la mortalidad";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"Error al eliminar la mortalidad: {error}";

            }

            return Json(new { mensaje = mensaje, rta = rta });
        }


        public ActionResult ReporteNatalidad(int? idNatalidad)
        {
            natalidad_registros nat = new natalidad_registros();

            try
            {
                if (idNatalidad != null && idNatalidad != 0)
                {
                    nat = BusClass.TraerDatosNatalidad(idNatalidad);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.rta = "";
            ViewBag.rta = 0;

            ViewBag.nat = nat;

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 6);
            años.Add(DateTime.Now.Year - 5);
            años.Add(DateTime.Now.Year - 4);
            años.Add(DateTime.Now.Year - 3);
            años.Add(DateTime.Now.Year - 2);
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);

            ViewBag.años = años;

            ViewBag.idNatalidad = idNatalidad;

            ViewBag.ciudades = BusClass.TotalCiudades();

            return View(nat);
        }

        [HttpPost]
        public ActionResult ReporteNatalidad(Models.MorNat.Natalidad Model)
        {
            natalidad_registros nat = new natalidad_registros();
            var gestion = 0;
            var mensaje = "";
            var rta = 0;
            try
            {
                nat.id_regional = Model.IdRegional;
                nat.identificacion_madre = Model.IdentificacionMadre;
                nat.cod_personal = Model.CodPersonal;
                nat.apellidos = Model.Apellidos;
                nat.nombres = Model.Nombres;
                nat.id_mes = Model.IdMes;

                var mes = Model.IdMes;
                var trimestre = 0;

                if (mes >= 1 && mes <= 3)
                {
                    trimestre = 1;
                }
                else if (mes >= 4 && mes <= 6)
                {
                    trimestre = 2;
                }
                else if (mes >= 7 && mes <= 9)
                {
                    trimestre = 3;
                }
                else
                {
                    trimestre = 4;
                }

                nat.id_trimestre = trimestre;
                nat.año = Model.Anio;
                nat.id_ciudad_smed = Model.IdCiudadSmed;
                nat.id_localidad = Model.IdLocalidad;
                nat.fecha_nacimiento = Model.FechaNacimiento;
                nat.id_unis = Model.IdUnis;
                nat.edad_gestacional = Model.EdadGestacional;
                nat.peso = Model.Peso;
                nat.via_parto = Model.ViaParto;
                nat.talla = Model.Talla;
                nat.sexo = Model.Sexo;
                nat.apgar = Model.Apgar;
                nat.causa_egreso = Model.CausaEgreso;
                nat.control_prenatal = Model.ControlPrenatal;
                nat.fecha = Model.Fecha;
                nat.observaciones = Model.Observaciones;
                nat.fecha_digita = DateTime.Now;
                nat.usuario_digita = SesionVar.UserName;
                nat.version = Model.VersionN != 0 && Model.VersionN != null ? Model.VersionN + 1 : 1;

                if (Model.IdNatalidad == null || Model.IdNatalidad == 0)
                {
                    gestion = BusClass.InsercionNatalidadRegistro(nat);
                }
                else
                {
                    nat.id_natalidad = Model.IdNatalidad;
                    gestion = BusClass.ActualizarRegistroNatalidad(nat);
                }

                if (gestion != 0)
                {
                    mensaje = "MORTALIDAD INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR LA MORTALIDAD";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INGRESAR LA MORTALIDAD: " + error;

            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.msg = mensaje;
            ViewBag.rta = rta;

            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);

            ViewBag.años = años;

            natalidad_registros reg = new natalidad_registros();
            //reg = BusClass.TraerDatosNatalidad(gestion);

            ViewBag.nat = reg;
            ViewBag.idNatalidad = 0;
            ViewBag.ciudades = BusClass.TotalCiudades();

            if (Model.IdNatalidad == null || Model.IdNatalidad == 0)
            {
                return View(reg);
            }
            else
            {
                return RedirectToAction("TableroNatalidad", "MorNat", new { rta = rta, msg = mensaje });
            }
        }

        public ActionResult TableroNatalidad(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro, int? rta, string msg)
        {
            List<management_TableroNatalidadResult> listadoNatalidad = new List<management_TableroNatalidadResult>();
            var conteo = 0;
            try
            {
                listadoNatalidad = BusClass.TraerNatalidadesTablero(año, trimestre, mes, unis, regional, documento, fechaFiltro);
                conteo = listadoNatalidad.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listadoNatalidad = listadoNatalidad;
            ViewBag.conteo = conteo;
            ViewBag.rol = SesionVar.ROL;

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);
            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();

            Session["ListaNatalidad"] = listadoNatalidad;

            ViewBag.msg = msg;
            ViewBag.rta = rta;

            return View();
        }

        public JsonResult EliminarNatalidadId(int? id)
        {
            var rta = 0;
            var mensaje = "";
            try
            {
                var elimina = BusClass.EliminarNatalidadId(id, SesionVar.UserName);
                if (elimina != 0)
                {
                    mensaje = "Natalidad eliminada correctamente";
                    rta = 1;
                }
                else
                {
                    mensaje = "Error al eliminar la natalidad";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = $"Error al eliminar la natalidad: {error}";
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }


        public void ExportarDatosMortalidad()
        {
            List<management_TableroMortalidadResult> lista = new List<management_TableroMortalidadResult>();

            try
            {
                lista = (List<management_TableroMortalidadResult>)Session["ListadoMortalidad"];
                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosMortalidad");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:X1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:X1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:X1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:X1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:X1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:X1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Año";
                    Sheet.Cells["B1"].Value = "Trimestre";
                    Sheet.Cells["C1"].Value = "Mes";
                    Sheet.Cells["D1"].Value = "Regional";
                    Sheet.Cells["E1"].Value = "Unis";
                    Sheet.Cells["F1"].Value = "Ciudad SMed";
                    Sheet.Cells["G1"].Value = "Tipo de Documento";
                    Sheet.Cells["H1"].Value = "No. Documento";
                    Sheet.Cells["I1"].Value = "Apellidos";
                    Sheet.Cells["J1"].Value = "Nombres";
                    Sheet.Cells["K1"].Value = "Género";
                    Sheet.Cells["L1"].Value = "Fecha Nacimiento";
                    Sheet.Cells["M1"].Value = "Edad";
                    Sheet.Cells["N1"].Value = "Tipo Beneficiario";
                    Sheet.Cells["O1"].Value = "No certificado";
                    Sheet.Cells["P1"].Value = "Fecha Fallecimiento";
                    Sheet.Cells["Q1"].Value = "Causa Fallecimiento - CIE10";
                    Sheet.Cells["R1"].Value = "Descripción Causa Fallecimiento - CIE10";
                    Sheet.Cells["S1"].Value = "¿Confirmado o descartado?";
                    Sheet.Cells["T1"].Value = "Fecha Notificación";
                    Sheet.Cells["U1"].Value = "Fuente";
                    Sheet.Cells["V1"].Value = "Observaciones";
                    Sheet.Cells["W1"].Value = "Fecha digita";
                    Sheet.Cells["X1"].Value = "Usuario digita";

                    int row = 2;

                    foreach (management_TableroMortalidadResult item in lista)
                    {
                        Sheet.Cells["A" + row + ":X" + row].Style.Font.Size = 10;

                        Sheet.Cells[string.Format("A{0}", row)].Value = item.año;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.nombreTrimestre;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.nombreMes;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.indice;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.nombreUnis;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.tipo_documento;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.numero_documento;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.apellidos;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.nombres;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.genero;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.edad;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.nombreTipoBeneficiario;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.nro_certificado;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.fecha_fallecimiento;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.causa_fallecimiento;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcionCausaFallecimiento;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.confirmado_descartado;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.fecha_notificacion;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.fuente;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.observacion;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.fecha_digita;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.nombreDigita;

                        Sheet.Cells[string.Format("L{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteMortalidad";
                    Sheet.Cells["A:X"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
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
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void ExportarDatosNatalidad()
        {
            List<management_TableroNatalidadResult> lista = new List<management_TableroNatalidadResult>();

            try
            {
                lista = (List<management_TableroNatalidadResult>)Session["ListaNatalidad"];

                if (lista != null)
                {
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DatosNatalidad");

                    Color colFromHex = Color.FromArgb(99, 99, 99);
                    //Sheet.Cells["A1:N1"].Style.WrapText = true;
                    Sheet.Cells["A1:X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:X1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:X1"].Style.Font.Color.SetColor(Color.White);
                    Sheet.Cells["A1:X1"].Style.Font.Size = 10;
                    Sheet.Cells["A1:X1"].Style.Font.Bold = true;
                    Sheet.Cells["A1:X1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A1:X1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    Sheet.Cells["A1"].Value = "Id natalidad";
                    Sheet.Cells["B1"].Value = "Regional Pertenece";
                    Sheet.Cells["C1"].Value = "Identificación de la Madre";
                    Sheet.Cells["D1"].Value = "Cód Personal";
                    Sheet.Cells["E1"].Value = "Apellidos";
                    Sheet.Cells["F1"].Value = "Nombres";
                    Sheet.Cells["G1"].Value = "Mes";
                    Sheet.Cells["H1"].Value = "Trimestre";
                    Sheet.Cells["I1"].Value = "Año";
                    Sheet.Cells["J1"].Value = "Descripción Ciudad SMed";
                    Sheet.Cells["K1"].Value = "Fecha de Nacimiento";
                    Sheet.Cells["L1"].Value = "Unis";
                    Sheet.Cells["M1"].Value = "Ciudad Nacimiento";
                    Sheet.Cells["N1"].Value = "Edad Gestacional";
                    Sheet.Cells["O1"].Value = "Peso";
                    Sheet.Cells["P1"].Value = "Vía del Parto";
                    Sheet.Cells["Q1"].Value = "Talla";
                    Sheet.Cells["R1"].Value = "Sexo";
                    Sheet.Cells["S1"].Value = "Apgar";
                    Sheet.Cells["T1"].Value = "Causa Egreso";
                    Sheet.Cells["U1"].Value = "Control Prenatal";
                    Sheet.Cells["V1"].Value = "Fecha";
                    Sheet.Cells["W1"].Value = "Observaciones";
                    Sheet.Cells["X1"].Value = "Fecha digita";

                    int row = 2;

                    foreach (management_TableroNatalidadResult item in lista)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.id_natalidad;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.indireRegional;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.identificacion_madre;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.cod_personal;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.apellidos;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.nombres;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.nombreMes;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.nombreTrimestre;
                        Sheet.Cells[string.Format("I{0}", row)].Value = item.año;
                        Sheet.Cells[string.Format("J{0}", row)].Value = item.nombreCiudad;
                        Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_nacimiento;
                        Sheet.Cells[string.Format("L{0}", row)].Value = item.nombreUnis;
                        Sheet.Cells[string.Format("M{0}", row)].Value = item.nombreCiudadNacimiento;
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.edad_gestacional;
                        Sheet.Cells[string.Format("O{0}", row)].Value = item.peso;
                        Sheet.Cells[string.Format("P{0}", row)].Value = item.via_parto;
                        Sheet.Cells[string.Format("Q{0}", row)].Value = item.talla;
                        Sheet.Cells[string.Format("R{0}", row)].Value = item.genero;
                        Sheet.Cells[string.Format("S{0}", row)].Value = item.apgar;
                        Sheet.Cells[string.Format("T{0}", row)].Value = item.causa_egreso;
                        Sheet.Cells[string.Format("U{0}", row)].Value = item.control_prenatal;
                        Sheet.Cells[string.Format("V{0}", row)].Value = item.fecha;
                        Sheet.Cells[string.Format("W{0}", row)].Value = item.observaciones;
                        Sheet.Cells[string.Format("X{0}", row)].Value = item.fecha_digita;

                        Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        row++;
                    }

                    string namefile = "ReporteNatalidad";
                    Sheet.Cells["A:X"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + "_" + DateTime.Now + ".xlsx");
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
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                       "window.alert('ERROR EN LA DESCARGA');" +
                       "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }
    }
}