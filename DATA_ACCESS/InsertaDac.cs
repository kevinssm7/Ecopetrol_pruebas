using DATA_ACCESS.Exten;
using ANALITICA_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DATA_ACCESS
{
    public class InsertaDac
    {
        #region PROPIEDADES
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

        internal Repository DaccInsetfull
        {
            get
            {
                if (_DaccInsetfull == null)
                {
                    return _DaccInsetfull = new Repository(new ECOPETROL_DataContexDataContext());
                }
                else
                {
                    return _DaccInsetfull;
                }

            }

            set
            {
                _DaccInsetfull = value;
            }
        }
        private Repository _DaccInsetfull;

        internal Repository DaccInsertfullDos
        {
            get
            {
                if (_DaccInsertfullDos == null)
                {
                    return _DaccInsertfullDos = new Repository(new ANALITICA_DataContexDataContext());
                }
                else
                {
                    return _DaccInsertfullDos;
                }

            }

            set
            {
                _DaccInsertfullDos = value;
            }
        }
        private Repository _DaccInsertfullDos;

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PrestadoresConnectionString"].ConnectionString;

        ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

        #endregion

        #region METODOS

        #region LOGIN

        public Int32 CrearUsuairo(sis_usuario usuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.sis_usuario.InsertOnSubmit(usuario);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return usuario.id_usuario;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public Int32 InsertarLogGestionUusario(log_gestion_usuarios log, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    log_gestion_usuarios newobj = log;
                    log.id_log_gestion_usuarios = 0;
                    db.log_gestion_usuarios.InsertOnSubmit(newobj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return log.id_log_gestion_usuarios;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 10 de abril de 2023
        /// Metodo para insertar en base de datos los datos de inicio de sesion de un usuario.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarLogInicioSesion(Log_InicioSession obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Log_InicioSession.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Datos de ingreso guardados correctamente";

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de insertar los datos";
            }
        }

        #endregion

        #region CENSO

        public Int32 InsertarCenso(ecop_censo OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_censo.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_censo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarLogCensoReingreso(log_censo_reingresos OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_censo_reingresos.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_log;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public int InsertarCargueAHCenso(cargue_censo_ah_lote obj, List<cargue_censo_ah_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_censo_ah_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarDetalleRegistroAH(cargue_censo_ah_registros_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_censo_ah_registros_detalle.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarMasivoArchivosAlertasEpidemiologicas(List<alerta_epidemiologica_gestion_archivos> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(detalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarGestionRequiereAnlisis(alerta_epidemiologica_gestion_gestionAnalisis obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alerta_epidemiologica_gestion_gestionAnalisis.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_gestionAnalisis;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertrMasivogestionesDemorasAE(List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(detalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        #endregion

        #region EVOLUCION
        public void InsertaConcurrenciaEvolucion(ecop_concurrencia_evolucion Evolucion, List<ecop_concurrencia_evolucion_procedimientos> lista, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_evolucion.InsertOnSubmit(Evolucion);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    var id_evolucion = Evolucion.id_evolucion;

                    if (id_evolucion != 0 && lista.Count() > 0)
                    {
                        foreach (ecop_concurrencia_evolucion_procedimientos item in lista)
                        {
                            ecop_concurrencia_evolucion_procedimientos otroproducto = item;
                            otroproducto.id_evolucion = id_evolucion;
                            db.ecop_concurrencia_evolucion_procedimientos.InsertOnSubmit(otroproducto);
                            db.SubmitChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int InsertarHospitalizacionPrevenible(ecop_hospitalizacion_prevenible obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_hospitalizacion_prevenible.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_HE;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public void InsertaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Concurrencia_Diagnostico_Definitivo_id, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_evolucion_diag_def.InsertOnSubmit(Concurrencia_Diagnostico_Definitivo_id);
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

        public void InsertaGlosa(ecop_concurrencia_glosa ObjGlosa, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_glosa.InsertOnSubmit(ObjGlosa);
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

        public void InsertaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdv, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_eventos_adversos.InsertOnSubmit(ObjEventoAdv);
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

        public void InsertaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituacionCalid, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_situaciones_de_calidad.InsertOnSubmit(ObjSituacionCalid);
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

        public void InsertaProcedimientoQuirugicoCancelado(ecop_concurrencia_procedimientos_quirurgicos_cancelados ProcedimientoQuirCance, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_procedimientos_quirurgicos_cancelados.InsertOnSubmit(ProcedimientoQuirCance);
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

        public int InsertaEgreso(egreso_auditoria_Hospitalaria Egreso, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.egreso_auditoria_Hospitalaria.InsertOnSubmit(Egreso);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return (int)Egreso.id_egreso_auditoria_Hospitalaria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void InsertarNatalidad(natalidad_sin_concurrencia Natalidad, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.natalidad_sin_concurrencia.InsertOnSubmit(Natalidad);
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

        public void InsertarMortalidad(mortalidad_sin_concurrencia Mortalidad, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.mortalidad_sin_concurrencia.InsertOnSubmit(Mortalidad);
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

        public void InsertarAlertasConcurrencia(alertas_generadas_concurrencia Alertas, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alertas_generadas_concurrencia.InsertOnSubmit(Alertas);
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

        public void InsertarEncuestaConcurrencia(ecop_concurrencia_encuesta Encuesta, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_encuesta.InsertOnSubmit(Encuesta);
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

        public void InsertarConcurrenciaAhorro(ecop_concurrencia_ahorro Ahorro, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_ahorro.InsertOnSubmit(Ahorro);
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

        public void InsertarConcurrenciaCohorte(ecop_concurrencia_cohorte Cohorte, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_cohorte.InsertOnSubmit(Cohorte);
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


        public void InsertarEgresoGestantes(egreso_gestantes Gestantes, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.egreso_gestantes.InsertOnSubmit(Gestantes);
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


        public void insertarcategorizacion(categorizacion_egreso_hospitalario obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.categorizacion_egreso_hospitalario.InsertOnSubmit(obj);
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


        public int InsertarLogCambioEstanciaEvolucion(log_ecop_concurrencia_evolucion_habitacion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_ecop_concurrencia_evolucion_habitacion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }catch(Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region FACTURAS

        public Int32 InsertarDevolucionFacturas(factura_devolucion OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_devolucion.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_devolucion_factura;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarDevolucionFacturasDetalle(factura_devolucion_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_devolucion_detalle.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_devolucion_factura_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarDevolucionGestionadas(factura_devolucion_gestionadas OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_devolucion_gestionadas.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_factura_devolucion_gestionadas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarFacturaSinCenso(factura_sin_censo OBJ, ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_sin_censo.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_factura_sin_censo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public Int32 InsertarHallazgos(hallazgo_RIPS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.hallazgo_RIPS.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_hallazgo_RIPS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarHallazgosDetalle(hallazgo_RIPS_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.hallazgo_RIPS_detalle.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_hallazgo_RIPS_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarCostoEvitado(factura_sin_censo_cos_evitado Obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_sin_censo_cos_evitado.InsertOnSubmit(Obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Obj.id_factura_sin_censo_cos_evitado;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarDiagnosticoCuentas(factura_sin_censo_diagnosticos Obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_sin_censo_diagnosticos.InsertOnSubmit(Obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Obj.id_factura_sin_censo_diagnosticos;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarFacturacionContabilizado(List<ecop_gestion_factura_digital_contabilizados> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {

                    DaccInsetfull.BulkInsertEntities(OBJDetalle);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public static void UpdateData<T>(List<T> list, string TableName)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PrestadoresConnectionString"].ConnectionString;
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            DataTable dt = new DataTable("ecop_gestion_factura_digital_contabilizados");
            dt = ConvertToDataTable(list);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();

                        //Creating temp table on database
                        command.CommandText = "CREATE TABLE #TmpTable(...)";
                        command.ExecuteNonQuery();

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 660;
                            bulkcopy.DestinationTableName = "#TmpTable";
                            bulkcopy.WriteToServer(dt);
                            bulkcopy.Close();
                        }

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 300;
                        command.CommandText = "UPDATE T SET ... FROM " + TableName + " T INNER JOIN #TmpTable Temp ON ...; DROP TABLE #TmpTable;";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        // Handle exception properly
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public Int32 InsertarControlCambios(ecop_gestion_factura_digital_control_cambios OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_factura_digital_control_cambios.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_gestion_factura_control;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarloteContabilizado(ecop_gestion_factura_digital_contabilizados_lote OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_factura_digital_contabilizados_lote.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_lote_contabilizado;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarGestionAnalista(ref_cuentas_medicas_analista OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_cuentas_medicas_analista.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ref_cuentas_medicas_analista;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarGestionAuditor(ref_cuentas_medicas_auditores OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_cuentas_medicas_auditores.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ref_cuentas_medicas_auditores;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 17-nov-2022
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarLogBusquedaTableros(log_busquedas_tableros_facturas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_busquedas_tableros_facturas.InsertOnSubmit(obj);
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

        #endregion

        #region PQRS

        public Int32 InsertarPQRS(ecop_PQRS OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_PQRS.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_PQRS;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPQRSAuditor(ecop_PQRS_Auditor OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_PQRS_Auditor.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_PQRS_auditor;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPQRSProyeccion(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalPQRS2.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_pqr;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarArchivoPQRRespuestaProyectada(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalPQRS2.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_pqr;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 PqrInsertarArchivoRepositorioCierre(GestionDocumentalPQRS2 OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalPQRS2.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_pqr;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int InsertarArchivoReaperturaPQR(GestionDocumentalPQRS2 OBJ)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalPQRS2.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    return OBJ.id_ecop_pqr;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }




        public Int32 InsertarPQRSEnrevision(ecop_PQRS_enrevision OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_PQRS_enrevision.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_PQRA_enrevision;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }



        public Int32 InsertarPQRSEliminar(Log_eliminacion_pqrs OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Log_eliminacion_pqrs.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_log_pqrs;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int insertarConteoAnalistaPQR(int idAnalista, int idUsuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_pqrsActualizarConteoAnalista(idAnalista, idUsuario);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogReaperturaPqrs(log_pqrs_reaperturas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_pqrs_reaperturas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogCierrePqrsSolucionador(log_pqrs_cerradasSolucionador obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_pqrs_cerradasSolucionador.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int CargueMedicamentosRegulados(ecop_pqrs_registroMasivo obj, List<ecop_PQRS> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_pqrs_registroMasivo.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_masivo;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_masivo = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int GuardarLogEliminarArchivoPqr(log_ecop_pqrs_eliminarArchivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_ecop_pqrs_eliminarArchivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_registro;
                    return idBase;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int PqrGuardarObservaciopnAuditor(ecop_pqrs_observacionesAuditor obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_pqrs_observacionesAuditor.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_obs;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int CargueMasivoQuienLlamoPqrs(List<ecop_pqrs_a_quien_llamo> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(detalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int CargueMasivoAuditores(List<ecop_pqrs_auditores> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(detalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int CargueMasivoPrestadores(List<ecop_pqrs_prestadores> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(detalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int insertarDatosCorreos(ecop_pqrs_envioCorreos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_pqrs_envioCorreos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_correo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }



        #endregion

        #region CUENTAS MEDICAS
        /// <summary>
        /// Metodo que inserta un RIPS en la tabla global
        /// </summary>
        /// <param name="Objrips"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        /// 
        public int InsertarLogEliminacionRips(log_rips_eliminarCargue log)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_rips_eliminarCargue.InsertOnSubmit(log);
                    db.SubmitChanges();
                    return log.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }



        public Int32 InsertarRips(RIPS Objrips, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<RIPS> anteriores = db.RIPS.Where(l => l.id_regional == Objrips.id_regional && l.activo == true).ToList();
                    if (anteriores.Count > 0)
                    {
                        foreach (var item in anteriores)
                        {
                            db.ManagmentdeleteRipsData(item.id_rips);
                        }
                    }
                    db.RIPS.InsertOnSubmit(Objrips);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Objrips.id_rips;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Metodo que inserta un RIPS de tipo AC en base de datos
        /// </summary>
        /// <param name="ObjripsAC"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAC(List<RIPS_AC> ObjripsAC, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {

                    DaccInsetfull.BulkInsertEntities(ObjripsAC);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        /// <summary>
        /// Insertar en rips ad
        /// </summary>
        /// <param name="ObjripsAD"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAD(List<RIPS_AD> ObjripsAD, ref MessageResponseOBJ MsgRes)
        {

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {

                try
                {
                    //foreach (RIPS_AD item in ObjripsAD)
                    //{
                    //    db.RIPS_AD.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsAD);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }

            }

        }

        /// <summary>
        /// insertar en rips af
        /// </summary>
        /// <param name="ObjripsAF"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAF(List<RIPS_AF> ObjripsAF, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(ObjripsAF);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }

        }

        /// <summary>
        /// insertar en rips ah
        /// </summary>
        /// <param name="ObjripsAH"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAH(List<RIPS_AH> ObjripsAH, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try

                {
                    DaccInsetfull.BulkInsertEntities(ObjripsAH);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {


                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        /// <summary>
        /// insertar en rips am
        /// </summary>
        /// <param name="ObjripsAM"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAM(List<RIPS_AM> ObjripsAM, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_AM item in ObjripsAM)
                    //{
                    //    db.RIPS_AM.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsAM);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        /// <summary>
        /// insertar en rips an
        /// </summary>
        /// <param name="ObjripsAN"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarRipsAN(List<RIPS_AN> ObjripsAN, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_AN item in ObjripsAN)
                    //{
                    //    db.RIPS_AN.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsAN);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarRipsAP(List<RIPS_AP> ObjripsAP, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_AP item in ObjripsAP)
                    //{
                    //    db.RIPS_AP.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}

                    DaccInsetfull.BulkInsertEntities(ObjripsAP);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;

                }
            }
        }

        public Int32 InsertarRipsAT(List<RIPS_AT> ObjripsAT, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_AT item in ObjripsAT)
                    //{
                    //    db.RIPS_AT.InsertAllOnSubmit<RIPS_AT>(ObjripsAT);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsAT);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarRipsAU(List<RIPS_AU> ObjripsAU, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_AU item in ObjripsAU)
                    //{
                    //    db.RIPS_AU.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsAU);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarRipsCT(List<RIPS_CT> ObjripsCT, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    //foreach (RIPS_CT item in ObjripsCT)
                    //{
                    //    db.RIPS_CT.InsertOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    DaccInsetfull.BulkInsertEntities(ObjripsCT);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarRipsUS(List<RIPS_US> ObjripsUS, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(ObjripsUS);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        #endregion

        #region ProcesosInternos

        public void InsertarCronogramaVisitas(cronograma_visitas objcronograma, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                cronograma_visitas cronogram = new cronograma_visitas();
                cronogram = objcronograma;
                db.cronograma_visitas.InsertOnSubmit(cronogram);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

        }

        public bool InsertarItemCapitulo(item_capitulo itemcapitulo)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.item_capitulo.InsertOnSubmit(itemcapitulo);
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return false;
            }

        }

        public bool InsertaCapitulo(capitulos capitulo)
        {

            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.capitulos.InsertOnSubmit(capitulo);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InsertarPrestador(calidad_prestadores Obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.calidad_prestadores.InsertOnSubmit(Obj);
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

        public void InsertarVisita(cronograma_visitas ObjCronocrama, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {

                db.cronograma_visitas.InsertOnSubmit(ObjCronocrama);
                db.SubmitChanges();
            }
        }

        public void insertaRegionalPrestadores(Int32 idregional, List<int> prestadores)
        {

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                List<indicador_regional> lista = db.indicador_regional.Where(l => l.id_regional == idregional).ToList();
                if (lista.Count() > 0)
                {
                    foreach (var item in lista)
                    {
                        var encontrado = prestadores.Where(l => l == item.id_indicador).FirstOrDefault();
                        if (encontrado == 0)
                        {
                            var capitulosindicador = db.capitulo_indicador.Where(l => l.regional_id == idregional && l.indicador_id == item.id_indicador).ToList();
                            if (capitulosindicador.Count() > 0)
                            {
                                foreach (var capitulo in capitulosindicador)
                                {
                                    var itemscapitulos = db.item_capitulo.Where(l => l.regional_id == idregional && l.indicador_id == item.id_indicador && l.capitulo_id == capitulo.capitulo_id).ToList();
                                    if (itemscapitulos.Count() > 0)
                                    {
                                        db.item_capitulo.DeleteAllOnSubmit(itemscapitulos);
                                        db.SubmitChanges();
                                    }
                                }

                                db.capitulo_indicador.DeleteAllOnSubmit(capitulosindicador);
                                db.SubmitChanges();
                            }

                            db.indicador_regional.DeleteOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                }

                lista = db.indicador_regional.Where(l => l.id_regional == idregional).ToList();

                foreach (int item in prestadores)
                {
                    var encontrado = lista.Where(l => l.id_indicador == item).FirstOrDefault();
                    if (encontrado == null)
                    {
                        indicador_regional ir = new indicador_regional();
                        ir.id_indicador = item;
                        ir.id_regional = idregional;
                        db.indicador_regional.InsertOnSubmit(ir);
                        db.SubmitChanges();
                    }
                }
            }
        }

        public void InsertarCapitulosPrestador(Int32 idregional, Int32 idindicador, List<int> capitulos)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                List<capitulo_indicador> lista = db.capitulo_indicador.Where(l => l.regional_id == idregional && l.indicador_id == idindicador).ToList();
                if (lista.Count() > 0)
                {
                    foreach (var item in lista)
                    {
                        var encontrado = capitulos.Where(l => l == item.capitulo_id).FirstOrDefault();
                        if (encontrado == 0)
                        {
                            var items = db.item_capitulo.Where(l => l.indicador_id == idindicador && l.capitulo_id == item.capitulo_id).ToList();
                            if (items.Count() > 0)
                            {
                                //db.item_capitulo.DeleteAllOnSubmit(items);
                                //db.SubmitChanges();
                            }

                            db.capitulo_indicador.DeleteOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                }

                lista = db.capitulo_indicador.Where(l => l.regional_id == idregional && l.indicador_id == idindicador).ToList();

                foreach (int item in capitulos)
                {
                    var encontrado = lista.Where(l => l.capitulo_id == item).FirstOrDefault();
                    if (encontrado == null)
                    {
                        capitulo_indicador ci = new capitulo_indicador();
                        ci.indicador_id = idindicador;
                        ci.regional_id = idregional;
                        ci.capitulo_id = item;
                        ci.peso_general_capitulo = 0;
                        db.capitulo_indicador.InsertOnSubmit(ci);
                        db.SubmitChanges();
                    }
                }
            }
        }

        public Int32 InsertarCargueRanking(calidad_cargue_ranking ranking)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.calidad_cargue_ranking.InsertOnSubmit(ranking);
                    db.SubmitChanges();
                    Int32 id = ranking.id_cargue_ranking;

                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void InsertarDetalleCargueRanking(List<calidad_detalle_cargue_ranking> detalleranking)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                DaccInsetfull.BulkInsertEntities(detalleranking);
            }
        }

        public void insertardetallevisita(int id_cronograma, int id_regional, int id_indicador, List<item_capitulo> listadoitems, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                List<cronograma_visita_detalle> List = new List<cronograma_visita_detalle>();
                foreach (var item in listadoitems)
                {
                    item_capitulo deb = db.item_capitulo.Where(l => l.id_item == item.id_item).FirstOrDefault();
                    cronograma_visita_detalle det = new cronograma_visita_detalle();
                    det.id_cronograma_visita = id_cronograma;
                    det.id_item = item.id_item;
                    det.valor_digitado = item.Valor_digitado.Value;
                    det.aplica = item.Aplica;
                    det.id_capitulo = item.capitulo_id;
                    det.cap_peso_individual = deb.Peso_individual;
                    det.cap_condicion_meta = deb.condicion_meta;
                    det.cap_valor_meta = deb.valor_meta;
                    det.condicion_especial = deb.condicion_especial;
                    List.Add(det);
                }

                DaccInsetfull.BulkInsertEntities(List);

                //Actualiza nuevamente los valores de la tabla principal en ceros
                db.ManagementActualizaIndicadores();

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }

        public void insertarcalificacionesvisita(int idcronograma, string[] calificaciones, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                List<cronograma_visitas_calificaciones> List = new List<cronograma_visitas_calificaciones>();
                for (int i = 0; i < calificaciones.Length; i++)
                {

                    cronograma_visitas_calificaciones cal = new cronograma_visitas_calificaciones();
                    cal.cronograma_visita_id = idcronograma;
                    cal.capitulo_id = Convert.ToInt32(calificaciones[i]);
                    i += 1;
                    cal.calificacion = Convert.ToDecimal(calificaciones[i]);
                    List.Add(cal);
                }
                DaccInsetfull.BulkInsertEntities(List);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }

        }

        public int insertardetallevisitaindicador(List<capitulo_indicador> capitulos, int idcronograma, int idprestador, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                List<cronograma_visita_detalle_indicador> list = new List<cronograma_visita_detalle_indicador>();
                foreach (capitulo_indicador item in capitulos)
                {
                    cronograma_visita_detalle_indicador obj = new cronograma_visita_detalle_indicador();
                    obj.id_cronograma_visitas = idcronograma;
                    obj.id_prestador = idprestador;
                    obj.id_regional = item.regional_id.Value;
                    obj.id_indicador = item.indicador_id.Value;
                    obj.id_capitulo = item.capitulo_id.Value;
                    obj.peso_general_capitulo = item.peso_general_capitulo.Value;
                    list.Add(obj);
                }

                DaccInsetfull.BulkInsertEntities(list);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return 0;
            }
        }

        public void GuardarActaVisitas(cronograma_visita_documento obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cronograma_visita_documento.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
            }
        }


        public void GuardarInformeOperativo(cronograma_visita_informeOperativo obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cronograma_visita_informeOperativo.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
            }
        }

        public int GuardarLogEliminarActaVisitas(int? idVisita, int? idArchivo, string nombreArchivo, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_cronogramaVisitas_logEliminarActa(idVisita, idArchivo, nombreArchivo, usuario);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void InsertarNovedadVisita(cronograma_visita_novedades obj, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.cronograma_visita_novedades.InsertOnSubmit(obj);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarMasivamenteReportesEvaluacionVisitas(List<cronograma_visitas_reportesDoc_evaluacion_calidad> obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsetfull.BulkInsertEntities(obj);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
            }
        }
        #endregion

        #region GESTION DOCUMENTAL

        public Int32 InsertarGestionDoc(GestionDocumentalMedicamentos ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.GestionDocumentalMedicamentos.InsertOnSubmit(ObjobjGD);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarGestionDocMedCalidad(GestionDocumentalMedicamentosCad ObjobjGD, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.GestionDocumentalMedicamentosCad.InsertOnSubmit(ObjobjGD);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public void InsertarGestionDocPQRS(GestionDocumentalPQRS obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalPQRS.InsertOnSubmit(obj);
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


        public int InsertarLogReinicioConteoPqrs(log_pqrs_reinicioConteo_asignacionAnalistas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_pqrs_reinicioConteo_asignacionAnalistas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void InsertarGestionDocVisitasCalidad(GestionDocumentalVisitasCalidad Obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionDocumentalVisitasCalidad.InsertOnSubmit(Obj);
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

        public void InsertarLogActividad(Log_GestionDocumental Log)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.Log_GestionDocumental.InsertOnSubmit(Log);
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }

        public void InsertarComunicaciones(md_comunicaciones OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_comunicaciones.InsertOnSubmit(OBJ);
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

        #region medicamentos

        public Int32 InsertarGlosaMD(md_glosa OBJGlosa, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.md_glosa.InsertOnSubmit(OBJGlosa);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarGlosaDetalleMD(md_glosa_detalle OBJGlosaDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.md_glosa_detalle.InsertOnSubmit(OBJGlosaDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarIndicador(md_indicadores OBJIndicadores, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_indicadores.InsertOnSubmit(OBJIndicadores);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJIndicadores.id_indicadores_medicamentos;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarIndicadorDetalle(md_indicadores_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_indicadores_detalle.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_indicador_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarIndicadorGestion(md_indicadores_gestion OBJGestion, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_indicadores_gestion.InsertOnSubmit(OBJGestion);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJGestion.id_md_gestion;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarObligaciones(md_obligaciones_contractuales OBJObligacionesContractuales, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_obligaciones_contractuales.InsertOnSubmit(OBJObligacionesContractuales);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJObligacionesContractuales.id_obligaciones_contractuales;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarObligacionesDetalle(md_obligaciones_contractuales_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_obligaciones_contractuales_detalle.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_obligaciones_contractuales_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }


        public Int32 InsertarHerramientaTecnologica(md_herramienta_tec OBJHerramienta, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_herramienta_tec.InsertOnSubmit(OBJHerramienta);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJHerramienta.id_herramienta_tec_med;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarDetallet1(List<md_herramienta_tec_detalle_t1> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try

                {
                    DaccInsetfull.BulkInsertEntities(OBJDetalle);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {


                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarDetallet2(List<md_herramienta_tec_detalle_t2> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try

            {
                DaccInsetfull.BulkInsertEntities(OBJDetalle);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 0;
            }
            catch (Exception ex)
            {


                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 1;
            }

        }


        public Int32 InsertarDetallet3(List<md_herramienta_tec_detalle_t3> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try

            {
                DaccInsetfull.BulkInsertEntities(OBJDetalle);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 0;
            }
            catch (Exception ex)
            {


                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 1;
            }

        }


        public Int32 InsertarDetallet4(List<md_herramienta_tec_detalle_t4> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try

            {
                DaccInsetfull.BulkInsertEntities(OBJDetalle);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return 0;
            }
            catch (Exception ex)
            {


                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 1;
            }

        }


        public void InsertarCronoVisitas(md_crono_visita OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_crono_visita.InsertOnSubmit(OBJ);
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

        public Int32 InsertarInterventoriaGeneral(md_interventoria_general OBJInterventoriaGeneral, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_interventoria_general.InsertOnSubmit(OBJInterventoriaGeneral);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJInterventoriaGeneral.id_md_interventoria_general;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarGeneral1Detalle(md_interventoria_general_detalle1 OBJDetallleG1, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_interventoria_general_detalle1.InsertOnSubmit(OBJDetallleG1);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetallleG1.id_md_interventoria_general_detalle1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarGeneral2Detalle(md_interventoria_general_detalle2 OBJDetallleG2, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_interventoria_general_detalle2.InsertOnSubmit(OBJDetallleG2);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetallleG2.id_md_interventoria_general_detalle2;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarGeneral3Detalle(md_interventoria_general_detalle3 OBJDetallleG3, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_interventoria_general_detalle3.InsertOnSubmit(OBJDetallleG3);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetallleG3.id_md_interventoria_general_detalle3;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarGeneral4Detalle(md_interventoria_general_detalle4 OBJDetallleG4, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_interventoria_general_detalle4.InsertOnSubmit(OBJDetallleG4);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetallleG4.id_md_interventoria_general_detalle4;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }


        public Int32 InsertarCargueCuentasMd(md_cargue_cuentas OBJCargueCuentas, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_cargue_cuentas.InsertOnSubmit(OBJCargueCuentas);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJCargueCuentas.id_md_cargue_cuentas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarSeguimientoPendientes(md_seguimiento_pendientes OBJSeguimientoPendientes, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_seguimiento_pendientes.InsertOnSubmit(OBJSeguimientoPendientes);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJSeguimientoPendientes.id_md_seguimiento_pendientes;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32? InsertarSeguimientoPendientesDetalle(md_seguimiento_pendientes_detalle OBJSeguimientoPendientesDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_seguimiento_pendientes_detalle.InsertOnSubmit(OBJSeguimientoPendientesDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarConsolidadoFacturacion(List<md_consolidado_facturacion> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(OBJDetalle);
                    //var bulkloader = new BulkLoader();
                    //var file = ConfigurationManager.AppSettings["FileLocation"];
                    //var streamReader = new DataReader(file);
                    //Task.Run(async () => await bulkloader.BulkLoadTableAsync(streamReader, "md_consolidado_facturacion")).Wait();


                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarConsolidadoSeguimientoCovid19(List<cargue_seguimiento_covid19> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {


                    DaccInsetfull.BulkInsertEntities(OBJDetalle);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }

        public Int32 InsertarSeguimientoDetalleCovid19(List<cargue_seguimiento_covid19_detalle> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {


                    DaccInsetfull.BulkInsertEntities(OBJDetalle);

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 0;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 1;
                }
            }
        }


        public Int32 InsertarHerramientaGestion(md_herramienta_tec_gestion OBJGestion, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_herramienta_tec_gestion.InsertOnSubmit(OBJGestion);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJGestion.id_md_gestion_Herramienta;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 Insertardispensacion_ambulatoria(md_dispensacion_ambulatoria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_dispensacion_ambulatoria.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_dispensacion_ambulatoria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 Insertardispensacion_Hospitalaria(md_dispensacion_hospitalaria OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_dispensacion_hospitalaria.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_dispensacion_hospitalaria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 Insertardispensacion_ambulatoriaDetalle(md_dispensacion_ambulatoria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_dispensacion_ambulatoria_detalle.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_dispensacion_ambulatoria_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 Insertardispensacion_HospitalariaDetalle(md_dispensacion_hospitalaria_detalle OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_dispensacion_hospitalaria_detalle.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_md_dispensacion_hospitalaria_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 Insertarcontrol_gasto(md_control_gastos OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_control_gastos.InsertOnSubmit(OBJDetalle);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJDetalle.id_presupuesto_ejecutado;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        /*Alexis Quiñones 4-dic-2019*/

        //public void CarguePrefacturas(md_prefacturas_cargue_base carguebase, List<md_prefacturas_detalle> carguedetalle, ref MessageResponseOBJ MsgRes)
        //{
        //    md_prefacturas_cargue_base prefacturas = new md_prefacturas_cargue_base();
        //    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
        //    {
        //        try
        //        {

        //            prefacturas.id_regional = carguebase.id_regional;
        //            prefacturas.num_prefactura = carguebase.num_prefactura;
        //            prefacturas.id_prestador = carguebase.id_prestador;
        //            prefacturas.fecha_digita = carguebase.fecha_digita;
        //            prefacturas.usuario_digita = carguebase.usuario_digita;
        //            db.md_prefacturas_cargue_base.InsertOnSubmit(prefacturas);
        //            db.SubmitChanges();

        //            if (prefacturas.id_cargue_base != 0)
        //            {
        //                foreach (var item in carguedetalle)
        //                {
        //                    item.Id_prefactura_cargue_base = prefacturas.id_cargue_base;
        //                    try
        //                    {
        //                        string strDate = item.fecha_despacho_formula;
        //                        string[] dateString = strDate.Split('/');
        //                        DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                        item.fecha_despacho_formula = enter_date.Date.ToString("MM/dd/yyyy");
        //                    }
        //                    catch
        //                    {

        //                    }

        //                }
        //                DaccInsetfull.BulkInsertEntities(carguedetalle);
        //                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            if (prefacturas.id_cargue_base != 0)
        //            {
        //                var prefactura = db.md_prefacturas_cargue_base.Where(l => l.id_cargue_base == prefacturas.id_cargue_base).FirstOrDefault();
        //                db.md_prefacturas_cargue_base.DeleteOnSubmit(prefactura);
        //                db.SubmitChanges();
        //            }
        //            MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
        //            MsgRes.DescriptionResponse = ex.Message;
        //        }
        //    }
        //}


        public void CargueLupe(md_Lupe_cargue_base carguebase, List<md_lupe_cargue_base_detalle> carguedetalle, List<md_lupe_intermediacion_dtll> Intermediaciones, ref MessageResponseOBJ MsgRes)
        {
            md_Lupe_cargue_base lupes = new md_Lupe_cargue_base();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    lupes.id_prestador = carguebase.id_prestador;
                    lupes.vigencia_desde = carguebase.vigencia_desde;
                    lupes.fecha_digita = carguebase.fecha_digita;
                    lupes.usuario_digita = carguebase.usuario_digita;
                    db.md_Lupe_cargue_base.InsertOnSubmit(lupes);
                    db.SubmitChanges();

                    if (lupes.id_lupe_cargue_base != 0)
                    {
                        /*En este paso actualiza la fecha de finalizacion de la lupe vigente*/
                        md_Lupe_cargue_base lupe = db.md_Lupe_cargue_base.Where(l => l.id_lupe_cargue_base != lupes.id_lupe_cargue_base && l.vigencia_hasta == null).FirstOrDefault();
                        if (lupe != null)
                        {
                            lupe.vigencia_hasta = carguebase.vigencia_desde.AddDays(-1);
                            db.SubmitChanges();
                        }

                        /*En este punto recorremos el listado del detalle de la lupe, agregando a el el id de la lupe*/
                        foreach (var item in carguedetalle)
                        {
                            item.id_lupe_cargue_base = lupes.id_lupe_cargue_base;
                        }

                        /*Guardamos el detalle del lupe con bulk copy*/
                        DaccInsetfull.BulkInsertEntities(carguedetalle);

                        /*Iniciamos guardando la base de la intermediacion*/
                        md_lupe_intermediacion md = new md_lupe_intermediacion();
                        md.lupe_id = lupes.id_lupe_cargue_base;
                        md.fecha_vigencia = carguebase.vigencia_desde;
                        md.prestador_id = carguebase.id_prestador.Value;
                        md.fecha_digita = (DateTime)carguebase.fecha_digita;
                        md.usuario_digita = carguebase.usuario_digita;
                        db.md_lupe_intermediacion.InsertOnSubmit(md);
                        db.SubmitChanges();

                        if (md.id_lupe_interemediacion != 0)
                        {
                            foreach (var item in Intermediaciones)
                            {
                                item.id_lupe_int_base = md.id_lupe_interemediacion;
                            }
                            DaccInsetfull.BulkInsertEntities(Intermediaciones);
                        }

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }
                }
                catch (Exception ex)
                {
                    if (lupes.id_lupe_cargue_base != 0)
                    {
                        var lupe = db.md_Lupe_cargue_base.Where(l => l.id_lupe_cargue_base == lupes.id_lupe_cargue_base).FirstOrDefault();
                        db.md_Lupe_cargue_base.DeleteOnSubmit(lupe);
                        db.SubmitChanges();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }



        public void InsertarCargueMasivoDispensaciones(dispensacion_cargue_base obj, List<dispensacion_cargue_base_dtll> lista, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    var reg = db.dispensacion_cargue_base.Where(l => l.mes == obj.mes && l.año == obj.año && l.id_regional == obj.id_regional && l.prestador_id == obj.prestador_id).FirstOrDefault();
                    if (reg == null)
                    {
                        db.dispensacion_cargue_base.InsertOnSubmit(obj);
                        db.SubmitChanges();
                        if (obj.id_cargue_base != 0)
                        {
                            foreach (var item in lista)
                            {
                                item.dispensacion_cargue_base_id = obj.id_cargue_base;
                            }
                            DaccInsetfull.BulkInsertEntities(lista);
                            MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ya se encuentra un cargue similar en la base.";
                    }
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        /*kevin suarez 16-03-2022*/

        public int CarguePrefacturaBase(md_prefacturas_cargue_base carguebase, List<md_prefacturas_detalle> listaDetalle)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 20000;

                    db.md_prefacturas_cargue_base.InsertOnSubmit(carguebase);
                    db.SubmitChanges();
                    var idBase = carguebase.id_cargue_base;

                    foreach (var item in listaDetalle)
                    {
                        item.Id_prefactura_cargue_base = idBase;
                    }
                    try
                    {
                        DaccInsetfull.BulkInsertEntities(listaDetalle);
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }

                    return idBase;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int CarguePrefacturaLista(List<md_prefacturas_detalle> listadoCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int CargueLUPEBase(md_Lupe_cargue_base obj, List<md_lupe_cargue_base_detalle> obj2)
        {
            var idCargue = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_Lupe_cargue_base.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    idCargue = obj.id_lupe_cargue_base;
                    if (idCargue != 0)
                    {
                        foreach (var item in obj2)
                        {
                            item.id_lupe_cargue_base = idCargue;
                        }

                        try
                        {
                            DaccInsetfull.BulkInsertEntities(obj2);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                        }


                    }

                    return idCargue;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int CargueLUPELista(List<md_lupe_cargue_base_detalle> listadoCargue, int id_cargueBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    foreach (var item in listadoCargue)
                    {
                        item.id_lupe_cargue_base = id_cargueBase;
                    }

                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int CargueLUPEListaReaprobacion(List<md_lupe_cargue_base_detalle_reaprobacion> listadoCargue, int idCargue, int idPrefactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listadoCargue);

                    //db.management_mdLupe_actualizarReaprobacion(idCargue, idPrefactura);
                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int InsertarReparobacioPrefacturas(List<md_prefacturas_reaprobacionMasiva> listadoCargue, int idPrefacturaBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_eliminarCargueMasivoAnteriores(idPrefacturaBase);
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    db.management_mdLupe_actualizarReaprobacion(idPrefacturaBase);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int InsertarDesaparobacioPrefacturas(List<md_prefacturas_desaprobacionMasiva> listadoCargue, int idPrefacturaBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_eliminarCargueMasivoAnterioresDesapobacion(idPrefacturaBase);
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    db.management_mdLupe_actualizarDeaprobacion(idPrefacturaBase);
                    db.management_mdLupe_actualizarDeaprobacion_conteo(idPrefacturaBase);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int InsertarCierrePrefacturasMasivo(List<md_prefacturas_cierreMasivo> listadoCargue, int idPrefacturaBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    db.management_prefacturas_eliminarCierreMasivoAnterior(idPrefacturaBase);
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    db.management_prefacturas_insertarCierrePrefacturasFinal(idPrefacturaBase);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }


        public int CargueLupeIntermediacionBase(md_lupe_intermediacion obj, int idCargueBase)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj.lupe_id = idCargueBase;
                    db.md_lupe_intermediacion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_lupe_interemediacion;
                }
            }
            catch (Exception e)
            {
                var mensajerrror = e.Message;
                return 0;
            }
        }


        public int CargueMedicamentosRegulados(md_medicamentos_regulados obj, List<md_medicamentos_regulados_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_medicamentos_regulados.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_med;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_med = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int CargueMedicamentosDatosOPLS(md_medicamentos_OPLS obj, List<md_medicamentos_OPLS_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    try
                    {
                        db.management_prefacturas_eliminarOPLSAntiguos();
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }

                    db.md_medicamentos_OPLS.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_OPL;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_opl = idBase;
                        }


                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int CargueLupeIntermediacionLista(List<md_lupe_intermediacion_dtll> listadoCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    return 1;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int GuardarLogEliminacionPrefacturas(log_prefacturas_eliminarCargues obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_prefacturas_eliminarCargues.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception e)
            {
                var mensajeErrror = e.Message;
                return 0;
            }
        }

        public int GuardarPrefacturaCerrada(md_prefacturas_cargue_cerradas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_prefacturas_cargue_cerradas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_prefactura_cerrada;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }


        public int guardarLogDesaprobacionPrefacturas(List<log_prefacturas_desaprobacion> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(lista);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int guardarLogAprobacionPrefacturas(List<log_prefacturas_aprobacion> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(lista);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarLogAprobacionMasiva(log_prefacturas_aprobacionMasiva obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_prefacturas_aprobacionMasiva.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }

        public int GuardarLogControl_validacionesPrefacturas(log_control_validaciones_prefacturas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_control_validaciones_prefacturas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }


        public int GuardarLogDesaprobacionMasiva(log_prefacturas_desaprobacionMasiva obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_prefacturas_desaprobacionMasiva.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }

        public int GuardarLogDatosAprobacionMasiva(int? idCargue, int? idLog, string usuarioDigita)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_historicoAprobacion(idCargue, idLog, usuarioDigita);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }

        public int GuardarLogDatosDesaprobacionMasiva(int? idCargue, int? idLog, string usuarioDigita)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_historicoDesaprobacion(idCargue, idLog, usuarioDigita);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }

        public int LogErroresFasesPrefacturas(log_prefacturas_errorFases obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_prefacturas_errorFases.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }


        #endregion

        #region Analisis Casos

        public int InsertarAnalisisCasos(analisis_caso_original Analisis, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.analisis_caso_original.InsertOnSubmit(Analisis);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Analisis.id_analisis_caso_original;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarAnalisisMuestreo(analisis_caso_muestreo AnalisisMuestreo, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.analisis_caso_muestreo.InsertOnSubmit(AnalisisMuestreo);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return AnalisisMuestreo.id_analisis_caso_muestreo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarAnalisisCasosAlerta(analisis_caso_alertas AnalisisAlerta, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.analisis_caso_alertas.InsertOnSubmit(AnalisisAlerta);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return AnalisisAlerta.id_analisis_caso_alertas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }



        public int InsertarAnalisisCasosSaludP(analisis_caso_salud_publica Analisis, ref MessageResponseOBJ MsgRes)
        {
            int rta = 0;

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                db.analisis_caso_salud_publica.InsertOnSubmit(Analisis);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                rta = Analisis.id_analisis_caso_salud_publica;
            }

            return rta;
        }

        public void InsertarAnalisisCasosEventos(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia_eventos_en_asalud newobj = Analisis;
                    Analisis.id_ecop_concurrencia_eventos_en_asalud = 0;
                    db.ecop_concurrencia_eventos_en_asalud.InsertOnSubmit(newobj);
                    db.SubmitChanges();
                    int ideventosenasalud = Analisis.id_ecop_concurrencia_eventos_en_asalud;
                    if (ideventosenasalud != 0 && otrosiList.Count() > 0)
                    {
                        foreach (ecop_concurrencia_eventos_en_salud_detalle item in otrosiList)
                        {
                            ecop_concurrencia_eventos_en_salud_detalle otrosi = item;
                            otrosi.id_ecop_concurrencia_eventos_en_salud = ideventosenasalud;
                            db.ecop_concurrencia_eventos_en_salud_detalle.InsertOnSubmit(otrosi);
                            db.SubmitChanges();
                        }
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


        public void InsertarAnalisisCasosEventosDet(ecop_concurrencia_eventos_en_asalud Analisis, List<ecop_concurrencia_eventos_en_salud_detalle> otrosiList, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ecop_concurrencia_eventos_en_asalud newobj = Analisis;
                    int ideventosenasalud = Analisis.id_ecop_concurrencia_eventos_en_asalud;
                    if (ideventosenasalud != 0 && otrosiList.Count() > 0)
                    {
                        foreach (ecop_concurrencia_eventos_en_salud_detalle item in otrosiList)
                        {
                            if (item.id_ecop_concurrencia_eventos_en_salud_detalle == 0)
                            {
                                ecop_concurrencia_eventos_en_salud_detalle otrosi = item;
                                otrosi.id_ecop_concurrencia_eventos_en_salud = ideventosenasalud;
                                db.ecop_concurrencia_eventos_en_salud_detalle.InsertOnSubmit(otrosi);
                                db.SubmitChanges();
                            }

                        }
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

        public Int32 InsertarAnalisisCasosEventosDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_concurrencia_eventos_en_salud_detalle.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ecop_concurrencia_eventos_en_salud_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void InsertarLogConcurrenciaEnviadaCallCenter(List<log_concurrenciaEnviada_contactCenter> log, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(log);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }


            }

        }

        public void InsertarLogindividualConcurrenciaEnviadaCallCenter(log_concurrenciaEnviada_contactCenter log, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.log_concurrenciaEnviada_contactCenter.InsertOnSubmit(log);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }

        public void Insertargestionanalisisdecaso(GestionAnalisisDeCasos Analisis, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.GestionAnalisisDeCasos.InsertOnSubmit(Analisis);
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

        #region URGENCIAS

        public void InsertarUrgencias(List<urg_cargue_base_ok> ListUrgencias, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(ListUrgencias);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }


            }

        }

        public void InsertarAuditoriaUrgencias(urg_auditoria_urgencias aud_urgencias, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.urg_auditoria_urgencias.InsertOnSubmit(aud_urgencias);
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

            }
        }
        #endregion

        #region MEGA

        public void InsertarMega(List<megas_cargue_base> ListMega, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(ListMega);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }


            }

        }
        #endregion

        #region Cierre Contable
        public Int32 InsertarCierreContable(cierre_contable obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    cierre_contable cierre = obj;
                    db.cierre_contable.InsertOnSubmit(cierre);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return cierre.id_cierre;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public bool InsertarFacturasMesInterior(List<cierre_cont_mes_anterior> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }

        public bool InsertarFacturasRechazos(List<cierre_cont_rechazos> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }

        public bool InsertarFacturasPendientesProcs(List<cierre_cont_pendiente_procesar> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }

        public bool InsertarFacturasdevoluciones(List<cierre_cont_devoluciones> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }

        public bool InsertarFacturascausadas(List<cierre_cont_causadas> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }

        public bool InsertarFacturasradicadas(List<cierre_cont_radicadas> List, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    return true;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return false;
                }
            }
        }


        public int InsertarCierreContable(cierre_contable_cargue_base obj, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.cierre_contable_cargue_base.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    int idinsert = obj.id_cargue;
                    if (idinsert != 0)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        return idinsert;
                    }
                    else
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "No ha retornado el id del cargue base de gastos por servicio";
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 0;
                }
            }
        }

        public void InsertarCierreContableDetalle(List<cierre_contable_cargue_detalle> dtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsetfull.BulkInsertEntities(dtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de insertar los datos: " + ex.Message;
            }
        }

        public int InsertarLogEliminarCierreContable(log_cierreContable_eliminarConsolidado obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_cierreContable_eliminarConsolidado.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region Cohortes


        public int InsertCohortesDatos(cohortes_cargue_base obj, List<cohortes_detalle_cargue_OK> lista, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                Int32 idcargue = 0;
                try
                {
                    db.cohortes_cargue_base.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    idcargue = obj.id_cohortes_cargue_base;
                    if (idcargue > 0)
                    {
                        foreach (cohortes_detalle_cargue_OK item in lista)
                        {
                            item.id_cargue_cohortes = idcargue;
                        }
                        DaccInsetfull.BulkInsertEntities(lista);
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_cohortes_cargue_base;
                }
                catch (Exception ex)
                {
                    if (idcargue != 0)
                    {
                        var epoc = db.cohortes_cargue_base.Where(l => l.id_cohortes_cargue_base == idcargue).FirstOrDefault();
                        db.cohortes_cargue_base.DeleteOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 0;
                }
            }
        }

        public int InsertCohortesEPOC(cohortes_cargue_base obj, List<cohortes_detalle_cargue_OK> listaepoc, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                Int32 idcargue = 0;
                try
                {
                    db.cohortes_cargue_base.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    idcargue = obj.id_cohortes_cargue_base;
                    if (idcargue > 0)
                    {
                        foreach (cohortes_detalle_cargue_OK item in listaepoc)
                        {
                            item.id_cargue_cohortes = idcargue;
                        }
                        DaccInsetfull.BulkInsertEntities(listaepoc);
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_cohortes_cargue_base;
                }
                catch (Exception ex)
                {
                    if (idcargue != 0)
                    {
                        var epoc = db.cohortes_cargue_base.Where(l => l.id_cohortes_cargue_base == idcargue).FirstOrDefault();
                        db.cohortes_cargue_base.DeleteOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 0;
                }
            }
        }

        public void InsertCohortesPAD(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaPAD, ref MessageResponseOBJ MsgRes)
        {
            Int32 idcargue = 0;
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.cohortes_cargue_base.InsertOnSubmit(cargue);
                    db.SubmitChanges();

                    idcargue = cargue.id_cohortes_cargue_base;
                    if (idcargue > 0)
                    {
                        foreach (cohortes_detalle_cargue_OK item in listaPAD)
                        {
                            item.id_cargue_cohortes = idcargue;
                        }
                        DaccInsetfull.BulkInsertEntities(listaPAD);
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    if (idcargue != 0)
                    {
                        var epoc = db.cohortes_cargue_base.Where(l => l.id_cohortes_cargue_base == idcargue).FirstOrDefault();
                        db.cohortes_cargue_base.DeleteOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }

            }

        }

        public void InsertCohortesRCV(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaRCV, ref MessageResponseOBJ MsgRes)
        {
            Int32 idcargue = 0;
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.cohortes_cargue_base.InsertOnSubmit(cargue);
                    db.SubmitChanges();

                    idcargue = cargue.id_cohortes_cargue_base;
                    if (idcargue > 0)
                    {
                        foreach (cohortes_detalle_cargue_OK item in listaRCV)
                        {
                            item.id_cargue_cohortes = idcargue;
                        }

                        DaccInsetfull.BulkInsertEntities(listaRCV);
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    if (idcargue != 0)
                    {
                        var epoc = db.cohortes_cargue_base.Where(l => l.id_cohortes_cargue_base == idcargue).FirstOrDefault();
                        db.cohortes_cargue_base.DeleteOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }


            }
        }

        public void InsertCohortesGESTANTES(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaGestantes, ref MessageResponseOBJ MsgRes)
        {

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                Int32 idcargue = 0;
                try
                {
                    db.cohortes_cargue_base.InsertOnSubmit(cargue);
                    db.SubmitChanges();

                    idcargue = cargue.id_cohortes_cargue_base;
                    if (idcargue > 0)
                    {
                        foreach (cohortes_detalle_cargue_OK item in listaGestantes)
                        {
                            item.id_cargue_cohortes = idcargue;
                        }
                        DaccInsetfull.BulkInsertEntities(listaGestantes);
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    if (idcargue != 0)
                    {
                        var epoc = db.cohortes_cargue_base.Where(l => l.id_cohortes_cargue_base == idcargue).FirstOrDefault();
                        db.cohortes_cargue_base.DeleteOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

        }


        #endregion

        #region Adherencia

        public void InsertarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_adh_tipo_criterio.InsertOnSubmit(obj);
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

        public void InsertarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.adh_criterio.InsertOnSubmit(criterio);
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

        public int InsertarResultados(adh_resultados resultados, List<string> resultadoshc1, ref MessageResponseOBJ Msg)
        {
            int result = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.adh_resultados.InsertOnSubmit(resultados);
                    db.SubmitChanges();

                    Int32 id = resultados.id_adh_resultados;

                    if (id > 0)
                    {
                        result = id;
                        List<ref_adh_tipo_criterio> tipocriterios = db.ref_adh_tipo_criterio.ToList();
                        List<adh_criterio> criterios = db.adh_criterio.Where(l => l.id_tipo_cohorte == resultados.id_tipo_cohorte).ToList();

                        for (int i = 0; i < resultadoshc1.Count; i++)
                        {
                            adh_resultados_detalle resul = new adh_resultados_detalle();
                            var rst1 = resultadoshc1[i].Split('-');
                            resul.id_adh_resultados = id;
                            resul.id_criterio = Convert.ToInt32(rst1[0]);
                            resul.resultado_historia_clinica = Convert.ToInt32(rst1[1]);
                            resul.seleccion_historia_clinica = rst1[2];
                            db.adh_resultados_detalle.InsertOnSubmit(resul);
                            db.SubmitChanges();
                        }
                    }
                }

                Msg.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return result;

            }
            catch (Exception ex)
            {
                Msg.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                Msg.DescriptionResponse = ex.Message;
                return result;
            }
        }

        public void InsertarTipoCohorte(ref_cohortes obj)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                db.ref_cohortes.InsertOnSubmit(obj);
                db.SubmitChanges();
            }
        }


        public void InsertarAdminCriterios(int tipoadherencia, List<int> seleccionados, List<int> seleccionados2, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    List<adh_tipocriterio> lst = db.adh_tipocriterio.Where(l => l.id_tipo_cohorte == tipoadherencia).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            int? encontrado = seleccionados.Where(l => l == item.id_tipo_criterio).FirstOrDefault();
                            if (encontrado == 0)
                            {
                                var criterios = db.adh_criterio.Where(l => l.id_tipo_cohorte == tipoadherencia && l.id_tipo_criterio == item.id_tipo_criterio).ToList();
                                if (criterios.Count > 0)
                                {
                                    db.adh_criterio.DeleteAllOnSubmit(criterios);
                                    db.SubmitChanges();
                                }
                                db.adh_tipocriterio.DeleteOnSubmit(item);
                                db.SubmitChanges();
                            }
                        }
                    }

                    for (int i = 0; i < seleccionados.Count; i++)
                    {
                        adh_tipocriterio resul = db.adh_tipocriterio.Where(l => l.id_tipo_cohorte == tipoadherencia && l.id_tipo_criterio == seleccionados[i]).FirstOrDefault();
                        if (resul == null)
                        {
                            adh_tipocriterio obj = new adh_tipocriterio();
                            obj.id_tipo_cohorte = tipoadherencia;
                            obj.id_tipo_criterio = seleccionados[i];
                            obj.puntaje = 0;
                            obj.id_grupo_tipocriterio = seleccionados2[i];
                            db.adh_tipocriterio.InsertOnSubmit(obj);
                            db.SubmitChanges();
                        }
                        else
                        {
                            resul.id_tipo_criterio = seleccionados[i];
                            resul.id_grupo_tipocriterio = seleccionados2[i];
                            db.SubmitChanges();

                        }
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }
        #endregion


        #region ODONTOLOGIA

        public Int32 InsertarOdontOrtodoncia(odont_tratamiento_ortodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_tratamiento_ortodoncia.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_tratamiento_ortodoncia;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarOdontOrtodonciaDetalle(odont_tratamiento_ortodoncia_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_tratamiento_ortodoncia_detalle.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_lista_cheq_tratamiento_ortodoncia;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarOdontEndodoncia(odont_tratamiento_endodoncia OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_tratamiento_endodoncia.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_tratamiento_endodoncia;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarOdontEndodonciadtll(odont_tratamiento_endodoncia_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_tratamiento_endodoncia_dtll.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_tratamiento_endodoncia_dtll;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarOdontFija(odont_rehabilitacion_oral_protesis_fija OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_rehabilitacion_oral_protesis_fija.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_rehabilitacion_oral_protesis_fija;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void InsertarOdontFijaDtll(List<odont_rehabilitacion_oral_protesis_fija_dtll> OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_rehabilitacion_oral_protesis_fija_dtll.InsertAllOnSubmit(OBJ);
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

        public Int32 InsertarOdontRemovible(odont_rehabilitacion_oral_protesis_removibles OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_rehabilitacion_oral_protesis_removibles.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_rehabilitacion_oral_protesis_removibles;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarOdontRemovibledtll(odont_rehabilitacion_oral_protesis_removibles_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_rehabilitacion_oral_protesis_removibles_dtll.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_rehabilitacion_oral_protesis_removibles_dll;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoraTratamiento(odont_plan_mejora_tratamiento OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora_tratamiento.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora_tratamiento;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoraTratamientodetalle(odont_plan_mejora_tratamiento_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora_tratamiento_dtll.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora_tratamiento_dtll;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoraBeneficiario(odont_plan_mejora_beneficiario OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora_beneficiario.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora_beneficiario;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoraBeneficiariodetalle(odont_plan_mejora_beneficiario_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora_beneficiario_dtll.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora_beneficiario_dtll;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejora(odont_plan_mejora OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoradetalle(odont_plan_mejora_dtll OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_plan_mejora_dtll.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_plan_mejora_dtll;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarHistoriaClinica(odont_historia_clinica OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_historia_clinica.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_historia_clinica;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarHistoriaClinicaPaciente(odont_historia_clinica_paciente OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_historia_clinica_paciente.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_historia_clinica_paciente;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarHistoriaClinicaDetalle(odont_historia_clinica_detalle_calidad OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_historia_clinica_detalle_calidad.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_historia_clinica_detalle;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarHistoriaClinicaDetalleConte(odont_historia_clinica_detalle_contenido OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_historia_clinica_detalle_contenido.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_odont_historia_clinica_detalle_contenido;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public void InsertarRemisionesEspecialidades(odont_remisiones_especialidades obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_remisiones_especialidades.InsertOnSubmit(obj);
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

        public void InsertarRemisionesVerificadas(odont_remisiones_verificadas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.odont_remisiones_verificadas.InsertOnSubmit(obj);
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

        public void InsertarprestadorOdontologia(Ref_odont_prestadores obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Ref_odont_prestadores.InsertOnSubmit(obj);
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


        public Int32 InsertarSeguimientoCovid19Detalle(cargue_seguimiento_covid19_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_seguimiento_covid19_detalle newobj = OBJ;
                    OBJ.id_seguimiento_detalle = 0;
                    db.cargue_seguimiento_covid19_detalle.InsertOnSubmit(newobj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_seguimiento_detalle;
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

        #region FFMM

        public Int32 InsertarFFMMRadicacion(ffmm_Cuentas_radicacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_Cuentas_radicacion.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ref_ffmm_radicacion_Cuentas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarFFMMAuditoria(ffmm_Cuentas_auditoria OBJ, List<ffmm_cuentas_medicas_cups> obj2, List<ffmm_cuentas_medicas_glosas> obj3, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_Cuentas_auditoria.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    var idffmm_ca = OBJ.id_ffmm_Cuentas_auditoria;

                    try
                    {
                        if (obj2.Count() > 0)
                        {
                            foreach (ffmm_cuentas_medicas_cups item in obj2)
                            {

                                ffmm_cuentas_medicas_cups objCups = new ffmm_cuentas_medicas_cups();
                                objCups = item;
                                objCups.id_ffmm_Cuentas_auditoria = idffmm_ca;

                                db.ffmm_cuentas_medicas_cups.InsertOnSubmit(objCups);
                                db.SubmitChanges();
                            }
                        }

                        if (obj3.Count() > 0)
                        {
                            foreach (ffmm_cuentas_medicas_glosas item in obj3)
                            {

                                ffmm_cuentas_medicas_glosas objGlosa = new ffmm_cuentas_medicas_glosas();
                                objGlosa = item;
                                objGlosa.id_ffmm_Cuentas_auditoria = idffmm_ca;
                                db.ffmm_cuentas_medicas_glosas.InsertOnSubmit(objGlosa);
                                db.SubmitChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }
                    return OBJ.id_ffmm_Cuentas_auditoria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarFFMMAuditoriaGlosas(ffmm_cuentas_glosas OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_cuentas_glosas.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ffmm_Cuentas_auditoria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }


        public Int32 InsertarFFMMref_proveedor(Ref_ffmm_prestadores_proveedor OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Ref_ffmm_prestadores_proveedor.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ref_ffmm_prestadores_proveedor;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarFFMMref_paliativos(ffmm_cuidados_paliativos OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_cuidados_paliativos.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ffmm_cuidados_paliativos;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public Int32 InsertarFFMMGlosaConciliacion(md_glosa_conciliacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.md_glosa_conciliacion.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_glosa_conciliacion;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }


        public int GuardarMedicamentosFacturacion(medicamentos_facturacion Obj, List<medicamentos_facturacion_dtll> Result, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            try
            {
                db.medicamentos_facturacion.InsertOnSubmit(Obj);
                db.SubmitChanges();
                int id = Obj.id_medicamentos_facturacion;
                if (id > 0)
                {
                    foreach (var item in Result)
                    {
                        item.id_medicamentos_facturacion = id;
                    }

                    DaccInsetfull.BulkInsertEntities(Result);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                else
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = "No se ha insertado el cargue base";
                }

                return id;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarFFMMAuditoria(ffmm_auditoria OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_auditoria.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_ffmm_auditoria;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public int InsertarIpsPrestador(ref_ffmm_ips_prestadores obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_ffmm_ips_prestadores.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.ip_ips_proveedor;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarContratosFFMM(ffmm_contratos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_contratos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_contrato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarCargueMasivoPagoFactura(List<ffmm_cargue_facturas_pago> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext dbb = new ECOPETROL_DataContexDataContext())
                {
                    try
                    {
                        DaccInsetfull.BulkInsertEntities(List);
                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                        return 0;
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
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

        #region Contratacion 
        public void InsertarCargueContratacion(contratacion_cargue_base obj, List<contratacion_cargue_dtll> ListaContratacion, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.contratacion_cargue_base.InsertOnSubmit(obj);
                db.SubmitChanges();

                if (obj.id_contratacion_cargue_base > 0)
                {
                    foreach (var item in ListaContratacion)
                    {
                        item.id_contratacion_cargue_base = obj.id_contratacion_cargue_base;
                    }
                    DaccInsetfull.BulkInsertEntities(ListaContratacion);
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }
        #endregion

        public Int32 InsertarFirmadigital(ecop_firma_digital_cod_barras obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_firma_digital_cod_barras.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_firma_digital;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarFirmadigitalsami(ecop_firma_digital_sami obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_firma_digital_sami.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_firmas_sami;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarGestionFacturadigital(ecop_gestion_factura_digital obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_factura_digital.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_gestion_factura_digital;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarLogCambiosGetionHospitalaria(log_cambios_gestion_hospitalaria obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_cambios_gestion_hospitalaria.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_log_cambios;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarGestionFacturadigitalGasto(ecop_gestion_factura_digital_gasto obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_factura_digital_gasto.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_factura_digital_gasto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void insertarListadoGestionFacturadigitalGasto(List<ecop_gestion_factura_digital_gasto> obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_factura_digital_gasto.InsertAllOnSubmit(obj);
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

        public Int32 InsertarFacturaAprobadas(ecop_gestion_facturas_aprobadas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_gestion_facturas_aprobadas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Carta de aprobación insertada correctamente ";
                    return obj.id_gestion_factura_aprobadas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void Insertaranalistalote(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            {
                try
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.ref_analista_lote.InsertOnSubmit(obj);
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
        }

        #region GASTOS POR SERVICIO

        public int InsertarGastosPorServicio(gasto_por_servicio_cargue_base obj, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.gasto_por_servicio_cargue_base.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    int idinsert = obj.id_cargue_base;
                    if (idinsert != 0)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        return idinsert;
                    }
                    else
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "No ha retornado el id del cargue base de gastos por servicio";
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 0;
                }
            }
        }

        public void InsertarGastosPorServicioDtll(List<gasto_por_Servicio_cargue_dtll> dtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsetfull.BulkInsertEntities(dtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de insertar los datos: " + ex.Message;
            }
        }

        public int InsertarLogEliminarGastoxServicio(log_gastoxServicio_eliminarConsolidado obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_gastoxServicio_eliminarConsolidado.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.log_id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }
        #endregion

        #region SEGUIMIENTO ENTREGABLES


        public void InsertarOActualizarSeguimientoEntregable(seguimiento_entregables obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (obj.id_seg_entregables == 0)
                    {
                        db.seguimiento_entregables.InsertOnSubmit(obj);
                        db.SubmitChanges();

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        MsgRes.DescriptionResponse = "El entregable se ha guardado correctamente.";
                    }
                    else
                    {
                        seguimiento_entregables entregable = db.seguimiento_entregables.Where(l => l.id_seg_entregables == obj.id_seg_entregables).FirstOrDefault();
                        entregable.Nom_entregable = obj.Nom_entregable;
                        entregable.Proceso = obj.Proceso;
                        entregable.periodicidad_entrega = obj.periodicidad_entrega;
                        entregable.persona_responsable = obj.persona_responsable;
                        entregable.fecha_control = obj.fecha_control;
                        entregable.id_tipo_entregable = obj.id_tipo_entregable;
                        db.SubmitChanges();

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        MsgRes.DescriptionResponse = "Los datos del entregable han sido actualizados correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }

        public void InsertarSeguimientoEntregableDTLL(int id_seg_entregable, seguimiento_dtll_entrega obj, List<seguimiento_entregables_documentos> Objdocumentos, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    seguimiento_entregables_periodo segper = db.seguimiento_entregables_periodo.Where(l => (l.id_estado_entregable == 2 || l.id_estado_entregable == 6) && l.id_seg_entregable == id_seg_entregable).FirstOrDefault();
                    if (obj.id_seg_entregable_periodo == null)
                    {
                        obj.id_seg_entregable_periodo = segper.id_seg_entregable_periodo;
                    }
                    db.seguimiento_dtll_entrega.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    int id_dtll = obj.id_seg_dtll_entrega;
                    if (id_dtll > 0)
                    {
                        foreach (seguimiento_entregables_documentos item in Objdocumentos)
                        {
                            item.id_seg_dtll_entrega = id_dtll;
                        }

                        db.seguimiento_entregables_documentos.InsertAllOnSubmit(Objdocumentos);
                        db.SubmitChanges();
                    }

                    /*Editar el estado de seguimiento periodo*/
                    segper = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == obj.id_seg_entregable_periodo).FirstOrDefault();
                    segper.id_estado_entregable = obj.id_estado_entregable;
                    segper.fecha_entrega = DateTime.Now;
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


        public Int32 InsertarSeguimientoEntregableDTLL1(int id_seg_entregable, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            int id_dtll = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    seguimiento_entregables_periodo segper = db.seguimiento_entregables_periodo.Where(l => (l.id_estado_entregable == 2 || l.id_estado_entregable == 6) && l.id_seg_entregable == id_seg_entregable).FirstOrDefault();
                    if (obj.id_seg_entregable_periodo == null)
                    {
                        obj.id_seg_entregable_periodo = segper.id_seg_entregable_periodo;
                    }
                    db.seguimiento_dtll_entrega.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    id_dtll = obj.id_seg_dtll_entrega;
                    /*Editar el estado de seguimiento periodo*/
                    segper = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == obj.id_seg_entregable_periodo).FirstOrDefault();
                    segper.id_estado_entregable = obj.id_estado_entregable;
                    segper.fecha_entrega = DateTime.Now;
                    db.SubmitChanges();

                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                ;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                id_dtll = 0;
            }

            return id_dtll;
        }

        public void InsertarSeguimientoEntregableDTLL2(List<seguimiento_entregables_documentos> lista, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    db.seguimiento_entregables_documentos.InsertAllOnSubmit(lista);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }
        public void InsertarSeguimientoEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.seguimiento_entregables_periodo.InsertOnSubmit(obj);
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

        public void InsertarGestionEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.seguimiento_dtll_entrega.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    int id = obj.id_seg_dtll_entrega;
                    if (id > 0)
                    {
                        seguimiento_entregables_periodo segper = db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == id_seg_entregable_periodo).FirstOrDefault();
                        if (segper != null)
                        {
                            segper.id_estado_entregable = obj.id_estado_entregable;
                            /*Si resulta que la entrega tiene observaciones, la fecha de entrega de este periodo sera nula nuevamente.*/
                            if (obj.id_estado_entregable == 4)
                            {
                                segper.fecha_entrega = null;
                            }
                            db.SubmitChanges();
                        }
                    }
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }


        }

        public int InsertarPeriodoEntregable(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.seguimiento_entregables_periodo.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    int id = obj.id_seg_entregable_periodo;
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return id;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 11 de enero de 2022
        /// Metodo para guardar los datos de la evaluacion de calidad realizada a un entregable.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarDatosEvalCalidadSegEntregable(seguimiento_entregables_periodo_eval_calidad obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.seguimiento_entregables_periodo_eval_calidad.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "La evaluación de calidad del entregable se ha guardado de manera correcta";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        #endregion

        #region GESTION INTERNA
        public void InsertarLogActualizacionFechaEgreso(log_update_fecha_egreso log, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.log_update_fecha_egreso.InsertOnSubmit(log);
                db.SubmitChanges();

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }
        #endregion

        #region CONCURRENCIA
        public Int32 InsertarPlanMejora(ecop_plan_de_mejora obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_de_mejora.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_plan_de_mejora;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarAmpliacionPlanMejora(log_PM_ampliaciones obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_PM_ampliaciones.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejorafoco(ecop_plan_mejora_foco_intervencion obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_mejora_foco_intervencion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_plan_mejora_foco_intervencion;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejoratarea(ecop_plan_mejora_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_mejora_tareas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_plan_mejora_tareas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejora_obs(ecop_plan_mejora_obs_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_mejora_obs_tareas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_plan_mejora_obs_tareas;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 InsertarPlanMejora_documentos(ecop_plan_de_mejora_documental obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_de_mejora_documental.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_archivo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogReactivacionPlanMejora(log_plan_mejora_reactivar obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_plan_mejora_reactivar.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogEliminarPlanMejora(log_plan_mejora_eliminar obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_plan_mejora_eliminar.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int IngresarPlanMejora_notificacionAdmin(ecop_plan_de_mejora_notificarAdmin obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_plan_de_mejora_notificarAdmin.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_notificacion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int IngresarPlanMejora_notificacionAdmin_archivos(List<ecop_plan_de_mejora_notificarAdmin_archivos> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(lista);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogAlertasPlanes(List<log_planes_mejora_notificaciones> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(lista);
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

        #region CONTACT CENTER
        public int InsertarIngresoContactCenter(contact_center obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.contact_center.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_contact_center;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void InsertarBitacoraCallCenter(List<contact_center_dtll> List, int id_contact_center, string usuario)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var lista = db.contact_center_dtll.Where(l => l.id_contact_center == id_contact_center).ToList();
            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    contact_center_dtll obj = List.Where(l => l.ruta_imagen == item.ruta_imagen).FirstOrDefault();
                    if (obj == null)
                    {
                        if (System.IO.File.Exists(item.ruta_imagen))
                        {
                            System.IO.File.Delete(item.ruta_imagen);
                        }
                        db.contact_center_dtll.DeleteOnSubmit(item);
                        db.SubmitChanges();
                    }
                }
            }

            foreach (var item in List)
            {
                //contact_center_dtll encontrado = lista.Where(l => l.ruta_imagen == item.ruta_imagen).FirstOrDefault();
                //if (encontrado == null)
                //{
                contact_center_dtll obj = new contact_center_dtll();
                obj.id_contact_center = id_contact_center;
                obj.id_concurrencia = item.id_concurrencia;
                obj.id_censo = item.id_censo;
                obj.fecha_ingreso = item.fecha_ingreso;
                obj.Observaciones = item.Observaciones;
                obj.fecha_digita = item.fecha_digita;
                obj.usuario_digita = item.usuario_digita;
                obj.estado_solicitud = item.estado_solicitud;
                obj.id_tipo_solicitud = item.id_tipo_solicitud;
                obj.id_servicio = item.id_servicio;
                obj.id_medio_notificacion = item.id_medio_notificacion;
                obj.correo_notificacion = item.correo_notificacion;
                obj.telefono_notificacion = item.telefono_notificacion;
                obj.ruta_imagen = item.ruta_imagen;
                obj.tipo_archivo = item.tipo_archivo;
                obj.nom_contacto_solucionador_ips = item.nom_contacto_solucionador_ips;
                obj.telefono_contacto_solucionador_ips = item.telefono_contacto_solucionador_ips;
                obj.correo_contacto_solucionador_ips = item.correo_contacto_solucionador_ips;
                obj.casoEfectivo = item.casoEfectivo;

                db.contact_center_dtll.InsertOnSubmit(obj);
                db.SubmitChanges();

                if (obj.estado_solicitud == 2)
                {
                    contact_center o = db.contact_center.Where(i => i.id_contact_center == id_contact_center).FirstOrDefault();
                    o.estado_solicitud = item.estado_solicitud;
                    o.usuario_entrega_auditor = usuario;
                    db.SubmitChanges();
                }
                //}
            }
        }

        public int InsertarBitacoraContactCenter(contact_center_dtll obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.contact_center_dtll.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_contact_center_dtll;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region Insumos

        public void InsertarQuejasValidasDtll(List<calidad_quejas_validas_dtll> List, calidad_quejas_validas objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_quejas_validas.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_quejas_validas;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_quejas_validas = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarOportunidadRips(List<calidad_oportunidad_rips_dtll> List, calidad_oportunidad_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_oportunidad_rips.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_oportunidad_rips;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_oportunidad_rips = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarCalidadRips(List<calidad_de_rips_dtll> List, calidad_de_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_de_rips.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_oportunidad_rips;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_de_rips = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarOportunidadCitasMedicas(List<calidad_oportunidad_citas_medicina_gnral_dtll> List, calidad_oportunidad_citas_medicina_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_oportunidad_citas_medicina_gnral.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_oportunidad_citas_medicina_gnral;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_oportunidad_citas_medicina_gnral = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarCalidadCitasCumplidas(List<calidad_citas_cumplidas_dtll> List, calidad_citas_cumplidas objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_citas_cumplidas.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_citas_cumplidas;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_citas_cumplidas = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarOportunidadOdontologia(List<calidad_oportunidad_odontologia_gnral_dtll> List, calidad_oportunidad_odontologia_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.calidad_oportunidad_odontologia_gnral.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_calidad_oportunidad_odontologia_gnral;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_calidad_oportunidad_odontologia_gnral = id;
                    }

                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarEventosAdversos(List<calidad_evento_adverso> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                DaccInsetfull.BulkInsertEntities(List);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarDocumentoInsumo(calidad_gestor_documental_insumos obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.calidad_gestor_documental_insumos.InsertOnSubmit(obj);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void InsertarArchivoQuejasValidas(calidad_quejas_validas_base_zip obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.calidad_quejas_validas_base_zip.InsertOnSubmit(obj);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public void InsertarIndicadoresCalidadDtll(List<insumos_capacidad_resolutiva_dtll> List, insumos_capacidad_resolutiva objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.insumos_capacidad_resolutiva.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = objbase.id_capacidad;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_capacidad = id;
                    }
                    try
                    {
                        DaccInsetfull.BulkInsertEntities(List);
                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
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

        public int InsertarBaseBeneficiariosMasivo(List<base_beneficiarios> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext dbb = new ECOPETROL_DataContexDataContext())
                {
                    try
                    {
                        DaccInsetfull.BulkInsertEntities(List);
                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                        return 0;
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarLogBaseBeneficiarios(log_cargue_base_beneficiarios obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                db.log_cargue_base_beneficiarios.InsertOnSubmit(obj);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return obj.id_log;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return obj.id_log;
            }
        }


        public int insertarPrestador(ref_adherencia_prestador obj, List<ref_adherencia_profesional> lista, int creado)
        {
            var idPrestador = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (creado == 0)
                    {
                        db.ref_adherencia_prestador.InsertOnSubmit(obj);
                        db.SubmitChanges();
                        idPrestador = obj.id_adherencia_prestador;
                    }
                    else
                    {
                        string prestador = obj.Nit_prestador;
                        List<management_traerPrestadoresResult> list = traerPrestadoresId(prestador);
                        management_traerPrestadoresResult valor = list.FirstOrDefault();
                        idPrestador = valor.id_adherencia_prestador;
                    }

                    try
                    {
                        if (lista.Count() > 0)
                        {
                            foreach (ref_adherencia_profesional item in lista)
                            {


                                ref_adherencia_profesional obj2 = new ref_adherencia_profesional();
                                obj2.Nom_profesional = item.Nom_profesional;
                                obj2.Num_documento = item.Num_documento;
                                obj2.Especialidad = item.Especialidad;

                                db.ref_adherencia_profesional.InsertOnSubmit(obj2);
                                db.SubmitChanges();

                                var idprofesional = obj2.id_ref_adherencia_profesional;
                                ref_adherencia_profesional_prestador obj3 = new ref_adherencia_profesional_prestador();

                                obj3.prestador_id = idPrestador;
                                obj3.profesional_id = idprofesional;

                                db.ref_adherencia_profesional_prestador.InsertOnSubmit(obj3);
                                db.SubmitChanges();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        var mensaje = ex.Message;
                        return idPrestador;

                    }
                    return idPrestador;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public List<management_traerPrestadoresResult> traerPrestadoresId(string id)
        {
            List<management_traerPrestadoresResult> list = new List<management_traerPrestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_traerPrestadores(id).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var excepcion = ex.Message;
                return list;
            }
        }

        public int insertarPrestadorCiudad(ref_adherencia_prestador_ciudad obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_adherencia_prestador_ciudad.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idPrestadorCiudad = obj.id_ref_adherencia_prestador_ciudad;
                    return idPrestadorCiudad;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return 0;
            }
        }

        public void InsertarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_verificacion_farmaceutico.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_ver_tipoCriterio.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public void InsertarAdminCriteriosver(int tipoVerificacion, List<int> seleccionados, List<int> seleccionados2, List<string> seleccionados3, string usuario, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    List<ver_tipocriterio> lst = db.ver_tipocriterio.Where(l => l.id_tipo_verificacion == tipoVerificacion).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            int? encontrado = seleccionados.Where(l => l == item.id_tipo_criterio).FirstOrDefault();
                            if (encontrado == 0)
                            {
                                var criterios = db.ver_criterio.Where(l => l.id_tipo_criterio == tipoVerificacion && l.id_tipo_criterio == item.id_tipo_criterio).ToList();
                                if (criterios.Count > 0)
                                {
                                    db.ver_criterio.DeleteAllOnSubmit(criterios);
                                    db.SubmitChanges();
                                }
                                db.ver_tipocriterio.DeleteOnSubmit(item);
                                db.SubmitChanges();
                            }
                        }
                    }

                    for (int i = 0; i < seleccionados.Count; i++)
                    {
                        ver_tipocriterio resul = db.ver_tipocriterio.Where(l => l.id_tipo_verificacion == tipoVerificacion && l.id_tipo_criterio == seleccionados[i]).FirstOrDefault();
                        if (resul == null)
                        {
                            ver_tipocriterio obj = new ver_tipocriterio();
                            obj.id_tipo_verificacion = tipoVerificacion;
                            obj.id_tipo_criterio = seleccionados[i];
                            obj.puntaje = 0;
                            obj.id_grupo_tipocriterio = seleccionados2[i];
                            obj.impacto = seleccionados3[i];
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = usuario;
                            db.ver_tipocriterio.InsertOnSubmit(obj);
                            db.SubmitChanges();
                        }
                        else
                        {
                            resul.id_tipo_criterio = seleccionados[i];
                            resul.id_grupo_tipocriterio = seleccionados2[i];
                            resul.impacto = seleccionados3[i];
                            db.SubmitChanges();
                        }
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }
        }


        public void InsertarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_criterio.InsertOnSubmit(criterio);
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

        public void InsertarCarguePuntoDispensacion(List<ver_puntoDispensacion> List, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                DaccInsetfull.BulkInsertEntities(List);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }
        #endregion

        #region verificacion_medicamentos

        public int InsertarEvaluacionDispensacion(ver_dispen_evaluacion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_dispen_evaluacion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_evaluacion;
                }
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return 0;
            }
        }

        public int InsertarEvaluacionDispensacionTotal(List<ver_dispen_evaluacion_total> List)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                DaccInsetfull.BulkInsertEntities(List);
                return 1;

            }
            catch (Exception ex)
            {
                var mensajeError = ex.Message;
                return 0;
            }

        }

        public int InsertarArchivosEvaluacion(ver_evaluacion_archivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_evaluacion_archivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return (int)obj.id_img;
                }
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return 0;
            }
        }
        public int InsertarArchivosEvaluacionPDFS(ver_evaluacion_pdfs obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_evaluacion_pdfs.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return (int)obj.id_pdf;
                }
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return 0;
            }
        }

        public int insertarHallazgoItemEvaluacion(ver_evaluacion_hallazgo obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_evaluacion_hallazgo.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_hallazgo;
                }
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return 0;
            }
        }

        public int insertarHallazgoItemEvaluacionArchivos(ver_evaluacion_hallazgo_archivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ver_evaluacion_hallazgo_archivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_archivo;
                }
            }
            catch (Exception e)
            {
                var mensajeError = e.Message;
                return 0;
            }
        }




        #endregion

        #region CUIDADOS PALEATIVOS

        public Int32 SaveCuidadosPaliativos(ffmm_cuidados_paliativos objeto, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_cuidados_paliativos.InsertOnSubmit(objeto);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return objeto.id_ffmm_cuidados_paliativos;
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

        #region correosPPE
        public int CargueCorreosPPE(List<ecop_directorioPPE_correos> listadoCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listadoCargue);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var mensajeErrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarFacturacionSAP(List<facturacion_sap_dtll> List, facturacion_sap_cargue objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.facturacion_sap_cargue.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = 0;
                id = objbase.id_factura_cargue;
                if (id > 0)
                {
                    foreach (var item in List)
                    {
                        item.id_factura_cargue = id;
                    }
                    try
                    {
                        DaccInsetfull.BulkInsertEntities(List);
                    }
                    catch (Exception ex)
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = ex.Message;
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
                return id;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        #endregion

        #region GlosasFFMM


        public Int32 SaveProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ffmm_glosas.InsertOnSubmit(objeto);
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


        public int InsertarDispensacionMedicamentosCargue(medicamentos_dispen_cargue objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.medicamentos_dispen_cargue.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = 0;
                id = objbase.id_cargue;
                return id;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        //leo
        public int InsertarDispensacionMedicamentosCalidad(List<medicamentos_dispen_calidad> List, Int32 id_cargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return id_cargue;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }

        }

        public int CargarLoteMedicamentosDispensacion(medicamentos_dispen_lote obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.medicamentos_dispen_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_lote;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region METODOS S/N
        public int InsertarHospitalizacionPrevenibleDtll(ecop_hospitalizacion_prevenible_dtll obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_hospitalizacion_prevenible_dtll.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.int_HE_Dtll;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }
        public Int32 InsertarArchivoHospitalziacionEvitable(ecop_HE_gestion_documental obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_HE_gestion_documental.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_documento;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }
        public int InsertarVigilanciaEpidemiologica(ecop_vigilancia_epidemiologica obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_vigilancia_epidemiologica.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_vigilancia;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }
        public int InsertarVigilanciaEpidemiologica_archivos(ecop_VE_gestion_documental obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ecop_VE_gestion_documental.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_documento;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarGestionAlertaEpidemio(alerta_epidemiologica_gestion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alerta_epidemiologica_gestion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarAnalistas(List<ref_cuentas_medicas_analista> obj)
        {
            try
            {
                DaccInsetfull.BulkInsertEntities(obj);
                return 1;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int Insertar_rips_homologacion(rips_homologacion objbase, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                db.rips_homologacion.InsertOnSubmit(objbase);
                db.SubmitChanges();

                int id = 0;
                id = objbase.id_rips_homologacion;
                return id;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        public int Insertar_rips_homologacion_dtll(List<rips_homologacion_dtll> objbase, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(objbase);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }



        public int InsertarNuevosAnalistas_asignacionAutomatica(List<ref_cuentas_medicas_analista> obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(obj);
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

        #region INVENTARIO FACTURAS CONTABILIZADAS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo que guardar y retorna el id del cargue base de inventario de facturas contabilizadas
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public Int32 InsertarInventarioFacturasContabilizadasCargueBase(inventario_facturas_contabilizadas_carguebase obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.inventario_facturas_contabilizadas_carguebase.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Los datos han sido guardados correctamente";
                    return obj.id_invertario_cargue_base;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo que guarda el detalle de inventario de facturas contabilizadas
        /// </summary>
        /// <param name="dtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarInventarioFacturasContabilizadasCargueDtll(List<inventario_facturas_contabilizadas_carguedtll> dtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsetfull.BulkInsertEntities(dtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos han sido guardados correctamente";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Metodo utilizado para guardar la gestion hecha a una factura en el inventario
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarGestionInvetarioFacturaContabilizada(inventario_facturas_contabilizadas_gestion obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.inventario_facturas_contabilizadas_gestion.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    inventario_facturas_contabilizadas_carguedtll fac = db.inventario_facturas_contabilizadas_carguedtll.Where(l => l.id_inventario_cargue_dtll == obj.inventario_cargue_dtll_id).FirstOrDefault();

                    if (obj.descarga_imagen == "NO" || obj.imagenes_sin_novedades == "NO")
                    {
                        fac.id_estado = 3;

                        if (fac.fue_conNovedad != 1)
                        {
                            fac.fue_conNovedad = 1;
                        }
                    }
                    else
                    {
                        fac.id_estado = 2;
                    }

                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Se han guardado los datos correctamente.";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para porder insertar los datos del archivo cargado en el tablero de inventario facturas contabilizadas consolidado
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="MsgRes"></param>
        public void insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(inventario_facturas_contabilizadas_gestor_documental doc, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.inventario_facturas_contabilizadas_gestor_documental.InsertOnSubmit(doc);
                    db.SubmitChanges();
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "El archivo ha sido cargado correctamente.";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud..";
            }
        }
        #endregion

        #region INVENTARIO ALTO COSTO

        public int insercionMasivaAltoCosto(inventario_altoCosto_carguebase obj, List<inventario_altoCosto_detalle> dtl, ref MessageResponseOBJ MsgRes)
        {
            var idBase = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.inventario_altoCosto_carguebase.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    idBase = obj.id_inventario;

                    if (idBase != 0)
                    {
                        foreach (var item in dtl)
                        {
                            item.id_inventario = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(dtl);
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        MsgRes.DescriptionResponse = "El archivo ha sido cargado correctamente.";
                    }
                    else
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud.";
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + error;
            }

            return idBase;
        }

        public int insercionGestionInventario(inventario_altoCosto_gestiones obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.inventario_altoCosto_gestiones.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "El registro se ha guardado correctamente.";
                    return obj.id_gestion;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + error;
                return 0;
            }
        }


        public Int32 InsertarArchivoisAltoCostoCancer(List<inventario_altoCosto_archivos> archivos, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    DaccInsetfull.BulkInsertEntities(archivos);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return 0;
                }
            }
        }

        public int InsertarLogEliminacionArchivoAltoCosto(log_inventario_altoCosto_eliminacionArchivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_inventario_altoCosto_eliminacionArchivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int cargue_cuentas_altoCosto(cargue_cuentas_altoCosto obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_cuentas_altoCosto.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_Cargue;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
                return 0;
            }
        }


        public int InsertarCuentasAltoCostoConfirmnada(List<cargue_cuentas_altoCosto_confirmada> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarCuentasAltoCostoCancer(List<cargue_cuentas_altoCosto_cancer> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarCuentasAltoCostoHemofilia(List<cargue_cuentas_altoCosto_hemofilia> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarCuentasAltoCostoArtritis(List<cargue_cuentas_altoCosto_artritis> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarCuentasAltoCostoVIH(List<cargue_cuentas_altoCosto_vih> List, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(List);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int GuardarGestionCuentasAltoCosto(cargue_cuentas_altoCosto_gestiones obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_cuentas_altoCosto_gestiones.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                    if (obj.tipo == 1)
                    {
                        cargue_cuentas_altoCosto_cancer obj2 = db.cargue_cuentas_altoCosto_cancer.Where(x => x.id_cancer == obj.id_registro).FirstOrDefault();
                        obj2.estado = obj.estado_caso;
                    }

                    else if (obj.tipo == 2)
                    {
                        cargue_cuentas_altoCosto_hemofilia obj2 = db.cargue_cuentas_altoCosto_hemofilia.Where(x => x.id_hemofilia == obj.id_registro).FirstOrDefault();
                        obj2.estado = obj.estado_caso;
                    }

                    else if (obj.tipo == 3)
                    {
                        cargue_cuentas_altoCosto_artritis obj2 = db.cargue_cuentas_altoCosto_artritis.Where(x => x.id_artritis == obj.id_registro).FirstOrDefault();
                        obj2.estado = obj.estado_caso;
                    }

                    else if (obj.tipo == 4)
                    {
                        cargue_cuentas_altoCosto_vih obj2 = db.cargue_cuentas_altoCosto_vih.Where(x => x.id_vih == obj.id_registro).FirstOrDefault();
                        obj2.estado = obj.estado_caso;
                    }

                    db.SubmitChanges();

                    return obj.id_gestion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
                return 0;
            }
        }

        public Int32 InsertarArchivoReposAltoCosto(cargue_cuentas_altoCosto_archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_cuentas_altoCosto_archivos.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_archivo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public Int32 LogArchivoReposAltoCosto(log_cargue_cuentas_altoCosto_archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_cargue_cuentas_altoCosto_archivos.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_log;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public Int32 GuardarObservacionesCuentasAltoCosto(cargue_cuentas_altoCosto_observaciones OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_cuentas_altoCosto_observaciones.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_observacion;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        #endregion INVENTARIO ALTO COSTO

        #region CARGUE MASIVO CONTRATOS
        public int CargueMasivoContratos(contratos_cargue obj, List<contratos_detalle> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.contratos_cargue.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_cargue;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_cargue = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarContratoNuevoPrestador(contratos_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.contratos_detalle.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_contrato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogContratoNuevoPrestador(log_contratos_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_contratos_detalle.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        #endregion

        #region CARGUE MASIVO RIPS DEPURADOS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue base de los rips depurados
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public int GuardarCargueBaseRipsDepurados(rips_depurados_carguebase cb, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ANALITICA_DataContexDataContext db = new ANALITICA_DataContexDataContext())
                {
                    db.rips_depurados_carguebase.InsertOnSubmit(cb);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "El cargue base se ha realizado de manera correcta";
                    return cb.id_cargue_base;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un erroe la momento de realizar el cargue base de la información: " + ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AC
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAC(List<rips_depurados_ac_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AC de forma exitosa";

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AC
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAP(List<rips_depurados_ap_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AP de forma exitosa";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AC
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAU(List<rips_depurados_au_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AU de forma exitosa";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AM
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAM(List<rips_depurados_am_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AM de forma exitosa";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AM
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAN(List<rips_depurados_an_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AN de forma exitosa";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo utilizado para realizar el cargue dtll de los rips depurados AH
        /// </summary>
        /// <param name="cdtll"></param>
        /// <param name="MsgRes"></param>
        public void InsertarCargueMasivoRipsDepuradosAH(List<rips_depurados_ah_carguedtll> cdtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DaccInsertfullDos.BulkInsertEntities(cdtll);
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Los datos se han insertado correctamente. Se han cargado " + cdtll.Count + " registros del archivo AH de forma exitosa";
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
            }
        }

        public int InsertarPrestadorRIPS(Ref_RIPS_Prestadores obj)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Ref_RIPS_Prestadores (depa_nombre, muni_nombre, codigo_habilitacion, nits_nit, razon_social, muni_nombre1, tipo_id) " +
                                   "VALUES (@depa_nombre, @muni_nombre, @codigo_habilitacion, @nits_nit, @razon_social, @muni_nombre1, @tipo_id)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@depa_nombre", obj.depa_nombre);
                        command.Parameters.AddWithValue("@muni_nombre", obj.muni_nombre);
                        command.Parameters.AddWithValue("@codigo_habilitacion", obj.codigo_habilitacion);
                        command.Parameters.AddWithValue("@nits_nit", obj.nits_nit);
                        command.Parameters.AddWithValue("@razon_social", obj.razon_social);
                        command.Parameters.AddWithValue("@muni_nombre1", obj.muni_nombre1);
                        command.Parameters.AddWithValue("@tipo_id", obj.tipo_id);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0 ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }


        public int InsertarPrestadorRIPS2(Ref_RIPS_Prestadores obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Ref_RIPS_Prestadores.InsertOnSubmit(obj);
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

        #endregion


        #region REEMBOLSO

        public int InsertarRembolso(cuentas_reembolsos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cuentas_reembolsos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_reembolso;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarRembolsoDetalle(cuentas_reembolso_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cuentas_reembolso_detalle.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_detalle;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarRembolsoArchivos(cuentas_reembolsos_archivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cuentas_reembolsos_archivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_archivo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        #endregion REEMBOLSO

        #region NORIPS

        public int InsertarNoRips(cuentas_medicas_norips obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cuentas_medicas_norips.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj.id_med;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud " + ex.Message;
                return 0;
            }
        }

        public Int32 IngresoArchivosRipsNoEsalud(cuentas_medicas_norips_Archivos OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cuentas_medicas_norips_Archivos.InsertOnSubmit(OBJ);
                    db.SubmitChanges();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return OBJ.id_archivo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }
        #endregion NORIPS

        #region CAMPAÑAS

        public int InsertarCreacionCampanas(creacion_campana obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.creacion_campana.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_campana;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarCreacionCampanasDetalle(creacion_campana_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.creacion_campana_detalle.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_detalle;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarCreacionCampanasDetalleListados(List<creacion_campana_listas> listas, List<creacion_campana_camposSimples> simples)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    if (listas.Count() > 0)
                    {
                        DaccInsetfull.BulkInsertEntities(listas);
                    }
                    if (simples.Count() > 0)
                    {
                        DaccInsetfull.BulkInsertEntities(simples);
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int insertarRespuestasCamapana(List<campana_respuestas> listaPreguntas, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listaPreguntas);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
                return 0;
            }
        }

        public int IngresarRespuestaCampañaVerificacion_Archivos(campana_respuestas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.campana_respuestas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_respuesta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int insertarRespuestasCampanaVerificaciones(List<campana_respuestas_tipoVerificaciones> listaVerificaciones, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listaVerificaciones);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
                return 0;
            }
        }

        public int insertarRespuestasCampanaArchivos(List<campana_respuestas_tipoArchivo> listaArchivos, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(listaArchivos);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = error;
                return 0;
            }
        }


        public int InsertarLoteCampaña(campana_respuestas_lote lote)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.campana_respuestas_lote.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    return lote.id_lote;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion CAMPAÑAS


        public int InsertarLogCarguesMasivos(log_cargues_masivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_cargues_masivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #region ALERTAS DISPENSACIÓN

        public int CargueAlertasDispensacion(alertas_dispensacion obj, List<alertas_dispensacion_registros> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alertas_dispensacion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_alerta;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_alerta = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }


        public int InsertarRespuestaAlertaDiepnsacion(alertas_dispensacion_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alertas_dispensacion_detalle.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_detalle;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarLogRespuestaAlertaDiepnsacion(log_alertas_dispensacion_detalle obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_alertas_dispensacion_detalle.InsertOnSubmit(obj);
                    //db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarRespuestaAlertaDiepnsacionTramite(List<alertas_dispensacion_detalle_entramite> obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(obj);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarRespuestaAlertaDiepnsacionTramiteRespuestas(List<alertas_dispensacion_detalle_entramite_respuestas> obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(obj);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int InsertarArchivoAlertasDispen(alertas_dispensacion_detalle_archivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.alertas_dispensacion_detalle_archivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_archivo;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }


        #endregion

        #region MORTALIDAD_NATALIDAD

        public int InsercionMortalidadRegistro(mortalidad_registros obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.mortalidad_registros.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_mortalidad;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsercionNatalidadRegistro(natalidad_registros obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.natalidad_registros.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_natalidad;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region EVENTOS_SALUD

        public int CargueEventosSalud(evento_salud_cargue obj, List<eventos_salud_registros> detalle, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.evento_salud_cargue.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    var idBase = obj.id_cargue;

                    if (idBase != 0)
                    {
                        foreach (var item in detalle)
                        {
                            item.id_cargue = idBase;
                        }
                        DaccInsetfull.BulkInsertEntities(detalle);

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return idBase;
                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarEventoSalud(eventos_salud_registros evento)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.eventos_salud_registros.InsertOnSubmit(evento);
                    db.SubmitChanges();
                    return evento.id_evento;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion

        #region ENCUESTA SAMI

        public int InsertarRespuestaSAMI(encuesta_sami dato, List<encuesta_sami_respuestas> detalles, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.encuesta_sami.InsertOnSubmit(dato);
                    db.SubmitChanges();
                    var id_lote = dato.id_lote;

                    if (id_lote != 0)
                    {
                        foreach (var item in detalles)
                        {
                            item.idlote = id_lote;
                        }

                        if (detalles.Count() > 0)
                        {
                            DaccInsetfull.BulkInsertEntities(detalles);
                        }

                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                        return id_lote;

                    }
                    else
                    {
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Error al ingresar los datos de encuesta";
                        return 0;
                    }

                }
            }
            catch (Exception ex)
            {
                var mensajerrror = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        #endregion ENCUESTA SAMI

        #region FIS PRESTADORES

        public int InsertarPrestadorFis(fis_rips_prestadores prestador)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores.InsertOnSubmit(prestador);
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

        public int InsertarSedePrestadorFis(fis_rips_prestadores_sedes sede)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_sedes.InsertOnSubmit(sede);
                    db.SubmitChanges();
                    return sede.id_sede;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarArchivosPrestador(fis_rips_prestadores_archivos archivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_archivos.InsertOnSubmit(archivo);
                    db.SubmitChanges();
                    return archivo.id_archivo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarContratoPrestador(fis_rips_prestadores_contratos contrato)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_contratos.InsertOnSubmit(contrato);
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

        public int GuardarTigaContratoPrestador(fis_rips_prestadores_contrato_tigas tiga)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_contrato_tigas.InsertOnSubmit(tiga);
                    db.SubmitChanges();
                    return tiga.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int insertarCargueTarifas(fis_rips_prestadores_contratos_tarifas obj, List<fis_rips_prestadores_contratos_tarifas_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var actualiza = ActualizarEstadoTarifas(obj.id_contrato);

                    db.fis_rips_prestadores_contratos_tarifas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_masivo != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_masivo = obj.id_masivo;
                        }
                        DaccInsetfull.BulkInsertEntities(lista);
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    return obj.id_masivo;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
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

        public int CrearCups(fis_rips_cups obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cups.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_cups;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarGlosaFacturaFis(fis_rips_facturas_glosa obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_facturas_glosa.InsertOnSubmit(obj);
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

        public int InsertarFacturaFis(fis_rips_facturas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_facturas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_fis_factura;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogFacturaFis(log_fis_rips_facturas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fis_rips_facturas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogGlosaFacturasFis(log_fisFactura_levantarGlosa obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fisFactura_levantarGlosa.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogGlosaFacturasFisMantener(log_fisFactura_mantenerGlosa obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fisFactura_mantenerGlosa.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarPrestadorIpsCuentas(Ref_ips_cuentas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Ref_ips_cuentas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_ref_ips_cuentas;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogEliminarNegociacion(log_fisPrestador_contrato_eliminar obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fisPrestador_contrato_eliminar.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return 0;
            }
        }

        public int GuardarRegistroTarifasRipsFacturas(string cuv, int? idFactura, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_facturasCuv_insercionRegistros(cuv, idFactura, usuario);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int IngresarNuevoRipsFis(fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_registrosCompletos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_registro;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int IngresarLogRipsFis(log_fis_rips_cargue_registrosCompletos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fis_rips_cargue_registrosCompletos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_edicion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarTodoElCargueFisIdFactura(int? idFactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_eliminarTodoCargue(idFactura);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarCargueDetalles(fis_rips_sinJson_lote obj, List<fis_rips_sinJson_detalle> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;

                    db.fis_rips_sinJson_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarCargueIVM(fis_rips_cargueMasivo_ivm_lote obj, List<fis_rips_cargueMasivo_ivm_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargueMasivo_ivm_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarCarguePedidos(fis_rips_cargueMasivo_pedidos_lote obj, List<fis_rips_cargueMasivo_pedidos_registros> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargueMasivo_pedidos_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarFisRipsConsultaMasivo(List<fis_rips_cargue_consulta> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(lista);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int InsertarLogEliminaRegistroFis(log_fis_rips_cargue_registrosCompletos_eliminar obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fis_rips_cargue_registrosCompletos_eliminar.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarDatosGlosaCompleta(int? idFactura, int? concepto_general, int? concepto_especifico, int? concepto_aplicacion, string observacion, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_insertarGlosaCompleta(idFactura, concepto_general, concepto_especifico, concepto_aplicacion, observacion, usuario);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarCIE1OFIS(ref_cie10_fis obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_cie10_fis.InsertOnSubmit(obj);
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

        public int InsertarLoteCIE10(ref_cie10_fis_lote obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_cie10_fis_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_lote;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarCargueCIE10Masivo(ref_cie10_fis_lote obj, List<ref_cie10_fis> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.ref_cie10_fis_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarCargueMasivoContratos(fis_rips_prestadores_contratos_lote obj, List<fis_rips_prestadores_contratos> lista, ref MessageResponseOBJ MsgRes)
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
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarTIGASContratoMasivo(fis_rips_prestadores_contrato_tigas_lote obj, List<fis_rips_prestadores_contrato_tigas> lista, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_prestadores_contrato_tigas_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    if (obj.id_lote != 0)
                    {
                        foreach (var item in lista)
                        {
                            item.id_lote = obj.id_lote;
                        }

                        DaccInsetfull.BulkInsertEntities(lista);
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

        public int InsertarLogCambioContratoFactura(log_fis_rips_prestadores_contratos_actualizaciones obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_fis_rips_prestadores_contratos_actualizaciones.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarNovedadFactura(factura_novedades obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.factura_novedades.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_novedad;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion FIS PRESTADORES

        #region CHATBOT

        public int ChatBotInsertarProceso(chatbot_ref_procesos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.chatbot_ref_procesos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_chatbot_ref_proceso;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ChatBotInsertarSubProceso(chatbot_ref_subprocesos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.chatbot_ref_subprocesos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_chatbot_ref_subproceso;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ChatBotInsertarPreguntas(chatbot_ref_preguntas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.chatbot_ref_preguntas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_chatbot_ref_pregunta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ChatBotInsertarRespuestas(chatbot_ref_respuestas obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.chatbot_ref_respuestas.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_chatbot_ref_respuesta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ChatBotInsertarRespuestasArchivos(chatbot_ref_respuestas_archivos obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.chatbot_ref_respuestas_archivos.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_archivo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }



        #endregion CHATBOT


        #region INSERCION JSONS FACTURAS FIS

        public int GuardarCargueJsonConsultas(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_consulta> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 1).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 1;

                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonHospitalizacion(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_hospitalizacion> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 2).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 2;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonMedicamentos(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_medicamentos> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 3).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 3;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonotrosServicios(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_otros_servicios> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 4).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 4;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonProcedimientos(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_procedimientos> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 5).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 5;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonRecienNacido(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_reciennacido> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 6).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 6;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonTransaccion(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_transaccion> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 7).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 7;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonUrgencias(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_urgencias> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 8).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 8;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int GuardarCargueJsonUsuarios(fis_rips_cargue_LoteConsultas lote, List<fis_rips_cargue_usuarios> lista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<fis_rips_cargue_LoteConsultas> existe = db.fis_rips_cargue_LoteConsultas.Where(x => x.codigo_cuv == lote.codigo_cuv && x.id_factura == lote.id_factura && x.tipo_ingreso == 9).ToList();
                    if (existe != null)
                    {
                        foreach (var item in existe)
                        {
                            item.estado = 0;
                            db.SubmitChanges();
                        }
                    }

                    var id = 0;
                    lote.tipo_ingreso = 9;
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    id = lote.id_cargue;
                    foreach (var item in lista)
                    {
                        item.id_lote = id;
                    }

                    DaccInsetfull.BulkInsertEntities(lista);
                    return id;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionLote(fis_rips_cargue_LoteConsultas lote)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_LoteConsultas.InsertOnSubmit(lote);
                    db.SubmitChanges();
                    return lote.id_cargue;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionTransaccion(fis_rips_cargue_transaccion transa)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_transaccion.InsertOnSubmit(transa);
                    db.SubmitChanges();
                    return transa.id_transaccion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionUsuario(fis_rips_cargue_usuarios usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_usuarios.InsertOnSubmit(usuario);
                    db.SubmitChanges();
                    return usuario.id_usuarios;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionConsulta(fis_rips_cargue_consulta consulta)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_consulta.InsertOnSubmit(consulta);
                    db.SubmitChanges();
                    return consulta.id_consulta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionHospitalizacion(fis_rips_cargue_hospitalizacion hospi)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_hospitalizacion.InsertOnSubmit(hospi);
                    db.SubmitChanges();
                    return hospi.id_hospitalizacion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionProcedimientos(fis_rips_cargue_procedimientos proce)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_procedimientos.InsertOnSubmit(proce);
                    db.SubmitChanges();
                    return proce.id_procedimiento;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionUrgencias(fis_rips_cargue_urgencias urge)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_urgencias.InsertOnSubmit(urge);
                    db.SubmitChanges();
                    return urge.id_urgencias;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionRecienNacido(fis_rips_cargue_reciennacido recien)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_reciennacido.InsertOnSubmit(recien);
                    db.SubmitChanges();
                    return recien.id_nacido;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionMedicamento(fis_rips_cargue_medicamentos medi)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_medicamentos.InsertOnSubmit(medi);
                    db.SubmitChanges();
                    return medi.id_medicamentos;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int FisRipsInsercionOtroServicio(fis_rips_cargue_otros_servicios otro)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.fis_rips_cargue_otros_servicios.InsertOnSubmit(otro);
                    db.SubmitChanges();
                    return otro.id_otros_servicios;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarUsuariosFisMasivo(List<fis_rips_facturas_usuarios> list)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    DaccInsetfull.BulkInsertEntities(list);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int InsertarLogEliminarMasivoDetalles(log_eliminar_cargueMasivo_detalles obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.log_eliminar_cargueMasivo_detalles.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_log;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion INSERCION JSON

        #region VIGILANCIA COHORTES
        public int InsertarGRCP(cargue_vigilanciaCohortes_lote obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_vigilanciaCohortes_lote.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_lote;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void InsertarDatosGrpc(List<cargue_vigilanciaCohortesRegistros_normal> dtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    DaccInsetfull.BulkInsertEntities(dtll);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de insertar los datos: " + ex.Message;
            }
        }

        public void InsertarDatosGrpcEpoc(List<cargue_vigilanciaCohortesRegistros_EPOC> dtll, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    DaccInsetfull.BulkInsertEntities(dtll);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de insertar los datos: " + ex.Message;
            }
        }

        public int InsertarGestionVigiCohorte(cargue_vigilanciaCohortes_registros_gestion obj)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.cargue_vigilanciaCohortes_registros_gestion.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.id_gestion;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion VIGILANCA COHORTES
    }
}


#endregion



