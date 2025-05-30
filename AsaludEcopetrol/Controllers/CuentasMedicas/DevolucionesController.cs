using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using OfficeOpenXml;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    [SessionExpireFilter]
    public class DevolucionesController : Controller
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

        public ActionResult BuscarDevoluciones()
        {
            Models.Consulta.Consulta Model = new Models.Consulta.Consulta();
            Session["devsingestionar"] = Model.ListDevSinGestionar;
            return View(Model);
        }

        public ActionResult GestionarDevolucion(String ID)
        {
            Models.CuentasMedicas.DevolucionesFacturas Model = new Models.CuentasMedicas.DevolucionesFacturas();

            Model.id_devolucion_factura = Convert.ToInt32(ID);

            return View(Model);
        }

        [HttpPost]
        public ActionResult GestionarDevolucion(Models.CuentasMedicas.DevolucionesFacturas Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.fecha_facturaok != null)
            {

            }
            else
            {

                variable2 = "FECHA DE FACTURA";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_recepcion_asaludok != null)
            {

            }
            else
            {

                variable2 = "FECHA DE RECEPCION ASALUD";
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
                Model.OBJdEV.id_devolucion_factura = Model.id_devolucion_factura;
                Model.OBJdEV.numero_factura = Model.numero_factura;
                Model.OBJdEV.valor_factura = Model.valor_factura;
                Model.OBJdEV.observaciones = Model.observaciones;
                Model.OBJdEV.fecha_factura = Model.fecha_facturaok;
                Model.OBJdEV.fecha_recepcion_asalud = Model.fecha_recepcion_asaludok;
          
                Model.OBJdEV.usuario_digita = Convert.ToString(SesionVar.UserName);
                Model.OBJdEV.fecha_digita = Convert.ToDateTime(DateTime.Now);

                Model.InsertarDevolucionGestionadas(ref MsgRes);

                if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                {

                    return RedirectToAction("BuscarDevoluciones", "Devoluciones");
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


        public void ExportarLstDevoluciones()
        {
            List<vw_Devoluciones_sin_gestionar> Lista = (List<vw_Devoluciones_sin_gestionar>)Session["devsingestionar"];

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Devolucion Facturas");

            Sheet.Cells["A1:F1"].Style.Font.Bold = true;
            Sheet.Cells["A1:F1"].Style.Font.Size = 14;

            Sheet.Cells["A1"].Value = "NUMERO FACTURA";
            Sheet.Cells["B1"].Value = "NIT";
            Sheet.Cells["C1"].Value = "PRESTADOR";
            Sheet.Cells["D1"].Value = "FECHA  DEVOLUCION";
            Sheet.Cells["E1"].Value = "CIUDAD";
            Sheet.Cells["F1"].Value = "AUDITOR";
            int row = 2;

            foreach (vw_Devoluciones_sin_gestionar item in Lista)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.NumeroFactura;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Nit;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Prestador;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_devolucion.Value.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("E{0}", row)].Value = item.ciudad;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.Auditor;
                row++;
            }

            string namefile = "Devolucion Facturas";
            Sheet.Cells["A:Z"].AutoFitColumns();

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".xls");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

    }
}