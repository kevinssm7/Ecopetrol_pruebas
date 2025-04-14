using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.Medicamentos;
using ECOPETROL_COMMON.ENTIDADES;
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
    public class controlGastoController : Controller
    {


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

        public ActionResult controlgastos()
        {
            Models.Medicamentos.ControldeGastos Model = new Models.Medicamentos.ControldeGastos();

            Model.control_gastosTotal(1);

            return View(Model);
        }

        [HttpPost]
        public ActionResult controlgastos(Models.Medicamentos.ControldeGastos Model)
        {
            md_control_gastos obj = new md_control_gastos();

            if (Model.id_presupuesto_ejecutado == 0)
            {
                obj.valor_facturado_con_aval = Convert.ToDecimal(Model.valor_facturado_con_aval);
                obj.valor_retenido_por_glosa = Convert.ToDecimal(Model.valor_retenido_por_glosa);
                obj.valor_anticipo_generado = Convert.ToDecimal(Model.valor_anticipo_generado);
                obj.mes_ingresado = Model.mesIngresado;
                obj.año = Convert.ToString(Model.ano);
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);
                Model.Insertarcontrol_gasto(obj, ref MsgRes);
            }
            else
            {
                obj.id_presupuesto_ejecutado = Model.id_presupuesto_ejecutado;
                obj.valor_facturado_con_aval = Convert.ToDecimal(Model.valor_facturado_con_aval);
                obj.valor_retenido_por_glosa = Convert.ToDecimal(Model.valor_retenido_por_glosa);
                obj.valor_anticipo_generado = Convert.ToDecimal(Model.valor_anticipo_generado);
                obj.mes_ingresado = Model.mesIngresado;
                obj.año = Convert.ToString(Model.ano);
                obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
                obj.usuario_ingreso = Convert.ToString(SesionVar.UserName);
                Model.ActualizarControlGastos(obj, ref MsgRes);
            }
            
            return View(Model);
        }

        [HttpPost]
        public ActionResult ConsultaGastos(Int32 id,String id2, Models.Medicamentos.ControldeGastos Model)
        {
            var List = Model.control_gastosMes(id,id2);
            md_control_gastos OBJ = new md_control_gastos();
            var opcion = 0;
            if (List == null)
            {
                OBJ.valor_facturado_con_aval = 0;
                OBJ.valor_retenido_por_glosa = 0;
                OBJ.valor_anticipo_generado = 0;
                OBJ.id_presupuesto_ejecutado = 0;
            }
            else
            {
                OBJ.valor_facturado_con_aval = List.valor_facturado_con_aval;
                OBJ.valor_retenido_por_glosa = List.valor_retenido_por_glosa;
                OBJ.valor_anticipo_generado = List.valor_anticipo_generado;
                OBJ.id_presupuesto_ejecutado = List.id_presupuesto_ejecutado;
            }
            return Json(new { success = true, OBJ }, JsonRequestBehavior.AllowGet);
                       
        }

        public JsonResult Get2(Int32 id, Int32 id2, int? page, int? limit)
        {

            Models.Medicamentos.ControldeGastos Model = new ControldeGastos();

            List<Managment_md_control_gastos_tableroResult> Lista = new List<Managment_md_control_gastos_tableroResult>();

            //Model.LlenaLista();
            Lista = Model.tableroControlGastos1(id, Convert.ToInt32(id2));

            var query = Lista;
            List<Managment_md_control_gastos_tableroResult> records = new List<Managment_md_control_gastos_tableroResult>();
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


         public JsonResult Get3(Int32 id, Int32 id2, int? page, int? limit)
        {

            Models.Medicamentos.ControldeGastos Model = new ControldeGastos();

            List<Managment_md_control_gastos_tablero2Result> Lista = new List<Managment_md_control_gastos_tablero2Result>();

            //Model.LlenaLista();
            Lista = Model.tableroControlGastos2(id, Convert.ToInt32(id2));

            //List<Managment_md_control_gastos_tablero2Result> list = new List<Managment_md_control_gastos_tablero2Result>();

            //foreach (var item in Lista)
            //{

            //    Managment_md_control_gastos_tablero2Result obj = new Managment_md_control_gastos_tablero2Result();

            //    obj.año = item.año;
            //    obj.titulo = item.titulo;
            //    String valorEn = $"{ item.ENERO.Value:0.#%}";
            //    obj.ENERO = Convert.ToDouble(valorEn);

            //    list.Add(obj);
            //}

            var query = Lista;
            List<Managment_md_control_gastos_tablero2Result> records = new List<Managment_md_control_gastos_tablero2Result>();
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
        [HttpPost]
        public ActionResult ConsultaGastosTablero1(Int32 id, String id2, Models.Medicamentos.ControldeGastos Model)
        {
            var List = Model.tableroControlGastos1(id, Convert.ToInt32(id2));

            if (List.Count > 0)
            {
                return Json(new { success = true, List }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
          

        }


        public JsonResult GetMeses(string text)
        {
            Models.Medicamentos.ControldeGastos Model = new Models.Medicamentos.ControldeGastos();
            var products = Model.Listameses();
           
            return Json(products, JsonRequestBehavior.AllowGet);
        }

    }
}