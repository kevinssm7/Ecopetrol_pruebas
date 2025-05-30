using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class Glosas
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

        #endregion

        #region ATRIBUTOS 

        public int id_ffmm_Cuentas_auditoria2 { get; set; }
        public int id_glosa { get; set; }
        public int id_ffmm_cuentas_auditoria { get; set; }
        public DateTime fecha_programacion_visita { get; set; }
        public DateTime fecha_ejecucion_visita { get; set; }
        public String observaciones { get; set; }
        public DateTime fecha_aprobacion_acta { get; set; }
        public DateTime fecha_firma_acta { get; set; }
        public int estado { get; set; }
        public int[] arreglo { get; set; }

        public DateTime fecha_respuesta_glosa_inicial { get; set; }
        public int codigo_respuesta_glosa { get; set; }
        public string descripcion_respuesta_glosa { get; set; }
        public decimal vlr_aceptado_respuesta_glosa { get; set; }
        public decimal vlr_levantado_contratista_res_glosa { get; set; }
        public decimal vlr_glosa_ratificada_res_glosa_primera_conciliacion { get; set; }
        public DateTime fecha_primera_conciliacion { get; set; }
        public decimal vlr_aceptado_primera_conciliacion { get; set; }
        public decimal vlr_levantado_primera_conciliacion { get; set; }
        public decimal vlr_glosa_ratificada_contratista_segunda_conciliacion { get; set; }
        public DateTime fecha_segunda_conciliacion { get; set; }
        public decimal vlr_aceptado_segunda_conciliacion { get; set; }
        public decimal vlr_levantado_segunda_conciliacion { get; set; }
        public decimal vlr_glosa_definitiva_segunda_conciliacion { get; set; }
        public string descripcion_glosa_definitiva { get; set; }
        public DateTime fecha_acta_respuesta_glosa { get; set; }
        public int numero_acta_respuesta_glosa { get; set; }
        public DateTime fecha_acta_primera_conciliacion { get; set; }
        public int numero_acta_primera_conciliacion { get; set; }
        public DateTime fecha_acta_segunda_conciliacion { get; set; }
        public int numero_acta_segunda_conciliacion { get; set; }
        public string analista_cuentas_medicas { get; set; }
        public string auditor_profesional_salud_cuentas { get; set; }
        public string auditor_medico_cuentas { get; set; }
        public DateTime fecha_entrega_contratistas_factura { get; set; }
        public DateTime fecha_entrega_contratistas_respuesta_glosa { get; set; }
        public DateTime fecha_entrega_contratistas_primera_conciliacion_glosa { get; set; }
        public DateTime fecha_entraga_contratistas_segunda_conciliacion_glosa { get; set; }
        public string proceso { get; set; }


        #endregion

        #region FUNCIONES

        public List<vw_ffmm_glosas> GetFFMMGlosas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFFMMGlosas(ref MsgRes);
        }


        public List<Management_FFMM_Glosas_PrestadoresResult> GetFFMMGlosasPrestadores(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFFMMGlosasPrestadores(ref MsgRes);
        }

        public Int32 SaveProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.SaveProgramarVisitaGlosa(objeto, ref MsgRes);

        }

        public Int32 UpdateProgramarVisitaGlosa(ffmm_glosas objeto, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.UpdateProgramarVisitaGlosa(objeto, ref MsgRes);

        }





        public Int32 UpdateGlosa(ffmm_glosas objeto, string caso, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.UpdateGlosa(objeto, caso, ref MsgRes);
        }



        public ffmm_cuentas_glosas GetCuentasGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetCuentasGlosas(id_glosa, ref MsgRes);
        }



        public ffmm_glosas GetGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetGlosas(id_glosa, ref MsgRes);
        }



        #endregion

    }
}