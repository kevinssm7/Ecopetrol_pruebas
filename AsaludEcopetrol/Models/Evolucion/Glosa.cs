using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class Glosa
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

        private ecop_concurrencia _OBJConcurencia;
        public ecop_concurrencia OBJConcurencia
        {
            get
            {
                if (_OBJConcurencia == null)
                {
                    _OBJConcurencia = new ecop_concurrencia();
                }

                return _OBJConcurencia;
            }
            set { _OBJConcurencia = value; }
        }

        private List<ecop_concurrencia> _LstConcurencia;
        public List<ecop_concurrencia> LstConcurencia
        {
            get
            {
                if (_LstConcurencia == null)
                {
                    _LstConcurencia = new List<ecop_concurrencia>();
                }

                return _LstConcurencia;
            }
            set { _LstConcurencia = value; }
        }

        private List<vw_ref_cups> _LstCups;
        public List<vw_ref_cups> LstCups
        {
            get
            {
                if (_LstCups == null)
                {
                    _LstCups = BusClass.GetCups();
                }

                return _LstCups;
            }
            set { _LstCups = value; }
        }

        private List<ecop_concurrencia_glosa> _lstGlosa;
        public List<ecop_concurrencia_glosa> lstGlosa
        {
            get
            {
                if (_lstGlosa == null)
                {
                    _lstGlosa = new List<ecop_concurrencia_glosa>();
                }

                return _lstGlosa;
            }
            set { _lstGlosa = value; }
        }

        //ref_cuenta_maestro_ips
        private List<vw_ips_ciudad> _lstIPS;

        public List<vw_ips_ciudad> lstIPS
        {
            get
            {
                if (_lstIPS == null)
                {

                    _lstIPS = BusClass.GetIPS();
                    _lstIPS = _lstIPS.Where(x => x.usuario == Convert.ToString(SesionVar.UserName)).ToList();
                    return _lstIPS;
                }

                return _lstIPS;
            }
            set { _lstIPS = value; }
        }

        //private List<ref_maestro_ips> _lstmaestroIPS;

        //public List<ref_maestro_ips> lstmaestroIPS
        //{
        //    get
        //    {
        //        if (_lstmaestroIPS == null)
        //        {

        //            _lstmaestroIPS = BusClass.GetrefMaestroIps();
        //        }

        //        return _lstmaestroIPS;
        //    }
        //    set { _lstmaestroIPS = value; }
        //}


        private List<Ref_cuentas_glosa> _lstCodGlosa;
        public List<Ref_cuentas_glosa> lstCodGlosa
        {
            get
            {
                if (_lstCodGlosa == null)
                {
                    List<Ref_cuentas_glosa> _lstCodGlosa2;
                    _lstCodGlosa2 = BusClass.GetCuentaGlosa();
                    List<Ref_cuentas_glosa> _lstCodGlosa3 = new List<Ref_cuentas_glosa>();
                    foreach (Ref_cuentas_glosa item in _lstCodGlosa2)
                    {
                        if (item.estado == "A")
                        {

                            _lstCodGlosa3.Add(item);

                        }
                    }
                    _lstCodGlosa = _lstCodGlosa3;
                }

                return _lstCodGlosa;
            }
            set { _lstCodGlosa = value; }
        }


        private List<Ref_responsable_glosa> _lstRespGlosa;
        public List<Ref_responsable_glosa> lstRespGlosa
        {
            get
            {
                if (_lstRespGlosa == null)
                {
                    _lstRespGlosa = BusClass.GetResGlosa();
                }

                return _lstRespGlosa;
            }
            set { _lstRespGlosa = value; }
        }



        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CODIGO CUPS")]
        public String id_cups { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESPONSABLE GLOSA")]
        public String responsable_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CODIGO GLOSA")]
        public String id_codigo_glosa { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "IPS")]
        public String IPS { get; set; }

        [Required(ErrorMessage = "***")]
        [Range(typeof(int), "1", "100000000", ErrorMessage = "Mayor a 0")]
        [Display(Name = "CANTIDAD GLOSA")]
        public Int32 cantidad_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        
        [Display(Name = "VALOR GLOSA")]
        public String valor_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "OBSERVACIONES AUDITORIA")]
        public string observacion_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CAUSAL GLOSA")]
        public String causal_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,500}$", ErrorMessage = "Maximo 500 caracteres")]
        [Display(Name = "OTRO CAUSAL GLOSA")]
        public String otro_causal_glosa { get; set; }


        #endregion

        #region METODOS
        public DateTime ManagmentHora()
        {
            return BusClass.ManagmentHora();
        }

        public void InsertaGlosa(ecop_concurrencia_glosa ObjGlosa, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertaGlosa(ObjGlosa, UserName, IPAddress, ref MsgRes);
        }
        public void ConsultaGlosa(Int32 idConcu)
        {
            ecop_concurrencia_glosa objGlosa = new ecop_concurrencia_glosa();
            objGlosa.id_concurrencia = idConcu;
            lstGlosa = BusClass.ConsultaGlosa(objGlosa, ref MsgRes);
        }

       
        public void ConsultaIdConcurrenia(Int32 IntIdconcu)
        {
            OBJConcurencia.id_concurrencia = IntIdconcu;
            LstConcurencia = BusClass.ConsultaIdConcurrenia(OBJConcurencia, ref MsgRes);
            if (LstConcurencia.Count != 0)
            {
                foreach (ecop_concurrencia item in LstConcurencia)
                {
                    IPS = Convert.ToString(item.id_ips);
                }
            }
        }

        public List<Ref_causal_glosa> ColsultaCausalGlosa(int idresponsableglosa) {
            
            List<Ref_causal_glosa> listadocausales = BusClass.ConsultaCausalGlosa(idresponsableglosa, ref MsgRes);

            return listadocausales;
        }
        #endregion

    }
}