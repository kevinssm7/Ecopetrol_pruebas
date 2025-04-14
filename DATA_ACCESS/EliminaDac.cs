using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANALITICA_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace DATA_ACCESS
{
    public class EliminaDac
    {
        #region EVOLUCION

        public void EliminarConcurrenciaEvolucion(ecop_concurrencia_evolucion ObjEvolucion, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Evolucion = db.ecop_concurrencia_evolucion.Where(s => s.id_evolucion == ObjEvolucion.id_evolucion);
                    db.ecop_concurrencia_evolucion.DeleteAllOnSubmit(Evolucion);
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

        public void EliminarGlosa(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Evolucion = db.md_glosa_detalle.Where(s => s.id_md_glosa_detalle == id);
                    db.md_glosa_detalle.DeleteAllOnSubmit(Evolucion);
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

        #region GESTOR_DOCUMENTAL

        public void EliminarDocumento_med_cal(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Gestion = db.GestionDocumentalMedicamentosCad.Where(s => s.id_gestion_documental__medicamentos_cad == id);
                    db.GestionDocumentalMedicamentosCad.DeleteAllOnSubmit(Gestion);
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

        public int eliminarLoteMedicamentosDispensacion(int lote)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Gestion = db.medicamentos_dispen_lote.Where(s => s.id_lote == lote);
                    db.medicamentos_dispen_lote.DeleteAllOnSubmit(Gestion);
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
        public void EliminarDocumento_med(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Gestion = db.GestionDocumentalMedicamentos.Where(s => s.id_gestion_documental__medicamentos == id);
                    db.GestionDocumentalMedicamentos.DeleteAllOnSubmit(Gestion);
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


        public bool EliminarDocPQRS(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Gestion = db.GestionDocumentalPQRS.Where(s => s.id_gestion_documental_pqrs == id).FirstOrDefault();
                    db.GestionDocumentalPQRS.DeleteOnSubmit(Gestion);
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

        public int EiminarQuienesLlamoGestion(int? idPqr)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var Gestion = db.ecop_pqrs_a_quien_llamo.Where(s => s.id_ecop_pqr == idPqr);
                    db.ecop_pqrs_a_quien_llamo.DeleteAllOnSubmit(Gestion);
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

        #region ANALISIS CASOS 

        public void EliminarPlanAccion(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var PlanAccion = db.ecop_concurrencia_eventos_en_salud_detalle.Where(s => s.id_ecop_concurrencia_eventos_en_salud_detalle == OBJ.id_ecop_concurrencia_eventos_en_salud_detalle);
                    db.ecop_concurrencia_eventos_en_salud_detalle.DeleteAllOnSubmit(PlanAccion);
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

        #region CALIDAD
        public void EliminarCapitulo(int idcapitulo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    capitulos capitulo = db.capitulos.Where(l => l.id_capitulo == idcapitulo).FirstOrDefault();
                    if (capitulo != null)
                    {
                        List<item_capitulo> itemcap = db.item_capitulo.Where(l => l.capitulo_id == capitulo.id_capitulo).ToList();
                        //foreach(var item in itemcap)
                        //{
                        //    item.activo = false;
                        //    item.Aplica = false;
                        //    db.SubmitChanges();
                        //}
                        if (itemcap.Count() > 0)
                        {
                            db.item_capitulo.DeleteAllOnSubmit(itemcap);
                            db.SubmitChanges();
                        }
                        db.capitulos.DeleteOnSubmit(capitulo);
                        db.SubmitChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void EliminarVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            try
            {
                cronograma_visitas visita = db.cronograma_visitas.Where(l => l.id_cronograma_visitas == idvisita).FirstOrDefault();
                if (visita != null)
                {
                    obj.auditor_asignado = visita.Auditor_Asignado;
                    obj.fecha_visita = visita.fecha_visita;
                    obj.prestador = visita.id_prestador;
                    db.log_eliminacion_visitas.InsertOnSubmit(obj);
                    var detalles = db.cronograma_visita_detalle_indicador.Where(l => l.id_cronograma_visitas == idvisita).ToList();
                    if (detalles.Count > 0)
                    {
                        db.cronograma_visita_detalle_indicador.DeleteAllOnSubmit(detalles);
                        db.SubmitChanges();
                    }
                    db.cronograma_visitas.DeleteOnSubmit(visita);
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

        public void EliminarEvaluacionVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            cronograma_visitas visita = db.cronograma_visitas.Where(l => l.id_cronograma_visitas == idvisita).FirstOrDefault();
            try
            {
                obj.auditor_asignado = visita.Auditor_Asignado;
                obj.fecha_visita = visita.fecha_visita;
                obj.prestador = visita.id_prestador;
                db.log_eliminacion_visitas.InsertOnSubmit(obj);
                db.ManagmentEliminarEvaluacionVisita(idvisita);
                db.SubmitChanges();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int EliminarActaVisitasCronogramaId(int? idCronograma, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cronograma_visita_documento obj = db.cronograma_visita_documento.FirstOrDefault(x => x.id_cronograma_visita == idCronograma);
                    db.cronograma_visita_documento.DeleteOnSubmit(obj);
                    db.SubmitChanges();
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

        public int EliminarInformeOperativo(int? idCronograma, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cronograma_visita_informeOperativo obj = db.cronograma_visita_informeOperativo.FirstOrDefault(x => x.id_visita == idCronograma);
                    db.cronograma_visita_informeOperativo.DeleteOnSubmit(obj);
                    db.SubmitChanges();
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

        #region Adherencia 

        public void EliminarCriterioCohorte(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.adh_criterio.Where(s => s.id_adh_criterio == idcriterio).FirstOrDefault();
                    db.adh_criterio.DeleteOnSubmit(criterio);
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

        public void EliminarTipoCriterio(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<adh_criterio> criterios = db.adh_criterio.Where(l => l.id_tipo_criterio == idtipocriterio).ToList();
                    if (criterios.Count > 0)
                    {
                        foreach (var item in criterios)
                        {
                            var resul = db.adh_resultados_detalle.Where(l => l.id_criterio == item.id_adh_criterio).ToList();
                            if (resul.Count > 0)
                            {
                                db.adh_resultados_detalle.DeleteAllOnSubmit(resul);
                                db.SubmitChanges();
                            }

                            db.adh_criterio.DeleteOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                    var tipocriterio = db.ref_adh_tipo_criterio.Where(s => s.id_tipo_criterio == idtipocriterio).FirstOrDefault();
                    db.ref_adh_tipo_criterio.DeleteOnSubmit(tipocriterio);
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

        public void EliminarEgreso(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var egreso = db.egreso_auditoria_Hospitalaria.Where(s => s.id_concurrencia == id);
                    db.egreso_auditoria_Hospitalaria.DeleteAllOnSubmit(egreso);
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

        #region

        public void EliminarPQRS(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ecop_PQRS.Where(s => s.id_ecop_PQRS == id_ecop_PQRS).FirstOrDefault();
                    db.ecop_PQRS.DeleteOnSubmit(criterio);
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

        public int eliminarArchivoPqrsidArchivo(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    GestionDocumentalPQRS2 obj = db.GestionDocumentalPQRS2.Where(x => x.id_gestion_documental_pqrs == id).FirstOrDefault();
                    db.GestionDocumentalPQRS2.DeleteOnSubmit(obj);
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

        #region GESTION INTERNA

        public void EliminarHistoriaclinica(int id_hc_paciente, log_eliminacion_historias_clinicas_odontologia obj, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            try
            {
                //Se eliminan los registros de la tabla odont_historia_clinica_detalle_calidad relacionados a la historia clinica del paciente
                List<odont_historia_clinica_detalle_calidad> list1 = db.odont_historia_clinica_detalle_calidad.Where(l => l.id_odont_historia_clinica_paciente == id_hc_paciente).ToList();
                if (list1.Count > 0)
                {
                    db.odont_historia_clinica_detalle_calidad.DeleteAllOnSubmit(list1);
                    db.SubmitChanges();
                }

                //Se eliminan los registros de la tabla odont_historia_clinica_detalle_contenido relacionados a la historia clinica del paciente
                List<odont_historia_clinica_detalle_contenido> list2 = db.odont_historia_clinica_detalle_contenido.Where(l => l.id_odont_historia_clinica_paciente == id_hc_paciente).ToList();
                if (list2.Count > 0)
                {
                    db.odont_historia_clinica_detalle_contenido.DeleteAllOnSubmit(list2);
                    db.SubmitChanges();
                }

                odont_historia_clinica_paciente hc = db.odont_historia_clinica_paciente.Where(l => l.id_odont_historia_clinica_paciente == id_hc_paciente).FirstOrDefault();
                if (hc != null)
                {
                    obj.id_historia_clinica = hc.id_odont_historia_clinica.Value;
                    obj.id_historia_clinica_paciente = hc.id_odont_historia_clinica_paciente;
                    obj.numero_hc = hc.numero_hc;
                    obj.paciente = hc.paciente;
                    obj.fecha_atencion = hc.fecha_atencion;
                    obj.estado = hc.estado;
                    obj.observaciones = hc.observaciones;
                    db.log_eliminacion_historias_clinicas_odontologia.InsertOnSubmit(obj);
                    db.odont_historia_clinica_paciente.DeleteOnSubmit(hc);
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

        public void EliminarPQRSEnrevision(int id_ecop_PQRS, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ecop_PQRS_enrevision.Where(s => s.id_ecop_PQRS == id_ecop_PQRS).FirstOrDefault();
                    db.ecop_PQRS_enrevision.DeleteOnSubmit(criterio);
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

        public void EliminarBaseBeneficiariosEco(ref MessageResponseOBJ MsgRes)
        {

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_EliminarBeneficiariosAnalitica();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }

        public int EliminarBaseBeneficiariosPeriodo(string periodo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_eliminarCargueBB_periodo(periodo);
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

        #region CONCURRENCIA

        public void EliminarDetallePlan(int id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ecop_plan_mejora_foco_intervencion.Where(s => s.id_plan_mejora_foco_intervencion == id_plan_mejora_foco_intervencion).FirstOrDefault();
                    db.ecop_plan_mejora_foco_intervencion.DeleteOnSubmit(criterio);
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

        public void EliminarDetallePlanTarea(int id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ecop_plan_mejora_tareas.Where(s => s.id_plan_mejora_tareas == id_plan_mejora_tareas).FirstOrDefault();
                    db.ecop_plan_mejora_tareas.DeleteOnSubmit(criterio);
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

        public void EliminarArchivoPM(int? idArchivo, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ecop_plan_de_mejora_documental.Where(s => s.id_archivo == idArchivo).FirstOrDefault();
                    db.ecop_plan_de_mejora_documental.DeleteOnSubmit(criterio);
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

        public int EliminarPlanDeMejora_id(int? idPlan)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_planMejora_eliminarPlanid(idPlan);
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

        #region INSUMOS

        public void EliminarDocumento(calidad_gestor_documental_insumos obj)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            calidad_gestor_documental_insumos obj2 = db.calidad_gestor_documental_insumos.Where(l => l.id_calidad_gestor_documental_insumos == obj.id_calidad_gestor_documental_insumos).FirstOrDefault();
            if (obj2 != null)
            {
                db.calidad_gestor_documental_insumos.DeleteOnSubmit(obj2);
                db.SubmitChanges();
            }

        }

        public void EliminarArchivoZipQuejasValidas(calidad_quejas_validas_base_zip obj)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            calidad_quejas_validas_base_zip obj2 = db.calidad_quejas_validas_base_zip.Where(l => l.id_calidad_quejas_validas_base_zip == obj.id_calidad_quejas_validas_base_zip).FirstOrDefault();
            if (obj2 != null)
            {
                db.calidad_quejas_validas_base_zip.DeleteOnSubmit(obj2);
                db.SubmitChanges();
            }
        }

        #endregion

        #region verificacion

        public void EliminarTipoCriteriover(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<ver_criterio> criterios = db.ver_criterio.Where(l => l.id_tipo_criterio == idtipocriterio).ToList();
                    if (criterios.Count > 0)
                    {
                        foreach (var item in criterios)
                        {
                            //var resul = db.resultado adh_resultados_detalle.Where(l => l.id_criterio == item.id_adh_criterio).ToList();
                            //if (resul.Count > 0)
                            //{
                            //    db.adh_resultados_detalle.DeleteAllOnSubmit(resul);
                            //    db.SubmitChanges();
                            //}

                            db.ver_criterio.DeleteOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                    var tipocriterio = db.ref_ver_tipoCriterio.Where(s => s.id_tipo_criterio == idtipocriterio).FirstOrDefault();
                    db.ref_ver_tipoCriterio.DeleteOnSubmit(tipocriterio);
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

        public void EliminarCriterioVerificacion(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ver_criterio.Where(s => s.id_ver_criterio == idcriterio).FirstOrDefault();
                    db.ver_criterio.DeleteOnSubmit(criterio);
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

        public void EliminarCarguePrefactura(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.md_prefacturas_cargue_base.Where(s => s.id_cargue_base == idCargue).FirstOrDefault();
                    db.md_prefacturas_cargue_base.DeleteOnSubmit(criterio);
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

        public void EliminarCargueLUPE(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.md_Lupe_cargue_base.Where(s => s.id_lupe_cargue_base == idCargue).FirstOrDefault();
                    db.md_Lupe_cargue_base.DeleteOnSubmit(criterio);
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

        public void EliminarMedicamentosRegulados(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.md_medicamentos_regulados.Where(s => s.id_med == idCargue).FirstOrDefault();
                    db.md_medicamentos_regulados.DeleteOnSubmit(criterio);
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

        public int EliminarLupe(int idLupe, string usuarioElimina)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_lupe_eliminarCargue(idLupe, usuarioElimina);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarMedicamentosRegulados(int idMed, string usuarioElimina)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_prefacturas_eliminarMedicamentosRegulados(idMed, usuarioElimina);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public void EliminarCargueDispen(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.medicamentos_dispen_cargue.Where(s => s.id_cargue == idCargue).FirstOrDefault();
                    db.medicamentos_dispen_cargue.DeleteOnSubmit(criterio);
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

        public void EliminarCargueDispendtll(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.medicamentos_dispen_calidad.Where(s => s.id_cargue == idCargue).FirstOrDefault();
                    db.medicamentos_dispen_calidad.DeleteOnSubmit(criterio);
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

        #region correosPPE

        public int eliminarCorreosPPE(ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_eliminarRegistros_COrreosPpe();
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

        #endregion

        #region GASTO POR SERVICIO

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha 04/08/2022
        /// Metodo para elminar el cargue de gastos por servicio
        /// </summary>
        /// <param name="idCargue"></param>
        public int EliminarCargueGastoPorServicio(int idCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    db.Management_eliminar_cargue_gastosxservicio(idCargue);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarTotalEvaluacion(int idEvaluacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_evaluacionVisitas_eliminarTotales(idEvaluacion);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public int EliminarTotalEvaluacionArchivos(int idEvaluacion, int? tipoCriterio, int? idVerificacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_evaluacionVisitas_eliminarTotales_archivos(idEvaluacion, tipoCriterio, idVerificacion);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarEvaluacionVisitasAutoguardado(int idEvaluacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_evaluacionVisitas_eliminarAutoGuardada(idEvaluacion);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarArchivosPDFevaluacionDispensacion(int idEvaluacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.ver_evaluacion_pdfs.Where(s => s.id_evaluacion == idEvaluacion).ToList();

                    foreach (var item in criterio)
                    {
                        db.ver_evaluacion_pdfs.DeleteOnSubmit(item);
                        db.SubmitChanges();
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

        public int EliminarArchivosEvaluacionVisitas(int idEvaluacion, int idArchivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var egreso = db.ver_evaluacion_archivos.Where(s => s.id_evaluacion == idEvaluacion && s.id_img == idArchivo);
                    db.ver_evaluacion_archivos.DeleteAllOnSubmit(egreso);
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

        public int EliminarLoteFacturas(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_actualizarRegional_facturas(id);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarCarguePrefacturasCompleto(int idCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    db.management_prefacturas_eliminarCargue(idCargue);
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

        #region inventarioAltoCosto
        public int eliminarArchivoAltoCostoCanceridArchivo(int idArchivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    inventario_altoCosto_archivos obj = db.inventario_altoCosto_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                    db.inventario_altoCosto_archivos.DeleteOnSubmit(obj);
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

        public int eliminarDatosCuentasAltoCosto(int idCargue, int? tipo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_cuentasAltoCosto_eliminarCargues(idCargue, tipo);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int eliminarArchivoRepositorioAltoCosto(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_cuentas_altoCosto_archivos obj = db.cargue_cuentas_altoCosto_archivos.Where(x => x.id_archivo == id).FirstOrDefault();
                    db.cargue_cuentas_altoCosto_archivos.DeleteOnSubmit(obj);
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

        public int eliminarObservacionAltoCosto(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_cuentas_altoCosto_observaciones obj = db.cargue_cuentas_altoCosto_observaciones.Where(x => x.id_observacion == id).FirstOrDefault();
                    db.cargue_cuentas_altoCosto_observaciones.DeleteOnSubmit(obj);
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

        #region RIPS

        public int EliminarCargueRipsId(int? idRips)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_rips_eliminarCargue(idRips);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion RIPS

        #region Entregables

        public int eliminarEvaluacioEntregablesID(int? idPeriodo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    seguimiento_entregables_periodo_eval_calidad obj = db.seguimiento_entregables_periodo_eval_calidad.Where(x => x.seguimiento_entregables_periodo_id == idPeriodo).OrderByDescending(x => x.id_seguimiento_entregables_periodo_eval_calidad).FirstOrDefault();
                    db.seguimiento_entregables_periodo_eval_calidad.DeleteOnSubmit(obj);
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

        public int eliminarFelicitacionesEntregablesID(int? idPeriodo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    seguimiento_entregables_periodo_felicitaciones obj = db.seguimiento_entregables_periodo_felicitaciones.Where(x => x.id_seguimeinto_entregable_periodo == idPeriodo).OrderByDescending(x => x.id_seguimiento_entregables_periodo_felicitaciones).FirstOrDefault();
                    db.seguimiento_entregables_periodo_felicitaciones.DeleteOnSubmit(obj);
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

        #region CARGUE MASIVO RIPS DEPURADOS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo Para eliminar el cargue base de un registro de cargue rips depurados mediante su ID
        /// </summary>
        /// <param name="idCargueBase"></param>
        public void EliminarRipsDepuradosCargueBasePorId(int idCargueBase)
        {
            using (ANALITICA_DataContexDataContext db = new ANALITICA_DataContexDataContext())
            {
                var carguebase = db.rips_depurados_carguebase.Where(l => l.id_cargue_base == idCargueBase).FirstOrDefault();
                if (carguebase != null)
                {
                    db.rips_depurados_carguebase.DeleteOnSubmit(carguebase);
                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Reembolsos

        public int EliminarArchivoReembolsos(int? idArchivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cuentas_reembolsos_archivos arch = db.cuentas_reembolsos_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                    db.cuentas_reembolsos_archivos.DeleteOnSubmit(arch);
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

        #endregion Reembolsos

        #region
        public int EliminarCasoNoRips(int? idMed)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cuentas_medicas_norips arch = db.cuentas_medicas_norips.Where(x => x.id_med == idMed).FirstOrDefault();
                    db.cuentas_medicas_norips.DeleteOnSubmit(arch);
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

        #region CIERRE CONTABLE

        public int EliminarCargueCierreContable(int idCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.Management_eliminar_cargue_cierreContable(idCargue);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        #endregion CIERRE CONTABLE

        #region FIS PRESTADOR

        public int EliminarSedePrestadores(int? idSede)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_sedes datos = db.fis_rips_prestadores_sedes.Where(x => x.id_sede == idSede).FirstOrDefault();
                    db.fis_rips_prestadores_sedes.DeleteOnSubmit(datos);
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

        public int EliminarArchivoPrestador(int? idArchivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_archivos datos = db.fis_rips_prestadores_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                    db.fis_rips_prestadores_archivos.DeleteOnSubmit(datos);
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

        public int EliminarNegociacionContrato(int? idMasivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_contratos_tarifas datos = db.fis_rips_prestadores_contratos_tarifas.Where(x => x.id_masivo == idMasivo).FirstOrDefault();
                    db.fis_rips_prestadores_contratos_tarifas.DeleteOnSubmit(datos);
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

        public int EliminarTigaContratosPrestadores(int? idTiga)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_contrato_tigas datos = db.fis_rips_prestadores_contrato_tigas.Where(x => x.id_registro == idTiga).FirstOrDefault();
                    db.fis_rips_prestadores_contrato_tigas.DeleteOnSubmit(datos);
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

        public int EliminarDatosRipsFisFacturasIdFactura(int? idFactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_facturasRips_eliminarIdFactura(idFactura);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarCupsFis(int? idCups)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cups dato = db.fis_rips_cups.FirstOrDefault(x => x.id_cups == idCups);
                    db.fis_rips_cups.DeleteOnSubmit(dato);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int EliminarGlosaFactura(int? idGlosa)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_facturas_glosa dato = db.fis_rips_facturas_glosa.FirstOrDefault(x => x.id_glosa == idGlosa);
                    db.fis_rips_facturas_glosa.DeleteOnSubmit(dato);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int EliminarRegistroRipsFis(int? idRegistro)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_cargue_registrosCompletos dato = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == idRegistro);
                    db.fis_rips_cargue_registrosCompletos.DeleteOnSubmit(dato);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int EliminarCargueMasivoDetalles(int? idCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 1200;
                    db.management_eliminarCargueMasivo_detalles(idCargue);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarCargueMasivoDetallesSinJson(int? idCargue)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 1200;
                    db.management_fis_eliminarDetalles_sinJson(idCargue);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarRegistroFis(int? id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_facturasCuv_EliminarRegistros(id);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarDetallesIdFactura(int? idFactura)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_eliminarDetalles_idFactura(idFactura);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int EliminarSinJsonIdFactura(int? idFac)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_elminarDetalles_idFactura(idFac);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        public int ElininarCIE10FIS(string codigo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_cie10_fis dato = db.ref_cie10_fis.FirstOrDefault(x => x.codigo == codigo);
                    db.ref_cie10_fis.DeleteOnSubmit(dato);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DevolverNroPedidoFacturaId(int? idAf, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_fis_devolverEstadoNroPedido(idAf, usuario);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }

        }

        public int EliminarTodoCargueFIS(int? idFactura)
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

        public int EliminarNovedadFactura(int? idNovedad)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    factura_novedades dato = db.factura_novedades.FirstOrDefault(x => x.id_novedad == idNovedad);
                    db.factura_novedades.DeleteOnSubmit(dato);
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion FIS PRESTADOR
        #region CENSO

        public int EliminarDetalleAHCenso(int? idDetalle)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    cargue_censo_ah_registros_detalle datos = db.cargue_censo_ah_registros_detalle.Where(x => x.id_detalle == idDetalle).FirstOrDefault();
                    db.cargue_censo_ah_registros_detalle.DeleteOnSubmit(datos);
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

        public int EliminarArchivoEpidemiologico(int? idArchivo)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    alerta_epidemiologica_gestion_archivos datos = db.alerta_epidemiologica_gestion_archivos.Where(x => x.id_registro == idArchivo).FirstOrDefault();
                    db.alerta_epidemiologica_gestion_archivos.DeleteOnSubmit(datos);
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


        public int EliminarDemorasEpidemiologicas(int? idGestion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_alertasEpidemiologicas_eliminarDemoras_automatico(idGestion);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }

        #endregion CENSO

        #region COHORTE
        public void EliminarCargueCohorte(int? idCargue, int? tipo, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    var criterio = db.cargue_vigilanciaCohortes_lote.Where(s => s.id_lote == idCargue).FirstOrDefault();
                    db.cargue_vigilanciaCohortes_lote.DeleteOnSubmit(criterio);
                    db.SubmitChanges();

                    if (tipo == 4)
                    {
                        var epoc = db.cargue_vigilanciaCohortesRegistros_EPOC.Where(x => x.id_lote == idCargue).ToList();
                        db.cargue_vigilanciaCohortesRegistros_EPOC.DeleteAllOnSubmit(epoc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        var normal = db.cargue_vigilanciaCohortesRegistros_normal.Where(x => x.id_lote == idCargue).ToList();
                        db.cargue_vigilanciaCohortesRegistros_normal.DeleteAllOnSubmit(normal);
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


        #endregion COHORTE

        #region Mortalidad_Natalidad
        public int EliminarMOrtalidadId(int? idMortalidad, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_mortalidad_eliminarRegistro_id(idMortalidad, usuario);
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



        public int EliminarNatalidadId(int? idNatalidad, string usuario)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.management_natalidad_eliminarRegistros_id(idNatalidad, usuario);
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


        #endregion Mortalidad_Natalidad
    }
}
