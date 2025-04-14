using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.BaseBeneficiarios

{
    [SessionExpireFilter]

    public class BaseBeneficiariosController : Controller
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

        public ActionResult CargueBaseBeneficiariosMasivo(int? rta, string msj)
        {
            ViewBag.meses = BusClass.meses();
            Models.BaseBeneficiarios.BaseBeneficiariosClass Model = new Models.BaseBeneficiarios.BaseBeneficiariosClass();
            ViewBag.idusuario = SesionVar.IDUser;
            ViewBag.rta = rta;
            ViewData["msj"] = msj;

            return View();
        }

        [HttpPost]
        public ActionResult GuardarBaseBeneficiariosMasivo(HttpPostedFileBase file, int mes, int año)
        {
            Models.BaseBeneficiarios.BaseBeneficiariosClass Model = new Models.BaseBeneficiarios.BaseBeneficiariosClass();
            String msg = "";
            var rta = 0;

            List<management_baseBeneficiariosPeriodoValidoResult> list = new List<management_baseBeneficiariosPeriodoValidoResult>();
            Int32 IntContador = 1;
            var periodo = new DateTime(año, mes, 01);

            log_cargues_masivos logMas = new log_cargues_masivos
            {
                fecha_Cargue = DateTime.Now,
                periodo_cargue = periodo,
                nombre_digita = SesionVar.NombreUsuario,
                usuario_digita = SesionVar.UserName,
                tipo_registro = "Beneficiarios"
            };

            log_cargue_base_beneficiarios objLog = new log_cargue_base_beneficiarios
            {
                fecha_digita = DateTime.Now,
                usuario_digita = SesionVar.UserName
            };

            try
            {
                //list = BusClass.GetBeneficiariosPerodoValido(mes, año);

                //if (list.Count() != 0)
                //{
                //    msg = "Ya existe un cargue con este mes y año.";
                //    return RedirectToAction("CargueBaseBeneficiariosMasivo", "BaseBeneficiarios", new
                //    {
                //        rta = 2,
                //        msj = msg
                //    });
                //}

                List<base_beneficiarios> BaBe = new List<base_beneficiarios>();
                CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                var asposeOptions = new Aspose.Cells.LoadOptions
                {
                    MemorySetting = MemorySetting.MemoryPreference
                };

                Workbook wb = new Workbook(file.InputStream, asposeOptions);
                Worksheet worksheet = wb.Worksheets[0];
                Cells cells = worksheet.Cells;
                int MaximaFila = cells.MaxDataRow;

                var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                {
                    CheckMixedValueType = false,
                    ExportColumnName = true,
                    ExportAsString = true
                };

                DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                objLog.periodo = periodo;

                try
                {
                    BusClass.EliminarBaseBeneficiariosEco(ref MsgRes);
                    var inserta = Model.ExcelAPrefacturasReaprobacion(dataTable, mes, año, ref MsgRes);

                    if (inserta != 0)
                    {
                        objLog.estado_cargue = "Completado";
                        rta = 1;
                        msg = "Cargue subido correctamente.";
                    }
                    else
                    {
                        objLog.estado_cargue = "Error de cargue : " + MsgRes.DescriptionResponse;
                        rta = 2;
                        msg = "Errores en el cargue: " + MsgRes.DescriptionResponse;
                    }

                    var idLog = BusClass.InsertarLogBaseBeneficiarios(objLog, ref MsgRes);

                    logMas.id_cargue = idLog;
                    logMas.estado_cargue = objLog.estado_cargue;
                    logMas.fecha_Cargue = DateTime.Now;
                    logMas.periodo_cargue = periodo;
                    logMas.registros_Cargados = BaBe.Count();
                    logMas.nombre_digita = SesionVar.NombreUsuario;
                    logMas.usuario_digita = SesionVar.UserName;
                    logMas.tipo_registro = "Beneficiarios";

                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                    return RedirectToAction("CargueBaseBeneficiariosMasivo", "BaseBeneficiarios", new { rta = rta, msj = msg });
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    objLog.estado_cargue = "Error en el cargue. " + " " + MsgRes.DescriptionResponse + " " + "Linea: " + IntContador.ToString();
                    BusClass.InsertarLogBaseBeneficiarios(objLog, ref MsgRes);
                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                    return RedirectToAction("CargueBaseBeneficiariosMasivo", "BaseBeneficiarios", new { rta = 2, msj = "Revise el archivo." + " " + MsgRes.DescriptionResponse + " " + "Linea: " + IntContador.ToString() });
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                objLog.estado_cargue = "Error en el cargue. " + " " + MsgRes.DescriptionResponse + " " + "Linea: " + IntContador.ToString();
                BusClass.InsertarLogBaseBeneficiarios(objLog, ref MsgRes);
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                return RedirectToAction("CargueBaseBeneficiariosMasivo", "BaseBeneficiarios", new { rta = 2, msj = "Revise el archivo." + " " + MsgRes.DescriptionResponse + " " + "Linea: " + IntContador.ToString() });
            }
        }

        //public base_beneficiarios CargarObjBB(string Linea, int line, int mes, int año, ref MessageResponseOBJ MsgRes)
        //{

        //    string errorDtll = "";
        //    string columna = "";

        //    //Int32 error = 0;
        //    base_beneficiarios ObjBB = new base_beneficiarios();
        //    try
        //    {
        //        string[] array = Linea.Split('|');

        //        int longitud = array.Length;

        //        DateTime Periodo = new DateTime(año, mes, 01);
        //        ObjBB.periodo = Periodo;
        //        var texto = "";

        //        columna = "1";
        //        texto = array[0];

        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {

        //            if (texto.Length <= 50)
        //            {
        //                ObjBB.cod_personal = Convert.ToString(array[0]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 50 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "2";
        //        if (String.IsNullOrEmpty(array[1]))
        //        {
        //            ObjBB.dependencia = 0;
        //        }
        //        else
        //        {
        //            ObjBB.dependencia = int.Parse(array[1]);
        //        }

        //        columna = "3";
        //        texto = array[2];
        //        if (texto.Length <= 150)
        //        {
        //            ObjBB.descripcion_depen = Convert.ToString(array[2]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 150 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "4";
        //        if (String.IsNullOrEmpty(array[3]))
        //        {
        //            ObjBB.distrito = 0;
        //        }
        //        else
        //        {
        //            ObjBB.distrito = int.Parse(array[3]);
        //        }


        //        columna = "5";
        //        texto = array[4];
        //        if (texto.Length <= 200)
        //        {
        //            ObjBB.descripcion_distrito = Convert.ToString(array[4]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 200 caracteres.";
        //            throw new Exception(errorDtll);
        //        }


        //        columna = "6";
        //        texto = array[5];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 250)
        //            {
        //                ObjBB.regsitro = Convert.ToString(array[5]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 250 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "7";
        //        if (String.IsNullOrEmpty(array[6]))
        //        {
        //            ObjBB.historia_clinica = 0;
        //        }
        //        else
        //        {
        //            ObjBB.historia_clinica = int.Parse(array[6]);
        //        }

        //        columna = "8";
        //        texto = array[7];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.Apellidos = Convert.ToString(array[7]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "9";
        //        texto = array[8];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.Nombre = Convert.ToString(array[8]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }


        //        columna = "10";
        //        texto = array[9];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.Clase_de_identificacion = Convert.ToString(array[9]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "11";
        //        texto = array[10];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 100)
        //            {
        //                ObjBB.Numero_identificacion = Convert.ToString(array[10]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 100 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }



        //        columna = "12";
        //        texto = array[11];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.genero = Convert.ToString(array[11]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }


        //        columna = "13";
        //        if (!String.IsNullOrEmpty(array[12]))
        //        {
        //            string strDate = array[12];
        //            string[] dateString = strDate.Split('/');
        //            if (dateString.Count() <= 1)
        //                dateString = strDate.Split('-');

        //            var opcion = dateString[1];
        //            var opcion2 = dateString[2];
        //            var opcion3 = opcion2.Substring(0, 4);

        //            DateTime enter_date = new DateTime();

        //            if (int.Parse(opcion3) < 1900)
        //            {
        //                enter_date = new DateTime(1900, 01, 01);
        //            }
        //            else
        //            {
        //                if (int.Parse(opcion) > 12)
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
        //                }
        //                else
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                }
        //            }
        //            ObjBB.fecha_nacimiento = enter_date;
        //        }
        //        else
        //        {
        //            ObjBB.fecha_nacimiento = new DateTime(1900, 01, 01);
        //        }


        //        columna = "14";
        //        texto = array[13];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 50)
        //            {
        //                ObjBB.edad = Convert.ToString(array[13]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 50 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "15";
        //        texto = array[14];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.grupo_etareo = Convert.ToString(array[14]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "16";
        //        texto = array[15];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.deparrtamento = Convert.ToString(array[15]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "17";
        //        texto = array[16];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 50)
        //            {
        //                ObjBB.cod_ciu = Convert.ToString(array[16]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 50 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "18";
        //        texto = array[17];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.ciudad = Convert.ToString(array[17]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "19";
        //        texto = array[18];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.telefono = Convert.ToString(array[18]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "20";
        //        texto = array[19];

        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.celular = Convert.ToString(array[19]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "21";
        //        texto = array[20];
        //        if (texto.Length <= 200)
        //        {
        //            ObjBB.email = Convert.ToString(array[20]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 200 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "22";
        //        texto = array[21];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 50)
        //            {
        //                ObjBB.ciudad_medico = Convert.ToString(array[21]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 50 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "23";
        //        texto = array[22];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.descripcion_ciudad = Convert.ToString(array[22]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "24";
        //        texto = array[23];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.regional = Convert.ToString(array[23]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "25";
        //        texto = array[24];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.unis = Convert.ToString(array[24]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "26";
        //        texto = array[25];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.estado_civil = Convert.ToString(array[25]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "27";
        //        texto = array[26];
        //        if (texto.Length <= 10)
        //        {
        //            ObjBB.nivel_estudio = Convert.ToString(array[26]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 10 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "28";
        //        texto = array[27];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (array[27] == "")
        //            {
        //                ObjBB.nomina = 0;
        //            }
        //            else
        //            {
        //                ObjBB.nomina = int.Parse(array[27]);
        //            }
        //        }

        //        columna = "29";
        //        texto = array[28];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.descripcion_nomina = Convert.ToString(array[28]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "30";
        //        texto = array[29];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.contrato = Convert.ToString(array[29]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "31";
        //        texto = array[30];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.descripcion_contrato = Convert.ToString(array[30]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "32";
        //        texto = array[31];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.centro_costo = Convert.ToString(array[31]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "33";
        //        texto = array[32];
        //        if (!texto.All(char.IsDigit))
        //        {
        //            errorDtll = columna + ", solo puede ingresar numeros";
        //            throw new Exception(errorDtll);
        //        }
        //        else
        //        {
        //            if (texto.Length <= 50)
        //            {
        //                ObjBB.codifo_funcion_miembro = Convert.ToString(array[32]).ToUpper();
        //            }
        //            else
        //            {
        //                errorDtll = columna + ", solo puede contener 50 caracteres.";
        //                throw new Exception(errorDtll);
        //            }
        //        }

        //        columna = "34";
        //        texto = array[33];
        //        if (texto.Length <= 250)
        //        {
        //            ObjBB.descrip_funcion_denomi = Convert.ToString(array[33]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 250 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "35";

        //        if (!String.IsNullOrEmpty(array[34]))
        //        {
        //            string strDate = array[34];

        //            string len = Convert.ToString(strDate.Length);

        //            string[] dateString = strDate.Split('/');
        //            if (dateString.Count() <= 1)
        //                dateString = strDate.Split('-');

        //            var opcion = dateString[1];
        //            var opcion2 = dateString[2];
        //            var opcion3 = opcion2.Substring(0, 4);

        //            DateTime enter_date = new DateTime();

        //            if (int.Parse(opcion3) < 1900)
        //            {
        //                enter_date = new DateTime(1900, 01, 01);
        //            }
        //            else
        //            {
        //                if (int.Parse(opcion) > 12)
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
        //                }
        //                else
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                }
        //            }

        //            ObjBB.fecha_vin_o_incrip = enter_date;
        //        }
        //        else
        //        {
        //            ObjBB.fecha_vin_o_incrip = new DateTime(1900, 01, 01);
        //        }

        //        columna = "36";
        //        if (!String.IsNullOrEmpty(array[35]))
        //        {
        //            string strDate = array[35];
        //            string[] dateString = strDate.Split('/');
        //            if (dateString.Count() <= 1)
        //                dateString = strDate.Split('-');

        //            var opcion = dateString[1];
        //            var opcion2 = dateString[2];
        //            var opcion3 = opcion2.Substring(0, 4);

        //            DateTime enter_date = new DateTime();
        //            if (int.Parse(opcion3) < 1900)
        //            {
        //                enter_date = new DateTime(1900, 01, 01);
        //            }
        //            else
        //            {
        //                if (int.Parse(opcion) > 12)
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
        //                }
        //                else
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                }
        //            }
        //            ObjBB.inicio_medico = enter_date;
        //        }
        //        else
        //        {
        //            ObjBB.inicio_medico = new DateTime(1900, 01, 01);
        //        }

        //        columna = "37";
        //        if (!String.IsNullOrEmpty(array[36]))
        //        {
        //            string strDate = array[36];
        //            string[] dateString = strDate.Split('/');
        //            if (dateString.Count() <= 1)
        //                dateString = strDate.Split('-');

        //            var opcion = dateString[1];
        //            var opcion2 = dateString[2];
        //            var opcion3 = opcion2.Substring(0, 4);

        //            DateTime enter_date = new DateTime();

        //            if (int.Parse(opcion3) < 1900)
        //            {
        //                enter_date = new DateTime(1900, 01, 01);
        //            }
        //            else
        //            {
        //                if (int.Parse(opcion) > 12)
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
        //                }
        //                else
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                }
        //            }
        //            ObjBB.terminacion_medico = enter_date;
        //        }
        //        else
        //        {
        //            ObjBB.terminacion_medico = new DateTime(1900, 01, 01);
        //        }

        //        columna = "38";
        //        if (!String.IsNullOrEmpty(array[37]))
        //        {
        //            string strDate = array[37];
        //            string[] dateString = strDate.Split('/');
        //            if (dateString.Count() <= 1)
        //                dateString = strDate.Split('-');
        //            var opcion = dateString[1];
        //            var opcion2 = dateString[2];
        //            var opcion3 = opcion2.Substring(0, 4);

        //            DateTime enter_date = new DateTime();

        //            if (int.Parse(opcion3) < 1900)
        //            {
        //                enter_date = new DateTime(1900, 01, 01);
        //            }
        //            else
        //            {
        //                if (int.Parse(opcion) > 12)
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
        //                }
        //                else
        //                {
        //                    enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
        //                }
        //            }
        //            ObjBB.ingreso_ECP = enter_date;
        //        }
        //        else
        //        {
        //            ObjBB.ingreso_ECP = new DateTime(1900, 01, 01);
        //        }

        //        columna = "39";
        //        texto = array[38];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.estado = Convert.ToString(array[38]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "40";
        //        texto = array[39];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.costo = Convert.ToString(array[39]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "41";
        //        texto = array[40];
        //        if (texto.Length <= 350)
        //        {
        //            ObjBB.direccion = Convert.ToString(array[40]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 350 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "42";
        //        texto = array[41];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.reg_personal = Convert.ToString(array[41]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "43";
        //        texto = array[42];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.sub_personal = Convert.ToString(array[42]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "44";
        //        texto = array[43];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.tipo_salud = Convert.ToString(array[43]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "45";
        //        texto = array[44];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.tipo1 = Convert.ToString(array[44]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "46";
        //        texto = array[45];
        //        if (texto.Length <= 100)
        //        {
        //            ObjBB.tipo2 = Convert.ToString(array[45]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 100 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "47";
        //        if (array[46] == "")
        //        {
        //            ObjBB.contador = 0;
        //        }
        //        else
        //        {
        //            ObjBB.contador = int.Parse(array[46]);
        //        }

        //        columna = "48";
        //        texto = array[47];
        //        if (texto.Length <= 200)
        //        {
        //            ObjBB.mega_nombre = Convert.ToString(array[47]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 200 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        columna = "49";
        //        texto = array[48];
        //        if (texto.Length <= 50)
        //        {
        //            ObjBB.mega_documento = Convert.ToString(array[48]).ToUpper();
        //        }
        //        else
        //        {
        //            errorDtll = columna + ", solo puede contener 50 caracteres.";
        //            throw new Exception(errorDtll);
        //        }

        //        ObjBB.fecha_ingreso_cargue = DateTime.Now;

        //        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (errorDtll != "" && errorDtll != null)
        //        {
        //            MsgRes.DescriptionResponse = "Error en la columna: " + errorDtll;
        //        }
        //        else
        //        {
        //            MsgRes.DescriptionResponse = "Error en la  columna: " + columna;
        //        }
        //        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
        //        MsgRes.CodeError = ex.Message;

        //        throw;
        //    }
        //    return ObjBB;
        //}

        public ActionResult TableroBaseBeneficiarios()
        {
            List<management_baseBeneficiarios_tableroControlResult> Listado = new List<management_baseBeneficiarios_tableroControlResult>();
            Listado = BusClass.TraerListadoBaseBeneficiarios();
            ViewBag.rol = SesionVar.ROL;
            return View(Listado);
        }

        public PartialViewResult ConsolidadoBaseBeneficiarios(string periodo)
        {
            List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> listado = new List<management_baseBeneficiarios_consolidadoDetallePeriodoResult>();
            listado = BusClass.TraerListadoBaseBeneficiariosConsolidado(periodo);
            Session["listadoBaseBeneficiarios"] = listado;
            return PartialView(listado);
        }

        public JsonResult EliminarPeriodoBaseBeneficiarios(string periodo)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                var elimina = BusClass.EliminarBaseBeneficiariosPeriodo(periodo);

                if (elimina != 0)
                {
                    log_cargue_base_beneficiarios log = new log_cargue_base_beneficiarios();
                    log.periodo = Convert.ToDateTime(periodo);
                    log.estado_cargue = $"Cargue del periodo: {periodo} eliminado";
                    log.usuario_digita = SesionVar.UserName;
                    log.fecha_digita = DateTime.Now;

                    var insertaLog = BusClass.InsertarLogBaseBeneficiarios(log, ref MsgRes);
                    mensaje = "BASE BENEFICIARIOS ELIMINADO CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    mensaje = "ERROR EN LA ELIMINACIÓN DE BASE BENEFICIARIOS";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR EN LA ELIMINACIÓN DE BASE BENEFICIARIOS: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public void ExcelConsolidadoBB()
        {
            try
            {
                List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> listareporte = new List<management_baseBeneficiarios_consolidadoDetallePeriodoResult>();
                listareporte = (List<management_baseBeneficiarios_consolidadoDetallePeriodoResult>)Session["listadoBaseBeneficiarios"];

                if (listareporte.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reporte Consolidado Base beneficiarios");

                    Sheet.Cells["A1:H1"].Style.Font.Bold = true;
                    System.Drawing.Color colFromHex = System.Drawing.Color.FromArgb(22, 54, 92);
                    Sheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    Sheet.Cells["A1:H1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    Sheet.Cells["A1:H1"].Style.Font.Name = "Century Gothic";
                    Sheet.Cells["A1:H1"].Style.Font.Family = 12;

                    Sheet.Cells["A1"].Value = "Conteo";
                    Sheet.Cells["B1"].Value = "Clase de identificación";
                    Sheet.Cells["C1"].Value = "Genero";
                    Sheet.Cells["D1"].Value = "Grupo etereo";
                    Sheet.Cells["E1"].Value = "Departamento";
                    Sheet.Cells["F1"].Value = "Regional";
                    Sheet.Cells["G1"].Value = "Unis";
                    Sheet.Cells["H1"].Value = "Estado";

                    int row = 2;
                    foreach (management_baseBeneficiarios_consolidadoDetallePeriodoResult item in listareporte)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.conteo;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.clase_de_identificacion;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.genero;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.grupo_etareo;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.deparrtamento;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.regional;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.unis;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.estado;
                        row++;
                    }
                    Sheet.Cells["A:H"].AutoFitColumns();


                    Response.Clear();
                    Response.ContentType = "application/excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReporteConsolidadoBB_" + DateTime.Now + ".xlsx");
                    Response.BinaryWrite(Ep.GetAsByteArray());
                    Response.End();
                }
                else
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                             "window.alert('NO HAY DATOS POR MOSTRAR');" +
                             "</script> ";
                    Response.Write(rta);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Error: " + ex.Message;

                string rta = "<script LANGUAGE='JavaScript'>" +
                               "window.alert('ERROR EN LA DESCARGA');" +
                               "</script> ";
                Response.Write(rta);
                Response.End();
                //throw new Exception(mensaje);
            }
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^0-9A-Za-z]", ",", RegexOptions.None);
        }

        public ActionResult CargueCorreosPPE(int? rta, string msj)
        {

            Models.General General = new Models.General();
            if (rta == 1)
            {
                ViewData["alerta"] = General.MsgRespuesta("success", "Transaccion Exitosa!", msj);
            }
            else if (rta == 2)
            {
                ViewData["alerta"] = General.MsgRespuesta("danger", "Error!", msj);
            }
            else
            {
                ViewData["alerta"] = "";
            }

            return View();
        }

        [HttpPost]
        public JsonResult CargueCorreosPPE(HttpPostedFileBase file)
        {
            Models.BaseBeneficiarios.BaseBeneficiariosClass Model = new Models.BaseBeneficiarios.BaseBeneficiariosClass();
            if (this.Request.Files["file"].ContentLength > 0)
            {
                try
                {
                    using (var excel = new ExcelPackage(file.InputStream))
                    {
                        var tbl = new DataTable();
                        var ws = excel.Workbook.Worksheets.First();
                        var hasHeader = true;
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text
                                : String.Format("Column {0}", firstRowCell.Start.Column));

                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.NewRow();
                            foreach (var cell in wsRow)
                                row[cell.Start.Column - 1] = cell.Text;
                            tbl.Rows.Add(row);
                        }
                        var eliminar = Model.eliminarCorreosPPE(ref MsgRes);

                        if (eliminar != 0)
                        {
                            var pasa = Model.ExcelCorreosPPE(tbl, ref MsgRes);
                        }
                        var resultado = MsgRes.ResponseType;
                        var mensajeSalida = MsgRes.DescriptionResponse;

                        if (resultado == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            var mensaje = "REGISTROS PPE INGRESADOS CORRECTAMENTE";
                            return Json(new { success = true, message = mensaje, rta = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + MsgRes.DescriptionResponse;
                            return Json(new { success = false, message = mensaje, rta = 2 }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }
                catch (Exception e)
                {
                    var error = e.Message;
                    var mensaje = MsgRes.DescriptionResponse;
                    return Json(new { success = false, message = mensaje, rta = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var mensaje = "SELECCIONE UN ARCHIVO TIPO EXCEL.";
                return Json(new { success = false, message = mensaje, rta = 2 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}