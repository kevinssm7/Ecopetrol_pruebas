using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class EventoAdverso
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

        private List<Ref_eventos_adversos> _LstEventoAdversos;
        public List<Ref_eventos_adversos> LstEventoAdversos
        {
            get
            {
                if (_LstEventoAdversos == null)
                {
                  _LstEventoAdversos = BusClass.GetEventosAdversos();
                }

                return _LstEventoAdversos;
            }
            set { _LstEventoAdversos = value; }
        }

        private List<Ref_eventos_adversos> _LstEventoAdversos2;
        public List<Ref_eventos_adversos> LstEventoAdversos2
        {
            get
            {
                if (_LstEventoAdversos2 == null)
                {
                    //_LstEventoAdversos = BusClass.GetEventosAdversos();
                    return _LstEventoAdversos2 = new List<Ref_eventos_adversos>();
                }

                return _LstEventoAdversos2;
            }
            set { _LstEventoAdversos2 = value; }
        }


        private List<Ref_categorias_eventos_adv> _LstCategoriasAdversos;
        public List<Ref_categorias_eventos_adv> LstCategoriasAdversos
        {
            get
            {
                if (_LstCategoriasAdversos == null)
                {
                    return _LstCategoriasAdversos = BusClass.GetRefCategoriaEvad();
                }
                else
                {
                    return _LstCategoriasAdversos;
                }
             
            }

            set
            {
                _LstCategoriasAdversos = value;
            }
        }


        private List<Ref_grado_lesion> _lstGradoDeLesion;
        public List<Ref_grado_lesion> lstGradoDeLesion
        {
            get
            {
                if (_lstGradoDeLesion == null)
                {
                    _lstGradoDeLesion = BusClass.GetGradoLesion();
                }

                return _lstGradoDeLesion;
            }
            set { _lstGradoDeLesion = value; }
        }

        private List<Ref_plan_de_manejo> _lstPlanDeManejo;
        public List<Ref_plan_de_manejo> lstPlanDeManejo
        {
            get
            {
                if (_lstPlanDeManejo == null)
                {
                    _lstPlanDeManejo = BusClass.GetPlanDeManejo();
                }

                return _lstPlanDeManejo;
            }
            set { _lstPlanDeManejo = value; }
        }

        private List<Ref_barreras_seguridad> _lstBarreraSeguridad;
        public List<Ref_barreras_seguridad> lstBarreraSeguridad
        {
            get
            {
                if (_lstBarreraSeguridad == null)
                {
                    _lstBarreraSeguridad = BusClass.GetBarrerasDeSeguridad();
                }

                return _lstBarreraSeguridad;
            }
            set { _lstBarreraSeguridad = value; }
        }

        private List<Ref_factores_contribuyentes> _lstFacContribu;
        public List<Ref_factores_contribuyentes> lstFacContribu
        {
            get
            {
                if (_lstFacContribu == null)
                {
                    _lstFacContribu = BusClass.GetFactoresContribuyentes();
                }

                return _lstFacContribu;
            }
            set { _lstFacContribu = value; }
        }

        private List<Ref_acciones_inseguras> _lstAccionesInseg;
        public List<Ref_acciones_inseguras> lstAccionesInseg
        {
            get
            {
                if (_lstAccionesInseg == null)
                {
                    _lstAccionesInseg = BusClass.GetAccionesInseguras();
                }

                return _lstAccionesInseg;
            }
            set { _lstAccionesInseg = value; }
        }

        private List<ecop_concurrencia_eventos_adversos> _lstConcEventosAd;
        public List<ecop_concurrencia_eventos_adversos> lstConcEventosAd
        {
            get
            {
                if (_lstConcEventosAd == null)
                {
                    _lstConcEventosAd = new List<ecop_concurrencia_eventos_adversos>();
                }

                return _lstConcEventosAd;
            }
            set { _lstConcEventosAd = value; }
        }
        

        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "EVENTO ADVERSO")]
        public Int32 id_evento_adverso { get; set; }

        [Display(Name = "CATEGORIA EVENTOS ADVERSOS")]
        public Int32 id_categoria { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        [Display(Name = "DESCRIPCION DEL EVENTO ADVERSO")]
        public string descripcion_evento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "GRADO DE LESION")]
        public String id_grado_lesion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "PLAN DE MANEJO")]
        public String id_plan_de_manejo { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "BARRERAS DE SEGURIDAD")]
        public String id_barreras_seguridad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FACTORES CONTRIBUYENTES A LA OCURRENCIA")]
        public String id_factores_contribuyentes { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "ACCIONES INSEGURAS")]
        public String id_acciones_inseguras { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA EVENTO ADVERSO")]
        public Nullable<DateTime> fecha_evento_adverso { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA RADICACION DE CARTA")]
        public Nullable<DateTime> fecha_de_radica_carta { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "QUIEN RECIBE")]
        public String quien_recibe { get; set; }

        public DateTime? fecha_evento_adversook { get; set; }

        public DateTime? fecha_de_radica_cartaok { get; set; }

        public int Items { get; set; }

        public List<vw_tablero_eventos_adversos> Lista { get; set; }


        #endregion
        #region METODOS

        public void ConsultaLista(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            LstEventoAdversos2 = BusClass.GetEventosAdversos();
            LstEventoAdversos2 = LstEventoAdversos2.Where(x => x.categoria == Convert.ToString(strFiltro)).ToList();
        }

        public DateTime ManagmentHora()
        {
            return BusClass.ManagmentHora();
        }

        public void InsertaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdv, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertaEventoAdverso(ObjEventoAdv, UserName, IPAddress, ref MsgRes);
        }

        public void ConsultaEventoAdverso(Int32 idConcu)
        {
            ecop_concurrencia_eventos_adversos objEventoAdv = new ecop_concurrencia_eventos_adversos();
            objEventoAdv.id_concurrencia = idConcu;
            lstConcEventosAd = BusClass.ConsultaEventoAdverso(objEventoAdv, ref MsgRes);
        }
        
        public List<vw_tablero_eventos_adversos> GetLista()
        {
            return BusClass.ReportesEventoAdverso();
        }
        #endregion


    }
}