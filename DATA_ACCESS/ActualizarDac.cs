using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;


namespace DATA_ACCESS
{
    public class ActualizarDac
    {
        #region PROPIEDADES
        private InsertaDac _DACInserta;
        public InsertaDac DACInserta
        {
            get
            {
                if (_DACInserta != null)
                {
                    return _DACInserta;
                }
                else
                {
                    return _DACInserta = new InsertaDac();
                }

            }
            set { _DACInserta = value; }
        }

        private ConsultasDac _DACConsulta;
        public ConsultasDac DACConsulta
        {
            get
            {
                if (_DACConsulta != null)
                {
                    return _DACConsulta;
                }
                else
                {
                    return _DACConsulta = new ConsultasDac();
                }

            }
            set { _DACConsulta = value; }
        }



        #endregion

        #region LOGIN

        public void ActualizaIngreso(String Strusuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.usuario == Strusuario);
                    objAdi.ultimo_ingreso = DACConsulta.ManagmentHora();
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }

        }
        public void ActualizaContraseña(sis_usuario Usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.usuario == Usuario.usuario);
                    objAdi.contraseña = Usuario.contraseña;
                    objAdi.actualiza_contraseña = DACConsulta.ManagmentHora();
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaContraseñaOlvido(sis_usuario Usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.id_usuario == Usuario.id_usuario);
                    objAdi.contraseña = Usuario.contraseña;
                    objAdi.actualiza_contraseña = DACConsulta.ManagmentHora();
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }
        public void ActualizaEstadoUsuario(sis_usuario Usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.id_usuario == Usuario.id_usuario);
                    objAdi.estado_usuario = Usuario.estado_usuario;
                    objAdi.id_estado = Usuario.id_estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaClaveUsuario(sis_usuario OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario Objsis = db.sis_usuario.Single(p => p.id_usuario == OBJ.id_usuario);
                    Objsis.contraseña = OBJ.contraseña;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaContraseña2(sis_usuario Usuario, ref MessageResponseOBJ MsgRes)
        {
            var existeCodigo = false;
            string Pin = "";
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.usuario == Usuario.usuario);

                    while (existeCodigo == false)
                    {
                        Random generator = new Random();
                        int r = generator.Next(1, 1000000);
                        Pin = r.ToString().PadLeft(6, '0');
                        if (Pin != "")
                        {
                            existeCodigo = true;
                        }
                    }

                    objAdi.codigo = Pin;
                    objAdi.Ultimafechacodigo = Convert.ToDateTime(DateTime.Now);

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizaCodigoIngreso(string usuario, string codigo, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    sis_usuario objAdi = db.sis_usuario.Single(p => p.usuario == usuario);
                    objAdi.codigo = codigo;
                    objAdi.Ultimafechacodigo = Convert.ToDateTime(DateTime.Now);

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }



        #endregion

        #region CENSO

        public void ActualizarFechaEgresoCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == OBJ.id_censo);

                    OBJCenso.fecha_egreso_censo = OBJ.fecha_egreso_censo;
                    OBJCenso.condicion_de_alta = OBJ.condicion_de_alta;
                    OBJCenso.admitido_auditor = OBJ.admitido_auditor;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int ActualizarCensoSacarDelTablero(ecop_censo censo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo cen = db.ecop_censo.Where(x => x.id_censo == censo.id_censo).FirstOrDefault();
                    cen.admitido_auditor = censo.admitido_auditor;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }

        public void ActualizarLeEgresoCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == OBJ.id_censo);

                    OBJCenso.fecha_egreso = OBJ.fecha_egreso;
                    OBJCenso.admitido_auditor = OBJ.admitido_auditor;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarEgresoConcu(ecop_concurrencia OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia OBJCenso = db.ecop_concurrencia.Single(p => p.id_concurrencia == OBJ.id_concurrencia);

                    OBJCenso.fecha_egreso = OBJ.fecha_egreso;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarCenso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == ActualizaSiniestro.id_censo);

                    OBJCenso.tipo_identifi_afiliado = ActualizaSiniestro.tipo_identifi_afiliado;
                    OBJCenso.fecha_recepcion_censo = ActualizaSiniestro.fecha_recepcion_censo;
                    OBJCenso.tipo_ingreso = ActualizaSiniestro.tipo_ingreso;
                    OBJCenso.origen_evento = ActualizaSiniestro.origen_evento;
                    OBJCenso.tipo_habitacion = ActualizaSiniestro.tipo_habitacion;
                    OBJCenso.dias_estancia = ActualizaSiniestro.dias_estancia;
                    OBJCenso.id_medico_auditor = ActualizaSiniestro.id_medico_auditor;
                    OBJCenso.fecha_egreso = ActualizaSiniestro.fecha_egreso;
                    OBJCenso.condicion_alta = ActualizaSiniestro.condicion_alta;
                    OBJCenso.valor_egreso = ActualizaSiniestro.valor_egreso;
                    OBJCenso.dx_egreso = ActualizaSiniestro.dx_egreso;
                    OBJCenso.incapacidad = ActualizaSiniestro.incapacidad;
                    OBJCenso.dx_actual = ActualizaSiniestro.dx_actual;
                    OBJCenso.ips_primaria = ActualizaSiniestro.ips_primaria;
                    OBJCenso.caso_Especial = ActualizaSiniestro.caso_Especial;
                    OBJCenso.caso_Especial_detalle = ActualizaSiniestro.caso_Especial_detalle;
                    OBJCenso.linea_pago = ActualizaSiniestro.linea_pago;

                    OBJCenso.estado_contact = 1;
                    OBJCenso.reingresado = 1;

                    OBJCenso.Numero_factura = Convert.ToString(ActualizaSiniestro.Numero_factura);

                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarCensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == ActualizaSiniestro.id_censo);


                    OBJCenso.id_censo = ActualizaSiniestro.id_censo;
                    OBJCenso.Numero_factura = ActualizaSiniestro.Numero_factura;
                    OBJCenso.valor_egreso = ActualizaSiniestro.valor_egreso;
                    OBJCenso.fecha_factura = ActualizaSiniestro.fecha_factura;
                    OBJCenso.fecha_recepcion_factura = ActualizaSiniestro.fecha_recepcion_factura;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }
        public void CensoEgreso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == ActualizaSiniestro.id_censo);


                    OBJCenso.id_censo = ActualizaSiniestro.id_censo;
                    OBJCenso.fecha_egreso_censo = ActualizaSiniestro.fecha_egreso_censo;
                    OBJCenso.condicion_de_alta = ActualizaSiniestro.condicion_de_alta;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarEgresoCenso(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == ActualizaSiniestro.id_censo);

                    OBJCenso.fecha_egreso_censo = ActualizaSiniestro.fecha_egreso_censo;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizarCensoEgresoOK(ecop_censo ActualizaSiniestro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo OBJCenso = db.ecop_censo.Single(p => p.id_censo == ActualizaSiniestro.id_censo);

                    OBJCenso.fecha_egreso = ActualizaSiniestro.fecha_egreso;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaEgresoConcurrenciaOk(ecop_concurrencia ObjConcurrencia, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == ObjConcurrencia.id_concurrencia);

                    ObjConcu.fecha_egreso = ObjConcurrencia.fecha_egreso;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        #endregion

        #region CONCURRENCIA

        public void ActualizaConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == ObjConcurrencia.id_concurrencia);

                    ObjConcu.afi_tipo_doc = ObjConcurrencia.afi_tipo_doc;
                    ObjConcu.afi_nom = ObjConcurrencia.afi_nom;
                    ObjConcu.id_afi = ObjConcurrencia.id_afi;

                    ObjConcu.afi_edad = ObjConcurrencia.afi_edad;
                    ObjConcu.Genero = ObjConcurrencia.Genero;
                    ObjConcu.afi_dir = ObjConcurrencia.afi_dir;
                    ObjConcu.afi_tel = ObjConcurrencia.afi_tel;
                    ObjConcu.afi_cel = ObjConcurrencia.afi_cel;

                    ObjConcu.afi_contacto_nom = ObjConcurrencia.afi_contacto_nom;
                    ObjConcu.afi_contacto_cel = ObjConcurrencia.afi_contacto_cel;
                    ObjConcu.afi_eps = ObjConcurrencia.afi_eps;
                    ObjConcu.lugar_origen_evento = ObjConcurrencia.lugar_origen_evento;
                    ObjConcu.des_origen = ObjConcurrencia.des_origen;
                    ObjConcu.lugar = ObjConcurrencia.lugar;
                    ObjConcu.fecha_atep = ObjConcurrencia.fecha_atep;
                    ObjConcu.calif_origen = ObjConcurrencia.calif_origen;

                    ObjConcu.id_ips = ObjConcurrencia.id_ips;
                    ObjConcu.num_siniestro = ObjConcurrencia.num_siniestro;
                    ObjConcu.emp_doc = ObjConcurrencia.emp_doc;
                    ObjConcu.emp_nom = ObjConcurrencia.emp_nom;
                    ObjConcu.emp_tel = ObjConcurrencia.emp_tel;
                    ObjConcu.servicio = ObjConcurrencia.servicio;
                    ObjConcu.med_ser_trata = ObjConcurrencia.med_ser_trata;
                    ObjConcu.lesion_severa = ObjConcurrencia.lesion_severa;
                    ObjConcu.id_lesion_severa = ObjConcurrencia.id_lesion_severa;
                    ObjConcu.accidente_grave = ObjConcurrencia.accidente_grave;
                    ObjConcu.id_accidente_grave = ObjConcurrencia.id_accidente_grave;

                    ObjConcu.modo = ObjConcurrencia.modo;
                    ObjConcu.tiempo = ObjConcurrencia.tiempo;
                    ObjConcu.lugar = ObjConcurrencia.lugar;
                    ObjConcu.Ocupacion = ObjConcurrencia.Ocupacion;
                    ObjConcu.miembro_dominante = ObjConcurrencia.miembro_dominante;
                    ObjConcu.Parte_cuerpo_afectada = ObjConcurrencia.Parte_cuerpo_afectada;
                    ObjConcu.reingreso = ObjConcurrencia.reingreso;
                    ObjConcu.id_reingreso = ObjConcurrencia.id_reingreso;
                    ObjConcu.otro_reingreso = ObjConcurrencia.otro_reingreso;
                    ObjConcu.dx1 = ObjConcurrencia.dx1;

                    ObjConcu.fecha_mod = DACConsulta.ManagmentHora();
                    ObjConcu.id_editor = ObjConcurrencia.id_editor;

                    ObjConcu.salud_publica = ObjConcurrencia.salud_publica;
                    ObjConcu.id_salud_publica = ObjConcurrencia.id_salud_publica;

                    if (ObjConcurrencia.hospitalizacion_prevenible == "undefined")
                    {
                        ObjConcu.hospitalizacion_prevenible = "";
                    }
                    else
                    {
                        ObjConcu.hospitalizacion_prevenible = ObjConcurrencia.hospitalizacion_prevenible;

                    }

                    ObjConcu.descripcion_prevenible = ObjConcurrencia.descripcion_prevenible;

                    ObjConcu.Gestantes = ObjConcurrencia.Gestantes;
                    ObjConcu.Trabajador = ObjConcurrencia.Trabajador;
                    ObjConcu.id_origen_hospitalizacion = ObjConcurrencia.id_origen_hospitalizacion;

                    if (ObjConcurrencia.Trabajador == "SI")
                    {
                        ObjConcu.ciudad_trabajador = ObjConcurrencia.ciudad_trabajador;
                    }
                    else
                    {
                        ObjConcu.ciudad_trabajador = "0";
                    }

                    ObjConcu.triage = ObjConcurrencia.triage;
                    ObjConcu.auditoria_telefonica = ObjConcurrencia.auditoria_telefonica;
                    ObjConcu.otro_salud_publica = ObjConcurrencia.otro_salud_publica;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarFacturas_automaticas(int idBaseFactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_actualizarEstado_facturas(idBaseFactura);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ActualizarRutaFirmasDigital(string ruta, int? idFirma)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_crearFirma_digital(ruta, idFirma);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public void ActualizaEgresoConcurrencia(ecop_concurrencia ObjConcurrencia, String User, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == ObjConcurrencia.id_concurrencia);


                    ObjConcu.fecha_egreso = ObjConcurrencia.fecha_egreso;
                    ObjConcu.condicion_egreso = ObjConcurrencia.condicion_egreso;
                    ObjConcu.incapacidad = ObjConcurrencia.incapacidad;
                    ObjConcu.dias_incap_amb = ObjConcurrencia.dias_incap_amb;
                    ObjConcu.condicion_del_egreso = ObjConcurrencia.condicion_del_egreso;
                    ObjConcu.desCondiciondelEgreso = ObjConcurrencia.desCondiciondelEgreso;
                    ObjConcu.FormulaMedica = ObjConcurrencia.FormulaMedica;
                    ObjConcu.diagnostico_calificado = ObjConcurrencia.diagnostico_calificado;
                    ObjConcu.uci = ObjConcurrencia.uci;
                    ObjConcu.cirugia = ObjConcurrencia.cirugia;
                    ObjConcu.cirugia_descripcion = ObjConcurrencia.cirugia_descripcion;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void Actualizarprevenible(ecop_concurrencia Objconcurrencia, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == Objconcurrencia.id_concurrencia);


                    ObjConcu.hospitalizacion_prevenible = Objconcurrencia.hospitalizacion_prevenible;
                    ObjConcu.descripcion_prevenible = Objconcurrencia.descripcion_prevenible;
                    ObjConcu.lesion_severa = Objconcurrencia.lesion_severa;
                    ObjConcu.id_lesion_severa = Objconcurrencia.id_lesion_severa;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarEgreso(egreso_auditoria_Hospitalaria Objegreso, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    egreso_auditoria_Hospitalaria ObjConcu = db.egreso_auditoria_Hospitalaria.Single(p => p.id_concurrencia == Objegreso.id_concurrencia);


                    ObjConcu.fechaTCH = Objegreso.fechaTCH;
                    ObjConcu.resultadoTCH = Objegreso.resultadoTCH;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void actualizarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    categorizacion_egreso_hospitalario objc = db.categorizacion_egreso_hospitalario.Single(p => p.id_categorizacion_egreso_hospitalario == obj.id_categorizacion_egreso_hospitalario);

                    objc.id_tipo_patologia_catastrofica = obj.id_tipo_patologia_catastrofica;
                    objc.id_tipo_hospitalizacion = obj.id_tipo_hospitalizacion;
                    objc.id_condicion_estancia_prolongada = obj.id_condicion_estancia_prolongada;
                    objc.id_pertinencia_estancia_prolongada = obj.id_pertinencia_estancia_prolongada;
                    objc.tipo_medico_admin = obj.tipo_medico_admin;
                    objc.responsable_no_pertinencia = obj.responsable_no_pertinencia;
                    objc.num_dias_no_pertinentes = obj.num_dias_no_pertinentes;
                    objc.valor_aprox_pertinencia = obj.valor_aprox_pertinencia;
                    objc.causal_no_pertinencia = obj.causal_no_pertinencia;
                    objc.observaciones = obj.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarIps(int idconcurrencia, int idips, ref MessageResponseOBJ Msg)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                ecop_concurrencia concu = db.ecop_concurrencia.Where(l => l.id_concurrencia == idconcurrencia).First();
                if (concu != null)
                {
                    concu.id_ips = idips;
                    db.SubmitChanges();

                    ecop_censo censo = db.ecop_censo.Where(l => l.id_censo == concu.id_censo).FirstOrDefault();
                    if (censo != null)
                    {
                        censo.ips_primaria = idips.ToString();
                        db.SubmitChanges();
                    }

                    Msg.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                Msg.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                Msg.DescriptionResponse = ex.Message;
            }

        }

        public void ActualizarFechaEgresoConcucenso(ecop_censo censo, int idconcu, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                ecop_censo obj = db.ecop_censo.Where(l => l.id_censo == censo.id_censo).FirstOrDefault();
                obj.fecha_egreso = censo.fecha_egreso;
                db.SubmitChanges();

                ecop_concurrencia concu = db.ecop_concurrencia.Where(l => l.id_concurrencia == idconcu).FirstOrDefault();
                concu.fecha_egreso = censo.fecha_egreso;
                db.SubmitChanges();

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }

        public void MandarConcurrenciaContactCenter(List<int> listado, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                if (listado.Count() > 0)
                {
                    ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                    try
                    {
                        foreach (var item in listado)
                        {
                            ecop_concurrencia obj = db.ecop_concurrencia.Where(x => x.id_concurrencia == item).FirstOrDefault();
                            obj.enContactCenter = 1;
                            obj.estado_contact = 1;
                            db.SubmitChanges();
                        }

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        throw new Exception(error);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void MandarindividualConcurrenciaContactCenter(int? idConcu, string observacion, int? usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                try
                {
                    ecop_concurrencia obj = db.ecop_concurrencia.Where(x => x.id_concurrencia == idConcu).FirstOrDefault();
                    obj.enContactCenter = 1;
                    obj.estado_contact = 1;

                    if (DevolverExistenciaDetalleContactConcurrencia(idConcu) != 0)
                    {
                        obj.reenviado = 1;
                    }

                    obj.observacion_contact = observacion;
                    obj.fecha_enviaContact = DateTime.Now;
                    obj.usuario_enviaContact = usuario;

                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public int DevolverExistenciaDetalleContactConcurrencia(int? idConcurrencia)
        {
            contact_center_dtll dt = new contact_center_dtll();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dt = db.contact_center_dtll.Where(x => x.id_concurrencia == idConcurrencia && x.estado_solicitud == 2).FirstOrDefault();

                    if (dt != null)
                    {
                        if (dt.id_concurrencia != null && dt.id_concurrencia != 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void ActualizarPlanEgreso(int id_plan_mejora, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                ecop_plan_de_mejora obj = db.ecop_plan_de_mejora.Where(l => l.id_plan_de_mejora == id_plan_mejora).FirstOrDefault();
                obj.estado_plan = estado;

                if (estado == 2)
                {
                    obj.fecha_cierre = DateTime.Now;
                }

                db.SubmitChanges();

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void Actualizarplan_estado_tarea(int id_plan_mejora_tareas, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                ecop_plan_mejora_tareas obj = db.ecop_plan_mejora_tareas.Where(l => l.id_plan_mejora_tareas == id_plan_mejora_tareas).FirstOrDefault();
                obj.id_estado_tarea = estado;
                db.SubmitChanges();

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public int ActualizarDatosPlanMejoraAccion(ecop_plan_mejora_obs_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_plan_mejora_obs_tareas obj2 = db.ecop_plan_mejora_obs_tareas.FirstOrDefault(x => x.id_plan_mejora_obs_tareas == obj.id_plan_mejora_obs_tareas);
                    obj2.id_estado = obj.id_estado;
                    obj2.fecha_seguimiento = obj.fecha_seguimiento;
                    obj2.observacion = obj.observacion;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return (int)obj2.id_plan_de_mejora;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public int ActualizarPlanEgresoAmpliacion(DateTime? fechaAmpliacion, string obsAmpliacion, int? idPlan)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                ecop_plan_de_mejora obj = db.ecop_plan_de_mejora.Where(l => l.id_plan_de_mejora == idPlan).FirstOrDefault();
                obj.fecha_ampliacion = fechaAmpliacion;
                obj.observacion_ampliacion = obsAmpliacion;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }
        public int ActualizarEstadoPlanesDeMejora(int? idPlan)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_plan_mejora_reactivarPlanId(idPlan);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoPlanMejora(int? idPlan, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_plan_de_mejora obj = db.ecop_plan_de_mejora.FirstOrDefault(x => x.id_plan_de_mejora == idPlan);
                    obj.estado_plan = estado;
                    db.SubmitChanges();
                    return obj.id_plan_de_mejora;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarFocoPlanMejora(ecop_plan_mejora_foco_intervencion obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_plan_mejora_foco_intervencion obj2 = db.ecop_plan_mejora_foco_intervencion.FirstOrDefault(x => x.id_plan_mejora_foco_intervencion == obj.id_plan_mejora_foco_intervencion);
                    obj2.hallazgo = obj.hallazgo;
                    obj2.porque_1 = obj.porque_1;
                    obj2.porque_2 = obj.porque_2;
                    obj2.porque_3 = obj.porque_3;
                    obj2.porque_4 = obj.porque_4;
                    obj2.porque_5 = obj.porque_5;
                    obj2.descripcion_problema = obj.descripcion_problema;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return (int)obj2.id_plan_mejora_foco_intervencion;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        #endregion

        #region FACTURAS
        public void ActualizaHallazgosRips(hallazgo_RIPS Objrips, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    hallazgo_RIPS ObjhallazgosRIPS = db.hallazgo_RIPS.Single(p => p.id_hallazgo_RIPS == Objrips.id_hallazgo_RIPS);
                    ObjhallazgosRIPS.fecha_recepcion_nuevo_Rips = Objrips.fecha_recepcion_nuevo_Rips;
                    ObjhallazgosRIPS.fecha_ingreso_gestion = Objrips.fecha_ingreso_gestion;
                    ObjhallazgosRIPS.gestionado = Objrips.gestionado;
                    ObjhallazgosRIPS.usuario_ingreso_gestion = Objrips.usuario_ingreso_gestion;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaFacturaAuditada(factura_sin_censo ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    factura_sin_censo ObjAuditada = db.factura_sin_censo.Single(p => p.id_factura_sin_censo == ObjAudi.id_factura_sin_censo);
                    ObjAuditada.valor_factura_definitiva = ObjAudi.valor_factura_definitiva;
                    ObjAuditada.auditada = ObjAudi.auditada;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaAnalistaAsignado(ref_cuentas_medicas_analista ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cuentas_medicas_analista ObjAuditada = db.ref_cuentas_medicas_analista.Single(p => p.id_ref_cuentas_medicas_analista == ObjAudi.id_ref_cuentas_medicas_analista);
                    ObjAuditada.id_usuario = ObjAudi.id_usuario;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaAuditorAsignado(ref_cuentas_medicas_auditores ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cuentas_medicas_auditores ObjAuditada = db.ref_cuentas_medicas_auditores.Single(p => p.id_ref_cuentas_medicas_auditores == ObjAudi.id_ref_cuentas_medicas_auditores);
                    ObjAuditada.id_usuario = ObjAudi.id_usuario;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        #endregion

        #region RIPS

        public bool ActualizaRips(RIPS ObjRips, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    RIPS Obj = db.RIPS.Single(p => p.id_rips == ObjRips.id_rips);

                    Obj.estado = ObjRips.estado;
                    Obj.rips_nom = ObjRips.rips_nom;
                    Obj.descripcion = ObjRips.descripcion;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return true;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return false;
            }
        }

        #endregion

        #region PQRS

        public void ActualizarPQRS(ecop_PQRS ObjPqrs, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS ObjAuditada = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == ObjPqrs.id_ecop_PQRS);

                    var fechaExiste = ObjAuditada.fecha_envio_prestador;

                    ObjAuditada.estado_gestion = ObjPqrs.estado_gestion;
                    ObjAuditada.id_pqrs_subtematica = ObjPqrs.id_pqrs_subtematica;
                    ObjAuditada.prestador = ObjPqrs.prestador;
                    ObjAuditada.fecha_gestion = ObjPqrs.fecha_gestion;


                    if (ObjAuditada.estado_gestion == 5)
                    {
                        ObjAuditada.fecha_envio_revision = ObjPqrs.fecha_envio_revision;
                        ObjAuditada.usuario_envio_revision = ObjPqrs.usuario_envio_revision;
                    }

                    ObjAuditada.requirio_llamada = ObjPqrs.requirio_llamada;
                    ObjAuditada.a_quie_llamo = ObjPqrs.a_quie_llamo;
                    ObjAuditada.nombre_quien_llamo = ObjPqrs.nombre_quien_llamo;
                    ObjAuditada.observacion_gestion = ObjPqrs.observacion_gestion;
                    ObjAuditada.validez = ObjPqrs.validez;
                    ObjAuditada.atributo = ObjPqrs.atributo;
                    ObjAuditada.observacion_ampliacion = ObjPqrs.observacion_ampliacion;
                    ObjAuditada.fecha_ampliacion = ObjPqrs.fecha_ampliacion;
                    ObjAuditada.evento_adverso = ObjPqrs.evento_adverso;
                    if (ObjPqrs.estado_gestion == 2)
                    {
                        if (fechaExiste == null)
                        {
                            ObjAuditada.fecha_envio_prestador = ObjPqrs.fecha_envio_prestador;
                        }
                        else
                        {
                            if ((bool)ObjPqrs.fecha_envio_prestador_opcion)
                            {
                                ObjAuditada.fecha_envio_prestador = ObjPqrs.fecha_envio_prestador;
                            }
                        }
                    }
                    else if (ObjPqrs.estado_gestion == 7)
                    {
                        ObjAuditada.fecha_recepcion_prestador = ObjPqrs.fecha_recepcion_prestador;
                    }

                    ObjAuditada.fecha_envio_proyectada = ObjPqrs.fecha_envio_proyectada;
                    ObjAuditada.auditor_asignado = ObjPqrs.auditor_asignado;
                    ObjAuditada.vobo_auditor = ObjPqrs.vobo_auditor;
                    ObjAuditada.id_vobo_auditor = ObjPqrs.id_vobo_auditor;
                    ObjAuditada.doc_beneficiario = ObjPqrs.doc_beneficiario;
                    ObjAuditada.edad_beneficiario = ObjPqrs.edad_beneficiario;
                    ObjAuditada.ciudad_del_caso = ObjPqrs.ciudad_del_caso;
                    ObjAuditada.regional = ObjPqrs.regional;
                    ObjAuditada.analisis_caso = ObjPqrs.analisis_caso;
                    ObjAuditada.pasa_auditor = ObjPqrs.pasa_auditor;
                    ObjAuditada.otro_prestador = ObjPqrs.otro_prestador;
                    //ObjAuditada.reabierto = ObjPqrs.reabierto;
                    //ObjAuditada.devuelto_en_cierre = ObjPqrs.devuelto_en_cierre;

                    db.SubmitChanges();

                    ecop_PQRS_DetalleG ObjAudit = new ecop_PQRS_DetalleG();

                    ObjAudit.id_ecop_PQRS = ObjPqrs.id_ecop_PQRS;
                    ObjAudit.estado_gestion = ObjPqrs.estado_gestion;
                    ObjAudit.id_pqrs_subtematica = ObjPqrs.id_pqrs_subtematica;
                    ObjAudit.prestador = ObjPqrs.prestador;
                    ObjAudit.fecha_gestion = ObjPqrs.fecha_gestion;
                    ObjAudit.requirio_llamada = ObjPqrs.requirio_llamada;
                    ObjAudit.a_quie_llamo = ObjPqrs.a_quie_llamo;
                    ObjAudit.nombre_quien_llamo = ObjPqrs.nombre_quien_llamo;
                    ObjAudit.observacion_gestion = ObjPqrs.observacion_gestion;
                    ObjAudit.validez = ObjPqrs.validez;
                    ObjAudit.atributo = ObjPqrs.atributo;
                    ObjAudit.observacion_ampliacion = ObjPqrs.observacion_ampliacion;
                    ObjAudit.fecha_ampliacion = ObjPqrs.fecha_ampliacion;
                    ObjAudit.vobo_auditor = ObjPqrs.vobo_auditor;
                    ObjAudit.id_vobo_auditor = ObjPqrs.id_vobo_auditor;
                    ObjAudit.doc_beneficiario = ObjPqrs.doc_beneficiario;
                    ObjAudit.edad_beneficiario = ObjPqrs.edad_beneficiario;
                    ObjAudit.analisis_caso = ObjPqrs.analisis_caso;
                    ObjAudit.componente = ObjPqrs.componente;
                    ObjAudit.otro_prestador = ObjPqrs.otro_prestador;

                    ObjAudit.usuario_digita = ObjPqrs.usuario_ingreso;
                    ObjAudit.fecha_digita = DateTime.Now;

                    ActualizarPQRSDetalle(ObjAudit, ref MsgRes);

                    //ecop_PQRS_Auditor audi = new ecop_PQRS_Auditor();

                    //audi.id_ecop_PQRS = ObjPqrs.id_ecop_PQRS;
                    //ActualizarPQRSAuditor(audi);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarPQRSDetalle(ecop_PQRS_DetalleG ObjPqrs, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    db.ecop_PQRS_DetalleG.InsertOnSubmit(ObjPqrs);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarPQRSAuditor(ecop_PQRS_Auditor obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS_Auditor obj2 = db.ecop_PQRS_Auditor.Where(x => x.id_ecop_PQRS == obj.id_ecop_PQRS).OrderByDescending(x => x.id_ecop_PQRS_auditor).FirstOrDefault();
                    if (obj2 != null)
                    {
                        obj2.vobo = null;
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public int? ActualizarPQRSAuditorId(int? idPqrs)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS_Auditor obj2 = db.ecop_PQRS_Auditor.Where(x => x.id_ecop_PQRS == idPqrs).OrderByDescending(x => x.id_ecop_PQRS_auditor).FirstOrDefault();
                    if (obj2 != null)
                    {
                        obj2.vobo = null;
                        db.SubmitChanges();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void ActualizarEstadoEnrevisionpryectada(ecop_PQRS_enrevision OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS_enrevision CON = db.ecop_PQRS_enrevision.Single(p => p.id_ecop_PQRA_enrevision == OBJ.id_ecop_PQRA_enrevision);

                    CON.estado_revision = OBJ.estado_revision;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarFechaPQRS(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS ObjAuditada = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == id_ecop_PQRS);

                    ObjAuditada.fecha_envio_correo = Convert.ToDateTime(DateTime.Now);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizaestadoPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS_enrevision ObjAuditada = db.ecop_PQRS_enrevision.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);

                    ObjAuditada.estado_revision = obj.estado_revision;
                    ObjAuditada.pdf_enrevision = obj.pdf_enrevision;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizarGestionPQRSEnrevision(ecop_PQRS_enrevision obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS_enrevision ObjAuditada = db.ecop_PQRS_enrevision.Single(p => p.id_ecop_PQRA_enrevision == obj.id_ecop_PQRA_enrevision);

                    ObjAuditada.estado_revision = obj.estado_revision;
                    ObjAuditada.observacion_auditor = obj.observacion_auditor;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizaReabrirPQRS(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS ObjAuditada = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);

                    ObjAuditada.fecha_gestion = obj.fecha_gestion;
                    ObjAuditada.estado_gestion = obj.estado_gestion;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarFechaPQRSDirec(Int32 id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS ObjAuditada = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == id_ecop_PQRS);

                    ObjAuditada.fecha_envio_correo_direc = Convert.ToDateTime(DateTime.Now);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int ActualizarPqrsEstado(ecop_PQRS obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS Obj2 = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);
                    Obj2.pasa_auditor = obj.pasa_auditor;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Obj2.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void ActualizarEnvioPQRS(Int32 id_ecop_PQRS, String usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS ObjAuditada = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == id_ecop_PQRS);

                    ObjAuditada.fecha_envio_ecopetrol = Convert.ToDateTime(DateTime.Now);
                    ObjAuditada.usuario_envio_ecopetrol = Convert.ToString(usuario);
                    ObjAuditada.estado_gestion = 6;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }
        public int ActualizarUsuarioAsignado(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS CON = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == OBJ.id_ecop_PQRS);

                    CON.usuario_asignado = OBJ.usuario_asignado;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return CON.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int ActualizarCategorizacionPQR(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS CON = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == OBJ.id_ecop_PQRS);

                    //CON.id_ref_categorizacon = OBJ.id_ref_categorizacon;
                    CON.supersalud = OBJ.supersalud;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return CON.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public int ActualizarAvanzarProyectada(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS CON = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == OBJ.id_ecop_PQRS);

                    CON.estado_gestion = OBJ.estado_gestion;
                    CON.observacion_aprobacion = OBJ.observacion_aprobacion;
                    CON.fecha_envio_proyectada = OBJ.fecha_envio_proyectada;
                    CON.pasa_auditor = OBJ.pasa_auditor;
                    db.SubmitChanges();

                    ecop_PQRS_DetalleG ObjAudit = new ecop_PQRS_DetalleG();

                    ObjAudit.id_ecop_PQRS = OBJ.id_ecop_PQRS;
                    ObjAudit.estado_gestion = OBJ.estado_gestion;
                    ObjAudit.id_pqrs_subtematica = 0;
                    //ObjAudit.prestador = 0;
                    ObjAudit.fecha_gestion = DateTime.Now;
                    ObjAudit.requirio_llamada = "NA";
                    ObjAudit.a_quie_llamo = 0;
                    ObjAudit.nombre_quien_llamo = "NA";
                    ObjAudit.observacion_gestion = CON.observacion_aprobacion;
                    ObjAudit.validez = "NA";
                    ObjAudit.atributo = 0;
                    ObjAudit.observacion_ampliacion = "NA";
                    ObjAudit.usuario_digita = OBJ.usuario_ingreso;

                    ActualizarPQRSDetalle(ObjAudit, ref MsgRes);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return CON.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int ActualizarCerrarProyectada(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS CON = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == OBJ.id_ecop_PQRS);

                    CON.estado_gestion = OBJ.estado_gestion;
                    CON.observacion_aprobacionCierre = OBJ.observacion_aprobacionCierre;
                    CON.fecha_envio_proyectadaCierre = OBJ.fecha_envio_proyectadaCierre;
                    CON.reabierto = OBJ.reabierto;
                    CON.devuelto_en_cierre = OBJ.devuelto_en_cierre;
                    db.SubmitChanges();

                    ecop_PQRS_DetalleG ObjAudit = new ecop_PQRS_DetalleG();

                    ObjAudit.id_ecop_PQRS = OBJ.id_ecop_PQRS;
                    ObjAudit.estado_gestion = OBJ.estado_gestion;
                    ObjAudit.id_pqrs_subtematica = 0;
                    //ObjAudit.prestador = 0;
                    ObjAudit.fecha_gestion = DateTime.Now;
                    ObjAudit.requirio_llamada = "NA";
                    ObjAudit.a_quie_llamo = 0;
                    ObjAudit.nombre_quien_llamo = "NA";
                    ObjAudit.validez = "NA";
                    ObjAudit.atributo = 0;
                    ObjAudit.observacion_ampliacion = "NA";
                    ObjAudit.observacion_gestion = OBJ.observacion_aprobacionCierre;
                    ObjAudit.usuario_digita = OBJ.usuario_ingreso;

                    ActualizarPQRSDetalle(ObjAudit, ref MsgRes);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return CON.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int ActualizarDatosReaperturaPQR(ecop_PQRS OBJ)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS CON = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == OBJ.id_ecop_PQRS);

                    CON.observacionReapertura = OBJ.observacionReapertura;
                    CON.observacionReaperturaseguimiento = OBJ.observacionReaperturaseguimiento;
                    CON.reabierto = OBJ.reabierto;
                    CON.reabiertoSeguimiento = OBJ.reabiertoSeguimiento;
                    CON.estado_gestion = OBJ.estado_gestion;
                    db.SubmitChanges();
                    return CON.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int ActualizarAnalistaAsignadoPqr(ecop_PQRS obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS obj2 = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);

                    obj2.usuario_asignado = obj.usuario_asignado;
                    db.SubmitChanges();
                    return obj2.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarPasaArchivoPqrinicial(ecop_PQRS obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS obj2 = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);
                    obj2.conArchivo = 1;

                    db.SubmitChanges();
                    return obj2.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int CerrarCasoPqrSolucionador(ecop_PQRS obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_PQRS obj2 = db.ecop_PQRS.Single(p => p.id_ecop_PQRS == obj.id_ecop_PQRS);
                    obj2.cerradoSolucionador = obj.cerradoSolucionador;
                    db.SubmitChanges();
                    return obj2.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizaConteoPqrsAnalistas()
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_pqrs_actualizaConteoACeroAnalistas();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region Procesos Internos

        /// <summary>
        /// Metodo utilizado para actualizar los items de un capitulo
        /// </summary>
        /// <param name="objitem"></param>
        /// <returns></returns>
        public bool ActualizarItemCapitulo(item_capitulo objitem)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    item_capitulo Obj = db.item_capitulo.Where(p => p.id_item == objitem.id_item).FirstOrDefault();
                    Obj.indicador_id = objitem.indicador_id;
                    Obj.capitulo_id = objitem.capitulo_id;
                    Obj.Peso_individual = objitem.Peso_individual;
                    Obj.condicion_meta = objitem.condicion_meta;
                    Obj.valor_meta = objitem.valor_meta;
                    Obj.nom_item = objitem.nom_item;
                    Obj.activo = objitem.activo;
                    Obj.Aplica = objitem.Aplica;
                    Obj.Valor_digitado = objitem.Valor_digitado;
                    Obj.condicion_especial = objitem.condicion_especial;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                return false;
            }
        }

        /// <summary>
        /// metodo para actualizar los capitulos de un indicador
        /// </summary>
        /// <param name="idcapitulo"></param>
        /// <param name="idindicador"></param>
        /// <param name="pesogen"></param>
        /// <param name="nomcapitulo"></param>
        /// <returns></returns>
        public bool ActualizarCapituloIndicador(Int32 idcapituloIndicador, int pesogen)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    capitulo_indicador cap = db.capitulo_indicador.Where(l => l.id_cap_indicador == idcapituloIndicador).FirstOrDefault();
                    cap.peso_general_capitulo = pesogen;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return false;
            }
        }



        /// <summary>
        /// metodo utilizado para actualizar el cronograma de visitas 
        /// </summary>
        /// <param name="objcronograma"></param>
        /// <param name="MsgRes"></param>
        public void ActualizarCronogramaVisitas(cronograma_visitas objcronograma, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cronograma_visitas obj = db.cronograma_visitas.Single(p => p.id_cronograma_visitas == objcronograma.id_cronograma_visitas);
                    if (!string.IsNullOrEmpty(objcronograma.Auditor_Asignado))
                    {
                        obj.Auditor_Asignado = objcronograma.Auditor_Asignado;
                    }

                    obj.fecha_visita = objcronograma.fecha_visita;
                    obj.Realizo_evaluacion = objcronograma.Realizo_evaluacion;
                    obj.usuario_modificacion = objcronograma.usuario_modificacion;
                    obj.fecha_modificacion = DateTime.Now;
                    obj.Observaciones_Evaluacion = objcronograma.Observaciones_Evaluacion;
                    obj.motivo_actualizacion = objcronograma.motivo_actualizacion;
                    obj.periodo_fecha_inicio = objcronograma.periodo_fecha_inicio;
                    obj.periodo_fecha_final = objcronograma.periodo_fecha_final;
                    obj.fecha_final_visita = objcronograma.fecha_final_visita;
                    obj.calificacion_final_visita = objcronograma.calificacion_final_visita;
                    obj.id_tipo_prestador = objcronograma.id_tipo_prestador;
                    obj.proxima_fecha_visita = objcronograma.proxima_fecha_visita;
                    obj.version_pdf = objcronograma.version_pdf;
                    obj.id_contrato = objcronograma.id_contrato;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int ActualizarHallazgoVisitas(ver_evaluacion_hallazgo obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_evaluacion_hallazgo obj2 = db.ver_evaluacion_hallazgo.Where(x => x.id_total == obj.id_total && x.id_tipoCriterio == obj.id_tipoCriterio).FirstOrDefault();

                    obj2.fecha_respuesta = obj.fecha_respuesta;
                    obj2.observacion = obj.observacion;
                    obj2.tipo_hallazgo = obj.tipo_hallazgo;
                    obj2.estado = obj.estado;
                    obj2.cumplimiento = obj.cumplimiento;
                    obj2.tipo_soporte = obj.tipo_soporte;
                    db.SubmitChanges();

                    return obj2.id_hallazgo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarVisitaDispensacion(ver_dispen_evaluacion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_dispen_evaluacion obj2 = db.ver_dispen_evaluacion.Where(x => x.id_evaluacion == obj.id_evaluacion).FirstOrDefault();

                    obj2.id_dispensacion = obj.id_dispensacion;
                    obj2.total_peso = obj.total_peso;
                    obj2.total_resultado = obj.total_resultado;
                    obj2.resultado = obj.resultado;
                    obj2.recurso_humano = obj.recurso_humano;
                    obj2.condiciones_locativasDotacion = obj.condiciones_locativasDotacion;
                    obj2.procesos_generales = obj.procesos_generales;
                    obj2.procesos_especiales = obj.procesos_especiales;
                    obj2.hallazgos = obj.hallazgos;
                    obj2.observacion_adicionales = obj.observacion_adicionales;
                    obj2.fecha_digita = DateTime.Now;
                    obj2.usuario_digita = obj.usuario_digita;
                    obj2.estado = obj.estado;

                    db.SubmitChanges();
                    return obj2.id_evaluacion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarDesVerificacionFarmaceutica(int? id, string desc)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_verificacion_farmaceutico obj = db.ref_verificacion_farmaceutico.FirstOrDefault(x => x.id_veriticacion == id);
                    obj.descripcion = desc;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void actualizarPrestador(calidad_prestadores Obj, int idprestador)
        {

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                calidad_prestadores prestador = db.calidad_prestadores.Where(l => l.id_prestador == idprestador).FirstOrDefault();
                prestador.id_regional = Obj.id_regional;
                prestador.cod_habilitacion = Obj.cod_habilitacion;
                prestador.cod_sap = Obj.cod_sap;
                prestador.razon_social = Obj.razon_social;
                prestador.tipo_id_prestador = Obj.tipo_id_prestador;
                prestador.no_id_prestador = Obj.no_id_prestador;
                prestador.tipo_contrato = Obj.tipo_contrato;
                prestador.nom_representante_legal = Obj.nom_representante_legal;
                prestador.direccion = Obj.direccion;
                prestador.telefono_1 = Obj.telefono_1;
                prestador.telefono_2 = Obj.telefono_2;
                prestador.telefono_3 = Obj.telefono_3;
                prestador.celular = Obj.celular;
                prestador.fax = Obj.fax;
                prestador.correo_electronico = Obj.correo_electronico;
                prestador.pagina_web = Obj.pagina_web;
                prestador.id_muni_dane = Obj.id_muni_dane;
                prestador.muni_dane = Obj.muni_dane;
                prestador.actividad_economica = Obj.actividad_economica;
                prestador.clase_persona = Obj.clase_persona;
                prestador.ambito = Obj.ambito;
                prestador.regimen_tributario = Obj.regimen_tributario;
                prestador.Autoretenedor = Obj.Autoretenedor;
                prestador.Porcentaje_ICA = Obj.Porcentaje_ICA;
                prestador.Poblacion_Capitada = Obj.Poblacion_Capitada;
                prestador.Días_Ofertados = Obj.Días_Ofertados;
                prestador.Horas_ofertadas = Obj.Horas_ofertadas;
                prestador.fecha_inicio_contrato = Obj.fecha_inicio_contrato;
                prestador.fecha_fin_contrato = Obj.fecha_fin_contrato;
                prestador.num_contrato = Obj.num_contrato;
                prestador.especialidad = Obj.especialidad;
                prestador.tipo_prestador = Obj.tipo_prestador;
                prestador.tipo_localidad = Obj.tipo_localidad;
                db.SubmitChanges();

            }

        }

        public int ActualizarContratosPrestadorVisitas(string sap, DateTime? fechaInicio, DateTime? fechaFin, string numContrato, int? tipo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_visitasActualizar_contratos(sap, fechaInicio, fechaFin, numContrato, tipo);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region medicamentos

        public void ActualizarObligacionesDetalleMD(md_obligaciones_contractuales_detalle OBJObligacionesContractualesDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_obligaciones_contractuales_detalle obj = db.md_obligaciones_contractuales_detalle.Single(p => p.id_md_obligaciones_contractuales_detalle == OBJObligacionesContractualesDetalle.id_md_obligaciones_contractuales_detalle && p.id_obligaciones_contractuales == OBJObligacionesContractualesDetalle.id_obligaciones_contractuales);

                    obj.valor = OBJObligacionesContractualesDetalle.valor;
                    obj.resultado = OBJObligacionesContractualesDetalle.resultado;
                    obj.observaciones = OBJObligacionesContractualesDetalle.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarObligacionesMD(md_obligaciones_contractuales OBJObligacionesContractuales, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_obligaciones_contractuales obj = db.md_obligaciones_contractuales.Single(p => p.id_obligaciones_contractuales == OBJObligacionesContractuales.id_obligaciones_contractuales);

                    obj.resultado = OBJObligacionesContractuales.resultado;
                    obj.hallazgos = OBJObligacionesContractuales.hallazgos;
                    obj.estado = OBJObligacionesContractuales.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarIndicadoresMedicamentos(md_indicadores_detalle OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_indicadores_detalle obj = db.md_indicadores_detalle.Single(p => p.id_md_ref_indicador == OBJIndicadoresMD.id_md_ref_indicador && p.id_indicadores_medicamentos == OBJIndicadoresMD.id_indicadores_medicamentos);

                    obj.valor = OBJIndicadoresMD.valor;
                    obj.resultado = OBJIndicadoresMD.resultado;
                    obj.observaciones = OBJIndicadoresMD.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarIndicadoresMD(md_indicadores OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_indicadores obj = db.md_indicadores.Single(p => p.id_indicadores_medicamentos == OBJIndicadoresMD.id_indicadores_medicamentos);

                    obj.resultado = OBJIndicadoresMD.resultado;
                    obj.hallazgos = OBJIndicadoresMD.hallazgos;
                    obj.estado = OBJIndicadoresMD.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarIndicadoresMDEstado(md_indicadores OBJIndicadoresMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_indicadores obj = db.md_indicadores.Single(p => p.id_indicadores_medicamentos == OBJIndicadoresMD.id_indicadores_medicamentos);

                    obj.estado = OBJIndicadoresMD.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarDetallet1(md_herramienta_tec_detalle_t1 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec_detalle_t1 obj = db.md_herramienta_tec_detalle_t1.Single(p => p.id_md_detalle_tabla1 == OBJDetalleT.id_md_detalle_tabla1);

                    obj.valor = OBJDetalleT.valor;
                    obj.resultado = OBJDetalleT.resultado;
                    obj.observaciones = OBJDetalleT.observaciones;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarDetallet2(md_herramienta_tec_detalle_t2 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec_detalle_t2 obj = db.md_herramienta_tec_detalle_t2.Single(p => p.id_md_detalle_tabla2 == OBJDetalleT.id_md_detalle_tabla2);

                    obj.valor = OBJDetalleT.valor;
                    obj.resultado = OBJDetalleT.resultado;
                    obj.observaciones = OBJDetalleT.observaciones;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarDetallet3(md_herramienta_tec_detalle_t3 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec_detalle_t3 obj = db.md_herramienta_tec_detalle_t3.Single(p => p.id_md_detalle_tabla3 == OBJDetalleT.id_md_detalle_tabla3);

                    obj.valor = OBJDetalleT.valor;
                    obj.resultado = OBJDetalleT.resultado;
                    obj.observaciones = OBJDetalleT.observaciones;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarDetallet4(md_herramienta_tec_detalle_t4 OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec_detalle_t4 obj = db.md_herramienta_tec_detalle_t4.Single(p => p.id_md_detalle_tabla4 == OBJDetalleT.id_md_detalle_tabla4);

                    obj.valor = OBJDetalleT.valor;
                    obj.resultado = OBJDetalleT.resultado;
                    obj.observaciones = OBJDetalleT.observaciones;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }

        public void ActualizarGeneral1(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec obj = db.md_herramienta_tec.Single(p => p.id_herramienta_tec_med == OBJDetalleT.id_herramienta_tec_med);

                    obj.hallazgosT1 = OBJDetalleT.hallazgosT1;
                    obj.resultadoT1 = OBJDetalleT.resultadoT1;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarGeneral2(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec obj = db.md_herramienta_tec.Single(p => p.id_herramienta_tec_med == OBJDetalleT.id_herramienta_tec_med);

                    obj.hallazgosT2 = OBJDetalleT.hallazgosT2;
                    obj.resultadoT2 = OBJDetalleT.resultadoT2;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarGeneral3(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec obj = db.md_herramienta_tec.Single(p => p.id_herramienta_tec_med == OBJDetalleT.id_herramienta_tec_med);

                    obj.hallazgosT3 = OBJDetalleT.hallazgosT3;
                    obj.resultadoT3 = OBJDetalleT.resultadoT3;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }
        public void ActualizarGeneral4(md_herramienta_tec OBJDetalleT, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_herramienta_tec obj = db.md_herramienta_tec.Single(p => p.id_herramienta_tec_med == OBJDetalleT.id_herramienta_tec_med);

                    obj.hallazgosT4 = OBJDetalleT.hallazgosT4;
                    obj.resultadoT4 = OBJDetalleT.resultadoT4;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                String variable = ex.Message;
            }
        }


        public void ActualizarInterventoriaGeneralMD(md_interventoria_general OBJInterventoriaGeneral, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_interventoria_general obj = db.md_interventoria_general.Single(p => p.id_md_interventoria_general == OBJInterventoriaGeneral.id_md_interventoria_general);

                    obj.resultado = OBJInterventoriaGeneral.resultado;
                    obj.hallazgos = OBJInterventoriaGeneral.hallazgos;
                    obj.estado = OBJInterventoriaGeneral.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarInterventoriaGeneralDetalle1MD(md_interventoria_general_detalle1 OBJDetallleG1, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_interventoria_general_detalle1 obj = db.md_interventoria_general_detalle1.Single(p => p.id_md_interventoria_general_detalle1 == OBJDetallleG1.id_md_interventoria_general_detalle1 && p.id_md_interventoria_general == OBJDetallleG1.id_md_interventoria_general);

                    obj.valor = OBJDetallleG1.valor;
                    obj.resultado = OBJDetallleG1.resultado;
                    obj.observaciones = OBJDetallleG1.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarInterventoriaGeneralDetalle2MD(md_interventoria_general_detalle2 OBJDetallleG2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_interventoria_general_detalle2 obj = db.md_interventoria_general_detalle2.Single(p => p.id_md_interventoria_general_detalle2 == OBJDetallleG2.id_md_interventoria_general_detalle2 && p.id_md_interventoria_general == OBJDetallleG2.id_md_interventoria_general);

                    obj.valor = OBJDetallleG2.valor;
                    obj.resultado = OBJDetallleG2.resultado;
                    obj.observaciones = OBJDetallleG2.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarInterventoriaGeneralDetalle3MD(md_interventoria_general_detalle3 OBJDetallleG3, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_interventoria_general_detalle3 obj = db.md_interventoria_general_detalle3.Single(p => p.id_md_interventoria_general_detalle3 == OBJDetallleG3.id_md_interventoria_general_detalle3 && p.id_md_interventoria_general == OBJDetallleG3.id_md_interventoria_general);

                    obj.valor = OBJDetallleG3.valor;
                    obj.resultado = OBJDetallleG3.resultado;
                    obj.observaciones = OBJDetallleG3.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarInterventoriaGeneralDetalle4MD(md_interventoria_general_detalle4 OBJDetallleG4, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_interventoria_general_detalle4 obj = db.md_interventoria_general_detalle4.Single(p => p.id_md_interventoria_general_detalle4 == OBJDetallleG4.id_md_interventoria_general_detalle4 && p.id_md_interventoria_general == OBJDetallleG4.id_md_interventoria_general);

                    obj.valor = OBJDetallleG4.valor;
                    obj.resultado = OBJDetallleG4.resultado;
                    obj.observaciones = OBJDetallleG4.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarSeguimientoPendientesMD(md_seguimiento_pendientes OBJSeguimientoPendientes, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_seguimiento_pendientes obj = db.md_seguimiento_pendientes.Single(p => p.id_md_seguimiento_pendientes == OBJSeguimientoPendientes.id_md_seguimiento_pendientes);

                    obj.resultado = OBJSeguimientoPendientes.resultado;
                    obj.hallazgo = OBJSeguimientoPendientes.hallazgo;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarSeguimientoPendientesDetalleMD(md_seguimiento_pendientes_detalle OBJSeguimientoPendientesDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_seguimiento_pendientes_detalle obj = db.md_seguimiento_pendientes_detalle.Single(p => p.id_md_seguimiento_pendientes_detalle == OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes_detalle && p.id_md_seguimiento_pendientes == OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes);

                    obj.valor = OBJSeguimientoPendientesDetalle.valor;
                    obj.resultado = OBJSeguimientoPendientesDetalle.resultado;
                    obj.observaciones = OBJSeguimientoPendientesDetalle.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarDispersacionAmbulatorio(md_dispensacion_ambulatoria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_dispensacion_ambulatoria_detalle obj = db.md_dispensacion_ambulatoria_detalle.Single(p => p.id_md_ref_dispens_ambulatoria == OBJMD.id_md_ref_dispens_ambulatoria && p.id_md_dispensacion_ambulatoria == OBJMD.id_md_dispensacion_ambulatoria);

                    obj.valor = OBJMD.valor;
                    obj.resultado = OBJMD.resultado;
                    obj.observaciones = OBJMD.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarDispersacionHospitalizacion(md_dispensacion_hospitalaria_detalle OBJMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_dispensacion_hospitalaria_detalle obj = db.md_dispensacion_hospitalaria_detalle.Single(p => p.id_md_ref_dispens_hospitalaria == OBJMD.id_md_ref_dispens_hospitalaria && p.id_md_dispensacion_hospitalaria == OBJMD.id_md_dispensacion_hospitalaria);

                    obj.valor = OBJMD.valor;
                    obj.resultado = OBJMD.resultado;
                    obj.observaciones = OBJMD.observaciones;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarAmbulatoriaMD(md_dispensacion_ambulatoria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_dispensacion_ambulatoria obj = db.md_dispensacion_ambulatoria.Single(p => p.id_md_dispensacion_ambulatoria == OBJMD.id_md_dispensacion_ambulatoria);

                    obj.resultado = OBJMD.resultado;
                    obj.hallazgos = OBJMD.hallazgos;
                    obj.estado = OBJMD.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void ActualizarHospitalariaMD(md_dispensacion_hospitalaria OBJMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_dispensacion_hospitalaria obj = db.md_dispensacion_hospitalaria.Single(p => p.id_md_dispensacion_hospitalaria == OBJMD.id_md_dispensacion_hospitalaria);

                    obj.resultado = OBJMD.resultado;
                    obj.hallazgos = OBJMD.hallazgos;
                    obj.estado = OBJMD.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }


        public void ActualizarControlGastos(md_control_gastos OBJMD, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_control_gastos obj = db.md_control_gastos.Single(p => p.id_presupuesto_ejecutado == OBJMD.id_presupuesto_ejecutado);

                    obj.valor_facturado_con_aval = OBJMD.valor_facturado_con_aval;
                    obj.valor_retenido_por_glosa = OBJMD.valor_retenido_por_glosa;
                    obj.valor_anticipo_generado = OBJMD.valor_anticipo_generado;
                    obj.mes_ingresado = OBJMD.mes_ingresado;
                    obj.año = Convert.ToString(OBJMD.año);
                    obj.fecha_ingreso = Convert.ToDateTime(OBJMD.fecha_ingreso);
                    obj.usuario_ingreso = Convert.ToString(OBJMD.usuario_ingreso);

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        //Kevin suarez
        public void ActualizarPrefactura(md_prefacturas_detalle obj)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    md_prefacturas_detalle prefactura = db.md_prefacturas_detalle.Where(l => l.id_detalle_prefactura == obj.id_detalle_prefactura).FirstOrDefault();
                    prefactura.observaciones = obj.observaciones;
                    prefactura.nuevo_valor = obj.nuevo_valor;
                    prefactura.aprobado = obj.aprobado;
                    prefactura.desaprobada = obj.desaprobada;
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }

        public void ActualizarPrefacturaCargue(int? cargueBase, int? fase, string usuario, int? terminado)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    md_prefacturas_cargue_base prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == cargueBase).FirstOrDefault();

                    if (terminado == 1)
                    {
                        prefactura.usuario_terminaValidacion = usuario;
                        prefactura.fase_validacion = 4;
                        prefactura.fecha_fin_validacion3 = DateTime.Now;
                        prefactura.usuario_valida_3 = usuario;
                    }

                    else
                    {
                        if (fase == 1)
                        {
                            prefactura.usuario_comienzaValidacion = usuario;
                            prefactura.fecha_inicio_validacion1 = DateTime.Now;
                            prefactura.fase_validacion = fase;
                        }
                        else if (fase == 2)
                        {
                            prefactura.fecha_inicio_validacion2 = DateTime.Now;
                            prefactura.fase_validacion = fase;
                        }
                        else if (fase == 3)
                        {
                            prefactura.fecha_inicio_validacion3 = DateTime.Now;
                            prefactura.fase_validacion = fase;
                        }
                    }

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }



        public void ActualizarPrefacturaCargueFase(int? cargueBase, int? fase, string usuario)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    md_prefacturas_cargue_base prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == cargueBase).FirstOrDefault();
                    prefactura.fase_validacion = fase + 1;

                    if (fase == 1)
                    {
                        prefactura.fecha_fin_validacion1 = DateTime.Now;
                        prefactura.usuario_valida_1 = usuario;
                    }

                    else if (fase == 2)
                    {
                        prefactura.fecha_fin_validacion2 = DateTime.Now;
                        prefactura.usuario_valida_2 = usuario;
                    }

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }

        public int ActualizarPrefacturaCargueFaseDevolver(int? cargueBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    md_prefacturas_cargue_base prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == cargueBase).FirstOrDefault();
                    prefactura.enValidacion = 0;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarFasePrefacturas(int? cargueBase, int? fase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    md_prefacturas_cargue_base prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == cargueBase).FirstOrDefault();
                    prefactura.fase_validacion = fase - 1;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEnValidacionPrefacturas(int? cargueBase, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    md_prefacturas_cargue_base prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == cargueBase).FirstOrDefault();
                    prefactura.enValidacion = estado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarPrefacturaLista(List<int> ListaIds, string observaciones, double nuevo_valor)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    foreach (int item in ListaIds)
                    {
                        md_prefacturas_detalle prefactura = db.md_prefacturas_detalle.Where(l => l.id_detalle_prefactura == item).FirstOrDefault();
                        prefactura.observaciones = observaciones;
                        prefactura.nuevo_valor = (decimal?)nuevo_valor;
                        //prefactura.valor_total = (decimal?)nuevo_valor;
                        prefactura.aprobado = true;
                        prefactura.desaprobada = 0;
                        db.SubmitChanges();
                    }
                    return 1;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }


        public int DesaprobarPrefacturas(List<int> ListaIds, string observacionDesaprobacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    foreach (int item in ListaIds)
                    {
                        var iddetalle = item;
                        md_prefacturas_detalle prefactura = db.md_prefacturas_detalle.Where(l => l.id_detalle_prefactura == (int)item).FirstOrDefault();
                        prefactura.desaprobada = 1;
                        prefactura.aprobado = false;
                        prefactura.observacion_desaprobacion = observacionDesaprobacion;
                        db.SubmitChanges();

                        md_prefactura_contador conta = db.md_prefactura_contador.Where(x => x.id_detalle == item).FirstOrDefault();
                        conta.pasa = 2;
                        db.SubmitChanges();
                    }

                    return 1;
                }
            }

            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }

        }


        public void ActualizarPrefacturaListaTotal(int idCargue, string observaciones, double nuevo_valor)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.management_prefacturaActualizar_todos(idCargue, observaciones, nuevo_valor);
                }
                catch (Exception e)
                {
                    var error = e.Message;
                }
            }
        }

        public int ActualizarPrefacturaCerrar(md_prefacturas_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_cerrarPorActualizacion(obj.remision_prefactura_fact, obj.Id_prefactura_cargue_base);
                    return 1;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }


        public int ActualizarAlertaDispensacionDetalle(alertas_dispensacion_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    alertas_dispensacion_detalle det = db.alertas_dispensacion_detalle.FirstOrDefault(x => x.id_detalle == obj.id_detalle);

                    det.id_alerta = obj.id_alerta;
                    det.id_registro = obj.id_registro;
                    det.factura = obj.factura;
                    det.numero_factura = obj.numero_factura;
                    det.valor_facturado = obj.valor_facturado;
                    det.diagnostico_asociaco_medicamento_hc = obj.diagnostico_asociaco_medicamento_hc;
                    det.es_medicamento_pertinente = obj.es_medicamento_pertinente;
                    det.cantidad_dispensada_corresponde_prescrita = obj.cantidad_dispensada_corresponde_prescrita;
                    det.cantidades_pertinentes = obj.cantidades_pertinentes;
                    det.es_desviacion = obj.es_desviacion;
                    det.causa_desviacion = obj.causa_desviacion;
                    det.responsable_desviacion = obj.responsable_desviacion;
                    det.plan_accion = obj.plan_accion;
                    det.observacion = obj.observacion;
                    det.confirmacion_alerta_dispensacion = obj.confirmacion_alerta_dispensacion;
                    det.tipoDato = obj.tipoDato;
                    det.fecha_terminada = obj.fecha_terminada;

                    det.tiene_soporte = obj.tiene_soporte;
                    det.observacion_soporte = obj.observacion_soporte;
                    det.fecha_recepcion_soporte = obj.fecha_recepcion_soporte;

                    db.SubmitChanges();

                    return det.id_detalle;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region AnalisisCaso

        public void ActualizarAnalisisAlertas(analisis_caso_alertas AnalisisA, ref MessageResponseOBJ MsgRes)
        {
            analisis_caso_alertas ObjAnalisisAlerta = new analisis_caso_alertas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ObjAnalisisAlerta = db.analisis_caso_alertas.Single(l => l.id_analisis_caso_alertas == AnalisisA.id_analisis_caso_alertas);
                    ObjAnalisisAlerta.id_regional = AnalisisA.id_regional;
                    ObjAnalisisAlerta.id_ciudad = AnalisisA.id_ciudad;
                    ObjAnalisisAlerta.id_ips = AnalisisA.id_ips;
                    ObjAnalisisAlerta.tipo_documento = AnalisisA.tipo_documento;
                    ObjAnalisisAlerta.numero_identificacion = AnalisisA.numero_identificacion;
                    ObjAnalisisAlerta.nombres_completos = AnalisisA.nombres_completos;
                    ObjAnalisisAlerta.edad = AnalisisA.edad;
                    ObjAnalisisAlerta.sexo = AnalisisA.sexo;
                    ObjAnalisisAlerta.nombre_mega = AnalisisA.nombre_mega;
                    ObjAnalisisAlerta.fecha_revision = AnalisisA.fecha_revision;
                    ObjAnalisisAlerta.tipo_evento = AnalisisA.tipo_evento;
                    ObjAnalisisAlerta.nombre_alerta = AnalisisA.nombre_alerta;
                    ObjAnalisisAlerta.descripcion_alerta = AnalisisA.descripcion_alerta;
                    ObjAnalisisAlerta.fecha_registro = AnalisisA.fecha_registro;
                    ObjAnalisisAlerta.diagnostico_ingreso = AnalisisA.diagnostico_egreso;
                    ObjAnalisisAlerta.resumen_caso = AnalisisA.resumen_caso;
                    ObjAnalisisAlerta.analisis_caso = AnalisisA.analisis_caso;
                    ObjAnalisisAlerta.fuente_informacion = AnalisisA.fuente_informacion;
                    ObjAnalisisAlerta.eventual_causa_evento = AnalisisA.eventual_causa_evento;
                    ObjAnalisisAlerta.fallos_calidad = AnalisisA.fallos_calidad;
                    ObjAnalisisAlerta.alerta_evitable = AnalisisA.alerta_evitable;
                    ObjAnalisisAlerta.conclusiones = AnalisisA.conclusiones;
                    ObjAnalisisAlerta.recomendaciones = AnalisisA.recomendaciones;
                    ObjAnalisisAlerta.plan_mejora = AnalisisA.plan_mejora;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarAnalisisMuestreo(analisis_caso_muestreo AnalisisM, ref MessageResponseOBJ MsgRes)
        {
            analisis_caso_muestreo ObjAnalisisMuestreo = new analisis_caso_muestreo();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ObjAnalisisMuestreo = db.analisis_caso_muestreo.Single(l => l.id_analisis_caso_muestreo == AnalisisM.id_analisis_caso_muestreo);
                    ObjAnalisisMuestreo.regional_origen = AnalisisM.regional_origen;
                    ObjAnalisisMuestreo.numero_casos = AnalisisM.numero_casos;
                    ObjAnalisisMuestreo.id_ips = AnalisisM.id_ips;
                    ObjAnalisisMuestreo.tipo_documento = AnalisisM.tipo_documento;
                    ObjAnalisisMuestreo.numero_documento = AnalisisM.numero_documento;
                    ObjAnalisisMuestreo.nombres_paciente = AnalisisM.nombres_paciente;
                    ObjAnalisisMuestreo.edad = AnalisisM.edad;
                    ObjAnalisisMuestreo.sexo = AnalisisM.sexo;
                    ObjAnalisisMuestreo.nombre_mega = AnalisisM.nombre_mega;
                    ObjAnalisisMuestreo.fecha_revision = AnalisisM.fecha_revision;
                    ObjAnalisisMuestreo.fecha_ultimo_control = AnalisisM.fecha_ultimo_control;
                    ObjAnalisisMuestreo.cohorte_pertenece = AnalisisM.cohorte_pertenece;
                    ObjAnalisisMuestreo.antecedentes_personales = AnalisisM.antecedentes_personales;
                    ObjAnalisisMuestreo.descripcion_atenciones_ambulatorias = AnalisisM.descripcion_atenciones_ambulatorias;
                    ObjAnalisisMuestreo.numero_controles = AnalisisM.numero_controles;
                    ObjAnalisisMuestreo.diagnostico_ingreso = AnalisisM.diagnostico_ingreso;
                    ObjAnalisisMuestreo.diagnostico_egreso = AnalisisM.diagnostico_egreso;
                    ObjAnalisisMuestreo.plan_manejo_definido_ultimo_control = AnalisisM.plan_manejo_definido_ultimo_control;
                    ObjAnalisisMuestreo.conclusion_caso_ambulatorio = AnalisisM.conclusion_caso_ambulatorio;
                    ObjAnalisisMuestreo.adherencia_del_paciente = AnalisisM.adherencia_del_paciente;
                    ObjAnalisisMuestreo.plan_mejora_ppe = AnalisisM.plan_mejora_ppe;
                    ObjAnalisisMuestreo.recomendaciones_ambulatorio = AnalisisM.recomendaciones_ambulatorio;
                    ObjAnalisisMuestreo.auditor_asalud = AnalisisM.auditor_asalud;
                    ObjAnalisisMuestreo.fecha_ingreso = AnalisisM.fecha_ingreso;
                    ObjAnalisisMuestreo.fecha_egreso = AnalisisM.fecha_egreso;
                    ObjAnalisisMuestreo.descripcion_hospitalizacion = AnalisisM.descripcion_hospitalizacion;
                    ObjAnalisisMuestreo.concepto_auditoria = AnalisisM.concepto_auditoria;
                    ObjAnalisisMuestreo.conclusion_caso_hospitalario = AnalisisM.conclusion_caso_hospitalario;
                    ObjAnalisisMuestreo.hospitalizacion_evitable = AnalisisM.hospitalizacion_evitable;
                    ObjAnalisisMuestreo.recomendaciones_hospitalario = AnalisisM.recomendaciones_hospitalario;
                    ObjAnalisisMuestreo.definicion_integral_caso = AnalisisM.definicion_integral_caso;

                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }



        public void ActualizarAnalisisCasos(analisis_caso_original Analisis, ref MessageResponseOBJ MsgRes)
        {
            analisis_caso_original ObjAnalisis = new analisis_caso_original();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ObjAnalisis = db.analisis_caso_original.Single(l => l.id_analisis_caso_original == Analisis.id_analisis_caso_original);
                    ObjAnalisis.fecha_analisis = Analisis.fecha_analisis;
                    ObjAnalisis.solicitud = Analisis.solicitud;
                    ObjAnalisis.id_regional = Analisis.id_regional;
                    ObjAnalisis.tipo_evento = Analisis.tipo_evento;
                    ObjAnalisis.fecha_inicio_evento = Analisis.fecha_inicio_evento;
                    ObjAnalisis.fecha_fin_evento = Analisis.fecha_fin_evento;
                    ObjAnalisis.id_ips = Analisis.id_ips;
                    ObjAnalisis.prestadores_intervinientes = Analisis.prestadores_intervinientes;
                    ObjAnalisis.objeto_alcance_actividad = Analisis.objeto_alcance_actividad;
                    ObjAnalisis.marco_legal = Analisis.marco_legal;
                    ObjAnalisis.cie101 = Analisis.cie101;
                    ObjAnalisis.cie102 = Analisis.cie102;
                    ObjAnalisis.cie103 = Analisis.cie103;
                    ObjAnalisis.resumen_secuencial_caso = Analisis.resumen_secuencial_caso;
                    ObjAnalisis.analisis_clinico_caso = Analisis.analisis_clinico_caso;
                    ObjAnalisis.aplicacion_metodologia = Analisis.aplicacion_metodologia;
                    ObjAnalisis.eventuales_causas = Analisis.eventuales_causas;
                    ObjAnalisis.eventuales_fallas_control = Analisis.eventuales_fallas_control;
                    ObjAnalisis.eventuales_fallas_calidad = Analisis.eventuales_fallas_calidad;
                    ObjAnalisis.fuentes_info = Analisis.fuentes_info;
                    ObjAnalisis.conclucion_analisis = Analisis.conclucion_analisis;
                    ObjAnalisis.prevenible_atribuible = Analisis.prevenible_atribuible;
                    ObjAnalisis.costo_no_calidad = Analisis.eventuales_fallas_calidad;
                    ObjAnalisis.hallazgos_legal = Analisis.hallazgos_legal;
                    ObjAnalisis.cumplimientos_indicadores = Analisis.cumplimientos_indicadores;
                    ObjAnalisis.patologias_eventos_centinelas = Analisis.patologias_eventos_centinelas;
                    ObjAnalisis.pertinencia_acciones = Analisis.pertinencia_acciones;
                    ObjAnalisis.eficacia_acciones = Analisis.eficacia_acciones;
                    ObjAnalisis.recomendaciones_mejora = Analisis.recomendaciones_mejora;
                    ObjAnalisis.compromiso_mejora = Analisis.compromiso_mejora;
                    ObjAnalisis.evaluacion_impacto = Analisis.evaluacion_impacto;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ActualizarAnalisisCasoSaludPublica(analisis_caso_salud_publica analisis, ref MessageResponseOBJ MsgRes)
        {
            analisis_caso_salud_publica ObjAnalisis = new analisis_caso_salud_publica();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ObjAnalisis = db.analisis_caso_salud_publica.Single(l => l.id_analisis_caso_salud_publica == analisis.id_analisis_caso_salud_publica);
                    ObjAnalisis.id_concurrencia = analisis.id_concurrencia;
                    ObjAnalisis.fecha_del_analisis = analisis.fecha_del_analisis;
                    ObjAnalisis.edad_momento_analisis = analisis.edad_momento_analisis;
                    ObjAnalisis.tipo_evento = analisis.tipo_evento;
                    ObjAnalisis.fecha_ocurrencia_evento = analisis.fecha_ocurrencia_evento;
                    ObjAnalisis.fuente_del_reporte = analisis.fuente_del_reporte;
                    ObjAnalisis.nombre_reportante = analisis.nombre_reportante;
                    ObjAnalisis.id_ips = analisis.id_ips;
                    ObjAnalisis.entidades_responsables = analisis.entidades_responsables;
                    ObjAnalisis.objetivo_auditoria = analisis.objetivo_auditoria;
                    ObjAnalisis.descripcion_evento = analisis.descripcion_evento;
                    ObjAnalisis.id_ref_ambito_evento = analisis.id_ref_ambito_evento;
                    ObjAnalisis.Resumen_clinico_evento = analisis.Resumen_clinico_evento;
                    ObjAnalisis.concepto_primer_nivel = analisis.concepto_primer_nivel;
                    ObjAnalisis.guias_terapeuticas = analisis.guias_terapeuticas;
                    ObjAnalisis.periocidad_controles = analisis.periocidad_controles;
                    ObjAnalisis.pruebas_diagnosticas = analisis.pruebas_diagnosticas;
                    ObjAnalisis.plan_terapeutico = analisis.plan_terapeutico;
                    ObjAnalisis.eventuales_causas_muerte = analisis.eventuales_causas_muerte;
                    ObjAnalisis.concepto_auditoria = analisis.concepto_auditoria;
                    ObjAnalisis.propuesta_acciones = analisis.propuesta_acciones;
                    ObjAnalisis.relacion_anexos = analisis.relacion_anexos;
                    ObjAnalisis.firmas = analisis.firmas;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void ActualizarAnalisisEventosSalud(ecop_concurrencia_eventos_en_asalud Analisis, ref MessageResponseOBJ MsgRes)
        {
            ecop_concurrencia_eventos_en_asalud analisis = new ecop_concurrencia_eventos_en_asalud();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    analisis = db.ecop_concurrencia_eventos_en_asalud.Single(l => l.id_ecop_concurrencia_eventos_en_asalud == Analisis.id_ecop_concurrencia_eventos_en_asalud);
                    analisis.id_concurrencia = Analisis.id_concurrencia;
                    analisis.fecha_analisis = Analisis.fecha_analisis;
                    analisis.examenes_hallazgos = Analisis.examenes_hallazgos;
                    analisis.cie10_antes_del_evento = Analisis.cie10_antes_del_evento;
                    analisis.cie10_resultado_evento = Analisis.cie10_resultado_evento;
                    analisis.conducta_inmediata = Analisis.conducta_inmediata;
                    analisis.id_ref_evento_adv = Analisis.id_ref_evento_adv;
                    analisis.acciones_inseguras = Analisis.acciones_inseguras;
                    analisis.id_decisiones = Analisis.id_decisiones;
                    analisis.otros_general_acciones_inseguras = Analisis.otros_general_acciones_inseguras;
                    analisis.otros_general_factores_contibutivos = Analisis.otros_general_factores_contibutivos;
                    analisis.id_habilidad = Analisis.id_habilidad;

                    analisis.id_percepcion = Analisis.id_percepcion;

                    analisis.id_rutinaria = Analisis.id_rutinaria;

                    analisis.id_excepcionales = Analisis.id_excepcionales;

                    analisis.factores_contributivos = Analisis.factores_contributivos;
                    analisis.id_paciente = Analisis.id_paciente;

                    analisis.id_tarea_tecnologia = Analisis.id_tarea_tecnologia;

                    analisis.id_equipo_trabajo = Analisis.id_equipo_trabajo;

                    analisis.id_individuo = Analisis.id_individuo;

                    analisis.id_ambiente_trabajo = Analisis.id_ambiente_trabajo;

                    analisis.id_organizacion_gerencia = Analisis.id_organizacion_gerencia;

                    analisis.id_contexto = Analisis.id_contexto;

                    analisis.falla_activa_final = Analisis.falla_activa_final;
                    analisis.probabilidad_repeticion = Analisis.probabilidad_repeticion;
                    analisis.id_tipo_evento = Analisis.id_tipo_evento;
                    analisis.prevenible = Analisis.prevenible;
                    analisis.concluciones = Analisis.concluciones;
                    analisis.seguimiento_auditoria = Analisis.seguimiento_auditoria;
                    analisis.equipo_analisis = Analisis.equipo_analisis;
                    analisis.id_severidad = Analisis.id_severidad;
                    db.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public void Actualizargestionanalisisdecaso(GestionAnalisisDeCasos Analisis, ref MessageResponseOBJ MsgRes)
        {
            GestionAnalisisDeCasos caso = new GestionAnalisisDeCasos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    caso = db.GestionAnalisisDeCasos.Where(l => l.id_tipo_analisis_caso == Analisis.id_tipo_analisis_caso && l.id_analisis_caso == Analisis.id_analisis_caso).FirstOrDefault();
                    caso.estado_caso = Analisis.estado_caso;
                    caso.id_analisis_caso = Analisis.id_analisis_caso;
                    caso.observacion = Analisis.observacion;
                    caso.justificacion_rechazo = Analisis.justificacion_rechazo;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public String ActualizarRutaByteMed(GestionDocumentalMedicamentos obj, ref MessageResponseOBJ MsgRes)
        {
            GestionDocumentalMedicamentos caso = new GestionDocumentalMedicamentos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    caso = db.GestionDocumentalMedicamentos.Where(l => l.id_gestion_documental__medicamentos == obj.id_gestion_documental__medicamentos).FirstOrDefault();
                    caso.ruta_byte = obj.ruta_byte;
                    db.SubmitChanges();
                    var rutaok = Convert.ToString(caso.ruta_byte);
                    return rutaok;

                }
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

                return mensaje;
            }
        }



        public String ActualizarRutasDocsVisitas(cronograma_visita_documento obj, ref MessageResponseOBJ MsgRes)
        {

            cronograma_visita_documento caso = new cronograma_visita_documento();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    caso = db.cronograma_visita_documento.Where(l => l.id_cronograma_visita == obj.id_cronograma_visita).FirstOrDefault();
                    caso.ruta = obj.ruta;
                    db.SubmitChanges();
                    var rutaok = Convert.ToString(caso.ruta);
                    return rutaok;

                }
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

                return mensaje;
            }

        }

        public void ActualizarRutaDocumentoVisitasCronograma(string ruta, int? idVisita)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_cronograma_visita_documento_crearRuta(ruta, idVisita);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }



        public String ActualizarRutaBytePQRS(GestionDocumentalPQRS2 obj, ref MessageResponseOBJ MsgRes)
        {
            GestionDocumentalPQRS2 caso = new GestionDocumentalPQRS2();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    caso = db.GestionDocumentalPQRS2.Where(l => l.id_gestion_documental_pqrs == obj.id_gestion_documental_pqrs).FirstOrDefault();
                    caso.ruta = obj.ruta;
                    db.SubmitChanges();
                    var rutaok = Convert.ToString(caso.ruta);
                    return rutaok;

                }
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

                return mensaje;
            }
        }

        public String ActualizarRutaByteMedCalidad(GestionDocumentalMedicamentosCad obj, ref MessageResponseOBJ MsgRes)
        {
            GestionDocumentalMedicamentosCad caso = new GestionDocumentalMedicamentosCad();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    caso = db.GestionDocumentalMedicamentosCad.Where(l => l.id_gestion_documental__medicamentos_cad == obj.id_gestion_documental__medicamentos_cad).FirstOrDefault();
                    caso.ruta_byte = obj.ruta_byte;
                    db.SubmitChanges();
                    var rutaok = Convert.ToString(caso.ruta_byte);
                    return rutaok;

                }
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;

                return mensaje;
            }
        }


        #endregion

        #region Adherencias

        public void ActualizarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_adh_tipo_criterio model = db.ref_adh_tipo_criterio.Where(l => l.id_tipo_criterio == obj.id_tipo_criterio).FirstOrDefault();
                    model.indice = obj.indice;
                    model.nom_tipo_criterion = obj.nom_tipo_criterion;
                    //model.puntaje = obj.puntaje;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    adh_criterio obj = db.adh_criterio.Where(l => l.id_adh_criterio == criterio.id_adh_criterio).FirstOrDefault();
                    obj.descripcion = criterio.descripcion;
                    obj.puntaje = criterio.puntaje;
                    obj.id_tipo_criterio = criterio.id_tipo_criterio;
                    obj.atributo = criterio.atributo;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }


        public void ActualizarTipoCohorte(ref_cohortes obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cohortes cohorte = db.ref_cohortes.Where(l => l.id_ref_cohortes == obj.id_ref_cohortes).FirstOrDefault();
                    cohorte.descripcion = obj.descripcion;
                    db.SubmitChanges();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region ODONTOLOGIA

        public void ActualizarOdontPlanMejora(odont_plan_mejora_dtll obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora_dtll obj = db.odont_plan_mejora_dtll.Where(l => l.id_odont_plan_mejora_dtll == obj2.id_odont_plan_mejora_dtll).FirstOrDefault();

                    obj.estado = obj2.estado;
                    obj.fecha_ingreso = obj2.fecha_ingreso;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        public void ActualizarOdontPlanMejoraBeneficiario(odont_plan_mejora_beneficiario_dtll obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora_beneficiario_dtll obj = db.odont_plan_mejora_beneficiario_dtll.Where(l => l.id_odont_plan_mejora_beneficiario_dtll == obj2.id_odont_plan_mejora_beneficiario_dtll).FirstOrDefault();

                    obj.estado = obj2.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        public void ActualizarOdontPlanMejoraObsTratamiento(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora obj = db.odont_plan_mejora.Where(l => l.id_odont_plan_mejora == obj2.id_odont_plan_mejora).FirstOrDefault();


                    obj.estado = obj2.estado;
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(obj2.usuario_ingreso);

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        public void ActualizarOdontPlanMejoraObsBeneficiario(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora_beneficiario obj = db.odont_plan_mejora_beneficiario.Where(l => l.id_odont_plan_mejora_beneficiario == obj2.id_odont_plan_mejora_beneficiario).FirstOrDefault();


                    obj.estado = obj2.estado;
                    obj.observacion_tratamiento = obj2.observacion_tratamiento;
                    obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    obj.usuario_ingreso = Convert.ToString(obj2.usuario_ingreso);

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void ActualizarOdontPlanMejoraCierreTrat(odont_plan_mejora obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora obj = db.odont_plan_mejora.Where(l => l.id_odont_plan_mejora == obj2.id_odont_plan_mejora).FirstOrDefault();


                    obj.estado = obj2.estado;
                    obj.fecha_cierre = obj2.fecha_cierre;
                    obj.usuario_cierre = obj2.usuario_cierre;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        public void ActualizarOdontPlanMejoraCierreBen(odont_plan_mejora_beneficiario obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_plan_mejora_beneficiario obj = db.odont_plan_mejora_beneficiario.Where(l => l.id_odont_plan_mejora_beneficiario == obj2.id_odont_plan_mejora_beneficiario).FirstOrDefault();


                    obj.estado = obj2.estado;
                    obj.fecha_cierre = obj2.fecha_cierre;
                    obj.usuario_cierre = obj2.usuario_cierre;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void ActualizarOdontHCdtll1(odont_historia_clinica_detalle_calidad obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_historia_clinica_detalle_calidad obj = db.odont_historia_clinica_detalle_calidad.Where(l => l.id_odont_historia_clinica_detalle == obj2.id_odont_historia_clinica_detalle).FirstOrDefault();


                    obj.calificacionf = obj2.calificacionf;
                    obj.calificacion_ponderadaf = obj2.calificacion_ponderadaf;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        public void ActualizarOdontHCdtll2(odont_historia_clinica_detalle_contenido obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_historia_clinica_detalle_contenido obj = db.odont_historia_clinica_detalle_contenido.Where(l => l.id_odont_historia_clinica_detalle_contenido == obj2.id_odont_historia_clinica_detalle_contenido).FirstOrDefault();


                    obj.calificacionc = obj2.calificacionc;
                    obj.calificacion_ponderadac = obj2.calificacion_ponderadac;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void ActualizarOdontHCdtllFinal(odont_historia_clinica_paciente obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    odont_historia_clinica_paciente obj = db.odont_historia_clinica_paciente.Where(l => l.id_odont_historia_clinica_paciente == obj2.id_odont_historia_clinica_paciente).FirstOrDefault();

                    obj.estado = 2;
                    obj.observaciones = obj2.observaciones;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }

        public void ActualizarEstadoSeguimientoCovid19(cargue_seguimiento_covid19 OBJ2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_seguimiento_covid19 objDesa = db.cargue_seguimiento_covid19.Single(p => p.id == OBJ2.id);

                    objDesa.estado = OBJ2.estado;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }





        #endregion

        #region FFMM

        public void ActualizarEstadoRadicacion(ffmm_Cuentas_radicacion obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ffmm_Cuentas_radicacion obj = db.ffmm_Cuentas_radicacion.Where(l => l.id_ref_ffmm_radicacion_Cuentas == obj2.id_ref_ffmm_radicacion_Cuentas).FirstOrDefault();

                    obj.estado = obj2.estado;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void ActualizarEstadoGlosa(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ffmm_cuentas_glosas obj = db.ffmm_cuentas_glosas.Where(l => l.id_ffmm_Cuentas_auditoria == obj2.id_ffmm_Cuentas_auditoria).FirstOrDefault();

                    obj.fecha_primera_conciliacion = obj2.fecha_primera_conciliacion;
                    obj.vlr_aceptado_primera_conciliacion = obj2.vlr_aceptado_primera_conciliacion;
                    obj.vlr_levantado_primera_conciliacion = obj2.vlr_levantado_primera_conciliacion;
                    obj.vlr_glosa_ratificada_contratista_segunda_conciliacion = obj2.vlr_glosa_ratificada_contratista_segunda_conciliacion;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }


        public void ActualizarEstadoGlosaSegundaConci(ffmm_cuentas_glosas obj2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ffmm_cuentas_glosas obj = db.ffmm_cuentas_glosas.Where(l => l.id_ffmm_Cuentas_auditoria == obj2.id_ffmm_Cuentas_auditoria).FirstOrDefault();

                    obj.fecha_segunda_conciliacion = obj2.fecha_segunda_conciliacion;
                    obj.vlr_aceptado_segunda_conciliacion = obj2.vlr_aceptado_segunda_conciliacion;
                    obj.vlr_levantado_segunda_conciliacion = obj2.vlr_levantado_segunda_conciliacion;
                    obj.vlr_glosa_definitiva_segunda_conciliacion = obj2.vlr_glosa_definitiva_segunda_conciliacion;
                    obj.descripcion_glosa_definitiva = obj2.descripcion_glosa_definitiva;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public int ActualizarIpsPrestadores(ref_ffmm_ips_prestadores obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_ffmm_ips_prestadores obj2 = db.ref_ffmm_ips_prestadores.Where(x => x.nit == obj.nit).FirstOrDefault();
                    obj2.tipo = obj.tipo;
                    obj2.nit = obj.nit;
                    obj2.nombre = obj.nombre;
                    obj2.codigohabilitacion = obj.codigohabilitacion;
                    obj2.cod_departamento = obj.cod_departamento;
                    obj2.cod_municipio = obj.cod_municipio;
                    obj2.digito_verificacion = obj.digito_verificacion;
                    obj2.direccion = obj.direccion;
                    obj2.telefono = obj.telefono;
                    obj2.najunombre = obj.najunombre;
                    obj2.red_interna = obj.red_interna;
                    db.SubmitChanges();
                    return obj2.ip_ips_proveedor;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public List<Management_actualizar_FacturaDigResult> ActualizarFFMMFacturas(Int32 Id_factura, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            List<Management_actualizar_FacturaDigResult> list = new List<Management_actualizar_FacturaDigResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Management_actualizar_FacturaDig(Id_factura, estado).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return list;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return list;
            }
        }


        public Int32 UpdateGlosa(ffmm_glosas objeto, string caso, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ffmm_actas_glosas ob = new ffmm_actas_glosas();


                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ffmm_glosas objetoConsultado = db.ffmm_glosas.Where(l => l.id_glosa == objeto.id_glosa).FirstOrDefault();

                    switch (caso)
                    {
                        case "Ejecutar":
                            objetoConsultado.fecha_ejecucion_visita = objeto.fecha_ejecucion_visita;
                            objetoConsultado.observaciones = objeto.observaciones;
                            objetoConsultado.estado = objeto.estado;
                            break;
                        case "Aprobar":
                            objetoConsultado.fecha_aprobacion_acta = objeto.fecha_aprobacion_acta;
                            if (objetoConsultado.fecha_firma_acta == null)
                            {
                                objetoConsultado.estado = 2; /// VISITA  CON FECHA PROGRAMADA 
                            }
                            else
                            {
                                objetoConsultado.estado = 3; /// VISITA CON FECHA EJECUTADA 
                            }
                            break;
                        case "Finalizar":
                            if (objetoConsultado.fecha_aprobacion_acta == null)
                            {
                                objetoConsultado.estado = 2;  /// ACTA APROBADA 
                            }
                            else
                            {
                                objetoConsultado.estado = 3; /// ACTA FIRMADA 
                            }
                            objetoConsultado.fecha_firma_acta = objeto.fecha_firma_acta;
                            break;

                        case "PrimeraConciliacion":
                            //objetoConsultado.fecha_respuesta_glosa_inicial = objeto.fecha_respuesta_glosa_inicial;
                            objetoConsultado.codigo_respuesta_glosa = objeto.codigo_respuesta_glosa;
                            //objetoConsultado.descripcion_respuesta_glosa = objeto.descripcion_respuesta_glosa;
                            objetoConsultado.vlr_aceptado_respuesta_glosa = objeto.vlr_aceptado_respuesta_glosa;
                            objetoConsultado.vlr_levantado_contratista_res_glosa = objeto.vlr_levantado_contratista_res_glosa;
                            objetoConsultado.vlr_levantado_contratista_res_glosa = objeto.vlr_levantado_contratista_res_glosa;
                            objetoConsultado.fecha_primera_conciliacion = objeto.fecha_primera_conciliacion;
                            objetoConsultado.vlr_aceptado_primera_conciliacion = objeto.vlr_aceptado_primera_conciliacion;
                            objetoConsultado.vlr_levantado_primera_conciliacion = objeto.vlr_levantado_primera_conciliacion;
                            objetoConsultado.vlr_glosa_ratificada_res_glosa_primera_conciliacion = objeto.vlr_glosa_ratificada_res_glosa_primera_conciliacion;

                            objetoConsultado.vlr_glosa_ratificada_contratista_segunda_conciliacion = objeto.vlr_glosa_ratificada_contratista_segunda_conciliacion;
                            if (objetoConsultado.vlr_glosa_ratificada_contratista_segunda_conciliacion > 0)
                            {
                                objetoConsultado.estado = 4; // PARA PRIMERA CONCILIACIÓN 

                            }
                            else
                            {
                                objetoConsultado.estado = 3; // FINALIZADA 

                            }

                            break;

                        case "SegundaConciliacion":
                            objetoConsultado.fecha_segunda_conciliacion = objeto.fecha_segunda_conciliacion;
                            objetoConsultado.vlr_aceptado_segunda_conciliacion = objeto.vlr_aceptado_segunda_conciliacion;
                            objetoConsultado.vlr_levantado_segunda_conciliacion = objeto.vlr_levantado_segunda_conciliacion;
                            objetoConsultado.vlr_glosa_definitiva_segunda_conciliacion = objeto.vlr_glosa_definitiva_segunda_conciliacion;
                            objetoConsultado.descripcion_glosa_definitiva = objeto.descripcion_glosa_definitiva;
                            objetoConsultado.estado = 3; // FINALIZADA 
                            break;
                        case "FechaPrimeraConciliacion":
                            objetoConsultado.fecha_acta_primera_conciliacion = objeto.fecha_acta_primera_conciliacion;
                            //insertar en la tabla x. 
                            ob.fecha_acta = objeto.fecha_acta_primera_conciliacion;
                            ob.tipo_acta = "PRIMERA CONCILIACIÓN";
                            ob.id_glosa = objetoConsultado.id_glosa;
                            //inserta en la tabla acta 
                            db.ffmm_actas_glosas.InsertOnSubmit(ob);
                            db.SubmitChanges();
                            objetoConsultado.numero_acta_primera_conciliacion = ob.id_acta;
                            break;
                        case "FechaSegundaConciliacion":
                            objetoConsultado.fecha_acta_segunda_conciliacion = objeto.fecha_acta_segunda_conciliacion;
                            //insertar en la tabla x. 
                            ob.fecha_acta = objeto.fecha_acta_primera_conciliacion;
                            ob.tipo_acta = "SEGUNDA CONCILIACIÓN";
                            ob.id_glosa = objetoConsultado.id_glosa;
                            //inserta en la tabla acta 
                            db.ffmm_actas_glosas.InsertOnSubmit(ob);
                            db.SubmitChanges();
                            objetoConsultado.numero_acta_segunda_conciliacion = ob.id_acta;
                            break;

                        default:
                            break;
                    }
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return objeto.id_glosa;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        #endregion

        #region COVID19

        public void Actualizacasocarguecovid19(cargue_seguimiento_covid19 OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_seguimiento_covid19 objDesa = db.cargue_seguimiento_covid19.Single(p => p.id == OBJ.id);

                    objDesa.No = OBJ.No;
                    objDesa.tipo_documento = OBJ.tipo_documento;
                    objDesa.documento = OBJ.documento;
                    objDesa.nombres = OBJ.nombres;
                    objDesa.apellidos = OBJ.apellidos;
                    objDesa.genero = OBJ.genero;
                    objDesa.viceprecidencia = OBJ.viceprecidencia;
                    objDesa.descripcion_ciudad = OBJ.descripcion_ciudad;
                    objDesa.localidad = OBJ.localidad;
                    objDesa.departamento = OBJ.departamento;
                    objDesa.regional = OBJ.regional;
                    objDesa.fecha_nacimiento = OBJ.fecha_nacimiento;
                    objDesa.edad = OBJ.edad;
                    objDesa.tipo_salud = OBJ.tipo_salud;
                    objDesa.direccion = OBJ.direccion;
                    objDesa.telefono_1 = OBJ.telefono_1;
                    objDesa.telefono_2 = OBJ.telefono_2;
                    objDesa.correo = OBJ.correo;
                    objDesa.incluido_en_sivigila = OBJ.incluido_en_sivigila;
                    objDesa.tipificacion = OBJ.tipificacion;
                    objDesa.fecha_ingreso_pais = OBJ.fecha_ingreso_pais;
                    objDesa.seguimiento = OBJ.seguimiento;
                    objDesa.estado_base = OBJ.estado_base;
                    objDesa.causa_finalizacion_del_seguimiento = OBJ.causa_finalizacion_del_seguimiento;
                    objDesa.observacion_de_caracterizacion = OBJ.observacion_de_caracterizacion;
                    objDesa.fecha_caracterizacion_registro = OBJ.fecha_caracterizacion_registro;
                    objDesa.nombre_quien_reporta = OBJ.nombre_quien_reporta;
                    objDesa.auditor_asignado = OBJ.auditor_asignado;
                    objDesa.usuario_cargue = OBJ.usuario_cargue;
                    //objDesa.fecha_cargue = OBJ.fecha_cargue;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        #endregion

        #region CAMBIOS

        public void ActualizarAuditorConcurrencia(ecop_concurrencia OBJ, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == OBJ.id_concurrencia);


                    ObjConcu.id_auditor = OBJ.id_auditor;
                    ObjConcu.id_editor = OBJ.id_editor;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int ActualizarEnContactCenterConcurrencia(int? idConcurrencia, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    ecop_concurrencia obj = db.ecop_concurrencia.Where(l => l.id_concurrencia == idConcurrencia).FirstOrDefault();
                    obj.estado_contact = 2;
                    obj.reenviado = null;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int ActualizarEnContactCenterCenso(int? idCenso, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    ecop_censo obj = db.ecop_censo.Where(l => l.id_censo == idCenso).FirstOrDefault();
                    obj.estado_contact = 2;
                    obj.reingresado = 0;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void ActualizarAuditorCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo ObjConcu = db.ecop_censo.Single(p => p.id_censo == OBJ.id_censo);


                    ObjConcu.id_censo = OBJ.id_censo;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizarCambiosPacienteCenso(ecop_censo OBJ, String tipocambio, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_censo ObjConcu = db.ecop_censo.Single(p => p.id_censo == OBJ.id_censo);

                    if (tipocambio == "1")
                    {
                        ObjConcu.primer_apellido = OBJ.primer_apellido;
                        ObjConcu.segundo_apellido = OBJ.segundo_apellido;
                        ObjConcu.primer_nombre = OBJ.primer_nombre;
                        ObjConcu.segundo_nombre = OBJ.segundo_nombre;
                        db.SubmitChanges();
                    }
                    else if (tipocambio == "2")
                    {
                        ObjConcu.tipo_identifi_afiliado = OBJ.tipo_identifi_afiliado;
                        ObjConcu.num_identifi_afil = OBJ.num_identifi_afil;
                        db.SubmitChanges();

                    }
                    else if (tipocambio == "3")
                    {
                        ObjConcu.fecha_ingreso = OBJ.fecha_ingreso;
                        db.SubmitChanges();

                    }



                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarCambiosPacienteConcu(ecop_concurrencia OBJ, String tipocambio, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia ObjConcu = db.ecop_concurrencia.Single(p => p.id_concurrencia == OBJ.id_concurrencia);

                    if (tipocambio == "1")
                    {
                        ObjConcu.afi_nom = OBJ.afi_nom;
                        db.SubmitChanges();
                    }
                    else if (tipocambio == "2")
                    {
                        ObjConcu.afi_tipo_doc = OBJ.afi_tipo_doc;
                        ObjConcu.id_afi = OBJ.id_afi;
                        db.SubmitChanges();

                    }
                    else if (tipocambio == "3")
                    {
                        ObjConcu.fecha_ingreso = OBJ.fecha_ingreso;
                        db.SubmitChanges();

                    }



                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;


                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int ActualizarEstanciaEvolucion(ecop_concurrencia_evolucion obj)
        {

            var idEstancia = 0;

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia_evolucion obj2 = db.ecop_concurrencia_evolucion.FirstOrDefault(x => x.id_evolucion == obj.id_evolucion);
                    idEstancia = (int)obj2.id_tipo_habitacion;

                    obj2.id_tipo_habitacion = obj.id_tipo_habitacion;
                    db.SubmitChanges();
                    return idEstancia;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return idEstancia;
            }
        }


        #endregion

        #region SEGUIMIENTO ENTREGABLES

        public void ActualizarEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                /*Actualizamos primero el detalle de la entrega*/
                seguimiento_dtll_entrega segdtll = db.seguimiento_dtll_entrega.Where(l => l.id_seg_dtll_entrega == obj.id_seg_dtll_entrega).FirstOrDefault();
                segdtll.id_estado_entregable = obj.id_estado_entregable;
                if (!string.IsNullOrEmpty(obj.observaciones))
                    segdtll.observaciones = obj.observaciones;
                db.SubmitChanges();

                seguimiento_entregables_periodo segper = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == id_seg_entregable_periodo).FirstOrDefault();
                if (segper != null)
                {
                    segper.id_estado_entregable = obj.id_estado_entregable;
                    db.SubmitChanges();
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void GuardarRespuestaObservaciones(seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                /*Actualizamos primero el detalle de la entrega*/
                seguimiento_dtll_entrega segdtll = db.seguimiento_dtll_entrega.Where(l => l.id_seg_dtll_entrega == obj.id_seg_dtll_entrega).FirstOrDefault();
                segdtll.fecha_obs_respuesta = obj.fecha_obs_respuesta;
                segdtll.observaciones_respuesta = obj.observaciones_respuesta;
                segdtll.ruta_evidencia_rta = obj.ruta_evidencia_rta;
                db.SubmitChanges();

                seguimiento_entregables_periodo pe = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == obj.id_seg_entregable_periodo).FirstOrDefault();
                pe.id_estado_entregable = 5;
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }


        public int ActualizarEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                seguimiento_entregables_periodo pe = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == obj.id_seg_entregable_periodo).FirstOrDefault();
                pe.id_estado_entregable = obj.id_estado_entregable;
                pe.tiene_ampliacion = obj.tiene_ampliacion;
                pe.fecha_limite_ampliacion = obj.fecha_limite_ampliacion;
                pe.observaciones_ampliacion = obj.observaciones_ampliacion;
                if (!string.IsNullOrEmpty(obj.solicitud_ampliacion_realizada_por))
                {
                    pe.solicitud_ampliacion_realizada_por = obj.solicitud_ampliacion_realizada_por;
                }

                /*Si el estado es 7, significa que se aprobo la ampliacion del plazo, pero no se ha realizado la entrega. por eso la fecha de entrega quedaria nula.*/
                if (obj.id_estado_entregable != 7)
                {
                    pe.fecha_entrega = obj.fecha_entrega;
                }
                else
                {
                    pe.fecha_entrega = null;
                }

                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return obj.id_seg_entregable_periodo;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }





        #endregion

        #region CONTACT CENTER
        public void ActualizarImagenCaso(string rutaImagen, string tipo, int contactcenter)
        //public void ActualizarImagenCaso(string rutaImagen, int contactcenter)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            contact_center obj = db.contact_center.Where(l => l.id_contact_center == contactcenter).FirstOrDefault();
            obj.ruta_imagen = rutaImagen;
            obj.tipo_archivo = tipo;
            obj.fecha_entrega_auditor = DateTime.Now;
            db.SubmitChanges();
        }

        public int ActualizarContactCenterPrincipal(int? idContact)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                contact_center obj = db.contact_center.Where(x => x.id_contact_center == idContact).FirstOrDefault();
                obj.estado_solicitud = 2;
                db.SubmitChanges();
                return obj.id_contact_center;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarContactObligatorios(contact_center obj)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                contact_center obj2 = new contact_center();

                obj2 = db.contact_center.Where(x => x.id_contact_center == obj.id_contact_center).FirstOrDefault();

                if (obj2 == null)
                {
                    obj2 = db.contact_center.Where(x => x.id_censo == obj.id_censo).FirstOrDefault();
                }

                if (obj2 == null)
                {
                    obj2 = db.contact_center.Where(x => x.id_concurrencia == obj.id_concurrencia).FirstOrDefault();
                }
                if (obj2 != null)
                {
                    if (obj2.clasificacion_contacto_solicitante == null)
                    {
                        obj2.clasificacion_contacto_solicitante = obj.clasificacion_contacto_solicitante;
                    }

                    if (string.IsNullOrEmpty(obj2.nom_solicitante))
                    {
                        obj2.nom_solicitante = obj.nom_solicitante;
                    }

                    if (obj2.ciudad == null)
                    {
                        obj2.ciudad = obj.ciudad;
                    }

                    if (obj2.regional == null || obj2.regional == 0)
                    {
                        obj2.regional = obj.regional;
                    }

                    if (obj2.id_auditor == null)
                    {
                        obj2.id_auditor = obj.id_auditor;
                    }

                    if (obj2.Ips == null)
                    {
                        obj2.Ips = obj.Ips;
                    }

                    //obj2.num_documento_paciente = obj.num_documento_paciente;
                    //obj2.nom_paciente = obj.nom_paciente;
                    //obj2.id_censo = obj.id_censo;
                    //obj2.id_concurrencia = obj.id_concurrencia;
                    //obj2.num_documento_solicitante = obj.num_documento_solicitante;
                    //obj2.nom_solicitante = obj.nom_solicitante;
                    //obj2.telefono_solicitante = obj.telefono_solicitante;
                    //obj2.otro_telefono_solicitante = obj.otro_telefono_solicitante;
                    //obj2.ips_paciente = obj.ips_paciente;
                    //obj2.regional_paciente = obj.regional_paciente;
                    //obj2.telefono_paciente = obj.telefono_paciente;
                    //obj2.diagnostico = obj.diagnostico;
                    //obj2.regional = obj.regional;

                    obj2.id_censo = obj.id_censo;
                    obj2.id_concurrencia = obj.id_concurrencia;
                    obj2.num_documento_solicitante = obj.num_documento_solicitante;
                    obj2.nom_solicitante = obj.nom_solicitante;
                    obj2.telefono_solicitante = obj.telefono_solicitante;
                    obj2.otro_telefono_solicitante = obj.otro_telefono_solicitante;
                    obj2.correo_electronico_solicitante = obj.correo_electronico_solicitante;

                    obj2.clasificacion_contacto_solicitante = obj.clasificacion_contacto_solicitante;
                    obj2.num_documento_paciente = obj.num_documento_paciente;
                    obj2.nom_paciente = obj.nom_paciente;
                    obj2.tipo_beneficiario_paciente = obj.tipo_beneficiario_paciente;
                    obj2.regional_paciente = obj.regional_paciente;
                    obj2.telefono_paciente = obj.telefono_paciente;
                    obj2.otro_telefono_paciente = obj.otro_telefono_paciente;
                    obj2.correo_electronico_paciente = obj.correo_electronico_paciente;
                    obj2.otro_correo_paciente = obj.otro_correo_paciente;
                    obj2.diagnostico = obj.diagnostico;
                    obj2.diagnostico_txt = obj.diagnostico_txt;
                    obj2.diagnostico2 = obj.diagnostico2;
                    obj2.diagnostico2_txt = obj.diagnostico2_txt;

                    obj2.regional = obj.regional;
                    obj2.ciudad = obj.ciudad;
                    obj2.ips_paciente = obj.ips_paciente;
                    obj2.id_auditor = obj.id_auditor;
                    obj2.otro_auditor = obj.otro_auditor;
                    obj2.agente_solucionador = obj.agente_solucionador;
                    obj2.servicio = obj.servicio;
                    obj2.otro_servicio = obj.otro_servicio;
                    obj2.estado_solicitud = obj.estado_solicitud;
                    obj2.fecha_digita = obj.fecha_digita;
                    obj2.usuario_digita = obj.usuario_digita;
                    obj2.Ips = obj.Ips;
                    obj2.Ips_otro = obj.Ips_otro;
                    obj2.tipo_insercion = obj.tipo_insercion;

                    obj2.otroContacto_nombre_paciente = obj.otroContacto_nombre_paciente;
                    obj2.otroContacto_parentezco_paciente = obj.otroContacto_parentezco_paciente;
                    obj2.otroContacto_telefono = obj.otroContacto_telefono;



                }

                db.SubmitChanges();
                return obj2.id_contact_center;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region
        public void ActualizarGestionFacturadigitalGasto(vw_factura_digital_gasto_total obj, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            ecop_gestion_factura_digital_gasto obj2 = db.ecop_gestion_factura_digital_gasto.Where(l => l.id_factura_digital_gasto == obj.id_factura_digital_gasto).FirstOrDefault();
            obj2.id_ref_tipo_gasto_facturas = obj.id_ref_tipo_gasto_facturas;
            obj2.observacion_gasto = obj.observacion_gasto;
            db.SubmitChanges();
        }
        #endregion


        #region verificacion

        public void ActualizarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_ver_tipoCriterio model = db.ref_ver_tipoCriterio.Where(l => l.id_tipo_criterio == obj.id_tipo_criterio).FirstOrDefault();
                    model.nombre_criterio = obj.nombre_criterio;
                    model.indice = obj.indice;
                    model.peso = obj.peso;
                    model.fecha_digita = obj.fecha_digita;
                    model.usuario_digita = obj.usuario_digita;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void ActualizarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_criterio obj = db.ver_criterio.Where(l => l.id_ver_criterio == criterio.id_ver_criterio).FirstOrDefault();
                    obj.descripcion = criterio.descripcion;
                    obj.puntaje = criterio.puntaje;
                    obj.id_tipo_criterio = criterio.id_tipo_criterio;
                    obj.atributo = criterio.atributo;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void ActualizarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_verificacion_farmaceutico model = db.ref_verificacion_farmaceutico.Where(l => l.id_veriticacion == obj.id_veriticacion).FirstOrDefault();
                    model.descripcion = obj.descripcion;
                    model.peso = obj.peso;
                    model.fecha_digita = obj.fecha_digita;
                    model.usuario_digita = obj.usuario_digita;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public int ActualizarPuntoDispensacionOff(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_puntoDispensacion model = db.ver_puntoDispensacion.Where(l => l.id_cargue_verificacion == id).FirstOrDefault();
                    model.reasignacion = "NO";
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public int ActualizarPuntoDispensacion(ver_puntoDispensacion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_puntoDispensacion model = db.ver_puntoDispensacion.Where(l => l.id_cargue_verificacion == obj.id_cargue_verificacion).FirstOrDefault();
                    model.reasignacion = obj.reasignacion;
                    model.motivo = obj.motivo;
                    model.fecha_nuevavisita = obj.fecha_nuevavisita;
                    model.auditor_nuevo = obj.auditor_nuevo;
                    model.observacion = obj.observacion;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public int ActualizarAuditadoVisitasDispensacion(ver_puntoDispensacion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ver_puntoDispensacion model = db.ver_puntoDispensacion.Where(l => l.id_cargue_verificacion == obj.id_cargue_verificacion).FirstOrDefault();
                    model.nombre_auditado = obj.nombre_auditado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        #endregion


        public Int32 UpdateProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ffmm_glosas obj2 = db.ffmm_glosas.Where(x => x.id_glosa == objeto.id_glosa).FirstOrDefault();

                    obj2.fecha_programacion_visita = objeto.fecha_programacion_visita;
                    obj2.estado = objeto.estado;
                    db.SubmitChanges();
                    return obj2.id_glosa;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarVigenciaHastaLupe(md_Lupe_cargue_base obj)
        {
            md_Lupe_cargue_base obj2 = new md_Lupe_cargue_base();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj2 = db.md_Lupe_cargue_base.Where(x => x.id_lupe_cargue_base == obj.id_lupe_cargue_base).FirstOrDefault();
                    obj2.vigencia_hasta = obj.vigencia_hasta;
                    db.SubmitChanges();
                    return obj2.id_lupe_cargue_base;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int Actualizar_md_Lupe_cargue_base_H(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    rips_homologacion_dtll model = db.rips_homologacion_dtll.Where(l => l.id_rips_homologacion_dtll == obj.id_rips_homologacion_dtll).FirstOrDefault();
                    model.cod_homologacion = obj.cod_homologacion;
                    model.valorH = obj.valorH;
                    model.descripcion_homologacion = obj.descripcion_homologacion;
                    model.obs_homologacion = obj.obs_homologacion;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int Actualizar_md_Lupe_cargue_base_G(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    rips_homologacion_dtll model = db.rips_homologacion_dtll.Where(l => l.id_rips_homologacion_dtll == obj.id_rips_homologacion_dtll).FirstOrDefault();
                    model.tiene_glosa = Convert.ToBoolean(obj.tiene_glosa);
                    model.descripcionGlosa = obj.descripcionGlosa;
                    model.id_tipo_glosa = obj.id_tipo_glosa;


                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int Actualizar_rips_homologacion(rips_homologacion obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    rips_homologacion model = db.rips_homologacion.Where(l => l.id_rips_homologacion == obj.id_rips_homologacion).FirstOrDefault();
                    model.tiene_glosa = obj.tiene_glosa;
                    model.id_motivo_glosa = obj.id_motivo_glosa;
                    model.descripcion_glosa = obj.descripcion_glosa;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstado_Facturas(int idFac, int estadoAntiguo, int estadoNuevo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_facturas_cambioEstado(estadoAntiguo, estadoNuevo, idFac);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return 0;
            }
        }



        public int ActualizarConteo_Facturas_fase1(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 10000;
                db.management_actualizarConteo_prefacturas_fase1(idCargue, usuarioDigita);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }

        public int ActualizarConteo_Facturas_fase2(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 30000;
                db.management_actualizarConteo_prefacturas_fase2(idCargue, usuarioDigita);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }


        public int ActualizarConteo_Facturas_fase2_2(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 30000;
                db.management_actualizarConteo_prefacturas_fase2_2(idCargue, usuarioDigita);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }

        public int ActualizarConteo_Facturas_fase3(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 10000;
                db.management_actualizarConteo_prefacturas_fase3(idCargue, usuarioDigita);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }

        public int ActualizarConteo_FacturasInicial(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 10000;
                db.management_actualizarConteo_prefacturas_inicial(idCargue);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.DescriptionResponse = error;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }


        //public int ActualizarConteo_FacturasUno(int idCargue, string usuarioDigita, int? tipo, ref MessageResponseOBJ MsgRes)
        //{
        //    var posicion = 0;

        //    try
        //    {
        //        ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
        //        db.CommandTimeout = 20000;
        //        db.management_actualizarConteo_prefacturas_uno(idCargue, usuarioDigita, tipo);
        //        posicion = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        var erorr = ex.Message;
        //        posicion = 0;
        //        MsgRes.DescriptionResponse = ex.Message;
        //    }

        //    return posicion;
        //}

        public int ActualizarConteo_FacturasUno(int idCargue, string usuarioDigita, ref MessageResponseOBJ MsgRes)

        {
            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 30000;
                    db.management_actualizarConteo_prefacturas_uno(idCargue, usuarioDigita);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.DescriptionResponse = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }
        public int ActualizarConteo_Facturas(int idCargue, string usuarioDigita, int? tipo, ref MessageResponseOBJ MsgRes)
        {
            var posicion = 0;

            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 20000;
                db.management_actualizarConteo_prefacturas(idCargue, usuarioDigita, tipo);

                posicion = 1;
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                posicion = 0;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return posicion;
        }


        public int ActualizarConteo_Facturas2(int idCargue, string usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 10000;
                db.management_actualizarConteo_prefacturas_2(idCargue, usuario);
                return 1;
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                MsgRes.DescriptionResponse = ex.Message;

                return 0;
            }
        }

        public int ActualizarConteo_Facturas3(int idCargue, string usuarioValida, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 30000;
                db.management_actualizarConteo_prefacturas_3(idCargue, usuarioValida);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                return 1;
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                MsgRes.DescriptionResponse = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;

                return 0;
            }
        }

        public int ActualizarConteo_Facturas4(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 3000;
                db.management_actualizarConteo_prefacturas_4(idCargue);
                return 1;
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                MsgRes.DescriptionResponse = ex.Message;

                return 0;
            }
        }

        public int ActualizarConteo_Facturas5(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.CommandTimeout = 3000;
                db.management_actualizarConteo_prefacturas_5(idCargue);
                return 1;
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                MsgRes.DescriptionResponse = ex.Message;

                return 0;
            }
        }


        public int ActualizarAsignacion_automatica(int idPrestador)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_asignacionAutomatica_actualizarAsignaciones(idPrestador);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return 0;
            }
        }

        public int ActualizarAsignacionAutomatica(ref_cuentas_medicas_analista obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cuentas_medicas_analista model = db.ref_cuentas_medicas_analista.Where(l => l.id_ref_cuentas_medicas_analista == obj.id_ref_cuentas_medicas_analista).FirstOrDefault();
                    model.activo = obj.activo;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public int ActualizarLoteAnalista(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_analista_lote obj2 = db.ref_analista_lote.Where(x => x.id_lote_facturas == obj.id_lote_facturas).FirstOrDefault();
                    obj2.id_analista = obj.id_analista;
                    obj2.fecha_digita = obj.fecha_digita;
                    obj2.usuario_digita = obj.usuario_digita;
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        #region Contratos

        public int ActualizarContrato(contratos_detalle obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    contratos_detalle obj2 = db.contratos_detalle.Where(x => x.id_contrato == obj.id_contrato).FirstOrDefault();
                    obj2.numero_contrato = obj.numero_contrato;
                    obj2.periodo_finalizacion = obj.periodo_finalizacion;
                    obj2.fecha_inicio = obj.fecha_inicio;
                    obj2.fecha_fin = obj.fecha_fin;
                    obj2.estado_contrato = obj.estado_contrato;
                    obj2.nombre_gestor_contratacion = obj.nombre_gestor_contratacion;
                    obj2.nombre_funcionario_autorizado = obj.nombre_funcionario_autorizado;
                    obj2.objeto_contrato = obj.objeto_contrato;
                    obj2.subcategoria_contrato_nombre = obj.subcategoria_contrato_nombre;
                    obj2.categoria_nombre = obj.categoria_nombre;
                    obj2.lugar_ejecucion_contrato = obj.lugar_ejecucion_contrato;
                    obj2.departamento_ejecucion_contrato = obj.departamento_ejecucion_contrato;
                    obj2.subregional_ejecucion_contrato = obj.subregional_ejecucion_contrato;
                    obj2.regional_ejecucion_contrato = obj.regional_ejecucion_contrato;
                    obj2.nombre_administrador_funcionario = obj.nombre_administrador_funcionario;
                    obj2.nombre_interventor = obj.nombre_interventor;
                    obj2.nit_proveedor = obj.nit_proveedor;
                    obj2.documento_SAP = obj.documento_SAP;
                    obj2.nombre_proveedor = obj.nombre_proveedor;
                    obj2.id_contrato = obj.id_contrato;

                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj2.id_contrato;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        #endregion

        #region REEMBOLSO

        public int ActualizarEstadoReembolso(cuentas_reembolsos reem)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cuentas_reembolsos obj = db.cuentas_reembolsos.Where(x => x.id_reembolso == reem.id_reembolso).FirstOrDefault();
                    obj.id_estado = reem.id_estado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int ActualizarDatosReembolso(cuentas_reembolsos reem)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cuentas_reembolsos obj = db.cuentas_reembolsos.Where(x => x.id_reembolso == reem.id_reembolso).FirstOrDefault();
                    obj.id_reembolso = reem.id_reembolso;
                    obj.id_regional = reem.id_regional;
                    obj.id_ciudad = reem.id_ciudad;
                    obj.fecha_recepcion = reem.fecha_recepcion;
                    obj.sad_titular = reem.sad_titular;
                    obj.titular = reem.titular;
                    obj.unis = reem.unis;
                    obj.tipo_reembolso = reem.tipo_reembolso;
                    obj.identificacion_beneficiario = reem.identificacion_beneficiario;
                    obj.beneficiario = reem.beneficiario;
                    obj.num_factura = reem.num_factura;
                    obj.sap_entidad = reem.sap_entidad;
                    obj.prestador = reem.prestador;
                    obj.nit = reem.nit;
                    obj.valor = reem.valor;
                    obj.id_tipo_moneda = reem.id_tipo_moneda;
                    //obj.id_estado = reem.id_estado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        #endregion REEMBOLSO

        #region CAMPAÑAS

        public int DesactivarActivarCampaña(int? idCampana, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    creacion_campana obj = db.creacion_campana.Where(x => x.id_campana == idCampana).FirstOrDefault();
                    obj.estado = estado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }


        public int ActualizarVersionCampaña(creacion_campana cam)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    creacion_campana obj = db.creacion_campana.Where(x => x.id_campana == cam.id_campana).FirstOrDefault();
                    obj.titulo = cam.titulo;
                    obj.descripcion = cam.descripcion;
                    obj.version = cam.version;
                    obj.fecha_digita = DateTime.Now;
                    obj.usuario_digita = cam.usuario_digita;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }


        public int ActualizarVersionCampañaPregunta(int? idPregunta, int? version)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    creacion_campana_detalle obj = db.creacion_campana_detalle.Where(x => x.id_detalle == idPregunta).FirstOrDefault();
                    obj.version = version;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public int ActualizarDatosCampañaPregunta(creacion_campana_detalle cam)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    creacion_campana_detalle obj = db.creacion_campana_detalle.Where(x => x.id_detalle == cam.id_detalle).FirstOrDefault();
                    obj.version = cam.version;
                    obj.pregunta = cam.pregunta;
                    obj.tipo_pregunta = cam.tipo_pregunta;
                    obj.estado = cam.estado;
                    obj.version = cam.version;
                    obj.obligatoria = cam.obligatoria;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        //public int ActualizarDatosCampañaPregunta(creacion_campana_detalle cam)
        //{
        //    var idcampana = cam.id_campana;

        //    try
        //    {
        //        using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
        //        {
        //            // Obtener todos los id_detalle con el id_campana que viene de cam
        //            var idDetallesCampana = db.creacion_campana_detalle
        //                .Where(c => c.id_campana == idcampana)
        //                .Select(c => c.id_detalle)
        //                .ToList();

        //            // Actualizar el estado de los detalles que no se encuentran en cam.id_detalle
        //            var detallesNoEncontrados = db.creacion_campana_detalle
        //                .Where(c => !idDetallesCampana.Contains(c.id_detalle) && c.id_campana == idcampana)
        //                .ToList();

        //            foreach (var detalleNoEncontrado in detallesNoEncontrados)
        //            {
        //                detalleNoEncontrado.estado = 0;
        //            }

        //            // Actualizar los datos en la base de datos
        //            foreach (var idDetalle in idDetallesCampana)
        //            {
        //                var detalle = db.creacion_campana_detalle
        //                    .Where(c => c.id_detalle == idDetalle && c.id_campana == idcampana)
        //                    .FirstOrDefault();

        //                if (detalle != null)
        //                {
        //                    detalle.version = cam.version;
        //                    detalle.pregunta = cam.pregunta;
        //                    detalle.tipo_pregunta = cam.tipo_pregunta;
        //                    detalle.estado = cam.id_detalle == idDetalle ? 1 : 0;
        //                    detalle.obligatoria = cam.obligatoria;
        //                }
        //            }

        //            db.SubmitChanges();
        //            return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var mensaje = ex.Message;
        //        return 0;
        //    }
        //}

        public void ActualizarInactivas(List<creacion_campana_detalle> ActualizarInactivas, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    foreach (var pregunta in ActualizarInactivas)
                    {
                        var obj = db.creacion_campana_detalle.Where(l => l.id_detalle == pregunta.id_detalle && l.id_campana == pregunta.id_campana).FirstOrDefault();
                        obj.estado = 0;
                        db.SubmitChanges();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
        }



        public int ActualizarCamposPreguntas(int? idPregunta)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_campana_actualizarEstadoPreguntas(idPregunta);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }

        #endregion CAMPAÑAS

        #region MORTALIDAD_NATALIDAD
        public int ActualizarRegistroMortalidad(mortalidad_registros reg)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    mortalidad_registros obj = db.mortalidad_registros.Where(x => x.id_mortalidad == reg.id_mortalidad).FirstOrDefault();
                    obj.año = reg.año;



                    obj.trimestre = reg.trimestre;
                    obj.mes = reg.mes;
                    obj.regional = reg.regional;
                    obj.unis = reg.unis;
                    obj.ciudad_smed = reg.ciudad_smed;
                    obj.tipo_documento = reg.tipo_documento;
                    obj.numero_documento = reg.numero_documento;
                    obj.apellidos = reg.apellidos;
                    obj.nombres = reg.nombres;
                    obj.genero = reg.genero;
                    obj.fecha_nacimiento = reg.fecha_nacimiento;
                    obj.edad = reg.edad;
                    obj.tipo_beneficiario = reg.tipo_beneficiario;
                    obj.nro_certificado = reg.nro_certificado;
                    obj.fecha_fallecimiento = reg.fecha_fallecimiento;
                    obj.causa_fallecimiento = reg.causa_fallecimiento;
                    obj.confirmado_descartado = reg.confirmado_descartado;
                    obj.fecha_notificacion = reg.fecha_notificacion;
                    obj.fuente = reg.fuente;
                    obj.version = reg.version;
                    obj.fecha_digita = reg.fecha_digita;
                    obj.usuario_digita = reg.usuario_digita;

                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public int ActualizarRegistroNatalidad(natalidad_registros nat)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    natalidad_registros obj = db.natalidad_registros.Where(x => x.id_natalidad == nat.id_natalidad).FirstOrDefault();
                    obj.id_natalidad = nat.id_natalidad;
                    obj.id_regional = nat.id_regional;
                    obj.identificacion_madre = nat.identificacion_madre;
                    obj.cod_personal = nat.cod_personal;
                    obj.apellidos = nat.apellidos;
                    obj.nombres = nat.nombres;
                    obj.id_mes = nat.id_mes;
                    obj.id_trimestre = nat.id_trimestre;
                    obj.año = nat.año;
                    obj.id_ciudad_smed = nat.id_ciudad_smed;
                    obj.id_localidad = nat.id_localidad;
                    obj.fecha_nacimiento = nat.fecha_nacimiento;
                    obj.id_unis = nat.id_unis;
                    obj.edad_gestacional = nat.edad_gestacional;
                    obj.peso = nat.peso;
                    obj.via_parto = nat.via_parto;
                    obj.talla = nat.talla;
                    obj.sexo = nat.sexo;
                    obj.apgar = nat.apgar;
                    obj.causa_egreso = nat.causa_egreso;
                    obj.control_prenatal = nat.control_prenatal;
                    obj.fecha = nat.fecha;
                    obj.observaciones = nat.observaciones;
                    obj.version = nat.version;
                    obj.fecha_digita = nat.fecha_digita;
                    obj.usuario_digita = nat.usuario_digita;


                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        #endregion

        #region eventos_salud
        public int ActualizarRegistroEventosSalud(eventos_salud_registros even)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    eventos_salud_registros evento = db.eventos_salud_registros.Where(x => x.id_evento == even.id_evento).FirstOrDefault();
                    evento.dependencia_de_salud = even.dependencia_de_salud;
                    evento.anio = even.anio;
                    evento.id_mes = even.id_mes;
                    evento.mes = even.mes;
                    evento.consecutivo = even.consecutivo;
                    evento.fecha_de_reporte = even.fecha_de_reporte;
                    evento.fecha_de_ocurrencia_del_evento = even.fecha_de_ocurrencia_del_evento;
                    evento.localidad_de_servicios_de_salud = even.localidad_de_servicios_de_salud;
                    evento.fuente_del_reporte = even.fuente_del_reporte;
                    evento.reportante_nombre_de_quien_realiza_el_reporte = even.reportante_nombre_de_quien_realiza_el_reporte;
                    evento.nombre_de_municipio_donde_ocurrio_el_evento = even.nombre_de_municipio_donde_ocurrio_el_evento;
                    evento.codigo_municipal_donde_ocurrio_el_evento = even.codigo_municipal_donde_ocurrio_el_evento;
                    evento.reportante_identificacion_de_quien_realiza_el_reporte = even.reportante_identificacion_de_quien_realiza_el_reporte;
                    evento.ambito_de_ocurrencia_del_evento = even.ambito_de_ocurrencia_del_evento;
                    evento.nombre_del_prestador_en_donde_ocurrio_el_evento_adverso = even.nombre_del_prestador_en_donde_ocurrio_el_evento_adverso;
                    evento.nit_del_prestador_en_donde_ocurrio_el_evento_adverso = even.nit_del_prestador_en_donde_ocurrio_el_evento_adverso;
                    evento.numero_de_identificacion_del_prestador_codigo_sap = even.numero_de_identificacion_del_prestador_codigo_sap;
                    evento.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento = even.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento;
                    evento.numero_de_identificacion_del_beneficiario = even.numero_de_identificacion_del_beneficiario;
                    evento.nombre_del_beneficiario = even.nombre_del_beneficiario;
                    evento.edad_del_beneficiario = even.edad_del_beneficiario;
                    evento.descripcion_del_evento = even.descripcion_del_evento;
                    evento.clasificacion_del_evento_de_la_atencion_en_salud = even.clasificacion_del_evento_de_la_atencion_en_salud;
                    evento.categoria_del_evento = even.categoria_del_evento;
                    evento.subcategoria_del_evento = even.subcategoria_del_evento;
                    evento.resultado_negativo_de_la_medicacion = even.resultado_negativo_de_la_medicacion;
                    evento.confirmacion_evento_prevenible_no_prevenible = even.confirmacion_evento_prevenible_no_prevenible;
                    evento.severidad_del_desenlace = even.severidad_del_desenlace;
                    evento.probabilidad_de_repeticion = even.probabilidad_de_repeticion;
                    evento.concepto_auditoria = even.concepto_auditoria;
                    evento.gestion_de_la_gestoria_integral_de_la_calidad = even.gestion_de_la_gestoria_integral_de_la_calidad;
                    evento.plan_de_mejora_al_prestador_si_o_no = even.plan_de_mejora_al_prestador_si_o_no;
                    evento.fecha_radicacion_del_plan_de_mejora = even.fecha_radicacion_del_plan_de_mejora;
                    evento.fecha_programada_para_revision_de_plan_de_mejora = even.fecha_programada_para_revision_de_plan_de_mejora;
                    evento.costo_de_no_calidad = even.costo_de_no_calidad;
                    evento.descripcion_de_costo_de_no_calidad = even.descripcion_de_costo_de_no_calidad;
                    evento.seguimiento = even.seguimiento;
                    evento.novedades = even.novedades;
                    evento.edicion_regional = even.edicion_regional;
                    evento.edicion_nacional = even.edicion_nacional;
                    evento.fecha_digita = even.fecha_digita;
                    evento.usuario_digita = even.usuario_digita;

                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }
        #endregion

        #region FIS PRESTADORES



        public int ActualizarEstadoPrestadoresFIS(int? idPrestador)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisPrestadores_actualizarEstadoSedes(idPrestador);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }

        public int ActualizarDatosPrestador(fis_rips_prestadores pre)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores prestador = db.fis_rips_prestadores.Where(x => x.id_prestador == pre.id_prestador).FirstOrDefault();
                    prestador.nit = pre.nit;
                    prestador.codigo_verfificacion = pre.codigo_verfificacion;
                    prestador.codigo_SAP = pre.codigo_SAP;
                    prestador.codigo_habilitacion = pre.codigo_habilitacion;
                    prestador.codigo_sede = pre.codigo_sede;
                    prestador.razon_social = pre.razon_social;
                    prestador.ciudad_proveedor = pre.ciudad_proveedor;
                    prestador.departamento_proveedor = pre.departamento_proveedor;
                    prestador.regional = pre.regional;
                    prestador.direccion = pre.direccion;
                    prestador.contacto_telefonico = pre.contacto_telefonico;
                    prestador.correo_electronico = pre.correo_electronico;
                    prestador.estado = pre.estado;
                    prestador.tiene_mas_sedes = pre.tiene_mas_sedes;
                    prestador.tipoPrestador = pre.tipoPrestador;
                    prestador.fecha_digita = DateTime.Now;
                    prestador.usuario_digita = pre.usuario_digita;

                    db.SubmitChanges();
                    return prestador.id_prestador;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarDatosContratoPrestador(fis_rips_prestadores_contratos contra)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_contratos contrato = db.fis_rips_prestadores_contratos.Where(x => x.id_contrato == contra.id_contrato).FirstOrDefault();

                    contrato.id_prestador = contra.id_prestador;
                    contrato.num_contrato = contra.num_contrato;
                    contrato.fecha_suscripcion = contra.fecha_suscripcion;
                    contrato.fecha_inicial = contra.fecha_inicial;
                    contrato.fecha_final = contra.fecha_final;
                    contrato.objeto_contrato = contra.objeto_contrato;
                    contrato.id_adm_contrato = contra.id_adm_contrato;
                    contrato.nom_adm_contrato = contra.nom_adm_contrato;
                    contrato.nom_apoyo_transaccional = contra.nom_apoyo_transaccional;
                    contrato.id_interventor = contra.id_interventor;
                    contrato.nom_interventor = contra.nom_interventor;
                    contrato.valor_contrato = contra.valor_contrato;
                    contrato.grupo_compras = contra.grupo_compras;
                    contrato.centro_logistico = contra.centro_logistico;
                    contrato.posicion_contrato = contra.posicion_contrato;
                    contrato.contrato_operativo = contra.contrato_operativo;
                    contrato.manual_tarifario = contra.manual_tarifario;
                    contrato.id_apoyo_transaccional = contra.id_apoyo_transaccional;
                    contrato.fecha_digita = contra.fecha_digita;
                    contrato.usuario_digita = contra.usuario_digita;
                    contrato.estado = contra.estado;

                    db.SubmitChanges();
                    return contrato.id_contrato;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoTigasContrato(int? idContrato)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisPrestadores_contratos_ActualizarTigas(idContrato);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoTarifas(int? idContrato)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisPrestadores_contratos_ActualizarEstadoTarifas(idContrato);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoTarifasDiferenteId(int? idContrato, int? idTarifas)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_prestadores_contratos_tarifas> lista = db.fis_rips_prestadores_contratos_tarifas.Where(x => x.id_contrato == idContrato && x.id_masivo != idTarifas).ToList();

                    foreach (var item in lista)
                    {
                        item.estado = 0;
                    }

                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarCupsFis(fis_rips_cups cups)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cups fis = db.fis_rips_cups.Where(x => x.id_cups == cups.id_cups).FirstOrDefault();

                    fis.codigo_cups = cups.codigo_cups;
                    fis.descripcion = cups.descripcion;
                    fis.manual = cups.manual;
                    fis.estado = cups.estado;
                    fis.fecha_digita = cups.fecha_digita;
                    fis.usuario_digita = cups.usuario_digita;

                    db.SubmitChanges();
                    return fis.id_cups;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoFacturaFis(int? idDetalle, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    db.management_fisFacturas_actualizarEstadoFactura(idDetalle, estado);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }

        public int Levantarglosa(int? idGlosa)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa obj = db.fis_rips_facturas_glosa.Where(x => x.id_glosa == idGlosa).FirstOrDefault();
                    obj.estado = 2;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int MantenerGlosa(int? idGlosa, string observacionMantenida)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa obj = db.fis_rips_facturas_glosa.Where(x => x.id_glosa == idGlosa).FirstOrDefault();
                    obj.estado = 3;
                    obj.observacion_mantenida = observacionMantenida;
                    obj.en_prestadores = 1;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FinalizarGlosa(int? idGlosa)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa obj = db.fis_rips_facturas_glosa.Where(x => x.id_glosa == idGlosa).FirstOrDefault();
                    obj.estado = 5;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int ActualizarEstadoGlosa_prestadores(int? idRegistro, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisActualizarEstadoGlosa_prestadores(idRegistro, estado);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarCuvFacturaId(int? idFactura, string cuv)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisFacturas_actualizarCuvFacturaId(cuv, idFactura);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarGlosaRipsFactura(fis_rips_facturas_glosa obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa obj2 = db.fis_rips_facturas_glosa.FirstOrDefault(x => x.id_glosa == obj.id_glosa);
                    obj2.concepto_general = obj.concepto_general;
                    obj2.concepto_especifico = obj.concepto_especifico;
                    obj2.concepto_aplicacion = obj.concepto_aplicacion;
                    obj2.cantidad = obj.cantidad;
                    obj2.valor_glosado = obj.valor_glosado;
                    obj2.observacion = obj.observacion;
                    db.SubmitChanges();
                    return obj.id_glosa;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarRipsFactura(fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj2 = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == obj.id_registro);
                    obj2.cod_cups = obj.cod_cups;
                    obj2.descripcion_cuvs = obj.descripcion_cuvs;
                    obj2.conteo_cups = obj.conteo_cups;
                    obj2.valor_individual = obj.valor_individual;
                    obj2.valor_cups = obj.valor_cups;
                    obj2.codigo_tiga = obj.codigo_tiga;
                    obj2.descripcion_tiga = obj.descripcion_tiga;
                    //obj2.fecha_prestacion = obj.fecha_prestacion;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarRipsFacturaTiga(fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj2 = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == obj.id_registro);
                    obj2.cod_cups = obj.cod_cups;
                    obj2.descripcion_cuvs = obj.descripcion_cuvs;
                    obj2.conteo_cups = obj.conteo_cups;
                    obj2.valor_individual = obj.valor_individual;
                    obj2.valor_cups = obj.valor_cups;
                    obj2.codigo_tiga = obj.codigo_tiga;
                    obj2.descripcion_tiga = obj.descripcion_tiga;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarTigaInteralFisFactura(int? tiga, int? idFactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_ActualizarTigaDetallado_gestionAuditor(tiga, idFactura);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var errort = ex.Message;
                return 0;
            }
        }

        public int ActualizarTigasRipsFactura(fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj2 = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == obj.id_registro);
                    obj2.codigo_tiga = obj.codigo_tiga;
                    obj2.descripcion_tiga = obj.descripcion_tiga;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarTigaRipsCompletos(fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj2 = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == obj.id_registro);
                    obj2.codigo_tiga = obj.codigo_tiga;
                    obj2.descripcion_tiga = obj.descripcion_tiga;

                    //obj2.tiga_editado_lider = 1;

                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }



        public int ActualizarRipsFacturaFac(fis_rips_facturas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas obj2 = db.fis_rips_facturas.FirstOrDefault(x => x.id_factura == obj.id_factura);
                    obj2.cod_tiga = obj.cod_tiga;
                    obj2.descripcion_tiga = obj.descripcion_tiga;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarCantidadCupsEnGlosa(int? cantidad, int? idRegistro)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisFacturas_actualizarConteoGlosas(cantidad, idRegistro);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarTipoIva(int? idRegistro, string tipo_iva, int? iva)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == idRegistro);
                    obj.tipo_iva = tipo_iva;
                    obj.iva_recalculado = iva;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActulizaGlosaDocContable(fis_rips_facturas_glosa obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa obj2 = db.fis_rips_facturas_glosa.FirstOrDefault(x => x.id_glosa == obj.id_glosa);
                    obj2.documento_contable_sap = obj.documento_contable_sap;
                    obj2.fecha_contable = obj.fecha_contable;
                    obj2.aplicacion_sap = obj.aplicacion_sap;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarFacturasAContabilizadas(int? idLote)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_actualizarFacturasPedidosContabilizados(idLote);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarDiagnosticosCie10Fis(int? idUsuario, int? idFactura, string dx, string dxDes, string relDx, string relDxDes, string dxA,
        string dxDesA, string relDxA, string relDxDesA)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fisFacturas_actualizarDiagnosticos(idUsuario, idFactura, dx, dxDes, relDx, relDxDes, dxA, dxDesA, relDxA, relDxDesA);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int ActualizarVersionFactura(int? idDetalle)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_ActualizarVersionFactura(idDetalle);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarCIE10Registro(fis_rips_cargue_registrosCompletos dato)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos obj = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == dato.id_registro);
                    obj.cie10 = dato.cie10;
                    obj.cie10_relacionado = dato.cie10_relacionado;
                    obj.descripcion_cie10 = dato.descripcion_cie10;
                    obj.descripcion_cie10_relacionado = dato.descripcion_cie10_relacionado;

                    db.SubmitChanges();
                    return dato.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 1;
            }
        }


        public int ActualizarCie10FIS(ref_cie10_fis obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cie10_fis obj2 = db.ref_cie10_fis.FirstOrDefault(x => x.codigo == obj.codigo);
                    obj2.descripcion = obj.descripcion;
                    obj2.usuario_digita = obj.usuario_digita;

                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return 0;
            }
        }


        public int ActualizarCargueMasivoContratos(fis_rips_prestadores_contratos_lote obj, List<fis_rips_prestadores_contratos> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_contratos_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            fis_rips_prestadores_contratos con = db.fis_rips_prestadores_contratos.FirstOrDefault(x => x.id_contrato == item.id_contrato);

                            con.id_lote = obj.id_lote;
                            con.id_prestador = item.id_prestador;
                            con.num_contrato = item.num_contrato;
                            con.fecha_suscripcion = item.fecha_suscripcion;
                            con.fecha_inicial = item.fecha_inicial;
                            con.fecha_final = item.fecha_final;
                            con.objeto_contrato = item.objeto_contrato;
                            con.id_adm_contrato = item.id_adm_contrato;
                            con.nom_adm_contrato = item.nom_adm_contrato;
                            con.id_apoyo_transaccional = item.id_apoyo_transaccional;
                            con.nom_apoyo_transaccional = item.nom_apoyo_transaccional;
                            con.id_interventor = item.id_interventor;
                            con.nom_interventor = item.nom_interventor;
                            con.valor_contrato = item.valor_contrato;
                            con.manual_tarifario = item.manual_tarifario;
                            con.neogociacion = item.neogociacion;
                            con.grupo_compras = item.grupo_compras;
                            con.centro_logistico = item.centro_logistico;
                            con.posicion_contrato = item.posicion_contrato;
                            con.contrato_operativo = item.contrato_operativo;
                            con.estado = 1;
                            con.FI = item.FI;
                            con.fecha_digita = item.fecha_digita;
                            con.usuario_digita = item.usuario_digita;

                        }

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return obj.id_lote;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int ActualizarContratoGestionFisFactura(int? idFac, fis_rips_prestadores_contratos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas fac = db.fis_rips_facturas.FirstOrDefault(x => x.id_factura == idFac && x.tipo_ingreso == 1);
                    fac.idContrato = obj.id_contrato;
                    fac.centro_logistico = obj.centro_logistico;
                    fac.numero_contrato = obj.num_contrato;
                    fac.grupo_compras = obj.grupo_compras;
                    fac.posicion_contrato = obj.posicion_contrato;
                    fac.numero_contrato_operativo = obj.contrato_operativo;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarNovedadFactura(factura_novedades obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    factura_novedades fac = db.factura_novedades.FirstOrDefault(x => x.id_novedad == obj.id_novedad);
                    fac.novedad = obj.novedad;
                    fac.observacion = obj.observacion;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarEstadoContrato(int? idContrato, int? estado)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_contratos con = db.fis_rips_prestadores_contratos.FirstOrDefault(x => x.id_contrato == idContrato);
                    con.estado = estado;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarContabilizadoPedidoFactura(int? idFactura, string documentoConta, DateTime? fechaConta, string numPedido, DateTime? fechaPedido)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_actualizarContabilizados_nroPedidoIdFactura(idFactura, documentoConta, fechaConta, numPedido, fechaPedido);
                    return 1;
                }
            }catch(Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion FIS PRESTADORES

        #region ALERTAS EPIDEMIOLOGICAS

        public int CerrarAlertaEpidemiologica(int? idRegistro, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    alerta_epidemiologica_gestion al = db.alerta_epidemiologica_gestion.FirstOrDefault(x => x.id_registro == idRegistro);
                    al.cerrada = 1;
                    al.fecha_cierre = DateTime.Now;
                    al.usuario_cierre = usuario;
                    db.SubmitChanges();
                    return al.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ActualizarGestionAnalisisAE(alerta_epidemiologica_gestion_gestionAnalisis obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    alerta_epidemiologica_gestion_gestionAnalisis obj2 = db.alerta_epidemiologica_gestion_gestionAnalisis.FirstOrDefault(x => x.id_gestionAnalisis == obj.id_gestionAnalisis);
                    obj2.id_gestion = obj.id_gestion;
                    obj2.fecha_analisis = obj.fecha_analisis;
                    obj2.nombres_beneficiario = obj.nombres_beneficiario;
                    obj2.tipo_beneficiario = obj.tipo_beneficiario;
                    obj2.identificacion_beneficiario = obj.identificacion_beneficiario;
                    obj2.fecha_inicio_servicio = obj.fecha_inicio_servicio;
                    obj2.edad_momento_evento = obj.edad_momento_evento;
                    obj2.tipo_evento = obj.tipo_evento;
                    obj2.fecha_ocurrencia_evento = obj.fecha_ocurrencia_evento;
                    obj2.fecha_fin_evento = obj.fecha_fin_evento;
                    obj2.fuente_reporte = obj.fuente_reporte;
                    obj2.nombre_reportante = obj.nombre_reportante;
                    obj2.nombre_prestador_ocurre_evento = obj.nombre_prestador_ocurre_evento;
                    obj2.nit_prestador = obj.nit_prestador;
                    obj2.profesionales_entidadesresponsables_nivel1 = obj.profesionales_entidadesresponsables_nivel1;
                    obj2.diagnosticos = obj.diagnosticos;
                    obj2.objetivo_auditoria = obj.objetivo_auditoria;
                    obj2.descripcion_evento = obj.descripcion_evento;
                    obj2.ambito_sucedio_evento = obj.ambito_sucedio_evento;
                    obj2.analisis_critico_caso = obj.analisis_critico_caso;
                    obj2.concepto_resolutividad_primer_nivel = obj.concepto_resolutividad_primer_nivel;
                    obj2.aplicacion_guias_terapeuticas = obj.aplicacion_guias_terapeuticas;
                    obj2.periodicidad_controles_medicos = obj.periodicidad_controles_medicos;
                    obj2.aplicacion_pruebas_diagnosticos_monitoreo = obj.aplicacion_pruebas_diagnosticos_monitoreo;
                    obj2.informacion_paciente_cuidadores_plan_terapeutico = obj.informacion_paciente_cuidadores_plan_terapeutico;
                    obj2.descripcion_eventuales_Causasmuerte_relacionadas = obj.descripcion_eventuales_Causasmuerte_relacionadas;
                    obj2.conclusiones_analisis_caso = obj.conclusiones_analisis_caso;
                    obj2.concepto_auditoria_evento_prevenible = obj.concepto_auditoria_evento_prevenible;
                    obj2.propuesta_acciones_mejora = obj.propuesta_acciones_mejora;
                    obj2.estado = obj.estado;
                    obj2.fecha_digita = obj.fecha_digita;
                    obj2.usuario_digita = obj.usuario_digita;

                    db.SubmitChanges();
                    return obj2.id_gestionAnalisis;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion ALERTAS EPIDEMIOLOGICAS
    }
}
