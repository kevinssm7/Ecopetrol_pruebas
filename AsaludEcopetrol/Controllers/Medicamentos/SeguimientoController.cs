using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.Models.Medicamentos;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Medicamentos
{
    [SessionExpireFilter]
    public class SeguimientoController : Controller
    {
        #region  PROPIEDADES

        private SeguimientoPendientes db = new SeguimientoPendientes();

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

        #endregion

        // GET: Seguimiento

        public ActionResult SeguimientoPendientes()
        {
            Models.Medicamentos.SeguimientoPendientes Model = new Models.Medicamentos.SeguimientoPendientes();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            string RolUsuario = SesionVar.ROL;
            ViewBag.idadmin = SesionVar.UserName;
            List<sis_usuario> usuarios = Model.Usuarios().ToList();
            ViewBag.solicitantes = usuarios;

            return View(Model);

        }

        public ActionResult SeguimientoPendientesDetalle(Int32 id, String proveedor, String hallazgo)
        {
            Models.Medicamentos.SeguimientoPendientes Model = new Models.Medicamentos.SeguimientoPendientes();

            Model.id_md_seguimiento_pendientes = id;
            Model.nombre_auditado = proveedor;
            Model.hallazgos = hallazgo;

            return View(Model);
        }


        public ActionResult BuscarSeguimiento()
        {
            Models.Medicamentos.SeguimientoPendientes Model = new Models.Medicamentos.SeguimientoPendientes();

            Model.ConsultaLista();

            return View(Model);
        }


        [HttpPost]
        public ActionResult BuscarSeguimiento(Models.Medicamentos.SeguimientoPendientes Model)
        {

            String proveedor = Model.nombre_auditor;

            Model.SeguimientoPendientesProveedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }



        [HttpPost]
        public ActionResult SeguimientoPendientes(Models.Medicamentos.SeguimientoPendientes Model, string solicitante)
        {
            try
            {
                string RolUsuario = SesionVar.ROL;

                List<sis_usuario> usuarios = Model.Usuarios();
                if (!RolUsuario.Contains("1"))
                    usuarios = usuarios.ToList();

                ViewBag.solicitantes = usuarios;
                ViewBag.idadmin = SesionVar.UserName;
                ViewBag.rolusuario = RolUsuario;
                Int32 Conteo = 0;
                String variable = "";
                String variable2 = "";

                if (Model.fecha_auditoriaOK != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA AUDITORIA";
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
                    Model.OBJSeguimientoPendientes.fecha_auditoria = Model.fecha_auditoriaOK;
                    Model.OBJSeguimientoPendientes.usuario_auditor = solicitante;
                    Model.OBJSeguimientoPendientes.nombre_auditado = Model.nombre_auditado;
                    Model.OBJSeguimientoPendientes.nombre_puntos_dispensacion = Model.nombre_farmacia;
                    Model.OBJSeguimientoPendientes.fuerza = Model.fuerza;
                    Model.OBJSeguimientoPendientes.ciudad = Model.ciudad;
                    Model.OBJSeguimientoPendientes.fecha_digita = Convert.ToDateTime(DateTime.Now);
                    Model.OBJSeguimientoPendientes.usuario_digita = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.InsertarSeguimientoPendientes(ref MsgRes);
                    Model.id_md_seguimiento_pendientes = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<Managment_md_Ref_seguimiento_pendientesResult> list = new List<Managment_md_Ref_seguimiento_pendientesResult>();
                        list = Model.ListaSeguimientoPendietnes(Model.id_md_seguimiento_pendientes);

                        foreach (var item in list)
                        {
                            Model.OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes = Model.id_md_seguimiento_pendientes;
                            Model.OBJSeguimientoPendientesDetalle.id_md_ref_seguimiento_pendiente = item.id_md_ref_seguimiento_pendiente;
                            Model.OBJSeguimientoPendientesDetalle.peso = item.peso;
                            Model.OBJSeguimientoPendientesDetalle.valor = 1;
                            Model.OBJSeguimientoPendientesDetalle.resultado = 1 * item.peso;
                            Model.OBJSeguimientoPendientesDetalle.observaciones = "";

                            Model.InsertarSeguimientoPendientesDetalle(ref MsgRes);
                        }

                        return RedirectToAction("SeguimientoPendientesDetalle", "Seguimiento", new { id = Model.id_md_seguimiento_pendientes, proveedor = Model.nombre_auditado });
                    }
                    else
                    {
                        ModelState.AddModelError("", "ERROR AL INGRESO!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
                }



            }
            catch
            {

            }
            return View(Model);

        }

        public JsonResult SaveSeguimiento4(Int32 id_md_seguimiento_pendientes, String hallazgos)
        {
            Models.Medicamentos.SeguimientoPendientes Model = new SeguimientoPendientes();
            Model.id_md_seguimiento_pendientes = Convert.ToInt32(id_md_seguimiento_pendientes);
            vw_total_md_seguimiento_detalle OBJ = new vw_total_md_seguimiento_detalle();

            OBJ = Model.Total_DetalleSeguimientoPendientesMD(id_md_seguimiento_pendientes);


            Decimal? VALOR = OBJ.sum_resultado.Value /  OBJ.sum_peso * 100;

            Model.resultado = VALOR;

            Model.OBJSeguimientoPendientes.id_md_seguimiento_pendientes = id_md_seguimiento_pendientes;
            Model.OBJSeguimientoPendientes.resultado = Model.resultado;
            Model.OBJSeguimientoPendientes.hallazgo = hallazgos;
 

            Model.ActualizarSeguimientoPendientesMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                //CrearPdfCartaIndicadoresMD("CartaIndicaMD   " + id_obligaciones_contractuales, id_obligaciones_contractuales);
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }


        }

        public JsonResult GetSeguimineto2(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.SeguimientoPendientes Model = new SeguimientoPendientes();
            Model.id_md_seguimiento_pendientes = id.Value;

            List<Managment_md_Ref_seguimiento_pendientesResult> Lista = new List<Managment_md_Ref_seguimiento_pendientesResult>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefSeguimientoPendientes(Model.id_md_seguimiento_pendientes);

            var query = Lista;
            List<Managment_md_Ref_seguimiento_pendientesResult> records = new List<Managment_md_Ref_seguimiento_pendientesResult>();
            int total;

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }

            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSeguimiento3(Models.Medicamentos.SeguimientoPendientes record)
        {

            List<md_seguimiento_pendientes_detalle> List = new List<md_seguimiento_pendientes_detalle>();
            List = record.GetSeguimientoPendientesDetalleID(record.id_md_ref_seguimiento_pendiente, record.id_md_seguimiento_pendientes);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes_detalle = item.id_md_seguimiento_pendientes_detalle;
                    record.OBJSeguimientoPendientesDetalle.id_md_seguimiento_pendientes = item.id_md_seguimiento_pendientes;
                    record.OBJSeguimientoPendientesDetalle.peso = record.peso;
                    record.OBJSeguimientoPendientesDetalle.valor = record.valor;
                    record.OBJSeguimientoPendientesDetalle.resultado = record.valor * record.peso;
                    record.OBJSeguimientoPendientesDetalle.observaciones = record.observaciones;

                    record.ActualizarSeguimientoPendientesDetalleMD(ref MsgRes);
                }

            }

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }

        }

        [HttpPost]
        public ActionResult ListaDispersacion(Int32 id, Models.Medicamentos.SeguimientoPendientes Model)
        {
            var List = Model.ListaPuntosDispersacion(id);
            if (List.Count() > 0)
            {
                md_ref_puntos_dispensacion OBJ = new md_ref_puntos_dispensacion();
                foreach (var item in List)
                {
                    OBJ.direccion = item.direccion;
                    OBJ.telefono = item.telefono;
                    OBJ.fuerza = item.fuerza;
                }


                return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }


        }




    }
}