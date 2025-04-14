using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.VerificacionEstablecimiento
{

    [SessionExpireFilter]
    public class VerificacionController : Controller
    {
        readonly Models.Verificacion.VerificacionFarmaceutica Model = new Models.Verificacion.VerificacionFarmaceutica();

        #region PROPIEDADES

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

        public ActionResult VerificacionFarmaceutica(int? tab, int? id)
        {
            try
            {
                ViewBag.reftipocriterio = Model.Get_refTipoCriterio();
                List<ref_verificacion_farmaceutico> lista = BusClass.Get_refVerificacionFarmaceutita();
                ViewBag.reftipoverificacion = lista;
                ViewBag.conteo = lista.Count();
                ViewBag.tipoVer = Model.getTiposVerificacion();

                ViewBag.tab = tab;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public JsonResult ActualizarVerificacion(int? id, string descripcion)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var actualiza = BusClass.ActualizarDesVerificacionFarmaceutica(id, descripcion);
                if (actualiza != 0)
                {
                    mensaje = "DESCRIPCIÓN ACTUALIZADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ACTUALIZAR LA DESCRIPCIÓN";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ACTUALIZAR LA DESCRIPCIÓN: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult GetdatosVerificacion(int idTipoVerificacion)
        {
            try
            {
                ref_verificacion_farmaceutico verificacion = Model.Get_refVerificacionFarmaceutitaById(idTipoVerificacion);

                if (verificacion != null)
                {
                    var data = new
                    {
                        idverificacion = verificacion.id_veriticacion,
                        nomverificacion = verificacion.descripcion,
                        peso = verificacion.peso
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(0);
            }
        }

        public JsonResult GuardarTipoVerificacion(int? idtipoverificacion, string NomTipoverificacion, int peso)
        {
            try
            {
                List<ref_verificacion_farmaceutico> validacion = BusClass.Get_refVerificacionFarmaceutita();
                validacion = validacion.Where(x => x.descripcion.Contains(NomTipoverificacion)).ToList();

                ref_verificacion_farmaceutico obj = new ref_verificacion_farmaceutico();
                obj.id_veriticacion = (int)idtipoverificacion;
                obj.descripcion = NomTipoverificacion.ToUpper();
                obj.peso = (int)peso;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = Convert.ToString(SesionVar.IDUser);


                if (idtipoverificacion == 0 && validacion.Count == 0)
                {
                    Model.InsertarVerificacion(obj, ref MsgRes);
                }
                else
                {
                    if (validacion.Count() != 0)
                    {
                        ref_verificacion_farmaceutico valida = validacion.FirstOrDefault();
                        obj.id_veriticacion = valida.id_veriticacion;
                    }

                    Model.ActualizarVerificacion(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { rta = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 1, msj = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { rta = 1, msj = error }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarTipoverificacion(int idVerificacion)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    var criterios = db.ver_criterio.Where(l => l.id_tipo_verificacion == idVerificacion).ToList();
                    if (criterios.Count > 0)
                    {
                        db.ver_criterio.DeleteAllOnSubmit(criterios);
                        db.SubmitChanges();
                    }

                    var tipocriterios = db.ver_tipocriterio.Where(l => l.id_tipo_verificacion == idVerificacion).ToList();
                    if (tipocriterios.Count > 0)
                    {
                        db.ver_tipocriterio.DeleteAllOnSubmit(tipocriterios);
                        db.SubmitChanges();
                    }

                    var verificacion = db.ref_verificacion_farmaceutico.Where(l => l.id_veriticacion == idVerificacion).FirstOrDefault();
                    db.ref_verificacion_farmaceutico.DeleteOnSubmit(verificacion);
                    db.SubmitChanges();

                    return RedirectToAction("VerificacionFarmaceutica", "Verificacion");
                }
                catch (Exception ex)
                {
                    var mensajeError = ex.Message;
                    throw;
                }
            }
        }

        public ActionResult AdminCriterios(int idTipoVerificacion)
        {
            try
            {
                ViewBag.reftipocriterio = Model.Get_refTipoCriterio();
                List<ref_ver_tipoCriterio> list = Model.Get_refTipoCriterio();

                ViewBag.conteo = list.Count();

                ViewBag.tipoVerificacion = idTipoVerificacion;
                ViewBag.ver_tipoVerificacion = Model.get_ref_tipoCriterio(idTipoVerificacion);
                ViewBag.refverGrupoTipoVerificacion = Model.get_ver_grupoTipoCritero();
                ViewBag.tipoImpacto = BusClass.ListImpactosEvaluacion();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public JsonResult GuardarAdmincriterios(int tipoVerificacion, List<int> seleccionados, List<int> seleccionados2, List<string> seleccionados3)
        {
            try
            {
                var usuario = SesionVar.UserName;
                Model.InsertarAdminCriteriosver(tipoVerificacion, seleccionados, seleccionados2, seleccionados3, usuario, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { rta = 1, msj = "Guardado Exitosamente" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 2, msj = "Error: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { rta = 2, msj = "Error: " + error }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetdatosTipoCriterio(int idtipocriterio)
        {
            try
            {
                ref_ver_tipoCriterio tipocriterio = new ref_ver_tipoCriterio();
                tipocriterio = Model.Get_refTipoCriterio().Where(l => l.id_tipo_criterio == idtipocriterio).FirstOrDefault();

                if (tipocriterio != null)
                {
                    var data = new
                    {
                        idtipocriterio = tipocriterio.id_tipo_criterio,
                        idc = tipocriterio.indice,
                        nomtipocriterio = tipocriterio.nombre_criterio,
                        peso = tipocriterio.peso,
                        impacto = tipocriterio.impacto
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(0);
            }
        }

        public JsonResult GuardarTipoCriterio(int idtipocriterio, string indice, string NomTipoCriterio, decimal? peso, string impacto)
        {
            try
            {
                ref_ver_tipoCriterio obj = new ref_ver_tipoCriterio();
                obj.indice = indice;
                obj.nombre_criterio = NomTipoCriterio.ToUpper();
                obj.peso = peso;
                obj.impacto = impacto;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = Convert.ToString(SesionVar.IDUser);

                if (idtipocriterio == 0)
                {
                    Model.InsertarTipoCriteriover(obj, ref MsgRes);
                }
                else
                {
                    obj.id_tipo_criterio = idtipocriterio;
                    Model.ActualizarTipoCriteriover(obj, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return Json(new { rta = 0, msj = "DATO INGRESADO CORRECTAMENTE" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 1, msj = "Error: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Json(new { rta = 1, msj = "Error: " + error }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult EliminarTipoCriterio(int idtipocriterio, int tipoahd)
        {
            try
            {
                Model.EliminarTipoCriteriover(idtipocriterio, ref MsgRes);
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }
            return RedirectToAction("AdminCriterios", "Verificacion", new { idTipoVerificacion = tipoahd });
        }

        public ActionResult VerificaciontipoCriterio(int idTipoVer)
        {
            try
            {
                var lista = BusClass.getTipoCriterioId(idTipoVer);
                lista = lista.Where(x => x.id_tipo_criterio_real != null).ToList();
                ViewBag.ver_tipoVerificacion = lista;
                ViewBag.conteoTipoCriterio = lista.Count();
                ViewBag.pesogeneral = lista.Select(l => l.puntaje).Sum();
                ViewBag.idTipoVer = idTipoVer;
                ViewBag.nomcohorte = BusClass.Get_refVerificacionFarmaceutitaById(idTipoVer).descripcion;
                ViewBag.criterios = Model.getcriteriosbytipoverificacion(idTipoVer);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public ActionResult CriteriosVerificacion(int idTipoVer, int tipocriterio)
        {
            var modelo = new List<ver_criterio>();
            try
            {
                var obj = BusClass.getTipoCriterioId(idTipoVer).Where(l => l.id_tipo_criterio == tipocriterio).FirstOrDefault();
                ViewBag.nombre = obj.nombre_criterio;
                ViewBag.tipocriterio = obj.descripcion;
                ViewBag.pesotipocriterio = obj.puntaje;
                ViewBag.idtipocriterio = tipocriterio;
                ViewBag.tipoVer = idTipoVer;
                ViewBag.respuesta = 0;
                ViewData["mensajeRespuesta"] = "";

                modelo = Model.getcriteriosbytipoverificacion(idTipoVer).Where(l => l.id_tipo_criterio == tipocriterio).ToList();
                ViewBag.totalpeso = modelo.Select(l => l.puntaje).Sum();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult CriteriosVerificacion(int idcriterio, int idtipoverificacion, int tipocriterio, string nomcriterio, string atributo)
        {
            var modelo = new List<ver_criterio>();
            try
            {

                ver_criterio criterio = null;
                if (idcriterio == 0)
                {
                    criterio = new ver_criterio();
                }
                else
                {
                    criterio = Model.ConsultarCriterioById2(idcriterio);
                }

                criterio.id_tipo_verificacion = idtipoverificacion;
                criterio.id_tipo_criterio = tipocriterio;
                criterio.descripcion = nomcriterio;
                criterio.puntaje = 0;
                criterio.atributo = atributo;

                if (idcriterio == 0)
                {
                    Model.InsertarCriteriover(criterio, ref MsgRes);
                }
                else
                {
                    Model.ActualizarCriteriover(criterio, ref MsgRes);
                }


                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewBag.respuesta = 1;
                    ViewData["mensajeRespuesta"] = "Guardado Exitosamente";
                }
                else
                {
                    ViewBag.respuesta = 2;
                    ViewData["mensajeRespuesta"] = "Ha ocurrido un error al momento de guardar la información: " + MsgRes.DescriptionResponse;
                }

                var obj = BusClass.getTipoCriterioId(idtipoverificacion).Where(l => l.id_tipo_criterio == tipocriterio).FirstOrDefault();
                ViewBag.nombre = obj.descripcion;
                ViewBag.tipocriterio = obj.nombre_criterio;
                ViewBag.pesotipocriterio = obj.puntaje;
                ViewBag.idtipocriterio = tipocriterio;
                ViewBag.tipoVer = idtipoverificacion;

                modelo = Model.getcriteriosbytipoverificacion(idtipoverificacion).Where(l => l.id_tipo_criterio == tipocriterio).ToList();
                ViewBag.totalpeso = modelo.Select(l => l.puntaje).Sum();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(modelo);
        }

        public JsonResult GetInfoCriterio(int idcriterio)
        {
            ver_criterio criterio = Model.ConsultarCriterioById2(idcriterio);
            var data = new
            {
                atributo = criterio.atributo,
                idcriterio = criterio.id_ver_criterio,
                descripcion = criterio.descripcion,
                puntaje = criterio.puntaje,
                idtipocriterio = criterio.id_tipo_criterio
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarCriterioVer(int idTipoVer, int idcriterio)
        {
            Model.EliminarCriterioVerificacion(idcriterio, ref MsgRes);

            return RedirectToAction("CriteriosVerificacion", "Verificacion", new { idTipoVer = idTipoVer });
        }

        public ActionResult CargueVerificacionPuntoDispensacion()
        {
            ViewData["rta"] = 0;
            ViewData["Msg"] = "";

            return View();
        }

        [HttpPost]
        public ActionResult CargueVerificacionPuntoDispensacion(HttpPostedFileBase file)
        {

            string ruta = System.Configuration.ConfigurationManager.AppSettings["RutaTemporalArchivos2"];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(ruta, fileName);

            try
            {
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

                List<ver_puntoDispensacion> list = VerificacionExcel(dataTable, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.InsertarCarguePuntoDispensacion(list, ref MsgRes);

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["rta"] = 1;
                        ViewData["Msg"] = "Se han cargado los registros exitosamente!";
                    }
                    else
                    {
                        ViewData["rta"] = 2;
                        ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                    }
                }
                else
                {
                    ViewData["rta"] = 2;
                    ViewData["exitoso"] = 0;
                    ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + MsgRes.CodeError;
                }
            }
            catch (Exception ex)
            {
                ViewData["exitoso"] = 0;
                ViewData["rta"] = 2;
                ViewData["Msg"] = MsgRes.DescriptionResponse + " .....Codigo: " + ex.Message;
            }
            return View();
        }

        private List<ver_puntoDispensacion> VerificacionExcel(DataTable dt, ref MessageResponseOBJ MsgRes)
        {
            List<ver_puntoDispensacion> result = new List<ver_puntoDispensacion>();

            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    ver_puntoDispensacion obj = new ver_puntoDispensacion();
                    fila++;
                    if (!string.IsNullOrEmpty(item[0].ToString()))
                    {
                        var texto = "";

                        columna = "NIT PRESTADOR";
                        texto = Convert.ToString(item[0]);

                        if (texto.Length <= 30)
                        {
                            obj.nit_prestador = Convert.ToString(item[0]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 30 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE PRESTADOR";
                        texto = Convert.ToString(item[1]);

                        if (texto.Length <= 200)
                        {
                            obj.nombre_prestador = Convert.ToString(item[1]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DIRECCION PUNTO DE DISPENSACION";
                        texto = Convert.ToString(item[2]);

                        if (texto.Length <= 200)
                        {
                            obj.direccion_punto_dispensacion = Convert.ToString(item[2]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CONTACTO TELEFONICO";
                        texto = Convert.ToString(item[3]);

                        if (texto.Length <= 50)
                        {
                            obj.contacto_telefonico = Convert.ToString(item[3]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD";
                        texto = Convert.ToString(item[4]);

                        if (texto.Length <= 50)
                        {
                            obj.ciudad = Convert.ToString(item[4]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL";
                        texto = Convert.ToString(item[5]);

                        if (texto.Length <= 50)
                        {
                            obj.regional = Convert.ToString(item[5]).ToUpper();

                            List<Ref_regional> regionales = new List<Ref_regional>();
                            regionales = BusClass.listadoRegionalesIndice(obj.regional);

                            if (regionales.Count() == 0)
                            {
                                throw new Exception("No existe este indice en las regionales");
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "UNIS";
                        texto = Convert.ToString(item[6]);
                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item[6]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE FARMACIA";
                        texto = Convert.ToString(item[7]);

                        if (texto.Length <= 100)
                        {
                            obj.nombre_farmacia = Convert.ToString(item[7]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE VERIFICACION O VISITA";
                        obj.fecha_verificacionOvisita = Convert.ToDateTime(item[8]);

                        columna = "Auditor Asignado (Usuario SAMI)";
                        texto = Convert.ToString(item["Auditor Asignado (Usuario SAMI)"]);

                        if (texto.Length <= 50)
                        {
                            obj.auditor_asignado = Convert.ToString(item[9]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        obj.usuario_cargue = SesionVar.UserName;
                        obj.fecha_cargue = DateTime.Now;


                        result.Add(obj);
                        obj = new ver_puntoDispensacion();
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

            return result;
        }

        public ActionResult TableroPuntoDispensacion(string regional, DateTime? fechaIni, DateTime? fechaFin, string prestador, int? nitprestador, string msg, int? tipo)
        {
            ViewBag.regional = BusClass.GetRefRegion();
            List<management_dispensacion_evaluacionRelacionResult> dispensacion = new List<management_dispensacion_evaluacionRelacionResult>();
            List<management_dispensacion_evaluacionRelacionResult> dispensacionTotal = new List<management_dispensacion_evaluacionRelacionResult>();
            List<management_dispensacion_evaluacionRelacionResult> dispensacionPendiente = new List<management_dispensacion_evaluacionRelacionResult>();
            List<management_dispensacion_evaluacionRelacionResult> dispensacionEvaluadas = new List<management_dispensacion_evaluacionRelacionResult>();
            List<management_dispensacion_evaluacionRelacionResult> dispensacionContruccion = new List<management_dispensacion_evaluacionRelacionResult>();

            List<sis_auditor_regional> regionalPropia = BusClass.listadoRegionalesUsuario(SesionVar.IDUser);

            try
            {
                dispensacion = BusClass.getDispensacionEvaluacion().OrderByDescending(x => x.id_evaluacion).ToList();

                if (SesionVar.ROL != "1")
                {
                    foreach (var reg in regionalPropia)
                    {
                        List<management_dispensacion_evaluacionRelacionResult> dispensacionparcial = new List<management_dispensacion_evaluacionRelacionResult>();

                        Ref_regional regi = BusClass.GetRefRegionId((int)reg.id_regional);

                        dispensacionparcial = regi != null ? dispensacion.Where(x => x.regional.Contains(regi.indice)).ToList() : dispensacionparcial;
                        dispensacionTotal.AddRange(dispensacionparcial);
                    }
                }
                else
                {
                    dispensacionTotal.AddRange(dispensacion);
                }

                if (!string.IsNullOrEmpty(regional))
                {
                    dispensacionTotal = dispensacionTotal.Where(x => x.regional == regional).ToList();
                }

                if (fechaIni != null)
                {
                    dispensacionTotal = dispensacionTotal.Where(x => x.fechaVisita >= fechaIni).ToList();
                }

                if (fechaFin != null)
                {
                    dispensacionTotal = dispensacionTotal.Where(x => x.fechaVisita <= fechaFin).ToList();
                }

                if (!string.IsNullOrEmpty(prestador))
                {
                    dispensacionTotal = dispensacionTotal.Where(x => x.nombre_prestador.Contains(prestador)).ToList();
                }

                if (nitprestador != null)
                {
                    dispensacionTotal = dispensacionTotal.Where(x => x.nit_prestador.Contains(Convert.ToString(nitprestador))).ToList();
                }

                dispensacionPendiente = dispensacionTotal.Where(x => x.id_evaluacion == null).ToList();
                dispensacionEvaluadas = dispensacionTotal.Where(x => x.id_evaluacion != null && x.estado == 1).ToList();
                dispensacionContruccion = dispensacionTotal.Where(x => x.estado == 2).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.conteoPendiente = dispensacionPendiente.Count();
            ViewBag.conteoEvaluadas = dispensacionEvaluadas.Count();
            ViewBag.conteoContruccion = dispensacionContruccion.Count();

            ViewBag.listaPendiente = dispensacionPendiente;
            ViewBag.listaEvaluadas = dispensacionEvaluadas;
            ViewBag.listaContruccion = dispensacionContruccion;

            return View();
        }

        public PartialViewResult _modalEvaluacionRepositorio(int? idEvaluacion)
        {
            try
            {
                List<management_dispensacion_archivosRepositorioResult> lista = new List<management_dispensacion_archivosRepositorioResult>();
                lista = BusClass.MostrarArchivosEvaluacionVisitasMD(idEvaluacion);

                var conteo = lista.Count();
                ViewBag.conteo = conteo;
                ViewBag.lista = lista;
                ViewBag.idEvaluacion = idEvaluacion;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();
        }

        public JsonResult EliminarArchivoEvaluacion(int idEvaluacion, int idArchivo)
        {
            var mensaje = "";

            try
            {
                var respuesta = BusClass.EliminarArchivosEvaluacionVisitas(idEvaluacion, idArchivo);
                if (respuesta == 1)
                {
                    mensaje = "EL ARCHIVO HA SIDO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "NO SE PUDO ELIMINAR EL ARCHIVO.";

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE PUDO ELIMINAR EL ARCHIVO.";
            }



            return Json(new { msg = mensaje }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult DescargarArchivo(int idArchivo)
        {
            ver_evaluacion_archivos obj = new ver_evaluacion_archivos();

            try
            {
                obj = BusClass.DescargarArchivoEvaluacionVisitas(idArchivo);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta_imagen);
                WebClient User = new WebClient();
                obj.ruta_imagen = dirpath;
                string filename = obj.ruta_imagen;

                var tipo = obj.extension;

                Byte[] FileBuffer = User.DownloadData(filename);

                if (FileBuffer != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = tipo;
                    //Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public PartialViewResult _ModalGestion(int idVerificacion)
        {
            ViewBag.idVerificacion = idVerificacion;

            return PartialView();
        }

        public ActionResult VerArchivosPdfVisitas(int idEvaluacion)
        {
            ver_evaluacion_pdfs obj = new ver_evaluacion_pdfs();
            try
            {
                obj = BusClass.TraerPDFEvaluacionVisitas(idEvaluacion);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                WebClient User = new WebClient();
                obj.ruta = dirpath;
                string filename = obj.ruta;

                var tipo = obj.extension;

                Byte[] FileBuffer = User.DownloadData(filename);

                if (FileBuffer != null)
                {

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = tipo;
                    //Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        public JsonResult MotificarPuntoDspensacion(int idVerificacion, int motivo, DateTime? fecha, string auditor, string observacion)
        {
            var mensaje = "";
            var motivoTotal = "";

            switch (motivo)
            {
                case 1:
                    motivoTotal = "PRESTADOR NO ESTA DISPONIBLE PARA ATENDER VISITA";
                    break;
                case 2:
                    motivoTotal = "CONTRATO FINALIZADO";
                    break;

                case 3:
                    motivoTotal = "REPROGRAMADA";

                    break;
                case 4:
                    motivoTotal = "OTRO";
                    break;
            }

            try
            {
                ver_puntoDispensacion obj = new ver_puntoDispensacion();
                obj.id_cargue_verificacion = idVerificacion;
                obj.motivo = motivoTotal;
                obj.fecha_nuevavisita = fecha;
                obj.auditor_nuevo = auditor;
                obj.observacion = observacion;
                obj.reasignacion = "SI";
                var respuesta = Model.ActualizarPuntoDispensacion(obj);

                if (respuesta != 0)
                {
                    mensaje = "VISITA MODIFICADA CORRECTAMENTE.";
                    return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "ERROR AL GENERAR LA VISITA.";
                    return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EvaluacionDispensacion(int? idPunto, int? tipo, int? idEvaluacion)
        {
            AsaludEcopetrol.Models.Verificacion.VerificacionFarmaceutica Modelo = new AsaludEcopetrol.Models.Verificacion.VerificacionFarmaceutica();

            try
            {
                List<ref_verificacion_farmaceutico> titulos = Modelo.Get_refVerificacionFarmaceutita();
                management_dispensacion_evaluacionRelacion_totalIdResult TipicaBase = idEvaluacion != null ? Modelo.getDispensacionEvaluacionTotalId((int)idEvaluacion).FirstOrDefault() : new management_dispensacion_evaluacionRelacion_totalIdResult();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> dispensacion = idEvaluacion != null ? Modelo.getDispensacionEvaluacionTotalId((int)idEvaluacion).ToList() : new List<management_dispensacion_evaluacionRelacion_totalIdResult>();

                List<ver_puntoDispensacion> lista = Model.getPuntoDispensacionList().Where(x => x.id_cargue_verificacion == idPunto).ToList();

                List<ref_ver_tipoCriterio> tipoCriterio = Modelo.Get_refTipoCriterio();
                List<ver_tipocriterio> tipoCriterioIntermedio = BusClass.get_ref_tipoCriterioGeneral();

                List<ver_evaluacion_archivos> archivosEva = new List<ver_evaluacion_archivos>();

                if (idEvaluacion != null)
                {
                    archivosEva = BusClass.TraerArchivosVisitasDispensacion((int)idEvaluacion);
                    Model.id_evaluacion = (int)idEvaluacion;
                }

                ViewBag.existe = dispensacion.Count();
                ViewBag.dispenEvalu = dispensacion;
                ViewBag.id_evaluacion = idEvaluacion;

                ViewBag.idDispen = idPunto;
                ViewBag.tipo = tipo;
                ViewBag.lista = lista;
                ViewBag.titulos = titulos;
                ViewBag.conteoTitulos = titulos.Count();
                ViewBag.TipicaBase = TipicaBase;
                ViewBag.tipoCriterio = tipoCriterio;
                ViewBag.tipoCriterioIntermedio = tipoCriterioIntermedio;
                ViewBag.tipoImpacto = BusClass.ListImpactosEvaluacion();

                ViewBag.archivosEva = archivosEva;
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public string DevolverArchivos(int idEvaluacion)
        {
            var resultado = "";
            try
            {
                List<ver_evaluacion_archivos> lista = new List<ver_evaluacion_archivos>();
                lista = BusClass.TraerArchivosVisitasDispensacion(idEvaluacion);
                return resultado;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return resultado;
            }
        }

        public JsonResult GuardarEvaluacionDispensacion(int? idDispen, decimal? total, decimal? totalResultados, decimal? resultadosporcentaje, decimal? recursohumano, 
            decimal? condiciones_locativasDotacion, decimal? procesos_generales, decimal? procesos_especiales, string hallazgos, string observacionesAdi, 
            string arrayRadio, string arrayPeso, string arrayValor, string arrayResultado, string arrayImpacto, string arrayObservaciones, string arrayTitulos, 
            string finalTitulo, string finalSubTitulo, int? id_evaluacion, string nombre_auditado)
        {
            var mensaje = "";

            try
            {
                ver_dispen_evaluacion obj = new ver_dispen_evaluacion();

                obj.id_dispensacion = idDispen;
                obj.total_peso = total;
                obj.total_resultado = totalResultados;
                obj.resultado = resultadosporcentaje;
                obj.recurso_humano = recursohumano;
                obj.condiciones_locativasDotacion = condiciones_locativasDotacion;
                obj.procesos_generales = procesos_generales;
                obj.procesos_especiales = procesos_especiales;
                obj.hallazgos = hallazgos;
                obj.observacion_adicionales = observacionesAdi;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.estado = 1;
                var idEvaluacion = 0;
                if (id_evaluacion != null && id_evaluacion != 0)
                {
                    obj.id_evaluacion = (int)id_evaluacion;
                    idEvaluacion = BusClass.ActualizarVisitaDispensacion(obj);
                }
                else
                {
                    idEvaluacion = BusClass.InsertarEvaluacionDispensacion(obj);
                }

                var finalizado = 0;
                var eliminado = 0;
                var actualizado = 0;
                if (idEvaluacion != 0)
                {
                    List<ver_dispen_evaluacion_total> evaluacionTotal = estructuracion(idEvaluacion, arrayRadio, arrayPeso, arrayValor, arrayResultado, arrayImpacto, arrayObservaciones, finalTitulo, finalSubTitulo);

                    if (evaluacionTotal.Count() != 0)
                    {
                        ver_puntoDispensacion objPD = new ver_puntoDispensacion();
                        objPD.id_cargue_verificacion = (int)idDispen;
                        objPD.nombre_auditado = nombre_auditado;

                        actualizado = BusClass.ActualizarAuditadoVisitasDispensacion(objPD);

                        eliminado = BusClass.EliminarTotalEvaluacion(idEvaluacion);
                        finalizado = BusClass.InsertarEvaluacionDispensacionTotal(evaluacionTotal);
                    }
                }

                mensaje = "EVALUACIÓN REALIZADA CORRECTAMENTE.";
                return Json(new { success = true, message = mensaje, idevaluacion = idEvaluacion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                mensaje = "ERROR EN LA REALIZACIÓN DE LA EVALUACIÓN.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AutoguardadoEvaluacionDispensacion(int? idDispen, decimal? total, decimal? totalResultados, decimal? resultadosporcentaje, 
            decimal? recursohumano, decimal? condiciones_locativasDotacion, decimal? procesos_generales, decimal? procesos_especiales, string hallazgos, 
            string observacionesAdi, string arrayRadio, string arrayPeso, string arrayValor, string arrayResultado, string arrayImpacto, string arrayObservaciones, 
            string arrayTitulos, string finalTitulo, string finalSubTitulo, int? id_evaluacion, string nombre_auditado)
        {
            var mensaje = "";
            var evaluacionExiste = Model.id_evaluacion;

            try
            {
                ver_dispen_evaluacion obj = new ver_dispen_evaluacion();

                obj.id_dispensacion = idDispen;
                obj.total_peso = total;
                obj.total_resultado = totalResultados;
                obj.resultado = resultadosporcentaje;
                obj.recurso_humano = recursohumano;
                obj.condiciones_locativasDotacion = condiciones_locativasDotacion;
                obj.procesos_generales = procesos_generales;
                obj.procesos_especiales = procesos_especiales;
                obj.hallazgos = hallazgos;
                obj.observacion_adicionales = observacionesAdi;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.estado = 2;

                var idEvaluacion = 0;
                if (id_evaluacion != null && id_evaluacion != 0)
                {
                    obj.id_evaluacion = (int)id_evaluacion;
                    idEvaluacion = BusClass.ActualizarVisitaDispensacion(obj);
                }
                else
                {
                    idEvaluacion = BusClass.InsertarEvaluacionDispensacion(obj);
                }

                var finalizado = 0;
                var eliminado = 0;
                var actualizado = 0;

                if (idEvaluacion != 0)
                {
                    List<ver_dispen_evaluacion_total> evaluacionTotal = estructuracion(idEvaluacion, arrayRadio, arrayPeso, arrayValor, arrayResultado, arrayImpacto, arrayObservaciones, finalTitulo, finalSubTitulo);

                    if (evaluacionTotal.Count() != 0)
                    {
                        ver_puntoDispensacion objPD = new ver_puntoDispensacion();
                        objPD.id_cargue_verificacion = (int)idDispen;
                        objPD.nombre_auditado = nombre_auditado;

                        actualizado = BusClass.ActualizarAuditadoVisitasDispensacion(objPD);

                        eliminado = BusClass.EliminarTotalEvaluacion(idEvaluacion);
                        finalizado = BusClass.InsertarEvaluacionDispensacionTotal(evaluacionTotal);
                    }
                }

                mensaje = "EVALUACIÓN REALIZADA CORRECTAMENTE.";
                return Json(new { success = true, message = mensaje, idevaluacion = idEvaluacion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                mensaje = "ERROR EN LA REALIZACIÓN DE LA EVALUACIÓN.";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarVisitaAutoGuardada(int idEvaluacion)
        {
            var mensaje = "";
            try
            {
                var resultado = BusClass.EliminarEvaluacionVisitasAutoguardado((int)idEvaluacion);
                mensaje = "ELIMINADA CORRECTAMENTE.";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR.";
            }

            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarImagenes(List<HttpPostedFileBase> files, int? idEvaluacion, String[] origen, int? tipoIngreso)
        {
            //tipoIngreso) 1: Envio total, 2: Envio automatico parcial
            var mensaje = "";
            var rta = 0;

            try
            {
                if (files != null)
                {
                    HttpPostedFileBase archivo = files[0];
                    String origenes = origen[0];

                    if (tipoIngreso == 2)
                    {

                    }

                    var resultado = IngresoArchivos(archivo, idEvaluacion, origenes);
                }
                else
                {
                    mensaje = "";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        public int IngresoArchivos(HttpPostedFileBase file, int? idEvaluacion, String origen)
        {
            string archivo = "";

            ViewBag.af = false;
            try
            {

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionVisitas"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionVisitas2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);

                var NombreArchivo = RemplazarTextoArchivos(file.FileName);

                string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "Evaluacion" + "\\" + idEvaluacion);

                ver_evaluacion_archivos obj = new ver_evaluacion_archivos();

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                //var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                //archivo = String.Format(ruta,
                //fecha, nombre, Path.GetExtension(file.FileName));

                try
                {
                    origen = origen.Replace("|", "");

                    String[] origenFinal = new string[0];
                    if (origen != null)
                    {
                        origenFinal = origen.Split('.');
                    }


                    string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                    WebClient User = new WebClient();
                    archivo = dirpath2;
                    string filename = archivo;
                    //Byte[] FileBuffer = User.DownloadData(filename);
                    var extension = file.ContentType.ToString();

                    obj.id_evaluacion = idEvaluacion;
                    obj.id_verification = Convert.ToInt32(origenFinal[0]);
                    obj.id_tipoCriterio = Convert.ToInt32(origenFinal[1]);
                    obj.ruta_imagen = Convert.ToString(archivo);
                    obj.extension = extension;
                    obj.nombre = file.FileName;
                    obj.usuario_digita = SesionVar.NombreUsuario;
                    obj.fecha_digita = DateTime.Now;

                }
                catch (Exception ex)
                {
                    var mensaje = ex.Message;
                    return 0;
                }

                var yaExiste = BusClass.BuscarExistenciaArchivo(obj.id_evaluacion, obj.id_tipoCriterio, obj.id_verification, obj.nombre);
                var idRetorno = 0;

                if (yaExiste == null)
                {
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    file.SaveAs(archivo);

                    idRetorno = BusClass.InsertarArchivosEvaluacion(obj);
                }

                return idRetorno;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public string RemplazarTextoArchivos(string nombreArchivo)
        {
            var nombreFinal = "";
            nombreFinal = nombreArchivo.Replace(".Pdf", "");
            nombreFinal = nombreFinal.Replace(".pdf", "");
            nombreFinal = nombreFinal.Replace(".doxc", "");
            nombreFinal = nombreFinal.Replace(".Doxc", "");
            nombreFinal = nombreFinal.Replace(".doc", "");
            nombreFinal = nombreFinal.Replace(".Doc", "");
            nombreFinal = nombreFinal.Replace(".png", "");
            nombreFinal = nombreFinal.Replace(".Png", "");
            nombreFinal = nombreFinal.Replace(".jpg", "");
            nombreFinal = nombreFinal.Replace(".Jpg", "");
            nombreFinal = nombreFinal.Replace(".img", "");
            nombreFinal = nombreFinal.Replace(".Img", "");
            nombreFinal = nombreFinal.Replace(".xlsx", "");
            nombreFinal = nombreFinal.Replace(".Xlsx", "");
            nombreFinal = nombreFinal.Replace(".xls", "");
            nombreFinal = nombreFinal.Replace(".Xls", "");
            nombreFinal = nombreFinal.Replace(".zip", "");
            nombreFinal = nombreFinal.Replace(".Zip", "");
            nombreFinal = nombreFinal.Replace(".rar", "");
            nombreFinal = nombreFinal.Replace(".Rar", "");
            nombreFinal = nombreFinal.Replace(".txt", "");
            nombreFinal = nombreFinal.Replace(".Txt", "");

            return nombreFinal;

        }

        public List<ver_dispen_evaluacion_total> estructuracion(int idEvaluacion, string arrayRadio, string arrayPeso, string arrayValor, string arrayResultado,
            string arrayImpacto, string arrayObservaciones, string finalTitulo, string finalSubTitulo)
        {
            List<ver_dispen_evaluacion_total> lista = new List<ver_dispen_evaluacion_total>();

            try
            {
                string[] finalTituloF = finalTitulo.Split(',');
                string[] finalSubTituloF = finalSubTitulo.Split(',');
                string[] arrayRadioF = arrayRadio.Split('|');
                string[] arrayPesoF = arrayPeso.Split(',');

                string[] arrayValorF = arrayValor.Split(',');
                string[] arrayResultadoF = arrayResultado.Split(',');
                string[] arrayImpactoF = arrayImpacto.Split('|');
                string[] arrayObservacionesF = arrayObservaciones.Split('|');

                var count = 0;
                var variableConteo = 0;

                foreach (var item in finalTituloF)
                {
                    variableConteo = 0;

                    var idTitulo = finalTituloF[count];
                    List<ver_tipocriterio> listaCompara = BusClass.get_ref_tipoCriterio(int.Parse(idTitulo));

                    if (finalTituloF.Count() > count)
                    {
                        var count2 = 0;
                        foreach (var item2 in finalSubTituloF)
                        {
                            var idTipoCriterio = finalSubTituloF[count2];

                            if (finalSubTituloF.Count() > count2)
                            {
                                if (listaCompara.Count() != 0)
                                {
                                    foreach (var item3 in listaCompara)
                                    {
                                        ver_dispen_evaluacion_total obj = new ver_dispen_evaluacion_total();
                                        var idVerificacion = item3.id_tipo_verificacion;
                                        var idCriterio = item3.id_tipo_criterio;

                                        if (idVerificacion == int.Parse(idTitulo))
                                        {
                                            if (idCriterio == int.Parse(idTipoCriterio))
                                            {

                                                var conteo = lista.Where(x => x.id_tipoCriterio == idCriterio && x.id_verification == idVerificacion).ToList();
                                                if (conteo.Count() == 0)
                                                {
                                                    var observacionTotal = arrayObservacionesF[variableConteo];
                                                    observacionTotal = observacionTotal.Replace(",", "");

                                                    obj.id_evaluacion = idEvaluacion;
                                                    obj.id_verification = int.Parse(idTitulo);
                                                    obj.id_tipoCriterio = int.Parse(finalSubTituloF[variableConteo]);

                                                    var pesoTotal = arrayPesoF[variableConteo] ?? string.Empty; // Si es null, asigna una cadena vacía

                                                    pesoTotal = pesoTotal.Replace("[", "")
                                                                         .Replace("]", "")
                                                                         .Replace("\"", "");

                                                    pesoTotal = pesoTotal.Replace("null", null);

                                                    obj.peso = !string.IsNullOrEmpty(pesoTotal) ? Convert.ToDecimal(pesoTotal) : 0;

                                                    var valorTotal = arrayValorF[variableConteo];
                                                    valorTotal = valorTotal.Replace("[", "");
                                                    valorTotal = valorTotal.Replace("]", "");
                                                    valorTotal = valorTotal.Replace("\"", "");

                                                    if (valorTotal != "" && valorTotal != null && valorTotal != "null")
                                                    {
                                                        obj.valor = Convert.ToDecimal(valorTotal);
                                                    }
                                                    else
                                                    {
                                                        obj.valor = null;
                                                    }

                                                    var resultadoFinal = arrayResultadoF[variableConteo] ?? string.Empty; // Si es null, asigna una cadena vacía

                                                    resultadoFinal = resultadoFinal.Replace("[", "")
                                                                         .Replace("]", "")
                                                                         .Replace("\"", "");

                                                    resultadoFinal = resultadoFinal.Replace("null", null);

                                                    obj.resultado = !string.IsNullOrEmpty(resultadoFinal) ? Convert.ToDecimal(resultadoFinal) : 0;

                                                    var radioTotal = arrayRadioF[variableConteo];
                                                    radioTotal = radioTotal.Replace(",", "");
                                                    radioTotal = radioTotal.Replace("[", "");
                                                    radioTotal = radioTotal.Replace("\"", "");
                                                    radioTotal = radioTotal.Replace("]", "");

                                                    obj.decision = radioTotal;

                                                    var impactoTotal = arrayImpactoF[variableConteo];

                                                    impactoTotal = impactoTotal.Replace(",", "");
                                                    impactoTotal = impactoTotal.Replace("[", "");
                                                    impactoTotal = impactoTotal.Replace("]", "");
                                                    impactoTotal = impactoTotal.Replace("\"", "");

                                                    if (impactoTotal != "" && impactoTotal != null && impactoTotal != "null")
                                                    {
                                                        obj.impacto = Convert.ToInt32(impactoTotal);
                                                    }
                                                    else
                                                    {
                                                        obj.impacto = null;
                                                    }

                                                    observacionTotal = observacionTotal.Replace("[", "");
                                                    observacionTotal = observacionTotal.Replace("]", "");
                                                    observacionTotal = observacionTotal.Replace("\"", "");

                                                    obj.observaciones = observacionTotal;
                                                    obj.estado = 1;

                                                    obj.fecha_digita = DateTime.Now;
                                                    obj.usuario_digita = SesionVar.UserName;
                                                    lista.Add(obj);
                                                }
                                            }
                                        }

                                    }
                                }
                                variableConteo++;
                            }
                            count2++;
                        }
                    }
                    count++;
                }

                return lista;
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return lista;
            }
        }

        public void ExcelExportarIndividual(int id)
        {
            try
            {
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listareporte = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                listareporte = Model.getDispensacionEvaluacionTotalId((int)id);

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Evaluacion");

                Sheet.Cells["A1:AJ1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(12, 64, 102);
                Sheet.Cells["A1:AJ1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AJ1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AJ1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AJ1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "ÍTEM";
                Sheet.Cells["B1"].Value = "EVALUACION";
                Sheet.Cells["C1"].Value = "VERIFICACION";
                Sheet.Cells["D1"].Value = "TIPO CRITERIO";
                Sheet.Cells["E1"].Value = "DECISION";
                Sheet.Cells["F1"].Value = "PESO";
                Sheet.Cells["G1"].Value = "VALOR";
                Sheet.Cells["H1"].Value = "RESULTADO RESULTADO TOTAL";
                Sheet.Cells["I1"].Value = "IMPACTO";
                Sheet.Cells["J1"].Value = "OBSERVACIONES";
                Sheet.Cells["K1"].Value = "ID EVALUACION";
                Sheet.Cells["L1"].Value = "ID DISPENSACION";
                Sheet.Cells["M1"].Value = "TOTAL PESO";
                Sheet.Cells["N1"].Value = "TOTAL RESULTADO";
                Sheet.Cells["O1"].Value = "RESULTADO";
                Sheet.Cells["P1"].Value = "RECURSO HUMANO";
                Sheet.Cells["Q1"].Value = "CONDICIONES LOCATIVAS Y DOTACIÓN";
                Sheet.Cells["R1"].Value = "PROCESOS GENERALES";
                Sheet.Cells["S1"].Value = "PROCESOS ESPECIALES";
                Sheet.Cells["T1"].Value = "HALLAZGOS";
                Sheet.Cells["U1"].Value = "OBSERVACION ADICIONALES";
                Sheet.Cells["V1"].Value = "NIT PRESTADOR";
                Sheet.Cells["W1"].Value = "NOMBRE PRESTADOR";
                Sheet.Cells["X1"].Value = "DIRECCION PUNTO DISPENSACION";
                Sheet.Cells["Y1"].Value = "CONTACTO TELEFONICO";
                Sheet.Cells["Z1"].Value = "CIUDAD";
                Sheet.Cells["AA1"].Value = "REGIONAL";
                Sheet.Cells["AB1"].Value = "UNIS";
                Sheet.Cells["AC1"].Value = "NOMBRE FARMACIA";
                Sheet.Cells["AD1"].Value = "FECHA VERIFICACIONOVISITA";
                Sheet.Cells["AE1"].Value = "AUDITOR ASIGNADO";
                Sheet.Cells["AF1"].Value = "REASIGNACION";
                Sheet.Cells["AG1"].Value = "MOTIVO";
                Sheet.Cells["AH1"].Value = "FECHA NUEVAVISITA";
                Sheet.Cells["AI1"].Value = "AUDITOR NUEVO";
                Sheet.Cells["AJ1"].Value = "OBSERVACION";

                int row = 2;
                foreach (management_dispensacion_evaluacionRelacion_totalIdResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.descripcion;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.id_evaluacion_total;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.id_verification;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.nombre_criterio;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.decision;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.peso;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.valor;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.resultado_total;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.impacto;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.observaciones;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.id_evaluacion;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.id_dispensacion;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.total_peso;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.total_resultado;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.resultado;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.recurso_humano;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.condiciones_locativasDotacion;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.procesos_generales;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.procesos_especiales;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.hallazgos;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.observacion_adicionales;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.nit_prestador;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.nombre_prestador;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.direccion_punto_dispensacion;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.contacto_telefonico;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.ciudad;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.regional;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.nombre_farmacia;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.fecha_verificacionOvisita;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.auditor_asignado;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.reasignacion;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.motivo;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = Convert.ToDateTime(item.fecha_nuevavisita);
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.auditor_nuevo;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.observacion;

                    Sheet.Cells[string.Format("AD{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AH{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A" + row + ":AJ1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:AJ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Evaluacion_" + id + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

        }

        public void ExcelExportarTotal()
        {
            try
            {
                List<management_dispensacion_evaluacionRelacionResult> listareporte = Model.getDispensacionEvaluacion();
                listareporte = listareporte.Where(x => x.id_evaluacion != null && x.estado == 1).ToList();

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Evaluadas");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:R1"].Style.Font.Bold = true;
                Sheet.Cells["A1:R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:R1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:R1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:R1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "TRIMESTRE";
                Sheet.Cells["B1"].Value = "FECHA VISITA";
                Sheet.Cells["C1"].Value = "REGIONAL";
                Sheet.Cells["D1"].Value = "UNIS";
                Sheet.Cells["E1"].Value = "LOCALIDAD";
                Sheet.Cells["F1"].Value = "CIUDAD";
                Sheet.Cells["G1"].Value = "PROVEEDOR";
                Sheet.Cells["H1"].Value = "PUNTO SERVICIO FARMACÉUTICO";
                Sheet.Cells["I1"].Value = "DIRECCION";
                Sheet.Cells["J1"].Value = "TELEFONO";
                Sheet.Cells["K1"].Value = "TOTAL RESULTADO OBTENIDO";
                Sheet.Cells["L1"].Value = "TOTAL PESO REQUISITOS EVALUADOS";
                Sheet.Cells["M1"].Value = "% CALIFICACIÓN RESULTADO TOTAL";
                Sheet.Cells["N1"].Value = "% CALIFICACIÓN CONDICIONES LOCATIVAS Y DOTACIÓN";
                Sheet.Cells["O1"].Value = "% CALIFICACIÓN RECURSO HUMANO";
                Sheet.Cells["P1"].Value = "% CALIFICACIÓN PROCESOS GENERALES";
                Sheet.Cells["Q1"].Value = "% CALIFICACIÓN PROCESOS ESPECIALES";
                Sheet.Cells["R1"].Value = "CUMPLIMIENTO";

                int row = 2;
                foreach (management_dispensacion_evaluacionRelacionResult item in listareporte)
                {
                    var cumplimiento = "";

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.trimestre;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.fechaVisita;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.regional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.ciudad;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.nit_prestador + item.nombre_prestador;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.nit_prestador + item.nombre_prestador;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.direccion_punto_dispensacion;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.contacto_telefonico;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.total_resultado;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.total_peso;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.resultado;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.condiciones_locativasDotacion;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.recurso_humano;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.procesos_generales;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.procesos_especiales;


                    if (item.resultado >= 90)
                    {
                        cumplimiento = "Cumplimiento";
                    }
                    else if (item.resultado >= 70 && item.resultado < 90)
                    {
                        cumplimiento = "Cumplimiento parcial";
                    }
                    else
                    {
                        cumplimiento = "incumplimiento";
                    }

                    Sheet.Cells[string.Format("R{0}", row)].Value = cumplimiento;

                    Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("N{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("P{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("Q{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = "#0\\.00%";

                    row++;
                }

                Sheet.Cells["A" + row + ":R1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:R"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=VisitasEvaluadas_" + DateTime.Now + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void TraerPdf(int? idEvaluacion)
        {
            try
            {
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaTotal = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaRRHH = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaCondiciones = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaGenerales = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaProcesos = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();

                listaTotal = Model.getDispensacionEvaluacionTotalId((int)idEvaluacion);

                List<management_dispensacion_evaluacionRelacionResult> listaParcial = new List<management_dispensacion_evaluacionRelacionResult>();
                listaParcial = Model.getDispensacionEvaluacionId((int)idEvaluacion);

                listaRRHH = listaTotal.Where(x => x.descripcion.Contains("RECURSO")).ToList();
                listaCondiciones = listaTotal.Where(x => x.descripcion.Contains("CONDICION")).ToList();
                listaGenerales = listaTotal.Where(x => x.descripcion.Contains("GENERAL")).ToList();
                listaProcesos = listaTotal.Where(x => x.descripcion.Contains("ESPECIAL")).ToList();

                var pesoRRHH = 0;
                var pesoFD = 0;
                var pesoPP = 0;

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteEvaluacionVisitas.rdlc");
                Microsoft.Reporting.WebForms.ReportDataSource ltotal = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotal", listaTotal);
                Microsoft.Reporting.WebForms.ReportDataSource lparcial = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionParcial", listaParcial);

                Microsoft.Reporting.WebForms.ReportDataSource LRRHH = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalRRHH", listaRRHH);
                Microsoft.Reporting.WebForms.ReportDataSource LC = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLC", listaCondiciones);
                Microsoft.Reporting.WebForms.ReportDataSource LG = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLG", listaGenerales);
                Microsoft.Reporting.WebForms.ReportDataSource LP = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLP", listaProcesos);
                //ReportParameter nomcohorte = new ReportParameter("nomtipocohorte", nom);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //viewer.LocalReport.EnableExternalImages = true;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(ltotal);
                viewer.LocalReport.DataSources.Add(lparcial);
                viewer.LocalReport.DataSources.Add(LRRHH);
                viewer.LocalReport.DataSources.Add(LC);
                viewer.LocalReport.DataSources.Add(LG);
                viewer.LocalReport.DataSources.Add(LP);
                //viewer.LocalReport.SetParameters(nomcohorte);

                if (listaTotal.Count != 0)
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
                        byte[] pdfContent = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        //RETORNO PDF
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", pdfContent.Length.ToString());
                        Response.BinaryWrite(pdfContent);
                        Response.Flush();
                        GuardarPdf(pdfContent, (int)idEvaluacion);
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
                var error = ex.Message;
            }
        }

        public void PdfEvaluacionVisitas(int? idEvaluacion)
        {
            try
            {
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaTotal = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaRRHH = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaCondiciones = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaGenerales = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
                List<management_dispensacion_evaluacionRelacion_totalIdResult> listaProcesos = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();

                listaTotal = Model.getDispensacionEvaluacionTotalId((int)idEvaluacion);

                List<management_dispensacion_evaluacionRelacionResult> listaParcial = new List<management_dispensacion_evaluacionRelacionResult>();
                listaParcial = Model.getDispensacionEvaluacionId((int)idEvaluacion);

                listaRRHH = listaTotal.Where(x => x.descripcion.Contains("RECURSO")).ToList();
                listaCondiciones = listaTotal.Where(x => x.descripcion.Contains("CONDICION")).ToList();
                listaGenerales = listaTotal.Where(x => x.descripcion.Contains("GENERAL")).ToList();
                listaProcesos = listaTotal.Where(x => x.descripcion.Contains("ESPECIAL")).ToList();

                var pesoRRHH = 0;
                var pesoFD = 0;
                var pesoPP = 0;

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                string rPath = Path.Combine(Server.MapPath("~/ReportViewer"), "ReporteEvaluacionVisitas.rdlc");
                Microsoft.Reporting.WebForms.ReportDataSource ltotal = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotal", listaTotal);
                Microsoft.Reporting.WebForms.ReportDataSource lparcial = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionParcial", listaParcial);

                Microsoft.Reporting.WebForms.ReportDataSource LRRHH = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalRRHH", listaRRHH);
                Microsoft.Reporting.WebForms.ReportDataSource LC = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLC", listaCondiciones);
                Microsoft.Reporting.WebForms.ReportDataSource LG = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLG", listaGenerales);
                Microsoft.Reporting.WebForms.ReportDataSource LP = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluacionTotalLP", listaProcesos);
                //ReportParameter nomcohorte = new ReportParameter("nomtipocohorte", nom);

                // SE CREA REPORTE Y SE ASIGNAN PARAMETROS        
                Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //viewer.LocalReport.EnableExternalImages = true;

                viewer.LocalReport.ReportPath = rPath;
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(ltotal);
                viewer.LocalReport.DataSources.Add(lparcial);
                viewer.LocalReport.DataSources.Add(LRRHH);
                viewer.LocalReport.DataSources.Add(LC);
                viewer.LocalReport.DataSources.Add(LG);
                viewer.LocalReport.DataSources.Add(LP);
                //viewer.LocalReport.SetParameters(nomcohorte);

                if (listaTotal.Count != 0)
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
                        byte[] pdfContent = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                        //RETORNO PDF
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", pdfContent.Length.ToString());
                        Response.BinaryWrite(pdfContent);
                        Response.Flush();
                        GuardarPdf(pdfContent, (int)idEvaluacion);
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
                var error = ex.Message;
            }
        }

        public void GuardarPdf(byte[] bytes, int idEvaluacion)
        {
            try
            {
                byte[] array = new byte[0];
                array = bytes;
                array = array.ToArray();

                HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, "application/pdf", "Evaluacion_" + idEvaluacion + ".pdf");

                var resultado = 0;
                if (sigFile != null)
                {
                    var eliminar = BusClass.EliminarArchivosPDFevaluacionDispensacion((int)idEvaluacion);
                    resultado = GuardarPDFEvaluacion(sigFile, idEvaluacion);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
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

        public int GuardarPDFEvaluacion(HttpPostedFileBase file, int? idEvaluacion)
        {
            string archivo = "";

            ViewBag.af = false;
            try
            {

                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionPDF"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionPDF"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "Evaluacion" + "\\" + "nro_" + idEvaluacion);

                ver_evaluacion_pdfs obj = new ver_evaluacion_pdfs();

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                try
                {
                    file.SaveAs(archivo);

                    string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                    WebClient User = new WebClient();
                    archivo = dirpath2;
                    string filename = archivo;
                    Byte[] FileBuffer = User.DownloadData(filename);
                    var extension = file.ContentType.ToString();

                    obj.id_evaluacion = idEvaluacion;
                    obj.ruta = Convert.ToString(archivo);
                    obj.extension = extension;
                    obj.nombre = nombre;
                    obj.usuario_digita = SesionVar.NombreUsuario;
                    obj.fecha_digita = DateTime.Now;
                }
                catch (Exception ex)
                {
                    var mensaje = ex.Message;
                    return 0;
                }

                var retorno = BusClass.InsertarArchivosEvaluacionPDFS(obj);
                return retorno;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public JsonResult Buscar_codigoPrestador()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 0)
                {
                    List<ver_puntoDispensacion> prestadores = BusClass.getPuntoDispensacionList();
                    prestadores = prestadores.Where(x => x.nombre_prestador.ToUpper().Contains(term.ToUpper()) || Convert.ToString(x.nit_prestador).ToUpper().Contains(term.ToUpper())).ToList();

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

        public JsonResult Buscar_NombrePrestador()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 0)
                {

                    List<ver_puntoDispensacion> prestadores = BusClass.getPuntoDispensacionList();
                    prestadores = prestadores.Where(x => x.nombre_prestador.ToUpper().Contains(term.ToUpper()) || Convert.ToString(x.nit_prestador).ToUpper().Contains(term.ToUpper())).ToList();

                    var lista = (from reg in prestadores
                                 select new
                                 {
                                     nombre = reg.nombre_prestador,
                                     label = reg.nombre_prestador,
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

        public ActionResult tableroHallazgosVisitas(string regional, DateTime? fechaIni, DateTime? fechaFin, string prestador, int? nitprestador, string msg)
        {
            ViewBag.regional = BusClass.GetRefRegion();

            List<management_dispensacion_evaluacionRelacion_hallazgoResult> hallazgo = new List<management_dispensacion_evaluacionRelacion_hallazgoResult>();

            try
            {

                hallazgo = Model.getEvolucionHallazgos();
                hallazgo = hallazgo.Where(x => x.SinEvaluar == 1).ToList();

                if (!string.IsNullOrEmpty(regional))
                {
                    hallazgo = hallazgo.Where(x => x.regional == regional).ToList();
                }

                if (fechaIni != null)
                {
                    hallazgo = hallazgo.Where(x => x.fechaVisita >= fechaIni).ToList();
                }

                if (fechaFin != null)
                {
                    hallazgo = hallazgo.Where(x => x.fechaVisita <= fechaFin).ToList();
                }

                if (!string.IsNullOrEmpty(prestador))
                {
                    hallazgo = hallazgo.Where(x => x.nombre_prestador.Contains(prestador)).ToList();
                }

                if (nitprestador != null)
                {
                    hallazgo = hallazgo.Where(x => x.nit_prestador.Contains(Convert.ToString(nitprestador))).ToList();
                }

                ViewBag.dispenEvalu = hallazgo;

                ViewBag.total = hallazgo.Count();

                ViewBag.alerta = "";

                if (!string.IsNullOrEmpty(msg))
                {
                    ViewBag.alerta = msg;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        public PartialViewResult _VISITASCERRADAS()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            return PartialView();
        }

        public string _VISITASCERRADASDatos(string regionalM, DateTime? fechaIniM, DateTime? fechaFinM, string prestadorM, int? nitprestadorM, int? tipo)
        {
            string result = "";
            var conteo = 0;

            try
            {
                List<management_dispensacion_evaluacionRelacion_hallazgoResult> hallazgo = new List<management_dispensacion_evaluacionRelacion_hallazgoResult>();

                hallazgo = Model.getEvolucionHallazgos().Where(x => x.SinEvaluar == 0).ToList();

                if (!string.IsNullOrEmpty(regionalM))
                {
                    hallazgo = hallazgo.Where(x => x.regional == regionalM).ToList();
                }

                if (fechaIniM != null)
                {
                    hallazgo = hallazgo.Where(x => x.fechaVisita >= fechaIniM).ToList();
                }

                if (fechaFinM != null)
                {
                    hallazgo = hallazgo.Where(x => x.fechaVisita <= fechaFinM).ToList();
                }

                if (!string.IsNullOrEmpty(prestadorM))
                {
                    hallazgo = hallazgo.Where(x => x.nombre_prestador.Contains(prestadorM)).ToList();
                }

                if (nitprestadorM != null)
                {
                    hallazgo = hallazgo.Where(x => x.nit_prestador == Convert.ToString(nitprestadorM)).ToList();
                }

                conteo = hallazgo.Count();
                if (conteo > 0)
                {
                    foreach (var item in hallazgo)
                    {
                        result += "<tr>";
                        result += "<td>" + item.id_evaluacion + "</td>";
                        result += "<td>" + item.nit_prestador + "-" + item.nombre_prestador + "</td>";
                        result += "<td>" + item.total_peso + "</td>";
                        result += "<td>" + item.total_resultado + "</td>";
                        result += "<td>" + item.resultado + "</td>";
                        result += "<td>" + item.fechaVisita + "</td>";
                        result += "<td>" + item.recurso_humano + "</td>";
                        result += "<td>" + item.condiciones_locativasDotacion + "</td>";
                        result += "<td>" + item.procesos_generales + "</td>";
                        result += "<td>" + item.procesos_especiales + "</td>";
                        result += "<td>  <a class='button_Asalud_Aceptar' onclick='validacion(" + item.id_evaluacion + "," + 2 + ");'>Ver</a></td>";
                        result += "</tr>";
                    }
                }
                else
                {
                    if (tipo == 2)
                    {
                        result += "<tr>";
                        result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                        result += "<label>Use los filtros para buscar registros.</label>";
                        result += "</td>";
                        result += "</tr>";
                    }
                    else
                    {
                        result += "<tr>";
                        result += "<td colspan='12' style='text-align:center; font-size:15px;'>";
                        result += "<label>No hay hallazgos subsanados.</label>";
                        result += "</td>";
                        result += "</tr>";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result + "|" + conteo;
        }

        public PartialViewResult _modalHallazgos(int idEvaluacion, int? tipo)
        {
            try
            {
                List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> list = new List<management_dispensacion_evaluacionRelacion_total_hallazgoResult>();
                list = Model.getDispensacionEvaluacionTotalIdHallazgoId(idEvaluacion);

                if (tipo == 1)
                {
                    list = list.Where(x => x.valor == 0 && x.decision.Contains("si")).ToList();
                }
                else
                {
                    list = list.Where(x => x.valor == 0 && x.decision.Contains("si")).ToList();
                }

                ViewBag.list = list;
                var conteo = list.Count();

                ViewBag.total = conteo;
                ViewBag.tipo = tipo;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();
        }

        public PartialViewResult _modalHallazgosCerrar(int idTotal, int tipoCriterio)
        {
            try
            {


                ViewBag.idTotal = idTotal;
                ViewBag.tipoCriterio = tipoCriterio;

                ViewBag.tipoHallazgo = Model.getTipoHallazgoEvaluacion();
                ViewBag.estado = Model.getEstadosEvaluacionHallazgos();
                ViewBag.cumplimiento = Model.getCumplimientoEvaluacion();
                ViewBag.tipoSoporte = Model.getTipoSoporteEvaluacion();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return PartialView();
        }

        public ActionResult cerrarItemEvaluacion(int? idTotal, int? idTipoCriterio, DateTime? fechaRespuesta, string observacion, int? tipoHallazgo, int? estado, int? cumplimiento,
            int? tipoSoporte, HttpPostedFileBase file, string actividades)
        {
            var mensaje = "";

            try
            {
                var proceso = true;
                var idHallazgo = 0;
                ver_evaluacion_hallazgo obj = new ver_evaluacion_hallazgo();

                obj.id_total = idTotal;
                obj.id_tipoCriterio = idTipoCriterio;
                obj.fecha_respuesta = fechaRespuesta;
                obj.observacion = observacion;
                obj.tipo_hallazgo = tipoHallazgo;
                obj.estado = estado;
                obj.cumplimiento = cumplimiento;
                obj.tipo_soporte = tipoSoporte;
                obj.fecha_digita = DateTime.Now;
                obj.actividades_mejora = actividades;
                obj.usuario_digita = SesionVar.UserName;

                List<ver_evaluacion_hallazgo> listaExistencia = new List<ver_evaluacion_hallazgo>();
                listaExistencia = Model.ExisteHallazgoSubsanado((int)idTotal, (int)idTipoCriterio);

                if (listaExistencia.Count() == 0)
                {
                    idHallazgo = Model.insertarHallazgoItemEvaluacion(obj);
                }
                else
                {
                    idHallazgo = Model.ActualizarHallazgoVisitas(obj);
                }


                ver_evaluacion_hallazgo_archivos obj2 = new ver_evaluacion_hallazgo_archivos();

                if (idHallazgo != 0)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            obj2 = estructuraCargueArchivoHallazgo(file, idHallazgo);

                            if (obj2.ruta_archivo != null)
                            {
                                var idArchivo = Model.insertarHallazgoItemEvaluacionArchivos(obj2);

                                if (idArchivo != 0)
                                {
                                    proceso = true;
                                    mensaje = "SE INGRESO EL HALLAZGO CORRECTAMENTE.";
                                }
                                else
                                {
                                    proceso = false;
                                    mensaje = "NO SE INGRESO EL HALLAZGO CORRECTAMENTE.";
                                }
                            }
                            else
                            {
                                proceso = false;
                                mensaje = "HUBO PROBLEMAS CON EL GUARDADO DE ARCHIVO";
                            }
                        }
                        else
                        {
                            proceso = false;
                            mensaje = "SE INGRESO EL HALLAZGO CORRECTAMENTE.";
                        }
                    }
                    else
                    {
                        proceso = false;
                        mensaje = "SE INGRESO EL HALLAZGO CORRECTAMENTE.";
                    }
                }
                else
                {
                    proceso = false;
                    mensaje = "NO SE INGRESO EL HALLAZGO CORRECTAMENTE.";

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE INGRESO EL HALLAZGO CORRECTAMENTE: " + error;
            }

            return RedirectToAction("tableroHallazgosVisitas", "Verificacion", new
            {
                msg = mensaje
            });
        }

        public ver_evaluacion_hallazgo_archivos estructuraCargueArchivoHallazgo(HttpPostedFileBase file, int? idHallazgo)
        {
            string archivo = "";

            ver_evaluacion_hallazgo_archivos obj = new ver_evaluacion_hallazgo_archivos();

            ViewBag.af = false;
            try
            {
                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionVisitas"];
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionVisitas2"];
                }

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "EvaluacionVisitas" + "\\" + "Hallazgo" + "\\" + idHallazgo);

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                try
                {
                    file.SaveAs(archivo);

                    string dirpath2 = Path.Combine(Request.PhysicalApplicationPath, archivo);
                    WebClient User = new WebClient();
                    archivo = dirpath2;
                    string filename = archivo;
                    Byte[] FileBuffer = User.DownloadData(filename);
                    var extension = file.ContentType.ToString();

                    obj.id_hallazgo = idHallazgo;
                    obj.ruta_archivo = Convert.ToString(archivo);
                    obj.extension = extension;
                    obj.usuario_digita = SesionVar.UserName;
                    obj.fecha_digita = DateTime.Now;
                    return obj;

                }
                catch (Exception ex)
                {
                    var mensaje = ex.Message;
                    return obj;
                }
                return obj;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return obj;
            }
        }

        public void ExcelExportarTotalHallazgo()
        {
            try
            {

                List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> listareporte = Model.getDispensacionEvaluacionTotalHallazgo();
                listareporte = listareporte.Where(x => x.id_evaluacion != null).ToList();
                listareporte = listareporte.Where(x => x.valor == 0 && x.decision.Contains("si")).ToList();

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("EvaluadasHallazgos");

                Sheet.Cells["A1:AC1"].Style.Font.Bold = true;
                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AC1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AC1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AC1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AC1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "TRIMESTRE";
                Sheet.Cells["B1"].Value = "FECHA VISITA";
                Sheet.Cells["C1"].Value = "REGIONAL";
                Sheet.Cells["D1"].Value = "UNIS";
                Sheet.Cells["E1"].Value = "LOCALIDAD";
                Sheet.Cells["F1"].Value = "CIUDAD	";
                Sheet.Cells["G1"].Value = "PROVEEDOR	";
                Sheet.Cells["H1"].Value = "PUNTO SERVICIO FARMACÉUTICO";
                Sheet.Cells["I1"].Value = "DIRECCION";
                Sheet.Cells["J1"].Value = "TELEFONO";
                Sheet.Cells["K1"].Value = "ITEM EVALUADO";
                Sheet.Cells["L1"].Value = "REQUISITO EVALUADO";
                Sheet.Cells["M1"].Value = "HALLAZGO";
                Sheet.Cells["N1"].Value = "CATEGORIA";
                Sheet.Cells["O1"].Value = "IMPACTO";
                Sheet.Cells["P1"].Value = "TIPO DE HALLAZGO";
                Sheet.Cells["Q1"].Value = "DESCRIPCIÓN DEL HALLAZGO";
                Sheet.Cells["R1"].Value = "% CALIFICACIÓN RESULTADO TOTAL";
                Sheet.Cells["S1"].Value = "% CALIFICACIÓN RECURSO HUMANO";
                Sheet.Cells["T1"].Value = "% CALIFICACIÓN CONDICIONES LOCATIVAS";
                Sheet.Cells["U1"].Value = "% CALIFICACIÓN PROCESOS GENERALES";
                Sheet.Cells["V1"].Value = "% CALIFICACIÓN PROCESOS ESPECIALES";
                Sheet.Cells["W1"].Value = "CUMPLIMIENTO";
                Sheet.Cells["X1"].Value = "ACTIVIDADES DE MEJORA";
                Sheet.Cells["Y1"].Value = "SOPORTE";
                Sheet.Cells["Z1"].Value = "FECHA LÍMITE DE RESPUESTA";
                Sheet.Cells["AA1"].Value = "FECHA DE RESPUESTA";
                Sheet.Cells["AB1"].Value = "Observaciones ";
                Sheet.Cells["AC1"].Value = "ESTADO";

                int row = 2;
                foreach (management_dispensacion_evaluacionRelacion_total_hallazgoResult item in listareporte)
                {
                    var cumplimiento = "";

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.trimestre;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.fechaVisita;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.regional;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.unis;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.ciudad;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.nombre_prestador;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.nombre_prestador;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.direccion_punto_dispensacion;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.contacto_telefonico;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.id_tipoCriterio;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.nombre_criterio;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.observaciones;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.categoria;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.impacto;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.tipo_hallazgo_detalle;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.descripcion_tipoHallazgo;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.resultado;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.recurso_humano;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.condiciones_locativasDotacion;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.procesos_especiales;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.procesos_generales;

                    if (item.resultado >= 90)
                    {
                        cumplimiento = "Cumplimiento";
                    }
                    else if (item.resultado >= 70 && item.resultado < 90)
                    {
                        cumplimiento = "Cumplimiento parcial";
                    }
                    else
                    {
                        cumplimiento = "incumplimiento";
                    }

                    Sheet.Cells[string.Format("W{0}", row)].Value = cumplimiento;

                    Sheet.Cells[string.Format("X{0}", row)].Value = item.actividades_mejora;

                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.descripcion_tipoSoporte;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.fecha_respuesta;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.fecha_respuesta;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.observacionHallazgo;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.descripcion_estado;

                    Sheet.Cells[string.Format("B{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("Z{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AA{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells[string.Format("Q{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("R{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("S{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("U{0}", row)].Style.Numberformat.Format = "#0\\.00%";
                    Sheet.Cells[string.Format("V{0}", row)].Style.Numberformat.Format = "#0\\.00%";

                    row++;
                }

                Sheet.Cells["A" + row + ":AC1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:AC"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=VisitasEvaluadasHallazgos.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public ActionResult VerArchivosRepositorio(int idArchivo)
        {
            try
            {

                ver_evaluacion_pdfs obj = BusClass.TraerPDFEvaluacionVisitas(idArchivo);

                string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                WebClient User = new WebClient();
                obj.ruta = dirpath;
                string filename = obj.ruta;

                var tipo = obj.extension;

                Byte[] FileBuffer = User.DownloadData(filename);

                if (FileBuffer != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();

                    Response.ContentType = tipo;
                    //Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }
    }
}