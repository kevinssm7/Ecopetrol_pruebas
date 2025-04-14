using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using LinqToExcel;

namespace AsaludEcopetrol.Controllers.CuentasMedicas
{
    [SessionExpireFilter]
    public class ContratacionController : Controller
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

        public ActionResult CargueContratacion()
        {
            ViewData["alerta"] = "";
            ViewData["rta"] = 0;
            ViewBag.meses = BusClass.meses();
            return View();
        }

        [HttpPost]
        public ActionResult CargueContratacion(int mes, int año, HttpPostedFileBase archivo)
        {
            Models.CuentasMedicas.Contratacion Model = new Models.CuentasMedicas.Contratacion();
            /* Agrega el archivo y lo guarda */

            var fileName = System.IO.Path.GetFileName(archivo.FileName);
            var path = System.IO.Path.Combine(Server.MapPath("~/Resources"), fileName);
            archivo.SaveAs(path);

            var registro = Model.getcarguecontratacion(mes, año);
            if (registro == null)
            {
                try
                {
                    /*Luego que guarda el archivo, setea los valores y agrega a base de datos */
                    List<contratacion_cargue_dtll> ListaContratacion = Excelaentidad(path);
                    if (ListaContratacion.Count > 0)
                    {
                        contratacion_cargue_base obj = new contratacion_cargue_base();
                        obj.periodo_mes = mes;
                        obj.periodo_año = año;
                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;
                        Model.InsertarCargueContratacion(obj, ListaContratacion, ref MsgRes);
                    }

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["alerta"] = "Datos cargados exitosamente.";
                        ViewData["rta"] = 1;
                    }
                    else
                    {
                        ViewData["alerta"] = MsgRes.DescriptionResponse;
                        ViewData["rta"] = 2;
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception ex)
                {
                    if(MsgRes == null)
                    {
                        ViewData["alerta"] = ex.Message;
                    }else
                    {
                        ViewData["alerta"] = MsgRes.DescriptionResponse;
                    }
                    
                    ViewData["rta"] = 2;
                    System.IO.File.Delete(path);
                }
            }
            else
            {
                ViewData["alerta"] = "Ya se encuentra registrado un cargue con el mismo mes, y el mismo año. si desea reeemplazarlo, contacte al administrador.";
                ViewData["rta"] = 2;
                System.IO.File.Delete(path);
            }


            ViewBag.meses = BusClass.meses();
            return View();
        }

