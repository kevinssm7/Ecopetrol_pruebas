using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOPETROL_COMMON.ENUM;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using Rotativa.MVC;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using ECOPETROL_COMMON.UTILOBJECTS;
using System.IO;
using LinqToExcel;
using Rotativa.Core.Options;
using System.Globalization;
using System.Data.Linq;
using Ionic.Zip;
using Aspose.Cells;
using AsaludEcopetrol.Models;
using System.Data;
using System.Configuration;
using System.Net;
using System.Text;

namespace AsaludEcopetrol.Controllers.ProcesosInternos
{
    [SessionExpireFilter]
    public class ProcesosInternosController : Controller
    {
        readonly Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

        private FormCollection DeSerialize(FormCollection formulario)
        {
            FormCollection collection = new FormCollection();
            //un-encode, and add spaces back in
            string querystring = Uri.UnescapeDataString(formulario["formulario"]).Replace("+", " ");
            var split = querystring.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> items = new Dictionary<string, string>();
            foreach (string s in split)
            {
                string text = s.Substring(0, s.IndexOf("="));
                string value = s.Substring(s.IndexOf("=") + 1);

                if (items.Keys.Contains(text))
                    items[text] = items[text] + "," + value;
                else
                    items.Add(text, value);
            }
            foreach (var i in items)
            {
                collection.Add(i.Key, i.Value);
            }
            return collection;
        }
        // GET: ProcesosInternos


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

        string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosEvaluacionPDF"];
        #endregion

        #region VISITAS DE CALIDAD

        /// <summary>
        /// Vista donde se lista las regionales, y los prestadores que pertenecen esos prestadores
        /// </summary>
        /// <returns></returns>
        public ActionResult Regionalprestadores()
        {
            ViewData["listaindiregionales"] = Model.ConsultarIndicadorRegionalList(null, ref MsgRes);
            ViewBag.regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional != 0).ToList();
            return View();
        }

        /// <summary>
        /// Vista para configurar prestadores a una regional
        /// </summary>
        /// <param name="idregional"></param>
        /// <returns></returns>
        public ActionResult ConfiguracionPrestadores(Int32 idregional)
        {
            var listaprestadores = Model.ConsultarIndicadores(null);
            ViewData["listaregprestadores"] = Model.ConsultarIndicadorRegionalList(idregional, ref MsgRes);
            ViewBag.listaprestadores = listaprestadores;
            ViewBag.countprestadores = listaprestadores.Count();
            ViewBag.idregional = idregional;
            return View();
        }

