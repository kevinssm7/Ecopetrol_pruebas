using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    [SessionExpireFilter]
    public class HallazgosController : Controller
    {
        #region  PROPIEDADES

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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        public ActionResult Hallazgo()
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            return View(Model);
        }
     
        public ActionResult GestionarHallazgo(String ID)
        {
            Models.Facturacion.hallazgosRips Model = new Models.Facturacion.hallazgosRips();

            Model.id_hallazgo_RIPS = Convert.ToInt32(ID);

            return View(Model);
        }

        [HttpPost]
        public ActionResult GestionarHallazgo(Models.Facturacion.hallazgosRips Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_recepcionOK != null)
            {

            }
            else
            {

                variable2 = "FECHA DE RECEPCION";
                Conteo = Conteo + 1;
            }


            if (Conteo == 0)
            {
                variable = "OK";
            }
            else
            {
                variable = "ERROR";

            }

            if (variable != "ERROR")
            {
                Model.OBJRips.id_hallazgo_RIPS = Model.id_hallazgo_RIPS;
                Model.OBJRips.fecha_recepcion_nuevo_Rips = Model.fecha_recepcionOK;
                Model.OBJRips.gestionado = "SI";
                Model.OBJRips.fecha_ingreso_gestion = Convert.ToDateTime(DateTime.Now);
                Model.OBJRips.usuario_ingreso_gestion = Convert.ToString(SesionVar.UserName);


                Model.ActualizaHallazgosRips(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    return RedirectToAction("Hallazgo", "Hallazgos");
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error al ingreso" + MsgRes.DescriptionResponse);
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);


            }


            return View(Model);
        }
    }
}