        public List<contratacion_cargue_dtll> Excelaentidad(string pathDelFicheroExcel)
        {
            int fila = 0;
            string nom_columna = "";
            try
            {
                List<contratacion_cargue_dtll> resultado = new List<contratacion_cargue_dtll>();
                var book = new ExcelQueryFactory(pathDelFicheroExcel);
                var EData1 = (from c in book.WorksheetRange("A1", "BE50000", "Base Contratacion") select c).ToList();

                for (var i = 0; i < EData1.Count; i++)
                {
                    contratacion_cargue_dtll obj = new contratacion_cargue_dtll();
                    var item = EData1[i];

                    fila = i + 1;

                    nom_columna = "Jerarquía"; obj.Jerarquía = item[0];
                    nom_columna = "Contrato Central"; obj.Contrato_Central = item[1];
                    nom_columna = "Contrato Operativo"; obj.Contrato_Operativo = item[2];
                    nom_columna = "Periodo Finalización"; obj.Periodo_Finalizacion = item[3];
                    nom_columna = "Fecha Suscripción"; obj.Fecha_Suscripción = Convert.ToDateTime(item[4]);
                    nom_columna = "Fecha Inicial"; obj.Fecha_Inicial = Convert.ToDateTime(item[5]);
                    nom_columna = "Fecha Final"; obj.Fecha_Final = Convert.ToDateTime(item[6]);
                    nom_columna = "Estado Contrato"; obj.Estado_Contrato = item[7];
                    nom_columna = "Clase Documento"; obj.Clase_Documento = item[8];
                    nom_columna = "Tipo Contrato Central"; obj.Tipo_Contrato_Central = item[9];
                    nom_columna = "Sociedad Contrato Central"; obj.Sociedad_Contrato_Central = item[10];
                    nom_columna = "Area Trámite"; obj.Area_Trámite = item[11];
                    nom_columna = "Grupo Compras"; obj.Grupo_Compras = item[12];
                    nom_columna = "Registro Gestor Contratación"; obj.Registro_Gestor_Contratación = item[13];
                    nom_columna = "Nombre Gestor Contratación"; obj.Nombre_Gestor_Contratación = item[14];
                    nom_columna = "Registro Funcionario Autorizado"; obj.Registro_Funcionario_Autorizado = item[15];
                    nom_columna = "Nombre Funcionario Autorizado"; obj.Nombre_Funcionario_Autorizado = item[16];
                    nom_columna = "Tipo Cuantía"; obj.Tipo_Cuantía = item[17];
                    nom_columna = "Modalidad Contratación"; obj.Modalidad_Contratación = item[18];
                    nom_columna = "Objeto Contrato"; obj.Objeto_Contrato = item[19];
                    nom_columna = "Subcategoría Contrato"; obj.Subcategoría_Contrato = item[20];
                    nom_columna = "Subcategoría Contrato Nombre"; obj.Subcategoría_Contrato_Nombre = item[21];
                    nom_columna = "Categoría Nombre"; obj.Categoría_Nombre = item[22];
                    nom_columna = "Dominio"; obj.Dominio = item[23];
                    nom_columna = "Lugar Ejecución Contrato"; obj.Lugar_Ejecución_Contrato = item[24];
                    nom_columna = "Departamento Ejecución Contrato"; obj.Departamento_Ejecucion_Contrato = item[25];
                    nom_columna = "Subregional Ejecución Contrato"; obj.Subregional_Ejecucion_Contrato = item[26];
                    nom_columna = "Regional Ejecución Contrato"; obj.Regional_Ejecución_Contrato = item[27];
                    nom_columna = "País De Interés"; obj.País_De_Interés = item[28];
                    nom_columna = "Area Interés"; obj.Area_Interés = item[29];
                    nom_columna = "Vicepresidencia Ejecutiva"; obj.Vicepresidencia_Ejecutiva = item[30];
                    nom_columna = "Vicepresidencia Operativa"; obj.Vicepresidencia_Operativa = item[31];
                    nom_columna = "Negocio"; obj.Negocio = item[32];
                    nom_columna = "Gerencia"; obj.Gerencia = item[33];
                    nom_columna = "Registro Funcionario Solicitante"; obj.Registro_Funcionario_Solicitante = item[34];
                    nom_columna = "Nombre Funcionario Solicitante"; obj.Nombre_Funcionario_Solicitante = item[35];
                    nom_columna = "Registro Administrador / Funcionario Calificador"; obj.Registro_Administrador_Funcionario_Calificador = item[36];
                    nom_columna = "Nombre Administrador / Funcionario Calificador"; obj.Nombre_Administrador_Funcionario_Calificador = item[37];
                    nom_columna = "Interventor"; obj.Interventor = item[38];
                    nom_columna = "Nombre Interventor"; obj.Nombre_Interventor = item[39];
                    nom_columna = "Registro Apoyo Transaccional / Funcionario Seguimiento"; obj.Registro_Apoyo_Transaccional_Funcionario_Seguimiento = item[40];
                    nom_columna = "Nombre Apoyo Transaccional / Funcionario Seguimiento"; obj.Nombre_Apoyo_Transaccional_Funcionario_Seguimiento = item[41];
                    nom_columna = "Nit Proveedor"; obj.Nit_Proveedor = item[42];
                    nom_columna = "Nombre Proveedor"; obj.Nombre_Proveedor = item[43];
                    nom_columna = "Tipo De Industria"; obj.Tipo_De_Industria = item[44];
                    nom_columna = "Ciudad Proveedor"; obj.Ciudad_Proveedor = item[45];
                    nom_columna = "Departamento Proveedor"; obj.Departamento_Proveedor = item[46];
                    nom_columna = "Subregional Proveedor"; obj.Subregional_Proveedor = item[47];
                    nom_columna = "Regional Proveedor"; obj.Regional_Proveedor = item[48];
                    nom_columna = "Empresa"; obj.Empresa = item[49];
                    nom_columna = "Valor Inicial Pesos"; obj.Valor_Inicial_Pesos = Convert.ToDecimal(item[50]);
                    nom_columna = "Valor Actual Liberado Pesos"; obj.Valor_Actual_Liberado_Pesos = Convert.ToDecimal(item[51]);
                    nom_columna = "Valor Total Neto Ejecutado"; obj.Valor_Total_Neto_Ejecutado = Convert.ToDecimal(item[52]);
                    nom_columna = "Valor Total Ejecutado"; obj.Valor_Total_Ejecutado = Convert.ToDecimal(item[53]);
                    nom_columna = "Valor Total Ejecutado 2019"; obj.Valor_Total_Ejecutado_2019 = Convert.ToDecimal(item[54]);
                    nom_columna = "Valor Total Suscrito Para Ejecución 2019"; obj.Valor_Total_Suscrito_Para_Ejecución_2019 = Convert.ToDecimal(item[55]);
                    nom_columna = "Valor Total Suscrito Para Ejecución 2020"; obj.Valor_Total_Suscrito_Para_Ejecución_2020 = Convert.ToDecimal(item[56]); 
                    resultado.Add(obj);
                }

                book.Dispose();
                return resultado;
            }
            catch (Exception ex)
            {
                string msg = "<strong>Fila:</strong> " + fila+ " <strong>Columna:</strong> '" + nom_columna+ "' <strong>Mensaje:</strong> " + ex.Message;
                MsgRes.DescriptionResponse = msg;
                throw; 
            }
        }
    }
}