using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class ControldeGastos
    {
        #region Propiedades

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

        //private List<Managment_md_control_gastos_tableroResult> _ListTablero;
        //public List<Managment_md_control_gastos_tableroResult> ListTablero
        //{
        //    get
        //    {
        //        if (_ListTablero == null)
        //        {
        //            _ListTablero = BusClass.tableroControlGastos();
             
        //            return _ListTablero;
        //        }
        //        else
        //        {
        //            return _ListTablero;
        //        }

        //    }

        //    set
        //    {
        //        _ListTablero = value;
        //    }
        //}

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESUPUESTO TOTAL DEL CONTRATO")]
        public decimal proTotal { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESUPUESTO EJECUTADO")]
        public decimal proEjecutado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESUPUESTO PROMEDIO DESTINADO MES")]
        public Int64 propromedioMes { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR RETENIDO POR GLOSA")]
        public decimal ValorRetenidoGlosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "% PRESUPUESTO EJECUTADO")]
        public String PorcentajeEjecutado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR TOTAL ANTICIPOS GENERADOS")]
        public decimal ValorTotalAnticipos { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PRESUPUESTO EJECUTADO (VALOR FACTURADO CON AVAL DE PAGO)")]
        public String valor_facturado_con_aval { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR RETENIDO POR GLOSA")]
        public String valor_retenido_por_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR DE ANTICIPO GENERADO")]
        public String valor_anticipo_generado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MES")]
        public int mesIngresado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AÑO")]
        public int ano { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "AÑO")]
        public int ano2 { get; set; }


        public int id_presupuesto_ejecutado { get; set; }
        public DateTime? fecha_ingreso { get; set; }

        public String usuario_ingreso { get; set; }
               
        private List<ref_meses_del_año> _meses;
        public List<ref_meses_del_año> meses
        {
            get
            {
                if (_meses == null)
                {
                    _meses = BusClass.meses();


                    return _meses;
                }
                else
                {
                    return _meses;
                }

            }

            set
            {
                _meses = value;
            }
        }

        private List<Managment_md_control_gastos_tableroResult> _Tablero1;
        public List<Managment_md_control_gastos_tableroResult> Tablero1
        {
            get
            {
                if (_Tablero1 == null)
                {
                    _Tablero1 = BusClass.tableroControlGastos(1, 1);


                    return _Tablero1;
                }
                else
                {
                    return _Tablero1;
                }

            }

            set
            {
                _Tablero1 = value;
            }
        }

        private List<Managment_md_control_gastos_tablero2Result> _Tablero2;
        public List<Managment_md_control_gastos_tablero2Result> Tablero2
        {
            get
            {
                if (_Tablero2 == null)
                {
                    _Tablero2 = new List<Managment_md_control_gastos_tablero2Result>();


                    return _Tablero2;
                }
                else
                {
                    return _Tablero2;
                }

            }
            set
            {
                _Tablero2 = value;
            }
        }

        #endregion

        #region FUNCIONES

        public md_control_gastos control_gastosMes(Int32 mes, String año)
        {
            return BusClass.control_gastosMes(mes, año);

        }
        public Int32 Insertarcontrol_gasto(md_control_gastos OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertarcontrol_gasto(OBJDetalle, ref MsgRes);
        }
        public vw_md_control_gasto control_gastosTotal(Int32 mes)
        {
            vw_md_control_gasto OBJ = new vw_md_control_gasto();
            OBJ = BusClass.control_gastosTotal(mes);

            proTotal = Convert.ToDecimal(OBJ.total_presupuesto);
            propromedioMes = Convert.ToInt64(proTotal) / 30;
            proEjecutado = Convert.ToDecimal(OBJ.suma_ejecutado);
            ValorRetenidoGlosa = Convert.ToDecimal(OBJ.suma_retenido_glosa);
            ValorTotalAnticipos = Convert.ToDecimal(OBJ.suma_anticipos);
            String valor = $"{ OBJ.porcentaje_presupuesto.Value:0.#%}";
            PorcentajeEjecutado = valor;

            return OBJ;
        }

        public List<ref_meses_del_año> Listameses()
        {
            return BusClass.meses();
        }

        public void ActualizarControlGastos(md_control_gastos OBJMD, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarControlGastos(OBJMD, ref MsgRes);
        }

        public List<Managment_md_control_gastos_tableroResult> tableroControlGastos1(int opc, int año)
        {
            Tablero1 = BusClass.tableroControlGastos(opc, año);

            return Tablero1;
        }
        public List<Managment_md_control_gastos_tablero2Result> tableroControlGastos2(int opc, int año)
        {
            Tablero2 = BusClass.tableroControlGastos2(opc, año);

            return Tablero2;
        }

        #endregion

    }
}