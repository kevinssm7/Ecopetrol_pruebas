using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECOPETROL_COMMON.ENUM
{
    public class BitacoraSeguimiento
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string observaciones { get; set; }
        public string usuario { get; set; }
        public int estado { get; set; }
        public string nomestado { get; set; }
        public int medionotificacion { get; set; }
        public string mailnotificacion { get; set; }
        public string telefononotificacion { get; set; }
        public int tipoSolicitud { get; set; }
        public string tipoSolicitudDescripcion { get; set; }
        public string casoEfectivo { get; set; }

        public string ruta { get; set; }
        public string tipoArchivo { get; set; }

        public string nombrecontactsolucionadorIPS { get; set; }
        public string telefonocontactsolucionador { get; set; }
        public string correocontactosolucionador { get; set; }
        public int servicio { get; set; }
        public string servicioDescripcion { get; set; }
        public string otroservicio { get; set; }

        public int idConcurrencia { get; set; }
    }
}
