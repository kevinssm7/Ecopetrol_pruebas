using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENUM;
using System.Configuration;
using System.IO;

namespace AsaludEcopetrol.Models.ContactCenter
{
    public class ContactCenter
    {
        #region PROPIEDADES


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

        private BussinesManager.SessionState _SesionVar;
        public BussinesManager.SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new BussinesManager.SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        #endregion

        #region METODOS

        public List<ref_contact_clasificacion_contacto> GetListClasificacionContacto()
        {
            return BusClass.GetListClasificacionContacto();
        }

        public List<ref_contact_tipificacion> GetListTipificacion()
        {
            return BusClass.GetListTipificacion();
        }

        public List<ref_contact_tipo_servicio> GetListTipoServicio()
        {
            return BusClass.GetListTipoServicio();
        }

        public List<ref_contact_tipo_solicitud> GetListTipoSolicitud()
        {
            return BusClass.GetListTipoSolicitud();
        }

        public List<ref_contact_tipoSolicitudBitacora> GetListTipoSolicitudBitacora()
        {
            return BusClass.GetListTipoSolicitudBitacora();
        }

        public List<ref_contact_estado_solicitud> GetListEstadoSolicitud()
        {
            return BusClass.GetListEstadoSolicitud();
        }

        public List<ref_contact_medio_notificacion> GetListMediosNotificacion()
        {
            return BusClass.GetListMediosNotificacion();
        }


