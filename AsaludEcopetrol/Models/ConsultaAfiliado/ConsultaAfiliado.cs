using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.ConsultaAfiliado
{
    public class ConsultaAfiliado
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

        [Required(ErrorMessage = "***")]
        public string IdSeleccionado { get; set; }

        [Required(ErrorMessage = "***")]
        [DataType(DataType.Text)]
        [Display(Name = "NUMERO IDENTIFICACIÓN:")]
        public string numeroIdentificacion { get; set; }

        [Required(ErrorMessage = "***")]
        public String Items { get; set; }

        private List<Ref_tipo_documental> _ListTipoDoc;
        public List<Ref_tipo_documental> ListTipoDoc
        {
            get
            {
                if (_ListTipoDoc == null)
                {
                    return _ListTipoDoc = BusClass.GetTipoDocumnetal();
                }
                else
                {
                    return _ListTipoDoc;
                }

            }

            set
            {
                _ListTipoDoc = value;
            }
        }


        [Required(ErrorMessage = "***")]
        public Int32 id_concurrencia { get; set; }

        private List<ecop_concurrencia> _LstConcurrencia;
        public List<ecop_concurrencia> LstConcurrencia
        {
            get
            {
                if (_LstConcurrencia == null)
                {
                    _LstConcurrencia = new List<ecop_concurrencia>();
                }

                return _LstConcurrencia;
            }
            set { _LstConcurrencia = value; }
        }



        public List<Int32> IdSeleccionadoConcu { get; set; }

        #endregion

        #region METODOS
        public List<ecop_concurrencia> ConsultaAfiliadoConcurrenia(ecop_concurrencia ObjAfiliado)
        {
            return BusClass.ConsultaAfiliadoConcurrenia(ObjAfiliado, ref MsgRes);
        }

        public List<ecop_concurrencia> ConsultaIdConcurrenia(ecop_concurrencia ObjAfiliado)
        {
            return BusClass.ConsultaIdConcurrenia(ObjAfiliado, ref MsgRes);
        }

        #endregion

    }
}