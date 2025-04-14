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
    public class InterventoriaController : Controller
    {

        #region  PROPIEDADES

        private ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

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

        // GET: Interventoria
        public ActionResult InterventoriaGeneral()
        {
            Models.Medicamentos.interventoria Model = new Models.Medicamentos.interventoria();
            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            string RolUsuario = SesionVar.ROL;
            ViewBag.idadmin = SesionVar.UserName;
            List<sis_usuario> usuarios = Model.Usuarios().ToList();
            ViewBag.solicitantes = usuarios;

            return View(Model);
        }

        public ActionResult InterventoriaGeneralDetalle(Int32 id, String proveedor, String Hallazgo)
        {
            Models.Medicamentos.interventoria Model = new Models.Medicamentos.interventoria();

            Model.id_md_interventoria_general = id;
            Model.nombre_auditado = proveedor;
            Model.hallazgos = Hallazgo;

            return View(Model);
        }

        public ActionResult BuscarInterventoriaGeneral()
        {
            Models.Medicamentos.interventoria Model = new Models.Medicamentos.interventoria();
            Model.ConsultaLista();
            return View(Model);
        }


        [HttpPost]
        public ActionResult BuscarInterventoriaGeneral(Models.Medicamentos.interventoria Model)
        {
            String proveedor = Model.nombre_auditor;

            Model.InterventoriaGeneralProveedor(proveedor);
            Model.ConsultaLista();

            return View(Model);
        }


        [HttpPost]
        public ActionResult InterventoriaGeneral(string solicitante ,Models.Medicamentos.interventoria Model)
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

                if (Model.ciudad != 0)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "CIUDAD";
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
                    Model.OBJInterventoriaGeneral.fecha_auditoria = Model.fecha_auditoriaOK;
                    Model.OBJInterventoriaGeneral.nombre_auditor = solicitante;
                    Model.OBJInterventoriaGeneral.ciudad = Convert.ToString(Model.ciudad);
                    Model.OBJInterventoriaGeneral.nombre_auditado = Model.nombre_auditado;
                    Model.OBJInterventoriaGeneral.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                    Model.OBJInterventoriaGeneral.usuario_ingreso = Convert.ToString(SesionVar.UserName);

                    Int32 id = Model.InsertarInterventoriaG(ref MsgRes);
                    Model.id_md_interventoria_general = id;
                    if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        List<Managment_md_Ref_General1Result> list1 = new List<Managment_md_Ref_General1Result>();
                        List<Managment_md_Ref_General2Result> list2 = new List<Managment_md_Ref_General2Result>();
                        List<Managment_md_Ref_General3Result> list3 = new List<Managment_md_Ref_General3Result>();
                        List<Managment_md_Ref_General4Result> list4 = new List<Managment_md_Ref_General4Result>();

                        list1 = Model.ListaInterventoriaGeneral1(Model.id_md_interventoria_general);
                        list2 = Model.ListaInterventoriaGeneral2(Model.id_md_interventoria_general);
                        list3 = Model.ListaInterventoriaGeneral3(Model.id_md_interventoria_general);
                        list4 = Model.ListaInterventoriaGeneral4(Model.id_md_interventoria_general);


                        foreach (var item in list1)
                        {
                            Model.OBJDetallleG1.id_md_interventoria_general = Model.id_md_interventoria_general;
                            Model.OBJDetallleG1.id_md_ref_inte_general1 = item.id_md_ref_general1;
                            Model.OBJDetallleG1.peso = item.peso;
                            Model.OBJDetallleG1.valor = 1;
                            Model.OBJDetallleG1.resultado = 1 * item.peso;
                            Model.OBJDetallleG1.observaciones = "";

                            Model.InsertarGeneral1Detalle(ref MsgRes);
                        }


                        foreach (var item in list2)
                        {
                            Model.OBJDetallleG2.id_md_interventoria_general = Model.id_md_interventoria_general;
                            Model.OBJDetallleG2.id_md_ref_inte_general2 = item.id_md_ref_general2;
                            Model.OBJDetallleG2.peso = item.peso;
                            Model.OBJDetallleG2.valor = 1;
                            Model.OBJDetallleG2.resultado = 1 * item.peso;
                            Model.OBJDetallleG2.observaciones = "";

                            Model.InsertarGeneral2Detalle(ref MsgRes);
                        }

                        foreach (var item in list3)
                        {
                            Model.OBJDetallleG3.id_md_interventoria_general = Model.id_md_interventoria_general;
                            Model.OBJDetallleG3.id_md_ref_inte_general3 = item.id_md_ref_general3;
                            Model.OBJDetallleG3.peso = item.peso;
                            Model.OBJDetallleG3.valor = 1;
                            Model.OBJDetallleG3.resultado = 1 * item.peso;
                            Model.OBJDetallleG3.observaciones = "";

                            Model.InsertarGeneral3Detalle(ref MsgRes);
                        }


                        foreach (var item in list4)
                        {
                            Model.OBJDetallleG4.id_md_interventoria_general = Model.id_md_interventoria_general;
                            Model.OBJDetallleG4.id_md_ref_inte_general4 = item.id_md_ref_general4;
                            Model.OBJDetallleG4.peso = item.peso;
                            Model.OBJDetallleG4.valor = 1;
                            Model.OBJDetallleG4.resultado = 1 * item.peso;
                            Model.OBJDetallleG4.observaciones = "";

                            Model.InsertarGeneral4Detalle(ref MsgRes);
                        }



                        return RedirectToAction("InterventoriaGeneralDetalle", "Interventoria", new { id = Model.id_md_interventoria_general, proveedor = Model.nombre_auditado });
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

        public JsonResult GetInteDetalle1(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.interventoria Model = new interventoria();
            Model.id_md_interventoria_general = id.Value;

            List<Managment_md_Ref_General1Result> Lista = new List<Managment_md_Ref_General1Result>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefGeneral1(Model.id_md_interventoria_general);

            var query = Lista;
            List<Managment_md_Ref_General1Result> records = new List<Managment_md_Ref_General1Result>();
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

        public JsonResult GetInteDetalle2(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.interventoria Model = new interventoria();
            Model.id_md_interventoria_general = id.Value;

            List<Managment_md_Ref_General2Result> Lista = new List<Managment_md_Ref_General2Result>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefGeneral2(Model.id_md_interventoria_general);

            var query = Lista;
            List<Managment_md_Ref_General2Result> records = new List<Managment_md_Ref_General2Result>();
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

        public JsonResult GetInteDetalle3(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.interventoria Model = new interventoria();
            Model.id_md_interventoria_general = id.Value;

            List<Managment_md_Ref_General3Result> Lista = new List<Managment_md_Ref_General3Result>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefGeneral3(Model.id_md_interventoria_general);

            var query = Lista;
            List<Managment_md_Ref_General3Result> records = new List<Managment_md_Ref_General3Result>();
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

        public JsonResult GetInteDetalle4(Int32? id, int? page, int? limit)
        {

            Models.Medicamentos.interventoria Model = new interventoria();
            Model.id_md_interventoria_general = id.Value;

            List<Managment_md_Ref_General4Result> Lista = new List<Managment_md_Ref_General4Result>();

            //Model.LlenaLista();
            Lista = Model.DetalleRefGeneral4(Model.id_md_interventoria_general);

            var query = Lista;
            List<Managment_md_Ref_General4Result> records = new List<Managment_md_Ref_General4Result>();
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


        public JsonResult SaveInterventoriaGeneral(Int32 id_md_interventoria_general, String hallazgos)
        {

            String mensaje = "";


            Models.Medicamentos.interventoria Model = new interventoria();
            Model.id_md_interventoria_general = Convert.ToInt32(id_md_interventoria_general);
            vw_total_md_interventoria_detalle OBJ = new vw_total_md_interventoria_detalle();

            OBJ = Model.Total_DetalleInterventoriaGeneralMD(id_md_interventoria_general);


            Decimal? VALOR = OBJ.sum_resultado.Value / OBJ.sum_peso * 100;

            Model.resultado = VALOR;

            Model.OBJInterventoriaGeneral.id_md_interventoria_general = id_md_interventoria_general;
            Model.OBJInterventoriaGeneral.resultado = Model.resultado;
            Model.OBJInterventoriaGeneral.hallazgos = hallazgos;
            Model.OBJInterventoriaGeneral.estado = 1;

            Model.ActualizarInterventoriaGeneralMD(ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                mensaje = "SE INGRESO CORRECTAMENTE....";
                return Json(new { success = true, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mensaje = "ERROR EL INGRESO";
                return Json(new { success = false, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveInterventoriaGeneralDetalle1(Models.Medicamentos.interventoria record)
        {

            List<md_interventoria_general_detalle1> List = new List<md_interventoria_general_detalle1>();
            List = record.GetInterventoriaDetalle1ID(record.id_md_ref_general1, record.id_md_interventoria_general);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJDetallleG1.id_md_interventoria_general_detalle1 = item.id_md_interventoria_general_detalle1;
                    record.OBJDetallleG1.id_md_interventoria_general = item.id_md_interventoria_general;
                    record.OBJDetallleG1.peso = record.peso;
                    record.OBJDetallleG1.valor = record.valor;
                    record.OBJDetallleG1.resultado = record.valor * record.peso;
                    record.OBJDetallleG1.observaciones = record.observaciones;

                    record.ActualizarInterventoriaGeneralDetalle1MD(ref MsgRes);
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

        public JsonResult SaveInterventoriaGeneralDetalle2(Models.Medicamentos.interventoria record)
        {

            List<md_interventoria_general_detalle2> List = new List<md_interventoria_general_detalle2>();
            List = record.GetInterventoriaDetalle2ID(record.id_md_ref_general2, record.id_md_interventoria_general);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJDetallleG2.id_md_interventoria_general_detalle2 = item.id_md_interventoria_general_detalle2;
                    record.OBJDetallleG2.id_md_interventoria_general = item.id_md_interventoria_general;
                    record.OBJDetallleG2.peso = record.peso;
                    record.OBJDetallleG2.valor = record.valor;
                    record.OBJDetallleG2.resultado = record.valor * record.peso;
                    record.OBJDetallleG2.observaciones = record.observaciones;

                    record.ActualizarInterventoriaGeneralDetalle2MD(ref MsgRes);
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

        public JsonResult SaveInterventoriaGeneralDetalle3(Models.Medicamentos.interventoria record)
        {

            List<md_interventoria_general_detalle3> List = new List<md_interventoria_general_detalle3>();
            List = record.GetInterventoriaDetalle3ID(record.id_md_ref_general3, record.id_md_interventoria_general);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJDetallleG3.id_md_interventoria_general_detalle3 = item.id_md_interventoria_general_detalle3;
                    record.OBJDetallleG3.id_md_interventoria_general = item.id_md_interventoria_general;
                    record.OBJDetallleG3.peso = record.peso;
                    record.OBJDetallleG3.valor = record.valor;
                    record.OBJDetallleG3.resultado = record.valor * record.peso;
                    record.OBJDetallleG3.observaciones = record.observaciones;

                    record.ActualizarInterventoriaGeneralDetalle3MD(ref MsgRes);
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

        public JsonResult SaveInterventoriaGeneralDetalle4(Models.Medicamentos.interventoria record)
        {

            List<md_interventoria_general_detalle4> List = new List<md_interventoria_general_detalle4>();
            List = record.GetInterventoriaDetalle4ID(record.id_md_ref_general4, record.id_md_interventoria_general);

            if (List.Count != 0)
            {
                foreach (var item in List)
                {
                    record.OBJDetallleG4.id_md_interventoria_general_detalle4 = item.id_md_interventoria_general_detalle4;
                    record.OBJDetallleG4.id_md_interventoria_general = item.id_md_interventoria_general;
                    record.OBJDetallleG4.peso = record.peso;
                    record.OBJDetallleG4.valor = record.valor;
                    record.OBJDetallleG4.resultado = record.valor * record.peso;
                    record.OBJDetallleG4.observaciones = record.observaciones;

                    record.ActualizarInterventoriaGeneralDetalle4MD(ref MsgRes);
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


    }
}
