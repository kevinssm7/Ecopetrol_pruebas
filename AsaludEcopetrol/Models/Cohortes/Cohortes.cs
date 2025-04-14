using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
namespace AsaludEcopetrol.Models.Cohortes
{
    public class Cohortes
    {
        #region PROPIEDADES

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
        #endregion


        #region METODOS

        public List<ref_cohortes> Get_refCohortes()
        {
            return BusClass.Get_refCohortes();
        }
        public List<ref_cohortes> Get_refCohortesSindh()
        {
            return BusClass.Get_refCohortesSindh();
        }


        public void InsertCohortesEPOC(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaepoc, ref MessageResponseOBJ MsgRes)
        {
            _BusClass.InsertCohortesEPOC(cargue, listaepoc, ref MsgRes);
        }

        public void InsertCohortesPAD(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaPAD, ref MessageResponseOBJ MsgRes)
        {
            _BusClass.InsertCohortesPAD(cargue, listaPAD, ref MsgRes);
        }

        public void InsertCohortesRCV(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaRCV, ref MessageResponseOBJ MsgRes)
        {
            _BusClass.InsertCohortesRCV(cargue, listaRCV, ref MsgRes);
        }

        public void InsertCohortesGESTANTES(cohortes_cargue_base cargue, List<cohortes_detalle_cargue_OK> listaGestantes, ref MessageResponseOBJ MsgRes)
        {
            _BusClass.InsertCohortesGESTANTES(cargue, listaGestantes, ref MsgRes);
        }

        public class HttpPostedFileBaseCustom : HttpPostedFileBase
        {
            private byte[] _Bytes;
            private String _ContentType;
            private String _FileName;
            private MemoryStream _Stream;

            public override Int32 ContentLength { get { return this._Bytes.Length; } }
            public override String ContentType { get { return this._ContentType; } }
            public override String FileName { get { return this._FileName; } }

            public override Stream InputStream
            {
                get
                {
                    if (this._Stream == null)
                    {
                        this._Stream = new MemoryStream(this._Bytes);
                    }
                    return this._Stream;
                }
            }

            public HttpPostedFileBaseCustom(byte[] contentData, String contentType, String fileName)
            {
                this._ContentType = contentType;
                this._FileName = fileName;
                this._Bytes = contentData ?? new byte[0];
            }

            public override void SaveAs(String filename)
            {
                System.IO.File.WriteAllBytes(filename, this._Bytes);
            }

        }


