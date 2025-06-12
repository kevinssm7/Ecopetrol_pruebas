using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Odontologia
{
    public class ProtesisRemovibleDtll
    {
        public int id_rehabilitacion_oral_protesis_removibles { get; set; }
        public int id_ref_tratamiento_rem { get; set; }
        public int id_ref_paramatros_rem { get; set; }
        public int estabilidad { get; set; }
        public int funcion_oclusal { get; set; }
        public int terminado { get; set; }
        public int diseño { get; set; }
    }
}