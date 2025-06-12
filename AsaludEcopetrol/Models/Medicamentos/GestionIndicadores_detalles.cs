using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class GestionIndicadores_detalles
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

        public Int32 id_md_indicador_detalle { get; set; }

        public Int32 id_indicadores_medicamentos { get; set; }

        public Int32 id_md_ref_indicador { get; set; }
        
        [Required(ErrorMessage = "***")]
        [Range(0, 999999999)]
        [Display(Name = "PESO")]
        public Int32? peso { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "valor")]
        public int? valor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO")]
        public Int32? resultado { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "observaciones")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,1000}$", ErrorMessage = "Maximo 1000 caracteres")]
        public String observaciones { get; set; }




        #endregion

        #region METODOS




        #endregion
    }
}