using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using AsaludEcopetrol.Models.Evolucion;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class ConcurrenciaAhorro
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

        private ecop_concurrencia_ahorro _OBJAhorro;
        public ecop_concurrencia_ahorro OBJAhorro
        {
            get
            {
                if (_OBJAhorro == null)
                {
                    return _OBJAhorro = new ecop_concurrencia_ahorro();
                }
                else
                {
                    return _OBJAhorro;
                }

            }

            set
            {
                _OBJAhorro = value;
            }
        }

        private List<ecop_concurrencia_ahorro> _LstAhorro;
        public List<ecop_concurrencia_ahorro> LstAhorro
        {
            get
            {
                if (_LstAhorro == null)
                {
                    return _LstAhorro = new List<ecop_concurrencia_ahorro>();
                }
                else
                {
                    return _LstAhorro;
                }

            }

            set
            {
                _LstAhorro = value;
            }
        }


        private List<vw_ecop_concurrencia_ahorro> _LstAhorroOtro;
        public List<vw_ecop_concurrencia_ahorro> LstAhorroOtro
        {
            get
            {
                if (_LstAhorroOtro == null)
                {
                    return _LstAhorroOtro = new List<vw_ecop_concurrencia_ahorro>();
                }
                else
                {
                    return _LstAhorroOtro;
                }

            }

            set
            {
                _LstAhorroOtro = value;
            }
        }

        private List<Ref_tipo_ahorro> _LstRefTipoAhorro;
        public List<Ref_tipo_ahorro> LstRefTipoAhorro
        {
            get
            {
                if (_LstRefTipoAhorro == null)
                {
                    return _LstRefTipoAhorro = BusClass.GetRefTipoAhorro();
                }
                else
                {
                    return _LstRefTipoAhorro;
                }

            }

            set
            {
                _LstRefTipoAhorro = value;
            }
        }

        private List<Ref_valor_ahorro> _LstRefValorAhorro;
        public List<Ref_valor_ahorro> LstRefValorAhorro
        {
            get
            {
                if (_LstRefValorAhorro == null)
                {
                    return _LstRefValorAhorro = BusClass.GetRefValorAhorro();
                }
                else
                {
                    return _LstRefValorAhorro;
                }

            }

            set
            {
                _LstRefValorAhorro = value;
            }
        }

        [Required(ErrorMessage = "***")]
        [Display(Name = "AHORRO")]
        public String id_ref_valor_ahorro { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "COSTO AHORRO")]
        public Int32? costo_estancia { get; set; }

        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO AHORRO")]
        public int id_ref_tipo_ahorro { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OTRO TIPO AHORRO:")]
        public String otro_tipo_ahorro { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CANTIDAD")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR AHORRO OTRO")]
        public String valor_ahorro2 { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OTRO VALOR")]
        public String ingreso_valor_otro { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOR AHORRO")]
        public String valor_ahorro { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        [Display(Name = "OBSERVACIONES AUDITORIA")]




        public string observacion { get; set; }

        public DateTime fecha_digita { get; set; }

        public String usuario_digita { get; set; }






        #endregion

        #region METODOS

        public void InsertarConcurrenciaAhorro(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarConcurrenciaAhorro(OBJAhorro, ref MsgRes);
        }
        public void ConsultaAhorro(Int32 idConcu)
        {
            vw_ecop_concurrencia_ahorro objAh = new vw_ecop_concurrencia_ahorro();
            objAh.id_concurrencia = idConcu;
            LstAhorroOtro = BusClass.ConsultaAhorroOtro(objAh, ref MsgRes);
        }


        public void ConsultaIdConcurrenia(Int32 IntIdconcu)
        {
            //OBJConcurencia.id_concurrencia = IntIdconcu;
            //LstConcurencia = BusClass.ConsultaIdConcurrenia(OBJConcurencia, ref MsgRes);
            //if (LstConcurencia.Count != 0)
            //{
            //    foreach (ecop_concurrencia item in LstConcurencia)
            //    {
            //        IPS = Convert.ToString(item.id_ips);
            //    }
            //}
        }

        public List<Ref_valor_ahorro> ListaValorAhorro(Int32 id)
        {
            List<Ref_valor_ahorro> list = new List<Ref_valor_ahorro>();

            list = BusClass.ValorAhorro();

            list = list.Where(X => X.id_ref_valor_ahorro == id).ToList();

            foreach (var item in list)
            {
                costo_estancia = item.costo_dia_de_estancia;


            }

            return list;



        }
        #endregion
    }
}