        public List<contact_center_dtll> SetearDatosBitacora(List<BitacoraSeguimiento> List, int? id_concurrencia, int? id_censo, contact_center contact)
        {
            List<contact_center_dtll> ListBitacora = new List<contact_center_dtll>();
            try
            {

                foreach (var item in List)
                {
                    contact_center_dtll obj = new contact_center_dtll();

                    obj.id_concurrencia = id_concurrencia;
                    obj.id_censo = id_censo;
                    obj.fecha_ingreso = item.fecha;
                    obj.Observaciones = item.observaciones;
           
                    obj.estado_solicitud = item.estado;
                    obj.id_medio_notificacion = item.medionotificacion;
                    obj.correo_notificacion = item.mailnotificacion;
                    obj.telefono_notificacion = item.telefononotificacion;
                    obj.ruta_imagen = item.ruta;
                    obj.tipo_archivo = item.tipoArchivo;
                    obj.id_tipo_solicitud = item.tipoSolicitud;
                    obj.nom_contacto_solucionador_ips = item.nombrecontactsolucionadorIPS;
                    obj.telefono_contacto_solucionador_ips = item.telefonocontactsolucionador;
                    obj.correo_contacto_solucionador_ips = item.correocontactosolucionador;
                    obj.id_servicio = item.servicio;
           
                    obj.num_documento_solicitante = contact.num_documento_solicitante;
                    obj.nom_solicitante = contact.nom_solicitante;
                    obj.telefono_solicitante = contact.telefono_solicitante;
                    obj.otro_telefono_solicitante = contact.otro_telefono_solicitante;
                    obj.correo_electronico_solicitante = contact.correo_electronico_solicitante;
                    obj.clasificacion_contacto_solicitante = contact.clasificacion_contacto_solicitante;
                    obj.ips_paciente = contact.ips_paciente;
                    obj.num_documento_paciente = contact.num_documento_paciente;
                    obj.nom_paciente = contact.nom_paciente;
                    obj.tipo_beneficiario_paciente = contact.tipo_beneficiario_paciente;
                    obj.regional_paciente = contact.regional_paciente;
                    obj.telefono_paciente = contact.telefono_paciente;
                    obj.otro_telefono_paciente = contact.otro_telefono_paciente;
                    obj.correo_electronico_paciente = contact.correo_electronico_paciente;
                    obj.regional = contact.regional;
                    obj.unis = contact.unis;
                    obj.ciudad = contact.ciudad;
                    obj.ips = contact.Ips;
                    obj.id_auditor = contact.id_auditor;
                    obj.agente_solucionador = contact.agente_solucionador;
                    obj.tel_contacto_solucionador_ips = contact.tel_contacto_solucionador_ips;
                    
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = item.usuario;

                    ListBitacora.Add(obj);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return ListBitacora;
        }

        public List<contact_center_dtll> SetearDatosBitacoraOtro(List<BitacoraSeguimientoOtro> List)
        {
            List<contact_center_dtll> ListBitacora = new List<contact_center_dtll>();
            try
            {

                foreach (var item in List)
                {
                    contact_center_dtll obj = new contact_center_dtll();
                    obj.fecha_ingreso = item.fecha;
                    obj.Observaciones = item.observaciones;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = item.usuario;
                    obj.estado_solicitud = item.estado;
                    obj.id_medio_notificacion = item.medionotificacion;
                    obj.correo_notificacion = item.mailnotificacion;
                    obj.telefono_notificacion = item.telefononotificacion;
                    obj.ruta_imagen = item.ruta;
                    obj.tipo_archivo = item.tipoArchivo;
                    obj.id_tipo_solicitud = item.tipoSolicitud;
                    obj.nom_contacto_solucionador_ips = item.nombrecontactsolucionadorIPS;
                    obj.telefono_contacto_solucionador_ips = item.telefonocontactsolucionador;
                    obj.correo_contacto_solucionador_ips = item.correocontactosolucionador;
                    obj.id_servicio = item.servicio;
                    obj.casoEfectivo = item.casoEfectivo;

                    ListBitacora.Add(obj);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return ListBitacora;
        }

        public List<BitacoraSeguimiento> SetearDatosBitacora2(List<contact_center_dtll> List)
        {
            List<BitacoraSeguimiento> lista = new List<BitacoraSeguimiento>();

            try
            {
                int i = 0;
                foreach (var item in List)
                {
                    i++;
                    BitacoraSeguimiento obj = new BitacoraSeguimiento();
                    obj.id = i;
                    obj.fecha = Convert.ToDateTime(item.fecha_digita);
                    obj.observaciones = item.Observaciones;
                    obj.usuario = item.usuario_digita;
                    obj.estado = item.estado_solicitud.Value;
                    obj.nomestado = BusClass.GetListEstadoSolicitud().Where(l => l.id_ref_contact_estado_solicitud == item.estado_solicitud.Value).FirstOrDefault().descripcion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.tipoArchivo = item.tipo_archivo;
                    obj.tipoSolicitud = (int)item.id_tipo_solicitud;

                    if (item.id_tipo_solicitud != null)
                    {
                        obj.tipoSolicitudDescripcion = BusClass.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == item.id_tipo_solicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = (int)item.id_servicio;

                    if (obj.servicio != null)
                    {
                        obj.servicioDescripcion = BusClass.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }

                    obj.medionotificacion = item.id_medio_notificacion.Value;
                    obj.mailnotificacion = item.correo_notificacion;
                    obj.telefononotificacion = item.telefono_notificacion;
                    obj.ruta = item.ruta_imagen;
                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<BitacoraSeguimientoOtro> SetearDatosBitacora2Otro(List<contact_center_dtll> List)
        {
            List<BitacoraSeguimientoOtro> lista = new List<BitacoraSeguimientoOtro>();

            try
            {
                int i = 0;
                foreach (var item in List)
                {
                    i++;
                    BitacoraSeguimientoOtro obj = new BitacoraSeguimientoOtro();
                    obj.id = i;
                    obj.fecha = Convert.ToDateTime(item.fecha_ingreso);
                    obj.observaciones = item.Observaciones;
                    obj.usuario = item.usuario_digita;
                    obj.estado = item.estado_solicitud.Value;
                    obj.nomestado = BusClass.GetListEstadoSolicitud().Where(l => l.id_ref_contact_estado_solicitud == item.estado_solicitud.Value).FirstOrDefault().descripcion;
                    obj.casoEfectivo = item.casoEfectivo;
                    obj.tipoArchivo = item.tipo_archivo;

                    obj.tipoSolicitud = (int)item.id_tipo_solicitud;

                    if (item.id_tipo_solicitud != null)
                    {
                        obj.tipoSolicitudDescripcion = BusClass.GetListTipoSolicitudBitacora().Where(x => x.id_tipo == item.id_tipo_solicitud).FirstOrDefault().descripcion;
                    }

                    obj.servicio = (int)item.id_servicio;
                    if (obj.servicio != null)
                    {
                        obj.servicioDescripcion = BusClass.GetListTipoServicio().Where(x => x.id_ref_contact_tipo_servicio == obj.servicio).FirstOrDefault().descripcion;
                    }

                    obj.ruta = item.ruta_imagen;
                    obj.medionotificacion = item.id_medio_notificacion.Value;
                    obj.mailnotificacion = item.correo_notificacion;
                    obj.telefononotificacion = item.telefono_notificacion;
                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }



        public int InsertarIngresoContactCenter(contact_center obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarIngresoContactCenter(obj, ref MsgRes);
        }

        public void InsertarBitacoraCallCenter(List<contact_center_dtll> List, int id_contact_center, string usuario)
        {
            BusClass.InsertarBitacoraCallCenter(List, id_contact_center, usuario);
        }

        public int InsertarBitacoraContactCenter(contact_center_dtll obj)
        {
            return BusClass.InsertarBitacoraContactCenter(obj);
        }

        public contact_center GetContactCenterById(int id)
        {
            return BusClass.GetContactCenterById(id);
        }

        public management_contact_centerResult GetContactCenterCensoIdContact(int id)
        {
            return BusClass.GetContactCenterCensoIdContact(id);
        }

        public management_contact_centerResult GetContactCenterCensoIdCenso(int id)
        {
            return BusClass.GetContactCenterCensoIdCenso(id);
        }

        public management_contact_centerResult GetContactCenterCensoIdConcurrencia(int id)
        {
            return BusClass.GetContactCenterCensoIdConcurrencia(id);
        }

        public List<contact_center_dtll> GetListBitacoraByIngreso(int id_contact_center, int? censo, int? idConcurrencia)
        {
            return BusClass.GetListBitacoraByIngreso(id_contact_center, censo, idConcurrencia);
        }

        public List<vw_contact_center> GetListContactCenter(int? estado)
        {
            return BusClass.GetListContactCenter(estado);
        }

        public List<management_contact_centerResult> ListaTableroContactCenter()
        {
            return BusClass.ListaTableroContactCenter(null, null);
        }

        public void ActualizarImagenCaso(string rutaImagen, string tipo, int contactcenter)
        {
            BusClass.ActualizarImagenCaso(rutaImagen, tipo, contactcenter);
        }

        public string ObtenerPath(string ext)
        {
            Guid al = Guid.NewGuid();
            string guid = al.ToString().Substring(0, 8);
            string carpeta = "";
            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
            {
                carpeta = "ContactCenter";
            }
            else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
            {
                carpeta = "ContactCenterPruebas";
            }

            string Rutadocumentos = ConfigurationManager.AppSettings["rutaDocumentosContactCenter"];
            if (!System.IO.Directory.Exists(Rutadocumentos))
            {
                System.IO.Directory.CreateDirectory(Rutadocumentos);
            }

            Rutadocumentos += "//" + carpeta + "//";
            if (!System.IO.Directory.Exists(Rutadocumentos))
            {
                System.IO.Directory.CreateDirectory(Rutadocumentos);
            }

            string filename = guid + ext;
            return Path.Combine(Rutadocumentos, filename);
        }

        public List<ref_contact_solicitante> GetlistSolicitantesbytipo(string term, int tipo)
        {
            return BusClass.GetlistSolicitantesbytipo(term, tipo);
        }

        #endregion


        public int ActualizarContactObligatorios(contact_center obj)
        {
            return BusClass.ActualizarContactObligatorios(obj);
        }

    }



}