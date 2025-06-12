using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class ConsultaMed 
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


        [Required(ErrorMessage = "***")]
        [Display(Name = "CONSULTA")]
        public int opc { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA INICIAL")]
        public DateTime? fecha_inicial { get; set; }

        [Required(ErrorMessage = "***")]
        public DateTime? fecha_inicialT { get; set; }

        public List<ManagmentocargueconsolidadoResult> std { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA FINAL")]
        public DateTime? fecha_final { get; set; }

        [Required(ErrorMessage = "***")]
        public DateTime? fecha_finalT { get; set; }

        public DateTime? fecha_inicialok { get; set; }
        public DateTime? fecha_finalok { get; set; }


        public DateTime? fecha_inicialA { get; set; }
        public DateTime? fecha_finalA { get; set; }

        public DateTime? fecha_inicialB { get; set; }
        public DateTime? fecha_finalB { get; set; }

        public DateTime? fecha_inicialC { get; set; }
        public DateTime? fecha_finalC { get; set; }

        public DateTime? fecha_inicialD { get; set; }
        public DateTime? fecha_finalD { get; set; }

        public DateTime? fecha_inicialE { get; set; }
        public DateTime? fecha_finalE { get; set; }

        public DateTime? fecha_inicialF { get; set; }
        public DateTime? fecha_finalF { get; set; }

        public DateTime? fecha_inicialM { get; set; }

        public DateTime? fecha_finalM { get; set; }
        public String consulta { get; set; }

        private List<md_Ref_consultas> _ListMDconsulta;
        public List<md_Ref_consultas> ListMDconsulta
        {
            get
            {
                if (_ListMDconsulta == null)
                {
                    return _ListMDconsulta = BusClass.GetRefConsulta();
                }
                else
                {
                    return _ListMDconsulta;
                }

            }

            set
            {
                _ListMDconsulta = value;
            }
        }
        
        private List<Managment_md_ConsultasResult> _ListaConsulta;
        public List<Managment_md_ConsultasResult> ListaConsulta
        {
            get
            {
                if (_ListaConsulta == null)
                {
                    return _ListaConsulta = new List<Managment_md_ConsultasResult>();
                }
                else
                {
                    return _ListaConsulta;
                }

            }

            set
            {
                _ListaConsulta = value;
            }
        }

        private List<md_Ref_consultas> _ListMDcons;
        public List<md_Ref_consultas> ListMDcons
        {
            get
            {
                if (_ListMDcons == null)
                {
                    return _ListMDcons = new List<md_Ref_consultas>();
                }
                else
                {
                    return _ListMDcons;
                }
               
            }

            set
            {
                _ListMDcons = value;
            }
        }

        private List<ManagmentocargueconsolidadoResult> _ListConsolidado;
        public List<ManagmentocargueconsolidadoResult> ListConsolidado
        {
            get
            {
                if (_ListConsolidado == null)
                {
                    return _ListConsolidado = new List<ManagmentocargueconsolidadoResult>();
                    //return _ListConsolidado = BusClass.CuentaConsolidadoMD("1", "2", Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now), ref MsgRes);
                }
                else
                {
                    return _ListConsolidado;
                }

            }

            set
            {
                _ListConsolidado = value;
            }
        }

        private List<md_ref_puntos_control> _ListaConsultaPC;
        public List<md_ref_puntos_control> ListaConsultaPC
        {
            get
            {
                if (_ListaConsultaPC == null)
                {
                    return _ListaConsultaPC = new List<md_ref_puntos_control>();
                }
                else
                {
                    return _ListaConsultaPC;
                }

            }

            set
            {
                _ListaConsultaPC = value;
            }
        }

        [Display(Name = "GRAFICAR")]
        public int filtro { get; set; }



        #endregion

        #region FUNCIONES

        public List<ManagmentocargueconsolidadoResult> Read()
        {
            return BusClass.CuentaConsolidadoMD("", "", Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now), ref MsgRes);

        }
        public void CuentaFechaCargue(Int32 Opc, DateTime fecha_inicial, DateTime fecha_final)
        {
            ListaConsulta = BusClass.CuentaConsultasMedicamentos(Opc, fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<md_ref_puntos_control> GetpuntoControl()
        {
            return ListaConsultaPC =  BusClass.GetpuntoControl();
        }

        public void ListaC(Int32 opc)
        {
            ListMDcons = BusClass.GetRefConsulta();

            ListMDcons = ListMDcons.Where(x => x.id_ref_consultas == opc).ToList();

            foreach (var item in ListMDcons)
            {
                consulta = item.descripcion;
            }
        }

        public void ProductWiseSales(out string DescripcionCountList, out string ValorList)
        {
            var Descrip = (from temp in ListaConsulta
                                     select temp.descripcion).ToList();

            var valord = (from temp in ListaConsulta
                                select temp.valor).ToList();

            DescripcionCountList = string.Join(",", Descrip);

            ValorList = string.Join(",", valord);

        }

        public List<ManagmentocargueconsolidadoResult> CuentaConsolidadoMD(String numero_factura, String numero_formula, DateTime fecha_inicial, DateTime fecha_final)
        {
            return BusClass.CuentaConsolidadoMD(numero_factura, numero_formula, fecha_inicial, fecha_final, ref MsgRes);
        }

        #endregion
    }
}