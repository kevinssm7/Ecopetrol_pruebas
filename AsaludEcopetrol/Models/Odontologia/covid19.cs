using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Helpers;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;



namespace AsaludEcopetrol.Models.Odontologia
{
    public class covid19
    {


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


        public int conteorepetidas { get; set; }
        public int conteoingresadas { get; set; }

        public Int32? id_cargue { get; set; }

        public Int32? estadoanterior { get; set; }

        public Int32? estadoanteriorcargue { get; set; }


        public String documento_paciente { get; set; }


        public int total_casos { get; set; }

        public int singestion  { get; set; }

        public int congestion { get; set; }

        public int total_casosinter { get; set; }

        public int singestioninter { get; set; }

        public int congestioninter { get; set; }

        public String alertaSioNODiario { get; set; }
        public String alertaSioNOInterdiario { get; set; }




        private List<vw_seguimiento_covid19_diario> _Lista1;
        public List<vw_seguimiento_covid19_diario> Lista1
        {
            get
            {
                if (_Lista1 == null)
                {

                    return _Lista1 = new List<vw_seguimiento_covid19_diario>();
                }
                else
                {
                    return _Lista1;
                }

            }

            set
            {
                _Lista1 = value;
            }
        }


        public void CargarBaseMDT(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContador = 0;
            Int32 IntContador2 = 0;
            List<cargue_seguimiento_covid19> OBJ = new List<cargue_seguimiento_covid19>();
            List<cargue_seguimiento_covid19_detalle> OBJDetalle = new List<cargue_seguimiento_covid19_detalle>();
            try
            {

                foreach (DataRow item in dt2.Rows)
                {
                    

                    cargue_seguimiento_covid19 seguimiento = new cargue_seguimiento_covid19();


                    if (item["DOCUMENTO"].ToString() != "")
                    {
                        seguimiento.No = Convert.ToString(item["NO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.tipo_documento = Convert.ToString(item["TIPO DE DOCUMENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.documento = Convert.ToString(item["DOCUMENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.nombres = Convert.ToString(item["NOMBRES"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.apellidos = Convert.ToString(item["APELLIDOS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.genero = Convert.ToString(item["GENERO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.viceprecidencia = Convert.ToString(item["VICEPRESIDENCIA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.descripcion_ciudad = Convert.ToString(item["DESCRIPCIÓN CIUDAD S/MED"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.localidad = Convert.ToString(item["LOCALIDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.departamento = Convert.ToString(item["DEPARTAMENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.regional = Convert.ToString(item["REGIONAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["FECHA NACTO"].ToString() == "")
                        {
                            seguimiento.fecha_nacimiento = null;
                        }
                        else
                        {
                            seguimiento.fecha_nacimiento = Convert.ToDateTime(item["FECHA NACTO"]);
                        }
                        seguimiento.edad = Convert.ToString(item["EDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.tipo_salud = Convert.ToString(item["TIPO SALUD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.direccion = Convert.ToString(item["DIRECCIÓN/"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.telefono_1 = Convert.ToString(item["TELEFONO 1"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.telefono_2 = Convert.ToString(item["TELEFONO 2"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.correo = Convert.ToString(item["CORREO ELECTRONICO "]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.incluido_en_sivigila = Convert.ToString(item["INCLUIDO EN SIVIGILA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.tipificacion = Convert.ToString(item["TIPIFICACIÓN"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        string retorno = seguimiento.tipificacion.Substring(0, 1);
                        seguimiento.estado = retorno;
                        if (item["FECHA INGRESO AL PAÍS"].ToString() == "")
                        {
                            seguimiento.fecha_ingreso_pais = null;
                        }
                        else
                        {
                            seguimiento.fecha_ingreso_pais = Convert.ToDateTime(item["FECHA INGRESO AL PAÍS"]);
                        }
                        seguimiento.seguimiento = Convert.ToString(item["SEGUIMIENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.estado_base = Convert.ToString(item["ESTADO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.causa_finalizacion_del_seguimiento = Convert.ToString(item["CAUSA FINALIZACIÓN DEL SEGUIMIENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.observacion_de_caracterizacion = Convert.ToString(item["OBSERVACION DE CARACTERIZACIÓN"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["FECHA DE CARACTERIZACIÓN - REGISTRO"].ToString() == "")
                        {
                            seguimiento.fecha_caracterizacion_registro = null;
                        }
                        else
                        {
                            seguimiento.fecha_caracterizacion_registro = Convert.ToDateTime(item["FECHA DE CARACTERIZACIÓN - REGISTRO"]);
                        }
                        seguimiento.nombre_quien_reporta = Convert.ToString(item["NOMBRE DE QUIEN REPORTA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        seguimiento.auditor_asignado = Convert.ToString(item["AUDITOR ASIGNADO"]);
                        seguimiento.usuario_cargue = SesionVar.UserName;
                        seguimiento.fecha_cargue = DateTime.Now;
                    }
                    else
                    {

                    }


                    List<cargue_seguimiento_covid19> listacargue = BusClass.ConsultaDocumentoPacienteCovid19(seguimiento.documento).ToList();
                    if (listacargue.Count == 0)
                    {
                        OBJ.Add(seguimiento);
                        seguimiento = new cargue_seguimiento_covid19();
                        
                        IntContador = IntContador + 1;
                    }
                    else
                    {
                        IntContador2 = IntContador2 + 1;
                        cargue_seguimiento_covid19 OBJAc = new cargue_seguimiento_covid19();
                        foreach (var itemActualiza in listacargue)
                        {
                            OBJAc.id = itemActualiza.id;
                        }
                        OBJAc.No = seguimiento.No;
                        OBJAc.tipo_documento = seguimiento.tipo_documento;
                        OBJAc.documento = seguimiento.documento;
                        OBJAc.nombres = seguimiento.nombres;
                        OBJAc.apellidos = seguimiento.apellidos;
                        OBJAc.genero = seguimiento.genero;
                        OBJAc.viceprecidencia = seguimiento.viceprecidencia;
                        OBJAc.descripcion_ciudad = seguimiento.descripcion_ciudad;
                        OBJAc.localidad = seguimiento.localidad;
                        OBJAc.departamento = seguimiento.departamento;
                        OBJAc.regional = seguimiento.regional;
                        OBJAc.fecha_nacimiento = seguimiento.fecha_nacimiento;
                        OBJAc.edad = seguimiento.edad;
                        OBJAc.tipo_salud = seguimiento.tipo_salud;
                        OBJAc.direccion = seguimiento.direccion;
                        OBJAc.telefono_1 = seguimiento.telefono_1;
                        OBJAc.telefono_2 = seguimiento.telefono_2;
                        OBJAc.correo = seguimiento.correo;
                        OBJAc.incluido_en_sivigila = seguimiento.incluido_en_sivigila;
                        OBJAc.tipificacion = seguimiento.tipificacion;
                        OBJAc.fecha_ingreso_pais = seguimiento.fecha_ingreso_pais;
                        OBJAc.seguimiento = seguimiento.seguimiento;
                        OBJAc.estado_base = seguimiento.estado_base;
                        OBJAc.causa_finalizacion_del_seguimiento = seguimiento.causa_finalizacion_del_seguimiento;
                        OBJAc.observacion_de_caracterizacion = seguimiento.observacion_de_caracterizacion;
                        OBJAc.fecha_caracterizacion_registro = seguimiento.fecha_caracterizacion_registro;
                        OBJAc.nombre_quien_reporta = seguimiento.nombre_quien_reporta;
                        OBJAc.auditor_asignado = seguimiento.auditor_asignado;
                        OBJAc.usuario_cargue =  seguimiento.usuario_cargue;
                        //OBJAc.fecha_cargue = seguimiento.fecha_cargue;


                        BusClass.Actualizacasocarguecovid19(OBJAc,ref MsgRes);


                    }



                }
                conteoingresadas = IntContador;
                conteorepetidas = IntContador2;
                BusClass.InsertarConsolidadoSeguimientoCovid19(OBJ, ref MsgRes);

                cargue_seguimiento_covid19_detalle detalle = new cargue_seguimiento_covid19_detalle();
                List<cargue_seguimiento_covid19> listacarguecovid = BusClass.ConsultaCargueCovid19(ref MsgRes).ToList();

                foreach (var item in listacarguecovid)
                {
                    cargue_seguimiento_covid19_detalle insert = new cargue_seguimiento_covid19_detalle();
                    List<cargue_seguimiento_covid19_detalle> listacarguedetallecovid = BusClass.ConsultaDetalleSeguimientoCovid19(item.id ,ref MsgRes).ToList();
                    if (listacarguedetallecovid.Count == 0)
                    {
                        insert.id_cargue = item.id;
                        insert.observaciones = "DETALLE DEL PRIMER CARGUE";
                        insert.id_estado_gestion_asalud = 1;
                        insert.fecha_digita = DateTime.Now;
                        insert.usuario_digita = SesionVar.UserName;



                        OBJDetalle.Add(insert);
                        insert = new cargue_seguimiento_covid19_detalle();
                    }
                    else
                    {
                     



                    }


                }
                if (OBJDetalle.Count != 0)
                {
                    BusClass.InsertarSeguimientoDetalleCovid19(OBJDetalle, ref MsgRes);
                }



            }

            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message + "Linea: " + IntContador.ToString();
            }
        }


        public Int32 InsertarSeguimientoCovid19Detalle(cargue_seguimiento_covid19_detalle OBJ)
        {
            return BusClass.InsertarSeguimientoCovid19Detalle(OBJ, ref MsgRes);
        }


        public void ActualizarEstadoSeguimientoCovid19(cargue_seguimiento_covid19 OBJ)
        {
            BusClass.ActualizarEstadoSeguimientoCovid19(OBJ, ref MsgRes);
        }

        public void ConsultaTotales()
        {
            if (SesionVar.ROL == "1")
            {
                List<vw_seguimiento_covid19_diario> LstCasosTotales = BusClass.ConsultaListadoseguimientoCovid19();
                LstCasosTotales = LstCasosTotales.Where(l => l.estado != "5").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.seguimiento == "1. DIARIO").ToList();

                List<vw_seguimiento_covid19_diario> Lstsingestiondiaria = new List<vw_seguimiento_covid19_diario>();
                Lstsingestiondiaria = LstCasosTotales.Where(x => x.AlertaDiaria == "SI").ToList();
                List<vw_seguimiento_covid19_diario> Lstaldia = new List<vw_seguimiento_covid19_diario>();
                Lstaldia = LstCasosTotales.Where(x => x.AlertaDiaria == "NO").ToList();
     

                total_casos = LstCasosTotales.Count();
                singestion = Lstsingestiondiaria.Count();
                congestion = Lstaldia.Count();
           

            }
            else
            {

                List<vw_seguimiento_covid19_diario> LstCasosTotales = BusClass.ConsultaListadoseguimientoCovid19();
                LstCasosTotales = LstCasosTotales.Where(l => l.estado != "5").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.seguimiento == "1. DIARIO").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();

                List<vw_seguimiento_covid19_diario> Lstsingestiondiaria = new List<vw_seguimiento_covid19_diario>();
                Lstsingestiondiaria = LstCasosTotales.Where(x => x.AlertaDiaria == "SI").ToList();
                List<vw_seguimiento_covid19_diario> Lstaldia = new List<vw_seguimiento_covid19_diario>();
                Lstaldia = LstCasosTotales.Where(x => x.AlertaDiaria == "NO").ToList();


                total_casos = LstCasosTotales.Count();
                singestion = Lstsingestiondiaria.Count();
                congestion = Lstaldia.Count();

            }


        }

        public void ConsultaTotalesInterdiario()
        {
            if (SesionVar.ROL == "1")
            {
                List<vw_seguimiento_covid19_interdiario> LstCasosTotales = BusClass.ConsultaListadoseguimientoInterdiarioCovid19();
                LstCasosTotales = LstCasosTotales.Where(l => l.estado == "5").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                List<vw_seguimiento_covid19_interdiario> Lstsingestiondiaria = new List<vw_seguimiento_covid19_interdiario>();
                Lstsingestiondiaria = LstCasosTotales.Where(x => x.AlertaDiaria == "SI").ToList();
                List<vw_seguimiento_covid19_interdiario> Lstaldia = new List<vw_seguimiento_covid19_interdiario>();
                Lstaldia = LstCasosTotales.Where(x => x.AlertaDiaria == "NO").ToList();


                total_casosinter = LstCasosTotales.Count();
                singestioninter = Lstsingestiondiaria.Count();
                congestioninter = Lstaldia.Count();


            }
            else
            {

                List<vw_seguimiento_covid19_interdiario> LstCasosTotales = BusClass.ConsultaListadoseguimientoInterdiarioCovid19();
                LstCasosTotales = LstCasosTotales.Where(l => l.estado == "5").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.seguimiento == "2. INTERDIARIO").ToList();
                LstCasosTotales = LstCasosTotales.Where(l => l.auditor_asignado == SesionVar.UserName).ToList();

                List<vw_seguimiento_covid19_interdiario> Lstsingestiondiaria = new List<vw_seguimiento_covid19_interdiario>();
                Lstsingestiondiaria = LstCasosTotales.Where(x => x.AlertaDiaria == "SI").ToList();
                List<vw_seguimiento_covid19_interdiario> Lstaldia = new List<vw_seguimiento_covid19_interdiario>();
                Lstaldia = LstCasosTotales.Where(x => x.AlertaDiaria == "NO").ToList();


                total_casosinter = LstCasosTotales.Count();
                singestioninter = Lstsingestiondiaria.Count();
                congestioninter = Lstaldia.Count();

            }


        }



    }
}