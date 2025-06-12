using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Medicamentos;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Medicamentos
{
    [SessionExpireFilter]
    public class herramientaTecnologicaController : Controller
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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion



        // GET: herramientaTecnologica
        public ActionResult BuscarHerramienta(Int32? id )
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Int32 proveedor = Convert.ToInt32(id);

            Model.IndicadoresProvvedorHerramientas(proveedor);

            

            return View(Model);
        }

        public ActionResult ingreso()
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Model.fecha_auditoria = Convert.ToDateTime(DateTime.Now);
            Model.fecha_auditoriaOK = Convert.ToDateTime(DateTime.Now);
            Model.nombre_auditor = Convert.ToString(SesionVar.NombreUsuario);

            return View(Model);
        }

        public ActionResult Tabla1(Int32 id, Int32 tipo)
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Model.id_herramienta_tec_med = id;

            if (tipo == 1)
            {
                List<md_ref_tabla1> listt1 = new List<md_ref_tabla1>();

                listt1 = Model.ref_tabla1();
                List<md_herramienta_tec_detalle_t1> listaT1 = new List<md_herramienta_tec_detalle_t1>();
                foreach (var item in listt1)
                {
                    md_herramienta_tec_detalle_t1 obj = new md_herramienta_tec_detalle_t1();

                    obj.id_herramienta_tec_med = id;
                    obj.id_tabla1 = item.id_tabla1;
                    obj.valor = 1;
                    obj.resultado = Convert.ToInt32(item.peso) * obj.valor;
                    obj.observaciones = "";

                    listaT1.Add(obj);

                }

                Model.InsertarDetallet1(listaT1, ref MsgRes);
            }
            else
            {

            }






            return View(Model);
        }
        public ActionResult Tabla2(Int32 id, Int32 tipo)
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Model.id_herramienta_tec_med = id;

            if (tipo == 1)
            {
                List<md_ref_tabla2> listt1 = new List<md_ref_tabla2>();

                listt1 = Model.ref_tabla2();

                List<md_herramienta_tec_detalle_t2> listaT2 = new List<md_herramienta_tec_detalle_t2>();
                foreach (var item in listt1)
                {
                    md_herramienta_tec_detalle_t2 obj = new md_herramienta_tec_detalle_t2();

                    obj.id_herramienta_tec_med = id;
                    obj.id_tabla2 = item.id_tabla2;
                    obj.valor = 1;
                    obj.resultado = Convert.ToInt32(item.peso) * obj.valor;
                    obj.observaciones = "";

                    listaT2.Add(obj);

                }

                Model.InsertarDetallet2(listaT2, ref MsgRes);
            }

            return View(Model);
        }
        public ActionResult Tabla3(Int32 id, Int32 tipo)
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Model.id_herramienta_tec_med = id;

            if (tipo == 1)
            {
                List<md_ref_tabla3> listt1 = new List<md_ref_tabla3>();

                listt1 = Model.ref_tabla3();
                List<md_herramienta_tec_detalle_t3> listaT3 = new List<md_herramienta_tec_detalle_t3>();
                foreach (var item in listt1)
                {
                    md_herramienta_tec_detalle_t3 obj = new md_herramienta_tec_detalle_t3();

                    obj.id_herramienta_tec_med = id;
                    obj.id_tabla3 = item.id_tabla3;
                    obj.valor = 1;
                    obj.resultado = Convert.ToInt32(item.peso) * obj.valor;
                    obj.observaciones = "";

                    listaT3.Add(obj);
                }
                Model.InsertarDetallet3(listaT3, ref MsgRes);
            }

            return View(Model);
        }
        public ActionResult Tabla4(Int32 id, Int32 tipo)
        {
            Models.Medicamentos.herramientasTec Model = new Models.Medicamentos.herramientasTec();

            Model.id_herramienta_tec_med = id;

            if (tipo == 1)
            {
                List<md_ref_tabla4> listt1 = new List<md_ref_tabla4>();

                listt1 = Model.ref_tabla4();
                List<md_herramienta_tec_detalle_t4> listaT4 = new List<md_herramienta_tec_detalle_t4>();
                foreach (var item in listt1)
                {
                    md_herramienta_tec_detalle_t4 obj = new md_herramienta_tec_detalle_t4();

                    obj.id_herramienta_tec_med = id;
                    obj.id_tabla4 = item.id_tabla4;
                    obj.valor = 1;
                    obj.resultado = Convert.ToInt32(item.peso) * obj.valor;
                    obj.observaciones = "";

                    listaT4.Add(obj);

                }

                Model.InsertarDetallet4(listaT4, ref MsgRes);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult BuscarHerramienta(Models.Medicamentos.herramientasTec Model)
        {
            Int32 proveedor = Convert.ToInt32(Model.nombre_auditor);

            Model.IndicadoresProvvedorHerramientas(proveedor);

            return View(Model);
        }

        [HttpPost]
        public ActionResult ingreso(Models.Medicamentos.herramientasTec Model)
        {
            md_herramienta_tec obj = new md_herramienta_tec();

            obj.fecha_auditoria = Model.fecha_auditoriaOK;
            obj.nombre_auditor = SesionVar.NombreUsuario;
            obj.ciudad = Convert.ToString(Model.ciudad);
            obj.nombre_auditado = Convert.ToInt32(Model.nombre_auditado);
            obj.nombre_farmacia = Model.nombre_farmacia;
            obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
            obj.usuario_ingreso = SesionVar.UserName;

            var id = Model.InsertarHerramientaTecnologica(obj, ref MsgRes);

            if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
            {
                return RedirectToAction("BuscarHerramienta", "herramientaTecnologica", new { id = Model.nombre_auditado });
            }
            else
            {

            }
            return View(Model);
        }

        public JsonResult GetT1(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T1> Lista = new List<vw_md_detalle_T1>();


            Lista = Model.Tabla1Detalle(1, Id);

            var query = Lista;
            List<vw_md_detalle_T1> records = new List<vw_md_detalle_T1>();
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

        public JsonResult GetT2(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T1> Lista = new List<vw_md_detalle_T1>();


            Lista = Model.Tabla1Detalle(2, Id);

            var query = Lista;
            List<vw_md_detalle_T1> records = new List<vw_md_detalle_T1>();
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

        public JsonResult GetT3(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T1> Lista = new List<vw_md_detalle_T1>();


            Lista = Model.Tabla1Detalle(3, Id);

            var query = Lista;
            List<vw_md_detalle_T1> records = new List<vw_md_detalle_T1>();
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

        public JsonResult GetT4(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T2> Lista = new List<vw_md_detalle_T2>();


            Lista = Model.Tabla2Detalle(1, Id);

            var query = Lista;
            List<vw_md_detalle_T2> records = new List<vw_md_detalle_T2>();
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

        public JsonResult GetT5(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T2> Lista = new List<vw_md_detalle_T2>();


            Lista = Model.Tabla2Detalle(2, Id);

            var query = Lista;
            List<vw_md_detalle_T2> records = new List<vw_md_detalle_T2>();
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

        public JsonResult GetT6(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T2> Lista = new List<vw_md_detalle_T2>();


            Lista = Model.Tabla2Detalle(3, Id);

            var query = Lista;
            List<vw_md_detalle_T2> records = new List<vw_md_detalle_T2>();
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

        public JsonResult GetT7(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T2> Lista = new List<vw_md_detalle_T2>();


            Lista = Model.Tabla2Detalle(4, Id);

            var query = Lista;
            List<vw_md_detalle_T2> records = new List<vw_md_detalle_T2>();
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

        public JsonResult GetT8(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T3> Lista = new List<vw_md_detalle_T3>();


            Lista = Model.Tabla3Detalle(1, Id);

            var query = Lista;
            List<vw_md_detalle_T3> records = new List<vw_md_detalle_T3>();
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
        public JsonResult GetT9(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T3> Lista = new List<vw_md_detalle_T3>();


            Lista = Model.Tabla3Detalle(2, Id);

            var query = Lista;
            List<vw_md_detalle_T3> records = new List<vw_md_detalle_T3>();
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
        public JsonResult GetT10(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T3> Lista = new List<vw_md_detalle_T3>();


            Lista = Model.Tabla3Detalle(3, Id);

            var query = Lista;
            List<vw_md_detalle_T3> records = new List<vw_md_detalle_T3>();
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

        public JsonResult GetT11(Int32 Id, int? page, int? limit)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_categoria = Id;

            List<vw_md_detalle_T4> Lista = new List<vw_md_detalle_T4>();


            Lista = Model.Tabla4Detalle(1, Id);

            var query = Lista;
            List<vw_md_detalle_T4> records = new List<vw_md_detalle_T4>();
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



        //public JsonResult GetTeams(int Id,int Id2, int? page, int? limit)
        //{


        //    return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult Save1(Models.Medicamentos.herramientasTec record)
        {
            md_herramienta_tec_detalle_t1 obj = new md_herramienta_tec_detalle_t1();

            obj.id_md_detalle_tabla1 = record.id_md_detalle_tabla1;
            obj.valor = record.valor;
            obj.resultado = record.valor * record.peso;
            obj.observaciones = record.observaciones;

            record.ActualizarDetallet1(obj, ref MsgRes);

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
        public JsonResult Save2(Models.Medicamentos.herramientasTec record)
        {
            md_herramienta_tec_detalle_t2 obj = new md_herramienta_tec_detalle_t2();

            obj.id_md_detalle_tabla2 = record.id_md_detalle_tabla2;
            obj.valor = record.valor;
            obj.resultado = record.valor * record.peso;
            obj.observaciones = record.observaciones;

            record.ActualizarDetallet2(obj, ref MsgRes);

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
        public JsonResult Save3(Models.Medicamentos.herramientasTec record)
        {
            md_herramienta_tec_detalle_t3 obj = new md_herramienta_tec_detalle_t3();

            obj.id_md_detalle_tabla3 = record.id_md_detalle_tabla3;
            obj.valor = record.valor;
            obj.resultado = record.valor * record.peso;
            obj.observaciones = record.observaciones;

            record.ActualizarDetallet3(obj, ref MsgRes);

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
        public JsonResult Save4(Models.Medicamentos.herramientasTec record)
        {
            md_herramienta_tec_detalle_t4 obj = new md_herramienta_tec_detalle_t4();

            obj.id_md_detalle_tabla4 = record.id_md_detalle_tabla4;
            obj.valor = record.valor;
            obj.resultado = record.valor * record.peso;
            obj.observaciones = record.observaciones;

            record.ActualizarDetallet4(obj, ref MsgRes);

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
        public JsonResult SaveHallazgosT1(Int32 id_herramienta_tec_med, String hallazgos)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_md_detalle_tabla1 = Convert.ToInt32(id_herramienta_tec_med);

            var lista = Model.totalesT1(id_herramienta_tec_med);

            Decimal VALOR = lista.sum_resultado.Value / Convert.ToDecimal(lista.suma_peso) * 100;

            md_herramienta_tec OBJaCT = new md_herramienta_tec();
            
            OBJaCT.id_herramienta_tec_med = Model.id_md_detalle_tabla1;
            OBJaCT.hallazgosT1 = hallazgos;
            OBJaCT.resultadoT1 = VALOR;

            Model.ActualizarGeneral1(OBJaCT, ref MsgRes);

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
        public JsonResult SaveHallazgosT2(Int32 id_herramienta_tec_med, String hallazgos)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_herramienta_tec_med = Convert.ToInt32(id_herramienta_tec_med);

            var lista = Model.totalesT2(id_herramienta_tec_med);

            Decimal VALOR = lista.sum_resultado.Value / Convert.ToDecimal(lista.suma_peso) * 100;

            md_herramienta_tec OBJaCT = new md_herramienta_tec();

            OBJaCT.id_herramienta_tec_med = Model.id_herramienta_tec_med;
            OBJaCT.hallazgosT2 = hallazgos;
            OBJaCT.resultadoT2 = VALOR;

            Model.ActualizarGeneral2(OBJaCT, ref MsgRes);

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
        public JsonResult SaveHallazgosT3(Int32 id_herramienta_tec_med, String hallazgos)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_herramienta_tec_med = Convert.ToInt32(id_herramienta_tec_med);

            var lista = Model.totalesT3(id_herramienta_tec_med);

            Decimal VALOR = lista.sum_resultado.Value / Convert.ToDecimal(lista.suma_peso) * 100;


            md_herramienta_tec OBJaCT = new md_herramienta_tec();

            OBJaCT.id_herramienta_tec_med = Model.id_herramienta_tec_med;
            OBJaCT.hallazgosT3 = hallazgos;
            OBJaCT.resultadoT3 = VALOR;

            Model.ActualizarGeneral3(OBJaCT, ref MsgRes);

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
        public JsonResult SaveHallazgosT4(Int32 id_herramienta_tec_med, String hallazgos)
        {

            Models.Medicamentos.herramientasTec Model = new herramientasTec();

            Model.id_herramienta_tec_med = Convert.ToInt32(id_herramienta_tec_med);

            var lista = Model.totalesT4(id_herramienta_tec_med);

            Decimal VALOR = lista.sum_resultado.Value / Convert.ToDecimal(lista.suma_peso) * 100;

            md_herramienta_tec OBJaCT = new md_herramienta_tec();


            OBJaCT.id_herramienta_tec_med = Model.id_herramienta_tec_med;
            OBJaCT.hallazgosT4 = hallazgos;
            OBJaCT.resultadoT4 = VALOR;

            Model.ActualizarGeneral4(OBJaCT, ref MsgRes);

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