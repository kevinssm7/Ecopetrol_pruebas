using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Evolucion
{
    public class SituacionesCalidadAtencion
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

        private List<Ref_situaciones_de_calidad> _LstSituacionesCa;
        public List<Ref_situaciones_de_calidad> LstSituacionesCa
        {
            get
            {
                if (_LstSituacionesCa == null)
                {
                    //   _LstSituacionesCa = BusClass.GetSituacionesDeCalidad();
                    return _LstSituacionesCa = new List<Ref_situaciones_de_calidad>();
                }
                else
                {
                    return _LstSituacionesCa;
                }

              
            }
            set { _LstSituacionesCa = value; }
        }


        private List<Ref_situaciones_de_calidad> _LstSituacionesCa2;
        public List<Ref_situaciones_de_calidad> LstSituacionesCa2
        {
            get
            {
                if (_LstSituacionesCa2 == null)
                {
                     return  _LstSituacionesCa2 = BusClass.GetSituacionesDeCalidad();
                     
                }
                else
                {
                    return _LstSituacionesCa2;
                }


            }
            set { _LstSituacionesCa2 = value; }
        }
        private List<Ref_categorias_situaciones_de_calidad> _LstCategoriasSituacion;
        public List<Ref_categorias_situaciones_de_calidad> LstCategoriasSituacion
        {
            get
            {
                if (_LstCategoriasSituacion == null)
                {
                    return _LstCategoriasSituacion = BusClass.GetRefCategoriaSituacion();
                }
                else
                {
                    return _LstCategoriasSituacion;
                }
               
            }

            set
            {
                _LstCategoriasSituacion = value;
            }
        }

        private List<ecop_concurrencia_situaciones_de_calidad> _lstConcurrenciaSituacionCal;
        public List<ecop_concurrencia_situaciones_de_calidad> lstConcurrenciaSituacionCal
        {
            get
            {
                if (_lstConcurrenciaSituacionCal == null)
                {
                    _lstConcurrenciaSituacionCal = new List<ecop_concurrencia_situaciones_de_calidad>();
                }

                return _lstConcurrenciaSituacionCal;
            }
            set { _lstConcurrenciaSituacionCal = value; }
        }



        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia { get; set; }

        [Display(Name = "CATEGORIA SITUACION DE CALIDAD")]
        public Int32 id_categoria { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "SITUACION DE CALIDAD")]
        public String id_situacion_calidad { get; set; }

        [Required(ErrorMessage = "***")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,3000}$", ErrorMessage = "Maximo 3000 caracteres")]
        [Display(Name = "DESCRIPCION DE LA SITUACION DE CALIDAD")]
        public string descripcion_situacion_calidad { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA SITUACION DE CALIDAD")]
        public Nullable<DateTime> fecha_situacion_calid { get; set; }


        [Required(ErrorMessage = "***")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA RADICACION DE CARTA SOLICITUD")]
        public DateTime? fecha_de_radica_carta { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "QUIEN RECIBE")]
        public String quien_recibe { get; set; }
        public Nullable<DateTime> fecha_de_radica_cartaok { get; set; }

        public Nullable<DateTime> fecha_situacion_calidok { get; set; }


        #endregion

        #region METODOS

        public void ConsultaLista(int opc, string strFiltro, ref MessageResponseOBJ MsgRes)
        {
            LstSituacionesCa = BusClass.GetSituacionesDeCalidad();
            LstSituacionesCa = LstSituacionesCa.Where(x => x.categoria == Convert.ToString(strFiltro)).ToList();
        }

        public DateTime ManagmentHora()
        {
            return BusClass.ManagmentHora();
        }
        public void InsertaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituacionCalid, String UserName, String IPAddress, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertaSituacionesCalidad(ObjSituacionCalid, UserName, IPAddress, ref MsgRes);
        }

        public void ConsultaSituacionesDeCalidad(Int32 idConcu)
        {
            ecop_concurrencia_situaciones_de_calidad objSituacionCalid = new ecop_concurrencia_situaciones_de_calidad();
            objSituacionCalid.id_concurrencia = idConcu;
            lstConcurrenciaSituacionCal = BusClass.ConsultaSituacionesCalidad(objSituacionCalid, ref MsgRes);
        }
        #endregion
    }
}