        public void InsertarGrcp(DataTable dt, cargue_vigilanciaCohortes_lote objbase, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContadorFilas = 0;
            Int32 id_lote = 0;
            Int32 fila = 1;
            string columna = "";
            string texto = "";
            string textError = "";
            List<cargue_vigilanciaCohortesRegistros_normal> result = new List<cargue_vigilanciaCohortesRegistros_normal>();
            id_lote = BusClass.InsertarGRCP(objbase);
            var mensaje = "";

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    cargue_vigilanciaCohortesRegistros_normal obj = new cargue_vigilanciaCohortesRegistros_normal();
                    fila = fila + 1;
                    IntContadorFilas = IntContadorFilas + 1;

                    if (!string.IsNullOrEmpty(item["ID"].ToString()))
                    {
                        obj.id_lote = id_lote;

                        columna = "ID";
                        texto = Convert.ToString(item["ID"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.id = Convert.ToInt32(item["ID"]);
                        }

                        columna = "DOCUMENTO PACIENTE";
                        texto = Convert.ToString(item["DOCUMENTO PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.documento_paciente = Convert.ToString(item["DOCUMENTO PACIENTE"]);
                        }

                        columna = "NOMBRE PACIENTE";
                        texto = Convert.ToString(item["NOMBRE PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.nombre_paciente = Convert.ToString(item["NOMBRE PACIENTE"]);
                        }

                        columna = "FECHA DE NACIMIENTO";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE NACIMIENTO"]);

                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["FECHA DE NACIMIENTO"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = "Formato incorrecto";
                            throw new Exception(error);
                        }

                        columna = "EDAD PACIENTE";
                        texto = Convert.ToString(item["EDAD PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.edad_paciente = Convert.ToInt32(item["EDAD PACIENTE"]);
                        }

                        columna = "CIUDAD PRESTACIÓN";
                        texto = Convert.ToString(item["CIUDAD PRESTACIÓN"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.ciudad_prestacion = Convert.ToString(item["CIUDAD PRESTACIÓN"]);
                        }

                        columna = "LOCALIDAD PRESTACIÓN";
                        texto = Convert.ToString(item["LOCALIDAD PRESTACIÓN"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.localidad_prestacion = Convert.ToString(item["LOCALIDAD PRESTACIÓN"]);
                        }

                        columna = "REGIONAL PRESTACION";
                        texto = Convert.ToString(item["REGIONAL PRESTACION"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.regional_prestacion = Convert.ToString(item["REGIONAL PRESTACION"]);
                        }

                        columna = "NOMBRE PRESTADOR";
                        texto = Convert.ToString(item["NOMBRE PRESTADOR"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.nombre_prestador = Convert.ToString(item["NOMBRE PRESTADOR"]);
                        }

                        columna = "ESPECIALIDAD";
                        texto = Convert.ToString(item["ESPECIALIDAD"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.especialidad = Convert.ToString(item["ESPECIALIDAD"]);
                        }

                        columna = "FECHA DE PRESTACIÓN";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE PRESTACIÓN"]);

                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_prestacion = Convert.ToDateTime(item["FECHA DE PRESTACIÓN"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = "Formato incorrecto";
                            throw new Exception(error);
                        }

                        columna = "TIPO DE DIAGNOSTICO";
                        texto = Convert.ToString(item["TIPO DE DIAGNOSTICO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.tipo_diagnostico = Convert.ToString(item["TIPO DE DIAGNOSTICO"]);
                        }

                        columna = "CODIGO DX PRINCIPAL";
                        texto = Convert.ToString(item["CODIGO DX PRINCIPAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_principal = Convert.ToString(item["CODIGO DX PRINCIPAL"]);
                        }

                        columna = "CODIGO DX RELACIONADO 1";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 1"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_1 = Convert.ToString(item["CODIGO DX RELACIONADO 1"]);
                        }

                        columna = "CODIGO DX RELACIONADO 2";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 2"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_2 = Convert.ToString(item["CODIGO DX RELACIONADO 2"]);
                        }

                        columna = "CODIGO DX RELACIONADO 3";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 3"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_3 = Convert.ToString(item["CODIGO DX RELACIONADO 3"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX PRINCIPAL";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX PRINCIPAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_principal = Convert.ToString(item["DESCRIPCIÓN CODIGO DX PRINCIPAL"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 1";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 1"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_1 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 1"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 2";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 2"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_2 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 2"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 3";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 3"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_3 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 3"]);
                        }

                        columna = "POSICIÓN DIAGNOSTICO DETECTADO";
                        texto = Convert.ToString(item["POSICIÓN DIAGNOSTICO DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.posicion_diagnostico_detectado = Convert.ToString(item["POSICIÓN DIAGNOSTICO DETECTADO"]);
                        }

                        columna = "CODIGO DX DETECTADO";
                        texto = Convert.ToString(item["CODIGO DX DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_detectado = Convert.ToString(item["CODIGO DX DETECTADO"]);
                        }

                        columna = "DESCRIPCION DX DETECTADO";
                        texto = Convert.ToString(item["DESCRIPCION DX DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_dx_detectado = Convert.ToString(item["DESCRIPCION DX DETECTADO"]);
                        }

                        columna = "CODIGO DE LA CONSULTA";
                        texto = Convert.ToString(item["CODIGO DE LA CONSULTA"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_de_consulta = Convert.ToString(item["CODIGO DE LA CONSULTA"]);
                        }

                        columna = "DESCRIPCION DE LA CONSULTA";
                        texto = Convert.ToString(item["DESCRIPCION DE LA CONSULTA"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_consulta = Convert.ToString(item["DESCRIPCION DE LA CONSULTA"]);
                        }

                        columna = "PROGRAMA POTENCIAL";
                        texto = Convert.ToString(item["PROGRAMA POTENCIAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.programa_potencial = Convert.ToString(item["PROGRAMA POTENCIAL"]);
                        }

                        columna = "FUENTE";
                        texto = Convert.ToString(item["FUENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.fuente = Convert.ToString(item["FUENTE"]);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        result.Add(obj);
                        obj = new cargue_vigilanciaCohortesRegistros_normal();
                    }
                }

                BusClass.InsertarDatosGrpc(result, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                {
                    BusClass.EliminarCargueCohorte(id_lote, objbase.id_tipo, ref MsgRes);
                    mensaje = "Error en el cargue";
                }
                else
                {
                    mensaje = "Cargue completado";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna - " + columna + " : " + ex.Message;
                MsgRes.CodeError = ex.Message;

                mensaje = "Cargue completado";

                try
                {
                    BusClass.EliminarCargueCohorte(id_lote, objbase.id_tipo, ref MsgRes);
                }
                catch
                {
                    MsgRes.DescriptionResponse = "Error eliminando el cargue base";
                    MsgRes.CodeError = ex.Message;
                }

                throw new Exception(MsgRes.DescriptionResponse);

            }
        }

        public void InsertarGrcpEPOC(DataTable dt, cargue_vigilanciaCohortes_lote objbase, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContadorFilas = 0;
            Int32 id_lote = 0;
            Int32 fila = 1;
            string columna = "";
            string texto = "";
            string textError = "";
            List<cargue_vigilanciaCohortesRegistros_EPOC> result = new List<cargue_vigilanciaCohortesRegistros_EPOC>();
            id_lote = BusClass.InsertarGRCP(objbase);
            var mensaje = "";

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    cargue_vigilanciaCohortesRegistros_EPOC obj = new cargue_vigilanciaCohortesRegistros_EPOC();
                    fila = fila + 1;
                    IntContadorFilas = IntContadorFilas + 1;

                    if (!string.IsNullOrEmpty(item["ID"].ToString()))
                    {
                        obj.id_lote = id_lote;

                        columna = "ID";
                        texto = Convert.ToString(item["ID"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.id = Convert.ToInt32(item["ID"]);
                        }

                        columna = "DOCUMENTO PACIENTE";
                        texto = Convert.ToString(item["DOCUMENTO PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.documento_paciente = Convert.ToString(item["DOCUMENTO PACIENTE"]);
                        }

                        columna = "NOMBRE PACIENTE";
                        texto = Convert.ToString(item["NOMBRE PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.nombre_paciente = Convert.ToString(item["NOMBRE PACIENTE"]);
                        }

                        columna = "FECHA DE NACIMIENTO";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE NACIMIENTO"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["FECHA DE NACIMIENTO"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = "Formato incorrecto";
                            throw new Exception(error);
                        }

                        columna = "EDAD PACIENTE";
                        texto = Convert.ToString(item["EDAD PACIENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.edad_paciente = Convert.ToInt32(item["EDAD PACIENTE"]);
                        }

                        columna = "CIUDAD PRESTACIÓN";
                        texto = Convert.ToString(item["CIUDAD PRESTACIÓN"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.ciudad_prestacion = Convert.ToString(item["CIUDAD PRESTACIÓN"]);
                        }

                        columna = "LOCALIDAD PRESTACIÓN";
                        texto = Convert.ToString(item["LOCALIDAD PRESTACIÓN"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.localidad_prestacion = Convert.ToString(item["LOCALIDAD PRESTACIÓN"]);
                        }

                        columna = "REGIONAL PRESTACION";
                        texto = Convert.ToString(item["REGIONAL PRESTACION"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.regional_prestacion = Convert.ToString(item["REGIONAL PRESTACION"]);
                        }

                        columna = "NOMBRE PRESTADOR";
                        texto = Convert.ToString(item["NOMBRE PRESTADOR"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.nombre_prestador = Convert.ToString(item["NOMBRE PRESTADOR"]);
                        }

                        columna = "ESPECIALIDAD";
                        texto = Convert.ToString(item["ESPECIALIDAD"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.especialidad = Convert.ToString(item["ESPECIALIDAD"]);
                        }

                        columna = "FECHA DE PRESTACIÓN";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE PRESTACIÓN"]);

                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_prestacion = Convert.ToDateTime(item["FECHA DE PRESTACIÓN"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = "Formato incorrecto";
                            throw new Exception(error);
                        }

                        columna = "TIPO DE DIAGNOSTICO";
                        texto = Convert.ToString(item["TIPO DE DIAGNOSTICO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.tipo_diagnostico = Convert.ToString(item["TIPO DE DIAGNOSTICO"]);
                        }

                        columna = "CODIGO DX PRINCIPAL";
                        texto = Convert.ToString(item["CODIGO DX PRINCIPAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_principal = Convert.ToString(item["CODIGO DX PRINCIPAL"]);
                        }

                        columna = "CODIGO DX RELACIONADO 1";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 1"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_1 = Convert.ToString(item["CODIGO DX RELACIONADO 1"]);
                        }

                        columna = "CODIGO DX RELACIONADO 2";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 2"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_2 = Convert.ToString(item["CODIGO DX RELACIONADO 2"]);
                        }

                        columna = "CODIGO DX RELACIONADO 3";
                        texto = Convert.ToString(item["CODIGO DX RELACIONADO 3"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_relacionado_3 = Convert.ToString(item["CODIGO DX RELACIONADO 3"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX PRINCIPAL";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX PRINCIPAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_principal = Convert.ToString(item["DESCRIPCIÓN CODIGO DX PRINCIPAL"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 1";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 1"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_1 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 1"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 2";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 2"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_2 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 2"]);
                        }

                        columna = "DESCRIPCIÓN CODIGO DX RELACIONADO 3";
                        texto = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 3"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_codigo_dx_relacionado_3 = Convert.ToString(item["DESCRIPCIÓN CODIGO DX RELACIONADO 3"]);
                        }

                        columna = "POSICIÓN DIAGNOSTICO DETECTADO";
                        texto = Convert.ToString(item["POSICIÓN DIAGNOSTICO DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.posicion_diagnostico_detectado = Convert.ToString(item["POSICIÓN DIAGNOSTICO DETECTADO"]);
                        }

                        columna = "CODIGO DX DETECTADO";
                        texto = Convert.ToString(item["CODIGO DX DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_dx_detectado = Convert.ToString(item["CODIGO DX DETECTADO"]);
                        }

                        columna = "DESCRIPCION DX DETECTADO";
                        texto = Convert.ToString(item["DESCRIPCION DX DETECTADO"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_dx_detectado = Convert.ToString(item["DESCRIPCION DX DETECTADO"]);
                        }

                        columna = "CODIGO DE LA CONSULTA";
                        texto = Convert.ToString(item["CODIGO DE LA CONSULTA"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.codigo_de_consulta = Convert.ToString(item["CODIGO DE LA CONSULTA"]);
                        }

                        columna = "DESCRIPCION DE LA CONSULTA";
                        texto = Convert.ToString(item["DESCRIPCION DE LA CONSULTA"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.descripcion_consulta = Convert.ToString(item["DESCRIPCION DE LA CONSULTA"]);
                        }

                        columna = "PROGRAMA POTENCIAL";
                        texto = Convert.ToString(item["PROGRAMA POTENCIAL"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.programa_potencial = Convert.ToString(item["PROGRAMA POTENCIAL"]);
                        }

                        columna = "FUENTE";
                        texto = Convert.ToString(item["FUENTE"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.fuente = Convert.ToString(item["FUENTE"]);
                        }

                        columna = "ACCION";
                        texto = Convert.ToString(item["ACCION"]);
                        if (!string.IsNullOrEmpty(texto))
                        {
                            obj.accion = Convert.ToString(item["ACCION"]);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        result.Add(obj);
                        obj = new cargue_vigilanciaCohortesRegistros_EPOC();
                    }
                }

                BusClass.InsertarDatosGrpcEpoc(result, ref MsgRes);

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                {
                    BusClass.EliminarCargueCohorte(id_lote, objbase.id_tipo, ref MsgRes);
                    mensaje = "Error en el cargue";
                }
                else
                {
                    mensaje = "Cargue completado";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna - " + columna + " : " + ex.Message;
                MsgRes.CodeError = ex.Message;

                mensaje = "Cargue completado";

                try
                {
                    BusClass.EliminarCargueCohorte(id_lote, objbase.id_tipo, ref MsgRes);
                }
                catch
                {
                    MsgRes.DescriptionResponse = "Error eliminando el cargue base";
                    MsgRes.CodeError = ex.Message;
                }

                throw new Exception(MsgRes.DescriptionResponse);
            }
        }


        #endregion

    }
}