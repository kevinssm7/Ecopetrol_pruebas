using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Campañas;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Campañas
{
    [SessionExpireFilter]
    public class CampañasController : Controller
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

        public ActionResult CreacionCampañas()
        {
            ViewBag.conteo = 0;
            return View();
        }

        public PartialViewResult CreacionCampañaPreguntas(int? conteo)
        {
            ViewBag.conteo = conteo;
            ViewBag.tipoPregunta = BusClass.TraerTipoPreguntaCampaña();
            return PartialView();
        }

        /// <summary>
        /// Guarda la creación de la campaña
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public JsonResult GuardarCreacionCampana(Models.Campañas.CreacionCampaña data)
        {
            string titulo = data.Titulo;
            string descripcion = data.Descripcion;
            string[][] arrayBidimensionalPreguntas = data.ArrayBidimensionalPreguntas;
            string[][] arrayRespuestaBreve = data.ArrayRespuestaBreve;
            string[][][] arrayOpcionMultiple = data.ArrayOpcionMultiple;
            string[][][] arrayCasillasVerificacion = data.ArrayCasillasVerificacion;
            string[][][] arrayListaDesplegable = data.ArrayListaDesplegable;
            string[][] arrayCargaArchivos = data.ArrayCargaArchivos;
            string[][] arrayCargaFecha = data.arrayCargaFecha;

            var mensaje = "";
            var rta = false;
            List<creacion_campana_detalle> camposDetalle = new List<creacion_campana_detalle>();
            List<creacion_campana_camposSimples> camposSimples = new List<creacion_campana_camposSimples>();
            List<creacion_campana_listas> camposListas = new List<creacion_campana_listas>();

            var conteoRecorridoArrayArchivos = 0;
            var conteoRecorridoArrayFechas = 0;

            try
            {
                creacion_campana campana = new creacion_campana();
                campana.titulo = titulo;
                campana.descripcion = descripcion;
                campana.fecha_digita = DateTime.Now;
                campana.usuario_digita = SesionVar.UserName;
                campana.estado = 1;
                campana.version = 1;

                var idCampana = BusClass.InsertarCreacionCampanas(campana);

                var posicion = 1;
                if (idCampana != 0)
                {
                    arrayBidimensionalPreguntas = arrayBidimensionalPreguntas
                    .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                    .ToArray();

                    for (var i = 0; i < arrayBidimensionalPreguntas.Length; i++)
                    {
                        var idPregunta = arrayBidimensionalPreguntas[i][0];

                        creacion_campana_detalle det = new creacion_campana_detalle();
                        det.id_campana = idCampana;
                        det.pregunta = arrayBidimensionalPreguntas[i][1];
                        det.tipo_pregunta = Convert.ToInt32(arrayBidimensionalPreguntas[i][2]);
                        det.posicion = Convert.ToInt32(arrayBidimensionalPreguntas[i][0]);
                        det.obligatoria = Convert.ToInt32(arrayBidimensionalPreguntas[i][3]);

                        det.usuario_digita = SesionVar.UserName;
                        det.fecha_digita = DateTime.Now;
                        det.version = 1;
                        det.estado = 1;

                        int idDetalle = BusClass.InsertarCreacionCampanasDetalle(det);

                        if (idDetalle != 0)
                        {
                            var tipoPregunta = det.tipo_pregunta;

                            if (det.tipo_pregunta == 1 || det.tipo_pregunta == 5 || det.tipo_pregunta == 6)
                            {
                                creacion_campana_camposSimples simple = new creacion_campana_camposSimples();

                                simple.id_campana = idCampana;
                                simple.id_detalle = idDetalle;
                                simple.tipo = tipoPregunta;
                                simple.posicion = posicion;

                                if (tipoPregunta == 5)
                                {
                                    if (arrayCargaArchivos != null)
                                    {
                                        arrayCargaArchivos = arrayCargaArchivos
                                        .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                        .ToArray();

                                        arrayCargaArchivos = arrayCargaArchivos
                                        .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                        .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                        .ToArray();

                                        if (arrayCargaArchivos.Length > 0)
                                        {
                                            var tamaño = arrayCargaArchivos.Length - 1;
                                            var variableValida = arrayCargaArchivos[conteoRecorridoArrayArchivos][0];

                                            if (Convert.ToInt32(variableValida) == Convert.ToInt32(idPregunta))
                                            {
                                                var archivosEspecificos = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][1]);

                                                if (archivosEspecificos == 1)
                                                {
                                                    simple.soloArchivos_especificos = 1;
                                                    simple.extension_archivo = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][3]);
                                                    simple.cantidad_maximaArchivos = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][2]);
                                                }
                                                else
                                                {
                                                    simple.soloArchivos_especificos = 0;
                                                    simple.extension_archivo = 0;
                                                    simple.cantidad_maximaArchivos = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][2]);
                                                }
                                            }
                                        }
                                        conteoRecorridoArrayArchivos++;
                                    }
                                }

                                if (tipoPregunta == 6)
                                {
                                    if (arrayCargaFecha != null)
                                    {
                                        arrayCargaFecha = arrayCargaFecha
                                        .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                        .ToArray();

                                        arrayCargaFecha = arrayCargaFecha
                                        .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                        .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                        .ToArray();

                                        var variableValida = arrayCargaFecha[conteoRecorridoArrayFechas][0];

                                        if (arrayCargaFecha.Length > 0)
                                        {
                                            if (Convert.ToInt32(variableValida) == posicion)
                                            {
                                                simple.tipo_fecha = Convert.ToInt32(arrayCargaFecha[conteoRecorridoArrayFechas][1]);
                                            }
                                        }
                                    }
                                    conteoRecorridoArrayFechas++;
                                }

                                simple.fecha_digita = DateTime.Now;
                                simple.usuario = SesionVar.UserName;
                                simple.version = 1;
                                simple.estado = 1;

                                camposSimples.Add(simple);
                            }
                            else
                            {
                                List<creacion_campana_listas> lista = new List<creacion_campana_listas>();

                                //multiples
                                if (tipoPregunta == 2)
                                {
                                    arrayOpcionMultiple = arrayOpcionMultiple
                                    .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                    .ToArray();

                                    arrayOpcionMultiple = arrayOpcionMultiple
                                    .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                    .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                    .ToArray();

                                    if (arrayOpcionMultiple != null)
                                    {
                                        if (arrayOpcionMultiple.Length > 0)
                                        {
                                            foreach (var array in arrayOpcionMultiple)
                                            {
                                                foreach (var array2 in array)
                                                {
                                                    if (array2 != null)
                                                    {
                                                        var datos = array2;

                                                        if (datos[1] == posicion.ToString())
                                                        {
                                                            if (datos[2] != null)
                                                            {
                                                                creacion_campana_listas lis = new creacion_campana_listas();
                                                                lis.id_detalle = idDetalle;
                                                                lis.id_campana = idCampana;
                                                                lis.id_tipo = 1;
                                                                lis.posicion = Convert.ToInt32(datos[1]);
                                                                lis.opcion = datos[2];
                                                                lis.fecha_digita = DateTime.Now;
                                                                lis.usuario_digita = SesionVar.UserName;
                                                                lis.posicion = posicion;
                                                                lis.version = 1;
                                                                lis.estado = 1;
                                                                camposListas.Add(lis);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                                //Verificación
                                else if (tipoPregunta == 3)
                                {
                                    arrayCasillasVerificacion = arrayCasillasVerificacion
                                    .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                    .ToArray();

                                    arrayCasillasVerificacion = arrayCasillasVerificacion
                                    .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                    .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                    .ToArray();

                                    if (arrayCasillasVerificacion != null)
                                    {
                                        if (arrayCasillasVerificacion.Length > 0)
                                        {
                                            foreach (var array in arrayCasillasVerificacion)
                                            {
                                                foreach (var array2 in array)
                                                {
                                                    if (array2 != null)
                                                    {
                                                        var datos = array2;
                                                        if (datos[1] == posicion.ToString())
                                                        {
                                                            if (datos[2] != null)
                                                            {
                                                                creacion_campana_listas lis = new creacion_campana_listas();
                                                                lis.id_detalle = idDetalle;
                                                                lis.id_campana = idCampana;
                                                                lis.id_tipo = 2;
                                                                lis.posicion = Convert.ToInt32(datos[1]);
                                                                lis.opcion = datos[2];
                                                                lis.fecha_digita = DateTime.Now;
                                                                lis.usuario_digita = SesionVar.UserName;
                                                                lis.posicion = posicion;
                                                                lis.version = 1;
                                                                lis.estado = 1;
                                                                camposListas.Add(lis);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //selects
                                else if (tipoPregunta == 4)
                                {
                                    arrayListaDesplegable = arrayListaDesplegable
                                    .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                    .ToArray();

                                    arrayListaDesplegable = arrayListaDesplegable
                                    .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                    .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                    .ToArray();

                                    if (arrayListaDesplegable != null)
                                    {
                                        if (arrayListaDesplegable.Length > 0)
                                        {
                                            foreach (var array in arrayListaDesplegable)
                                            {
                                                foreach (var array2 in array)
                                                {
                                                    if (array2 != null)
                                                    {
                                                        var datos = array2;
                                                        if (datos[1] == posicion.ToString())
                                                        {
                                                            if (datos[2] != null)
                                                            {
                                                                creacion_campana_listas lis = new creacion_campana_listas();
                                                                lis.id_detalle = idDetalle;
                                                                lis.id_campana = idCampana;
                                                                lis.id_tipo = 3;
                                                                lis.opcion = datos[2];
                                                                lis.posicion = Convert.ToInt32(datos[1]);
                                                                lis.fecha_digita = DateTime.Now;
                                                                lis.usuario_digita = SesionVar.UserName;
                                                                lis.posicion = posicion;
                                                                lis.version = 1;
                                                                lis.estado = 1;
                                                                camposListas.Add(lis);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        posicion++;
                    }

                    camposListas = camposListas.Where(x => x.id_detalle != null).ToList();
                    camposSimples = camposSimples.Where(x => x.id_detalle != null).ToList();

                    var insertaListas = BusClass.InsertarCreacionCampanasDetalleListados(camposListas, camposSimples);

                    if (insertaListas != 0)
                    {
                        mensaje = "CAMPAÑAS INGRESADAS CORRECTAMENTE";
                        rta = true;
                    }
                    else
                    {
                        mensaje = "ERROR EN EL INGRESO DE LA CAMPAÑA";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA INSERCIÓN DE LA CAMPAÑA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult TableroControlCampañas()
        {
            List<management_campana_tableroControlResult> listado = new List<management_campana_tableroControlResult>();
            List<management_campana_tableroControlResult> listadoActivo = new List<management_campana_tableroControlResult>();
            ref_campanas_permisosEdicion permiso = new ref_campanas_permisosEdicion();
            var conteo = 0;
            var conteoActivo = 0;
            var acceso = 0;

            try
            {
                permiso = BusClass.traerPermisosEdicionCampana(SesionVar.IDUser);

                if (permiso != null)
                {
                    acceso = 1;
                }

                listado = BusClass.ListadoCampanas();
                conteo = listado.Count();

                listadoActivo = listado.Where(x => x.estado == 1).ToList();
                conteoActivo = listadoActivo.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.listadoActivo = listadoActivo;
            ViewBag.conteo = conteo;
            ViewBag.conteoActivo = conteoActivo;
            ViewBag.rol = SesionVar.ROL;
            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.acceso = acceso;

            return View();
        }

        public ActionResult RespuestaCampañas(int? idCampana)
        {
            creacion_campana campana = new creacion_campana();
            var titulo = "";
            var descripcion = "";
            List<creacion_campana_detalle> listaPreguntas = new List<creacion_campana_detalle>();
            List<creacion_campana_camposSimples> camposSimples = new List<creacion_campana_camposSimples>();
            List<creacion_campana_listas> camposLista = new List<creacion_campana_listas>();

            var idPreguntas = "";
            var idTipoPreguntas = "";

            try
            {
                campana = BusClass.TraerCampanaGeneralId(idCampana);
                titulo = campana.titulo;
                descripcion = campana.descripcion;

                listaPreguntas = BusClass.TraerCampanaGeneraDetallelId(idCampana);

                foreach (var item in listaPreguntas)
                {
                    if (idPreguntas == "")
                    {
                        idPreguntas = Convert.ToString(item.id_detalle);
                        idTipoPreguntas = Convert.ToString(item.tipo_pregunta);
                    }
                    else
                    {
                        idPreguntas += "," + Convert.ToString(item.id_detalle);
                        idTipoPreguntas += "," + Convert.ToString(item.tipo_pregunta);
                    }
                }

                camposSimples = BusClass.TraerCampanaCamposSimplesIdCampana(idCampana);
                camposLista = BusClass.TraerCampanaCamposListaIdCampana(idCampana);
            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.idCampana = idCampana;
            ViewBag.titulo = titulo;
            ViewBag.descripcion = descripcion;

            ViewBag.listaPreguntas = listaPreguntas;
            ViewBag.camposSimples = camposSimples;
            ViewBag.camposLista = camposLista;
            ViewBag.idPreguntas = idPreguntas;
            ViewBag.idTipoPreguntas = idTipoPreguntas;

            return View();
        }

        public JsonResult GuardarRespuestas(Models.Campañas.GuardarCampaña data)
        {
            var mensaje = "";
            var rta = 0;
            List<campana_respuestas> listaRespuestas = new List<campana_respuestas>();
            List<campana_respuestas_tipoVerificaciones> verificaciones = new List<campana_respuestas_tipoVerificaciones>();
            int? idLote = 0;

            try
            {
                int? idCampaña = data.idCampaña;
                string[][] arrayListasPreguntas = data.arrayListasPreguntas;
                string[][] ArraySimplesFechas = data.ArraySimplesFechas;
                string[][] ArraySimples = data.ArraySimples;
                string[][] ArrayListasSelect = data.ArrayListasSelect;
                string[][] ArrayListasMultiple = data.ArrayListasMultiple;
                string[][][] ArrayListasVerificacion = data.ArrayListasVerificacion;

                HttpPostedFileBase[] archi = data.ArraySimplesArchivos2;

                List<HttpPostedFileBase> ArraySimplesArchivos = new List<HttpPostedFileBase>();
                ArraySimplesArchivos = data.ArraySimplesArchivos;
                string[] conteoArchivos = data.conteoArchivos;

                campana_respuestas_lote lote = new campana_respuestas_lote();
                lote.id_campana = idCampaña;
                lote.fecha_digita = DateTime.Now;
                lote.usuario_digita = SesionVar.UserName;

                idLote = BusClass.InsertarLoteCampaña(lote);

                foreach (var preguntas in arrayListasPreguntas)
                {
                    var idPregunta = Convert.ToInt32(preguntas[0]);
                    var tipoPregunta = Convert.ToInt32(preguntas[1]);
                    campana_respuestas respuesta = new campana_respuestas();
                    respuesta.id_campana = idCampaña;
                    respuesta.id_pregunta = idPregunta;
                    respuesta.tipo_pregunta = tipoPregunta;
                    respuesta.fecha_digita = DateTime.Now;
                    respuesta.usuario_digita = SesionVar.UserName;
                    respuesta.id_lote = idLote;

                    if (tipoPregunta == 1)
                    {
                        foreach (var simples in ArraySimples)
                        {
                            var id = Convert.ToInt32(simples[0]);
                            if (id == idPregunta)
                            {
                                respuesta.pregunta_corta = simples[2];
                            }
                        }
                    }
                    else if (tipoPregunta == 2)
                    {
                        foreach (var multi in ArrayListasMultiple)
                        {
                            var id = Convert.ToInt32(multi[0]);
                            if (id == idPregunta)
                            {
                                if (multi[2] != null)
                                {
                                    respuesta.pregunta_radio = Convert.ToInt32(multi[2]);
                                }
                            }
                        }
                    }

                    else if (tipoPregunta == 3)
                    {

                        campana_respuestas respuestaVeri = new campana_respuestas();
                        respuestaVeri = respuesta;

                        foreach (var ver in ArrayListasVerificacion)
                        {
                            var idPreguntaVeri = Convert.ToInt32(ver[0][0]);

                            if (ver.Length > 2)
                            {
                                var resultados = ver[2];

                                if (idPreguntaVeri == idPregunta)
                                {
                                    if (resultados != null)
                                    {
                                        var idVeri = BusClass.IngresarRespuestaCampañaVerificacion_Archivos(respuestaVeri);

                                        foreach (var res in resultados)
                                        {
                                            campana_respuestas_tipoVerificaciones veri = new campana_respuestas_tipoVerificaciones();

                                            veri.id_campana = idCampaña;
                                            veri.id_pregunta = idPregunta;
                                            veri.id_respuesta = idVeri;
                                            veri.opcion = Convert.ToInt32(res);
                                            veri.fecha_digita = DateTime.Now;
                                            veri.usuario_digita = SesionVar.UserName;

                                            verificaciones.Add(veri);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (tipoPregunta == 4)
                    {
                        foreach (var sele in ArrayListasSelect)
                        {
                            var id = Convert.ToInt32(sele[0]);
                            if (id == idPregunta)
                            {
                                if (sele[2] != null)
                                {
                                    respuesta.pregunta_select = Convert.ToInt32(sele[2]);
                                }
                            }
                        }
                    }

                    else if (tipoPregunta == 6)
                    {
                        foreach (var fec in ArraySimplesFechas)
                        {
                            var id = Convert.ToInt32(fec[0]);
                            if (id == idPregunta)
                            {
                                if (fec[2] != null)
                                {
                                    respuesta.pregunta_fecha = Convert.ToDateTime(fec[2]);
                                }
                            }
                        }
                    }

                    if (tipoPregunta != 3 && tipoPregunta != 5)
                    {
                        listaRespuestas.Add(respuesta);
                    }
                }


                if (data.existeArchivos == 1)
                {
                    rta = 2;
                }

                if (verificaciones.Count() > 0)
                {
                    var insertaVerificaciones = BusClass.insertarRespuestasCampanaVerificaciones(verificaciones, ref MsgRes);
                }

                if (listaRespuestas.Count() > 0)
                {
                    var insertaRespuestas = BusClass.insertarRespuestasCamapana(listaRespuestas, ref MsgRes);
                }

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto || data.existeArchivos == 1)
                {
                    mensaje = "RESPUESTAS INGRESADAS CORRECTAMENTE";

                    if (data.existeArchivos == 1)
                    {
                        rta = 2;
                    }
                    else
                    {
                        rta = 1;
                    }
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LAS RESPUESTAS: " + MsgRes.DescriptionResponse;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LAS RESPUESTAS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta, idLote = idLote });
        }

        public JsonResult GuardadoArchivosCamapa(int? idCampaña, List<HttpPostedFileBase> archivos, string listaPreguntas, string listaConteoTotal, string listaConteo, int? idLote)
        {
            var mensaje = "";
            var rta = 0;

            try
            {
                if (archivos != null)
                {
                    string[] listPreguntas = listaPreguntas.Split(',');
                    string[] listConteoTotal = listaConteoTotal.Split(',');
                    string[] listConteo = listaConteo.Split(',');

                    var tamañoArchivos = archivos.Count();
                    var conteoRecorridoPreguntas = 0;
                    var conteoRecorridoArchivos = 0;
                    List<campana_respuestas_tipoArchivo> ListaArchivos = new List<campana_respuestas_tipoArchivo>();

                    if (archivos.Count() > 0)
                    {

                        foreach (var item in listPreguntas)
                        {
                            var idPregunta = Convert.ToInt32(item);

                            var conteoLocal = 0;

                            var tamañoLimitePregunta = Convert.ToInt32(listConteo[conteoRecorridoPreguntas]);

                            campana_respuestas respuesta = new campana_respuestas();

                            respuesta.id_campana = idCampaña;
                            respuesta.id_pregunta = Convert.ToInt32(item);
                            respuesta.tipo_pregunta = 5;
                            respuesta.fecha_digita = DateTime.Now;
                            respuesta.usuario_digita = SesionVar.UserName;
                            respuesta.id_lote = idLote;

                            var idRespuesta = BusClass.IngresarRespuestaCampañaVerificacion_Archivos(respuesta);

                            if (idRespuesta != 0)
                            {
                                foreach (var archivo in archivos)
                                {
                                    if (conteoLocal < tamañoLimitePregunta)
                                    {
                                        HttpPostedFileBase arc = archivos[conteoRecorridoArchivos];

                                        if (arc != null)
                                        {
                                            campana_respuestas_tipoArchivo datoArchivo = new campana_respuestas_tipoArchivo();
                                            datoArchivo.id_campana = idCampaña;
                                            datoArchivo.id_pregunta = idPregunta;
                                            datoArchivo.id_respuesta = idRespuesta;
                                            datoArchivo.extension = arc.ContentType;
                                            datoArchivo.nombre = arc.FileName;

                                            datoArchivo.ruta = guardarArchivo(idCampaña, idPregunta, idRespuesta, arc);

                                            datoArchivo.usuario_digita = SesionVar.UserName;
                                            datoArchivo.fecha_digita = DateTime.Now;

                                            ListaArchivos.Add(datoArchivo);
                                            conteoLocal++;
                                        }
                                        conteoRecorridoArchivos++;
                                    }

                                }
                            }
                            else
                            {
                                mensaje = "ERROR EN EL INGRESO DE LAS RESPUESTAS: " + MsgRes.DescriptionResponse;
                            }

                            conteoRecorridoPreguntas++;
                        }

                        if (ListaArchivos.Count() > 0)
                        {
                            var insertarDatosArchivos = BusClass.insertarRespuestasCampanaArchivos(ListaArchivos, ref MsgRes);
                            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                            {
                                mensaje = "RESPUESTAS INGRESADAS CORRECTAMENTE";
                                rta = 1;
                            }
                            else
                            {
                                mensaje = "ERROR EN EL INGRESO DE LAS RESPUESTAS: " + MsgRes.DescriptionResponse;
                            }
                        }
                    }
                    else
                    {
                        mensaje = "RESPUESTAS INGRESADAS CORRECTAMENTE";
                        rta = 1;
                    }
                }
                else
                {
                    mensaje = "RESPUESTAS INGRESADAS CORRECTAMENTE";
                    rta = 1;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LAS RESPUESTAS: " + error;
            }
            return Json(new { mensaje = mensaje, rta = rta });
        }

        public string guardarArchivo(int? idCampana, int? idPregunta, int? idRespuesta, HttpPostedFileBase file)
        {
            var rutaDevolucion = "";

            string strRetorno = string.Empty;
            StringBuilder sbRutaDefinitiva;
            string strRutaDefinitiva = string.Empty;
            string strError = string.Empty;

            try
            {
                strRutaDefinitiva = ConfigurationManager.AppSettings["rutaDocumentosCampañas"];

                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + file.FileName);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();

                DateTime fecha = DateTime.Now;
                string archivo = string.Empty;

                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "Campañas_" + idCampana;
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "CampañasPruebas_" + idCampana;
                }

                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta + "\\" + "pregunta_" + idPregunta);
                var nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmm}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                rutaDevolucion = archivo;


            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return rutaDevolucion;
        }

        public JsonResult ActualizarEstadosCampa(int? idCampana, int? estado)
        {
            var mensaje = "";
            try
            {
                var actualizaEstado = BusClass.DesactivarActivarCampaña(idCampana, estado);
                if (actualizaEstado != 0)
                {
                    mensaje = "ESTADO CAMPAÑA ACTUALIZADO CORRECTAMENTE";
                }
                else
                {
                    mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA: " + error;
            }

            return Json(new { mensaje = mensaje });

        }

        public ActionResult EditarPreguntas(int? idCampana)
        {
            creacion_campana campana = new creacion_campana();
            creacion_campana version = new creacion_campana();
            List<creacion_campana_detalle> preguntas = new List<creacion_campana_detalle>();
            List<creacion_campana_camposSimples> preguntasSimples = new List<creacion_campana_camposSimples>();
            List<creacion_campana_listas> preguntasListas = new List<creacion_campana_listas>();
            var idPreguntas = "";
            var idTipoPreguntas = "";
            var descripcionCampana = "";

            try
            {
                campana = BusClass.TraerCampanaGeneralId(idCampana);
                version = BusClass.TraerCampanaVersionGeneralId(idCampana);
                descripcionCampana = campana.descripcion;

                preguntas = BusClass.TraerCampanaGeneraDetallelId(idCampana);
                preguntasSimples = BusClass.TraerCampanaCamposSimplesIdCampana(idCampana);
                preguntasListas = BusClass.TraerCampanaCamposListaIdCampana(idCampana);

                foreach (var item in preguntas)
                {
                    if (idPreguntas == "")
                    {
                        idPreguntas = Convert.ToString(item.id_detalle);
                        idTipoPreguntas = Convert.ToString(item.tipo_pregunta);
                    }
                    else
                    {
                        idPreguntas += "," + Convert.ToString(item.id_detalle);
                        idTipoPreguntas += "," + Convert.ToString(item.tipo_pregunta);
                    }
                }


            }

            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.rol = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;

            ViewBag.idPreguntas = idPreguntas;
            ViewBag.idTipoPreguntas = idTipoPreguntas;
            ViewBag.descripcionCampana = descripcionCampana;

            ViewBag.idCampana = idCampana;
            ViewBag.campana = campana;
            ViewBag.preguntas = preguntas;
            ViewBag.preguntasSimples = preguntasSimples;
            ViewBag.preguntasListas = preguntasListas;


            return View();
        }

        public PartialViewResult EditarCampañaPreguntas(int? idPregunta, int? tipo, int? nueva)
        {
            creacion_campana_camposSimples preguntaSimple = new creacion_campana_camposSimples();
            List<creacion_campana_listas> preguntasListas = new List<creacion_campana_listas>();
            List<creacion_campana_listas> preguntasListasFiltradas = new List<creacion_campana_listas>();

            creacion_campana_detalle pregunta = new creacion_campana_detalle();
            var preguntaTexto = "";
            var idSimple = 0;
            var especificos = 0;
            var maximoArchivos = 0;
            var tipoExtension = 0;
            var idListas = "";
            var obligatoria = 0;

            if (nueva != 1)
            {
                try
                {
                    preguntaSimple = BusClass.TraerCampanaCamposSimplesIdDetalle(idPregunta);
                    preguntasListas = BusClass.TraerCampanaCamposListaIdDetalle(idPregunta);

                    if (preguntasListas.Count() > 0)
                    {
                        preguntasListas = preguntasListas.Where(x => x.estado == 1).ToList();
                    }

                    pregunta = BusClass.TraerDatosPreguntaCampana(idPregunta);
                    obligatoria = (int)pregunta.obligatoria;

                    preguntaTexto = pregunta.pregunta;

                    if (preguntaSimple != null)
                    {
                        idSimple = preguntaSimple.id_dato;

                        if (preguntaSimple.soloArchivos_especificos != null)
                        {
                            especificos = (int)preguntaSimple.soloArchivos_especificos;
                        }

                        if (preguntaSimple.cantidad_maximaArchivos != null)
                        {
                            maximoArchivos = (int)preguntaSimple.cantidad_maximaArchivos;
                        }

                        if (preguntaSimple.extension_archivo != null)
                        {
                            tipoExtension = (int)preguntaSimple.extension_archivo;
                        }
                    }

                    preguntasListasFiltradas = preguntasListas.Where(x => x.id_detalle == idPregunta).OrderBy(x => x.posicion).ToList();

                    if (preguntasListas.Count() > 0)
                    {

                        foreach (var item in preguntasListas)
                        {
                            if (idListas == "")
                            {
                                idListas = Convert.ToString(item.opcion);
                            }
                            else
                            {
                                idListas += "," + Convert.ToString(item.opcion);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }

            ViewBag.nueva = nueva;

            ViewBag.idPregunta = idPregunta;
            ViewBag.preguntaTexto = preguntaTexto;
            ViewBag.tipo = tipo;

            ViewBag.obligatoria = obligatoria;

            //Si el dato es pregunta simple, llena estas variables
            ViewBag.idSimple = idSimple;
            ViewBag.especificos = especificos;
            ViewBag.maximoArchivos = maximoArchivos;
            ViewBag.tipoExtension = tipoExtension;
            //fin


            //Datos de pregunta simple
            ViewBag.simples = preguntaSimple;

            //datos de preguntas lista
            ViewBag.listas = preguntasListas;

            ViewBag.rol = SesionVar.ROL;
            ViewBag.usuario = SesionVar.UserName;
            ViewBag.idListas = idListas;

            ViewBag.tipoPregunta = BusClass.TraerTipoPreguntaCampaña();

            return PartialView();
        }

        public JsonResult GuardarEdicionPreguntas(Models.Campañas.CreacionCampaña data)
        {
            int idCampana = (int)data.idCampana;

            string titulo = data.Titulo;
            string descripcion = data.Descripcion;
            string[][] arrayBidimensionalPreguntas = data.ArrayBidimensionalPreguntas;
            string[][] arrayRespuestaBreve = data.ArrayRespuestaBreve;
            string[][][] arrayOpcionMultiple = data.ArrayOpcionMultiple;
            string[][][] arrayCasillasVerificacion = data.ArrayCasillasVerificacion;
            string[][][] arrayListaDesplegable = data.ArrayListaDesplegable;
            string[][] arrayCargaArchivos = data.ArrayCargaArchivos;
            string[][] arrayCargaFecha = data.arrayCargaFecha;



            string[][] arrayBidimensionalPreguntasNuevas = data.ArrayBidimensionalPreguntasNuevas;
            string[][] arrayRespuestaBreveNuevas = data.ArrayRespuestaBreveNuevas;
            string[][][] arrayOpcionMultipleNuevas = data.ArrayOpcionMultipleNuevas;
            string[][][] arrayCasillasVerificacionNuevas = data.ArrayCasillasVerificacionNuevas;
            string[][][] arrayListaDesplegableNuevas = data.ArrayListaDesplegableNuevas;
            string[][] arrayCargaArchivosNuevas = data.ArrayCargaArchivosNuevas;
            string[][] arrayCargaFechaNuevas = data.arrayCargaFechaNuevas;
            var mensaje = "";
            var rta = false;

            creacion_campana creacion = new creacion_campana();
            List<creacion_campana_detalle> camposDetalle = new List<creacion_campana_detalle>();
            List<creacion_campana_camposSimples> camposSimples = new List<creacion_campana_camposSimples>();
            List<creacion_campana_listas> camposListas = new List<creacion_campana_listas>();

            List<creacion_campana_detalle> camposDetalleNueva = new List<creacion_campana_detalle>();
            List<creacion_campana_camposSimples> camposSimplesNueva = new List<creacion_campana_camposSimples>();
            List<creacion_campana_listas> camposListasNueva = new List<creacion_campana_listas>();

            var version = 0;

            var conteoRecorridoArrayArchivos = 0;
            var conteoRecorridoArrayFechas = 0;

            var conteoRecorridoArrayArchivosNueva = 0;
            var conteoRecorridoArrayFechasNueva = 0;

            try
            {
                creacion = BusClass.TraerCampanaGeneralId(idCampana);
                if (creacion != null)
                {
                    if (creacion.version != null)
                    {
                        version = (int)creacion.version;
                        if (version == 0)
                        {
                            version = 1;
                        }
                        else
                        {
                            version = version + 1;
                        }
                    }
                    else
                    {
                        version = 1;
                    }
                }

                creacion.version = version;
                creacion.titulo = titulo;
                creacion.descripcion = descripcion;
                creacion.usuario_digita = SesionVar.UserName;

                var actualizaCreacion = BusClass.ActualizarVersionCampaña(creacion);
                if (actualizaCreacion != 0)
                {
                    var posicion = 1;

                    //Consulta las preguntas activas de la campaña

                    List<creacion_campana_detalle> preguntasInactivas = new List<creacion_campana_detalle>();

                    preguntasInactivas = BusClass.ConsultaDtllPreguntasCampana(idCampana);

                    //Array de preguntas ya guardadas
                    arrayBidimensionalPreguntas = arrayBidimensionalPreguntas
                    .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                    .ToArray();

                    if (arrayBidimensionalPreguntas.Count() > 0)
                    {
                        foreach (var item in arrayBidimensionalPreguntas)
                        {
                            creacion_campana_detalle pregunta = new creacion_campana_detalle();
                            var idPregunta = Convert.ToInt32(item[0]);
                            var tipoPregunta = Convert.ToInt32(item[2]);

                            pregunta.id_detalle = idPregunta;
                            pregunta.id_campana = idCampana;
                            pregunta.pregunta = item[1];
                            pregunta.tipo_pregunta = Convert.ToInt32(item[2]);
                            pregunta.obligatoria = Convert.ToInt32(item[4]);
                            pregunta.estado = 1;
                            pregunta.version = version;

                            var actualiza = BusClass.ActualizarDatosCampañaPregunta(pregunta);
                            if (actualiza != 0)
                            {
                                var preguntaEncontrada = preguntasInactivas.Where(l => l.id_detalle == pregunta.id_detalle).FirstOrDefault();
                                preguntasInactivas.Remove(preguntaEncontrada);

                                //Cambia estados de todos los campos de pregunta, para que no se alteren los originales
                                var cambiaEstados = BusClass.ActualizarCamposPreguntas(idPregunta);

                                if (tipoPregunta == 1 || tipoPregunta == 5 || tipoPregunta == 6)
                                {
                                    creacion_campana_camposSimples simple = new creacion_campana_camposSimples();

                                    simple.id_campana = idCampana;
                                    simple.id_detalle = idPregunta;
                                    simple.tipo = tipoPregunta;
                                    simple.posicion = posicion;

                                    if (tipoPregunta == 5)
                                    {
                                        if (arrayCargaArchivos != null)
                                        {
                                            arrayCargaArchivos = arrayCargaArchivos
                                            .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                            .ToArray();

                                            arrayCargaArchivos = arrayCargaArchivos
                                            .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                            .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                            .ToArray();

                                            if (arrayCargaArchivos.Length > 0)
                                            {
                                                var tamaño = arrayCargaArchivos.Length - 1;
                                                var variableValida = arrayCargaArchivos[conteoRecorridoArrayArchivos][0];

                                                if (Convert.ToInt32(variableValida) == Convert.ToInt32(idPregunta))
                                                {
                                                    var archivosEspecificos = arrayCargaArchivos[conteoRecorridoArrayArchivos].Length - 1;

                                                    if (archivosEspecificos == 3)
                                                    {
                                                        simple.soloArchivos_especificos = 1;
                                                        simple.extension_archivo = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][3]);
                                                    }
                                                    else
                                                    {
                                                        simple.soloArchivos_especificos = 0;
                                                        simple.extension_archivo = 0;
                                                    }
                                                }
                                                simple.cantidad_maximaArchivos = Convert.ToInt32(arrayCargaArchivos[conteoRecorridoArrayArchivos][2]);
                                            }
                                            conteoRecorridoArrayArchivos++;
                                        }
                                    }

                                    if (tipoPregunta == 6)
                                    {
                                        if (arrayCargaFecha != null)
                                        {
                                            arrayCargaFecha = arrayCargaFecha
                                            .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                            .ToArray();

                                            arrayCargaFecha = arrayCargaFecha
                                            .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                            .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                            .ToArray();

                                            var tamaño = arrayCargaFecha.Length - 1;
                                            var variableValida = arrayCargaFecha[conteoRecorridoArrayFechas][0];

                                            if (arrayCargaFecha.Length > 0)
                                            {
                                                if (Convert.ToInt32(variableValida) == posicion)
                                                {
                                                    //simple.fecha = Convert.ToDateTime(arrayCargaFecha[tamaño][0]);
                                                }
                                            }
                                        }
                                        conteoRecorridoArrayFechas++;
                                    }

                                    simple.fecha_digita = DateTime.Now;
                                    simple.usuario = SesionVar.UserName;
                                    simple.version = version;
                                    simple.estado = 1;

                                    camposSimples.Add(simple);
                                }
                                else
                                {
                                    List<creacion_campana_listas> lista = new List<creacion_campana_listas>();

                                    //multiples
                                    if (tipoPregunta == 2)
                                    {
                                        arrayOpcionMultiple = arrayOpcionMultiple
                                        .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                        .ToArray();

                                        arrayOpcionMultiple = arrayOpcionMultiple
                                        .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                        .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                        .ToArray();

                                        if (arrayOpcionMultiple != null)
                                        {
                                            if (arrayOpcionMultiple.Length > 0)
                                            {
                                                foreach (var array in arrayOpcionMultiple)
                                                {
                                                    foreach (var array2 in array)
                                                    {
                                                        if (array2 != null)
                                                        {
                                                            var datos = array2;
                                                            var idDato = Convert.ToInt32(datos[1]);
                                                            if (idDato == idPregunta && datos[0] != "0")
                                                            {
                                                                if (datos[2] != null)
                                                                {
                                                                    creacion_campana_listas lis = new creacion_campana_listas();
                                                                    lis.id_detalle = idPregunta;
                                                                    lis.id_campana = idCampana;
                                                                    lis.id_tipo = 1;
                                                                    lis.posicion = Convert.ToInt32(datos[1]);
                                                                    lis.opcion = datos[2];
                                                                    lis.fecha_digita = DateTime.Now;
                                                                    lis.usuario_digita = SesionVar.UserName;
                                                                    lis.posicion = posicion;
                                                                    lis.version = version;
                                                                    lis.estado = 1;
                                                                    camposListas.Add(lis);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    //Verificación
                                    else if (tipoPregunta == 3)
                                    {
                                        arrayCasillasVerificacion = arrayCasillasVerificacion
                                        .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                        .ToArray();

                                        arrayCasillasVerificacion = arrayCasillasVerificacion
                                        .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                        .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                        .ToArray();

                                        if (arrayCasillasVerificacion != null)
                                        {
                                            if (arrayCasillasVerificacion.Length > 0)
                                            {
                                                foreach (var array in arrayCasillasVerificacion)
                                                {
                                                    foreach (var array2 in array)
                                                    {
                                                        if (array2 != null)
                                                        {
                                                            var datos = array2;
                                                            var idDato = Convert.ToInt32(datos[1]);
                                                            if (idDato == idPregunta && datos[0] != "0")
                                                            {
                                                                if (datos[2] != null)
                                                                {
                                                                    creacion_campana_listas lis = new creacion_campana_listas();
                                                                    lis.id_detalle = idPregunta;
                                                                    lis.id_campana = idCampana;
                                                                    lis.id_tipo = 2;
                                                                    lis.posicion = Convert.ToInt32(datos[1]);
                                                                    lis.opcion = datos[2];
                                                                    lis.fecha_digita = DateTime.Now;
                                                                    lis.usuario_digita = SesionVar.UserName;
                                                                    lis.posicion = posicion;
                                                                    lis.version = version;
                                                                    lis.estado = 1;
                                                                    camposListas.Add(lis);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //selects
                                    else if (tipoPregunta == 4)
                                    {
                                        arrayListaDesplegable = arrayListaDesplegable
                                        .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                        .ToArray();

                                        arrayListaDesplegable = arrayListaDesplegable
                                        .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                        .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                        .ToArray();

                                        if (arrayListaDesplegable != null)
                                        {
                                            if (arrayListaDesplegable.Length > 0)
                                            {
                                                foreach (var array in arrayListaDesplegable)
                                                {
                                                    foreach (var array2 in array)
                                                    {
                                                        if (array2 != null)
                                                        {
                                                            var datos = array2;
                                                            var idDato = Convert.ToInt32(datos[1]);
                                                            if (idDato == idPregunta && datos[0] != "0")
                                                            {
                                                                if (datos[2] != null)
                                                                {
                                                                    creacion_campana_listas lis = new creacion_campana_listas();
                                                                    lis.id_detalle = idPregunta;
                                                                    lis.id_campana = idCampana;
                                                                    lis.id_tipo = 3;
                                                                    lis.opcion = datos[2];
                                                                    lis.posicion = Convert.ToInt32(datos[1]);
                                                                    lis.fecha_digita = DateTime.Now;
                                                                    lis.usuario_digita = SesionVar.UserName;
                                                                    lis.posicion = posicion;
                                                                    lis.estado = 1;
                                                                    lis.version = version;
                                                                    camposListas.Add(lis);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";

                            }
                        }

                        camposListas = camposListas.Where(x => x.id_detalle != null).ToList();
                        camposSimples = camposSimples.Where(x => x.id_detalle != null).ToList();

                        var insertaListasNuevas = BusClass.InsertarCreacionCampanasDetalleListados(camposListas, camposSimples);

                        if (insertaListasNuevas != 0)
                        {
                            mensaje = "CAMPAÑAS ACTUALIZADAS CORRECTAMENTE";
                            rta = true;
                        }
                        else
                        {
                            mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";
                        }

                    }
                    else
                    {
                        mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";
                    }

                    BusClass.ActualizarInactivas(preguntasInactivas, ref MsgRes);

                    if (arrayBidimensionalPreguntasNuevas != null)
                    {
                        arrayBidimensionalPreguntasNuevas = arrayBidimensionalPreguntasNuevas
                      .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                      .ToArray();

                        if (arrayBidimensionalPreguntasNuevas.Count() > 0)
                        {
                            for (var i = 0; i < arrayBidimensionalPreguntasNuevas.Length; i++)
                            {
                                var idPregunta = arrayBidimensionalPreguntasNuevas[i][0];

                                creacion_campana_detalle det = new creacion_campana_detalle();
                                det.id_campana = idCampana;
                                det.pregunta = arrayBidimensionalPreguntasNuevas[i][1];
                                det.tipo_pregunta = Convert.ToInt32(arrayBidimensionalPreguntasNuevas[i][2]);
                                det.posicion = Convert.ToInt32(arrayBidimensionalPreguntasNuevas[i][0]);
                                det.obligatoria = Convert.ToInt32(arrayBidimensionalPreguntasNuevas[i][3]);

                                det.usuario_digita = SesionVar.UserName;
                                det.fecha_digita = DateTime.Now;
                                det.version = 1;
                                det.estado = 1;

                                int idDetalle = BusClass.InsertarCreacionCampanasDetalle(det);

                                if (idDetalle != 0)
                                {
                                    var tipoPregunta = det.tipo_pregunta;

                                    if (det.tipo_pregunta == 1 || det.tipo_pregunta == 5 || det.tipo_pregunta == 6)
                                    {
                                        creacion_campana_camposSimples simple = new creacion_campana_camposSimples();

                                        simple.id_campana = idCampana;
                                        simple.id_detalle = idDetalle;
                                        simple.tipo = tipoPregunta;
                                        simple.posicion = posicion;

                                        if (tipoPregunta == 5)
                                        {
                                            if (arrayCargaArchivosNuevas != null)
                                            {
                                                arrayCargaArchivosNuevas = arrayCargaArchivosNuevas
                                                .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                                .ToArray();

                                                arrayCargaArchivosNuevas = arrayCargaArchivosNuevas
                                                .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                                .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                                .ToArray();

                                                if (arrayCargaArchivosNuevas.Length > 0)
                                                {
                                                    var tamaño = arrayCargaArchivosNuevas.Length - 1;
                                                    var variableValida = arrayCargaArchivosNuevas[conteoRecorridoArrayArchivos][0];

                                                    if (Convert.ToInt32(variableValida) == Convert.ToInt32(idPregunta))
                                                    {
                                                        var archivosEspecificos = arrayCargaArchivosNuevas[conteoRecorridoArrayArchivos].Length - 1;

                                                        if (archivosEspecificos == 3)
                                                        {
                                                            simple.soloArchivos_especificos = 1;
                                                            simple.extension_archivo = Convert.ToInt32(arrayCargaArchivosNuevas[conteoRecorridoArrayArchivos][3]);
                                                        }
                                                        else
                                                        {
                                                            simple.soloArchivos_especificos = 0;
                                                            simple.extension_archivo = 0;
                                                        }
                                                    }
                                                    simple.cantidad_maximaArchivos = Convert.ToInt32(arrayCargaArchivosNuevas[conteoRecorridoArrayArchivos][2]);
                                                }
                                                conteoRecorridoArrayArchivosNueva++;
                                            }
                                        }

                                        if (tipoPregunta == 6)
                                        {
                                            if (arrayCargaFechaNuevas != null)
                                            {
                                                arrayCargaFechaNuevas = arrayCargaFechaNuevas
                                                .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                                .ToArray();

                                                arrayCargaFechaNuevas = arrayCargaFechaNuevas
                                                .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                                .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                                .ToArray();

                                                var tamaño = arrayCargaFechaNuevas.Length - 1;
                                                var variableValida = arrayCargaFechaNuevas[conteoRecorridoArrayFechasNueva][0];

                                                if (arrayCargaFechaNuevas.Length > 0)
                                                {
                                                    if (Convert.ToInt32(variableValida) == posicion)
                                                    {
                                                        //simple.fecha = Convert.ToDateTime(arrayCargaFecha[tamaño][0]);
                                                    }
                                                }
                                            }
                                            conteoRecorridoArrayFechasNueva++;
                                        }

                                        simple.fecha_digita = DateTime.Now;
                                        simple.usuario = SesionVar.UserName;
                                        simple.version = 1;
                                        simple.estado = 1;

                                        camposSimplesNueva.Add(simple);
                                    }
                                    else
                                    {
                                        List<creacion_campana_listas> lista = new List<creacion_campana_listas>();

                                        //multiples
                                        if (tipoPregunta == 2)
                                        {
                                            arrayOpcionMultipleNuevas = arrayOpcionMultipleNuevas
                                            .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                            .ToArray();

                                            arrayOpcionMultipleNuevas = arrayOpcionMultipleNuevas
                                            .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                            .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                            .ToArray();

                                            if (arrayOpcionMultipleNuevas != null)
                                            {
                                                if (arrayOpcionMultipleNuevas.Length > 0)
                                                {
                                                    foreach (var array in arrayOpcionMultipleNuevas)
                                                    {
                                                        foreach (var array2 in array)
                                                        {
                                                            if (array2 != null)
                                                            {
                                                                var datos = array2;

                                                                if (datos[1] == posicion.ToString())
                                                                {
                                                                    if (datos[2] != null)
                                                                    {
                                                                        creacion_campana_listas lis = new creacion_campana_listas();
                                                                        lis.id_detalle = idDetalle;
                                                                        lis.id_campana = idCampana;
                                                                        lis.id_tipo = 1;
                                                                        lis.posicion = Convert.ToInt32(datos[1]);
                                                                        lis.opcion = datos[2];
                                                                        lis.fecha_digita = DateTime.Now;
                                                                        lis.usuario_digita = SesionVar.UserName;
                                                                        lis.posicion = posicion;
                                                                        lis.version = 1;
                                                                        lis.estado = 1;
                                                                        camposListasNueva.Add(lis);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        //Verificación
                                        else if (tipoPregunta == 3)
                                        {
                                            arrayCasillasVerificacionNuevas = arrayCasillasVerificacionNuevas
                                            .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                            .ToArray();

                                            arrayCasillasVerificacionNuevas = arrayCasillasVerificacionNuevas
                                            .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                            .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                            .ToArray();

                                            if (arrayCasillasVerificacionNuevas != null)
                                            {
                                                if (arrayCasillasVerificacionNuevas.Length > 0)
                                                {
                                                    foreach (var array in arrayCasillasVerificacionNuevas)
                                                    {
                                                        foreach (var array2 in array)
                                                        {
                                                            if (array2 != null)
                                                            {
                                                                var datos = array2;
                                                                if (datos[1] == posicion.ToString())
                                                                {
                                                                    if (datos[2] != null)
                                                                    {
                                                                        creacion_campana_listas lis = new creacion_campana_listas();
                                                                        lis.id_detalle = idDetalle;
                                                                        lis.id_campana = idCampana;
                                                                        lis.id_tipo = 2;
                                                                        lis.posicion = Convert.ToInt32(datos[1]);
                                                                        lis.opcion = datos[2];
                                                                        lis.fecha_digita = DateTime.Now;
                                                                        lis.usuario_digita = SesionVar.UserName;
                                                                        lis.posicion = posicion;
                                                                        lis.version = 1;
                                                                        lis.estado = 1;
                                                                        camposListasNueva.Add(lis);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //selects
                                        else if (tipoPregunta == 4)
                                        {
                                            arrayListaDesplegableNuevas = arrayListaDesplegableNuevas
                                            .Where(arr => arr != null && arr.Any(innerArr => innerArr != null && innerArr.Length > 0))
                                            .ToArray();

                                            arrayListaDesplegableNuevas = arrayListaDesplegableNuevas
                                            .Select(arr => arr.Where(innerArr => innerArr != null && innerArr.Length > 0).ToArray())
                                            .Where(arr => arr.Any()) // Elimina los elementos nulos en la primera dimensión
                                            .ToArray();

                                            if (arrayListaDesplegableNuevas != null)
                                            {
                                                if (arrayListaDesplegableNuevas.Length > 0)
                                                {
                                                    foreach (var array in arrayListaDesplegableNuevas)
                                                    {
                                                        foreach (var array2 in array)
                                                        {
                                                            if (array2 != null)
                                                            {
                                                                var datos = array2;
                                                                if (datos[1] == posicion.ToString())
                                                                {
                                                                    if (datos[2] != null)
                                                                    {
                                                                        creacion_campana_listas lis = new creacion_campana_listas();
                                                                        lis.id_detalle = idDetalle;
                                                                        lis.id_campana = idCampana;
                                                                        lis.id_tipo = 3;
                                                                        lis.opcion = datos[2];
                                                                        lis.posicion = Convert.ToInt32(datos[1]);
                                                                        lis.fecha_digita = DateTime.Now;
                                                                        lis.usuario_digita = SesionVar.UserName;
                                                                        lis.posicion = posicion;
                                                                        lis.version = 1;
                                                                        lis.estado = 1;
                                                                        camposListasNueva.Add(lis);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                posicion++;
                            }

                            camposListasNueva = camposListasNueva.Where(x => x.id_detalle != null).ToList();
                            camposSimplesNueva = camposSimplesNueva.Where(x => x.id_detalle != null).ToList();

                            var insertaListas = BusClass.InsertarCreacionCampanasDetalleListados(camposListasNueva, camposSimplesNueva);

                            if (insertaListas != 0)
                            {
                                mensaje = "CAMPAÑAS ACTUALIZADAS CORRECTAMENTE";
                                rta = true;
                            }
                            else
                            {
                                mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";
                            }


                        }
                        else
                        {
                            mensaje = "CAMPAÑAS ACTUALIZADAS CORRECTAMENTE";
                            rta = true;
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    mensaje = "ERROR EN LA ACTUALIZACIÓN DE LA CAMPAÑA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public ActionResult EncuestaSAMI()
        {
            ViewBag.tipoPreguntas = BusClass.listaEncuestaSAMI();
            encuesta_sami encuesta = new encuesta_sami();
            var existeEncuesta = 0;

            try
            {
                encuesta = BusClass.TraerEncuestaEsteMes();
                if (encuesta != null)
                {
                    existeEncuesta = 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.existeEncuesta = existeEncuesta;

            return View();
        }

        public JsonResult RespuestaEncuestaSAMI(string[][] respuestas)
        {
            var rta = 0;
            var mensaje = "";
            encuesta_sami encuesta = new encuesta_sami();
            List<encuesta_sami_respuestas> listado = new List<encuesta_sami_respuestas>();

            try
            {
                encuesta.usuario_digita = SesionVar.UserName;
                encuesta.fecha_digita = DateTime.Now;

                var conteo = respuestas.Count();

                if (conteo > 0)
                {
                    for (var i = 1; i < conteo; i++)
                    {
                        encuesta_sami_respuestas datos = new encuesta_sami_respuestas();

                        if (respuestas[i][0] != null)
                        {
                            datos.id_pregunta = Convert.ToInt32(respuestas[i][0]);
                            var tipoPregunta = 0;

                            tipoPregunta = Convert.ToInt32(respuestas[i][1]);

                            if (tipoPregunta != 0)
                            {
                                if (tipoPregunta == 1)
                                {
                                    datos.calificacion = Convert.ToInt32(respuestas[i][2]);
                                }
                                else
                                {
                                    datos.encuesta_abierta = Convert.ToString(respuestas[i][2]);
                                }
                            }

                            datos.fecha_digita = DateTime.Now;
                            datos.usuario_digita = SesionVar.UserName;

                            listado.Add(datos);
                        }
                    }

                    if (listado.Count() > 0)
                    {
                        var respuesta = BusClass.InsertarRespuestaSAMI(encuesta, listado, ref MsgRes);

                        if (respuesta != 0)
                        {
                            mensaje = "ENCUESTA INGRESADA CORRECTAMENTE";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = "ENCUESTA INGRESADA CORRECTAMENTE: " + MsgRes.DescriptionResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ENCUESTA INGRESADA CORRECTAMENTE: " + ex.Message;
            }

            return Json(new { rta = rta, mensaje = mensaje });
        }

        public ActionResult TableroEncuestaSAMI()
        {
            List<management_encuesta_sami_datosResult> listado = new List<management_encuesta_sami_datosResult>();
            List<management_encuesta_sami_datos_detalleResult> listadoDetalle = new List<management_encuesta_sami_datos_detalleResult>();
            List<management_encuesta_sami_datos_promediosResult> promedios = new List<management_encuesta_sami_datos_promediosResult>();

            var conteo = 0;
            try
            {
                listado = BusClass.listaRespuestasSAMI();
                listadoDetalle = BusClass.listaRespuestasSAMIDetalle();
                promedios = BusClass.listaRespuestasSAMIPromedios();
                conteo = listado.Count();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.listado = listado;
            ViewBag.promedios = promedios;
            ViewBag.conteo = conteo;
            ViewBag.tipoPreguntas = BusClass.listaEncuestaSAMI();

            Session["listadoDetalle"] = listadoDetalle;
            Session["ListadoRespuestasPromedios"] = promedios;

            return View();
        }

        public PartialViewResult MostrarDetalleRespuestas(int? idLote)
        {
            List<management_encuesta_sami_datos_detalleResult> lista = new List<management_encuesta_sami_datos_detalleResult>();

            try
            {
                lista = (List<management_encuesta_sami_datos_detalleResult>)Session["listadoDetalle"];
                lista = lista.Where(x => x.id_lote == idLote).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.lista = lista;
            ViewBag.idLote = idLote;
            ViewBag.cuenta = lista.Count();

            return PartialView();
        }

        public void ExcelReporteCampanaId(int? idCampana)
        {
            List<management_campana_reporteIdResult> listareporte = new List<management_campana_reporteIdResult>();
            try
            {
                listareporte = BusClass.ListadoCampanasId(idCampana);

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosEncuesta");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AA1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AA1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AA1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AA1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AA1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Lote respuesta";
                Sheet.Cells["B1"].Value = "Id respuesta";
                Sheet.Cells["C1"].Value = "Id campana";
                Sheet.Cells["D1"].Value = "Título campana";
                Sheet.Cells["E1"].Value = "Id pregunta";
                Sheet.Cells["F1"].Value = "Pregunta";
                Sheet.Cells["G1"].Value = "Id tipo pregunta";
                Sheet.Cells["H1"].Value = "Tipo pregunta";
                Sheet.Cells["I1"].Value = "Respuesta corta";
                Sheet.Cells["J1"].Value = "Id respuesta radio";
                Sheet.Cells["K1"].Value = "respuesta radio";
                Sheet.Cells["L1"].Value = "Id respuesta select";
                Sheet.Cells["M1"].Value = "Respuesta select";
                Sheet.Cells["N1"].Value = "Respuesta fecha";
                Sheet.Cells["O1"].Value = "Fecha digita respuestas";
                Sheet.Cells["P1"].Value = "Nombre usuario contesta"; // Antes estaba en "Q1"
                Sheet.Cells["Q1"].Value = "id verificación";
                Sheet.Cells["R1"].Value = "Id Respuesta verificación";
                Sheet.Cells["S1"].Value = "Respuesta verificación";
                Sheet.Cells["T1"].Value = "Fecha digita verificación";
                Sheet.Cells["U1"].Value = "Nombre usuariodigita verificación ";
                Sheet.Cells["V1"].Value = "Id archivo"; // Antes estaba en "X1"
                Sheet.Cells["W1"].Value = "Extensión archivo"; // Antes estaba en "Y1"
                Sheet.Cells["X1"].Value = "Nombre archivo"; // Antes estaba en "Z1"
                Sheet.Cells["Y1"].Value = "Ruta archivo"; // Antes estaba en "AA1"
                Sheet.Cells["Z1"].Value = "Fecha digita archivo"; // Antes estaba en "AB1"
                Sheet.Cells["AA1"].Value = "Nombre digita archivo"; // Antes estaba en "AC1"

                int row = 2;
                foreach (management_campana_reporteIdResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.Lote_respuesta;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.Id_respuesta;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.Id_campana;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.Título_campana;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Id_pregunta;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.Pregunta;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.Id_tipo_pregunta;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.Tipo_pregunta;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.Respuesta_corta;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.Id_respuesta_radio;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.respuesta_radio;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.Id_respuesta_select;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.Respuesta_select;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.Respuesta_fecha;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.Fecha_digita_respuestas;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.Nombre_usuario_contesta;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.id_verificación;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.Id_Respuesta_verificación;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.Respuesta_verificación;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.Fecha_digita_verificación;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.Nombre_usuariodigita_verificación_;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.Id_archivo;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.Extensión_archivo;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.Nombre_archivo;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.Ruta_archivo;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.Fecha_digita_archivo;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.Nombre_digita_archivo;

                    //Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_digita.Value.ToString("dd/MM/yyyy");

                    Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("Z{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells["A" + row + ":AC1" + row].Style.Font.Name = "Century Gothic";

                    row++;
                }
                Sheet.Cells["A:AC"].AutoFitColumns();

                var nombreArchivo = "RptTableroControl_campanas_" + Convert.ToString(DateTime.Now);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelReporteRespuestas()
        {
            List<management_encuesta_sami_datos_detalleResult> listareporte = new List<management_encuesta_sami_datos_detalleResult>();
            try
            {
                listareporte = (List<management_encuesta_sami_datos_detalleResult>)Session["listadoDetalle"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosEncuesta");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:E1"].Style.Font.Bold = true;
                Sheet.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:E1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:E1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Id lote";
                Sheet.Cells["B1"].Value = "Usuario responde";
                Sheet.Cells["C1"].Value = "Pregunta";
                Sheet.Cells["D1"].Value = "Respuesta";
                Sheet.Cells["E1"].Value = "Fecha digita";

                int row = 2;
                foreach (management_encuesta_sami_datos_detalleResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nombreUsuario;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.descripcionPregunta;

                    if (item.tipoPregunta == "Selección")
                    {
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.calificacion;
                    }
                    else
                    {
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.encuesta_abierta;
                    }

                    Sheet.Cells[string.Format("E{0}", row)].Value = item.fecha_digita.Value.ToString("dd/MM/yyyy");

                    Sheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":E1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:E"].AutoFitColumns();

                var nombreArchivo = "RptTableroControl_encuesta_" + Convert.ToString(DateTime.Now);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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

        public void ExcelReporteRespuestasPromedios()
        {
            List<management_encuesta_sami_datos_promediosResult> listareporte = new List<management_encuesta_sami_datos_promediosResult>();
            try
            {
                listareporte = (List<management_encuesta_sami_datos_promediosResult>)Session["ListadoRespuestasPromedios"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosEncuestaPromedios");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:C1"].Style.Font.Bold = true;
                Sheet.Cells["A1:C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:C1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:C1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:C1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Ítem";
                Sheet.Cells["B1"].Value = "Pregunta";
                Sheet.Cells["C1"].Value = "Promedio";

                int row = 2;
                var i = 0;
                foreach (management_encuesta_sami_datos_promediosResult item in listareporte)
                {
                    i++;
                    Sheet.Cells[string.Format("A{0}", row)].Value = i;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.descripcion;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.promedio_calificaciones;

                    Sheet.Cells["A" + row + ":C1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:C"].AutoFitColumns();

                var nombreArchivo = "RptTableroControl_encuesta_promedios_" + Convert.ToString(DateTime.Now);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
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