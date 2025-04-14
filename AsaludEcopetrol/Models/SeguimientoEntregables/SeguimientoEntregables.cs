using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsaludEcopetrol.Models.SeguimientoEntregables
{
    public class SeguimientoEntregables
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

        public seguimiento_entregables GetSeguimientoEntregable(int id)
        {
            return BusClass.GetSeguimientoEntregable(id);
        }

        public List<seguimiento_entregables> GetListSeguimientoEntregable()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_entregables.ToList();
        }

        public List<seguimiento_entregables_periodo> GetListEntregablesPeriodo(int id_seg_entregable)
        {
            return BusClass.GetListEntregablesPeriodo(id_seg_entregable);
        }

        public seguimiento_entregables_periodo GetEntregablePeriodoById(int id_ent_periodo)
        {
            return BusClass.GetEntregablePeriodoById(id_ent_periodo);
        }

        public List<ref_periodicidad_entregables> GetListPeriodicidadEntregables()
        {
            return BusClass.GetListPeriodicidadEntregables();
        }

        public void InsertarOActualizarSeguimientoEntregable(seguimiento_entregables obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarOActualizarSeguimientoEntregable(obj, ref MsgRes);
        }


        public void InsertarSeguimientoEntregableDTLL(int id_seg_entregable, seguimiento_dtll_entrega obj, List<seguimiento_entregables_documentos> Objdocumentos, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarSeguimientoEntregableDTLL(id_seg_entregable, obj, Objdocumentos, ref MsgRes);
        }

        public Int32 InsertarSeguimientoEntregableDTLL1(int id_seg_entregable, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarSeguimientoEntregableDTLL1(id_seg_entregable, obj, ref MsgRes);
        }

        public void InsertarSeguimientoEntregableDTLL2(List<seguimiento_entregables_documentos> Objdocumentos, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarSeguimientoEntregableDTLL2(Objdocumentos, ref MsgRes);
        }
        public List<vw_seguimiento_entregables> GetListEntregables(int? periodicidad)
        {
            return BusClass.GetListEntregables(periodicidad);
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntrega(int id_dtll)
        {
            return BusClass.GetseguimientoDtllEntrega(id_dtll);
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntregaPresentado(int? id_dtll)
        {
            return BusClass.GetseguimientoDtllEntregaPresentado(id_dtll);
        }

        public List<seguimiento_dtll_entrega> GetListseguimientoDtllEntrega(int id_seg_periodo)
        {
            return BusClass.GetListseguimientoDtllEntrega(id_seg_periodo);
        }


        public List<seguimiento_entregables_documentos> GetSeguimientoEntregableDocs(int id)
        {
            return BusClass.GetSeguimientoEntregableDocs(id);
        }

        public seguimiento_entregables_documentos traerDocumentoEntregableId(int idDocumento)
        {
            return BusClass.traerDocumentoEntregableId(idDocumento);
        }
        public List<managmentSeguimiento_entregables_documentosResult> GetSeguimientoEntregableDocs2(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSeguimientoEntregableDocs2(ref MsgRes);
        }
        public void InsertarSeguimientoEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarSeguimientoEntregablePeriodo(obj, ref MsgRes);
        }

        public void InsertarGestionEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarGestionEntregable(id_seg_entregable_periodo, obj, ref MsgRes);
        }

        public void ActualizarEntregable(int id_seg_entregable_periodo, seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarEntregable(id_seg_entregable_periodo, obj, ref MsgRes);
        }

        public void GuardarRespuestaObservaciones(seguimiento_dtll_entrega obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.GuardarRespuestaObservaciones(obj, ref MsgRes);
        }


        public int InsertarEntregableInicial(int id_seg_entregable, ref MessageResponseOBJ MsgRes)
        {
            int idPeriodoEntregable = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    seguimiento_entregables seg = GetSeguimientoEntregable(id_seg_entregable);

                    /*Insertar el primer periodo a entregar*/
                    seguimiento_entregables_periodo obj = new seguimiento_entregables_periodo();
                    obj.id_seg_entregable = seg.id_seg_entregables;
                    obj.fecha_limite = seg.fecha_control.Value;
                    obj.fecha_entrega = DateTime.Now;
                    obj.id_estado_entregable = 2;
                    obj.tiene_ampliacion = false;
                    db.seguimiento_entregables_periodo.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    idPeriodoEntregable = obj.id_seg_entregable_periodo;

                    /*Agregar proximo periodo a entregar*/
                    obj = new seguimiento_entregables_periodo();
                    obj.id_seg_entregable = seg.id_seg_entregables;
                    obj.fecha_limite = CalcularFechaLimite(seg.fecha_control.Value, seg.periodicidad_entrega);
                    obj.fecha_entrega = null;
                    obj.id_estado_entregable = 1;
                    db.seguimiento_entregables_periodo.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    return idPeriodoEntregable;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public void InsertarProximoEntregable(int id_seg_periodo, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                DateTime fechalimite = new DateTime();
                seguimiento_entregables_periodo segent_periodo = GetEntregablePeriodoById(id_seg_periodo);
                List<seguimiento_entregables_periodo> SegEntregables_periodo = GetListEntregablesPeriodo(segent_periodo.id_seg_entregable);

                fechalimite = CalcularFechaLimite(segent_periodo.fecha_limite, segent_periodo.seguimiento_entregables.periodicidad_entrega);

                var PeriodoEncontrado = SegEntregables_periodo.Where(l => l.fecha_limite.ToString("dd/MM/yyyy") == fechalimite.ToString("dd/MM/yyyy")).FirstOrDefault();
                if (PeriodoEncontrado == null)
                {
                    seguimiento_entregables_periodo obj = new seguimiento_entregables_periodo();
                    obj.id_seg_entregable = segent_periodo.id_seg_entregable;
                    obj.fecha_limite = fechalimite;
                    obj.fecha_entrega = null;
                    obj.id_estado_entregable = 1;
                    obj.tiene_ampliacion = null;
                    InsertarSeguimientoEntregablePeriodo(obj, ref MsgRes);
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
        }


        public int InsertarPeriodoEntregable(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPeriodoEntregable(obj, ref MsgRes);
        }

        public int InsertarSolicitudAmpliacion(int? id_seg_periodo, int id_seg_entregable, string txtSolicitudRealizada, DateTime fecha_limite)
        {
            seguimiento_entregables seg = GetSeguimientoEntregable(id_seg_entregable);
            seguimiento_entregables_periodo obj = new seguimiento_entregables_periodo();
            if (id_seg_periodo != null)
            {
                obj = GetEntregablePeriodoById(id_seg_periodo.Value);
            }
            obj.id_seg_entregable = seg.id_seg_entregables;
            obj.fecha_limite = seg.fecha_control.Value;
            obj.id_estado_entregable = 6;
            obj.fecha_limite_ampliacion = fecha_limite;
            obj.solicitud_ampliacion_realizada_por = txtSolicitudRealizada;
            if (id_seg_periodo != null)
            {
                return ActualizarEntregablePeriodo(obj, ref MsgRes);
            }
            else
            {
                obj.tiene_ampliacion = false;
                return InsertarPeriodoEntregable(obj, ref MsgRes);
            }
        }

        public List<ref_cobertura_seguimiento_entregable> GetCoberturaSegEntregable()
        {
            return BusClass.GetCoberturaSegEntregable();
        }

        private DateTime CalcularFechaLimite(DateTime fecha, int periodicidad)
        {
            DateTime fechaLimite = new DateTime();

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                switch (periodicidad)
                {
                    case 1:
                        fechaLimite = db.fc_dias_habiles(fecha, 1).Value;
                        break;
                    case 2:
                        fechaLimite = db.fc_dias_habiles(fecha,30).Value;
                        break;
                    case 4:
                        fechaLimite = db.fc_dias_habiles(fecha, 90).Value;
                        break;
                    case 5:
                        fechaLimite = db.fc_dias_habiles(fecha, 180).Value;
                        break;
                    case 6:
                        fechaLimite = db.fc_dias_habiles(fecha, 8).Value;
                        break;
                    case 7:
                        fechaLimite = db.fc_dias_habiles(fecha, 15).Value;
                        break;
                    case 8:
                        fechaLimite = db.fc_dias_habiles(fecha, 60).Value;
                        break;
                    case 9:
                        fechaLimite = db.fc_dias_habiles(fecha, 365).Value;
                        break;
                }

            }
            return fechaLimite;
        }

        public string nomdocumento(int id_seg, int tipodoc, string ext)
        {
            Guid al = Guid.NewGuid();
            string guid = al.ToString().Substring(0, 5);
            string filename = "";
            string date = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "");
            filename = guid + "_" + tipodoc + "_" + id_seg + "_" + date + ext;
            return filename;
        }

        public List<ref_seguimiento_entregable_usuario_gestion> GetUsuariosSegGestion()
        {
            return BusClass.GetUsuariosSegGestion();
        }

        public int ActualizarEntregablePeriodo(seguimiento_entregables_periodo obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarEntregablePeriodo(obj, ref MsgRes);
        }

        public List<vw_seguimiento_entregables_competencias> GetSeguimientoEntregablesCompetencias()
        {
            return BusClass.GetSeguimientoEntregablesCompetencias();
        }

        public List<ref_proceso_entregable> Getprocesoentregable()
        {
            return BusClass.Getprocesoentregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Casstillo
        /// Fecha: 27/diciembre/2022
        /// Metodo para obtener el listado de tipo de seguimiento entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_seguimiento_entregables_tipo_entregable> GetListTipoEntregable()
        {
            return BusClass.GetListTipoEntregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo para obtener el listado de estado de entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_estado_entregable> GetListEstadoEntregable()
        {
            return BusClass.GetListEstadoEntregable();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Catillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo utilizado para consultar el listado de alertas enviadas
        /// 
        /// </summary>
        /// <returns></returns>
        public List<seguimiento_entregables_alerta_diaria> GetListNotificacionesEnviadasSeguimientoEntregables(DateTime? fecha)
        {
            return BusClass.GetListNotificacionesEnviadasSeguimientoEntregables(fecha);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de entregables para mostrar en el tablero de gestion 
        /// </summary>
        /// <param name="periodicidad"></param>
        /// <param name="tipoEntregable"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_gestionResult> GetListSeguimientoEntregableGestion(int? periodicidad, int? tipoEntregable)
        {
            return BusClass.GetListSeguimientoEntregableGestion(periodicidad, tipoEntregable);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de periodos entregados y por entregar de un entregable
        /// </summary>
        /// <param name="idSeguimientoEntregable"></param>
        /// <returns></returns>
        public List<vw_seguimiento_entregables> GetListEntregablesPorIdEntregable(int? idSeguimientoEntregable)
        {
            return BusClass.GetListEntregablesPorIdEntregable(idSeguimientoEntregable);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 11 de enero de 2023
        /// Metodo para guardar los datos de la evaluacion de calidad realizada a un entregable.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarDatosEvalCalidadSegEntregable(seguimiento_entregables_periodo_eval_calidad obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.GuardarDatosEvalCalidadSegEntregable(obj, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo utilizado para retornar los datos ingresados en la evaluacion de calidad de un entregable filtrado por el id periodo entregable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public seguimiento_entregables_periodo_eval_calidad ConsultarEvaluacionPorIdPeriodoSegEntregable(int id)
        {
            return BusClass.ConsultarEvaluacionPorIdPeriodoSegEntregable(id);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 13 de enero de 2023
        /// Metodo donde se consulta la informacion de los entregables pendientes por presentar y se setean en la clase event.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="rol"></param>
        /// <returns></returns>
        public List<Event> SetearEntregablesEnEvent(string usuario, string rol)
        {
            List<Event> eventos = new List<Event>();
            List<vw_seguimiento_entregables> entregables = new List<vw_seguimiento_entregables>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                if (rol != "1")
                {
                    entregables = db.vw_seguimiento_entregables.Where(l => (l.id_estado_entregable != 2 && l.id_estado_entregable != 3) && l.persona_responsable == usuario).ToList();
                }
                else
                {
                    entregables = db.vw_seguimiento_entregables.Where(l => (l.id_estado_entregable != 2 && l.id_estado_entregable != 3)).ToList();
                }
            }

            if (entregables.Count > 0)
            {
                foreach (vw_seguimiento_entregables item in entregables)
                {
                    Event obj = new Event();

                    DateTime fechainicio = item.fecha_entrega_validada.Value;
                    DateTime fechafinal = item.fecha_entrega_validada.Value;

                    obj.EventID = item.id_seg_entregables;
                    obj.SubEventID = item.id_seg_entregable_periodo;
                    obj.Subject = item.Nom_entregable;
                    obj.Description = item.Proceso;
                    obj.Start = fechainicio;
                    obj.End = fechafinal;
                    obj.IsFullDay = true;
                    if (item.id_tipo_entregable == 1)
                    {
                        obj.ThemeColor = "#428bca";
                        obj.TextColor = "#fff";
                        obj.BorderColor = "#fff";
                    }
                    else
                    {
                        obj.ThemeColor = "#D9DBDE";
                        obj.TextColor = "#636363";
                        obj.BorderColor = "#636363";
                    }

                    //if (item.tiene_ampliacion != null)
                    //{
                    //    if (item.tiene_ampliacion.Value)
                    //    {
                    //        obj.ThemeColor = "#d9edf7";
                    //    }
                    //    else
                    //    {
                    //        if (item.diferencia_dias > 2)
                    //        {
                    //            obj.ThemeColor = "#dff0d8";
                    //            obj.TextColor = "#80D651";
                    //            obj.BorderColor = "#80D651";
                    //        }
                    //        else if (item.diferencia_dias <= 2 && item.diferencia_dias >= 1)
                    //        {
                    //            obj.ThemeColor = "#fcf8e3";
                    //            obj.TextColor = "#FEAF20";
                    //            obj.BorderColor = "#FEAF20";
                    //        }
                    //        else
                    //        {
                    //            obj.ThemeColor = "#f2dede";
                    //            obj.TextColor = "#d73814";
                    //            obj.BorderColor = "#d73814";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (item.diferencia_dias > 2)
                    //    {
                    //        obj.ThemeColor = "#dff0d8";
                    //        obj.TextColor = "#80D651";
                    //        obj.BorderColor = "#80D651";
                    //    }
                    //    else if (item.diferencia_dias <= 2 && item.diferencia_dias >= 1)
                    //    {
                    //        obj.ThemeColor = "#fcf8e3";
                    //        obj.TextColor = "#FEAF20";
                    //        obj.BorderColor = "#FEAF20";
                    //    }
                    //    else
                    //    {
                    //        obj.ThemeColor = "#f2dede";
                    //        obj.TextColor = "#d73814";
                    //        obj.BorderColor = "#d73814";
                    //    }
                    //}

                    eventos.Add(obj);
                }
            }

            return eventos;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 16 de enero de 2022
        /// Metodo para obtener el listado de oportunidad  de seguimiento entregables
        /// </summary>
        /// <param name="personaResponsable"></param>
        /// <param name="tipoEntregable"></param>
        /// <param name="periodicidad"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_indicadoresResult> GetListadoOportunidadSeguimientoEntregables(string personaResponsable, int? tipoEntregable, int? periodicidad, int? año)
        {
            return BusClass.GetListadoOportunidadSeguimientoEntregables(personaResponsable, tipoEntregable, periodicidad, año);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 18 de enero de 2022
        /// Metodo para obtener los responsables de los entregables desde ecopetrol (proyecto)
        /// </summary>
        /// <returns></returns>
        public List<vw_seguimiento_entregables_responsables> GetResponsablesList()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_entregables_responsables.ToList();
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXPersonaResult> GetListadoIndicadoresXPersonaSeguimientoEntregables(int mesInicial, int mesFinal, int año, string responsable)
        {
            return BusClass.GetListadoIndicadoresXPersonaSeguimientoEntregables(mesInicial, mesFinal, año, responsable);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXComponenteResult> GetListadoIndicadoresXComponenteSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso)
        {
            return BusClass.GetListadoIndicadoresXComponenteSeguimientoEntregables(mesInicial, mesFinal, año, idProceso);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXCompyPeridicidadResult> GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso, int? idPeriodicidad)
        {
            return BusClass.GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(mesInicial, mesFinal, año, idProceso, idPeriodicidad);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarDatosFelicitaciones(seguimiento_entregables_periodo_felicitaciones obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.seguimiento_entregables_periodo_felicitaciones.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Se han ingresado las felicitaciones de manera correcta.";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de procesar la solicitud: " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public seguimiento_entregables_periodo_felicitaciones GetFelicitacionesByIdPeriodo(int id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.seguimiento_entregables_periodo_felicitaciones.Where(l => l.id_seguimeinto_entregable_periodo == id).FirstOrDefault();
            }

        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 3 de febrero de 2023
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="idProceso"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorVencimientoResult> GetIndicadorDiasVencimientSegEntregables(string responsable, int? idProceso, int? año)
        {
            return BusClass.GetIndicadorDiasVencimientSegEntregables(responsable, idProceso, año);
        }
        #endregion
    }
}