        /// <summary>
        /// Metodo json para guardar los prestadores de una regional
        /// </summary>
        /// <param name="idregional"></param>
        /// <param name="prestadores"></param>
        /// <returns></returns>
        public JsonResult GuardarRegionalPrestadores(Int32 idregional, List<int> prestadores)
        {
            try
            {
                if (prestadores.Count() > 0)
                {
                    Model.insertaRegionalPrestadores(idregional, prestadores);
                }

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Vista donde se listan los tipos de prestadores de una regional
        /// </summary>
        /// <param name="idregional"></param>
        /// <returns></returns>
        public ActionResult TipoPrestadores(Int32 idregional)
        {
            ViewBag.listIndicadores = Model.ConsultarIndicadores(null).Where(l => l.estado == "A").OrderByDescending(l => l.año_vigencia).ToList();
            ViewBag.capituloindicadores = Model.GetCapitulosByIndicador(null, idregional);
            ViewData["listaregprestadores"] = Model.ConsultarIndicadorRegionalList(idregional, ref MsgRes);
            ViewBag.idregional = idregional;
            ViewBag.nomregional = BusClass.GetRefRegion().Single(l => l.id_ref_regional == idregional).nombre_regional;
            return View();
        }

        /// <summary>
        /// Vista para configurar los capitulos de un prestador
        /// </summary>
        /// <param name="idregional"></param>
        /// <param name="idindicador"></param>
        /// <returns></returns>
        public ActionResult ConfiguracionCaptulos(Int32 idregional, Int32 idindicador)
        {
            List<capitulos> capitulosList = Model.GetCapitulosList();
            ViewBag.countprestadores = capitulosList.Count();
            ViewData["Capitulosindicador"] = Model.GetCapitulosByIndicador(idindicador, idregional);
            ViewBag.capitulos = capitulosList;
            ViewBag.idregional = idregional;
            ViewBag.idindicador = idindicador;
            return View();
        }

        /// <summary>
        /// Metodo post para guardar un nuevo capitulo en base de datos
        /// </summary>
        /// <param name="idcapitulo"></param>
        /// <param name="idregional"></param>
        /// <param name="idindicador"></param>
        /// <param name="nomcapitulo"></param>
        /// <param name="pesogeneneral"></param>
        /// <returns></returns>
        public ActionResult GuardarCapitulo(Int32? idcapitulo, Int32 idregional, Int32 idindicador, string nomcapitulo)
        {
            capitulos capitulo = new capitulos();
            capitulo.nom_capitulo = nomcapitulo.ToUpper();
            bool inserto = Model.InsertaCapitulo(capitulo);
            return RedirectToAction("ConfiguracionCaptulos", "ProcesosInternos", new { idregional = idregional, idindicador = idindicador });
        }

        /// <summary>
        /// Metodo post para eliminar un capitulo
        /// </summary>
        /// <param name="idregional"></param>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <returns></returns>
        public ActionResult EliminarCapitulo(Int32 idregional, Int32 idindicador, Int32 idcapitulo)
        {
            try
            {
                Model.EliminarCapitulo(idcapitulo);
                return RedirectToAction("ConfiguracionCaptulos", "ProcesosInternos", new { idregional = idregional, idindicador = idindicador });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo json para asociar los capitulos a un prestador
        /// </summary>
        /// <param name="idregional"></param>
        /// <param name="idindicador"></param>
        /// <param name="capitulos"></param>
        /// <returns></returns>
        public JsonResult GuardarCapitulosPrestador(Int32 idregional, Int32 idindicador, List<int> capitulos)
        {
            try
            {
                if (capitulos.Count() > 0)
                {
                    Model.InsertarCapitulosPrestador(idregional, idindicador, capitulos);
                }

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get de la vista capitulos
        /// </summary>
        /// <param name="idindicador"></param>
        /// <returns></returns>
        public ActionResult Capitulos(Int32 idregional, Int32 idindicador)
        {
            var capitulos = Model.GetCapitulosByIndicador(idindicador, idregional).ToList();
            ViewBag.idregional = idregional;
            ViewBag.idindicador = idindicador;
            ViewBag.listacapitulos = capitulos;
            double pesototal = 0;
            if (capitulos.Count() > 0)
                pesototal = capitulos.Select(l => l.peso_general_capitulo).Sum().Value;

            ViewBag.nameindicador = capitulos.FirstOrDefault().indicadores.nom_indicador;
            ViewBag.nomregional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault().nombre_regional;
            ViewBag.pesogeneral = pesototal;
            ViewData["itemcapitulos"] = Model.Getitemcapbyindcap(idregional, idindicador, null);
            ViewData["Alerta"] = "";
            return View();
        }

        public JsonResult GuardarCofiguracionCapitulo(int idindicador, int idregional, Int32 idcapituloindicador, int peso)
        {
            Model.ActualizarCapituloIndicador(idcapituloindicador, peso);
            var capitulos = Model.GetCapitulosByIndicador(idindicador, idregional).ToList();
            double pesototal = 0;
            if (capitulos.Count() > 0)
                pesototal = capitulos.Select(l => l.peso_general_capitulo).Sum().Value;

            return Json(pesototal, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// vista o pantalla de items o indicadores
        /// </summary>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <param name="rta"></param>
        /// <returns></returns> 
        public ActionResult Indicadores(Int32 idregional, Int32 idindicador, Int32 idcapitulo, int? rta)
        {
            ViewBag.Condiciones = from CondicionesMeta d in Enum.GetValues(typeof(CondicionesMeta))
                                  select new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = Model.GetDescription(d)
                                  };

            var listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).ToList();

            decimal totalpeso = 0;
            if (listaitems.Count() > 0)
                totalpeso = listaitems.Select(l => l.Peso_individual).Sum().Value;

            capitulo_indicador cap = Model.GetCapitulosByIndicador(idindicador, idregional).Where(l => l.capitulo_id == idcapitulo).FirstOrDefault();

            decimal b = (totalpeso % 1);

            if (Convert.ToDouble(b) < 0.5)
            {
                totalpeso = Math.Round(totalpeso);
            }
            else
            {
                totalpeso = Math.Ceiling(totalpeso);
            }

            ViewBag.listaitems = listaitems;
            ViewBag.nomcapitulo = cap.capitulos.nom_capitulo;
            ViewBag.pesocapitulo = cap.peso_general_capitulo;
            ViewBag.totalpeso = totalpeso;
            ViewBag.regional = idregional;
            ViewBag.nomregional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault().nombre_regional;
            ViewBag.idindicador = idindicador;
            ViewBag.nomindicador = Model.ConsultarIndicadores(idindicador).Where(l => l.id_indicador == idindicador).FirstOrDefault().nom_indicador;
            ViewBag.idcapitulo = idcapitulo;
            ViewBag.totalpeso = totalpeso;

            ViewData["Alerta"] = "";

            if (rta != null)
            {
                if (rta == 3)
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("danger", "Transaccion Fallida", "No se pudo habilitar este indicador, ya que con su reintegracion, el peso general seria mayor al 100%");
                }
                else
                if (rta == 2)
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("warning", "Transaccion Exitosa", "Haz inhabilitado uno de los indicadores. para continuar, debes distribuir el peso individual de los demas");
                }
                else
                if (rta == 1)
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("success", "Transaccion Exitosa", "Indicador Actualizado correctamente.");
                }
                else
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("danger", "Transaccion Fallida", "Ha ocurrido un error al actualizar el item.");
                }
            }
            ViewBag.rta = rta;
            return View();
        }

        //leo
        /// <summary>
        /// Metodo post de items
        /// </summary>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <param name="iditem"></param>
        /// <param name="pesoindv"></param>
        /// <param name="condicionmeta"></param>
        /// <param name="vlrmeta"></param>
        /// <param name="txtitem"></param>
        /// <param name="txtdistribucionpesos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Indicadores(Int32 idregional, Int32 idindicador, Int32 idcapitulo, Int32? iditem, string pesoindv, string txtOtrasCondiciones, int? condicionmeta, double? vlrmeta, string txtitem, string[] txtdistribucionpesos)
        {
            var lista = txtdistribucionpesos[0].Split(',').ToList();
            bool act = false;
            int count = 0;

            if (iditem == null)
            {
                List<item_capitulo> listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).OrderBy(l => l.id_item).ToList();
                foreach (string value in lista)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        item_capitulo itemcap = listaitems[count];
                        itemcap.Peso_individual = Convert.ToDecimal(value);
                        act = Model.ActualizarItemCapitulo(itemcap);
                        count++;
                    }
                }

                item_capitulo obj = new item_capitulo();
                obj.indicador_id = idindicador;
                obj.regional_id = idregional;
                obj.capitulo_id = idcapitulo;
                obj.Peso_individual = Convert.ToDecimal(pesoindv);
                if (txtOtrasCondiciones == "SI")
                {
                    obj.condicion_meta = "0";
                    obj.valor_meta = 0;
                    obj.condicion_especial = true;
                }
                else
                {
                    obj.condicion_meta = condicionmeta.ToString();
                    obj.valor_meta = Convert.ToDouble(vlrmeta);
                    obj.condicion_especial = false;
                }

                obj.nom_item = txtitem;
                obj.Valor_digitado = 0;
                obj.activo = true;
                obj.Aplica = true;

                bool rta = Model.InsertarItemCapitulo(obj);
                if (rta)
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("success", "Transaccion Exitosa", "Item Guardado exitosamente.");
                }
                else
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("danger", "Transaccion Fallida", "Ha ocurrido un error al momento de guardar el item.");
                }
            }
            else
            {
                List<item_capitulo> listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).Where(l => l.id_item != iditem).OrderBy(l => l.id_item).ToList();
                foreach (string value in lista)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        item_capitulo itemcap = listaitems[count];
                        itemcap.Peso_individual = Convert.ToDecimal(value);
                        act = Model.ActualizarItemCapitulo(itemcap);
                        count++;
                    }
                }


                item_capitulo objitem = Model.Getitemcapbyid(iditem.Value);
                objitem.indicador_id = idindicador;
                objitem.regional_id = idregional;
                objitem.capitulo_id = idcapitulo;
                objitem.Peso_individual = Convert.ToDecimal(pesoindv);
                if (txtOtrasCondiciones == "SI")
                {
                    objitem.condicion_meta = "0";
                    objitem.valor_meta = Convert.ToDouble(0);
                    objitem.condicion_especial = true;
                }
                else
                {
                    objitem.condicion_meta = condicionmeta.ToString();
                    objitem.valor_meta = Convert.ToDouble(vlrmeta);
                    objitem.condicion_especial = false;
                }

                objitem.nom_item = txtitem;
                objitem.activo = Model.Getitemcapbyid(iditem.Value).activo;


                bool rta = Model.ActualizarItemCapitulo(objitem);
                if (rta)
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("success", "Transaccion Exitosa", "Item actualizado exitosamente.");
                }
                else
                {
                    ViewData["Alerta"] = Model.MsgRestpuesta("danger", "Transaccion Fallida", "Ha ocurrido un error al momento de guardar el item.");
                }

            }


            ViewBag.Condiciones = from CondicionesMeta d in Enum.GetValues(typeof(CondicionesMeta))
                                  select new SelectListItem
                                  {
                                      Value = ((int)d).ToString(),
                                      Text = Model.GetDescription(d)
                                  };

            decimal totalpeso = 0;
            var listaact = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).ToList();
            if (listaact.Count() > 0)
                totalpeso = listaact.Select(l => l.Peso_individual).Sum().Value;

            decimal b = (totalpeso % 1);

            if (Convert.ToDouble(b) < 0.5)
            {
                totalpeso = Math.Round(totalpeso);
            }
            else
            {
                totalpeso = Math.Ceiling(totalpeso);
            }

            capitulo_indicador cap = Model.GetCapitulosByIndicador(idindicador, idregional).Where(l => l.capitulo_id == idcapitulo).FirstOrDefault();
            ViewBag.listaitems = listaact;
            ViewBag.nomcapitulo = cap.capitulos.nom_capitulo;
            ViewBag.pesocapitulo = cap.peso_general_capitulo;
            ViewBag.regional = idregional;
            ViewBag.nomregional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault().nombre_regional;
            ViewBag.idindicador = idindicador;
            ViewBag.nomindicador = Model.ConsultarIndicadores(idindicador).Where(l => l.id_indicador == idindicador).FirstOrDefault().nom_indicador;
            ViewBag.idcapitulo = idcapitulo;
            ViewBag.totalpeso = totalpeso;

            return View();
        }

        /// <summary>
        /// Metodo post para habilitar e inhabilitar los items
        /// </summary>
        /// <param name="IdItem"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public ActionResult HabilitareInhabilitaritem(Int32 idregional, Int32 idindicador, Int32 idcapitulo, Int32 IdItem, bool estado)
        {
            item_capitulo item = Model.Getitemcapbyid(IdItem);
            List<item_capitulo> listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).Where(l => l.activo == true).OrderBy(l => l.id_item).ToList();
            int rtaa = 0;
            bool valido = true;
            try
            {

                if (estado == false)
                {
                    decimal? pesoitemsactivos = listaitems.Select(l => l.Peso_individual).Sum();
                    pesoitemsactivos += item.Peso_individual;

                    if (pesoitemsactivos > 100)
                        valido = false;
                }

                if (valido)
                {
                    if (estado)
                    {
                        item.activo = false;
                        item.Aplica = false;
                        rtaa = 2;
                    }
                    else
                    {
                        item.activo = true;
                        item.Aplica = true;
                        rtaa = 1;
                    }

                    bool rta = Model.ActualizarItemCapitulo(item);
                    return RedirectToAction("Indicadores", "ProcesosInternos", new { idregional = idregional, idindicador = item.indicador_id, idcapitulo = item.capitulo_id, rta = rtaa });
                }
                else
                {
                    return RedirectToAction("Indicadores", "ProcesosInternos", new { idregional = idregional, idindicador = item.indicador_id, idcapitulo = item.capitulo_id, rta = 3 });
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Indicadores", "ProcesosInternos", new { idregional = idregional, idindicador = item.indicador_id, idcapitulo = item.capitulo_id, rta = 4 });
            }
        }

        public JsonResult RedistribuirPesoIndicadores(Int32 idregional, Int32 idindicador, Int32 idcapitulo, string[] txtdistribucionpesos)
        {
            var lista = txtdistribucionpesos[0].Split(',').ToList();
            int count = 0;
            List<item_capitulo> listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).OrderBy(l => l.id_item).ToList();
            foreach (string value in lista)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    item_capitulo itemcap = listaitems[count];
                    itemcap.Peso_individual = Convert.ToDecimal(value);
                    bool act = Model.ActualizarItemCapitulo(itemcap);
                    count++;
                }
            }

            return Json(0);
        }

        /// <summary>
        /// Metodo para retornar los datos de in item
        /// </summary>
        /// <param name="iditem"></param>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <returns></returns>
        public JsonResult GetItemInformacion(Int32? iditem, Int32 idregional, Int32 idindicador, Int32 idcapitulo)
        {
            string result = "";
            int i = 0;

            List<item_capitulo> listaitems = new List<item_capitulo>();
            if (iditem != null)
            {
                listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).Where(l => l.id_item != iditem.Value).OrderBy(l => l.id_item).ToList();
            }
            else
            {
                listaitems = Model.Getitemcapbyindcap(idregional, idindicador, idcapitulo).OrderBy(l => l.id_item).ToList();
            }

            result += "<table style='width:100%' class'table table-bordered table-striped table-condensed'>";
            result += "<tbody>";
            foreach (var obj in listaitems)
            {
                i += 1;
                result += "<tr>";
                result += "<td>" + obj.nom_item + "</td>";
                result += "<td><input type='text' style='max-width:50%' id='txt_" + i + "' class='form-control solo-numero' value='" + obj.Peso_individual + "'/></td>";
                result += "<tr>";
            }
            result += "</tbody></table>";
            if (iditem != null)
            {
                var item = Model.Getitemcapbyid(iditem.Value);
                var data = new
                {
                    idindicador = item.indicador_id,
                    idcapitulo = item.capitulo_id,
                    iditem = item.id_item,
                    nomitem = item.nom_item,
                    pesoindv = item.Peso_individual.ToString(),
                    condicionmeta = item.condicion_meta,
                    valormeta = item.valor_meta,
                    tabla = result,
                    countvalores = i,
                    OtraCondicion = item.condicion_especial
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var data = new
                {
                    tabla = result,
                    countvalores = i
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Metodo para obtener datos de un capitulo
        /// </summary>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <returns></returns>
        public JsonResult GetcapituloInformacion(Int32 idindicador, Int32? idcapitulo)
        {
            string result = "";
            var capitulos = Model.GetCapitulosByIndicador(idindicador, 1).ToList();
            int i = 0;
            if (capitulos.Count() > 0)
            {
                List<capitulo_indicador> lista = new List<capitulo_indicador>();
                if (idcapitulo != null)
                {
                    lista = capitulos.Where(l => l.capitulo_id != idcapitulo).OrderBy(l => l.id_cap_indicador).ToList();
                }
                else
                {
                    lista = capitulos.OrderBy(l => l.id_cap_indicador).ToList();
                }

                result += "<table style='width:100%' class'table table-bordered table-striped'>";
                result += "<tbody>";
                foreach (var obj in lista)
                {
                    i += 1;
                    result += "<tr>";
                    result += "<td>" + obj.capitulos.nom_capitulo + "</td>";
                    result += "<td><input type='text' style='max-width:50%' id='txt_" + i + "' class='form-control solo-numero' value='" + obj.capitulos.peso_general + "'/></td>";
                    result += "<tr>";
                }
                result += "</tbody></table>";
            }


            if (idcapitulo != null)
            {
                var item = Model.Getcapitulobyid(idcapitulo.Value);
                var data = new
                {
                    idcapitulo = item.id_capitulo,
                    pesogen = item.peso_general,
                    nomcapitulo = item.nom_capitulo,
                    tabla = result,
                    contcampos = i,
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new
                {
                    tabla = result,
                    contcampos = i
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Actualizar campo puntuacion en evaluacion de indicadores
        /// </summary>
        /// <param name="value"></param>
        /// <param name="idindicador"></param>
        /// <param name="idcapitulo"></param>
        /// <param name="iditem"></param>
        /// <returns></returns>
        public JsonResult ActualizarValor(string value, Int32 idindicador, Int32 idcapitulo, Int32 iditem, int aplica, int? sap)
        {

            List<item_capitulo> listadoitems = (List<item_capitulo>)Session["itemcapitulo"];
            double valor = Convert.ToDouble(value);

            item_capitulo obj = listadoitems.Where(l => l.id_item == iditem).FirstOrDefault();
            obj.Valor_digitado = valor;
            if (aplica == 0)
            {
                obj.Aplica = false;
            }
            else
            {
                obj.Aplica = true;
            }

            ViewBag.sap = sap;

            Session["itemcapitulo"] = listadoitems;
            return Json(1);

        }

        /// <summary>
        /// Obtener datos al actualizar un cronograma de visitas
        /// </summary>
        /// <param name="idcronograma"></param>
        /// <returns></returns>
        public JsonResult getcronograma(Int32 idcronograma)
        {
            vw_visitas item = Model.ConsultaCronogramaVisitas(idcronograma).FirstOrDefault();

            var data = new
            {
                fechaesvisita = item.fecha_visita.Value.ToString("MM/dd/yyyy"),
                res = item.Auditor_Asignado,
                observaciones = item.motivo_actualizacion

            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calidad prestadores
        /// </summary>
        /// <returns></returns>
        public ActionResult CalidadPrestadores()
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            ViewBag.CalidadPrestadores = Model.getprestadoresList(null, null);

            return View();
        }

        /// <summary>
        /// Tablero de prestadores
        /// </summary>
        /// <param name="id_prestador"></param>
        /// <returns></returns>
        public ActionResult CrearPrestador(int? id_prestador)
        {
            Models.Facturacion.FacturaDevolucion Modelfacuracion = new Models.Facturacion.FacturaDevolucion();
            Models.ConsultaAfiliado.ConsultaAfiliado modelcafiliado = new Models.ConsultaAfiliado.ConsultaAfiliado();

            ViewBag.refespecialidad = Model.GetRefEspecialidadList();
            ViewBag.refRegimen = Model.GetRefRegimentList();
            ViewBag.ref_regional = Modelfacuracion.RefRegional;
            ViewBag.ref_tipodocumental = modelcafiliado.ListTipoDoc;
            ViewBag.ref_tipoprestador = Model.ConsultarIndicadores(null).Where(l => l.estado == "A").OrderByDescending(l => l.año_vigencia).ToList();
            ViewBag.ref_ambito = Model.LstAmbito();
            ViewBag.ref_responsables = ViewBag.responsables = Model.LstResponsables().Where(l => l.id_rol == 1 || l.id_rol == 2 || l.id_rol == 7 || l.id_rol == 4 || l.id_rol == 15 || l.id_rol == 8 || l.id_rol == 6 || l.id_rol == 27);
            ViewBag.ref_clase_persona = Model.GetClasePersonaList();
            ViewBag.ref_municipios = Model.GetMunicipiosDane();
            ViewData["Alerta"] = "";

            if (id_prestador == null)
            {
                return View(new calidad_prestadores());
            }
            else
            {
                return View(Model.getPresadorById(id_prestador.Value));
            }
        }

        public JsonResult ObtenerCodMun(int id_mun)
        {
            var listado = Model.GetMunicipiosDane().Where(l => l.id_ref_calidad_mun == id_mun).FirstOrDefault();
            return Json(listado.COPDGO_MUNICIPIO, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo post para crear prestadores
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        public JsonResult PostCrearPrestador(FormCollection formulario)
        {
            formulario = DeSerialize(formulario);
            int rta = 0;
            string data = "";

            int id_prestador = int.Parse(formulario["id_prestador"]);

            try
            {
                calidad_prestadores prestador = new calidad_prestadores();
                prestador.id_regional = int.Parse(formulario["regional"]);
                prestador.cod_habilitacion = formulario["codhabilitacion"];
                prestador.cod_sap = formulario["codsap"];
                prestador.razon_social = formulario["razonsocial"];
                prestador.tipo_id_prestador = formulario["tipoidprestador"];
                prestador.no_id_prestador = formulario["noidprestador"];
                prestador.tipo_contrato = formulario["tipocontrato"];
                prestador.nom_representante_legal = formulario["nomrepresentante"];
                prestador.direccion = formulario["direccion"];
                prestador.telefono_1 = formulario["telefono1"];
                prestador.telefono_2 = formulario["telefono2"];
                prestador.telefono_3 = formulario["telefono3"];
                prestador.celular = formulario["celular"];
                prestador.fax = formulario["fax"];
                prestador.correo_electronico = formulario["correoelectronico"];
                prestador.pagina_web = formulario["paginaweb"];
                prestador.id_muni_dane = formulario["idmunidane"];
                prestador.muni_dane = int.Parse(formulario["nom_municipio"]);
                prestador.actividad_economica = Convert.ToDouble(formulario["actividad_economica"]);
                prestador.ambito = formulario["ambito"];
                prestador.clase_persona = int.Parse(formulario["clasepersona"]);
                if (!string.IsNullOrEmpty(formulario["regimen_tributario"]))
                    prestador.regimen_tributario = int.Parse(formulario["regimen_tributario"]);
                prestador.Autoretenedor = formulario["autoretenedor"];
                prestador.Porcentaje_ICA = Convert.ToDouble(formulario["prcentaje_ica"]);
                if (!string.IsNullOrEmpty(formulario["poblacion_capitada"]))
                    prestador.Poblacion_Capitada = Convert.ToDouble(formulario["poblacion_capitada"]);
                if (!string.IsNullOrEmpty(formulario["dias_ofertados"]))
                    prestador.Días_Ofertados = Convert.ToDouble(formulario["dias_ofertados"]);
                if (!string.IsNullOrEmpty(formulario["horas_ofertadas"]))
                    prestador.Horas_ofertadas = Convert.ToDouble(formulario["horas_ofertadas"]);
                prestador.fecha_inicio_contrato = Convert.ToDateTime(formulario["fechainiciocontrato"]);
                prestador.fecha_fin_contrato = Convert.ToDateTime(formulario["fechafincontrato"]);
                prestador.num_contrato = formulario["nocontrato"];
                prestador.especialidad = int.Parse(formulario["especialidad"]);
                prestador.tipo_prestador = Convert.ToInt32(formulario["tipoprestador"]);
                prestador.tipo_localidad = formulario["tipolocalidad"];
                prestador.usuario_creacion_ = SesionVar.UserName;
                prestador.fecha_creacion = DateTime.Now;
                rta = 0;
                if (id_prestador == 0)
                {
                    Model.InsertarPrestador(prestador, ref MsgRes);
                    data = "Insertado Correctamente";
                }
                else
                {
                    Model.actualizarPrestador(prestador, id_prestador);
                    data = "Actualizado Correctamente";
                }

                var actualizaContrato = Model.ActualizarContratosPrestadorVisitas(prestador.cod_sap, prestador.fecha_inicio_contrato, prestador.fecha_fin_contrato, prestador.num_contrato, 2);

            }
            catch (Exception ex)
            {
                rta = 1;
                data = "Error al insertar: " + ex.Message;
            }

            return Json(new { idrta = rta, mensaje = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Vista para agregar visitas
        /// </summary>
        /// <returns></returns>
        public ActionResult AgregarVisita()
        {
            Models.Facturacion.FacturaDevolucion Modelfacuracion = new Models.Facturacion.FacturaDevolucion();
            Models.ConsultaAfiliado.ConsultaAfiliado modelcafiliado = new Models.ConsultaAfiliado.ConsultaAfiliado();

            ViewBag.refespecialidad = Model.GetRefEspecialidadList();
            ViewBag.refRegimen = Model.GetRefRegimentList();
            ViewBag.ref_regional = Modelfacuracion.RefRegional;
            ViewBag.ref_tipodocumental = modelcafiliado.ListTipoDoc;
            ViewBag.ref_tipoprestador = Model.ConsultarIndicadores(null);
            ViewBag.ref_ambito = Model.LstAmbito();
            ViewBag.ref_responsables = Model.LstResponsables().Where(l => l.id_rol == 1 || l.id_rol == 2 || l.id_rol == 7 || l.id_rol == 4 || l.id_rol == 15 || l.id_rol == 8 || l.id_rol == 12 || l.id_rol == 6 || l.id_rol == 27).ToList();
            ViewBag.ref_clase_persona = Model.GetClasePersonaList();
            ViewData["msj"] = 0;
            ViewData["Alerta"] = "";
            return View();
        }

        /// <summary>
        /// Metodo post para agregar visitas
        /// </summary>
        /// <param name="regional"></param>
        /// <param name="codhabilitacion"></param>
        /// <param name="codsap"></param>
        /// <param name="razonsocial"></param>
        /// <param name="especialidadred"></param>
        /// <param name="tipoidprestador"></param>
        /// <param name="noidprestador"></param>
        /// <param name="tipocontrato"></param>
        /// <param name="nomrepresentante"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <param name="celular"></param>
        /// <param name="correoelectronico"></param>
        /// <param name="nom_municipio"></param>
        /// <param name="clasepersona"></param>
        /// <param name="nocontrato"></param>
        /// <param name="especialidad"></param>
        /// <param name="tipocontratista"></param>
        /// <param name="ambito"></param>
        /// <param name="tipolocalidad"></param>
        /// <param name="fechainiciocontrato"></param>
        /// <param name="fechafincontrato"></param>
        /// <param name="fechaestimadavisita"></param>
        /// <param name="responsable"></param>
        /// <param name="tipoprestador"></param>
        /// <param name="localidad"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgregarVisita(int idprestador, DateTime fechavisita, string responsable, string localidad)
        {
            ViewData["Alerta"] = "";
            try
            {
                cronograma_visitas cronograma = new cronograma_visitas();
                cronograma.id_prestador = idprestador;
                cronograma.fecha_visita = fechavisita;
                cronograma.Auditor_Asignado = responsable;
                cronograma.localidad = localidad;
                cronograma.Realizo_evaluacion = false;
                Model.InsertarVisita(cronograma);
                ViewData["msj"] = 1;
                ViewData["Alerta"] = Model.MsgRestpuesta("success", "Transaccion Exitosa", "Visita guardada exitosamente.");
            }
            catch (Exception ex)
            {
                ViewData["msj"] = 2;
                ViewData["Alerta"] = Model.MsgRestpuesta("danger", "Transaccion Fallida", "Ha ocurrido un error: " + ex.Message);
            }

            Models.Facturacion.FacturaDevolucion Modelfacuracion = new Models.Facturacion.FacturaDevolucion();
            Models.ConsultaAfiliado.ConsultaAfiliado modelcafiliado = new Models.ConsultaAfiliado.ConsultaAfiliado();


            ViewBag.refespecialidad = Model.GetRefEspecialidadList();
            ViewBag.refRegimen = Model.GetRefRegimentList();
            ViewBag.ref_regional = Modelfacuracion.RefRegional;
            ViewBag.ref_tipodocumental = modelcafiliado.ListTipoDoc;
            ViewBag.ref_tipoprestador = Model.ConsultarIndicadores(null);
            ViewBag.ref_ambito = Model.LstAmbito();
            ViewBag.ref_responsables = Model.LstResponsables().Where(l => l.id_rol == 1 || l.id_rol == 2 || l.id_rol == 7 || l.id_rol == 4 || l.id_rol == 15 || l.id_rol == 8 || l.id_rol == 12);
            ViewBag.ref_clase_persona = Model.GetClasePersonaList();
            return View();
        }

        public ActionResult AdminVisitas(Int32? tipofiltro, DateTime? FechaInicialfiltro, DateTime? FechaFinalfiltro, Int32? idregional,
            string numcontrato, string auditor, int? cargo_acta, int? rta)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();


            ViewBag.tipofiltro = from TipoFiltroCronogramaVisitas d in Enum.GetValues(typeof(TipoFiltroCronogramaVisitas))
                                 select new SelectListItem
                                 {
                                     Value = ((int)d).ToString(),
                                     Text = Model.GetDescriptionTipofiltro(d)
                                 };

            ViewBag.fechainicial = FechaInicialfiltro;
            ViewBag.fechafinal = FechaFinalfiltro;
            ViewBag.idregional = idregional;
            ViewBag.numcontrato = numcontrato;
            ViewBag.auditor = auditor;
            ViewBag.auditorsesion = SesionVar.UserName;
            ViewBag.tiponovedad = Model.GetListTipoNovedad();

            List<Management_Consulta_Cronograma_VisitasResult> model = new List<Management_Consulta_Cronograma_VisitasResult>();
            if (idregional != null || !String.IsNullOrEmpty(numcontrato) || !String.IsNullOrEmpty(auditor) || FechaInicialfiltro != null || FechaFinalfiltro != null || tipofiltro != null)
            {
                if (SesionVar.ROL != "1" && SesionVar.ROL != "2" && SesionVar.ROL != "8" && SesionVar.ROL != "30" && SesionVar.ROL != "6")
                {
                    model = Model.ConsultaCronogramaVisitasProcedimiento(tipofiltro.Value, SesionVar.UserName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(auditor))
                    {
                        model = Model.ConsultaCronogramaVisitasProcedimiento(tipofiltro.Value, auditor);
                    }
                    else
                    {
                        model = Model.ConsultaCronogramaVisitasProcedimiento(tipofiltro.Value, null);
                    }
                }

                if (idregional != null)
                {
                    model = model.Where(l => l.id_regional == idregional).ToList();
                }

                if (!String.IsNullOrEmpty(numcontrato))
                {
                    model = model.Where(l => l.num_contrato.Contains(numcontrato)).ToList();
                }

                if (FechaInicialfiltro != null)
                {
                    if (tipofiltro == 1)
                    {
                        model = model.Where(l => l.fecha_visita.Value.Date >= FechaInicialfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                    else if (tipofiltro == 3)
                    {
                        model = model.Where(l => l.fecha_visita.Value.Date >= FechaInicialfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                    else
                    {
                        model = model.Where(l => l.fecha_final_visita.Value.Date >= FechaInicialfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }

                    ViewBag.fechainicial = FechaInicialfiltro.Value.ToString("MM/dd/yyyy");
                }

                if (FechaFinalfiltro != null)
                {
                    if (tipofiltro == 1)
                    {
                        model = model.Where(l => l.fecha_visita.Value.Date <= FechaFinalfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                    else if (tipofiltro == 3)
                    {
                        model = model.Where(l => l.fecha_visita.Value.Date >= FechaInicialfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                    else
                    {
                        model = model.Where(l => l.fecha_final_visita.Value.Date <= FechaFinalfiltro).OrderByDescending(l => l.fecha_visita).ToList();
                    }

                    ViewBag.fechafinal = FechaFinalfiltro.Value.ToString("MM/dd/yyyy");
                }

                if (cargo_acta != null)
                {
                    if (cargo_acta == 1)
                    {
                        model = model.Where(l => l.num_documento > 0).OrderByDescending(l => l.fecha_visita).ToList();
                        //model = model.Where(l => l.num_documentos_docs > 0).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                    else
                    {
                        model = model.Where(l => l.num_documento == 0).OrderByDescending(l => l.fecha_visita).ToList();
                        //model = model.Where(l => l.num_documentos_docs > 0).OrderByDescending(l => l.fecha_visita).ToList();
                    }
                }

                Session["ListaCronograma"] = model;
            }

            if (tipofiltro != null)
            {
                ViewData["filtro"] = tipofiltro;
            }
            else
            {
                ViewData["filtro"] = 0;
            }

            ViewBag.responsables = Model.LstResponsables().Where(l => l.id_rol == 1 || l.id_rol == 2 || l.id_rol == 7 || l.id_rol == 4 || l.id_rol == 15 || l.id_rol == 8);
            ViewBag.countresultados = model.Count();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.cargoacta = cargo_acta;
            if (rta == null)
            {
                ViewBag.rta = 0;
            }
            else
            {
                ViewBag.rta = rta;
            }

            ViewBag.idUser = SesionVar.IDUser;

            return View(model);
        }

        public void ExcelReporteVisitas()
        {
            List<Management_Consulta_Cronograma_VisitasResult> listareporte = (List<Management_Consulta_Cronograma_VisitasResult>)Session["ListaCronograma"];
            listareporte = listareporte.OrderBy(l => l.id_cronograma_visitas).ToList();
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Cronograma");

            Sheet.Cells["A2:U2"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A2:U2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A2:U2"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A2:U2"].Style.Font.Color.SetColor(Color.White);

            Sheet.Cells["A2"].Value = "Id visita";
            Sheet.Cells["B2"].Value = "Regional";
            Sheet.Cells["C2"].Value = "NIT";
            Sheet.Cells["D2"].Value = "Razón Social";
            Sheet.Cells["E2"].Value = "Especialidad";
            Sheet.Cells["F2"].Value = "Fecha Visita";
            Sheet.Cells["G2"].Value = "Tipo Prestador";
            Sheet.Cells["H2"].Value = "No Contrato";
            Sheet.Cells["I2"].Value = "Auditor Responsable";
            Sheet.Cells["J2"].Value = "Periodo fecha inicio";
            Sheet.Cells["K2"].Value = "Periodo fecha final";
            Sheet.Cells["L2"].Value = "Fecha final visita";
            Sheet.Cells["M2"].Value = "Calificacion final";
            Sheet.Cells["N2"].Value = "Novedades";
            Sheet.Cells["O2"].Value = "Fecha novedad";

            Sheet.Cells["P2"].Value = "Cargo Acta SI/No";
            Sheet.Cells["Q2"].Value = "Tipo de localidad";
            Sheet.Cells["R2"].Value = "unis";
            Sheet.Cells["S2"].Value = "ciudad";
            Sheet.Cells["T2"].Value = "Estado de la visita";
            Sheet.Cells["U2"].Value = "Tiene informe operativo";

            int row = 3;
            foreach (Management_Consulta_Cronograma_VisitasResult item in listareporte)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.id_cronograma_visitas;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.nombre_regional;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.no_id_prestador;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.razon_social;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.descripcion;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.fecha_visita.Value.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("G{0}", row)].Value = item.nom_tipo_prestador;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.num_contrato;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre;
                if (item.periodo_fecha_inicio != null)
                {
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.periodo_fecha_inicio.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    Sheet.Cells[string.Format("J{0}", row)].Value = "No hay dato";
                }
                if (item.periodo_fecha_inicio != null)
                {
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.periodo_fecha_final.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    Sheet.Cells[string.Format("K{0}", row)].Value = "No hay dato";
                }
                if (item.periodo_fecha_inicio != null)
                {
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.fecha_final_visita.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    Sheet.Cells[string.Format("L{0}", row)].Value = "No hay dato";
                }
                Sheet.Cells[string.Format("M{0}", row)].Value = item.calificacion_final_visita;
                if (item.id_cronograma_novedades == null)
                {
                    Sheet.Cells[string.Format("N{0}", row)].Value = "Sin novedades";
                    Sheet.Cells[string.Format("O{0}", row)].Value = "Sin novedades";
                }
                else
                {
                    if (item.nom_tipo_novedad != "Otro")
                    {
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.nom_tipo_novedad;
                    }
                    else
                    {
                        Sheet.Cells[string.Format("N{0}", row)].Value = item.otra_novedad;
                    }
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.fecha_novedad;
                }
                Sheet.Cells[string.Format("P{0}", row)].Value = item.Cargo_Acta;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.localidad;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.unis;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.Nom_Municipio;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.Estado;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.idInformeOperativo != null ? "SI" : "NO";

                row++;
            }
            Sheet.Cells["A:U"].AutoFitColumns();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Reportecronogramavisitas" + listareporte.First().id_cronograma_visitas + ".xls");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        /// <summary>
        /// metodo post para actualizar visitas
        /// </summary>
        /// <param name="idcronograma"></param>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="FechaEstimadaVisita"></param>
        /// <param name="responsable"></param>
        /// <returns></returns>
        public ActionResult ActualizarVisita(Int32 idcronograma, DateTime? FechaInicial, DateTime? FechaFinal, DateTime FechaEstimadaVisita, string responsable, string motivocambio, int txtidtipoFiltro)
        {
            try
            {
                cronograma_visitas cronograma = Model.getvisitabyid(Convert.ToInt32(idcronograma));
                cronograma.fecha_modificacion = DateTime.Now;
                cronograma.usuario_modificacion = SesionVar.UserName;
                cronograma.fecha_visita = FechaEstimadaVisita;
                if (!string.IsNullOrEmpty(responsable))
                {
                    cronograma.Auditor_Asignado = responsable;
                }

                cronograma.motivo_actualizacion = motivocambio;
                Model.ActualizarCronogramaVisitas(cronograma, ref MsgRes);
                return RedirectToAction("AdminVisitas", "ProcesosInternos", new { tipofiltro = txtidtipoFiltro, FechaInicialfiltro = FechaInicial, FechaFinalfiltro = FechaFinal, rta = 1 });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EvaluacionCalidad(int? tipoprestador, int? idregional, DateTime? fechainicial, DateTime? fechafinal, string num_nit,
            string cod_sap, string num_contrato, string razon_social, int? std)
        {
            try
            {

                string estadoevaluacion = (string)Session["estadoevaluacion"];
                if (string.IsNullOrEmpty(estadoevaluacion) || std == 0)
                {
                    Session["estadoevaluacion"] = "";
                    Session["itemcapitulo"] = null;
                }


                ViewBag.prestador = "";
                ViewBag.idregional = "";
                List<vw_visitas> cronogramalist = new List<vw_visitas>();

                if (tipoprestador != null || idregional != null || fechainicial != null || fechafinal != null || !String.IsNullOrEmpty(num_nit) || !String.IsNullOrEmpty(num_contrato) || !String.IsNullOrEmpty(num_nit) || !String.IsNullOrEmpty(razon_social) || !String.IsNullOrEmpty(cod_sap))
                {
                    cronogramalist = Model.ConsultaCronogramaVisitas(null).Where(l => l.estado_indicador == "A").OrderBy(l => l.id_cronograma_visitas).ToList();
                }

                if (tipoprestador != null)
                {
                    cronogramalist = cronogramalist.Where(l => l.tipo_prestador == tipoprestador.Value && l.Realizo_evaluacion == false).ToList();
                    ViewBag.prestador = tipoprestador;
                }

                if (idregional != null)
                {
                    cronogramalist = cronogramalist.Where(l => l.id_regional == idregional.Value && l.Realizo_evaluacion == false).ToList();
                    ViewBag.idregional = idregional;
                }

                if (fechainicial != null)
                {
                    cronogramalist = cronogramalist.Where(l => l.fecha_visita >= fechainicial && l.Realizo_evaluacion == false).ToList();
                    ViewBag.fechainicial = fechainicial.Value.ToString("MM/dd/yyyy");
                }

                if (fechafinal != null)
                {
                    cronogramalist = cronogramalist.Where(l => l.fecha_visita <= fechafinal && l.Realizo_evaluacion == false).ToList();
                    ViewBag.fechafinal = fechafinal.Value.ToString("MM/dd/yyyy");
                }

                if (!String.IsNullOrEmpty(num_nit))
                {
                    cronogramalist = cronogramalist.Where(l => l.no_id_prestador.StartsWith(num_nit) && l.Realizo_evaluacion == false).ToList();

                }
                if (!String.IsNullOrEmpty(cod_sap))
                {
                    cronogramalist = cronogramalist.Where(l => l.cod_sap.StartsWith(cod_sap) && l.Realizo_evaluacion == false).ToList();
                }
                if (!String.IsNullOrEmpty(num_contrato))
                {
                    cronogramalist = cronogramalist.Where(l => l.num_contrato.StartsWith(num_contrato) && l.Realizo_evaluacion == false).ToList();
                }
                if (!String.IsNullOrEmpty(razon_social))
                {
                    cronogramalist = cronogramalist.Where(l => l.razon_social.ToLower().Contains(razon_social.ToLower()) && l.Realizo_evaluacion == false).ToList();
                }


                if (SesionVar.ROL != "1" && SesionVar.ROL != "2" && SesionVar.ROL != "8" && SesionVar.ROL != "30" && SesionVar.ROL != "32")
                {
                    cronogramalist = cronogramalist.Where(l => l.Auditor_Asignado == SesionVar.UserName && l.Realizo_evaluacion == false).ToList();
                }

                ViewBag.cronograma = cronogramalist;
                ViewBag.regional = BusClass.GetRefRegion();
                ViewBag.ref_tipoprestador = Model.ConsultarIndicadores(null).Where(l => l.estado == "A").ToList();
                ViewBag.idprestador = tipoprestador;
                ViewBag.idregional = idregional;
                ViewBag.estadoevaluacion = estadoevaluacion;
                ViewBag.numnut = num_nit;
                ViewBag.cod_sap = cod_sap;
                ViewBag.num_contrato = num_contrato;
                ViewBag.razon_social = razon_social;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View();
        }

        /// <summary>
        /// Vista 
        /// </summary>
        /// <param name="idcronograma"></param> 
        /// <param name="TipoIndicador"></param>
        /// <returns></returns>
        public ActionResult EvaluacionIndicadores(int idcronograma, int TipoIndicador, int idregional, DateTime? proxFecha, int? active, int? sap)
        {
            List<ECOPETROL_COMMON.ENTIDADES.indicadores> ltaindicadores = new List<indicadores>();

            contratos_detalle detalleContrato = new contratos_detalle();

            DateTime fechaInicioContrato = new DateTime();
            DateTime fechaFinContrato = new DateTime();
            var numContrato = "";
            var idContrato = 0;
            var pasaContrato = 0;

            try
            {
                ltaindicadores = Model.ConsultarIndicadores(TipoIndicador);

                Session["estadoevaluacion"] = "activa";
                cronograma_visitas visita = Model.getvisitabyid(idcronograma);
                List<item_capitulo> itemcapitulo = new List<item_capitulo>();
                List<item_capitulo> cargados = (List<item_capitulo>)Session["itemcapitulo"];
                if (ltaindicadores.Count() <= 1)
                {
                    ECOPETROL_COMMON.ENTIDADES.indicadores indicador = ltaindicadores.FirstOrDefault();
                    List<capitulo_indicador> capituloindicador = Model.GetCapitulosByIndicador(indicador.id_indicador, idregional);
                    ViewBag.listacapitulos = capituloindicador;
                    foreach (var i in capituloindicador)
                    {
                        List<item_capitulo> item = Model.Getitemcapbyindcap(idregional, TipoIndicador, i.capitulo_id).Where(l => l.activo == true).ToList();
                        itemcapitulo.AddRange(item);
                    }
                }

                ViewBag.indicador = Model.ConsultarIndicadores(TipoIndicador).FirstOrDefault().nom_indicador;
                ViewBag.regional = BusClass.GetRefRegion().Where(l => l.id_ref_regional == idregional).FirstOrDefault().nombre_regional;
                ViewBag.idregional = idregional;
                ViewBag.idindicador = TipoIndicador;
                ViewBag.idcronograma = idcronograma;
                ViewBag.idprestador = visita.id_prestador;
                ViewBag.acactive = active;
                ViewBag.sap = sap;

                if (cargados != null)
                {
                    ViewBag.sum = cargados.Select(l => l.Valor_digitado).Sum();
                }
                else
                {
                    ViewBag.sum = 0;
                }

                ViewData["Evaluo"] = false;

                if (cargados == null)
                {
                    Session["itemcapitulo"] = itemcapitulo;
                }

                detalleContrato = BusClass.MostrarDetallePContrato(Convert.ToString(sap));

                if (detalleContrato != null)
                {
                    fechaInicioContrato = (DateTime)detalleContrato.fecha_inicio;
                    fechaFinContrato = (DateTime)detalleContrato.fecha_fin;
                    numContrato = detalleContrato.numero_contrato;
                    idContrato = detalleContrato.id_contrato;
                    pasaContrato = 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.fechaInicioContrato = fechaInicioContrato.ToString("MM/dd/yyyy");
            ViewBag.fechaFinContrato = fechaFinContrato.ToString("MM/dd/yyyy");
            ViewBag.numContrato = numContrato;
            ViewBag.idContrato = idContrato;
            ViewBag.pasaContrato = pasaContrato;

            return View();
        }

        /// <summary>
        /// creacion de reporte en pdf
        /// </summary>
        /// <param name="idcronograma"></param>
        /// <param name="tipoindicador"></param>
        /// <param name="proximafecha"></param>
        public ActionResult Pdf(int idprestador, Int32 idcronograma, int tipoindicador, int idregional, DateTime? proximafecha)
        {
            string vistapdf = "", filename = "", carpeta = "";

            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
            {
                carpeta = "PDFSEvaluacionVisitasDeCalidad";
            }
            else
            {
                carpeta = "PDFSEvaluacionVisitasDeCalidadPruebas";
            }

            string rutaFacturaEscaneada = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

            if (!Directory.Exists(rutaFacturaEscaneada)) { Directory.CreateDirectory(rutaFacturaEscaneada); }

            try
            {
                Rotativa.Core.DriverOptions opciones = new Rotativa.Core.DriverOptions();
                opciones.PageSize = Rotativa.Core.Options.Size.Letter;
                opciones.PageMargins = new Rotativa.Core.Options.Margins(15, 10, 20, 10);
                opciones.PageOrientation = Orientation.Portrait;

                vw_visitas visita = Model.ConsultaCronogramaVisitas(idcronograma).FirstOrDefault();
                List<cronograma_visita_detalle_indicador> capitulos = Model.ConsultaCronogramaVisitaDetalleInd(idcronograma).ToList();

                Models.ProcesosInternos.reportevisitas model = new Models.ProcesosInternos.reportevisitas();
                model.listacapitulos = capitulos;

                model.NomRegional = visita.nombre_regional;
                model.NoContrato = visita.num_contrato;
                model.Observaciones = visita.Observaciones_Evaluacion;
                model.proximafecha = proximafecha;
                model.Nit = visita.no_id_prestador;
                model.NomPrestador = visita.razon_social.ToUpper();
                model.NomAuditor = visita.nombre.ToUpper();
                model.NomRepresentante = visita.nom_representante_legal.ToUpper();
                model.idregional = idregional;
                model.tipoindicador = tipoindicador;
                model.idcronograma = idcronograma;
                model.proximafecha = proximafecha;
                model.periodo_desde = visita.periodo_fecha_inicio;
                model.periodo_hasta = visita.periodo_fecha_final;
                model.fecha_final_visita = visita.fecha_final_visita;
                model.fecha_visita = visita.fecha_visita;
                model.proximafecha = visita.proxima_fecha_visita;
                model.fechamodificacion = visita.fecha_modificacion;

                if (visita.version_pdf == 1)
                {
                    vistapdf = "EvaluacionIndicadoresPDF";
                }
                else
                {
                    vistapdf = "EvaluacionIndicadoresPDF2";
                }

                var file = new ViewAsPdf(vistapdf, model)
                {
                    FileName = "ReporteEvalucaionCalidad_" + idcronograma + ".pdf",
                    RotativaOptions = opciones
                };
                var byteArray = file.BuildPdf(ControllerContext);

                string dirpath = Path.Combine(rutaFacturaEscaneada, "ReporteEvaluacionCalidad_" + visita.id_cronograma_visitas + ".pdf");

                System.IO.File.WriteAllBytes(dirpath, byteArray);

                List<cronograma_visitas_reportesDoc_evaluacion_calidad> documentos = new List<cronograma_visitas_reportesDoc_evaluacion_calidad>();
                cronograma_visitas_reportesDoc_evaluacion_calidad doc = new cronograma_visitas_reportesDoc_evaluacion_calidad();
                doc.id_cronograma_visitas = visita.id_cronograma_visitas;
                doc.ruta_documento = dirpath;
                doc.fecha_digita = DateTime.Now;
                documentos.Add(doc);

                Model.InsertarMasivamenteReportesEvaluacionVisitas(documentos, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    return File(dirpath, "application/pdf");
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al guardar el registro del documento" });

                }


                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.Clear();

                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-length", byteArray.Length.ToString());
                //Response.BinaryWrite(byteArray);
                //Response.Flush();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al guardar el registro del documento " + ex.Message });
            }
        }

        public ActionResult EvaluacionIndicadoresPDF(AsaludEcopetrol.Models.ProcesosInternos.reportevisitas model)
        {
            return View(model);
        }

        public ActionResult EvaluacionIndicadoresPDF2(AsaludEcopetrol.Models.ProcesosInternos.reportevisitas model)
        {
            return View(model);
        }

        /// <summary>
        /// Vista evaluazion de calidad
        /// </summary>
        /// <returns></returns>
        public void EvaluarIndicador()
        {
            try
            {
                Int32 sap = Convert.ToInt32(Request.Form["sap"]);
                ViewBag.sap = sap;

                List<item_capitulo> listadoitems = (List<item_capitulo>)Session["itemcapitulo"];
                DateTime fechaestimadavisita = Convert.ToDateTime(Request.Form["fecha"]);
                Int32 idcronograma = Convert.ToInt32(Request.Form["idcronograma"]);
                Int32 idregional = Convert.ToInt32(Request.Form["idregional"]);
                Int32 idindicador = Convert.ToInt32(Request.Form["idindicador"]);
                string valuescalificaciones = Request.Form["calificaciones"];
                string[] calificaciones = valuescalificaciones.Replace("%", "").Split(',');

                string observaciones = Request.Form["observacion"];
                DateTime periodo_fecha_inicio = Convert.ToDateTime(Request.Form["periodo_fecha_inicial"]);
                DateTime periodo_fecha_fin = Convert.ToDateTime(Request.Form["periodo_fecha_final"]);
                DateTime fecha_final_visita = Convert.ToDateTime(Request.Form["fecha_final_visita"]);
                string p = Request.Form["calificacion_final"];
                decimal calificacion_final = Convert.ToDecimal(Request.Form["calificacion_final"]);
                List<capitulo_indicador> capitulos = BusClass.GetCapitulosByIndicador(idindicador, idregional, ref MsgRes);

                int idContrato = Convert.ToInt32(Request.Form["idContrato"]);
                int seActualiza = Convert.ToInt32(Request.Form["seActualiza"]);

                var insertarNuevoContrato = 0;

                contratos_detalle detalleContrato = new contratos_detalle();

                if (seActualiza == 1)
                {
                    string nuevonroContrato = Request.Form["nuevonroContrato"];
                    DateTime fechaInicioNueva = Convert.ToDateTime(Request.Form["fechaInicioNueva"]);
                    DateTime fechaFinNueva = Convert.ToDateTime(Request.Form["fechaFinNueva"]);
                    contratos_detalle detalleContrato2 = new contratos_detalle();
                    detalleContrato = BusClass.MostrarDetallePContrato(Convert.ToString(sap));

                    if (detalleContrato != null)
                    {
                        detalleContrato2 = detalleContrato;
                    }

                    detalleContrato2.id_cargue = null;
                    detalleContrato2.fecha_inicio = fechaInicioNueva;
                    detalleContrato2.fecha_fin = fechaFinNueva;
                    detalleContrato2.numero_contrato = nuevonroContrato;
                    detalleContrato2.documento_SAP = Convert.ToString(sap);

                    log_contratos_detalle log = new log_contratos_detalle();
                    log.id_cargue = detalleContrato2.id_cargue;
                    log.numero_contrato = detalleContrato2.numero_contrato;
                    log.periodo_finalizacion = detalleContrato2.periodo_finalizacion;
                    log.fecha_inicio = detalleContrato2.fecha_inicio;
                    log.fecha_fin = detalleContrato2.fecha_fin;
                    log.estado_contrato = detalleContrato2.estado_contrato;
                    log.nombre_gestor_contratacion = detalleContrato2.nombre_gestor_contratacion;
                    log.nombre_funcionario_autorizado = detalleContrato2.nombre_funcionario_autorizado;
                    log.objeto_contrato = detalleContrato2.objeto_contrato;
                    log.subcategoria_contrato_nombre = detalleContrato2.subcategoria_contrato_nombre;
                    log.categoria_nombre = detalleContrato2.categoria_nombre;
                    log.lugar_ejecucion_contrato = detalleContrato2.lugar_ejecucion_contrato;
                    log.departamento_ejecucion_contrato = detalleContrato2.departamento_ejecucion_contrato;
                    log.subregional_ejecucion_contrato = detalleContrato2.subregional_ejecucion_contrato;
                    log.regional_ejecucion_contrato = detalleContrato2.regional_ejecucion_contrato;
                    log.nombre_administrador_funcionario = detalleContrato2.nombre_administrador_funcionario;
                    log.nombre_interventor = detalleContrato2.nombre_interventor;
                    log.nit_proveedor = detalleContrato2.nit_proveedor;
                    log.documento_SAP = detalleContrato2.documento_SAP;
                    log.nombre_proveedor = detalleContrato2.nombre_proveedor;
                    log.fecha_digita = DateTime.Now;
                    log.usuario_digita = SesionVar.UserName;

                    if (detalleContrato != null)
                    {
                        insertarNuevoContrato = BusClass.ActualizarContrato(detalleContrato2, ref MsgRes);
                        var actualizaContrato = Model.ActualizarContratosPrestadorVisitas(Convert.ToString(sap), detalleContrato2.fecha_inicio, detalleContrato2.fecha_fin, detalleContrato2.numero_contrato, 1);
                    }
                    else
                    {
                        insertarNuevoContrato = BusClass.InsertarContratoNuevoPrestador(detalleContrato2);
                    }

                    if (insertarNuevoContrato != 0)
                    {
                        log.id_contrato = detalleContrato2.id_contrato;
                        var actualzia = BusClass.InsertarLogContratoNuevoPrestador(log);
                    }
                }

                if (insertarNuevoContrato != 0)
                {
                    idContrato = insertarNuevoContrato;
                }

                //Actualizar el cronograma de visitas
                cronograma_visitas cronograma = Model.getvisitabyid(Convert.ToInt32(idcronograma));
                cronograma.fecha_modificacion = DateTime.Now;
                cronograma.usuario_modificacion = SesionVar.UserName;
                cronograma.Realizo_evaluacion = true;
                cronograma.Observaciones_Evaluacion = observaciones;
                cronograma.periodo_fecha_inicio = periodo_fecha_inicio;
                cronograma.periodo_fecha_final = periodo_fecha_fin;
                cronograma.fecha_final_visita = fecha_final_visita;
                cronograma.calificacion_final_visita = calificacion_final;
                cronograma.id_tipo_prestador = idindicador;
                cronograma.proxima_fecha_visita = fechaestimadavisita;
                cronograma.version_pdf = 2;
                cronograma.id_contrato = idContrato;

                Model.ActualizarCronogramaVisitas(cronograma, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    Model.insertardetallevisita(idcronograma, idregional, idindicador, listadoitems, ref MsgRes);

                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        Model.insertarcalificacionesvisita(idcronograma, calificaciones, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            Model.insertardetallevisitaindicador(capitulos, idcronograma, cronograma.id_prestador, ref MsgRes);

                            cronograma_visitas nuevocronograma = new cronograma_visitas();
                            nuevocronograma.localidad = cronograma.localidad;
                            nuevocronograma.id_cronograma_visitas = 0;
                            nuevocronograma.fecha_visita = fechaestimadavisita;
                            nuevocronograma.Realizo_evaluacion = false;
                            nuevocronograma.observaciones = cronograma.Observaciones_Evaluacion;
                            nuevocronograma.id_prestador = cronograma.id_prestador;
                            nuevocronograma.Auditor_Asignado = cronograma.Auditor_Asignado;
                            Model.InsertarVisita(nuevocronograma);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            #region agregar cronograma

            #endregion
        }

        /// <summary>
        /// Vista para cargar los resultados de ranking
        /// </summary>
        /// <returns></returns>
        public ActionResult ResultadosRanking()
        {
            ViewData["MensajeRta"] = "";
            ViewBag.listaRegionales = BusClass.GetRefRegion();
            return View();
        }

        [HttpPost]
        public ActionResult ResultadosRanking(HttpPostedFileBase file, int idregional, int mes)
        {
            ViewData["MensajeRta"] = "";
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Resources"), fileName);
            file.SaveAs(path);

            var mensajeLog = "";
            log_cargues_masivos logMas = new log_cargues_masivos();

            logMas.fecha_Cargue = DateTime.Now;
            logMas.periodo_cargue = DateTime.Now;
            logMas.nombre_digita = SesionVar.NombreUsuario;
            logMas.usuario_digita = SesionVar.UserName;
            logMas.tipo_registro = "Ranking";

            try
            {
                List<calidad_detalle_cargue_ranking> ListaUrgencias = ExcelaEntidad(path);

                if (ListaUrgencias.Count() > 0)
                {
                    calidad_cargue_ranking ranking = new calidad_cargue_ranking();
                    ranking.fecha_digita = DateTime.Now;
                    ranking.usuario_digita = SesionVar.UserName;
                    ranking.mes_medicion = mes;
                    ranking.id_regional = idregional;
                    Int32 idcargueranking = Model.InsertarCargueRanking(ranking);

                    if (idcargueranking > 0)
                    {
                        int contlistaurgencias = ListaUrgencias.Count();
                        for (int i = 0; i < contlistaurgencias; i++)
                        {
                            ListaUrgencias[i].id_cargue_rankin = idcargueranking;
                        }

                        Model.InsertarDetalleCargueRanking(ListaUrgencias);
                    }

                    logMas.estado_cargue = mensajeLog;
                    logMas.registros_Cargados = ListaUrgencias.Count();
                    logMas.id_cargue = idcargueranking;

                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                    System.IO.File.Delete(path);
                    ViewData["MensajeRta"] = Model.MsgRestpuesta("success", "Proceso Exitoso", "Registros cargados correctamente");
                }
                else
                {
                    logMas.estado_cargue = mensajeLog;
                    logMas.registros_Cargados = ListaUrgencias.Count();
                    logMas.id_cargue = 0;

                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                    System.IO.File.Delete(path);
                    ViewData["MensajeRta"] = Model.MsgRestpuesta("info", "Proceso Terminado", "No se han hallado registros en el documento adjunto");
                }
            }
            catch (Exception ex)
            {
                String mensajeError = ex.Message;

                if (mensajeError.Contains("is not a valid worksheet name in file"))
                {
                    mensajeError = "El nombre de la hoja de calculo no es valida";
                }
                System.IO.File.Delete(path);
                ViewData["MensajeRta"] = Model.MsgRestpuesta("danger", "Proceso Fallido", "Ha ocurrido un error al momento de cargar los registros: " + mensajeError);

                logMas.estado_cargue = mensajeLog;
                logMas.registros_Cargados = 0;
                logMas.id_cargue = 0;

                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);
            }

            ViewBag.listaRegionales = BusClass.GetRefRegion();
            return View();
        }

        /// <summary>
        /// Excel a entidad
        /// </summary>
        /// <param name="pathDelFicheroExcel"></param>
        /// <returns></returns>
        public List<calidad_detalle_cargue_ranking> ExcelaEntidad(string pathDelFicheroExcel)
        {

            var book = new ExcelQueryFactory(pathDelFicheroExcel);
            var resultado = (from row in book.Worksheet("RANKING")
                             let item = new ECOPETROL_COMMON.ENTIDADES.calidad_detalle_cargue_ranking
                             {
                                 nit_prestador = Convert.ToInt32(row["Nit del prestador"]),
                                 nombre_prestador = row["Nombre del prestador"],
                                 locaclidad = row["Localidad"],
                                 resultado_LUPE = Convert.ToDouble(row["Resultado LUPE"]),
                                 evolucion_adherencia_LUPE = Convert.ToDouble(row["Evaluación Adherencia LUPE"]),
                                 resultado_guia_farmacoterapeuticas = Convert.ToDouble(row[" resultado  Guía Farmacoterapéuticas HTA, DM, Dislipidemia"]),
                                 evolucionr_esultado_guia_farmacoterapeuticas = Convert.ToDouble(row["Evaluación Resultado  Guía Farmacoterapéuticas"]),
                                 total_gestion_medicamentos = Convert.ToDouble(row["Total Gestión Medicamentos"]),
                                 resultado_oportunidad_citas = Convert.ToDouble(row["Resultado Oportunidad Citas"]),
                                 evolucion_oportunidad_citas_resultado = Convert.ToDouble(row["Evaluación Oportunidad citasResultado"]),
                                 total_oportunidad_citas = Convert.ToDouble(row["Total Oportunidad de Citas"]),
                                 resultado_solicitud_cambio_mega_mepa = Convert.ToDouble(row["resultado_Solictud cambio de Mega/Mepa"]),
                                 evaluacion_solicitud_cambio_mega_mepa = Convert.ToDouble(row["Evaluación_Solictud cambio de Mega/Mepa"]),
                                 resultado_quejas_prestacion_de_servicios = Convert.ToDouble(row["Resultado_Quejas prestación de servicios"]),
                                 evaluacion_quejas_prestacion_de_servicios = Convert.ToDouble(row["Evaluación_Quejas prestación de servicios"]),
                                 resultado_satisfaccion_beneficiarios = Convert.ToDouble(row["Resultado_Satisfación beneficiarios"]),
                                 evaluacion_satisfaccion_beneficiarios = Convert.ToDouble(row["Evaluación_Satisfación beneficiarios"]),
                                 total_satisfaccion_de_beneficiarios = Convert.ToDouble(row["Total_Satisfación de Beneficiarios"]),
                                 evaluacion_pacientes_controlados_con_patologias_traz_hipertencion = Convert.ToDouble(row["resultados_Pacientes controlados con patologías trazadores:Hiper"]),
                                 resultados_pacientes_controlados_con_patologias_traz_hipertencion = Convert.ToDouble(row["Evaluación_Pacientes controlados con patologías trazadores:Hiper"]),
                                 resultados_pacientes_controlados_con_patologias_traz_diabetes = Convert.ToDouble(row["resultados_Pacientes controlados con patologías trazadores: Diab"]),
                                 evaluacion_pacientes_controlados_con_patologias_traz_diabetes = Convert.ToDouble(row["Evaluación_Pacientes controlados con patologías trazadores: Diab"]),
                                 resultados_cobertura_tamizaje_cancer_de_mama = Convert.ToDouble(row["resultados_Cobertura tamizaje de cáncer de: mama#"]),
                                 evaluacion_cobertura_tamizaje_cancer_de_mama = Convert.ToDouble(row["Evalución_Cobertura tamizaje de cáncer de: mama#"]),
                                 resultados_cobertura_tamizaje_cancer_de_cervix = Convert.ToDouble(row[" resultados_Cobertura tamizaje de cáncer de: Cérvix#"]),
                                 evaluacion_cobertura_tamizaje_cancer_de_cervix = Convert.ToDouble(row["Evaluación_Cobertura tamizaje de cáncer de: Cérvix#__"]),
                                 resultados_cobertura_tamizaje_cancer_de_prostata = Convert.ToDouble(row["resultados _Cobertura tamizaje de cáncer de: Próstata"]),
                                 evaluacion_cobertura_tamizaje_cancer_de_prostata = Convert.ToDouble(row["Evaluación_Cobertura tamizaje de cáncer de: Próstata"]),
                                 resultados_cobertura_tamizaje_cancer_de_colon = Convert.ToDouble(row["resultados_Cobertura tamizaje de cáncer de: Colon"]),
                                 evaluacion_cobertura_tamizaje_cancer_de_colon = Convert.ToDouble(row["Evaluación_Cobertura tamizaje de cáncer de: Colon"]),
                                 resultados_cobertura_evaluación_individual_del_riesgo_gams = Convert.ToDouble(row[" resultados_Cobertura evaluación individual del riesgo (gams)"]),
                                 evaluacion_cobertura_evaluación_individual_del_riesgo_gams = Convert.ToDouble(row["Evaluación_Cobertura evaluación individual del riesgo (gams)"]),
                                 resultados_gestion_de_eventos_adversos_atribuibles_al_prestador = Convert.ToDouble(row["resultados_Gestión de eventos adversos atribuibles al prestador"]),
                                 evaluacion_gestion_de_eventos_adversos_atribuibles_al_prestador = Convert.ToDouble(row["Evaluación_Gestión de eventos adversos atribuibles al prestador"]),
                                 resultados_capacidad_resolutiva = Convert.ToDouble(row["resultados_Capacidad resolutiva"]),
                                 evaluacion_capacidad_resolutiva = Convert.ToDouble(row["Evaluación_Capacidad resolutiva"]),
                                 resultados_hospitalización_evitable = Convert.ToDouble(row["resultados_Hospitalización evitable"]),
                                 evaluacion_hospitalización_evitable = Convert.ToDouble(row["Evaluación_Hospitalización evitable"]),
                                 resultados_gestion_de_planes_de_mejoramiento = Convert.ToDouble(row["resultados_Gestión de planes de mejoramiento"]),
                                 evaluacion_gestion_de_planes_de_mejoramiento = Convert.ToDouble(row["Evaluación_Gestión de planes de mejoramiento"]),
                                 resultados_detencion_temprana_de_cancer_de_mama = Convert.ToDouble(row["resultados_Detención temprana de cáncer de mama"]),
                                 evaluacion_detencion_temprana_de_cancer_de_mama = Convert.ToDouble(row["Evaluación_Detención temprana de cáncer de mama"]),
                                 resultados_detencion_temprana_de_cancer_de_Cervix = Convert.ToDouble(row["resultados_Detención temprana de cáncer de Cérvix"]),
                                 evaluacion_detencion_temprana_de_cancer_de_Cervix = Convert.ToDouble(row["Evaluación_Detención temprana de cáncer de Cérvix"]),
                                 resultados_detencion_temprana_de_cancer_de_Prostata = Convert.ToDouble(row[" resultados_Detención temprana de cáncer de Próstata"]),
                                 evaluacion_detencion_temprana_de_cancer_de_Prostata = Convert.ToDouble(row["Evaluación_Detención temprana de cáncer de Próstata"]),
                                 Total_Gestion_del_Riesgo = Convert.ToDouble(row["Total Gestión del Riesgo"]),
                                 Total_General = Convert.ToDouble(row["Total General"])
                             }
                             select item).ToList();
            book.Dispose();
            return resultado;
        }

        /// <summary>
        /// Metodo para que un autocomplete busque los prestadores
        /// </summary>
        /// <param name="term"></param>
        /// <param name="tipofiltro"></param>
        /// <returns></returns>
        public JsonResult BuscarPrestador(string term, int tipofiltro)
        {
            List<calidad_prestadores> prestadores = Model.getprestadoresList(term, tipofiltro);
            var lista = new object();
            switch (tipofiltro)
            {
                case 1:
                    lista = (from item in prestadores
                             select new
                             {
                                 id = item.id_prestador,
                                 nit = item.no_id_prestador,
                                 nombre = item.razon_social.ToUpper(),
                                 codigosap = item.cod_sap,
                                 label = item.cod_sap,
                             }).Distinct().OrderBy(f => f.label).Take(15);
                    break;
                case 2:
                    lista = (from item in prestadores
                             select new
                             {
                                 id = item.id_prestador,
                                 nit = item.no_id_prestador,
                                 nombre = item.razon_social.ToUpper(),
                                 codigosap = item.cod_sap,
                                 label = item.no_id_prestador,
                             }).Distinct().OrderBy(f => f.label).Take(15);

                    break;
                case 3:
                    lista = (from item in prestadores
                             select new
                             {
                                 id = item.id_prestador,
                                 nit = item.no_id_prestador,
                                 nombre = item.razon_social.ToUpper(),
                                 codigosap = item.cod_sap,
                                 label = item.razon_social.ToUpper(),
                             }).Distinct().OrderBy(f => f.label).Take(15);
                    break;
                default:
                    break;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo para comr¿probar el estado de una evaluacion
        /// </summary>
        /// <returns></returns>
        public JsonResult Comprobarestadoevaluacion()
        {
            string estadoevaluacion = (string)Session["estadoevaluacion"];
            if (String.IsNullOrEmpty(estadoevaluacion))
            {
                return Json(0);
            }
            else
            {
                return Json(1);
            }
        }

        /// <summary>
        /// Metodo para cambiar el estado de una evaluacion
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public JsonResult cambiarestadoevaluacion(int estado)
        {
            if (estado == 1)
            {
                Session["estadoevaluacion"] = "Activa";
                return Json(0);

            }
            else
            {
                Session["estadoevaluacion"] = "";
                Session["itemcapitulo"] = null;
                return Json(0);
            }

        }

        /// <summary>
        /// Metodo para cargar el acta de una visita
        /// </summary>
        /// <returns></returns>
        //public JsonResult CargarActaVisita()
        //{
        //    try
        //    {
        //        AsaludEcopetrol.Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
        //        cronograma_visita_documento doc = new cronograma_visita_documento();
        //        doc.id_cronograma_visita = Convert.ToInt32(Request.Form["idvisita"]);

        //        if (Request.Files["documentovisita"] != null)
        //        {
        //            var file = Request.Files["documentovisita"];
        //            if (file.FileName != "")
        //            {
        //                if (Request.Files.AllKeys[0].Contains("documentovisita"))
        //                {
        //                    HttpPostedFileBase documento = file;
        //                    System.IO.Stream fs = documento.InputStream;
        //                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
        //                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //                    doc.documento = bytes;
        //                    doc.nom_documento = documento.FileName;
        //                    doc.usuario_digita = SesionVar.UserName;
        //                    doc.ext = Path.GetExtension(documento.FileName);
        //                    doc.fecha_digita = DateTime.Now;
        //                    Model.GuardarActaVisitas(doc);
        //                }
        //            }
        //        }

        //        return Json(new { success = true, mensaje = "Acta cargada correctamente" }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: " + ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult CargarActaVisita(int? idVisita, HttpPostedFileBase documentovisita)
        {
            cronograma_visita_documento doc = new cronograma_visita_documento();
            List<management_cronograma_visita_documento_sinRutaResult> sinRuta = new List<management_cronograma_visita_documento_sinRutaResult>();
            var nombreArchivo = "";

            try
            {
                doc.id_cronograma_visita = (int)idVisita;

                if (documentovisita != null)
                {
                    var file = documentovisita;
                    if (file.FileName != "")
                    {
                        HttpPostedFileBase documento = file;

                        nombreArchivo = file.FileName.Replace(",", "").Replace("  ", "").Replace(" ", "").Replace("-", "");
                        nombreArchivo = nombreArchivo.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");

                        string strRetorno = string.Empty;
                        StringBuilder sbRutaDefinitiva;
                        string strRutaDefinitiva = string.Empty;
                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosVisitas"];
                        sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                        string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreArchivo);
                        string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                        string strError = string.Empty;

                        DateTime fecha = DateTime.Now;
                        string archivo = string.Empty;

                        String carpeta = "";

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            carpeta = "DocumentoCronogramaVisitas";
                        }
                        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                        {
                            carpeta = "DocumentoCronogramaVisitasPruebas";
                        }

                        ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idVisita_" + doc.id_cronograma_visita);
                        var nombre = Path.GetFileNameWithoutExtension(nombreArchivo);
                        archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                        fecha, nombre, Path.GetExtension(nombreArchivo));

                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        //file.SaveAs(archivo);

                        //using (var fileStream = new FileStream(archivo, FileMode.Create))
                        //{
                        //    // Copiar los bytes del archivo al FileStream
                        //    file.InputStream.CopyTo(fileStream);
                        //}

                        using (var memoryStream = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memoryStream);
                            byte[] fileData = memoryStream.ToArray();

                            // Escribir los datos en el archivo de destino
                            System.IO.File.WriteAllBytes(archivo, fileData);
                        }

                        doc.ruta = archivo;
                        doc.documento = null;
                        doc.nom_documento = nombreArchivo;
                        doc.usuario_digita = SesionVar.UserName;
                        doc.ext = Path.GetExtension(documento.ContentType);
                        doc.fecha_digita = DateTime.Now;
                        Model.GuardarActaVisitas(doc, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            sinRuta = BusClass.GetactavisitaSinRuta();

                            if (sinRuta.Count() > 0)
                            {
                                for (var i = false; i == false;)
                                {
                                    List<management_cronograma_visita_documento_sinRutaResult> sinRutaFinal = new List<management_cronograma_visita_documento_sinRutaResult>();

                                    var retorno = ActualizarRutaDocumentosVisitas();
                                    if (retorno != "")
                                    {
                                        sinRutaFinal = BusClass.GetactavisitaSinRuta();

                                        if (sinRutaFinal.Count() > 0)
                                        {
                                            i = false;
                                        }
                                        else
                                        {
                                            i = true;
                                        }
                                    }
                                }
                            }

                            return Json(new { success = true, mensaje = "Acta cargada correctamente" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: Archivo sin nombre" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: No se acepta documento" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo para convertir bytes de documento a archivo fisico 
        /// Autor : jonathan lopez + alexis 
        /// FEcha: 19032024
        /// </summary>
        /// <returns></returns>

        public JsonResult CargarInformeOperativo(int? idVisita, HttpPostedFileBase documentovisitaInforme)
        {
            cronograma_visita_informeOperativo doc = new cronograma_visita_informeOperativo();

            try
            {
                doc.id_visita = (int)idVisita;

                if (documentovisitaInforme != null)
                {
                    var file = documentovisitaInforme;
                    if (file.FileName != "")
                    {
                        HttpPostedFileBase documento = file;

                        string strRetorno = string.Empty;
                        StringBuilder sbRutaDefinitiva;
                        string strRutaDefinitiva = string.Empty;
                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosVisitas"];
                        sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                        string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\");
                        string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                        string strError = string.Empty;

                        DateTime fecha = DateTime.Now;
                        string archivo = string.Empty;

                        String carpeta = "";

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            carpeta = "DocumentoInformeOperativo";
                        }
                        else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                        {
                            carpeta = "DocumentoInformeOperativo";
                        }

                        ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idVisita_" + doc.id_visita + "\\" + carpeta);
                        var nombre = Path.GetFileNameWithoutExtension("Informe_operativo");
                        archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                        fecha, nombre, Path.GetExtension(file.FileName));

                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        //file.SaveAs(archivo);

                        using (var fileStream = new FileStream(archivo, FileMode.Create))
                        {
                            // Copiar los bytes del archivo al FileStream
                            file.InputStream.CopyTo(fileStream);
                        }

                        doc.ruta = archivo;
                        doc.nombre_documento = documento.FileName;
                        doc.usuario_digita = SesionVar.UserName;
                        doc.extension = Path.GetExtension(documento.FileName);
                        doc.fecha_digita = DateTime.Now;
                        Model.GuardarInformeOperativo(doc, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            return Json(new { success = true, mensaje = "Informe operativo cargado correctamente" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el informe operativo: " + MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el informe operativo: Archivo sin nombre" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el informe operativo: No se acepta documento" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el informe operativo: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult VerArchivoinformeOperativo(int? idArchivo)
        {
            Models.PQRS.GestionPqrs Model = new Models.PQRS.GestionPqrs();

            try
            {
                cronograma_visita_informeOperativo dato = new cronograma_visita_informeOperativo();
                dato = BusClass.TraerArchivoInformeOperativo(idArchivo);

                if (dato == null)
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });
                }
                else
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.ruta;
                    string extension = "";

                    string[] nombrePartido = new string[0];
                    nombrePartido = obj.ruta.Split('\\');
                    var nombreFinal = nombrePartido[4];


                    if (filename.Contains(".pdf"))
                    {
                        extension = "application/pdf";
                    }
                    else if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    else
                    {
                        extension = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    return File(dirpath, extension, nombreFinal);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " + ex.Message });
            }
        }

        public void ConvertirBytesAPdfEquiposCompraDetalle()
        {
            try
            {
                WebClient User = new WebClient();
                string filename = "", carpeta = "";
                string strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosVisitas"];
                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "documentovisita";
                }
                else
                {
                    carpeta = "documentovisitaPruebas";
                }

                List<cronograma_visita_documento> ListadoDocumentosVisitas = BusClass.ObtenerListadoDocumentosVisitas();

                foreach (cronograma_visita_documento Dtll in ListadoDocumentosVisitas)
                {
                    cronograma_visita_documento obj = new cronograma_visita_documento();
                    obj.id_cronograma_visita = Dtll.id_cronograma_visita;

                    Binary documento = Dtll.documento;

                    if (documento != null && documento.Length > 0)
                    {
                        /*Se degine la ruta de las actas que son de entrega*/
                        string rutaFacturaEscaneada = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + "FacturasEscaneadas");

                        /*Si no existen la ruta, se crea*/
                        if (!Directory.Exists(rutaFacturaEscaneada)) { Directory.CreateDirectory(rutaFacturaEscaneada); }

                        /*Se crea el nombre del archivo*/
                        filename = Dtll.nom_documento;

                        /*Crear archivo en la ruta fisica*/
                        System.IO.File.WriteAllBytes(Path.Combine(rutaFacturaEscaneada, filename), Dtll.documento.ToArray());

                        /*Se pone la ruta de el archivo acta de Asignacion*/
                        obj.ruta = Path.Combine(rutaFacturaEscaneada, filename);


                        //BusClass.ActualizarRutaByteMed(obj, ref MsgRes);
                        BusClass.ActualizarRutasDocsVisitas(obj, ref MsgRes);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        //public JsonResult CargarActaVisita()
        //{
        //    try
        //    {
        //        AsaludEcopetrol.Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
        //        cronograma_visita_documento doc = new cronograma_visita_documento();
        //        doc.id_cronograma_visita = Convert.ToInt32(Request.Form["idvisita"]);

        //        if (Request.Files["documentovisita"] != null)
        //        {
        //            var file = Request.Files["documentovisita"];
        //            if (file.FileName != "")
        //            {
        //                if (Request.Files.AllKeys[0].Contains("documentovisita"))
        //                {
        //                    HttpPostedFileBase documento = file;
        //                    using (StreamReader reader = new StreamReader(documento.InputStream))
        //                    {
        //                        strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosVisitas"];
        //                        //string ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + doc.id_cronograma_visita +"\\" + file.FileName);


        //                        string rutaArchivo = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva, doc.id_cronograma_visita.ToString(), file.FileName);
        //                        string carpeta = Path.GetDirectoryName(rutaArchivo);


        //                        //if (!Directory.Exists(ruta))
        //                        //    Directory.CreateDirectory(ruta);

        //                        // Verificar si la carpeta existe, si no, crearla
        //                        if (!Directory.Exists(carpeta))
        //                            Directory.CreateDirectory(carpeta);

        //                        file.SaveAs(rutaArchivo);

        //                        doc.ruta = rutaArchivo;
        //                        doc.nom_documento = documento.FileName;
        //                        doc.usuario_digita = SesionVar.UserName;
        //                        doc.ext = Path.GetExtension(documento.FileName);
        //                        doc.fecha_digita = DateTime.Now;

        //                        // Guardar el contenido del archivo como cadena

        //                        Model.GuardarActasVisitas(doc);

        //                    }
        //                }
        //            }
        //        }

        //        return Json(new { success = true, mensaje = "Acta cargada correctamente" }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mensaje = "Ah ocurrido un error al momento de cargar el acta: " + ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        /// <summary>
        /// Metodo para ver el acta de la visita
        /// </summary>
        /// <param name="idvisita"></param>
        //public void VerActaVisita(int idvisita)
        //{


        //    AsaludEcopetrol.Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
        //    cronograma_visita_documento doc = Model.Getactavisita(idvisita);

        //    if (doc != null)
        //    {

        //        Binary binary2 = doc.documento;
        //        byte[] array = binary2.ToArray();

        //        if (array != null)
        //        {
        //            Response.ClearContent();
        //            Response.ClearHeaders();
        //            Response.Clear();

        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-length", array.Length.ToString());
        //            Response.BinaryWrite(array);
        //            Response.Flush();

        //        }
        //    }
        //    //else
        //    //{

        //    //    cronograma_visita_documentos docs = Model.Getactavisitas(idvisita);
        //    //    if (docs != null)
        //    //    {
        //    //        // Obtener la ruta del documento PDF desde el objeto docs
        //    //        string rutaPDF = docs.ruta;

        //    //        if (!string.IsNullOrEmpty(rutaPDF))
        //    //        {
        //    //            // Descargar el archivo PDF
        //    //            Response.ContentType = "application/pdf";
        //    //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(rutaPDF));
        //    //            Response.TransmitFile(rutaPDF);
        //    //            Response.End();
        //    //        }
        //    //        else
        //    //        {
        //    //            // Manejar el caso en el que no se encuentra el archivo
        //    //            Response.Write("El archivo PDF no se encontró.");
        //    //            Response.End();
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        // Manejar el caso en el que no se encuentra ninguna información relacionada con la visita
        //    //        Response.Write("No se encontró información de visita.");
        //    //        Response.End();
        //    //    }


        //    //}
        //}

        public ActionResult VerActaVisita(int idvisita)
        {
            management_cronograma_visita_documento_idResult dato = new management_cronograma_visita_documento_idResult();
            try
            {
                dato = BusClass.Getactavisita2(idvisita);

                if (dato != null)
                {
                    var obj = dato;
                    string dirpath = Path.Combine(Request.PhysicalApplicationPath, obj.ruta);
                    string filename = obj.nom_documento;
                    string extension = "";
                    extension = "application/pdf";

                    return File(dirpath, extension, filename);
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return RedirectToAction("ControlErrores", "Usuario", new { mensaje = "Ha ocurrido un error al momento de visualizar el archivo: " });

            }
        }

        public JsonResult EliminarActaVisitas(int? idCrono)
        {
            var mensaje = "";
            var rta = 0;
            management_cronograma_visita_documento_idResult dato = new management_cronograma_visita_documento_idResult();
            var nombreArchivo = "";
            var idArchivo = 0;

            try
            {
                dato = BusClass.Getactavisita2((int)idCrono);

                if (dato != null)
                {
                    nombreArchivo = dato.nom_documento;
                    idArchivo = dato.id_documento_visita;
                }

                var elimina = BusClass.EliminarActaVisitasCronogramaId(idCrono, ref MsgRes);
                if (elimina != 0)
                {
                    var log = BusClass.GuardarLogEliminarActaVisitas(idCrono, idArchivo, nombreArchivo, SesionVar.UserName);

                    mensaje = "ACTA ELIMINADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL ACTA: " + MsgRes.DescriptionResponse;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL ACTA: " + MsgRes.DescriptionResponse;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public JsonResult EliminarInformeOperativo(int? idCrono)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var elimina = BusClass.EliminarInformeOperativo(idCrono, ref MsgRes);

                if (elimina != 0)
                {
                    mensaje = "INFORME OPERATIVO ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR AL ELIMINAR EL INFORME OPERATIVO: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL ELIMINAR EL INFORME OPERARITO: " + MsgRes.DescriptionResponse;
            }

            return Json(new { mensaje = mensaje, rta = rta });


        }

        public string ActualizarRutaDocumentosVisitas()
        {
            var retorno = "";
            List<management_cronograma_visita_traerByteResult> listado = new List<management_cronograma_visita_traerByteResult>();

            try
            {
                listado = BusClass.ObtenerListadoDocumentosVisitasSinRuta();
                if (listado.Count() > 0)
                {
                    foreach (var item in listado)
                    {
                        WebClient User = new WebClient();
                        Binary binary = item.documento;

                        if (binary != null)
                        {
                            byte[] array = binary.ToArray();
                            if (array.Length > 0)
                            {
                                HttpPostedFileBase sigFile = (HttpPostedFileBase)new CustomPostedFile(array, item.id_cronograma_visita + ".pdf");

                                string strRetorno = string.Empty;
                                StringBuilder sbRutaDefinitiva;
                                string strRutaDefinitiva = string.Empty;
                                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosVisitas"];
                                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + sigFile.FileName);
                                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                                string strError = string.Empty;

                                DateTime fecha = DateTime.Now;
                                string archivo = string.Empty;

                                String carpeta = "";

                                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                                {
                                    carpeta = "DocumentoCronogramaVisitas";
                                }
                                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                                {
                                    carpeta = "DocumentoCronogramaVisitasPruebas";
                                }

                                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + "idVisita_" + item.id_cronograma_visita);
                                var nombre = Path.GetFileNameWithoutExtension(sigFile.FileName);
                                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                                fecha, nombre, Path.GetExtension(sigFile.FileName));

                                if (!Directory.Exists(ruta))
                                    Directory.CreateDirectory(ruta);

                                string filename2 = archivo;

                                using (FileStream fs = new FileStream(filename2, FileMode.Create))
                                {
                                    fs.Write(array, 0, array.Length);
                                }

                                if (Directory.Exists(ruta))
                                {
                                    BusClass.ActualizarRutaDocumentoVisitasCronograma(archivo, item.id_cronograma_visita);
                                }
                            }
                        }
                    }
                    retorno = "Aprobado";
                }
                else
                {
                    retorno = "Aprobado";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }

        public class CustomPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;
            MemoryStream stream;

            public CustomPostedFile(byte[] fileBytes, string filename = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = filename;
                this.ContentType = "application/pdf"; // Contenido de PDF
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

        /// <summary>
        /// Metodo para consultar visitas de calidad
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultaVisitasDeCalidad()
        {
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.ref_tipoprestador = Model.ConsultarIndicadores(null);
            return View();
        }

        public void ExportarVisitaCalidad(int regional, int prestador, DateTime fechainicial)
        {
            List<ManagementConsultaGnralVisitasResult> ConsultaGnralList = new List<ManagementConsultaGnralVisitasResult>();
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            List<Ref_regional> regionales = new List<Ref_regional>();

            if (regional == 7)
            {
                regionales = BusClass.GetRefRegion();
            }
            else
            {
                regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == regional).ToList();
            }

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");
            ExcelWorksheet Sheet2 = Ep.Workbook.Worksheets.Add("Indicadores");

            Sheet.Cells["A1:BL1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:BL1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:BL1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:BL1"].Style.Font.Color.SetColor(Color.White);
            Sheet.Cells["A1:BL1"].Style.Font.Name = "Century Gothic";

            string[] columnas_1 = new string[50] { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL" };
            string[] columnas_2 = new string[50] { "N{0}", "O{0}", "P{0}", "Q{0}", "R{0}", "S{0}", "T{0}", "U{0}", "V{0}", "W{0}", "X{0}", "Y{0}", "Z{0}", "AA{0}", "AB{0}", "AC{0}", "AD{0}", "AF{0}", "AG{0}", "AH{0}", "AI{0}", "AJ{0}", "AK{0}", "AL{0}", "AM{0}", "AN{0}", "AO{0}", "AP{0}", "AQ{0}", "AR{0}", "AS{0}", "AT{0}", "AU{0}", "AV{0}"
            , "AW{0}", "AX{0}", "AY{0}", "AZ{0}", "BA{0}", "BB{0}", "BC{0}", "BD{0}", "BE{0}", "BF{0}", "BG{0}", "BH{0}", "BI{0}", "BJ{0}", "BK{0}", "BL{0}"};

            Sheet.Cells["A1"].Value = "Id Visita";
            Sheet.Cells["B1"].Value = "Tipo prestador";
            Sheet.Cells["C1"].Value = "No id prestador";
            Sheet.Cells["D1"].Value = "Razon social";
            Sheet.Cells["E1"].Value = "Regional";
            Sheet.Cells["F1"].Value = "Cod SAP";
            Sheet.Cells["G1"].Value = "Fecha visita cronograma";
            Sheet.Cells["H1"].Value = "Fecha real visita";
            Sheet.Cells["I1"].Value = "Periodo fecha inicio";
            Sheet.Cells["J1"].Value = "Periodo fecha final";
            Sheet.Cells["K1"].Value = "Calificacion final";
            Sheet.Cells["L1"].Value = "Observaciones";
            Sheet.Cells["M1"].Value = "Auditor";

            int row = 2;

            foreach (Ref_regional reg in regionales)
            {
                ConsultaGnralList = Model.GetConsultageneralVisitas(reg.id_ref_regional, prestador, fechainicial, null, null);
                List<item_capitulo> Indicadores = BusClass.Getitemcapbyindcap(reg.id_ref_regional, prestador, null).ToList();

                if (ConsultaGnralList.Count > 0)
                {
                    foreach (ManagementConsultaGnralVisitasResult item in ConsultaGnralList)
                    {
                        Sheet.Cells["A" + row].Value = item.id_cronograma_visitas;
                        Sheet.Cells["B" + row].Value = item.tipo_prestador;
                        Sheet.Cells["C" + row].Value = item.no_id_prestador;
                        Sheet.Cells["D" + row].Value = item.razon_social;
                        Sheet.Cells["E" + row].Value = item.indice;
                        Sheet.Cells["F" + row].Value = item.cod_sap;
                        Sheet.Cells["G" + row].Value = item.fecha_visita_cronograma;
                        Sheet.Cells["H" + row].Value = item.fecha_real_visita;
                        Sheet.Cells["I" + row].Value = item.periodo_fecha_inicio;
                        Sheet.Cells["J" + row].Value = item.periodo_fecha_final;
                        Sheet.Cells["K" + row].Value = item.calificacion_final_visita;
                        Sheet.Cells["L" + row].Value = item.observaciones_evaluacion;
                        Sheet.Cells["M" + row].Value = item.nombre;


                        int i = 0;
                        foreach (item_capitulo item2 in Indicadores)
                        {
                            Sheet.Cells[columnas_1[i] + "1"].Value = item2.nom_item;
                            cronograma_visita_detalle resultado = Model.Getresultadovisitaindicador(item.id_cronograma_visitas, item2.id_item);
                            if (resultado != null)
                            {
                                if (resultado.aplica.Value)
                                {
                                    Sheet.Cells[string.Format(columnas_2[i], row)].Value = resultado.valor_digitado;
                                }
                                else
                                {
                                    Sheet.Cells[string.Format(columnas_2[i], row)].Value = "N/A";
                                }

                            }
                            else
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = null;

                            }

                            i = i + 1;
                        }
                        row++;
                    }
                }
            }

            string namefile = "Resultados_visitasdecalidad";
            Sheet.Cells["A:M"].AutoFitColumns();

            if (ConsultaGnralList.Count > 0)
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
            }
        }

        public void ExportarVisitaCalidadEspecifica(string nit, string codsap, DateTime fechadesde, int especifico_tipoprestador)
        {

            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            List<ManagementConsultaGnralVisitasResult> ConsultaGnralList = new List<ManagementConsultaGnralVisitasResult>();

            calidad_prestadores prestador = new calidad_prestadores();
            if (!string.IsNullOrEmpty(nit))
            {
                prestador = BusClass.getprestadoresList().Where(l => l.no_id_prestador == nit).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(codsap) && prestador == null)
            {
                prestador = BusClass.getprestadoresList().Where(l => l.cod_sap == codsap).FirstOrDefault();
            }

            List<Ref_regional> regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == prestador.id_regional).ToList();


            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

            Sheet.Cells["A1:BL1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:BL1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:BL1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:BL1"].Style.Font.Color.SetColor(Color.White);
            Sheet.Cells["A1:BL1"].Style.Font.Name = "Century Gothic";


            string[] columnas_1 = new string[50] { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL" };
            string[] columnas_2 = new string[50] { "N{0}", "O{0}", "P{0}", "Q{0}", "R{0}", "S{0}", "T{0}", "U{0}", "V{0}", "W{0}", "X{0}", "Y{0}", "Z{0}", "AA{0}", "AB{0}", "AC{0}", "AD{0}", "AF{0}", "AG{0}", "AH{0}", "AI{0}", "AJ{0}", "AK{0}", "AL{0}", "AM{0}", "AN{0}", "AO{0}", "AP{0}", "AQ{0}", "AR{0}", "AS{0}", "AT{0}", "AU{0}", "AV{0}"
            , "AW{0}", "AX{0}", "AY{0}", "AZ{0}", "BA{0}", "BB{0}", "BC{0}", "BD{0}", "BE{0}", "BF{0}", "BG{0}", "BH{0}", "BI{0}", "BJ{0}", "BK{0}", "BL{0}"};

            Sheet.Cells["A1"].Value = "Id Visita";
            Sheet.Cells["B1"].Value = "Tipo prestador";
            Sheet.Cells["C1"].Value = "No id prestador";
            Sheet.Cells["D1"].Value = "Razon social";
            Sheet.Cells["E1"].Value = "Regional";
            Sheet.Cells["F1"].Value = "Cod SAP";
            Sheet.Cells["G1"].Value = "Fecha visita cronograma";
            Sheet.Cells["H1"].Value = "Fecha real visita";
            Sheet.Cells["I1"].Value = "Periodo fecha inicio";
            Sheet.Cells["J1"].Value = "Periodo fecha final";
            Sheet.Cells["K1"].Value = "Calificacion final";
            Sheet.Cells["L1"].Value = "Observaciones";
            Sheet.Cells["M1"].Value = "Auditor";


            int row = 2;

            foreach (Ref_regional reg in regionales)
            {
                ConsultaGnralList = Model.GetConsultageneralVisitas(reg.id_ref_regional, especifico_tipoprestador, fechadesde, nit, prestador.cod_sap);
                List<item_capitulo> Indicadores = BusClass.Getitemcapbyindcap(reg.id_ref_regional, especifico_tipoprestador, null).ToList();

                if (ConsultaGnralList.Count > 0)
                {
                    foreach (ManagementConsultaGnralVisitasResult item in ConsultaGnralList)
                    {
                        Sheet.Cells["A" + row].Value = item.id_cronograma_visitas;
                        Sheet.Cells["B" + row].Value = item.tipo_prestador;
                        Sheet.Cells["C" + row].Value = item.no_id_prestador;
                        Sheet.Cells["D" + row].Value = item.razon_social;
                        Sheet.Cells["E" + row].Value = item.indice;
                        Sheet.Cells["F" + row].Value = item.cod_sap;
                        Sheet.Cells["G" + row].Value = item.fecha_visita_cronograma;
                        Sheet.Cells["H" + row].Value = item.fecha_real_visita;
                        Sheet.Cells["I" + row].Value = item.periodo_fecha_inicio;
                        Sheet.Cells["J" + row].Value = item.periodo_fecha_final;
                        Sheet.Cells["K" + row].Value = item.calificacion_final_visita;
                        Sheet.Cells["L" + row].Value = item.observaciones_evaluacion;
                        Sheet.Cells["M" + row].Value = item.nombre;


                        int i = 0;
                        foreach (item_capitulo item2 in Indicadores)
                        {
                            Sheet.Cells[columnas_1[i] + "1"].Value = item2.nom_item;
                            cronograma_visita_detalle resultado = Model.Getresultadovisitaindicador(item.id_cronograma_visitas, item2.id_item);
                            if (resultado != null)
                            {
                                if (resultado.aplica.Value)
                                {
                                    Sheet.Cells[string.Format(columnas_2[i], row)].Value = resultado.valor_digitado;
                                }
                                else
                                {
                                    Sheet.Cells[string.Format(columnas_2[i], row)].Value = "N/A";
                                }

                            }
                            else
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = null;

                            }

                            i = i + 1;
                        }
                        row++;
                    }
                }

            }

            string namefile = "Resultados_visitasdecalidad";
            Sheet.Cells["A:M"].AutoFitColumns();

            if (ConsultaGnralList.Count > 0)
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
            }
        }

        public void ExportarVisitaCalidadCalificaciones(int txtregional, int txtprestador, DateTime txtfechainicial)
        {
            List<ManagementConsultaGnralVisitasResult> ConsultaGnralList = new List<ManagementConsultaGnralVisitasResult>();
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            List<Ref_regional> regionales = new List<Ref_regional>();

            if (txtregional == 8)
            {
                regionales = BusClass.GetRefRegion();
            }
            else
            {
                regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == txtregional).ToList();
            }

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");

            Sheet.Cells["A1:AZ1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:Z1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:Z1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:Z1"].Style.Font.Color.SetColor(Color.White);

            string[] columnas_1 = new string[7] { "N", "O", "P", "Q", "R", "S", "T" };
            string[] columnas_2 = new string[7] { "N{0}", "O{0}", "P{0}", "Q{0}", "R{0}", "S{0}", "T{0}" };

            Sheet.Cells["A1"].Value = "Id Visita";
            Sheet.Cells["B1"].Value = "Tipo prestador";
            Sheet.Cells["C1"].Value = "No id prestador";
            Sheet.Cells["D1"].Value = "Razon social";
            Sheet.Cells["E1"].Value = "Regional";
            Sheet.Cells["F1"].Value = "Cod SAP";
            Sheet.Cells["G1"].Value = "Fecha visita cronograma";
            Sheet.Cells["H1"].Value = "Fecha real visita";
            Sheet.Cells["I1"].Value = "Periodo fecha inicio";
            Sheet.Cells["J1"].Value = "Periodo fecha final";
            Sheet.Cells["K1"].Value = "Calificacion final";
            Sheet.Cells["L1"].Value = "Observaciones";
            Sheet.Cells["M1"].Value = "Auditor";

            int row = 2;

            var existenDatos = 0;

            foreach (Ref_regional reg in regionales)
            {
                List<ManagementCalidadDtllIndicadorResult> capitulos = BusClass.GetCapitulosEvaluadosByIndicador(txtprestador, reg.id_ref_regional, ref MsgRes);

                ConsultaGnralList = Model.GetConsultageneralVisitas(reg.id_ref_regional, txtprestador, txtfechainicial, null, null);

                if (ConsultaGnralList.Count > 0)
                {
                    existenDatos = 1;

                    foreach (ManagementConsultaGnralVisitasResult item in ConsultaGnralList)
                    {
                        List<cronograma_visitas_calificaciones> calificaciones = BusClass.GetCalificacionesVisita(item.id_cronograma_visitas);

                        Sheet.Cells["A" + row].Value = item.id_cronograma_visitas;
                        Sheet.Cells["B" + row].Value = item.tipo_prestador;
                        Sheet.Cells["C" + row].Value = item.no_id_prestador;
                        Sheet.Cells["D" + row].Value = item.razon_social;
                        Sheet.Cells["E" + row].Value = item.indice;
                        Sheet.Cells["F" + row].Value = item.cod_sap;
                        Sheet.Cells["G" + row].Value = item.fecha_visita_cronograma;
                        Sheet.Cells["H" + row].Value = item.fecha_real_visita;
                        Sheet.Cells["I" + row].Value = item.periodo_fecha_inicio;
                        Sheet.Cells["J" + row].Value = item.periodo_fecha_final;
                        Sheet.Cells["K" + row].Value = item.calificacion_final_visita;
                        Sheet.Cells["L" + row].Value = item.observaciones_evaluacion;
                        Sheet.Cells["M" + row].Value = item.nombre;

                        Sheet.Cells[string.Format("G{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("H{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("I{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                        int i = 0;
                        foreach (ManagementCalidadDtllIndicadorResult item2 in capitulos)
                        {
                            var result = calificaciones.Where(l => l.capitulo_id == item2.id_capitulo).FirstOrDefault();
                            Sheet.Cells[columnas_1[i] + "1"].Value = item2.nom_capitulo;
                            if (result != null)
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = result.calificacion;
                            }
                            else
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = null;
                            }

                            i = i + 1;
                        }
                        row++;
                    }
                }
            }

            string namefile = "Resultados_visitasdecalidad";
            Sheet.Cells["A:AZ"].AutoFitColumns();

            if (existenDatos > 0)
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
            }
        }

        public void ExportarVisitaCalidadEspecificaCalifi(string nit2, string codsap2, DateTime fechadesde2, int especifico_tipoprestador2)
        {

            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            List<ManagementConsultaGnralVisitasResult> ConsultaGnralList = new List<ManagementConsultaGnralVisitasResult>();

            calidad_prestadores prestador = new calidad_prestadores();
            if (!string.IsNullOrEmpty(nit2))
            {
                prestador = BusClass.getprestadoresList().Where(l => l.no_id_prestador == nit2).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(codsap2) && prestador == null)
            {
                prestador = BusClass.getprestadoresList().Where(l => l.cod_sap == codsap2).FirstOrDefault();
            }

            List<Ref_regional> regionales = BusClass.GetRefRegion().Where(l => l.id_ref_regional == prestador.id_regional).ToList();


            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Resultados");
            ExcelWorksheet Sheet2 = Ep.Workbook.Worksheets.Add("Indicadores");

            Sheet.Cells["A1:AZ1"].Style.Font.Bold = true;
            Color colFromHex = Color.FromArgb(22, 54, 92);
            Sheet.Cells["A1:AZ1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["A1:AZ1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            Sheet.Cells["A1:AZ1"].Style.Font.Color.SetColor(Color.White);

            string[] columnas_1 = new string[7] { "N", "O", "P", "Q", "R", "S", "T" };
            string[] columnas_2 = new string[7] { "N{0}", "O{0}", "P{0}", "Q{0}", "R{0}", "S{0}", "T{0}" };

            Sheet.Cells["A1"].Value = "Id Visita";
            Sheet.Cells["B1"].Value = "Tipo prestador";
            Sheet.Cells["C1"].Value = "No id prestador";
            Sheet.Cells["D1"].Value = "Razon social";
            Sheet.Cells["E1"].Value = "Regional";
            Sheet.Cells["F1"].Value = "Cod SAP";
            Sheet.Cells["G1"].Value = "Fecha visita cronograma";
            Sheet.Cells["H1"].Value = "Fecha real visita";
            Sheet.Cells["I1"].Value = "Periodo fecha inicio";
            Sheet.Cells["J1"].Value = "Periodo fecha final";
            Sheet.Cells["K1"].Value = "Calificacion final";
            Sheet.Cells["L1"].Value = "Observaciones";
            Sheet.Cells["M1"].Value = "Auditor";
            Sheet2.Cells["A1"].Value = "Indicador";
            Sheet2.Cells["B1"].Value = "Nombre Indicador";

            int row = 2;


            List<capitulo_indicador> capitulos = BusClass.GetCapitulosByIndicador(especifico_tipoprestador2, prestador.id_regional, ref MsgRes);

            foreach (Ref_regional reg in regionales)
            {
                ConsultaGnralList = Model.GetConsultageneralVisitas(reg.id_ref_regional, especifico_tipoprestador2, fechadesde2, nit2, prestador.cod_sap);


                if (ConsultaGnralList.Count > 0)
                {
                    foreach (ManagementConsultaGnralVisitasResult item in ConsultaGnralList)
                    {
                        List<cronograma_visitas_calificaciones> calificaciones = BusClass.GetCalificacionesVisita(item.id_cronograma_visitas);

                        Sheet.Cells["A" + row].Value = item.id_cronograma_visitas;
                        Sheet.Cells["B" + row].Value = item.tipo_prestador;
                        Sheet.Cells["C" + row].Value = item.no_id_prestador;
                        Sheet.Cells["D" + row].Value = item.razon_social;
                        Sheet.Cells["E" + row].Value = item.indice;
                        Sheet.Cells["F" + row].Value = item.cod_sap;
                        Sheet.Cells["G" + row].Value = item.fecha_visita_cronograma;
                        Sheet.Cells["H" + row].Value = item.fecha_real_visita;
                        Sheet.Cells["I" + row].Value = item.periodo_fecha_inicio;
                        Sheet.Cells["J" + row].Value = item.periodo_fecha_final;
                        Sheet.Cells["K" + row].Value = item.calificacion_final_visita;
                        Sheet.Cells["L" + row].Value = item.observaciones_evaluacion;
                        Sheet.Cells["M" + row].Value = item.nombre;


                        int i = 0;
                        foreach (capitulo_indicador item2 in capitulos)
                        {
                            var result = calificaciones.Where(l => l.capitulo_id == item2.capitulo_id).FirstOrDefault();
                            Sheet.Cells[columnas_1[i] + "1"].Value = item2.capitulos.nom_capitulo;
                            if (result != null)
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = result.calificacion;
                            }
                            else
                            {
                                Sheet.Cells[string.Format(columnas_2[i], row)].Value = null;
                            }

                            i = i + 1;
                        }
                        row++;
                    }
                }
            }

            string namefile = "Resultados_visitasdecalidad";
            Sheet.Cells["A:AZ"].AutoFitColumns();


            if (ConsultaGnralList.Count > 0)
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                string rta = "<script LANGUAGE='JavaScript'>" +
                "window.alert('No se han encontrado resultados');" +
                "</script> ";
                Response.Write(rta);
            }
        }

        /*Metodo interno utilizado para actualizar las tabla de calificaciones*/
        public void actualizarcalificaciones()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<cronograma_visitas> listadovisitas = db.cronograma_visitas.Where(l => l.Realizo_evaluacion == true).ToList();

            foreach (var item in listadovisitas)
            {
                List<cronograma_visita_detalle> detalles_digitados = db.cronograma_visita_detalle.Where(l => l.id_cronograma_visita == item.id_cronograma_visitas).ToList();
                if (detalles_digitados.Count > 0)
                {
                    List<capitulo_indicador> capituloindicador = Model.GetCapitulosByIndicador(item.calidad_prestadores.tipo_prestador, item.calidad_prestadores.id_regional);

                    foreach (capitulo_indicador item2 in capituloindicador)
                    {
                        double? puntuacion = 0;
                        decimal totalpuntuacion = 0, total = 0;
                        int contador = 0;

                        List<item_capitulo> itemcapitulo = Model.Getitemcapbyindcap(item.calidad_prestadores.id_regional, item.calidad_prestadores.tipo_prestador.Value, item2.capitulo_id);

                        foreach (ECOPETROL_COMMON.ENTIDADES.item_capitulo item3 in itemcapitulo)
                        {
                            var detalle_digitado = detalles_digitados.Where(l => l.id_item == item3.id_item && l.id_capitulo == item3.capitulo_id).FirstOrDefault();
                            if (detalle_digitado != null)
                            {
                                if (detalle_digitado.aplica.Value)
                                {
                                    contador += 1;
                                    #region
                                    switch (item3.condicion_meta)
                                    {
                                        case "1":
                                            //signo_condicion = "=";
                                            if (detalle_digitado.valor_digitado == item3.valor_meta)
                                            {
                                                puntuacion = Convert.ToDouble(item3.Peso_individual);
                                            }
                                            else
                                            {
                                                if (item3.valor_meta > 0.5)
                                                {
                                                    puntuacion = (detalle_digitado.valor_digitado * Convert.ToDouble(item3.Peso_individual)) / 100;
                                                }
                                                else
                                                {
                                                    puntuacion = 0;
                                                }
                                            }
                                            break;
                                        case "2":
                                            //signo_condicion = "<=";
                                            if (detalle_digitado.valor_digitado <= item3.valor_meta)
                                            {
                                                puntuacion = Convert.ToDouble(item3.Peso_individual);
                                            }
                                            else
                                            {

                                                if (item3.valor_meta > 0.5)
                                                {
                                                    if (item3.valor_meta < detalle_digitado.valor_digitado)
                                                    {
                                                        puntuacion = 0;
                                                    }
                                                    else
                                                    {
                                                        puntuacion = (detalle_digitado.valor_digitado * Convert.ToDouble(item3.Peso_individual)) / 100;
                                                    }
                                                }
                                                else
                                                {
                                                    puntuacion = 0;
                                                }
                                            }
                                            break;
                                        case "3":
                                            //signo_condicion = "<";
                                            if (detalle_digitado.valor_digitado < item3.valor_meta)
                                            {
                                                puntuacion = Convert.ToDouble(item3.Peso_individual);
                                            }
                                            else
                                            {
                                                if (item3.valor_meta > 0.5)
                                                {
                                                    if (item3.valor_meta < detalle_digitado.valor_digitado)
                                                    {
                                                        puntuacion = 0;
                                                    }
                                                    else
                                                    {
                                                        puntuacion = (detalle_digitado.valor_digitado * Convert.ToDouble(item3.Peso_individual)) / 100;
                                                    }
                                                }
                                                else
                                                {
                                                    puntuacion = 0;
                                                }
                                            }
                                            break;
                                        case "4":
                                            //signo_condicion = ">=";
                                            if (detalle_digitado.valor_digitado >= item3.valor_meta)
                                            {
                                                puntuacion = Convert.ToDouble(item3.Peso_individual);
                                            }
                                            else
                                            {
                                                if (item3.valor_meta > 0.5)
                                                {
                                                    puntuacion = (detalle_digitado.valor_digitado * Convert.ToDouble(item3.Peso_individual)) / 100;
                                                }
                                                else
                                                {
                                                    puntuacion = 0;
                                                }
                                            }
                                            break;
                                        default:
                                            //signo_condicion = ">";
                                            if (detalle_digitado.valor_digitado > item3.valor_meta)
                                            {
                                                puntuacion = Convert.ToDouble(item3.Peso_individual);
                                            }
                                            else
                                            {
                                                if (item3.valor_meta > 0.5)
                                                {
                                                    puntuacion = (detalle_digitado.valor_digitado * Convert.ToDouble(item3.Peso_individual)) / 100;
                                                }
                                                else
                                                {
                                                    puntuacion = 0;
                                                }
                                            }
                                            break;
                                    }

                                    #endregion

                                }
                                else
                                {
                                    puntuacion = Convert.ToDouble(item3.Peso_individual);
                                }

                                double? pm = 0;
                                if (detalle_digitado.aplica.Value)
                                {
                                    if (puntuacion > 0)
                                    {
                                        pm = (puntuacion * 100) / Convert.ToDouble(item3.Peso_individual);
                                    }
                                    else
                                    {
                                        pm = (puntuacion * 100);
                                    }


                                    Math.Round(pm.Value, 2);
                                }

                                totalpuntuacion += Convert.ToDecimal(pm);
                            }
                        }
                        decimal promedio = 0;
                        if (totalpuntuacion > 0)
                        {
                            if (contador == 0)
                            {
                                promedio = 0;
                            }
                            else
                            {
                                promedio = totalpuntuacion / contador;
                            }
                        }

                        total = Convert.ToDecimal((promedio * item2.peso_general_capitulo) / 100);

                        double totaldefinitivo = Math.Round(Convert.ToDouble(total), 2);

                        cronograma_visitas_calificaciones obj = new cronograma_visitas_calificaciones();
                        obj.cronograma_visita_id = item.id_cronograma_visitas;
                        obj.capitulo_id = item2.capitulo_id.Value;
                        obj.calificacion = Convert.ToDecimal(totaldefinitivo);
                        db.cronograma_visitas_calificaciones.InsertOnSubmit(obj);
                        db.SubmitChanges();
                    }
                }
            }

        }

        public JsonResult GuardarNovedadVisita(int visitaid, int tiponovedad, string novedad)
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            cronograma_visita_novedades obj = new cronograma_visita_novedades();
            obj.id_cronograma_visitas = visitaid;
            obj.id_novedad = tiponovedad;
            if (!string.IsNullOrEmpty(novedad))
            {
                obj.otra_novedad = novedad;
            }
            obj.usuario_digita = SesionVar.UserName;
            obj.fecha_digita = DateTime.Now;
            Model.InsertarNovedadVisita(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { rta = 1, Msg = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { rta = 2, Msg = MsgRes.DescriptionResponse }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportarActas()
        {
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            return View();
        }


        //public void ExportarActasVisitas(int regional, int mes, int año)
        //{
        //    AsaludEcopetrol.Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
        //    try
        //    {

        //        List<vw_visitas_documentos> docs = Model.GetActasVisitas(regional, mes, año);

        //        if (docs.Count == 0)
        //        {
        //            string rta = "<script LANGUAGE='JavaScript'>" +
        //            "window.alert('No se han encontrado resultados');" +
        //            "</script> ";
        //            Response.Write(rta);
        //            Response.End();
        //        }
        //        else
        //        {
        //            using (ZipFile zip = new ZipFile())
        //            {
        //                int count = 0;
        //                foreach (var item in docs)
        //                {
        //                    var rutaArchivo = item.ruta;


        //                    Binary binary2 = item.documento;
        //                    if (binary2.Length == 0)
        //                        continue;
        //                    byte[] array = binary2.ToArray();
        //                    zip.AddEntry(item.Nom_documento + ".pdf", array);
        //                    count++;
        //                }
        //                using (MemoryStream salida = new MemoryStream())
        //                {
        //                    zip.Save(salida);

        //                    Response.Clear();
        //                    Response.ContentType = "application/zip";
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=ActasVisitas_" + mes + "_" + año + ".zip");
        //                    Response.BinaryWrite(salida.ToArray());
        //                    Response.End();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        string rta = "<script LANGUAGE='JavaScript'>" +
        //        "window.alert('ERROR EN LA DESCARGA');" +
        //        "</script> ";
        //        Response.Write(rta);
        //        Response.End();
        //    }
        //}


        public void ExportarActasVisitas(int regional, int mes, int año)
        {
            AsaludEcopetrol.Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            try
            {

                List<vw_visitas_documentos> docs = Model.GetActasVisitas(regional, mes, año);

                if (docs.Count == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                    "window.alert('No se han encontrado resultados');" +
                    "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
                else
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        int count = 0;

                        foreach (var item in docs)
                        {
                            var rutaArchivo = item.ruta;

                            // Abrimos un stream para leer el archivo desde la ruta
                            using (FileStream fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
                            {
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream); // Copiamos el contenido del archivo al MemoryStream
                                    byte[] array = memoryStream.ToArray(); // Convertimos el stream a un array de bytes

                                    if (array.Length == 0)
                                        continue; // Si el archivo está vacío, lo omitimos

                                    zip.AddEntry(item.Nom_documento + ".pdf", array); // Añadimos el archivo al ZIP con su nombre
                                    count++;
                                }
                            }
                        }

                        using (MemoryStream salida = new MemoryStream())
                        {
                            zip.Save(salida);

                            Response.Clear();
                            Response.ContentType = "application/zip";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=ActasVisitas_" + mes + "_" + año + ".zip");
                            Response.BinaryWrite(salida.ToArray());
                            Response.End();
                        }
                    }


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

        public string ObtenertiposPrestador(int idprestador)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<vw_calidad_prestador_indicador> indicadoresprestador = Model.GetListIndicadoresPrestador(idprestador);

            foreach (var item in indicadoresprestador)
            {
                result += "<option value='" + item.tipo_prestador + "'>" + item.nom_tipo_prestador + "</option>";
            }

            return result;
        }

        [ValidateInput(false)]
        public ActionResult CargueContratos(string nit, string sap, string mensaje)
        {
            List<management_contratos_listadoResult> listadoContratos = new List<management_contratos_listadoResult>();
            var conteo = 0;

            try
            {
                if (!string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(sap))
                {
                    listadoContratos = BusClass.listadoContratos();
                }

                if (!string.IsNullOrEmpty(nit))
                {
                    listadoContratos = listadoContratos.Where(x => x.nit_proveedor.Contains(nit)).ToList();
                }
                if (!string.IsNullOrEmpty(sap))
                {
                    listadoContratos = listadoContratos.Where(x => x.documento_SAP.Contains(sap)).ToList();
                }

                conteo = listadoContratos.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = listadoContratos;
            ViewBag.conteo = conteo;
            ViewBag.mensaje = mensaje;

            return View();
        }

        public PartialViewResult TableroControlContratos(string nit, string sap)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CargueContratosGuardar(List<HttpPostedFileBase> files)
        {
            var mensaje = "";
            var path = "";
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();

            try
            {
                if (files.Count() > 0)
                {
                    HttpPostedFileBase archivo = files[0];

                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(archivo.InputStream, asposeOptions);
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

                    contratos_cargue obj = new contratos_cargue();
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = SesionVar.UserName;

                    Int32 lote = Model.CargueMasivodeContratos(dataTable, obj, ref MsgRes);

                    var resultado = MsgRes.ResponseType;
                    var mensajeSalida = MsgRes.DescriptionResponse;
                    var idUsuario = SesionVar.IDUser;

                    if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        mensaje = "SE INGRESÓ CORRECTAMENTE CARGUE MASIVO CONTRATOS #" + lote;
                    }
                    else
                    {
                        mensaje = "ERROR AL INGRESAR CARGUE CONTRATOS: " + MsgRes.DescriptionResponse;
                    }
                }
                else
                {
                    mensaje += "SELECCIONE UN ARCHIVO VALIDO";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "NO SE CARGARON LOS REGISTROS:" + error;

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
                }
            }

            return RedirectToAction("CargueContratos", "ProcesosInternos", new { mensaje = mensaje });
        }

        public ActionResult GestionContrato(int? idContrato)
        {
            contratos_detalle contrato = new contratos_detalle();
            try
            {
                contrato = BusClass.MostrarDatosContratoId(idContrato);

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewData["MensajeRta"] = "";

            return View(contrato);
        }

        [HttpPost]
        public ActionResult GestionContrato(contratos_detalle obj)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                var respuesta = BusClass.ActualizarContrato(obj, ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {
                    ViewData["MensajeRta"] = "<div class='alert alert-success' role='alert'><strong>Éxito! </strong>SE ACTUALIZÓ CORRECTAMENTE </div>";
                }
                else
                {
                    mensaje = "ERROR AL INGRESAR LAS PREFACTURAS";
                    ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR AL INGRESAR LAS PREFACTURAS: " + error;
                ViewData["MensajeRta"] = "<div class='alert alert-danger' role='alert'>" + mensaje + "</div>";
            }

            return View(obj);
        }
        #endregion


        #region OPTIMIZACION REPORTES EVALUACION DE VISITAS DE CALIDAD

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de abril de 2023
        /// Metodo para leer y guardar masivamente todos los documentos de las visitas tramitadas
        /// </summary>
        public void exportarVisitasaDocumentoFisico()
        {
            Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
            List<cronograma_visitas_reportesDoc_evaluacion_calidad> documentos = new List<cronograma_visitas_reportesDoc_evaluacion_calidad>();

            try
            {
                string filename = "", carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "PDFSEvaluacionVisitasDeCalidad";
                }
                else
                {
                    carpeta = "PDFSEvaluacionVisitasDeCalidadPruebas";
                }

                string rutaFacturaEscaneada = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

                if (!Directory.Exists(rutaFacturaEscaneada)) { Directory.CreateDirectory(rutaFacturaEscaneada); }


                List<Management_Consulta_Cronograma_VisitasResult> Lista = Model.ConsultaCronogramaVisitasProcedimiento(2, null).Where(l => string.IsNullOrEmpty(l.ruta_documento_evaluacion_calidad)).ToList();
                int contador = 200;

                foreach (var item in Lista)
                {
                    string vistapdf = "";

                    try
                    {
                        Rotativa.Core.DriverOptions opciones = new Rotativa.Core.DriverOptions();
                        opciones.PageSize = Rotativa.Core.Options.Size.Letter;
                        opciones.PageMargins = new Rotativa.Core.Options.Margins(15, 10, 20, 10);
                        opciones.PageOrientation = Orientation.Portrait;

                        //vw_visitas visita = Model.ConsultaCronogramaVisitas(item.id_cronograma_visitas).FirstOrDefault();
                        List<cronograma_visita_detalle_indicador> capitulos = Model.ConsultaCronogramaVisitaDetalleInd(item.id_cronograma_visitas).ToList();

                        Models.ProcesosInternos.reportevisitas model2 = new Models.ProcesosInternos.reportevisitas();
                        model2.listacapitulos = capitulos;
                        model2.NomRegional = item.nombre_regional;
                        model2.NoContrato = item.num_contrato;
                        model2.Observaciones = item.Observaciones_Evaluacion;
                        model2.proximafecha = item.proxima_fecha_visita;
                        model2.Nit = item.no_id_prestador;
                        model2.NomPrestador = item.razon_social.ToUpper();
                        model2.NomAuditor = item.nombre.ToUpper();
                        model2.NomRepresentante = item.nom_representante_legal.ToUpper();
                        model2.idregional = item.id_regional;
                        model2.tipoindicador = item.tipo_prestador.Value;
                        model2.idcronograma = item.id_cronograma_visitas;
                        model2.periodo_desde = item.periodo_fecha_inicio;
                        model2.periodo_hasta = item.periodo_fecha_final;
                        model2.fecha_final_visita = item.fecha_final_visita;
                        model2.fecha_visita = item.fecha_visita;
                        model2.fechamodificacion = item.fecha_modificacion;

                        if (item.version_pdf == 1)
                        {
                            vistapdf = "EvaluacionIndicadoresPDF";
                        }
                        else
                        {
                            vistapdf = "EvaluacionIndicadoresPDF2";
                        }

                        var file = new ViewAsPdf(vistapdf, model2)
                        {
                            FileName = "ReporteEvalucaionCalidad_" + item.id_cronograma_visitas + ".pdf",
                            RotativaOptions = opciones
                        };
                        var byteArray = file.BuildPdf(ControllerContext);
                        System.IO.File.WriteAllBytes(Path.Combine(rutaFacturaEscaneada, "ReporteEvalucaionCalidad_" + item.id_cronograma_visitas + ".pdf"), byteArray);

                        cronograma_visitas_reportesDoc_evaluacion_calidad doc = new cronograma_visitas_reportesDoc_evaluacion_calidad();
                        doc.id_cronograma_visitas = item.id_cronograma_visitas;
                        doc.ruta_documento = Path.Combine(rutaFacturaEscaneada, "ReporteEvalucaionCalidad_" + item.id_cronograma_visitas + ".pdf");
                        doc.fecha_digita = DateTime.Now;
                        documentos.Add(doc);

                        contador = contador + 1;

                        if (contador == 200)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }
                }

                if (documentos.Count > 0)
                {
                    Model.InsertarMasivamenteReportesEvaluacionVisitas(documentos, ref MsgRes);
                }
            }
            catch
            {

            }

        }

        public ActionResult AbiriDocumentoEvaluacionVisitas(int idCronograma)
        {
            try
            {
                Models.ProcesosInternos.ProcesosInternos Model = new Models.ProcesosInternos.ProcesosInternos();
                Management_get_info_visita_por_idResult visita = Model.GetInfoVisitaById(idCronograma);
                string vistapdf = "";

                if (visita != null)
                {
                    if (!string.IsNullOrEmpty(visita.ruta_documento_evaluacion_calidad))
                    {
                        string dirpath = Path.Combine(Request.PhysicalApplicationPath, visita.ruta_documento_evaluacion_calidad);
                        if (System.IO.File.Exists(dirpath))
                        {
                            return File(dirpath, "application/pdf");
                        }
                        else
                        {
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar el archivo porque no existe la ruta de acceso." });
                        }
                    }
                    else
                    {
                        string filename = "", carpeta = "";

                        if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                        {
                            carpeta = "PDFSEvaluacionVisitasDeCalidad";
                        }
                        else
                        {
                            carpeta = "PDFSEvaluacionVisitasDeCalidadPruebas";
                        }

                        string rutaFacturaEscaneada = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

                        Rotativa.Core.DriverOptions opciones = new Rotativa.Core.DriverOptions();
                        opciones.PageSize = Rotativa.Core.Options.Size.Letter;
                        opciones.PageMargins = new Rotativa.Core.Options.Margins(15, 10, 20, 10);
                        opciones.PageOrientation = Orientation.Portrait;

                        List<cronograma_visita_detalle_indicador> capitulos = Model.ConsultaCronogramaVisitaDetalleInd(visita.id_cronograma_visitas).ToList();

                        Models.ProcesosInternos.reportevisitas model2 = new Models.ProcesosInternos.reportevisitas();
                        model2.listacapitulos = capitulos;
                        model2.NomRegional = visita.nombre_regional;
                        model2.NoContrato = visita.num_contrato;
                        model2.Observaciones = visita.Observaciones_Evaluacion;
                        model2.proximafecha = visita.proxima_fecha_visita;
                        model2.Nit = visita.no_id_prestador;
                        model2.NomPrestador = visita.razon_social.ToUpper();
                        model2.NomAuditor = visita.nombre.ToUpper();
                        model2.NomRepresentante = visita.nom_representante_legal.ToUpper();
                        model2.idregional = visita.id_regional;
                        model2.tipoindicador = visita.tipo_prestador.Value;
                        model2.idcronograma = visita.id_cronograma_visitas;
                        model2.periodo_desde = visita.periodo_fecha_inicio;
                        model2.periodo_hasta = visita.periodo_fecha_final;
                        model2.fecha_final_visita = visita.fecha_final_visita;
                        model2.fecha_visita = visita.fecha_visita;
                        model2.fechamodificacion = visita.fecha_modificacion;

                        if (visita.version_pdf == 1)
                        {
                            vistapdf = "EvaluacionIndicadoresPDF";
                        }
                        else
                        {
                            vistapdf = "EvaluacionIndicadoresPDF2";
                        }

                        var file = new ViewAsPdf(vistapdf, model2)
                        {
                            FileName = "ReporteEvaluacionCalidad_" + visita.id_cronograma_visitas + ".pdf",
                            RotativaOptions = opciones
                        };
                        var byteArray = file.BuildPdf(ControllerContext);

                        string dirpath = Path.Combine(rutaFacturaEscaneada, "ReporteEvaluacionCalidad_" + visita.id_cronograma_visitas + ".pdf");

                        System.IO.File.WriteAllBytes(dirpath, byteArray);

                        List<cronograma_visitas_reportesDoc_evaluacion_calidad> documentos = new List<cronograma_visitas_reportesDoc_evaluacion_calidad>();
                        cronograma_visitas_reportesDoc_evaluacion_calidad doc = new cronograma_visitas_reportesDoc_evaluacion_calidad();
                        doc.id_cronograma_visitas = visita.id_cronograma_visitas;
                        doc.ruta_documento = dirpath;
                        doc.fecha_digita = DateTime.Now;
                        documentos.Add(doc);

                        Model.InsertarMasivamenteReportesEvaluacionVisitas(documentos, ref MsgRes);
                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            return File(dirpath, "application/pdf");
                        }
                        else
                        {
                            return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al guardar el registro del documento" });

                        }
                    }
                }
                else
                {
                    return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "No se ha podido visualizar porque no se ha encontrado la visita" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ControlErrores", "Usuario", new { Mensaje = "Ha ocurrido un error al momento de procesar la solicitud." });
            }
        }
        #endregion
    }
}