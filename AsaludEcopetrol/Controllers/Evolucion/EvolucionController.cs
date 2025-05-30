using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Evolucion
{
    [SessionExpireFilter]
    public class EvolucionController : Controller
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

        #region METODOS
        Models.Evolucion.Evolucion Model = new Models.Evolucion.Evolucion();

        public ActionResult Evolucion(String idConcu, Models.Evolucion.Evolucion Model)
        {
            //var idConcurrencia = Models.Evolucion.Evolucion;

            //Models.Evolucion.Evolucion Model = new Models.Evolucion.Evolucion();
            var prueba = 0;

            try
            {

                if (!(String.IsNullOrEmpty(idConcu)))
                {
                    ViewBag.id_concurrencia = idConcu;

                    Model.id_concurrencia = (Convert.ToInt32(idConcu));
                    Model.ConsultaEvolucion(Convert.ToInt32(idConcu));
                    Model.ConsultaAfiliado(Convert.ToInt32(idConcu));
                    //Model.ConsultaCohorteAfil(Model.id_afi);

                    var idAfi = Model.id_afi;
                    List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
                    management_cohortesBeneficiarioResult cohortes = new management_cohortesBeneficiarioResult();

                    if (idAfi != null && idAfi != "")
                    {
                        list = BusClass.GetCohortesBeneficiario(Model.id_afi);
                        cohortes = list.FirstOrDefault();

                        ViewBag.listaCohortes = cohortes;
                        ViewBag.idAfi = idAfi;
                        ViewBag.conteoCohortes = list.Count();
                        Model.tieneCohorte = "TRUE";
                    }
                    else
                    {
                        Model.tieneCohorte = "FALSE";
                    }

                    ViewBag.usuario = SesionVar.ROL;
                    List<vw_ref_cups> lista = new List<vw_ref_cups>();
                    lista = Model.ListaCups().ToList();

                    List<ecop_concurrencia_evolucion_procedimientos> Agregadas = (List<ecop_concurrencia_evolucion_procedimientos>)Session["OtroProductoList"];
                    Session["OtroProductoList"] = null;
                    List<ecop_concurrencia_evolucion_procedimientos> Agregadas2 = new List<ecop_concurrencia_evolucion_procedimientos>();
                    ViewBag.listadoOtroProducto = Agregadas2;

                    ViewBag.id_concurrencia = idConcu;
                    ViewBag.rol = SesionVar.ROL;

                    return View(Model);
                }

                else
                {
                    return View(Model);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return View(Model);
            }

        }

        public ActionResult EliminarEvolucion(String id, String idConcu)
        {
            Models.Evolucion.Evolucion Model = new Models.Evolucion.Evolucion();
            if (!(String.IsNullOrEmpty(id)))
            {
                Model.EliminarEvolucion(Convert.ToInt32(id));
                return RedirectToAction("Evolucion", "Evolucion", new { idConcu = idConcu });
            }
            else
            {
                return RedirectToAction("Evolucion", "Evolucion");
            }
        }

        private Boolean ValidaPrimeraEvolucion(Models.Evolucion.Evolucion Model)
        {
            Boolean BoolRetorno = true;
            List<ecop_concurrencia_evolucion> lst = new List<ecop_concurrencia_evolucion>();
            ecop_concurrencia_evolucion ObjEvolucion = new ecop_concurrencia_evolucion();
            ecop_concurrencia ObjConcu = new ecop_concurrencia();
            List<vw_concurrencia_evolucion_Contrato> LstConcu = new List<vw_concurrencia_evolucion_Contrato>();
            ObjEvolucion.id_concurrencia = Model.id_concurrencia;
            lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);
            if (lst.Count == 0)
            {

                ObjConcu.id_concurrencia = Model.id_concurrencia;
                LstConcu = Model.ConsultaIdConcurreniaEvolucion(ObjConcu, ref MsgRes);

                foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                {

                    if (item.fecha_ingreso.Value.ToString("dd/MM/yyyy") == Model.fecha_evolucionok.Value.ToString("dd/MM/yyyy"))
                    {
                        BoolRetorno = true;
                    }
                    else
                    {
                        BoolRetorno = false;
                        Model.fecha_ingreso = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
            else
            {
                ObjConcu.id_concurrencia = Model.id_concurrencia;
                LstConcu = Model.ConsultaIdConcurreniaEvolucion(ObjConcu, ref MsgRes);
                foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                {
                    Model.fecha_ingreso = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                    break;
                }
                BoolRetorno = true;
            }

            return BoolRetorno;
        }

        private Boolean ValidaPrimeraEvolucion2(Models.Evolucion.Evolucion Model)
        {
            Boolean BoolRetorno = true;
            List<ecop_concurrencia_evolucion> lst = new List<ecop_concurrencia_evolucion>();
            ecop_concurrencia_evolucion ObjEvolucion = new ecop_concurrencia_evolucion();
            ecop_concurrencia ObjConcu = new ecop_concurrencia();
            List<vw_concurrencia_evolucion_Contrato> LstConcu = new List<vw_concurrencia_evolucion_Contrato>();
            ObjEvolucion.id_concurrencia = Model.id_concurrencia;
            lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);
            if (lst.Count == 0)
            {

                ObjConcu.id_concurrencia = Model.id_concurrencia;
                LstConcu = Model.ConsultaIdConcurreniaEvolucion(ObjConcu, ref MsgRes);

                foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                {
                    DateTime oldDate = item.fecha_ingreso.Value;
                    DateTime newDate = Model.fecha_evolucionok.Value;

                    TimeSpan result = newDate.Subtract(oldDate);

                    Int64 hora = Convert.ToInt32(result.TotalHours);

                    if (hora <= 24)
                    {
                        BoolRetorno = true;

                    }
                    else
                    {
                        BoolRetorno = false;
                        Model.fecha_ingreso = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                    }

                }
            }
            else
            {
                ObjConcu.id_concurrencia = Model.id_concurrencia;
                LstConcu = Model.ConsultaIdConcurreniaEvolucion(ObjConcu, ref MsgRes);
                foreach (vw_concurrencia_evolucion_Contrato item in LstConcu)
                {
                    DateTime oldDate = item.ultima_fecha.Value;
                    DateTime newDate = Model.fecha_evolucionok.Value;

                    TimeSpan result = newDate.Subtract(oldDate);

                    Int64 hora = Convert.ToInt32(result.TotalHours);

                    if (hora <= 72)
                    {
                        BoolRetorno = true;

                    }
                    else
                    {
                        BoolRetorno = false;
                        Model.fecha_ingreso = item.fecha_ingreso.Value.ToString("dd/MM/yyyy");
                    }
                }

            }

            return BoolRetorno;
        }

        private Boolean ValidaFechaIngresar(Models.Evolucion.Evolucion Model)
        {
            Boolean BoolRetorno = true;
            if (Model.fecha_evolucionok.Value.ToString("dd/MM/yyyy") == Model.fecha_por_ingresar)
            {
                BoolRetorno = true;
            }
            else
            {
                BoolRetorno = false;

            }
            return BoolRetorno;
        }

        private Boolean ValidaFechaIngresar2(Models.Evolucion.Evolucion Model)
        {
            Boolean BoolRetorno = true;

            return BoolRetorno;
        }

        public ActionResult DiagnosticoDefinitivo(String idConcu)
        {
            Models.Evolucion.DiagnosticoDefinitivo Model = new Models.Evolucion.DiagnosticoDefinitivo();

            if (!(String.IsNullOrEmpty(idConcu)))
            {

                Model.id_concurrencia = (Convert.ToInt32(idConcu));
                Model.ConsultaDiagnosticoDefinitivo(Convert.ToInt32(idConcu));
                return View(Model);
            }

            else
            {
                return View();
                // return RedirectToAction("Inicio", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult DiagnosticoDefinitivo(Models.Evolucion.DiagnosticoDefinitivo Model, string Command)
        {
            if (ModelState.IsValid)
            {
                ecop_concurrencia_evolucion_diag_def objdidef = new ecop_concurrencia_evolucion_diag_def();
                //objdidef.Concurrencia_Diagnostico_Definitivo_id= Model.id_concurrencia;
                objdidef.diagnosticoDefinitivo = Model.diagnosticoDefinitivo;
                objdidef.id_concurrencia = Model.id_concurrencia;
                objdidef.fecha_digita = System.DateTime.Today;
                objdidef.usuario_digita = Model.SesionVar.NombreUsuario;
                Model.InsertaDiagnosticoDefinitivo(objdidef, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
                ModelState.AddModelError("", "INGRESO EXITOSO.....");

                return RedirectToAction("EgresoConcurrencia", "Concurrencia", new { idConcu = Model.id_concurrencia });
                //window.open('@Url.Action("DiagnosticoDefinitivo", "Evolucion", new { idConcu = Model.id_concurrencia})', '', 'width=750,height=550,left=50,top=50,toolbar=yes');
                //return RedirectToAction("Evolucion", "Evolucion", new { idConcu = Model.id_concurrencia });
                //return View(Model);
            }
            else
            {
                ModelState.AddModelError("", "Error... Insertando....");
            }
            return View(Model);
            // return RedirectToAction("Concurrencia", "EgresoConcurrencia");
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Evolucion(Models.Evolucion.Evolucion Model, string Command)
        {
            ecop_concurrencia concu = new ecop_concurrencia();
            List<ecop_concurrencia_evolucion_procedimientos> Agregadas = new List<ecop_concurrencia_evolucion_procedimientos>();
            Agregadas = (List<ecop_concurrencia_evolucion_procedimientos>)Session["OtroProductoList"];
            List<ecop_concurrencia_evolucion_procedimientos> listaProcedimientos = new List<ecop_concurrencia_evolucion_procedimientos>();
            String variable = "";
            String variable2 = "";

            try
            {
                List<ecop_concurrencia_evolucion_procedimientos> Agregadas2 = new List<ecop_concurrencia_evolucion_procedimientos>();
                ViewBag.listadoOtroProducto = Agregadas2;

                try
                {
                    ViewBag.listadoOtroProducto = Agregadas2;
                    var idConcu = Model.id_concurrencia;
                    Model.ConsultaEvolucion(Convert.ToInt32(idConcu));
                    Model.ConsultaAfiliado(Convert.ToInt32(idConcu));
                    var idAfi = Model.id_afi;

                    concu = BusClass.ConsultaConcurrenciaId(idConcu);

                    if(concu != null)
                    {
                        if(concu.fecha_ingreso != null)
                        {
                            if(concu.fecha_ingreso > Model.fecha_evolucionok)
                            {
                                variable = "ERROR";
                                variable2 = "CAMPO DE FECHA EVOLUCIÓN MAYOR A LA FECHA DE INGRESO DEL PACIENTE.";
                            }
                        }
                    }

                    List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
                    management_cohortesBeneficiarioResult cohortes = new management_cohortesBeneficiarioResult();

                    if (idAfi != null && idAfi != "")
                    {
                        list = BusClass.GetCohortesBeneficiario(Model.id_afi);
                        cohortes = list.FirstOrDefault();

                        ViewBag.listaCohortes = cohortes;
                        ViewBag.idAfi = idAfi;
                        ViewBag.conteoCohortes = list.Count();
                        Model.tieneCohorte = "TRUE";
                    }
                    else
                    {
                        Model.tieneCohorte = "FALSE";
                    }

                    ViewBag.usuario = SesionVar.ROL;
                    List<vw_ref_cups> lista = new List<vw_ref_cups>();
                    lista = Model.ListaCups().ToList();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                if (Model.tieneProcedimientosQui == "SI")
                {
                    listaProcedimientos = Agregadas;

                    if (listaProcedimientos != null)
                    {
                        if (listaProcedimientos.Count() > 0)
                        {
                            variable = "OK";
                        }
                        else
                        {
                            variable = "ERROR";
                            variable2 = "FALTA SELECCIONAR CUPS...";
                        }
                    }
                    else
                    {
                        variable = "ERROR";
                        variable2 = "FALTA SELECCIONAR CUPS...";
                    }
                }

                if (Model.infencion_intrahospitalaria == "NO")
                {
                    ModelState.Remove("des_infencion_intrahospitalaria");
                }

                if (Model.notaIngreso == null)
                {
                    Model.notaIngreso = "NA";
                }

                if (Model.diagnosticoDefinitivo == null)
                {
                    Model.diagnosticoDefinitivo = "NA";
                }

                if (Model.fecha_ingreso == null)
                {
                    Model.fecha_ingreso = " .";

                }

                ViewBag.id_concurrencia = Model.id_concurrencia;

                if (variable != "ERROR")
                {
                    if (Model.tipo_h == "B")
                    {
                        if (Model.id_cie10_1 != null)
                        {
                            if (Model.diagnosticoDefinitivo != "NA")
                            {
                                ecop_concurrencia_evolucion_diag_def objdidef = new ecop_concurrencia_evolucion_diag_def();
                                objdidef.diagnosticoDefinitivo = Model.diagnosticoDefinitivo;
                                objdidef.id_concurrencia = Model.id_concurrencia;
                                objdidef.fecha_digita = System.DateTime.Today;
                                objdidef.usuario_digita = Model.SesionVar.NombreUsuario;
                                Model.InsertaDiagnosticoDefinitivo(objdidef, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
                            }
                            Boolean BolvalidaPrimerE = ValidaPrimeraEvolucion2(Model);

                            if (BolvalidaPrimerE == true)
                            {

                                ecop_concurrencia ObjConcu = new ecop_concurrencia();
                                List<ecop_concurrencia> LstConcu = new List<ecop_concurrencia>();
                                ObjConcu.id_concurrencia = Model.id_concurrencia;
                                LstConcu = Model.ConsultaIdConcurrenia(ObjConcu, ref MsgRes);
                                foreach (ecop_concurrencia item in LstConcu)
                                {
                                    if (!(String.IsNullOrEmpty(item.fecha_egreso.ToString())))
                                    {
                                        if (Model.fecha_evolucionok.Value.ToString("dd/MM/yyyy") == item.fecha_egreso.Value.ToString("dd/MM/yyyy"))
                                        {
                                            Model.evoluciones_cargadas = "SI";
                                            break;
                                        }
                                        else
                                        {
                                            Model.evoluciones_cargadas = "NO";
                                            break;
                                        }
                                    }
                                }
                                if (Model.evoluciones_cargadas == "NO")
                                {
                                    ecop_concurrencia_evolucion ObjEvolucion = new ecop_concurrencia_evolucion();
                                    ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                    ObjEvolucion.fecha = Model.fecha_evolucionok;
                                    ObjEvolucion.id_tipo_habitacion = Model.id_tipo_habitacion;
                                    ObjEvolucion.InfeccionIntra = Model.infencion_intrahospitalaria;
                                    // ObjEvolucion.justificacionEstancia = Model.justificacionEstancia;


                                    // ObjEvolucion.notaIngreso = Model.notaIngreso;

                                    List<ecop_concurrencia_evolucion> lst = new List<ecop_concurrencia_evolucion>();

                                    ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                    lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);
                                    if (lst.Count == 0)
                                    {
                                        ObjEvolucion.notaIngreso = Model.notaIngreso;
                                    }
                                    else
                                    {
                                        ObjEvolucion.notaIngreso = lst[0].notaIngreso;
                                    }

                                    if (ObjEvolucion.InfeccionIntra == "SI")
                                    {
                                        ObjEvolucion.DesInfeccionIntra = Model.des_infencion_intrahospitalaria;
                                    }
                                    else
                                    {
                                        ObjEvolucion.DesInfeccionIntra = string.Empty;
                                    }
                                    ObjEvolucion.tieneEventoA = Model.tieneEventoA;
                                    ObjEvolucion.tieneGlosa = Model.tieneGlosa;
                                    ObjEvolucion.tieneProcedimientoQ = Model.tieneProcedimientosQui;
                                    ObjEvolucion.tieneSituacionCA = Model.tieneSituacionCA;
                                    ObjEvolucion.descripcion = Model.descripcion_evolucion;
                                    ObjEvolucion.justificacionEstancia = Model.justificacionEstancia;

                                    ObjEvolucion.gestion_auditor = Model.gestion_auditor;
                                    ObjEvolucion.dx1 = Model.id_cie10_1;
                                    ObjEvolucion.dx2 = Model.id_cie10_2;
                                    ObjEvolucion.dx3 = Model.id_cie10_3;
                                    ObjEvolucion.dx4 = Model.id_cie10_4;
                                    ObjEvolucion.tiene_ahorro = Model.Ahorro;
                                    ObjEvolucion.tiene_estancia_pertinente = Model.tiene_estancia_pertinente;
                                    ObjEvolucion.tiene_ingresoCohorte = Model.tieneCohorteBenef;
                                    ObjEvolucion.digita_usuario = SesionVar.UserName;
                                    ObjEvolucion.ValidaEvolucion = ObjEvolucion.fecha.Value.Month.ToString() + ObjEvolucion.fecha.Value.Day.ToString() + ObjEvolucion.fecha.Value.Year.ToString() + Model.id_concurrencia.ToString();
                                    ObjEvolucion.fecha_digita = Model.ManagmentHora();

                                    Model.InsertaConcurrenciaEvolucion(ObjEvolucion, listaProcedimientos, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);

                                    Alertas(Model);

                                    ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                    lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);

                                    if (lst.Count != 0)
                                    {
                                        return RedirectToAction("Evolucion", "Evolucion", new { idConcu = ObjEvolucion.id_concurrencia });
                                    }

                                    else
                                    {
                                        Model.id_concurrencia = ObjEvolucion.id_concurrencia;
                                        ModelState.AddModelError("", "Error... Insertando....");
                                    }

                                }
                                else
                                {
                                    ModelState.AddModelError("", "!!...Error...El paciente ya tiene ingresada todas las evoluciones cargadas...!!!");
                                }

                            }
                            else
                            {
                                List<ecop_concurrencia_evolucion> lst2 = new List<ecop_concurrencia_evolucion>();
                                ecop_concurrencia_evolucion ObjEvolucion2 = new ecop_concurrencia_evolucion();
                                ecop_concurrencia ObjConcu2 = new ecop_concurrencia();
                                List<vw_concurrencia_evolucion_Contrato> LstConcu2 = new List<vw_concurrencia_evolucion_Contrato>();
                                ObjEvolucion2.id_concurrencia = Model.id_concurrencia;
                                lst2 = Model.ConsultaEvoluciones(ObjEvolucion2, ref MsgRes);
                                
                                
                                
                                if (lst2.Count == 0)
                                {
                                    ModelState.AddModelError("", "Error...no puede ser mayor de  24  Horas desde el ingreso" + "...");
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Error...no puede ser mayor a  72  Horas desde el ultimo ingreso" + "...");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error...Debe ingresar El diagnostico  DIAGNOSTICOS CIE10_1");
                        }
                    }
                    else
                    {

                        if (Model.id_cie10_1 != null)
                        {
                            if (Model.diagnosticoDefinitivo != "NA")
                            {
                                ecop_concurrencia_evolucion_diag_def objdidef = new ecop_concurrencia_evolucion_diag_def();
                                objdidef.diagnosticoDefinitivo = Model.diagnosticoDefinitivo;
                                objdidef.id_concurrencia = Model.id_concurrencia;
                                objdidef.fecha_digita = System.DateTime.Today;
                                objdidef.usuario_digita = Model.SesionVar.NombreUsuario;
                                Model.InsertaDiagnosticoDefinitivo(objdidef, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);
                            }
                            Boolean BolvalidaPrimerE = ValidaPrimeraEvolucion(Model);

                            if (BolvalidaPrimerE == true)
                            {
                                Boolean BolvalidaFechaIngres = ValidaFechaIngresar(Model);
                                if (BolvalidaFechaIngres == true)
                                {
                                    ecop_concurrencia ObjConcu = new ecop_concurrencia();
                                    List<ecop_concurrencia> LstConcu = new List<ecop_concurrencia>();
                                    ObjConcu.id_concurrencia = Model.id_concurrencia;
                                    LstConcu = Model.ConsultaIdConcurrenia(ObjConcu, ref MsgRes);
                                    foreach (ecop_concurrencia item in LstConcu)
                                    {
                                        if (!(String.IsNullOrEmpty(item.fecha_egreso.ToString())))
                                        {
                                            if (Model.fecha_evolucionok.Value.ToString("dd/MM/yyyy") == item.fecha_egreso.Value.ToString("dd/MM/yyyy"))
                                            {
                                                Model.evoluciones_cargadas = "SI";
                                                break;
                                            }
                                            else
                                            {
                                                Model.evoluciones_cargadas = "NO";
                                                break;
                                            }
                                        }
                                    }
                                    if (Model.evoluciones_cargadas == "NO")
                                    {
                                        ecop_concurrencia_evolucion ObjEvolucion = new ecop_concurrencia_evolucion();
                                        ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                        ObjEvolucion.fecha = Model.fecha_evolucionok;
                                        ObjEvolucion.id_tipo_habitacion = Model.id_tipo_habitacion;
                                        ObjEvolucion.InfeccionIntra = Model.infencion_intrahospitalaria;
                                        // ObjEvolucion.justificacionEstancia = Model.justificacionEstancia;


                                        // ObjEvolucion.notaIngreso = Model.notaIngreso;

                                        List<ecop_concurrencia_evolucion> lst = new List<ecop_concurrencia_evolucion>();

                                        ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                        lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);
                                        if (lst.Count == 0)
                                        {
                                            ObjEvolucion.notaIngreso = Model.notaIngreso;
                                        }
                                        else
                                        {
                                            ObjEvolucion.notaIngreso = lst[0].notaIngreso;
                                        }




                                        if (ObjEvolucion.InfeccionIntra == "SI")
                                        {
                                            ObjEvolucion.DesInfeccionIntra = Model.des_infencion_intrahospitalaria;
                                        }
                                        else
                                        {
                                            ObjEvolucion.DesInfeccionIntra = string.Empty;
                                        }
                                        ObjEvolucion.tieneEventoA = Model.tieneEventoA;
                                        ObjEvolucion.tieneGlosa = Model.tieneGlosa;
                                        ObjEvolucion.tieneProcedimientoQ = Model.tieneProcedimientosQui;
                                        ObjEvolucion.tieneSituacionCA = Model.tieneSituacionCA;
                                        ObjEvolucion.descripcion = Model.descripcion_evolucion;
                                        ObjEvolucion.justificacionEstancia = Model.justificacionEstancia;

                                        ObjEvolucion.gestion_auditor = Model.gestion_auditor;
                                        ObjEvolucion.dx1 = Model.id_cie10_1;
                                        ObjEvolucion.dx2 = Model.id_cie10_2;
                                        ObjEvolucion.dx3 = Model.id_cie10_3;
                                        ObjEvolucion.dx4 = Model.id_cie10_4;
                                        ObjEvolucion.digita_usuario = SesionVar.UserName;
                                        ObjEvolucion.ValidaEvolucion = ObjEvolucion.fecha.Value.Month.ToString() + ObjEvolucion.fecha.Value.Day.ToString() + ObjEvolucion.fecha.Value.Year.ToString() + Model.id_concurrencia.ToString();
                                        ObjEvolucion.fecha_digita = Model.ManagmentHora();
                                        Model.InsertaConcurrenciaEvolucion(ObjEvolucion, listaProcedimientos, SesionVar.UserName, SesionVar.IPAddress, ref MsgRes);



                                        ObjEvolucion.id_concurrencia = Model.id_concurrencia;
                                        lst = Model.ConsultaEvoluciones(ObjEvolucion, ref MsgRes);

                                        if (lst.Count != 0)
                                        {
                                            return RedirectToAction("Evolucion", "Evolucion", new { idConcu = ObjEvolucion.id_concurrencia });
                                        }

                                        else
                                        {
                                            ModelState.AddModelError("", "Error... Insertando....");
                                        }


                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "!!...Error...El paciente ya tiene ingresada todas las evoluciones cargadas...!!!");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Error...Debe ingresar la Evolucion del dia" + Convert.ToDateTime(Model.fecha_por_ingresar).ToString("dd/MM/yyyy") + "...");
                                }
                            }
                            else
                            {
                                var fecha = Model.fecha_ingreso;
                                //var fechaIngreso = Convert.ToDateTime(Model.fecha_ingreso);
                                ModelState.AddModelError("", "Error...Debe ingresar la Evolucion del dia " + fecha + "...");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error...Debe ingresar El diagnostico  DIAGNOSTICOS CIE10_1");
                        }
                    }
                }
                else
                {
                    ViewBag.id_concurrencia = Model.id_concurrencia;
                    ModelState.AddModelError("", "ERROR...DEBE INGRESAR EL" + ' ' + variable2);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                ModelState.AddModelError("", "Error: " + error);
            }

            ViewBag.id_concurrencia = Model.id_concurrencia;

            return View(Model);
        }


        public JsonResult GetCie10(Models.Evolucion.Evolucion Model)
        {
            return Json(Model.LstCie10.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCie102(Models.Evolucion.Evolucion Model)
        {
            return Json(Model.LstCie10.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCie10Principal(Models.Evolucion.Evolucion Model)
        {
            ObjectCache cache = MemoryCache.Default;
            List<vw_cie10> fileContents = cache["filecontents"] as List<vw_cie10>;
            List<vw_cie10> LstCie102Principal = new List<vw_cie10>();

            if (fileContents != null)
            {
                LstCie102Principal = fileContents;
            }
            else
            {
                LstCie102Principal = BusClass.GetCie10Unido();
                if (fileContents == null || fileContents.Count == 0)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);
                    fileContents = LstCie102Principal.ToList();

                    cache.Add("filecontents", fileContents, cacheItemPolicy);

                }
            }

            return Json(LstCie102Principal.ToList(), JsonRequestBehavior.AllowGet);
        }


        private void Alertas(Models.Evolucion.Evolucion Model)
        {
            Response.Write("<script language=javascript>alert('SIN REGISTROS...');</script>");
        }


        //[HttpPost]
        //public ActionResult UpdateOrder(String id, Int32? idconcu, Models.Evolucion.Evolucion Model)
        //{
        //    bool rta = false;
        //    if (id != null)
        //    {
        //        Model.ConsultaListaAlertas(id, ref MsgRes);

        //        ecop_concurrencia concurrencia = BusClass.ConsultaConcurrenciaId(idconcu.Value);

        //        var AlertList = Model.ConsultaAlertasConcurrencia(Convert.ToInt32(idconcu), id).ToList();
        //        if (AlertList.Count() <= 0)
        //        {
        //            if (Model.ListaAlertasCie10.Count != 0)
        //            {
        //                String mensaje = "";
        //                foreach (var item in Model.ListaAlertasCie10)
        //                {






        //                    if (item.rango_edad != null)
        //                    {
        //                        switch (item.tipo_rango)
        //                        {
        //                            case 1:
        //                                if (concurrencia.afi_edad  < item.rango_edad)
        //                                {
        //                                    mensaje = item.alerta;
        //                                    rta = true;
        //                                }
        //                                else
        //                                {
        //                                    rta = false;
        //                                }
        //                                break;
        //                            case 2:
        //                                if (concurrencia.afi_edad > item.rango_edad)
        //                                {
        //                                    mensaje = item.alerta;
        //                                    rta = true;
        //                                }
        //                                else
        //                                {
        //                                    rta = false;
        //                                }
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        mensaje = item.alerta;
        //                        return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                if (rta)
        //                {
        //                    return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    return Json(new { success = true, message = "Dx no cumple con el rango de edad", opc = 2 }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            else
        //            {
        //                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json(new { success = true, message = "", opc = 1 }, JsonRequestBehavior.AllowGet);
        //    }
        //    // return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);


        //}


        [HttpPost]
        public ActionResult UpdateOrder(String id, Int32? idconcu, Models.Evolucion.Evolucion Model)
        {
            bool rta = false;
            if (id != null)
            {
                Model.ConsultaListaAlertasNuevo(id, ref MsgRes);

                ecop_concurrencia concurrencia = BusClass.ConsultaConcurrenciaId(idconcu.Value);
                var paciente = concurrencia.id_afi;

                management_datosPaciente_alertasResult datos = new management_datosPaciente_alertasResult();
                datos = BusClass.DatosPaciente((int)idconcu);
                DateTime fechaNacimiento = new DateTime();

                if (datos.fecha_nacimiento != null)
                {
                    fechaNacimiento = (DateTime)datos.fecha_nacimiento;
                }

                var diff = DateTime.Now - fechaNacimiento;
                var dias = diff.TotalDays;
                var edadP = (int)(dias / 365.25);

                var AlertList = Model.ConsultaAlertasConcurrencia(Convert.ToInt32(idconcu), id).ToList();
                if (AlertList.Count() <= 0)
                {
                    if (Model.ListaAlertasCie10.Count != 0)
                    {
                        String mensaje = "";
                        var pasa = 0;

                        foreach (var item in Model.ListaAlertasCie10)
                        {
                            if (!item.tipo_rango.Equals("NULL"))
                            {
                                var tipoRango = item.tipo_rango;

                                //int edad = (int)concurrencia.afi_edad;
                                int edad = edadP;

                                String[] tipoRange = new string[0];

                                if (tipoRango.Contains("/"))
                                {
                                    tipoRange = tipoRango.Split('/');

                                    if (tipoRange.Length > 1)
                                    {
                                        for (int i = 0; i < tipoRange.Length; i++)
                                        {
                                            var tipoRangoDef = tipoRange[i];
                                            var rango = tipoRangoDef.Split('-');

                                            if (rango.Length > 1)
                                            {
                                                if (edad >= Convert.ToInt32(rango[0]) && edad <= Convert.ToInt32(rango[1]))
                                                {
                                                    pasa = pasa + 2;
                                                }
                                                else
                                                {
                                                    pasa--;
                                                }
                                            }
                                            else
                                            {
                                                if (edad >= Convert.ToInt32(rango[0]))
                                                {
                                                    pasa = pasa + 2;
                                                }
                                                else
                                                {
                                                    pasa--;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var rango = tipoRango.Split('-');
                                        var tipoRangoDef = rango[0];

                                        if (edad >= Convert.ToInt32(tipoRangoDef[0]) && edad <= Convert.ToInt32(tipoRangoDef[1]))
                                        {
                                            pasa = 1;
                                        }
                                        else
                                        {
                                            pasa = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var rango = tipoRango.Split('-');

                                    if (edad >= Convert.ToInt32(rango[0]) && edad <= Convert.ToInt32(rango[1]))
                                    {
                                        pasa = 1;
                                    }
                                    else
                                    {
                                        pasa = 0;
                                    }
                                }

                                if (pasa > 0)
                                {
                                    mensaje = item.alerta;
                                    return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    mensaje = "Dx no cumple con el rango de edad";
                                    return Json(new { success = false, message = mensaje, opc = 2 }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                mensaje = item.alerta;
                                return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (rta)
                        {
                            return Json(new { success = true, message = mensaje, opc = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, message = "Dx no cumple con el rango de edad", opc = 2 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = true, message = "", opc = 1 }, JsonRequestBehavior.AllowGet);
            }
            // return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult UpdateOrder2(String id, String concurrencia, Models.Evolucion.Evolucion Model)
        {
            int tipoevento = 0;
            string msgRta = "";

            var cie10_alerta = Model.ConsultaAlertaCie10(id);
            if (cie10_alerta.alerta.ToUpper().Contains("SALUD PUBLICA"))
            {
                Models.analisis_de_caso.AnalisisSaludPublica saludp = new Models.analisis_de_caso.AnalisisSaludPublica();
                var listadx = saludp.ConsultaEvolucionAnalisisSaludP(Convert.ToInt32(concurrencia), null).ToList();

                tipoevento = 1;
                msgRta = "Este diagnostico genera un analisis de caso en salud publica.";
            }
            else
            {
                Models.analisis_de_caso.AnalisisOriginal saludp = new Models.analisis_de_caso.AnalisisOriginal();
                var listadx = saludp.ConsultaAnalisisCasoOriginal(Convert.ToInt32(concurrencia), null).ToList();

                tipoevento = 2;
                msgRta = "Este diagnostico genera un analisis de caso en centinelas, catastroficas o trazadoras.";
            }

            if (id != null)
            {
                Model.ObjAlertas.id_concurrencia = Convert.ToInt32(concurrencia);
                Model.ObjAlertas.cie10 = Convert.ToString(id);
                Model.ObjAlertas.usuario_ingreso = SesionVar.UserName;
                Model.ObjAlertas.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

                var AlertList = Model.ConsultaAlertasConcurrencia(Convert.ToInt32(concurrencia), id).ToList();
                if (AlertList.Count() <= 0)
                    Model.InsertarAlertasConcurrencia(ref MsgRes);
            }

            var data = new
            {
                success = true,
                message = "Ingreso Exitoso....",
                msgEvento = msgRta,
                evento = tipoevento
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Cie10Second(String id, String idConcu, Models.Evolucion.Evolucion Model)
        {
            if (id != null)
            {
                Model.ConsultaNumeroEvolucion(Convert.ToInt32(idConcu));
                if (Model.LstNumeroEvoluciones.Count >= 3)
                {
                    Model.ConsultaCie10Second(id, ref MsgRes);
                    if (Model.LstCie10Secundarios.Count != 0)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {
                    return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            // return Json(Model.ListIPS2.Select(p => new { id_ref_ips_cuentas = p.id_ref_ips_cuentas, Nombre = p.Nombre }), JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult GetCie10jo(Models.Evolucion.Evolucion Model)
        {
            return Json(Model.LstCie10.ToList(), JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult _EvolucionCohorte(String ID)
        {
            Models.Evolucion.Evolucion Model = new Models.Evolucion.Evolucion();
            ViewBag.idrole = SesionVar.ROL;
            ViewData["alerta"] = "";
            Model.id_afi = ID;

            List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
            List<management_cohortesBeneficiarioResult> list2 = new List<management_cohortesBeneficiarioResult>();
            management_cohortesBeneficiarioResult cohorte = new management_cohortesBeneficiarioResult();

            list = BusClass.GetCohortesBeneficiario(Model.id_afi);
            cohorte = list.FirstOrDefault();

            list2.Add(cohorte);

            ViewBag.conteoCohortes = list2.Count();
            ViewBag.listaCohortes = list2;
            ViewBag.idAfi = ID;

            return PartialView(Model);
        }

        public string Agregarotroproducto(Int32 id_concurrencia, String id_cups_qx, String descripcion)
        {

            string result = "";
            List<ecop_concurrencia_evolucion_procedimientos> Agregadas = (List<ecop_concurrencia_evolucion_procedimientos>)Session["OtroProductoList"];

            ecop_concurrencia_evolucion_procedimientos NewOtroProducto = new ecop_concurrencia_evolucion_procedimientos();
            List<ecop_concurrencia_evolucion_procedimientos> lista = new List<ecop_concurrencia_evolucion_procedimientos>();


            NewOtroProducto.id_concurrencia = id_concurrencia;
            NewOtroProducto.id_cups_qx = id_cups_qx;
            NewOtroProducto.descripcion_cups = descripcion;
            NewOtroProducto.fecha_ingreso = Convert.ToDateTime(DateTime.Now);
            NewOtroProducto.usuario_ingreso = SesionVar.UserName;

            lista.Add(NewOtroProducto);

            //result += "<tbody>";
            int i = 0;
            if (Agregadas == null)
            {
                Agregadas = lista;
                Session["OtroProductoList"] = lista;
            }
            else
            {
                Agregadas.Add(NewOtroProducto);
            }

            foreach (ecop_concurrencia_evolucion_procedimientos a in Agregadas)
            {
                i += 1;

                result += "<tr>"
                   + "<td>" + i + "</td>";

                result += "<td>" + a.id_cups_qx + "</td>";
                result += "<td>" + a.descripcion_cups + "</td>";
                result += "<td>" + a.fecha_ingreso + "</td>";
                result += "<td class='text-center'><a title='Eliminar' href='javascript:removerotroProducto(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";

            }

            var data = new
            {
                tabla = result,

            };

            return result;
        }

        public JsonResult RemoverOtroProducto(Int32? idotroproucto)
        {

            string result = "";
            List<ecop_concurrencia_evolucion_procedimientos> otroproductolist = (List<ecop_concurrencia_evolucion_procedimientos>)Session["OtroProductoList"];

            otroproductolist.Remove(otroproductolist[Convert.ToInt32(idotroproucto) - 1]);
            Session["OtroProductoList"] = otroproductolist;


            int i = 0;


            foreach (ecop_concurrencia_evolucion_procedimientos a in otroproductolist)
            {
                i += 1;

                result += "<tr>"
                   + "<td>" + i + "</td>";

                result += "<td>" + a.id_cups_qx + "</td>";
                result += "<td>" + a.descripcion_cups + "</td>";
                result += "<td>" + a.fecha_ingreso + "</td>";
                result += "<td class='text-center'><a title='Eliminar' href='javascript:removerotroProducto(" + i + ")'><i class='glyphicon glyphicon-trash'></i></a></td>";
                result += "</tr>";

            }

            var data = new
            {
                tabla = result,

            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //Leo
    }
}