using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using System.Configuration;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;
using Dapper;

namespace AsaludEcopetrol.Controllers.ContactCenter
{
    [SessionExpireFilter]
    public class CallCenterController : Controller
    {
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

        public ActionResult IngresoCaso(int? idcontactcenter, int? idCenso, int? idConcurrencia, int? tipoIngreso)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            management_contact_centerResult Modelo = new management_contact_centerResult();

            try
            {
                ViewBag.clasificacion = Model.GetListClasificacionContacto();
                ViewBag.tipoServicio = Model.GetListTipoServicio();
                ViewBag.tiposolicitud = Model.GetListTipoSolicitud();
                ViewBag.tiposolicitudBitacora = Model.GetListTipoSolicitudBitacora();
                ViewBag.estadosolicitud = Model.GetListEstadoSolicitud();
                ViewBag.mediosnotificacion = Model.GetListMediosNotificacion();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.auditor = BusClass.GetSisUsuarioactivo().Where(l => l.id_rol == 7).ToList();
                ViewBag.eps = BusClass.GetRefEps().Where(x => x.id_ref_eps == 17).ToList();
                ViewBag.tipificacion = Model.GetListTipificacion();
                ViewBag.agente = SesionVar.NombreUsuario;

                ViewBag.idCenso = idCenso;
                ViewBag.idcontactcenter = idcontactcenter;
                ViewBag.idConcurrencia = idConcurrencia;
                ViewBag.tipoIngreso = tipoIngreso;

                ViewBag.ips = BusClass.GetRefIps();

                ViewData["rta"] = 0;

                if (idcontactcenter == null && idCenso == null)
                {
                    Session["bitacora"] = new List<BitacoraSeguimiento>();
                    return View(Modelo);
                }
                else
                {
                    List<contact_center_dtll> List = Model.GetListBitacoraByIngreso(idcontactcenter.Value, idCenso, idConcurrencia);
                    List<BitacoraSeguimiento> bitacora = Model.SetearDatosBitacora2(List);
                    Session["bitacora"] = bitacora != null ? bitacora : new List<BitacoraSeguimiento>();

                    if (idcontactcenter != null && idcontactcenter != 0)
                    {
                        Modelo = Model.GetContactCenterCensoIdContact((int)idcontactcenter);
                    }

                    if (Modelo.id_auditor == null)
                    {
                        if (idConcurrencia != null && idConcurrencia != 0)
                        {
                            Modelo = Model.GetContactCenterCensoIdConcurrencia((int)idConcurrencia);
                        }
                    }

                    if (Modelo.id_auditor == null)
                    {
                        if (idCenso != null && idCenso != 0)
                        {
                            Modelo = Model.GetContactCenterCensoIdCenso((int)idCenso);
                        }
                    }

                    return View(Modelo);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return View(Modelo);
        }

        [HttpPost]
        public ActionResult IngresoCaso(int? id_contactcenter, int? id_Censo, int? id_concurrencia, string documentosolicitante, string nombresolicitante,
          string telefonosolicitante, string correosolicitante, int? clasificacioncontacto, string documentopaciente, string nombrepaciente,
          string tipobeneficiario, string regionalpaciente, string telefonopaciente, string otrotelefonopaciente, string correopaciente,
          string otrocorreopaciente, string coddiagnostico, string diagnostico,
          string coddiagnostico2, string diagnostico2, int? regional, int? idRegional, int? unis, int? ciudad, int? ips_paciente,
          int? auditor, string nombreOtroAuditor, string agente, int? servicio, string telefonocontactsolucionador,
          string correocontactosolucionador, int? clasificacioncontactocaso, int? estadosolicitud, string infocaso, int? tipificacion,
          string otroservicio, string otrotiposolicitud, HttpPostedFileBase imagen, string otrotelefonosolicitante, string otroContactoNombre,
          string otroContactoParentezco, string otroContactoTelefono, int? ips, int? tipoIngreso, string Ips_otro)
        {
            try
            {
                Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

                ViewBag.clasificacion = Model.GetListClasificacionContacto();
                ViewBag.tipoServicio = Model.GetListTipoServicio();
                ViewBag.tiposolicitud = Model.GetListTipoSolicitud();
                ViewBag.estadosolicitud = Model.GetListEstadoSolicitud();
                ViewBag.mediosnotificacion = Model.GetListMediosNotificacion();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.auditor = BusClass.GetSisUsuarioactivo().Where(l => l.id_rol == 7).ToList();
                ViewBag.eps = BusClass.GetRefEps();
                ViewBag.tipificacion = Model.GetListTipificacion();
                @ViewBag.agente = SesionVar.NombreUsuario;
                ViewBag.tiposolicitudBitacora = Model.GetListTipoSolicitudBitacora();
                ViewBag.ips = BusClass.GetRefIps();

                ViewBag.idCenso = id_Censo;
                ViewBag.idcontactcenter = id_contactcenter;
                ViewBag.idConcurrencia = id_concurrencia;

                if (id_contactcenter == 0)
                {
                    ViewData["rta"] = 1;
                }
                else
                {
                    ViewData["rta"] = 2;
                }

                contact_center obj = new contact_center();
                obj.id_censo = id_Censo;
                obj.id_concurrencia = id_concurrencia;
                obj.num_documento_solicitante = documentosolicitante;
                obj.nom_solicitante = nombresolicitante;
                obj.telefono_solicitante = telefonosolicitante;
                obj.otro_telefono_solicitante = otrotelefonosolicitante;
                obj.correo_electronico_solicitante = correosolicitante;

                obj.clasificacion_contacto_solicitante = clasificacioncontacto;
                obj.num_documento_paciente = documentopaciente;
                obj.nom_paciente = nombrepaciente;
                obj.tipo_beneficiario_paciente = tipobeneficiario;
                obj.regional_paciente = regionalpaciente;
                obj.telefono_paciente = telefonopaciente;
                obj.otro_telefono_paciente = otrotelefonopaciente;
                obj.correo_electronico_paciente = correopaciente;
                obj.otro_correo_paciente = otrocorreopaciente;
                obj.diagnostico = coddiagnostico;
                obj.diagnostico_txt = diagnostico;
                obj.diagnostico2 = coddiagnostico2;
                obj.diagnostico2_txt = diagnostico2;

                obj.regional = regional;
                obj.ciudad = ciudad;
                obj.ips_paciente = ips_paciente;
                obj.id_auditor = auditor;
                obj.otro_auditor = nombreOtroAuditor;
                obj.agente_solucionador = agente;
                obj.servicio = servicio;
                obj.otro_servicio = otroservicio;
                obj.estado_solicitud = 1;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.Ips = ips;
                obj.Ips_otro = Ips_otro;
                obj.tipo_insercion = 1;

                obj.otroContacto_nombre_paciente = otroContactoNombre;
                obj.otroContacto_parentezco_paciente = otroContactoParentezco;
                obj.otroContacto_telefono = otroContactoTelefono;

                List<BitacoraSeguimiento> bitacora = (List<BitacoraSeguimiento>)Session["bitacora"];
                List<contact_center_dtll> List = Model.SetearDatosBitacora(bitacora, id_concurrencia, id_Censo, obj);

                /*Si es un nuevo caso, se inserta y retorna el id*/

                if ((id_contactcenter != 0 && id_contactcenter != null) || (id_concurrencia != 0 && id_concurrencia != null) || (id_Censo != 0 && id_Censo != null))
                {
                    obj.id_contact_center = (int)id_contactcenter;
                    id_contactcenter = Model.ActualizarContactObligatorios(obj);

                    if (id_contactcenter == 0)
                    {
                        id_contactcenter = Model.InsertarIngresoContactCenter(obj, ref MsgRes);
                    }
                }
                else
                {
                    id_contactcenter = Model.InsertarIngresoContactCenter(obj, ref MsgRes);
                }

                if (tipoIngreso != 1)
                {
                    obj.id_contact_center = (int)id_contactcenter;
                    id_contactcenter = Model.ActualizarContactObligatorios(obj);
                }
                /*Si la imagen principal es diferente de nula, significa que el caso sera cerrado asi que la imagen se guarda, se actualiza la ruta en el caso
                 y actualiza el estado del caso*/
                if (imagen != null)
                {
                    string path = Model.ObtenerPath(Path.GetExtension(imagen.FileName));
                    string tipo = imagen.ContentType;

                    imagen.SaveAs(path);
                    Model.ActualizarImagenCaso(path, tipo, (int)id_contactcenter);
                }

                /*Insercion del detalle de la bitacora*/
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto || id_contactcenter != 0)
                {
                    if (id_concurrencia != null && id_concurrencia != 0)
                    {
                        var actualizaContactCenter = BusClass.ActualizarEnContactCenterConcurrencia(id_concurrencia, ref MsgRes);
                    }

                    if (id_Censo != null && id_Censo != 0)
                    {
                        var ActualizarCenso = BusClass.ActualizarEnContactCenterCenso(id_Censo, ref MsgRes);
                    }

                    Model.InsertarBitacoraCallCenter(List, (int)id_contactcenter, SesionVar.UserName);
                }

                if ((int)id_contactcenter == 0)
                {
                    return View(new management_contact_centerResult());
                }
                else
                {
                    return View(Model.GetContactCenterCensoIdContact(id_contactcenter.Value));
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return View(new management_contact_centerResult());
            }
        }

        public ActionResult IngresoCasoOtro(int? idcontactcenter)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            try
            {

                ViewBag.clasificacion = Model.GetListClasificacionContacto();
                ViewBag.tipoServicio = Model.GetListTipoServicio();
                ViewBag.tiposolicitud = Model.GetListTipoSolicitud();
                ViewBag.tiposolicitudBitacora = Model.GetListTipoSolicitudBitacora();
                ViewBag.estadosolicitud = Model.GetListEstadoSolicitud();
                ViewBag.mediosnotificacion = Model.GetListMediosNotificacion();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.auditor = BusClass.GetSisUsuarioactivo().Where(l => l.id_rol == 7).ToList();
                ViewBag.eps = BusClass.GetRefEps();
                ViewBag.tipificacion = Model.GetListTipificacion();
                ViewBag.agente = SesionVar.NombreUsuario;

                ViewData["rta"] = 0;

                if (idcontactcenter == null)
                {
                    Session["bitacoraOtro"] = new List<BitacoraSeguimientoOtro>();
                    return View(new management_contact_centerResult());
                }
                else
                {
                    List<contact_center_dtll> List = Model.GetListBitacoraByIngreso(idcontactcenter.Value, 0, 0);
                    List<BitacoraSeguimientoOtro> bitacora = Model.SetearDatosBitacora2Otro(List);
                    Session["bitacoraOtro"] = bitacora;
                    return View(Model.GetContactCenterCensoIdContact(idcontactcenter.Value));
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return View(new contact_center());
            }
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult IngresoCasoOtro(int? id_contactcenter, int? id_Censo, string documentosolicitante, string nombresolicitante, string telefonosolicitante,
          string correosolicitante, int? ips, int? clasificacioncontacto, string documentopaciente, string nombrepaciente, string tipobeneficiario, string regionalpaciente,
          string telefonopaciente, string otrotelefonopaciente, string correopaciente, string otrocorreopaciente, string coddiagnostico, string diagnostico,
          string coddiagnostico2, string diagnostico2, int? regional, int? idRegional, int? unis, int? ciudad, int? ips_paciente, int? auditor, string nombreOtroAuditor,
          string agente, int? servicio, string telefonocontactsolucionador, string correocontactosolucionador, int? clasificacioncontactocaso,
          int? estadosolicitud, string infocaso, int? tipificacion, string otroservicio, string otrotiposolicitud, HttpPostedFileBase imagen, string otrotelefonosolicitante,
          string otroContactoNombre, string otroContactoParentezco, string otroContactoTelefono)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            try
            {
                List<BitacoraSeguimientoOtro> bitacora = (List<BitacoraSeguimientoOtro>)Session["bitacoraOtro"];
                List<contact_center_dtll> List = Model.SetearDatosBitacoraOtro(bitacora);

                if (id_contactcenter == 0)
                {
                    ViewData["rta"] = 1;
                }
                else
                {
                    ViewData["rta"] = 2;
                }

                contact_center obj = new contact_center();
                obj.id_censo = id_Censo;
                obj.num_documento_solicitante = documentosolicitante;
                obj.nom_solicitante = nombresolicitante;
                obj.telefono_solicitante = telefonosolicitante;
                obj.otro_telefono_solicitante = otrotelefonosolicitante;
                obj.correo_electronico_solicitante = correosolicitante;

                obj.clasificacion_contacto_solicitante = clasificacioncontacto;
                obj.num_documento_paciente = documentopaciente;
                obj.nom_paciente = nombrepaciente;
                obj.tipo_beneficiario_paciente = tipobeneficiario;
                obj.regional_paciente = regionalpaciente;
                obj.telefono_paciente = telefonopaciente;
                obj.otro_telefono_paciente = otrotelefonopaciente;
                obj.correo_electronico_paciente = correopaciente;
                obj.otro_correo_paciente = otrocorreopaciente;
                obj.diagnostico = coddiagnostico;
                obj.diagnostico_txt = diagnostico;
                obj.diagnostico2 = coddiagnostico2;
                obj.diagnostico2_txt = diagnostico2;
                obj.regional = regional;
                obj.unis = unis;
                obj.ciudad = ciudad;
                obj.ips_paciente = ips_paciente;
                obj.id_auditor = auditor;
                obj.otro_auditor = nombreOtroAuditor;
                obj.agente_solucionador = agente;
                obj.servicio = servicio;
                obj.otro_servicio = otroservicio;
                obj.estado_solicitud = 1;
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;
                obj.tipo_insercion = 2;

                obj.otroContacto_nombre_paciente = otroContactoNombre;
                obj.otroContacto_parentezco_paciente = otroContactoParentezco;
                obj.otroContacto_telefono = otroContactoTelefono;
                obj.Ips = ips;

                /*Si es un nuevo caso, se inserta y retorna el id*/
                if (id_contactcenter == 0 || id_contactcenter == null)
                {
                    id_contactcenter = Model.InsertarIngresoContactCenter(obj, ref MsgRes);
                }
                else
                {
                    obj.id_contact_center = (int)id_contactcenter;
                    id_contactcenter = Model.ActualizarContactObligatorios(obj);
                }
                /*Si la imagen principal es diferente de nula, significa que el caso sera cerrado asi que la imagen se guarda, se actualiza la ruta en el caso
                 y actualiza el estado del caso*/
                if (imagen != null)
                {
                    string path = Model.ObtenerPath(Path.GetExtension(imagen.FileName));
                    string tipo = imagen.ContentType;

                    imagen.SaveAs(path);
                    Model.ActualizarImagenCaso(path, tipo, (int)id_contactcenter);
                }

                /*Insercion del detalle de la bitacora*/
                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto || id_contactcenter != 0)
                {
                    Model.InsertarBitacoraCallCenter(List, (int)id_contactcenter, SesionVar.UserName);
                }

                ViewBag.clasificacion = Model.GetListClasificacionContacto();
                ViewBag.tipoServicio = Model.GetListTipoServicio();
                ViewBag.tiposolicitud = Model.GetListTipoSolicitud();
                ViewBag.estadosolicitud = Model.GetListEstadoSolicitud();
                ViewBag.mediosnotificacion = Model.GetListMediosNotificacion();
                ViewBag.regionales = BusClass.GetRefRegion();
                ViewBag.auditor = BusClass.GetSisUsuarioactivo().Where(l => l.id_rol == 7).ToList();
                ViewBag.eps = BusClass.GetRefEps();
                ViewBag.tipificacion = Model.GetListTipificacion();
                @ViewBag.agente = SesionVar.NombreUsuario;
                ViewBag.tiposolicitudBitacora = Model.GetListTipoSolicitudBitacora();

                if ((int)id_contactcenter == 0)
                {
                    return View(new management_contact_centerResult());
                }
                else
                {
                    return View(Model.GetContactCenterCensoIdContact(id_contactcenter.Value));
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return View(new management_contact_centerResult());
            }
        }

        public string ObtenerUnis(int idregional)
        {
            string result = "<option value=''>- Seleccionar -</option>";
            List<Ref_odont_unis> Unis = BusClass.Odont_unis().Where(l => l.id_regional == idregional).ToList();
            foreach (var item in Unis)
            {
                result += "<option value='" + item.id_ref_unis + "'>" + item.descripcion + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(int idunis)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.GetCiudades().Where(l => l.id_ref_odont_unis == idunis).ToList();
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        public string ObtenerCiudadesInicial(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ciudades> Ciudades = BusClass.TotalCiudadesXRegional(regional);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.id_ref_ciudades + "'>" + item.nombre + "</option>";
            }

            return result;
        }

        public int OntenerRegionalesInicial(int? ciudad)
        {
            management_regional_ciudadResult regionales = new management_regional_ciudadResult();
            regionales = BusClass.Ref_regional_ciudad_detallado().Where(x => x.id_ref_ciudades == ciudad).FirstOrDefault();

            var idRegional = 0;
            if (regionales != null)
            {
                idRegional = regionales.id_ref_regional;
            }

            return idRegional;
        }

        public string ObtenerIPS(int Ciudad)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<Ref_ips> IPS = BusClass.GetRefIps().Where(l => l.id_ref_ciudades == Ciudad).ToList();
            foreach (var item in IPS)
            {
                result += "<option value='" + item.id_ref_ips + "'>" + item.Nit + " - " + item.Nombre.ToUpper() + "</option>";
            }

            return result;
        }

        public string ObtenerAuditores(int? regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_usuarios_regionalIdResult> Auditor = BusClass.listadoRegionalesUsuarioReg(regional);
            foreach (var item in Auditor)
            {
                result += "<option value='" + item.id_auditor + "'>" +
                          (item.nombre != null ? item.nombre.ToUpper() : "") +
                          "</option>";
            }

            result += "<option value='" + 0 + "'>OTRO CONTACTO</option>";

            return result;
        }

        public JsonResult AgregarSeguimiento(string Observaciones, int? estado, int? medio_notificacion, string correo, string telefono,
         int? servicio, string nombrecontactsolucionadorIPS, string telefonocontactsolucionador, string correocontactosolucionador, string otroservicio, string casoEfectivo, int? tipoSolicitud)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            List<BitacoraSeguimiento> seguimientos = (List<BitacoraSeguimiento>)Session["bitacora"];
            List<BitacoraSeguimiento> newlist = new List<BitacoraSeguimiento>();
            BitacoraSeguimiento obj = null;
            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                foreach (var item in seguimientos)
                {
                    obj = new BitacoraSeguimiento();
                    i++;
                    obj.id = i;
                    obj.fecha = item.fecha;
                    obj.observaciones = item.observaciones;
                    obj.usuario = item.usuario;
                    obj.estado = item.estado;
                    obj.nomestado = item.nomestado;
                    obj.medionotificacion = item.medionotificacion;
                    obj.mailnotificacion = item.mailnotificacion;
                    obj.telefononotificacion = item.telefononotificacion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.ruta = item.ruta;
                    obj.tipoArchivo = item.tipoArchivo;

                    obj.nombrecontactsolucionadorIPS = item.nombrecontactsolucionadorIPS;
                    obj.telefonocontactsolucionador = item.telefonocontactsolucionador;
                    obj.correocontactosolucionador = item.correocontactosolucionador;
                    obj.tipoSolicitud = item.tipoSolicitud;

                    if (obj.tipoSolicitud != 0)
                    {
                        obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = item.servicio;

                    if (obj.servicio != 0)
                    {
                        obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }
                    obj.otroservicio = item.otroservicio;
                    obj.idConcurrencia = item.idConcurrencia;

                    ultestado = item.estado;
                    newlist.Add(obj);
                }

                obj = new BitacoraSeguimiento();
                obj.id = seguimientos.Count + 1;
                obj.fecha = DateTime.Now;
                obj.observaciones = Request.Form["Observaciones"];
                obj.estado = Int32.Parse(Request.Form["estado"]);
                obj.usuario = SesionVar.UserName;
                obj.nomestado = BusClass.GetListEstadoSolicitud().Where(l => l.id_ref_contact_estado_solicitud == estado).FirstOrDefault().descripcion;
                obj.medionotificacion = Int32.Parse(Request.Form["medio_notificacion"]);
                obj.mailnotificacion = Request.Form["correo"];
                obj.telefononotificacion = Request.Form["telefono"];

                string idConcurrencia = Request.Form["id_concurrencia"];

                obj.idConcurrencia = !string.IsNullOrEmpty(idConcurrencia) ? Convert.ToInt32(idConcurrencia) : 0;

                obj.nombrecontactsolucionadorIPS = nombrecontactsolucionadorIPS;
                obj.telefonocontactsolucionador = telefonocontactsolucionador;
                obj.correocontactosolucionador = correocontactosolucionador;
                obj.servicio = (int)servicio;
                obj.otroservicio = otroservicio;
                obj.medionotificacion = (int)medio_notificacion;
                obj.tipoSolicitud = (int)tipoSolicitud;
                obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;

                obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == servicio).FirstOrDefault().descripcion;
                obj.casoEfectivo = Request.Form["casoEfectivo"];

                //HttpPostedFileBase archivo = Request.Files[0];

                //if (archivo != null)
                //{
                //    obj.tipoArchivo = archivo.ContentType;
                //    obj.ruta = Model.ObtenerPath(Path.GetExtension(archivo.FileName));
                //    Request.Files[0].SaveAs(obj.ruta);
                //}

                ultestado = (int)estado;
                newlist.Add(obj);

                Session["bitacora"] = newlist;
                var j = 0;

                foreach (var item in newlist)
                {
                    j++;
                    result += "<tr>";
                    result += "<td>" + item.id + "</td>";
                    result += "<td>" + item.fecha.ToString("dd/MM/yyyy") + "</td>";

                    result += "<td>" +
                    "<span class='observaciones_" + j + "'>" + item.observaciones.Substring(0, Math.Min(100, @item.observaciones.Length)) + "</span>" +
                    "<span class='observaciones_completas_" + j + "' style='display: none;'>" + item.observaciones + "</span>";

                    if (@item.observaciones.Length > 100)
                    {
                        result += "<label class='text-secondary_asalud botonMostrar_" + j + "' onclick='MostrarTextoCompleto(" + j + ")'>Mostrar</label>" +
                            "<label class='text-secondary_asalud botonOcultar_" + j + "' style='display: none;' onclick='OcultarTextoCompleto(" + j + ")'>Ocultar</label>";
                    }

                    result += "</td>";

                    //result += "<td>" + item.observaciones + "</td>";
                    result += "<td>" + item.nomestado + "</td>";
                    result += "<td>" + item.tipoSolicitudDescripcion + "</td>";
                    result += "<td>" + item.servicioDescripcion + "</td>";
                    result += "<td>" + item.casoEfectivo + "</td>";

                    if (item.medionotificacion == 1)
                    {
                        result += "<td>" + "Correo: " + item.mailnotificacion + "</td>";
                    }
                    else if (item.medionotificacion == 2)
                    {
                        result += "<td>" + "Teléfono: " + item.telefononotificacion + "</td>";
                    }
                    else
                    {
                        result += "<td>";
                        result += "<ul>";
                        result += "<li> Correo: " + item.mailnotificacion + "</li>";
                        result += "<li> Teléfono: " + item.telefononotificacion + "</li>";
                        result += "</ul>";
                        result += "</td>";
                    }
                    result += "<td class='text-center'>" +
                        "<a class='btn btn-xs button_Asalud_Rechazar' href='javascript:EliminarBitacora(" + item.id + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                        //"<a class='btn btn-xs button_Asalud_descargas' href='javascript:VisualizarImagenBitacora(" + item.id + ")'><i class='glyphicon glyphicon-eye-open'></i>&nbsp; Ver Imagen</a>" +
                        "</td>";
                    result += "</tr>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                tabla = result,
                estado = ultestado
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoverSeguimiento(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                List<BitacoraSeguimiento> seguimientos = (List<BitacoraSeguimiento>)Session["bitacora"];
                List<BitacoraSeguimiento> newlist = new List<BitacoraSeguimiento>();

                BitacoraSeguimiento obj = seguimientos.Where(l => l.id == id).FirstOrDefault();
                seguimientos.Remove(obj);

                foreach (var item in seguimientos)
                {
                    obj = new BitacoraSeguimiento();
                    i++;
                    obj.id = i;
                    obj.fecha = item.fecha;
                    obj.observaciones = item.observaciones;
                    obj.usuario = item.usuario;
                    obj.estado = item.estado;
                    obj.nomestado = item.nomestado;
                    obj.medionotificacion = item.medionotificacion;
                    obj.mailnotificacion = item.mailnotificacion;
                    obj.telefononotificacion = item.telefononotificacion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.ruta = item.ruta;
                    obj.tipoArchivo = item.tipoArchivo;

                    obj.nombrecontactsolucionadorIPS = item.nombrecontactsolucionadorIPS;
                    obj.telefonocontactsolucionador = item.telefonocontactsolucionador;
                    obj.correocontactosolucionador = item.correocontactosolucionador;
                    obj.tipoSolicitud = item.tipoSolicitud;

                    if (obj.tipoSolicitud != 0)
                    {
                        obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = item.servicio;

                    if (obj.servicio != 0)
                    {
                        obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }
                    obj.otroservicio = item.otroservicio;

                    ultestado = item.estado;
                    newlist.Add(obj);
                }

                Session["bitacora"] = newlist;

                if (newlist.Count > 0)
                {
                    foreach (var item in newlist)
                    {
                        result += "<tr>";
                        result += "<td>" + item.id + "</td>";
                        result += "<td>" + item.fecha.ToString("dd/MM/yyyy") + "</td>";
                        result += "<td>" + item.observaciones + "</td>";
                        result += "<td>" + item.nomestado + "</td>";
                        result += "<td>" + item.tipoSolicitudDescripcion + "</td>";
                        result += "<td>" + item.servicioDescripcion + "</td>";
                        result += "<td>" + item.casoEfectivo + "</td>";

                        if (item.medionotificacion == 1)
                        {
                            result += "<td>" + "Correo: " + item.mailnotificacion + "</td>";
                        }
                        else if (item.medionotificacion == 2)
                        {
                            result += "<td>" + "Teléfono: " + item.telefononotificacion + "</td>";
                        }
                        else
                        {
                            result += "<td>";
                            result += "<ul>";
                            result += "<li> Correo: " + item.mailnotificacion + "</li>";
                            result += "<li> Teléfono: " + item.telefononotificacion + "</li>";
                            result += "</ul>";
                            result += "</td>";
                        }

                        result += "<td class='text-center'>" +
                            "<a class='btn btn-xs button_Asalud_Rechazar' href='javascript:EliminarBitacora(" + item.id + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                            "<a class='btn btn-xs button_Asalud_descargas' href='javascript:VisualizarImagenBitacora(" + item.id + ")'><i class='glyphicon glyphicon-eye-open'></i>&nbsp; Ver Imagen</a>" +
                            "</td>";
                        result += "</tr>";
                    }
                }
                else
                {
                    foreach (var item in newlist)
                    {
                        result += "<tr>";
                        result += "<td colspan='6' class='text-center'>No se han insertado datos</td>";
                        result += "</tr>";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                tabla = result,
                estado = ultestado
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarSeguimientoOtro(string Observaciones, int? estado, int? medio_notificacion, string correo, string telefono,
         int? servicio, string nombrecontactsolucionadorIPS, string telefonocontactsolucionador, string correocontactosolucionador, string otroservicio, string casoEfectivo, int? tipoSolicitud)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            List<BitacoraSeguimientoOtro> seguimientos = (List<BitacoraSeguimientoOtro>)Session["bitacoraOtro"];
            List<BitacoraSeguimientoOtro> newlist = new List<BitacoraSeguimientoOtro>();
            BitacoraSeguimientoOtro obj = null;
            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                foreach (var item in seguimientos)
                {
                    obj = new BitacoraSeguimientoOtro();
                    i++;
                    obj.id = i;
                    obj.fecha = item.fecha;
                    obj.observaciones = item.observaciones;
                    obj.usuario = item.usuario;
                    obj.estado = item.estado;
                    obj.nomestado = item.nomestado;
                    obj.medionotificacion = item.medionotificacion;
                    obj.mailnotificacion = item.mailnotificacion;
                    obj.telefononotificacion = item.telefononotificacion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.ruta = item.ruta;
                    obj.tipoArchivo = item.tipoArchivo;
                    obj.nombrecontactsolucionadorIPS = item.nombrecontactsolucionadorIPS;
                    obj.telefonocontactsolucionador = item.telefonocontactsolucionador;
                    obj.correocontactosolucionador = item.correocontactosolucionador;
                    obj.tipoSolicitud = item.tipoSolicitud;

                    if (obj.tipoSolicitud != 0)
                    {
                        obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = item.servicio;

                    if (obj.servicio != 0)
                    {
                        obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }
                    obj.otroservicio = item.otroservicio;

                    ultestado = item.estado;
                    newlist.Add(obj);
                }

                obj = new BitacoraSeguimientoOtro();
                obj.id = seguimientos.Count + 1;
                obj.fecha = DateTime.Now;
                obj.observaciones = Request.Form["Observaciones"];
                obj.estado = Int32.Parse(Request.Form["estado"]);
                obj.usuario = SesionVar.UserName;
                obj.nomestado = BusClass.GetListEstadoSolicitud().Where(l => l.id_ref_contact_estado_solicitud == estado).FirstOrDefault().descripcion;
                obj.medionotificacion = Int32.Parse(Request.Form["medio_notificacion"]);
                obj.mailnotificacion = Request.Form["correo"];
                obj.telefononotificacion = Request.Form["telefono"];

                obj.nombrecontactsolucionadorIPS = nombrecontactsolucionadorIPS;
                obj.telefonocontactsolucionador = telefonocontactsolucionador;
                obj.correocontactosolucionador = correocontactosolucionador;
                obj.servicio = (int)servicio;
                obj.otroservicio = otroservicio;

                obj.tipoSolicitud = (int)tipoSolicitud;
                obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;

                obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == servicio).FirstOrDefault().descripcion;
                obj.casoEfectivo = Request.Form["casoEfectivo"];

                HttpPostedFileBase archivo = Request.Files[0];

                if (archivo != null)
                {
                    obj.tipoArchivo = archivo.ContentType;
                    obj.ruta = Model.ObtenerPath(Path.GetExtension(archivo.FileName));
                    Request.Files[0].SaveAs(obj.ruta);
                }

                ultestado = (int)estado;
                newlist.Add(obj);

                Session["bitacoraOtro"] = newlist;

                foreach (var item in newlist)
                {
                    result += "<tr>";
                    result += "<td>" + item.id + "</td>";
                    result += "<td>" + item.fecha.ToString("dd/MM/yyyy") + "</td>";
                    result += "<td>" + item.observaciones + "</td>";
                    result += "<td>" + item.nomestado + "</td>";
                    result += "<td>" + item.tipoSolicitudDescripcion + "</td>";
                    result += "<td>" + item.servicioDescripcion + "</td>";
                    result += "<td>" + item.casoEfectivo + "</td>";

                    if (item.medionotificacion == 1)
                    {
                        result += "<td>" + item.mailnotificacion + "</td>";
                    }
                    else if (item.medionotificacion == 2)
                    {
                        result += "<td>" + item.telefononotificacion + "</td>";
                    }
                    else
                    {
                        result += "<td>";
                        result += "<ul>";
                        result += "<li>" + item.mailnotificacion + "</li>";
                        result += "<li>" + item.telefononotificacion + "</li>";
                        result += "</ul>";
                        result += "</td>";
                    }
                    result += "<td class='text-center'>" +
                        "<a class='btn btn-xs button_Asalud_Rechazar' href='javascript:EliminarBitacora(" + item.id + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                        "<a class='btn btn-xs button_Asalud_descargas' href='javascript:VisualizarImagenBitacora(" + item.id + ")'><i class='glyphicon glyphicon-eye-open'></i>&nbsp; Ver Imagen</a>" +
                        "</td>";
                    result += "</tr>";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                tabla = result,
                estado = ultestado
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoverSeguimientoOtro(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            int i = 0;
            int ultestado = 0;
            string result = "";

            try
            {
                List<BitacoraSeguimientoOtro> seguimientos = (List<BitacoraSeguimientoOtro>)Session["bitacoraOtro"];
                List<BitacoraSeguimientoOtro> newlist = new List<BitacoraSeguimientoOtro>();

                BitacoraSeguimientoOtro obj = seguimientos.Where(l => l.id == id).FirstOrDefault();
                seguimientos.Remove(obj);


                foreach (var item in seguimientos)
                {
                    obj = new BitacoraSeguimientoOtro();
                    i++;
                    obj.id = i;
                    obj.fecha = item.fecha;
                    obj.observaciones = item.observaciones;
                    obj.usuario = item.usuario;
                    obj.estado = item.estado;
                    obj.nomestado = item.nomestado;
                    obj.medionotificacion = item.medionotificacion;
                    obj.mailnotificacion = item.mailnotificacion;
                    obj.telefononotificacion = item.telefononotificacion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.ruta = item.ruta;
                    obj.tipoArchivo = item.tipoArchivo;

                    obj.nombrecontactsolucionadorIPS = item.nombrecontactsolucionadorIPS;
                    obj.telefonocontactsolucionador = item.telefonocontactsolucionador;
                    obj.correocontactosolucionador = item.correocontactosolucionador;
                    obj.tipoSolicitud = item.tipoSolicitud;

                    if (obj.tipoSolicitud != 0)
                    {
                        obj.tipoSolicitudDescripcion = Model.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == obj.tipoSolicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = item.servicio;

                    if (obj.servicio != 0)
                    {
                        obj.servicioDescripcion = Model.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }
                    obj.otroservicio = item.otroservicio;

                    ultestado = item.estado;
                    newlist.Add(obj);
                }

                Session["bitacoraOtro"] = newlist;

                if (newlist.Count > 0)
                {
                    foreach (var item in newlist)
                    {
                        result += "<tr>";
                        result += "<td>" + item.id + "</td>";
                        result += "<td>" + item.fecha.ToString("dd/MM/yyyy") + "</td>";
                        result += "<td>" + item.observaciones + "</td>";
                        result += "<td>" + item.nomestado + "</td>";
                        result += "<td>" + item.tipoSolicitudDescripcion + "</td>";
                        result += "<td>" + item.servicioDescripcion + "</td>";
                        result += "<td>" + item.casoEfectivo + "</td>";

                        if (item.medionotificacion == 1)
                        {
                            result += "<td>" + item.mailnotificacion + "</td>";
                        }
                        else if (item.medionotificacion == 2)
                        {
                            result += "<td>" + item.telefononotificacion + "</td>";
                        }
                        else
                        {
                            result += "<td>";
                            result += "<ul>";
                            result += "<li>" + item.mailnotificacion + "</li>";
                            result += "<li>" + item.telefononotificacion + "</li>";
                            result += "</ul>";
                            result += "</td>";
                        }
                        result += "<td class='text-center'>" +
                            "<a class='btn btn-xs button_Asalud_Rechazar' href='javascript:EliminarBitacora(" + item.id + ")'><i class='glyphicon glyphicon-trash'></i>&nbsp; Eliminar</a>" +
                            "<a class='btn btn-xs button_Asalud_descargas' href='javascript:VisualizarImagenBitacora(" + item.id + ")'><i class='glyphicon glyphicon-eye-open'></i>&nbsp; Ver Imagen</a>" +
                            "</td>";
                        result += "</tr>";
                    }
                }
                else
                {
                    foreach (var item in newlist)
                    {
                        result += "<tr>";
                        result += "<td colspan='6' class='text-center'>No se han insertado datos</td>";
                        result += "</tr>";
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            var data = new
            {
                tabla = result,
                estado = ultestado
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarSeguimientoPrincipal(int? idContact, int? id_censo, int? id_concurrencia, string Observaciones, int? estado, int? medio_notificacion, string correo, string telefono,
         int? servicio, string nombrecontactsolucionadorIPS, string telefonocontactsolucionador, string correocontactosolucionador, string otroservicio,
         string casoEfectivo, int? tipoSolicitud)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            string mensaje = "";
            var rta = 0;

            contact_center_dtll obj = new contact_center_dtll();

            try
            {
                obj.id_contact_center = idContact;
                obj.id_censo = id_censo;
                obj.id_concurrencia = id_concurrencia;
                obj.fecha_ingreso = DateTime.Now;
                obj.fecha_digita = DateTime.Now;
                obj.Observaciones = Observaciones;
                obj.id_medio_notificacion = medio_notificacion;
                obj.correo_notificacion = correo;
                obj.telefono_notificacion = telefono;
                obj.estado_solicitud = estado;
                obj.casoEfectivo = casoEfectivo;
                obj.nom_contacto_solucionador_ips = nombrecontactsolucionadorIPS;
                obj.telefono_contacto_solucionador_ips = telefonocontactsolucionador;
                obj.correo_contacto_solucionador_ips = correocontactosolucionador;
                obj.id_servicio = (int)servicio;
                obj.otro_servicio = otroservicio;
                obj.usuario_digita = SesionVar.UserName;
                obj.id_tipo_solicitud = tipoSolicitud;

                //obj.otroservicio = otroservicio;

                //HttpPostedFileBase archivo = Request.Files[0];

                //if (archivo != null)
                //{
                //    obj.ruta_imagen = Model.ObtenerPath(Path.GetExtension(archivo.FileName));
                //    obj.tipo_archivo = archivo.ContentType;
                //    Request.Files[0].SaveAs(obj.ruta_imagen);
                //}

                var respuesta = BusClass.InsertarBitacoraContactCenter(obj);
                if (respuesta != 0)
                {
                    if (estado == 2 && idContact != null && idContact != 0)
                    {
                        var actualiza = BusClass.ActualizarContactCenterPrincipal(idContact);
                    }

                    if (estado == 2)
                    {
                        if ((id_censo != null && id_censo != 0))
                        {
                            var ActualizarCenso = BusClass.ActualizarEnContactCenterCenso(id_censo, ref MsgRes);
                        }

                        if (id_concurrencia != null && id_concurrencia != 0)
                        {
                            var actualizaContactCenter = BusClass.ActualizarEnContactCenterConcurrencia(id_concurrencia, ref MsgRes);
                        }
                    }

                    mensaje = "BITACORA INGRESADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN EL INGRESO DE LA BITACORA";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN EL INGRESO DE LA BITACORA: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        /// <summary>
        /// Buscar beneficiarios autocomplete
        /// </summary>
        /// <returns></returns>
        public JsonResult Buscar_Beneficiario()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                List<VW_base_beneficiarios> beneficiarios = new List<VW_base_beneficiarios>();

                string term = Request.Params["term"];
                if (term.Length >= 3)
                {
                    beneficiarios = BusClass.GetBeneficiarios(term, ref MsgRes);
                    var lista = (from reg in beneficiarios
                                 select new
                                 {
                                     id = reg.id_base_beneficiarios,
                                     nit = reg.Numero_identificacion,
                                     nombre = reg.Nombre + " " + reg.Apellidos,
                                     telefono = reg.celular,
                                     email = reg.email,
                                     regional = reg.regional,
                                     tipo_beneficiario = reg.tipo_salud,
                                     label = reg.Numero_identificacion + "-" + reg.Nombre + " " + reg.Apellidos,
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

        public JsonResult Buscar_Solicitante(string term, int tipo)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            if (string.IsNullOrEmpty(term))
                return null;
            try
            {
                if (term.Length >= 3 && (tipo != 4 && tipo != 5))
                {
                    List<ref_contact_solicitante> solicitantes = Model.GetlistSolicitantesbytipo(term, tipo);
                    var lista = (from reg in solicitantes
                                 select new
                                 {
                                     id = reg.id_ref_contact_solicitante,
                                     nombre = reg.nombre_razon_social,
                                     telefono = reg.telefono,
                                     email = reg.correo_electronico,
                                     label = reg.nombre_razon_social,
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

        public JsonResult BuscarDiagnostico()
        {
            if (string.IsNullOrEmpty(Request.Params["term"]))
                return null;
            try
            {
                string term = Request.Params["term"];
                if (term.Length >= 1)
                {
                    List<Ref_cie10> Dx = BusClass.GetCie10Bycodigo(term);
                    var lista = (from reg in Dx
                                 select new
                                 {
                                     id = reg.id_cie10,
                                     label = reg.id_cie10 + "-" + reg.des,
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

        public ActionResult TableroControlContactCenter(DateTime? fechaInicio, DateTime? fechaFin)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            List<management_contact_centerResult> listaInicial = new List<management_contact_centerResult>();
            List<management_contact_centerResult> listaEcopetrol = new List<management_contact_centerResult>();
            List<management_contact_centerResult> listaCenso = new List<management_contact_centerResult>();
            List<management_contact_centerResult> listaConcurrencia = new List<management_contact_centerResult>();

            List<management_contact_centerResult> listaAbiertos = new List<management_contact_centerResult>();
            List<management_contact_centerResult> listaCerrados = new List<management_contact_centerResult>();
            List<management_contact_center_seguimientoResult> listaSeguimiento = new List<management_contact_center_seguimientoResult>();

            try
            {
                if (fechaInicio != null || fechaFin != null)
                {
                    listaInicial = BusClass.ListaTableroContactCenter(fechaInicio, fechaFin);
                    listaSeguimiento = BusClass.ListaTableroContactCenterSeguimiento(fechaInicio, fechaFin);
                }

                listaAbiertos = listaInicial.Where(x => x.estado_solicitud == 1 || x.reingresado == 1).ToList();
                listaCerrados = listaInicial.Where(x => x.estado_solicitud == 2 && (x.tipo_insercion == 1 || x.tipo_insercion == 2)).ToList();

                listaEcopetrol = listaAbiertos.Where(x => x.tipo_insercion == 1 || x.tipo_insercion == 2).ToList();
                listaCenso = listaAbiertos.Where(x => x.tipo_insercion == 3).ToList();
                listaConcurrencia = listaAbiertos.Where(x => x.tipo_insercion == 4).ToList();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            ViewBag.datostipoServicio = Model.GetListTipoServicio();
            ViewBag.clasificacion = Model.GetListClasificacionContacto();
            ViewBag.tiposolicitudBitacora = Model.GetListTipoSolicitudBitacora();
            ViewBag.mediosnotificacion = Model.GetListMediosNotificacion();
            ViewBag.estadosolicitud = Model.GetListEstadoSolicitud();

            ViewBag.total = listaInicial;
            ViewBag.ecopetrol = listaEcopetrol;
            ViewBag.censo = listaCenso;
            ViewBag.concurrencia = listaConcurrencia;
            ViewBag.cerrados = listaCerrados;
            ViewBag.seguimiento = listaSeguimiento;

            ViewBag.conteoTotal = listaInicial.Count();
            ViewBag.conteoEcopetrol = listaEcopetrol.Count();
            ViewBag.conteoCenso = listaCenso.Count();
            ViewBag.conteoConcurrencia = listaConcurrencia.Count();
            ViewBag.conteoCerradas = listaCerrados.Count();
            ViewBag.conteoSeguimiento = listaSeguimiento.Count();

            ViewBag.conteoTodo = listaInicial.Count() + listaSeguimiento.Count();

            Session["ListadoContactCenter"] = listaAbiertos;
            Session["ListadoContactCenterSeguimiento"] = listaSeguimiento;

            return View();
        }

        public string BusquedaCamposObligatorios(int? idContact, int? idCenso, int? idConcurrencia, string tipo)
        {
            var respuesta = "";
            management_contact_center_camposObligatoriosResult item = new management_contact_center_camposObligatoriosResult();
            try
            {
                item = BusClass.ListaCamposObligatoriosCC(idContact, idConcurrencia, idCenso).OrderByDescending(x => x.id_contact_center).FirstOrDefault();
                if (item != null)
                {

                    if (tipo != "OTRO")
                    {
                        // Verificar si alguno de los campos es nulo o vacío
                        if (string.IsNullOrWhiteSpace(item.correo_electronico_solicitante) || string.IsNullOrWhiteSpace(item.nom_solicitante) || item.ciudad == null
                            || item.id_auditor == null || item.Ips == null)
                        {
                            respuesta = "Al menos uno de los campos obligatorios está vacío.";
                            throw new Exception(respuesta);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(item.correo_electronico_solicitante) || string.IsNullOrWhiteSpace(item.nom_solicitante) || item.ciudad == null)
                        {
                            respuesta = "Al menos uno de los campos obligatorios está vacío.";
                            throw new Exception(respuesta);
                        }
                    }
                }
                else
                {
                    respuesta = "No hay datos para este registro";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                respuesta = "Error: " + error;
            }

            return respuesta;

        }

        public ActionResult Vertablerocontrol(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();
            ViewBag.tabla = id;

            List<vw_contact_center> List = new List<vw_contact_center>();
            if (id != 0)
            {
                List = Model.GetListContactCenter(id);
            }
            else
            {
                List = Model.GetListContactCenter(null);
            }
            Session["ListadoContactCenter"] = List;
            return View(List);
        }

        public void ArchivoCargado(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            contact_center obj = new contact_center();
            try
            {
                obj = Model.GetContactCenterById(id);
                if (obj == null)
                {
                    Response.Write("<script language=javascript>alert('No contiene evidencias');</script>");
                }

                if (!string.IsNullOrEmpty(obj.ruta_imagen))
                {
                    byte[] bytes;
                    using (FileStream file = new FileStream(obj.ruta_imagen, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                    }
                    byte[] array = bytes.ToArray();
                    var ruta = obj.ruta_imagen;
                    var extension = "";

                    if (ruta.Contains(".rar") || ruta.Contains(".zip"))
                    {
                        extension = "application/zip";
                    }
                    else
                    {
                        extension = obj.tipo_archivo;
                    }
                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.ContentType = extension;
                        Response.AddHeader("content-length", array.Length.ToString());
                        Response.BinaryWrite(array);
                        Response.Flush();
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No contiene evidencias');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write("<script language=javascript>alert('Error al mostrar evidencias');</script>");
            }
        }

        public void ArchivoBitacora(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            List<BitacoraSeguimiento> seguimientos = (List<BitacoraSeguimiento>)Session["bitacora"];
            BitacoraSeguimiento seg = seguimientos.Where(l => l.id == id).FirstOrDefault();

            try
            {

                if (seg != null)
                {
                    byte[] bytes;

                    using (FileStream file = new FileStream(seg.ruta, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                    }

                    byte[] array = bytes.ToArray();


                    var ruta = seg.ruta;
                    var extension = "";

                    if (ruta.Contains(".rar") || ruta.Contains(".zip"))
                    {
                        extension = "application/zip";
                    }
                    else
                    {
                        extension = seg.tipoArchivo;
                    }
                    if (array != null)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.ContentType = extension;
                        Response.AddHeader("content-length", array.Length.ToString());
                        Response.BinaryWrite(array);
                        Response.Flush();
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('No contiene evidencias');</script>");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Response.Write("<script language=javascript>alert('Error al encontrar evidencias');</script>");

            }
        }

        public void ArchivoBitacoraOtro(int id)
        {
            Models.ContactCenter.ContactCenter Model = new Models.ContactCenter.ContactCenter();

            List<BitacoraSeguimientoOtro> seguimientos = (List<BitacoraSeguimientoOtro>)Session["bitacoraOtro"];
            BitacoraSeguimientoOtro seg = seguimientos.Where(l => l.id == id).FirstOrDefault();

            if (seg != null)
            {
                byte[] bytes;

                using (FileStream file = new FileStream(seg.ruta, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                }

                byte[] array = bytes.ToArray();

                var ruta = seg.ruta;
                var extension = "";

                if (ruta.Contains(".rar") || ruta.Contains(".zip"))
                {
                    extension = "application/zip";
                }
                else
                {
                    extension = seg.tipoArchivo;
                }
                if (array != null)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.ContentType = extension;
                    Response.AddHeader("content-length", array.Length.ToString());
                    Response.BinaryWrite(array);
                    Response.Flush();
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('No contiene evidencias');</script>");
            }
        }

        public void ExcelReporteContactCenter()
        {
            List<management_contact_centerResult> listareporte = new List<management_contact_centerResult>();
            try
            {
                listareporte = (List<management_contact_centerResult>)Session["ListadoContactCenter"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosContact");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AD1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AD1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AD1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AD1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AD1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Tipo solicitante";
                Sheet.Cells["B1"].Value = "Nombre solicitante";
                Sheet.Cells["C1"].Value = "Teléfono solicitante";
                Sheet.Cells["D1"].Value = "Email solicitante";
                Sheet.Cells["E1"].Value = "No documento paciente";
                Sheet.Cells["F1"].Value = "Nombre paciente";
                Sheet.Cells["G1"].Value = "Teléfono paciente";
                Sheet.Cells["H1"].Value = "Email paciente";
                Sheet.Cells["I1"].Value = "Diagnostico";
                Sheet.Cells["J1"].Value = "Diagnostico secundario";
                Sheet.Cells["K1"].Value = "Regional";
                Sheet.Cells["L1"].Value = "Ciudad";
                Sheet.Cells["M1"].Value = "IPS";
                Sheet.Cells["N1"].Value = "Agente solucionador";
                Sheet.Cells["O1"].Value = "Nombre contacto solucionador";
                Sheet.Cells["P1"].Value = "Teléfono contacto solucionador";
                Sheet.Cells["Q1"].Value = "Email contacto solucionador";
                Sheet.Cells["R1"].Value = "Servicio";
                Sheet.Cells["S1"].Value = "Otro servicio";
                Sheet.Cells["T1"].Value = "Otro servicio";
                Sheet.Cells["U1"].Value = "Otro tipo solicitud";
                Sheet.Cells["V1"].Value = "Estado solicitud";
                Sheet.Cells["W1"].Value = "Dias en gestión";
                Sheet.Cells["X1"].Value = "Fecha entrega auditor";
                Sheet.Cells["Y1"].Value = "Usuario entrega auditor";
                Sheet.Cells["Z1"].Value = "Tipo";
                Sheet.Cells["AA1"].Value = "Observación concurrencia";
                Sheet.Cells["AB1"].Value = "Fecha solicitud";
                Sheet.Cells["AC1"].Value = "Fecha reingreso";
                Sheet.Cells["AD1"].Value = "Usuario reingreso";

                int row = 2;
                foreach (management_contact_centerResult item in listareporte)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.clasificacion_contacto_solicitante_descripcion;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.nom_solicitante;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.telefono_solicitante;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.correo_electronico_solicitante;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.num_documento_paciente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.nom_paciente;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.telefono_paciente;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.correo_electronico_paciente;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.diagnostico;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.diagnostico2;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.nombre_regional;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.nom_ciudad;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_ips;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.agente_solucionador;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.nom_contacto_solucionador_ips;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.tel_contacto_solucionador_ips;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.correo_contacto_solucionador_ips;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.servicioDescripcion;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.otro_servicio;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.tipoSolicitudDescripcion;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.otro_tipo_solicitud;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.estado_solicitud_descripcion;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.dias_en_gestion;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.fecha_entrega_auditor;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.usuario_entrega_auditor;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.tipo;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.observacion_contact;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.fecha_digita;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.fecha_reingreso;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.nomreReingreso;

                    Sheet.Cells[string.Format("X{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AB{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("AC{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    Sheet.Cells["A" + row + ":AD1" + row].Style.Font.Name = "Century Gothic";
                    row++;
                }
                Sheet.Cells["A:AD"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_ContactCenter.xlsx");
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

        public void ExcelReporteContactCenterSeguimiento()
        {
            List<management_contact_center_seguimientoResult> listareporte = new List<management_contact_center_seguimientoResult>();
            try
            {
                listareporte = (List<management_contact_center_seguimientoResult>)Session["ListadoContactCenterSeguimiento"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadosContactSeguimiento");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:W1"].Style.Font.Bold = true;
                Sheet.Cells["A1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:W1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:W1"].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A1"].Value = "Nombre contacto bitacora";
                Sheet.Cells["B1"].Value = "Teléfono contacto bitacora";
                Sheet.Cells["C1"].Value = "Correo contacto bitacora";
                Sheet.Cells["D1"].Value = "Servicio bitacora";
                Sheet.Cells["E1"].Value = "Observaciones bitacora";
                Sheet.Cells["F1"].Value = "Tipo solicitud bitacora";
                Sheet.Cells["G1"].Value = "Medio comunicación bitacora";
                Sheet.Cells["H1"].Value = "Caso efectivo bitacora";
                Sheet.Cells["I1"].Value = "Estado bitacora";
                Sheet.Cells["J1"].Value = "Fecha digita bitacora";
                Sheet.Cells["K1"].Value = "Usuario digita bitacora";

                Sheet.Cells["L1"].Value = "Documento solicitante";
                Sheet.Cells["M1"].Value = "Nombre solicitante";
                Sheet.Cells["N1"].Value = "Teléfono solicitante";
                Sheet.Cells["O1"].Value = "otro teléfono solicitante";
                Sheet.Cells["P1"].Value = "Correo solicitante";

                Sheet.Cells["Q1"].Value = "Número documento paciente";
                Sheet.Cells["R1"].Value = "Nombre paciente";
                Sheet.Cells["S1"].Value = "Télefono paciente";
                Sheet.Cells["T1"].Value = "Correo paciente";
                Sheet.Cells["U1"].Value = "Regional";
                Sheet.Cells["V1"].Value = "IPS";
                Sheet.Cells["W1"].Value = "Fecha bitácora";


                int row = 2;
                foreach (management_contact_center_seguimientoResult item in listareporte)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.nombreContactoSolucionadorSegui;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.telefonoContactoSegui;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.correoContacoSegui;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.descripcionServicioSegui;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.observacionesSeguimiento;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.tipoSolicitudDescripcionSegui;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.descripcionMedioComunicacionSegui;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.casoEfectivoSeguimiento;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.descripcionEstadoSolicitudSegui;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.fechaDigitaSeguimiento;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.nombreUsuarioSegui;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.num_documento_solicitante;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.nom_solicitante;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.telefono_solicitante;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.otro_telefono_solicitante;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.correo_electronico_solicitante;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.num_documento_paciente;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.nom_paciente;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.telefono_paciente;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.correo_electronico_paciente;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.nombre_regional;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.nom_ips;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.fechaDigitaSeguimiento;

                    Sheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    Sheet.Cells[string.Format("W{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                    row++;
                }

                Sheet.Cells["A" + row + ":W1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:D"].AutoFitColumns();
                Sheet.Cells["F:W"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=RptTableroControl_ContactCenter_Seguimiento.xlsx");
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