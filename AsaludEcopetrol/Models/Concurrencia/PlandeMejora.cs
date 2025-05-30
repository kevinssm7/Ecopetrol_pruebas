using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Concurrencia
{
    public class PlandeMejora
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

        public int? id_plan_de_mejora { get; set; }

        public int? id_plan_mejora_tareas { get; set; }

        public int? id_visitas { get; set; }

        public int? idFoco { get; set; }

        public int id_regional { get; set; }

        public int id_prioridad { get; set; }

        public int tipoPrioridad { get; set; }


        public int id_medicion { get; set; }
        public int id_noCalidad { get; set; }
        public int id_cobertura { get; set; }
        public decimal puntajeFinal { get; set; }


        public int id_unis { get; set; }
        public int id_localidad { get; set; }

        public int id_prestador { get; set; }

        public int id_mes { get; set; }

        public int id_trimestre { get; set; }


        public string año { get; set; }

        public int id_proceso { get; set; }

        public int estado_plan { get; set; }

        public String usuario_ingreso { get; set; }

        public DateTime? fecha_plazo { get; set; }

        public DateTime? fecha_ingreso { get; set; }

        public int id_plan_mejora_foco_intervencion { get; set; }

        public int id_categoria { get; set; }

        public int id_foco_gestion { get; set; }

        public String hallazgo { get; set; }

        public String tarea { get; set; }

        public String responsable { get; set; }

        public DateTime? fecha_seguimiento { get; set; }

        public int id_estado_tarea { get; set; }

        public DateTime? fecha_gestion_tarea { get; set; }
        public DateTime? fecha_creacion { get; set; }

        public DateTime? fecha_seguimiento_accion { get; set; }

        public String usuario_gestiona { get; set; }

        public String observacion { get; set; }
        public String descripcion_problema { get; set; }

        public String porque1 { get; set; }
        public String porque2 { get; set; }
        public String porque3 { get; set; }
        public String porque4 { get; set; }
        public String porque5 { get; set; }
        public int? idAccion { get; set; }

        public int tipo_prioridad { get; set; }

        #endregion
        public List<ref_trimeste> GetRefTrimestre()
        {
            return BusClass.GetRefTrimestre();
        }
        public List<Ref_plan_mejora_categoria> GetRefplan_mejora_categoria()
        {
            return BusClass.GetRefplan_mejora_categoria();
        }
        public List<Ref_plan_mejora_foco> GetRef_plan_mejora_foco()
        {
            return BusClass.GetRef_plan_mejora_foco();
        }
        public List<Ref_proceso_auditado> GetRef_proceso_auditado()
        {
            return BusClass.GetRef_proceso_auditado();
        }
        public List<management_planmejora_focoResult> Cuentadetallefoco(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Cuentadetallefoco(id_plan_de_mejora, ref MsgRes);
        }
        public Int32 InsertarPlanMejora(ecop_plan_de_mejora obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejora(obj, ref MsgRes);
        }
        public Int32 InsertarPlanMejorafoco(ecop_plan_mejora_foco_intervencion obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejorafoco(obj, ref MsgRes);
        }
        public void EliminarDetallePlan(int id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarDetallePlan(id_plan_mejora_foco_intervencion, ref MsgRes);
        }

        public List<management_planmejora_tareaResult> CuentadetalleTarea(Int32 id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentadetalleTarea(id_plan_mejora_foco_intervencion, ref MsgRes);
        }
        public Int32 InsertarPlanMejoratarea(ecop_plan_mejora_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejoratarea(obj, ref MsgRes);
        }

        public void EliminarDetallePlanTarea(int id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarDetallePlanTarea(id_plan_mejora_tareas, ref MsgRes);
        }
        public void ActualizarPlanEgreso(int id_plan_mejora, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarPlanEgreso(id_plan_mejora, estado, ref MsgRes);
        }
        public List<management_planmejora_tarea_controlResult> CuentadetalleTareacontrol(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentadetalleTareacontrol(id_plan_mejora, ref MsgRes);
        }

        public List<vw_ref_regiona_ciudad> Ref_regional_ciudad()
        {
            return BusClass.Ref_regional_ciudad();
        }

        public List<management_plan_mejora_tableroResult> PlanTableroGeneral()
        {
            return BusClass.PlanTableroGeneral();
        }

        public List<management_planMejora_reporteResult> DatosPMReporte(int? idPlan)
        {
            return BusClass.DatosPMReporte(idPlan);
        }
        public List<management_plan_mejora_tablero_dtllResult> PlanTableroGeneralDtll(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.PlanTableroGeneralDtll(id_plan_de_mejora, ref MsgRes);
        }
        public List<Ref_plan_mejora_estado_tarea> estadoTarea()
        {
            return BusClass.estadoTarea();
        }
        public List<management_planmejora_tarea_obsResult> GetobsTareas(Int32 id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetobsTareas(id_plan_mejora_tareas, ref MsgRes);
        }

        public void Actualizarplan_estado_tarea(int id_plan_mejora_tareas, Int32 estado, ref MessageResponseOBJ MsgRes)
        {
            BusClass.Actualizarplan_estado_tarea(id_plan_mejora_tareas, estado, ref MsgRes);
        }


        public Int32 InsertarPlanMejora_obs(ecop_plan_mejora_obs_tareas obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarPlanMejora_obs(obj, ref MsgRes);
        }

        public List<management_planmejora_tarea_control_estadoResult> CuentadetalleTareacontrolEstado(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentadetalleTareacontrolEstado(id_plan_mejora, ref MsgRes);
        }

    }
}