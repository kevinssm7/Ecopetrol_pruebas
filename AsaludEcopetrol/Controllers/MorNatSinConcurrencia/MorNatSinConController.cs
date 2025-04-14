using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.MorNatSinConcurrencia
{
    [SessionExpireFilter]
    public class MorNatSinConController : Controller
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

        public ActionResult BuscarAfiliado()
        {
            Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model = new Models.MorNatSinConcurrencia.MorNatSinConcurrencia();
            return View(Model);

        }

        public ActionResult NatalidadSinCon(String id,String id2)
        {
            Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model = new Models.MorNatSinConcurrencia.MorNatSinConcurrencia();

            Model.afi_tipoDoc = id2;
            Model.afi_NumDoc = id;

           

            return View(Model);

        }


        public ActionResult MortalidadaSinCon(String id, String id2)
        {
            Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model = new Models.MorNatSinConcurrencia.MorNatSinConcurrencia();

            Model.afi_tipoDoc = id2;
            Model.afi_NumDoc = id;


            return View(Model);

        }



        [HttpPost]
        public ActionResult BuscarAfiliado(Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model)
        {
            List<VW_base_beneficiarios> list = new List<VW_base_beneficiarios>();

            list = Model.BaseBeneficiarios();

            if (list.Count != 0)
            {
                if (Model.Items == "1")
                {
                    return RedirectToAction("NatalidadSinCon", "MorNatSinCon", new { id = Model.afi_NumDoc, id2 = Model.IdSeleccionado });
                }
                else
                {
                    return RedirectToAction("MortalidadaSinCon", "MorNatSinCon", new { id = Model.afi_NumDoc, id2 = Model.IdSeleccionado });
                }
            }
            else
            {
                ModelState.AddModelError("", "!!...SIN REGISTROS.!!!");
            }

            return View(Model);


        }

        [HttpPost]
        public ActionResult NatalidadSinCon(Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model)
        {
            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_ciudad != 0)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "CIUDAD DE NACIMIENTO";
                Conteo = Conteo + 1;
            }
            if (Model.fecha_nacimientook != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE NACIMIENTO";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_BCGok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE BCG";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_vitaminaKok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE VITAMINA K";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_consenjeria_lactanciaok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE CONSEJERIA DE LACTANCIA";
                    Conteo = Conteo + 1;
                }
                if (Model.fechaTCHok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE TCH";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_consulta_pediatriaok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE CONSULTA PEDIATRIA";
                    Conteo = Conteo + 1;
                }

                if (Model.fecha_hepatitisBok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE VACUNA HEPATITIS B";
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
                Model.ObjNatalidad.documento_afiliado = Model.afi_NumDoc;
                Model.ObjNatalidad.nombre_afiliado = Model.afi_Nom;
                Model.ObjNatalidad.fecha_nacimiento = Model.fecha_nacimientook;
                Model.ObjNatalidad.edad = Convert.ToString(Model.afi_Edad);
                Model.ObjNatalidad.peso = Model.peso;
                Model.ObjNatalidad.via_parto = Model.id_via_parto;
                Model.ObjNatalidad.talla = Model.talla;
                Model.ObjNatalidad.sexo = Model.sexo;
                Model.ObjNatalidad.apgar = Model.apgar;
                Model.ObjNatalidad.control_prenatal = Model.control_prenatal;
                Model.ObjNatalidad.fecha_BCG = Model.fecha_BCGok;
                Model.ObjNatalidad.fecha_vitaminaK = Model.fecha_vitaminaKok;
                Model.ObjNatalidad.fecha_consenjeria_lactancia = Model.fecha_consenjeria_lactanciaok;
                Model.ObjNatalidad.resultadoTCH = Model.resultadoTCH;
                Model.ObjNatalidad.fechaTCH = Model.fechaTCHok;
                Model.ObjNatalidad.fecha_consulta_pediatria = Model.fecha_consulta_pediatriaok;
                Model.ObjNatalidad.fecha_hepatitisB = Model.fecha_hepatitisBok;
                Model.ObjNatalidad.lugar_atencion = Convert.ToString(Model.id_ciudad);
                Model.ObjNatalidad.IPS_atencion = Convert.ToString(Model.nom_ips);

                Model.ObjNatalidad.usuario = Convert.ToString(SesionVar.UserName);
                Model.ObjNatalidad.fecha_digita = Convert.ToDateTime(DateTime.Now);

                Model.InsertarNatalidad(ref MsgRes);

                if (MsgRes.DescriptionResponse == null)
                {
                    return RedirectToAction("BuscarAfiliado", "MorNatSinCon");
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error...!!!" + MsgRes.DescriptionResponse);
                }
               
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }
         

            return View(Model);

        }

        [HttpPost]
        public ActionResult MortalidadaSinCon(Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model)
        {

            Int32 Conteo = 0;
            String variable = "";
            String variable2 = "";

            if (Model.id_ciudad != 0)
            {

            }
            else
            {
                variable = "ERROR";
                variable2 = "CIUDAD DE NACIMIENTO";
                Conteo = Conteo + 1;
            }

            if (Model.fecha_de_muerteok != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "FECHA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }

                if (Model.diag_causa_directa_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA DIRECTA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }


                if (Model.diag_causa_basica_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA BASICA DE LA MUERTE";
                    Conteo = Conteo + 1;
                }

                if (Model.diag_causa_antecedente_muerte != null)
                {

                }
                else
                {
                    variable = "ERROR";
                    variable2 = "DIAGNOSTICO CAUSA ANTECEDENTE DE LA MUERTE";
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
                Model.ObjMortalidad.documento_afiliado = Model.afi_NumDoc;
                Model.ObjMortalidad.nombre_afiliado = Model.afi_Nom;
                Model.ObjMortalidad.numero_defuncion = Model.NumeroDefuncion;
                Model.ObjMortalidad.hora_defuncion = Model.HoraDefuncion;
                Model.ObjMortalidad.fecha_fallecimiento = Model.fecha_de_muerteok;
                Model.ObjMortalidad.observacion = Model.Observacion;
                Model.ObjMortalidad.diag_causa_directa_muerte = Model.diag_causa_directa_muerte;
                Model.ObjMortalidad.diag_causa_basica_muerte = Model.diag_causa_basica_muerte;
                Model.ObjMortalidad.diag_causa_antecedente_muerte = Model.diag_causa_antecedente_muerte;
                Model.ObjMortalidad.fecha_exp_cedula_fallecido = Model.fecha_exp_cedula_fallecidook;
                Model.ObjMortalidad.ciudad_muerte = Convert.ToInt32(Model.id_ciudad);
                Model.ObjMortalidad.IPs_muerte = Convert.ToString(Model.nom_ips);
                Model.ObjMortalidad.usuario_digita = Convert.ToString(SesionVar.UserName);
                Model.ObjMortalidad.fecha_digita = Convert.ToDateTime(DateTime.Now);

                Model.InsertarMortalidad(ref MsgRes);

                if (MsgRes.DescriptionResponse == null)
                {
                    return RedirectToAction("BuscarAfiliado", "MorNatSinCon");
                }
                else
                {
                    ModelState.AddModelError("", "!!...Error...!!!" + MsgRes.DescriptionResponse);
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR...DEBE INGRESAR TODOS LOS CAMPOS!" + ' ' + variable2);
            }

           

            return View(Model);
        }
        
        public JsonResult GetCiudadesTotal(Models.MorNatSinConcurrencia.MorNatSinConcurrencia Model)
        {
            return Json(Model.GetCiudades.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}