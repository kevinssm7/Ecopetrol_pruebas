using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECOPETROL_COMMON.ENTIDADES;
using ANALITICA_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace DATA_ACCESS
{
    public class ConsultasDac
    {
        #region PROPIEDADES

        private ActualizarDac _DACActualiza;
        public ActualizarDac DACActualiza
        {
            get
            {
                if (_DACActualiza != null)
                {
                    return _DACActualiza;
                }
                else
                {
                    return _DACActualiza = new ActualizarDac();
                }

            }
            set { _DACActualiza = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        #region LOGIN

        public List<sis_usuario> ValidaIngreso(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    ListResult = db.sis_usuario.Where(p => p.usuario == ObjUsuario.usuario && p.contraseña == ObjUsuario.contraseña && p.id_estado == 1).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    if (ListResult.Count != 0)
                    {
                        DACActualiza.ActualizaIngreso(ObjUsuario.usuario, ref MsgRes);
                    }
                    return ListResult;
                }


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public sis_usuario ValidaIngresoClave(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            sis_usuario ListResult = new sis_usuario();
            sis_usuario ListResult2 = new sis_usuario();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    ListResult = db.sis_usuario.Where(p => p.usuario == ObjUsuario.usuario && p.numero_documento == ObjUsuario.numero_documento && p.id_estado == 1).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    if (ListResult != null)
                    {
                        sis_usuario obj = new sis_usuario();

                        obj.id_usuario = ListResult.id_usuario;
                        obj.usuario = ListResult.usuario;

                        DACActualiza.ActualizaContraseña2(obj, ref MsgRes);

                        ListResult2 = db.sis_usuario.Where(p => p.usuario == ObjUsuario.usuario && p.numero_documento == ObjUsuario.numero_documento && p.id_estado == 1).FirstOrDefault();

                        return ListResult2;
                    }
                    else
                    {
                        return ListResult;
                    }

                }


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }


        public List<ManagmentMenuResult> ManagmentMenu(String Strusuario, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentMenuResult> ListResult = new List<ManagmentMenuResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ManagmentMenu(Strusuario).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<sis_usuario> BuscaAuditorUsu(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.sis_usuario.Where(p => p.usuario == strUsuario).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<sis_usuario> BuscaAuditorNom(String strNombre, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.sis_usuario.Where(p => p.nombre.Contains(strNombre)).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public void GestionUsuarios(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (ObjUsuario.id_estado == 1)
                    {
                        sis_usuario objAdi = db.sis_usuario.Single(p => p.id_usuario == ObjUsuario.id_usuario);
                        objAdi.contraseña = "temporal";
                        objAdi.id_estado = ObjUsuario.id_estado;
                        db.SubmitChanges();
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }

                    if (ObjUsuario.id_estado == 2 || ObjUsuario.id_estado == 3)
                    {
                        sis_usuario objAdi = db.sis_usuario.Single(p => p.id_usuario == ObjUsuario.id_usuario);
                        objAdi.id_estado = ObjUsuario.id_estado;
                        db.SubmitChanges();
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public DateTime ManagmentHora()
        {
            ManagmentHoraActualResult obj = new ManagmentHoraActualResult();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                obj = db.ManagmentHoraActual().Single();
                DateTime dt = obj.Column1;
                return dt;
            }
        }

        public List<sis_usuario> BuscaAuditorDoc(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.sis_usuario.Where(p => p.numero_documento == strUsuario).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_pruebas_img_usuarios> BuscaAuditorImg(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            List<vw_pruebas_img_usuarios> ListResult = new List<vw_pruebas_img_usuarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_pruebas_img_usuarios.Where(p => p.numero_documento == strUsuario).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }
        public List<sis_usuario> BuscaAuditorUsuSami(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.sis_usuario.Where(p => p.usuario == strUsuario).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }


        public List<sis_usuario> GetUsuarios()
        {
            List<sis_usuario> ListResult = new List<sis_usuario>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    ListResult = db.sis_usuario.ToList();
                    return ListResult;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return ListResult;
                }

            }
        }

        public List<management_sis_usuarios_controlActividadesCensoResult> GetUsuariosCenso()
        {
            List<management_sis_usuarios_controlActividadesCensoResult> ListResult = new List<management_sis_usuarios_controlActividadesCensoResult>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    ListResult = db.management_sis_usuarios_controlActividadesCenso().ToList();
                    return ListResult;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return ListResult;
                }

            }
        }

        public List<management_sis_usuarios_controlActividadesCenso_optimizadaResult> GetUsuariosCensoOptimizada(int? regional, string documento, string nombre)
        {
            List<management_sis_usuarios_controlActividadesCenso_optimizadaResult> ListResult = new List<management_sis_usuarios_controlActividadesCenso_optimizadaResult>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                try
                {
                    ListResult = db.management_sis_usuarios_controlActividadesCenso_optimizada(regional, documento, nombre).ToList();
                    return ListResult;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = ex.Message;
                    return ListResult;
                }

            }
        }


        #endregion

        #region CENSO

        public List<vw_tablero_levante_egreso> GetlevanteEgreso(String Documento, ref MessageResponseOBJ MsgRes)
        {
            List<vw_tablero_levante_egreso> ListResult = new List<vw_tablero_levante_egreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_tablero_levante_egreso.Where(p => p.id_afi.Contains(Documento)).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_datos_censo> CensoDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            List<vw_datos_censo> ListResult = new List<vw_datos_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_datos_censo.Where(p => p.num_identifi_afil == Documento).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

                return ListResult;
            }

        }

        public List<vw_datos_censo> CensoFacturas(ref MessageResponseOBJ MsgRes)
        {
            List<vw_datos_censo> ListResult = new List<vw_datos_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_datos_censo.ToList();

                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<management_datos_censoResult> CensoFacturasDetallado(string documento, string nombre, DateTime? fechaEgresoConcu, DateTime? fechaEgresoCenso, ref MessageResponseOBJ MsgRes)
        {
            List<management_datos_censoResult> ListResult = new List<management_datos_censoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.management_datos_censo(documento, nombre, fechaEgresoConcu, fechaEgresoCenso).ToList();

                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_datos_censo> CensoId(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            List<vw_datos_censo> ListResult = new List<vw_datos_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_datos_censo.Where(p => p.id_censo == Id).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<ecop_censo> GetCensoId(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_censo> ListResult = new List<ecop_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ecop_censo.Where(p => p.id_censo == Id).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<VW_base_beneficiarios> BeneficiariosDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            List<VW_base_beneficiarios> ListResult = new List<VW_base_beneficiarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.VW_base_beneficiarios.Where(p => p.Numero_identificacion == Documento).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<VW_base_beneficiarios> GetBeneficiarios(string term, ref MessageResponseOBJ MsgRes)
        {
            List<VW_base_beneficiarios> ListResult = new List<VW_base_beneficiarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (!string.IsNullOrEmpty(term))
                    {
                        var queryAllCustomers = from item in db.VW_base_beneficiarios
                                                where item.Numero_identificacion.ToString().StartsWith(term) || item.Nombre.StartsWith(term) || item.Apellidos.StartsWith(term) && item.edad != null
                                                select item;

                        ListResult = queryAllCustomers.ToList();
                    }
                    else
                    {
                        ListResult = db.VW_base_beneficiarios.ToList();
                    }

                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<base_beneficiarios> GetUltimoBeneficiarios(string documento, string tipo, ref MessageResponseOBJ MsgRes)
        {
            List<base_beneficiarios> lista = new List<base_beneficiarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.base_beneficiarios.Where(x => x.Numero_identificacion == documento && x.Clase_de_identificacion == tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lista;
        }


        public List<base_beneficiarios_analitica> TraerBeneficiarioDocumento(string documento)
        {
            List<base_beneficiarios_analitica> lista = new List<base_beneficiarios_analitica>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.base_beneficiarios_analitica.Where(x => x.Numero_identificacion.Contains(documento)).OrderByDescending(x => x.id_base_beneficiarios).Take(15).ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lista;
        }


        public List<base_beneficiarios_analitica> TraerBeneficiarioDocumentoActivo(string documento)
        {
            List<base_beneficiarios_analitica> lista = new List<base_beneficiarios_analitica>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.base_beneficiarios_analitica.Where(x => x.Numero_identificacion == documento && x.estado == "ACTIVO").OrderByDescending(x => x.id_base_beneficiarios).Take(5).ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lista;
        }

        public int ExisteBeneficiario(string documento)
        {
            int retorno = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    base_beneficiarios_analitica bas = db.base_beneficiarios_analitica.FirstOrDefault(x => x.Numero_identificacion == documento);
                    retorno = bas != null ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return retorno;
        }


        public management_buscarBeneficiarios_MorNatResult TraerDatosBeneficiario(int? idBB)
        {
            management_buscarBeneficiarios_MorNatResult dato = new management_buscarBeneficiarios_MorNatResult();
            try
            {

                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    dato = db.management_buscarBeneficiarios_MorNat(idBB).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_baseBeneficiariosPeriodoValidoResult> GetBeneficiariosPerodoValido(int mes, int año)
        {
            List<management_baseBeneficiariosPeriodoValidoResult> list = new List<management_baseBeneficiariosPeriodoValidoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_baseBeneficiariosPeriodoValido(mes, año).ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var mensaje = e.Message;
                return list;
            }
        }

        public List<vw_consulta_censo> ConsultaCenso(ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_censo> ListResult = new List<vw_consulta_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_censo.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_consulta_ecop> ConsultaCensoConcurrencia(ref MessageResponseOBJ MsgRes)

        {
            List<vw_consulta_ecop> ListResult = new List<vw_consulta_ecop>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_ecop.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de agosto de 2022
        /// Segunda consulta de censo con concurrencia
        /// </summary>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_Consulta_EcopResult> ConsultaCensoConcurrenciaII(int tipo, int? regional, string documento, DateTime? fechaInicio, DateTime? fechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<Management_Consulta_EcopResult> ListResult = new List<Management_Consulta_EcopResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Management_Consulta_Ecop(tipo, regional, documento, fechaInicio, fechaFinal).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<Management_Consulta_Ecop2Result> ConsultaCensoConcurrenciaII2(int tipo, int? regional, string documento, DateTime? fechaInicio, DateTime? fechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<Management_Consulta_Ecop2Result> ListResult = new List<Management_Consulta_Ecop2Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Management_Consulta_Ecop2(tipo, regional, documento, fechaInicio, fechaFinal).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<ref_censo_caso_especial> listaCensoCasosEspeciales()
        {
            List<ref_censo_caso_especial> lista = new List<ref_censo_caso_especial>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_censo_caso_especial.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_censo_linea_pago> listaCensoLineasPago()
        {
            List<ref_censo_linea_pago> lista = new List<ref_censo_linea_pago>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_censo_linea_pago.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public management_censo_ultimaHabitacionResult datosEgreso(int? idCenso)
        {
            management_censo_ultimaHabitacionResult dato = new management_censo_ultimaHabitacionResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    dato = db.management_censo_ultimaHabitacion(idCenso).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_censo_tableroDetalladoResult> GetCensoDetallado(DateTime? fechaInicio, DateTime? fechaFin, string documento)
        {
            List<management_censo_tableroDetalladoResult> lstResult = new List<management_censo_tableroDetalladoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_censo_tableroDetallado(fechaInicio, fechaFin, documento).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<management_hospitalizacionPrevenible_TableroResult> GetHospitalizacionPrevenible()
        {
            List<management_hospitalizacionPrevenible_TableroResult> list = new List<management_hospitalizacionPrevenible_TableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_hospitalizacionPrevenible_Tablero().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public management_hospitalizacionPrevenible_detalleResult GetHospitalizacionPrevenibleDetalle(int idHE)
        {
            management_hospitalizacionPrevenible_detalleResult list = new management_hospitalizacionPrevenible_detalleResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_hospitalizacionPrevenible_detalle(idHE).FirstOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_hospitalizacionPrevenible_detalle_gestionResult> GetHospitalizacionPrevenibleDetalle_gestion(int idHE)
        {
            List<management_hospitalizacionPrevenible_detalle_gestionResult> list = new List<management_hospitalizacionPrevenible_detalle_gestionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_hospitalizacionPrevenible_detalle_gestion(idHE).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_hospitalizacionPrevenible_reporteResult> GetHospitalizacionPrevenible_Reporte()
        {
            List<management_hospitalizacionPrevenible_reporteResult> list = new List<management_hospitalizacionPrevenible_reporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_hospitalizacionPrevenible_reporte().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public ecop_HE_gestion_documental buscarArchivoHEDtll(int HEDtll)
        {
            ecop_HE_gestion_documental dato = new ecop_HE_gestion_documental();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_HE_gestion_documental.Where(x => x.id_he_dtll == HEDtll).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }
        public ecop_VE_gestion_documental buscarArchivoVE(int idVE, int tipo)
        {
            ecop_VE_gestion_documental dato = new ecop_VE_gestion_documental();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_VE_gestion_documental.Where(x => x.id_VE == idVE && x.tipo == tipo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }
        public List<management_vigilancia_epidemiologica_tableroResult> GetVigilanciaEpidemiologica()
        {
            List<management_vigilancia_epidemiologica_tableroResult> list = new List<management_vigilancia_epidemiologica_tableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_vigilancia_epidemiologica_tablero().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<management_concurrencia_alertasResult> ConsultaConcurrenciaAlertasEvolucion()
        {
            List<management_concurrencia_alertasResult> list = new List<management_concurrencia_alertasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_concurrencia_alertas().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_concurrencia_alerta_ReporteResult> ConsultaConcurrenciaAlertasEvolucionReporte()
        {
            List<management_concurrencia_alerta_ReporteResult> list = new List<management_concurrencia_alerta_ReporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_concurrencia_alerta_Reporte().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_he_analisisCaso> ListAnalisisCasoHE()
        {
            List<ref_he_analisisCaso> list = new List<ref_he_analisisCaso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_he_analisisCaso.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<ref_he_analisisCaso_si> ListAnalisisCasoHESi()
        {
            List<ref_he_analisisCaso_si> list = new List<ref_he_analisisCaso_si>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_he_analisisCaso_si.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<ref_he_analisisCaso_no> ListAnalisisCasoHENo()
        {
            List<ref_he_analisisCaso_no> list = new List<ref_he_analisisCaso_no>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_he_analisisCaso_no.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public int ConsultaConcurrenciaAlertasEvolucionConteo()
        {
            var conteo = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    conteo = db.management_concurrencia_alertas().ToList().Count();
                    return conteo;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return 0;
            }
        }


        public List<vw_consulta_pacientesActivos> ConsultaPacientesActivos()

        {
            List<vw_consulta_pacientesActivos> ListResult = new List<vw_consulta_pacientesActivos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_pacientesActivos.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_tablero_plan_mejora_ben> ConsultaTableroPlanBen()
        {
            List<vw_tablero_plan_mejora_ben> ListResult = new List<vw_tablero_plan_mejora_ben>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_tablero_plan_mejora_ben.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<management_egresBuscar_megaResult> BuscarMegaEgreso(string documento)
        {
            List<management_egresBuscar_megaResult> list = new List<management_egresBuscar_megaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_egresBuscar_mega(documento).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<management_censo_aseguramientoTableroControlResult> DatosCensoAseguramiento()
        {
            List<management_censo_aseguramientoTableroControlResult> list = new List<management_censo_aseguramientoTableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_censo_aseguramientoTableroControl().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_censo_aseguramientoTableroControl_detallesResult> DatosCensoAseguramiento_detalleId(int? idRegistro)
        {
            List<management_censo_aseguramientoTableroControl_detallesResult> list = new List<management_censo_aseguramientoTableroControl_detallesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_censo_aseguramientoTableroControl_detalles(idRegistro).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public management_censo_aseguramientoTableroControl_idResult TraerRegistroAH(int? idRegistro)
        {
            management_censo_aseguramientoTableroControl_idResult dato = new management_censo_aseguramientoTableroControl_idResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_censo_aseguramientoTableroControl_id(idRegistro).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<Ref_tipo_habitacion> ListadoTipoGabitacion()
        {
            List<Ref_tipo_habitacion> lista = new List<Ref_tipo_habitacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_tipo_habitacion.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }



        #endregion

        #region CONCURRENCIA

        public List<ecop_concurrencia> ConsultaAfiliadoConcurrenia(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia> lstResult = new List<ecop_concurrencia>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia.Where(p => p.id_afi == ObjAfiliado.id_afi && p.afi_tipo_doc == ObjAfiliado.afi_tipo_doc).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia> ConsultaIdConcurrenia(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia> lstResult = new List<ecop_concurrencia>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public ecop_concurrencia ConsultaConcurrenciaId(int idconcurrencia)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ecop_concurrencia.Where(l => l.id_concurrencia == idconcurrencia).FirstOrDefault();
        }

        public List<ecop_concurrencia> ConsultaConcurrenciaIdLista(Int32? idconcurrencia, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia> lstResult = new List<ecop_concurrencia>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia.Where(p => p.id_concurrencia == idconcurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_censo> ConsultaCensoIdLista(Int32? idcenso, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_censo> lstResult = new List<ecop_censo>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_censo.Where(p => p.id_censo == idcenso).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public ecop_censo ConsultaCensoIdentificacionPac(string idPaciente)
        {
            ecop_censo dato = new ecop_censo();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_censo.Where(x => x.num_identifi_afil.Equals(idPaciente)).OrderByDescending(x => x.id_censo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public ecop_censo ConsultaCensoIdCenso(int? idCenso)
        {
            ecop_censo dato = new ecop_censo();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_censo.Where(x => x.id_censo == idCenso).OrderByDescending(x => x.id_censo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }


        public List<vw_censo_control_concurrencia> CensoConcurrenciasTotales()
        {
            List<vw_censo_control_concurrencia> lista = new List<vw_censo_control_concurrencia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.vw_censo_control_concurrencia.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_censo_control_concurrencia_optimizadaResult> CensoConcurrenciasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            List<management_censo_control_concurrencia_optimizadaResult> lista = new List<management_censo_control_concurrencia_optimizadaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.management_censo_control_concurrencia_optimizada(regional, documento, nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<vw_censo_control_cuentasMedicas> CensoCuentasMedicasTotales()
        {
            List<vw_censo_control_cuentasMedicas> lista = new List<vw_censo_control_cuentasMedicas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.vw_censo_control_cuentasMedicas.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_censo_control_cuentasMedicas_optimizadaResult> CensoCuentasMedicasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            List<management_censo_control_cuentasMedicas_optimizadaResult> lista = new List<management_censo_control_cuentasMedicas_optimizadaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.management_censo_control_cuentasMedicas_optimizada(regional, documento, nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<vw_censo_control_visitas> CensoVisitasTotales()
        {
            List<vw_censo_control_visitas> lista = new List<vw_censo_control_visitas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.vw_censo_control_visitas.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_censo_control_visitas_optimizadaResult> CensoVisitasTotalesOptimizada(int? regional, string documento, string nombre)
        {
            List<management_censo_control_visitas_optimizadaResult> lista = new List<management_censo_control_visitas_optimizadaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.management_censo_control_visitas_optimizada(regional, documento, nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_ecop_censo_tiposConsulta> CensoConsultaReportesActividades()
        {
            List<ref_ecop_censo_tiposConsulta> lista = new List<ref_ecop_censo_tiposConsulta>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_ecop_censo_tiposConsulta.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public management_datosPaciente_alertasResult DatosPaciente(int idConcurrencia)
        {
            management_datosPaciente_alertasResult list = new management_datosPaciente_alertasResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_datosPaciente_alertas(idConcurrencia).OrderByDescending(x => x.id_concurrencia).FirstOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<vw_evo_ecop_concurrencia> ConsultaIdConcurreniaEvo(vw_evo_ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<vw_evo_ecop_concurrencia> lstResult = new List<vw_evo_ecop_concurrencia>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_evo_ecop_concurrencia.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }


        public List<ecop_concurrencia_evolucion> ConsultaNumeroEvoluciones(ecop_concurrencia_evolucion ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_evolucion> lstResult = new List<ecop_concurrencia_evolucion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_evolucion.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<vw_ecop_cohortes_evolucion> ConsultaCohortes(vw_ecop_cohortes_evolucion ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ecop_cohortes_evolucion> lstResult = new List<vw_ecop_cohortes_evolucion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_cohortes_evolucion.Where(p => p.No_documento == ObjAfiliado.No_documento).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }


        public List<vw_concurrencia_evolucion_Contrato> ConsultaIdConcurreniaEvolucion(ecop_concurrencia ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<vw_concurrencia_evolucion_Contrato> lstResult = new List<vw_concurrencia_evolucion_Contrato>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_concurrencia_evolucion_Contrato.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }
        public List<vw_ciudad_ips> ConsultaIdConcurreniaciudad(vw_ciudad_ips ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ciudad_ips> lstResult = new List<vw_ciudad_ips>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ciudad_ips.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia> ConsultaCriterioIngresoActualizado(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia> ListResult = new List<ecop_concurrencia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    ListResult = db.ecop_concurrencia.Where(p => p.id_concurrencia == IdConcu && p.afi_edad != 0).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<ecop_concurrencia_encuesta_satisfacion> ConsultaEncuestaCargada(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_encuesta_satisfacion> ListResult = new List<ecop_concurrencia_encuesta_satisfacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ecop_concurrencia_encuesta_satisfacion.Where(p => p.id_concurrencia == IdConcu).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_consulta_concurrencia> ConsultaConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_concurrencia> ListResult = new List<vw_consulta_concurrencia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    ListResult = db.vw_consulta_concurrencia.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)



            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_consulta_egreso> ConsultaEgreso(ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_egreso> ListResult = new List<vw_consulta_egreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_egreso.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<managment_vw_consulta_egresoResult> ConsultaEgreso2(ref MessageResponseOBJ MsgRes)
        {
            List<managment_vw_consulta_egresoResult> ListResult = new List<managment_vw_consulta_egresoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    ListResult = db.managment_vw_consulta_egreso().ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }



        public List<vw_consulta_eventos_adversos> ConsultaEventosAd(ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_eventos_adversos> ListResult = new List<vw_consulta_eventos_adversos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_eventos_adversos.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_consulta_situacion_calidad> ConsultaSituacionCal(ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_situacion_calidad> ListResult = new List<vw_consulta_situacion_calidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_consulta_situacion_calidad.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<vw_gestantes> ConsultaGestantes(ref MessageResponseOBJ MsgRes)
        {
            List<vw_gestantes> ListResult = new List<vw_gestantes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_gestantes.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<management_controlNatalidadResult> ConsultaGestantesNuevo(ref MessageResponseOBJ MsgRes)
        {
            List<management_controlNatalidadResult> ListResult = new List<management_controlNatalidadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.management_controlNatalidad().ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_gestantes_sin> ConsultaGestantesSin(ref MessageResponseOBJ MsgRes)
        {
            List<vw_gestantes_sin> ListResult = new List<vw_gestantes_sin>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_gestantes_sin.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<vw_Mortalidad> ConsultaMortalidad(ref MessageResponseOBJ MsgRes)
        {
            List<vw_Mortalidad> ListResult = new List<vw_Mortalidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_Mortalidad.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<ManagementConsultaConcuMortalidadCon_SinResult> ConsultaMortalidadV2(int tipoconsulta, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ManagementConsultaConcuMortalidadCon_Sin(tipoconsulta).ToList();

        }

        public List<vw_Mortalidad_sin> ConsultaMortalidadSin(ref MessageResponseOBJ MsgRes)
        {
            List<vw_Mortalidad_sin> ListResult = new List<vw_Mortalidad_sin>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_Mortalidad_sin.ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_tipo_habitacion_censo> ConsultaTipoHabitacion(vw_tipo_habitacion_censo ObjAfiliado, ref MessageResponseOBJ MsgRes)
        {
            List<vw_tipo_habitacion_censo> lstResult = new List<vw_tipo_habitacion_censo>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tipo_habitacion_censo.Where(p => p.id_concurrencia == ObjAfiliado.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<egreso_auditoria_Hospitalaria> ConsultaAgresoH(Int32 IdConcu, ref MessageResponseOBJ MsgRes)
        {
            List<egreso_auditoria_Hospitalaria> ListResult = new List<egreso_auditoria_Hospitalaria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.egreso_auditoria_Hospitalaria.Where(p => p.id_concurrencia == IdConcu).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }


        public List<management_egresosEvolucionesResult> ConsultaEgresoId(int idEgreso)
        {
            List<management_egresosEvolucionesResult> list = new List<management_egresosEvolucionesResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_egresosEvoluciones(idEgreso).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<vw_max_concurrencia_por_documento> ConsultaMaxConcurrenciaDocumento(String Documento, ref MessageResponseOBJ MsgRes)
        {
            List<vw_max_concurrencia_por_documento> ListResult = new List<vw_max_concurrencia_por_documento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_max_concurrencia_por_documento.Where(p => p.id_afi == Documento).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }


        }

        public List<egreso_gestantes> ConsultasEgresoGestantes(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            List<egreso_gestantes> ListResult = new List<egreso_gestantes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.egreso_gestantes.Where(p => p.id_concurrencia == id_concurrencia).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

                return ListResult;
            }

        }

        public List<vw_ecop_egresos_hospitalarios> GetListaEgresos(DateTime? fechainicial, DateTime? fechafinal)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<vw_ecop_egresos_hospitalarios> lista = new List<vw_ecop_egresos_hospitalarios>();
            if (fechainicial != null && fechafinal != null)
            {
                lista = db.vw_ecop_egresos_hospitalarios.Where(l => l.fecha_egreso.Value.Date >= fechainicial.Value.Date && l.fecha_egreso.Value.Date <= fechafinal.Value.Date).ToList();
            }
            else
            {
                if (fechainicial != null && fechafinal == null)
                {
                    lista = db.vw_ecop_egresos_hospitalarios.Where(l => l.fecha_egreso.Value.Date >= fechainicial.Value.Date).ToList();
                }
                else
                {
                    lista = db.vw_ecop_egresos_hospitalarios.Where(l => l.fecha_egreso.Value.Date <= fechafinal.Value.Date).ToList();
                }
            }

            List<vw_ecop_egresos_hospitalarios> result = new List<vw_ecop_egresos_hospitalarios>();
            foreach (vw_ecop_egresos_hospitalarios i in lista)
            {
                var obj = db.categorizacion_egreso_hospitalario.Where(l => l.id_egreso_hospitalario == i.id_egreso_auditoria_Hospitalaria).FirstOrDefault();
                if (obj == null)
                {
                    result.Add(i);
                }
            }
            return result;

        }
        public List<management_ecop_egresos_hospitalariosResult> listadoEgresosHospitalarios(DateTime? fechainicial, DateTime? fechafinal)
        {
            List<management_ecop_egresos_hospitalariosResult> listado = new List<management_ecop_egresos_hospitalariosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_ecop_egresos_hospitalarios(fechainicial, fechafinal).ToList();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }

        }

        public List<ref_tipo_hospitalizacion> GetRefTipoHospitalizacion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_tipo_hospitalizacion.ToList();
        }
        public List<ref_tipo_patologia_catastrofica> GetRefTipoPatologiaCatastrofica()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_tipo_patologia_catastrofica.ToList();
        }
        public List<ref_pertinencia_estancia_prolongada> GetRefPertinenciaProlongada()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_pertinencia_estancia_prolongada.ToList();
        }
        public List<ref_condicion_estancia_prolongada> GetRefCondicionProlongada()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_condicion_estancia_prolongada.ToList();
        }


        public categorizacion_egreso_hospitalario getcatbyegreso(int idegreso)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.categorizacion_egreso_hospitalario.Where(l => l.id_egreso_hospitalario == idegreso).FirstOrDefault();
        }

        public int cantidaddias(int idconcurrencia)
        {
            int? dias = 0;
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var result = db.vw_evo_ecop_concurrencia_evoluciones.Where(l => l.id_concurrencia == idconcurrencia).ToList();
            if (result.Count > 0)
            {
                dias = result.Select(l => l.dias_de_estancia).ToList().Sum();
            }
            return dias.Value;
        }

        public List<ecop_concurrencia> GetconcurrenciaAfiliados(string numidafiliado)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<ecop_concurrencia> result = db.ecop_concurrencia.Where(l => l.id_afi.StartsWith(numidafiliado) && l.fecha_egreso == null).ToList();
            return result;
        }

        public List<Ref_ips> GetRefIps()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Ref_ips.ToList();
        }

        public List<ref_eps> GetRefEps()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_eps.ToList();
        }

        public List<ref_trimeste> GetRefTrimestre()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_trimeste.ToList();
        }
        public List<Ref_plan_mejora_categoria> GetRefplan_mejora_categoria()
        {
            List<Ref_plan_mejora_categoria> lista = new List<Ref_plan_mejora_categoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_plan_mejora_categoria.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<Ref_plan_mejora_foco> GetRef_plan_mejora_foco()
        {
            List<Ref_plan_mejora_foco> lista = new List<Ref_plan_mejora_foco>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_plan_mejora_foco.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<Ref_proceso_auditado> GetRef_proceso_auditado()
        {
            List<Ref_proceso_auditado> lista = new List<Ref_proceso_auditado>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_proceso_auditado.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_planmejora_focoResult> Cuentadetallefoco(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            List<management_planmejora_focoResult> lstResult = new List<management_planmejora_focoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planmejora_foco(id_plan_de_mejora).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planmejora_tareaResult> CuentadetalleTarea(Int32 id_plan_mejora_foco_intervencion, ref MessageResponseOBJ MsgRes)
        {
            List<management_planmejora_tareaResult> lstResult = new List<management_planmejora_tareaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planmejora_tarea(id_plan_mejora_foco_intervencion).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planmejora_tarea_controlResult> CuentadetalleTareacontrol(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            List<management_planmejora_tarea_controlResult> lstResult = new List<management_planmejora_tarea_controlResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planmejora_tarea_control(id_plan_mejora).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_plan_mejora_tableroResult> PlanTableroGeneral()
        {
            List<management_plan_mejora_tableroResult> lstResult = new List<management_plan_mejora_tableroResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_plan_mejora_tablero().ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planMejora_reporteResult> DatosPMReporte(int? idPlan)
        {
            List<management_planMejora_reporteResult> lstResult = new List<management_planMejora_reporteResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_reporte(idPlan).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }


        public List<management_planMejora_reporte_detalleCeacionResult> DatosPMReporteDetalleCreacion(int? idPlan)
        {
            List<management_planMejora_reporte_detalleCeacionResult> lstResult = new List<management_planMejora_reporte_detalleCeacionResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_reporte_detalleCeacion(idPlan).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }



        public List<management_planMejora_reporte_detalleCierreResult> DatosPMReporteDetalleCierre(int? idPlan)
        {
            List<management_planMejora_reporte_detalleCierreResult> lstResult = new List<management_planMejora_reporte_detalleCierreResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_reporte_detalleCierre(idPlan).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planMejora_tableroDocumentalResult> DatosPMgestionDocumental(int? regional, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_planMejora_tableroDocumentalResult> lstResult = new List<management_planMejora_tableroDocumentalResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_tableroDocumental(regional, fechaInicio, fechaFin).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planesMejora_reporteSeguimientoResult> DatosPMgestionDocumentalReporte(int? regional, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_planesMejora_reporteSeguimientoResult> lstResult = new List<management_planesMejora_reporteSeguimientoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planesMejora_reporteSeguimiento(regional, fechaInicio, fechaFin).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planMejora_tableroVisitasResult> DatosPMVisitas(string auditor)
        {
            List<management_planMejora_tableroVisitasResult> lstResult = new List<management_planMejora_tableroVisitasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_tableroVisitas(auditor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_planMejora_tableroVisitas> DatosPMVisitasRoles()
        {

            List<vw_planMejora_tableroVisitas> lstResult = new List<vw_planMejora_tableroVisitas>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_planMejora_tableroVisitas.OrderByDescending(x => x.id_cronograma_visitas).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }




        }


        public List<ref_planMejora_prioridad> listaPrioridadPM()
        {
            List<ref_planMejora_prioridad> lista = new List<ref_planMejora_prioridad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_planMejora_prioridad.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_plan_mejora_tablero_dtllResult> PlanTableroGeneralDtll(Int32 id_plan_de_mejora, ref MessageResponseOBJ MsgRes)
        {
            List<management_plan_mejora_tablero_dtllResult> lstResult = new List<management_plan_mejora_tablero_dtllResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_plan_mejora_tablero_dtll(id_plan_de_mejora).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planMejora_ampliacionesResult> PlanMejoraAmpliaciones(int? idPlan)
        {
            List<management_planMejora_ampliacionesResult> lstResult = new List<management_planMejora_ampliacionesResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_ampliaciones(idPlan).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planMejora_DocumentosPlanResult> PlanMejoraArchivoporTipo(int? idPlan, int? tipo)
        {
            List<management_planMejora_DocumentosPlanResult> lstResult = new List<management_planMejora_DocumentosPlanResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planMejora_DocumentosPlan(idPlan, tipo).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }


        public ecop_plan_de_mejora_documental PlanMejoraGestionDocumentalId(int? idPlan, int? tipo)
        {
            ecop_plan_de_mejora_documental dato = new ecop_plan_de_mejora_documental();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_plan_de_mejora_documental.Where(x => x.id_planmejora == idPlan && x.id_tipo == tipo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }

        public ecop_plan_de_mejora_documental PlanMejoraGestionDocumentalIdArchivo(int? idArchivo)
        {
            ecop_plan_de_mejora_documental dato = new ecop_plan_de_mejora_documental();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_plan_de_mejora_documental.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }


        public ecop_plan_de_mejora PlanMejoraId(int? idPlan)
        {
            ecop_plan_de_mejora dato = new ecop_plan_de_mejora();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_plan_de_mejora.Where(x => x.id_plan_de_mejora == idPlan).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }

        public List<management_plan_mejora_tablero_reactivacionResult> TraerListadoPlanesMejora(int? idPlan)
        {
            List<management_plan_mejora_tablero_reactivacionResult> listado = new List<management_plan_mejora_tablero_reactivacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_plan_mejora_tablero_reactivacion(idPlan).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<ref_medicion_riesgo> PlanMejoraMedicionRiesgo()
        {
            List<ref_medicion_riesgo> dato = new List<ref_medicion_riesgo>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_medicion_riesgo.ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }

        public List<ref_costos_noCalidad> PlanMejoraCostosNoCalidad()
        {
            List<ref_costos_noCalidad> dato = new List<ref_costos_noCalidad>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_costos_noCalidad.ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }

        public List<ref_cobertura> PlanMejoraCobertura()
        {
            List<ref_cobertura> dato = new List<ref_cobertura>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_cobertura.ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

            return dato;
        }

        public List<management_planmejora_tarea_obsResult> GetobsTareas(Int32 id_plan_mejora_tareas, ref MessageResponseOBJ MsgRes)
        {
            List<management_planmejora_tarea_obsResult> lstResult = new List<management_planmejora_tarea_obsResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planmejora_tarea_obs(id_plan_mejora_tareas).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_planmejora_tarea_control_estadoResult> CuentadetalleTareacontrolEstado(Int32 id_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            List<management_planmejora_tarea_control_estadoResult> lstResult = new List<management_planmejora_tarea_control_estadoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_planmejora_tarea_control_estado(id_plan_mejora).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_evolucionEgresosListaResult> GetEvolucionesConcurrencia(int idConcu)
        {
            List<management_evolucionEgresosListaResult> lst = new List<management_evolucionEgresosListaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lst = db.management_evolucionEgresosLista(idConcu).OrderByDescending(x => x.id_concurrencia).ToList();
                    return lst;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return lst;
            }
        }


        public List<management_planesMejora_alertaVencimientoResult> ListadoAlertasVencimiento()
        {
            List<management_planesMejora_alertaVencimientoResult> lista = new List<management_planesMejora_alertaVencimientoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_planesMejora_alertaVencimiento().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public management_planMejora_correosNotificacionIdPlanResult DatosNotificacionCorreos(int? idPlan)
        {
            management_planMejora_correosNotificacionIdPlanResult lista = new management_planMejora_correosNotificacionIdPlanResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_planMejora_correosNotificacionIdPlan(idPlan).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public log_planes_mejora_notificaciones TraerUltimoLogNotificacionPM(int? id_plan)
        {
            log_planes_mejora_notificaciones dato = new log_planes_mejora_notificaciones();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.log_planes_mejora_notificaciones.OrderByDescending(x => x.id_log).FirstOrDefault(x => x.id_plan == id_plan);
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            return dato;
        }

        public management_planmejora_datosIntervencionResult datosIntervencionPM(int? idInter)
        {
            management_planmejora_datosIntervencionResult dato = new management_planmejora_datosIntervencionResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_planmejora_datosIntervencion(idInter).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        #endregion

        #region EVOLUCION

        public List<ecop_concurrencia_evolucion> ConsultaEvoluciones(ecop_concurrencia_evolucion ObjEvolu, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_evolucion> lstResult = new List<ecop_concurrencia_evolucion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_evolucion.Where(p => p.id_concurrencia == ObjEvolu.id_concurrencia).OrderBy(p => p.fecha).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<vw_evo_ecop_concurrencia_evoluciones> ConsultaEvolucionesIps(vw_evo_ecop_concurrencia_evoluciones ObjEvolu, ref MessageResponseOBJ MsgRes)
        {
            List<vw_evo_ecop_concurrencia_evoluciones> lstResult = new List<vw_evo_ecop_concurrencia_evoluciones>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_evo_ecop_concurrencia_evoluciones.Where(p => p.id_concurrencia == ObjEvolu.id_concurrencia).OrderBy(p => p.fecha).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_evolucion_diag_def> ConsultaDiagnosticoDefinitivo(ecop_concurrencia_evolucion_diag_def Objdiagdef, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_evolucion_diag_def> lstResult = new List<ecop_concurrencia_evolucion_diag_def>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_evolucion_diag_def.Where(p => p.id_concurrencia == Objdiagdef.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ecop_concurrencia_cohorte> ConsultaCohorte(ecop_concurrencia_cohorte ObjCohorte, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_cohorte> lstResult = new List<ecop_concurrencia_cohorte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_cohorte.Where(p => p.id_concurrencia == ObjCohorte.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }



        public List<ecop_concurrencia_glosa> ConsultaGlosa(ecop_concurrencia_glosa ObjGlosa, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_glosa> lstResult = new List<ecop_concurrencia_glosa>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_glosa.Where(p => p.id_concurrencia == ObjGlosa.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_glosa> ConsultaGlosabyidconcurrencia(int idconcurrencia, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_glosa> lstResult = new List<ecop_concurrencia_glosa>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_glosa.Where(p => p.id_concurrencia == idconcurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_eventos_adversos> ConsultaEventoAdverso(ecop_concurrencia_eventos_adversos ObjEventoAdverso, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_eventos_adversos> lstResult = new List<ecop_concurrencia_eventos_adversos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_eventos_adversos.Where(p => p.id_concurrencia == ObjEventoAdverso.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_situaciones_de_calidad> ConsultaSituacionesCalidad(ecop_concurrencia_situaciones_de_calidad ObjSituCali, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_situaciones_de_calidad> lstResult = new List<ecop_concurrencia_situaciones_de_calidad>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_situaciones_de_calidad.Where(p => p.id_concurrencia == ObjSituCali.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> ConsultaProcQuirurgicosCance(ecop_concurrencia_procedimientos_quirurgicos_cancelados ObjProcQuir, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_procedimientos_quirurgicos_cancelados> lstResult = new List<ecop_concurrencia_procedimientos_quirurgicos_cancelados>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_procedimientos_quirurgicos_cancelados.Where(p => p.id_concurrencia == ObjProcQuir.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<vw_tablero_eventos_adversos> ReportesEventoAdverso()
        {
            List<vw_tablero_eventos_adversos> lstResult = new List<vw_tablero_eventos_adversos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tablero_eventos_adversos.ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ecop_concurrencia_ahorro> ConsultaAhorro(ecop_concurrencia_ahorro ObjAhorro, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_ahorro> lstResult = new List<ecop_concurrencia_ahorro>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_concurrencia_ahorro.Where(p => p.id_concurrencia == ObjAhorro.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<vw_ecop_concurrencia_ahorro> ConsultaAhorroOtro(vw_ecop_concurrencia_ahorro ObjAhorro, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ecop_concurrencia_ahorro> lstResult = new List<vw_ecop_concurrencia_ahorro>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_concurrencia_ahorro.Where(p => p.id_concurrencia == ObjAhorro.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }


        public List<vw_ecop_concurrencia_cohorte> ConsultaCohorte(vw_ecop_concurrencia_cohorte ObjCohorte, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ecop_concurrencia_cohorte> lstResult = new List<vw_ecop_concurrencia_cohorte>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_concurrencia_cohorte.Where(p => p.id_concurrencia == ObjCohorte.id_concurrencia).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }


        /// <summary>
        /// Metodo que permite obtener un listado de las causales de la glosa por medio del responsable de la misma.
        /// </summary>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Ref_causal_glosa> ConsultaCausalGlosa(int id_responsable_glosa, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_causal_glosa> ListResult = new List<Ref_causal_glosa>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Ref_causal_glosa.Where(l => l.responsable_glosa_id == id_responsable_glosa).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public vw_cie10_alertas ConsultaAlertaCie10(String idcie10, ref MessageResponseOBJ MsgRes)
        {
            vw_cie10_alertas result = new vw_cie10_alertas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.vw_cie10_alertas.Where(l => l.id_cie10 == idcie10).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public ref_cie10_detalle ConsultaAlertaCie10Detalle(String idcie10, ref MessageResponseOBJ MsgRes)
        {
            ref_cie10_detalle result = new ref_cie10_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_cie10_detalle.Where(l => l.cie10 == idcie10).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<Ref_valor_ahorro> ValorAhorro()
        {
            List<Ref_valor_ahorro> lstResult = new List<Ref_valor_ahorro>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_valor_ahorro.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_evo_ecop_concurrencia_evoluciones> GetConcurrenciaEvolucionById(int id_evolucion)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_evo_ecop_concurrencia_evoluciones.Where(l => l.id_evolucion == id_evolucion).ToList();
        }

        public vw_ecop_evo_evaluacion_pertinencia GetEvaluacionPertinenciaById(int idevolucion)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_ecop_evo_evaluacion_pertinencia.Where(l => l.id_evolucion == idevolucion).FirstOrDefault();
        }

        public List<management_egresos_verCategorizacionResult> traerDatosCategorizacionEgreso(int idEgreso)
        {
            List<management_egresos_verCategorizacionResult> dato = new List<management_egresos_verCategorizacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_egresos_verCategorizacion(idEgreso).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<Management_evolucion_procedimientosResult> ConsultaProcedimientosConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            List<Management_evolucion_procedimientosResult> lstResult = new List<Management_evolucion_procedimientosResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Management_evolucion_procedimientos().ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }


        #endregion

        #region CONSULTAS
        public List<ManagmentAlertasCalidadResult> CuentaFechaCargue(Int32 Opc, DateTime FechaInicial, DateTime FechaFin, String strProveedor, String strEstado, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentAlertasCalidadResult> lstResult = new List<ManagmentAlertasCalidadResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentAlertasCalidad(Opc, FechaInicial, FechaFin, strProveedor, strEstado).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_Devoluciones_sin_gestionar> DevolucionesSinGestion()
        {
            List<vw_Devoluciones_sin_gestionar> lstResult = new List<vw_Devoluciones_sin_gestionar>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_Devoluciones_sin_gestionar.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_facturas_sin_auditar> FacturasporAuditar()
        {
            List<vw_facturas_sin_auditar> lstResult = new List<vw_facturas_sin_auditar>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_facturas_sin_auditar.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_hallazgos_RIPS> HallazgosRipsSinGestion()
        {
            List<vw_hallazgos_RIPS> lstResult = new List<vw_hallazgos_RIPS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_hallazgos_RIPS.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_costo_evitado> CostoEvitado(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            List<vw_costo_evitado> ListResult = new List<vw_costo_evitado>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_costo_evitado.Where(p => p.id_factura_sin_censo == Id).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }
        public List<vw_facturas_diagnosticos> DiagnosticosCuentas(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            List<vw_facturas_diagnosticos> ListResult = new List<vw_facturas_diagnosticos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_facturas_diagnosticos.Where(p => p.id_factura_sin_censo == Id).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }
        public List<vw_ECOPETROL_DEVOLUCION_FAC> VwDevoluciones()
        {
            List<vw_ECOPETROL_DEVOLUCION_FAC> lstResult = new List<vw_ECOPETROL_DEVOLUCION_FAC>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ECOPETROL_DEVOLUCION_FAC.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_ECOPETROL_HALLAZGOS_RIPS> VwHallazgosRIPS()
        {
            List<vw_ECOPETROL_HALLAZGOS_RIPS> lstResult = new List<vw_ECOPETROL_HALLAZGOS_RIPS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ECOPETROL_HALLAZGOS_RIPS.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<ECOPETROL_RECEPCION_FACTURAS> VwRecepcionFacturas()
        {
            List<ECOPETROL_RECEPCION_FACTURAS> lstResult = new List<ECOPETROL_RECEPCION_FACTURAS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ECOPETROL_RECEPCION_FACTURAS.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<alertas_generadas_concurrencia> ConsultaAlertasConcurrencia(Int32 Idconcurrencia, string idcie10, ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                List<alertas_generadas_concurrencia> Rsult = new List<alertas_generadas_concurrencia>();
                Rsult = db.alertas_generadas_concurrencia.Where(l => l.id_concurrencia == Idconcurrencia && l.cie10 == idcie10).ToList();
                return Rsult;
            }
        }

        public List<ManagmentClinicosCensoConConcurrenciaResult> CensoConcurrenciaEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentClinicosCensoConConcurrenciaResult> lstResult = new List<ManagmentClinicosCensoConConcurrenciaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentClinicosCensoConConcurrencia(fecha_ini, fecha_final).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 21 de diciembre de 2022
        /// Metodo que reemplazara la consulta CensoConcurrenciaEcopetrol se añade consulta por SQLCOMMAND
        /// </summary>
        /// <param name="fecha_ini"></param>
        /// <param name="fecha_final"></param>
        /// <param name="conexion"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public DataTable CensoConcurrenciaEcopetrolII(DateTime fecha_ini, DateTime fecha_final, String conexion, ref MessageResponseOBJ MsgRes)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd = conn.CreateCommand();
                cmd.Connection.Open();
                cmd.CommandText = "set arithabort on;";
                cmd.ExecuteNonQuery();

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fecha_inicial", SqlDbType.DateTime);
                param[1] = new SqlParameter("@fecha_final", SqlDbType.DateTime);
                param[0].Value = fecha_ini;
                param[1].Value = fecha_final;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ManagmentClinicosCensoConConcurrencia";
                cmd.Connection = conn;
                cmd.CommandTimeout = 5000;
                cmd.Parameters.AddRange(param);

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Datos consultados correctamente";
                return dt;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                MsgRes.DescriptionResponse = "Ha ocurrido un error consultando los datos: " + ex.Message;
                return dt;
            }
        }

        public List<ManagmentClinicosCensoResult> CensoEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentClinicosCensoResult> lstResult = new List<ManagmentClinicosCensoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentClinicosCenso(fecha_ini, fecha_final).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ManagmentClinicosConsultasAlertasResult> AlertasEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentClinicosConsultasAlertasResult> lstResult = new List<ManagmentClinicosConsultasAlertasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentClinicosConsultasAlertas(fecha_ini, fecha_final).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }


        //leo

        #endregion

        #region FACTURAS

        public List<ManagmentReportDevolucionFacResult> ConsultaReporteDevolucionFac(Int32 id_devolucion_factura)
        {
            List<ManagmentReportDevolucionFacResult> lstResult = new List<ManagmentReportDevolucionFacResult>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.ManagmentReportDevolucionFac(id_devolucion_factura).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ManagmentReportDevolucionFac_glosasResult> ConsultaReporteDevolucionFac_glosa(int? id_af)
        {
            List<ManagmentReportDevolucionFac_glosasResult> lstResult = new List<ManagmentReportDevolucionFac_glosasResult>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.ManagmentReportDevolucionFac_glosas(id_af).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<ManagmentReportHallazgosRipResult> ConsultaReporteHallazgosRips(Int32 id_hallazgo_RIPS)
        {
            List<ManagmentReportHallazgosRipResult> lstResult = new List<ManagmentReportHallazgosRipResult>();

            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentReportHallazgosRip(id_hallazgo_RIPS).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<factura_sin_censo> ConsultaFacturasSinAudi(Int32 id_factura_sin_censo)
        {
            List<factura_sin_censo> lstResult = new List<factura_sin_censo>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.factura_sin_censo.Where(p => p.id_factura_sin_censo == id_factura_sin_censo).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }

        }

        public List<factura_devolucion> ConsultaDevolucionesFactura(String Numero_factura)
        {
            List<factura_devolucion> lstResult = new List<factura_devolucion>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.factura_devolucion.Where(p => p.NumeroFactura == Numero_factura).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<factura_sin_censo> ConsultaFacturaNumero(String Numero_factura)
        {
            List<factura_sin_censo> lstResult = new List<factura_sin_censo>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.factura_sin_censo.Where(p => p.numero_factura == Numero_factura).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<factura_devolucion> ConsultaDevolucionesFacturaId(Int32 Id_devolucion)
        {
            List<factura_devolucion> lstResult = new List<factura_devolucion>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.factura_devolucion.Where(p => p.id_devolucion_factura == Id_devolucion).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<hallazgo_RIPS> ConsultaHallazgosId(Int32 Id_rips)
        {
            List<hallazgo_RIPS> lstResult = new List<hallazgo_RIPS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.hallazgo_RIPS.Where(p => p.id_hallazgo_RIPS == Id_rips).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<management_rips_busqueda_acResult> TraerConsultaRIPSAC(DateTime? fechaInicio, DateTime? fechaFin, string codCups, string diagnostico, string cedula)
        {
            List<management_rips_busqueda_acResult> lista = new List<management_rips_busqueda_acResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_rips_busqueda_ac(fechaInicio, fechaFin, codCups, diagnostico, cedula).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_rips_busqueda_apResult> TraerConsultaRIPSAP(DateTime? fechaInicio, DateTime? fechaFin, string codCups, string diagnostico, string cedula)
        {
            List<management_rips_busqueda_apResult> lista = new List<management_rips_busqueda_apResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_rips_busqueda_ap(fechaInicio, fechaFin, codCups, diagnostico, cedula).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }
        #endregion

        #region RIPS
        public List<RIPS> ConsultaRips(Int32? IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<RIPS> lstResult = new List<RIPS>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 300;
                    lstResult = db.RIPS.ToList();
                    if (IdRips != null && IdRips != 0)
                    {
                        lstResult = lstResult.Where(l => l.id_rips == IdRips).ToList();
                    }

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Ref_RIPS_Prestadores> ConsultaPrestadores(string codhabilitacion, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_RIPS_Prestadores> lstResult = new List<Ref_RIPS_Prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (!String.IsNullOrEmpty(codhabilitacion))
                    {
                        lstResult = db.Ref_RIPS_Prestadores.Where(l => l.codigo_habilitacion == codhabilitacion).ToList();
                    }
                    else
                    {
                        lstResult = db.Ref_RIPS_Prestadores.ToList();
                    }

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }




        public List<Ref_RIPS_Prestadores> ConsultaPrestadoresRipsNit(double nit, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_RIPS_Prestadores> lstResult = new List<Ref_RIPS_Prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_RIPS_Prestadores.Where(l => l.nits_nit == nit).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Ref_RIPS_Prestadores> ConsultaPrestadoresRipsIdPrestador(string IDPrestador, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_RIPS_Prestadores> lstResult = new List<Ref_RIPS_Prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_RIPS_Prestadores.Where(l => l.codigo_habilitacion == IDPrestador).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        /// <summary>
        /// Consultar el reporte de evaluacion de los rips
        /// </summary>
        /// <param name="IdRips"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<ECOPETROL_COMMON.ENUM.reporterips> ConsultaRipsEvaluacion(Int32 IdRips, string conexion, ref MessageResponseOBJ MsgRes)
        {
            List<ECOPETROL_COMMON.ENUM.reporterips> lstResult = new List<ECOPETROL_COMMON.ENUM.reporterips>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    SqlConnection conn = new SqlConnection(conexion);
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = conn;
                    cmd = conn.CreateCommand();
                    cmd.Connection.Open();
                    cmd.CommandText = "set arithabort on;";
                    cmd.ExecuteNonQuery();

                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
                    param[0].Value = IdRips;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "managmentReporteRips";
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddRange(param);

                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        ECOPETROL_COMMON.ENUM.reporterips obj2 = new ECOPETROL_COMMON.ENUM.reporterips();

                        obj2.codhabilitacion = row.ItemArray[0].ToString();
                        obj2.razon_social = row.ItemArray[1].ToString();
                        obj2.muni_nombre = row.ItemArray[2].ToString();

                        int cantidad = int.Parse(row.ItemArray[3].ToString());
                        obj2.cantidad = cantidad;

                        int reg_facturados_opor = (int.Parse(row.ItemArray[4].ToString()) + int.Parse(row.ItemArray[5].ToString()) + int.Parse(row.ItemArray[6].ToString()) + int.Parse(row.ItemArray[7].ToString()) + int.Parse(row.ItemArray[8].ToString()));
                        obj2.registros_facturados_oportunamente = reg_facturados_opor;

                        double porcentaje_oportunidad = ((double)reg_facturados_opor / (double)cantidad) * 100;

                        double b = (porcentaje_oportunidad % 1);
                        if (b < 0.5)
                        {
                            obj2.porcentaje_oportunidad = Math.Round(porcentaje_oportunidad);
                        }
                        else
                        {
                            obj2.porcentaje_oportunidad = Math.Ceiling(porcentaje_oportunidad);
                        }

                        obj2.Errores_dx = (int.Parse(row.ItemArray[9].ToString()) + int.Parse(row.ItemArray[11].ToString()) + int.Parse(row.ItemArray[10].ToString()) + int.Parse(row.ItemArray[12].ToString()));

                        obj2.Errores_pc = int.Parse(row.ItemArray[13].ToString());

                        obj2.Errores_rc = (int.Parse(row.ItemArray[14].ToString()) + int.Parse(row.ItemArray[15].ToString()) + int.Parse(row.ItemArray[16].ToString()) + int.Parse(row.ItemArray[17].ToString()) + int.Parse(row.ItemArray[18].ToString()));

                        obj2.Total_Errores = (int.Parse(row.ItemArray[19].ToString()) + int.Parse(row.ItemArray[20].ToString()) + int.Parse(row.ItemArray[21].ToString()) + int.Parse(row.ItemArray[22].ToString()) + int.Parse(row.ItemArray[23].ToString()));

                        obj2.Registros_unicos_con_error = (int.Parse(row.ItemArray[24].ToString()) + int.Parse(row.ItemArray[25].ToString()) + int.Parse(row.ItemArray[26].ToString()) + int.Parse(row.ItemArray[27].ToString()) + int.Parse(row.ItemArray[28].ToString()));

                        int reg_unicos_sin_error = (int.Parse(row.ItemArray[29].ToString()) + int.Parse(row.ItemArray[30].ToString()) + int.Parse(row.ItemArray[31].ToString()) + int.Parse(row.ItemArray[32].ToString()) + int.Parse(row.ItemArray[33].ToString()));
                        obj2.Registros_sin_error = reg_unicos_sin_error;

                        double porcentaje_calidad = ((double)reg_unicos_sin_error / (double)cantidad) * 100;

                        double c = (porcentaje_calidad % 1);
                        if (c < 0.5)
                        {
                            obj2.porcentaje_calidad_rips = Math.Round(porcentaje_calidad);
                        }
                        else
                        {
                            obj2.porcentaje_calidad_rips = Math.Ceiling(porcentaje_calidad);
                        }

                        lstResult.Add(obj2);
                    }
                }

                return lstResult;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lstResult;
            }

        }


        public List<managmentReportePrestadoresNoExistentesResult> getprestadoresnoexistentes(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<managmentReportePrestadoresNoExistentesResult> lista = new List<managmentReportePrestadoresNoExistentesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.managmentReportePrestadoresNoExistentes(IdRips).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                lista = null;
            }
            return lista;
        }

        /// <summary>
        /// Consular el log del reporte de evaluacion de rips
        /// </summary>
        /// <param name="IdRips"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Logerroresevaluacionrips> ConsultaLogRipsEvaluacion(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<Logerroresevaluacionrips> listado = new List<Logerroresevaluacionrips>();
            try
            {

                string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd = conn.CreateCommand();
                cmd.Connection.Open();
                cmd.CommandText = "set arithabort on;";
                cmd.ExecuteNonQuery();

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
                param[0].Value = IdRips;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ManagmentErroresRipsEvaluacion_historico";
                cmd.Connection = conn;
                cmd.CommandTimeout = 3000;
                cmd.Parameters.AddRange(param);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);


                foreach (DataRow row in dt.Rows)
                {

                    Logerroresevaluacionrips lista = new Logerroresevaluacionrips();
                    lista.codigo_prestador = row.ItemArray[0].ToString();
                    lista.prestador = row.ItemArray[1].ToString();
                    lista.AC_Num_factura_no_existe_en_AF = Convert.ToInt32(row.ItemArray[2].ToString());
                    lista.AP_Num_factura_no_existe_en_AF = Convert.ToInt32(row.ItemArray[3].ToString());
                    lista.AU_Num_factura_no_existe_en_AF = Convert.ToInt32(row.ItemArray[4].ToString());
                    lista.AH_Num_factura_no_existe_en_AF = Convert.ToInt32(row.ItemArray[5].ToString());
                    lista.AC_Dx_no_corresponde_con_finalidad = Convert.ToInt32(row.ItemArray[6].ToString());
                    lista.AC_Usuario_debe_estar_en_US = Convert.ToInt32(row.ItemArray[7].ToString());
                    lista.AP_Usuario_debe_estar_en_US = Convert.ToInt32(row.ItemArray[8].ToString());
                    lista.AU_Usuario_debe_estar_en_US = Convert.ToInt32(row.ItemArray[9].ToString());
                    lista.AH_Usuario_debe_estar_en_US = Convert.ToInt32(row.ItemArray[10].ToString());
                    lista.AC_sin_DX = Convert.ToInt32(row.ItemArray[11].ToString());
                    lista.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado = Convert.ToInt32(row.ItemArray[12].ToString());
                    lista.AP_Sin_ambito_en_el_CUPS = Convert.ToInt32(row.ItemArray[13].ToString());
                    lista.AP_Sin_CUPS = Convert.ToInt32(row.ItemArray[14].ToString());
                    lista.AU_Sin_causa_basica_de_muerte = Convert.ToInt32(row.ItemArray[15].ToString());
                    lista.AU_Error_en_fecha_de_egreso = Convert.ToInt32(row.ItemArray[16].ToString());
                    lista.AU_Error_en_causa_externa = Convert.ToInt32(row.ItemArray[17].ToString());
                    lista.AH_Error_en_causa_externa = Convert.ToInt32(row.ItemArray[18].ToString());
                    lista.AU_Error_de_DX_no_aplica_R_Z = Convert.ToInt32(row.ItemArray[19].ToString());
                    lista.AH_Error_de_DX_no_aplica_R_Z = Convert.ToInt32(row.ItemArray[20].ToString());
                    lista.AN_Sin_fecha_de_muerte = Convert.ToInt32(row.ItemArray[21].ToString());
                    lista.AN_Sin_hora_de_muerte = Convert.ToInt32(row.ItemArray[22].ToString());
                    lista.Total_Errores = Convert.ToInt32(row.ItemArray[23].ToString()) + Convert.ToInt32(row.ItemArray[24].ToString()) + Convert.ToInt32(row.ItemArray[25].ToString()) + Convert.ToInt32(row.ItemArray[26].ToString()) + Convert.ToInt32(row.ItemArray[27].ToString());
                    listado.Add(lista);

                }

                return listado;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return listado;
            }
        }

        public List<management_rips_tableroControlResult> TraerListadoRips()
        {
            List<management_rips_tableroControlResult> lista = new List<management_rips_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_rips_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_rips_tableoControl_detalladoResult> TraerListadoRipsConsolidadoId(int? idRips)
        {
            List<management_rips_tableoControl_detalladoResult> lista = new List<management_rips_tableoControl_detalladoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_rips_tableoControl_detallado(idRips).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<RIPS> GetListaRipsPorMesYAño(int mes, int año, int? regional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<RIPS> lista = db.RIPS.Where(l => l.mes == mes.ToString() && l.año == año.ToString()).ToList();
            if (regional != null)
            {
                lista = lista.Where(l => l.id_regional == regional).ToList();
            }

            return lista;
        }

        public List<ManagmentErroresRipsEvaluacion_historicoResult> ConsultaLogRipsEvaluacionHistorico(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentErroresRipsEvaluacion_historicoResult> lstResult = new List<ManagmentErroresRipsEvaluacion_historicoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.ManagmentErroresRipsEvaluacion_historico(IdRips).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<LogerroresevaluacionripsDtll> ConsultaLogRipsEvaluacionDtll(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<LogerroresevaluacionripsDtll> lstResult = new List<LogerroresevaluacionripsDtll>();
            try
            {

                string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd = conn.CreateCommand();
                cmd.Connection.Open();
                cmd.CommandText = "set arithabort on;";
                cmd.ExecuteNonQuery();

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
                param[0].Value = IdRips;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ManagmentErroresRipsEvaluacion_Dtll";
                cmd.Connection = conn;
                cmd.CommandTimeout = 3000;
                cmd.Parameters.AddRange(param);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                foreach (DataRow row in dt.Rows)
                {
                    LogerroresevaluacionripsDtll lista = new LogerroresevaluacionripsDtll();

                    lista.id_rips = Convert.ToInt32(row.ItemArray[0].ToString());
                    lista.id_registro = Convert.ToInt32(row.ItemArray[1].ToString());
                    lista.codigo_prestador = row.ItemArray[2].ToString();
                    lista.registro_con_error = Convert.ToInt32(row.ItemArray[3].ToString());
                    lista.tipoarchivo = row.ItemArray[4].ToString();
                    lstResult.Add(lista);
                }
                return lstResult;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ManagmentErroresRipsEvaluacion_Dtll_historicoResult> ConsultaLogRipsEvaluacionDtllHistorico(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentErroresRipsEvaluacion_Dtll_historicoResult> lstResult = new List<ManagmentErroresRipsEvaluacion_Dtll_historicoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.ManagmentErroresRipsEvaluacion_Dtll_historico(IdRips).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        /// <summary>
        /// Consultar los totales de el reporte de evaluacion rips
        /// </summary>
        /// <param name="IdRips"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public vw_totales_rips_evaluacion ConsultaTotalesRipsEvaluacion(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            vw_totales_rips_evaluacion lstResult = new vw_totales_rips_evaluacion();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.vw_totales_rips_evaluacion.Where(l => l.id_rips == IdRips).FirstOrDefault();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<managmentRips_AC_FechaconsultaResult> ConsultaRipsFechaConsulta(DateTime FechaInicial, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<managmentRips_AC_FechaconsultaResult> lstResult = new List<managmentRips_AC_FechaconsultaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.managmentRips_AC_Fechaconsulta(FechaInicial.Date, FechaFinal.Date).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<managmentRips_AP_FechaProcedimientoResult> ConsultaRipsFechaProcedimiento(int? regional, DateTime FechaInicial, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<managmentRips_AP_FechaProcedimientoResult> lstResult = new List<managmentRips_AP_FechaProcedimientoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;

                    if (regional != 0 && regional != 7)
                    {
                        lstResult = db.managmentRips_AP_FechaProcedimiento(FechaInicial.Date, FechaFinal.Date, regional).ToList();
                    }
                    else
                    {
                        lstResult = db.managmentRips_AP_FechaProcedimiento(FechaInicial.Date, FechaFinal.Date, regional).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_consulta_rips_an_fechanacimiento> ConsultaRipsFechaNacimiento(DateTime FechaInicial, DateTime FechaFinal, ref MessageResponseOBJ MsgRes)
        {

            List<vw_consulta_rips_an_fechanacimiento> lstResult = new List<vw_consulta_rips_an_fechanacimiento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.vw_consulta_rips_an_fechanacimiento.Where(l => l.fecha_nacimiento_rn.Value.Date >= FechaInicial.Date && l.fecha_nacimiento_rn.Value.Date <= FechaFinal.Date).OrderBy(l => l.fecha_nacimiento_rn).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Ref_tipo_rips> ConsultaTipoRips(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_tipo_rips> lstResult = new List<Ref_tipo_rips>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_rips.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_consulta_rips_ah_mortalidad> GetListRipsMortalidadAH(DateTime? FechaInicial, DateTime? FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_rips_ah_mortalidad> lstResult = new List<vw_consulta_rips_ah_mortalidad>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lstResult = db.vw_consulta_rips_ah_mortalidad.Where(l => l.fecha_salida.Value.Date >= FechaInicial.Value.Date && l.fecha_salida.Value.Date <= FechaFinal.Value.Date).OrderBy(l => l.fecha_salida).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_consulta_rips_au_mortalidad> GetListRipsMortalidadAU(DateTime? FechaInicial, DateTime? FechaFinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_consulta_rips_au_mortalidad> lstResult = new List<vw_consulta_rips_au_mortalidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 300;
                    lstResult = db.vw_consulta_rips_au_mortalidad.Where(l => l.fecha_salida.Value.Date >= FechaInicial.Value.Date && l.fecha_salida.Value.Date <= FechaFinal.Value.Date).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        /// <summary>
        /// Validacion de los rips
        /// </summary>
        /// <param name="idregional"></param>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public RIPS ValidacionCargueRips(int idregional, int mes, int año)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS.Where(l => l.id_regional == idregional && Convert.ToInt32(l.mes) == Convert.ToInt32(mes) && Convert.ToInt32(l.año) == Convert.ToInt32(año)).FirstOrDefault();
            }
        }

        public RIPS_AC GetRipsAcById(int idripsac)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS_AC.Where(l => l.id_ac == idripsac).FirstOrDefault();
            }
        }

        public RIPS_AP GetRipsApById(int idripsap)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS_AP.Where(l => l.id_ap == idripsap).FirstOrDefault();
            }
        }

        public RIPS_AU GetRipsAuById(int id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS_AU.Where(l => l.id_au == id).FirstOrDefault();
            }
        }

        public RIPS_AH GetRipsAhById(int id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS_AH.Where(l => l.id_ah == id).FirstOrDefault();
            }
        }

        public RIPS_AN GetRipsAnById(int id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.RIPS_AN.Where(l => l.id_an == id).FirstOrDefault();
            }
        }


        public List<RIPS_AC_HISTORICO> GetRipsAcHistoricoById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "ManagmentRipsCorrectosAC";
            }
            else
            {
                cmd.CommandText = "ManagmentRipsIncorrectosAC";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AC_HISTORICO> list = new List<RIPS_AC_HISTORICO>();

            foreach (DataRow row in dt.Rows)
            {
                RIPS_AC_HISTORICO obj2 = new RIPS_AC_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.codigo_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[4].ToString()))
                    obj2.fecha_consulta = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.num_autorizacion = row.ItemArray[5].ToString();
                obj2.cod_consulta = row.ItemArray[6].ToString();
                obj2.finalidad_consulta = row.ItemArray[7].ToString();
                obj2.causa_externa = row.ItemArray[8].ToString();
                obj2.cod_dx_ppal = row.ItemArray[9].ToString();
                obj2.cod_dx_rel_1 = row.ItemArray[10].ToString();
                obj2.cod_dx_rel_2 = row.ItemArray[11].ToString();
                obj2.cod_dx_rel_3 = row.ItemArray[12].ToString();
                obj2.tipo_dx_ppal = row.ItemArray[13].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[14].ToString()))
                    obj2.valor_consulta = Convert.ToDecimal(row.ItemArray[14].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[15].ToString()))
                    obj2.valor_cuota_moderadora = Convert.ToDecimal(row.ItemArray[15].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[16].ToString()))
                    obj2.valor_neto_a_pagar = Convert.ToDecimal(row.ItemArray[16].ToString());
                list.Add(obj2);
            }

            return list;
        }
        public List<RIPS_AC_HISTORICO> GetRipsAcOportunidadById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "management_rips_ac_oportunos";
            }
            else
            {
                cmd.CommandText = "management_rips_ac_inoportunos";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AC_HISTORICO> list = new List<RIPS_AC_HISTORICO>();

            foreach (DataRow row in dt.Rows)
            {
                RIPS_AC_HISTORICO obj2 = new RIPS_AC_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.codigo_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[4].ToString()))
                    obj2.fecha_consulta = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.num_autorizacion = row.ItemArray[5].ToString();
                obj2.cod_consulta = row.ItemArray[6].ToString();
                obj2.finalidad_consulta = row.ItemArray[7].ToString();
                obj2.causa_externa = row.ItemArray[8].ToString();
                obj2.cod_dx_ppal = row.ItemArray[9].ToString();
                obj2.cod_dx_rel_1 = row.ItemArray[10].ToString();
                obj2.cod_dx_rel_2 = row.ItemArray[11].ToString();
                obj2.cod_dx_rel_3 = row.ItemArray[12].ToString();
                obj2.tipo_dx_ppal = row.ItemArray[13].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[14].ToString()))
                    obj2.valor_consulta = Convert.ToDecimal(row.ItemArray[14].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[15].ToString()))
                    obj2.valor_cuota_moderadora = Convert.ToDecimal(row.ItemArray[15].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[16].ToString()))
                    obj2.valor_neto_a_pagar = Convert.ToDecimal(row.ItemArray[16].ToString());
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AP_HISTORICO> GetRipsApHistoricoById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "ManagmentRipsCorrectosAP";
            }
            else
            {
                cmd.CommandText = "ManagmentRipsIncorrectosAP";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AP_HISTORICO> list = new List<RIPS_AP_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AP_HISTORICO obj2 = new RIPS_AP_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.fecha_procedimiento = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.num_autorizacion = row.ItemArray[5].ToString();
                obj2.cod_procedimiento = row.ItemArray[6].ToString();
                obj2.ambito_procedimiento = row.ItemArray[7].ToString();
                obj2.finalidad_procedimiento = row.ItemArray[8].ToString();
                obj2.personal_atiende = row.ItemArray[9].ToString();
                obj2.dx_ppal = row.ItemArray[10].ToString();
                obj2.dx_rel = row.ItemArray[11].ToString();
                obj2.complicacion = row.ItemArray[12].ToString();
                obj2.forma_acto_quirurgico = row.ItemArray[13].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[14].ToString()))
                    obj2.valor_procedimiento = Convert.ToDecimal(row.ItemArray[14].ToString());
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AP_HISTORICO> GetRipsApOportunidadById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "management_rips_ap_oportunos";
            }
            else
            {
                cmd.CommandText = "management_rips_ap_inoportunos";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AP_HISTORICO> list = new List<RIPS_AP_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AP_HISTORICO obj2 = new RIPS_AP_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.fecha_procedimiento = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.num_autorizacion = row.ItemArray[5].ToString();
                obj2.cod_procedimiento = row.ItemArray[6].ToString();
                obj2.ambito_procedimiento = row.ItemArray[7].ToString();
                obj2.finalidad_procedimiento = row.ItemArray[8].ToString();
                obj2.personal_atiende = row.ItemArray[9].ToString();
                obj2.dx_ppal = row.ItemArray[10].ToString();
                obj2.dx_rel = row.ItemArray[11].ToString();
                obj2.complicacion = row.ItemArray[12].ToString();
                obj2.forma_acto_quirurgico = row.ItemArray[13].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[14].ToString()))
                    obj2.valor_procedimiento = Convert.ToDecimal(row.ItemArray[14].ToString());
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AU_HISTORICO> GetRipsAuHistoricoById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "ManagmentRipsCorrectosAU";
            }
            else
            {
                cmd.CommandText = "ManagmentRipsIncorrectosAU";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AU_HISTORICO> list = new List<RIPS_AU_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AU_HISTORICO obj2 = new RIPS_AU_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.fecha_ingreso_observacion = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.hora_ingreso_observacion = row.ItemArray[5].ToString();
                obj2.num_autorizacion = row.ItemArray[6].ToString();
                obj2.causa_externa = row.ItemArray[7].ToString();
                obj2.dx_salida = row.ItemArray[8].ToString();
                obj2.dx_rel_salida_1 = row.ItemArray[9].ToString();
                obj2.dx_rel_salida_2 = row.ItemArray[10].ToString();
                obj2.dx_rel_salida_3 = row.ItemArray[11].ToString();
                obj2.destino_usuario_salida = row.ItemArray[12].ToString();
                obj2.estado_usuario_salida = row.ItemArray[13].ToString();
                obj2.causa_basica_muerte = row.ItemArray[14].ToString();
                obj2.fecha_salida = Convert.ToDateTime(row.ItemArray[15].ToString());
                obj2.hora_salida = row.ItemArray[16].ToString();
                list.Add(obj2);
            }

            return list;
        }
        public List<RIPS_AU_HISTORICO> GetRipsAuoportunidadById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "management_rips_au_oportunos";
            }
            else
            {
                cmd.CommandText = "management_rips_au_inoportunos";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AU_HISTORICO> list = new List<RIPS_AU_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AU_HISTORICO obj2 = new RIPS_AU_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.fecha_ingreso_observacion = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.hora_ingreso_observacion = row.ItemArray[5].ToString();
                obj2.num_autorizacion = row.ItemArray[6].ToString();
                obj2.causa_externa = row.ItemArray[7].ToString();
                obj2.dx_salida = row.ItemArray[8].ToString();
                obj2.dx_rel_salida_1 = row.ItemArray[9].ToString();
                obj2.dx_rel_salida_2 = row.ItemArray[10].ToString();
                obj2.dx_rel_salida_3 = row.ItemArray[11].ToString();
                obj2.destino_usuario_salida = row.ItemArray[12].ToString();
                obj2.estado_usuario_salida = row.ItemArray[13].ToString();
                obj2.causa_basica_muerte = row.ItemArray[14].ToString();
                obj2.fecha_salida = Convert.ToDateTime(row.ItemArray[15].ToString());
                obj2.hora_salida = row.ItemArray[16].ToString();
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AH_HISTORICO> GetRipsAhHistoricoById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "ManagmentRipsCorrectosAH";
            }
            else
            {
                cmd.CommandText = "ManagmentRipsIncorrectosAH";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AH_HISTORICO> list = new List<RIPS_AH_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AH_HISTORICO obj2 = new RIPS_AH_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.via_ingreso = row.ItemArray[4].ToString();
                obj2.fecha_ingreso = Convert.ToDateTime(row.ItemArray[5].ToString());
                obj2.hora_ingreso = row.ItemArray[6].ToString();
                obj2.num_autorizacion = row.ItemArray[7].ToString();
                obj2.causa_externa = row.ItemArray[8].ToString();
                obj2.dx_ppal_ingreso = row.ItemArray[9].ToString();
                obj2.dx_ppal_egreso = row.ItemArray[10].ToString();
                obj2.dx_rel_1_egreso = row.ItemArray[11].ToString();
                obj2.dx_rel_2_egreso = row.ItemArray[12].ToString();
                obj2.dx_rel_3_egreso = row.ItemArray[13].ToString();
                obj2.dx_complicacion = row.ItemArray[14].ToString();
                obj2.estado_salida = row.ItemArray[15].ToString();
                obj2.dx_causa_basica_muerte = row.ItemArray[16].ToString();
                obj2.fecha_egreso = Convert.ToDateTime(row.ItemArray[17].ToString());
                obj2.hora_egreso = row.ItemArray[18].ToString();
                list.Add(obj2);
            }

            return list;
        }
        public List<RIPS_AH_HISTORICO> GetRipsAhOportunidadById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "management_rips_ah_inoportunos";
            }
            else
            {
                cmd.CommandText = "management_rips_ah_inoportunos";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AH_HISTORICO> list = new List<RIPS_AH_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AH_HISTORICO obj2 = new RIPS_AH_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_usuario = row.ItemArray[2].ToString();
                obj2.num_id_usuario = row.ItemArray[3].ToString();
                obj2.via_ingreso = row.ItemArray[4].ToString();
                obj2.fecha_ingreso = Convert.ToDateTime(row.ItemArray[5].ToString());
                obj2.hora_ingreso = row.ItemArray[6].ToString();
                obj2.num_autorizacion = row.ItemArray[7].ToString();
                obj2.causa_externa = row.ItemArray[8].ToString();
                obj2.dx_ppal_ingreso = row.ItemArray[9].ToString();
                obj2.dx_ppal_egreso = row.ItemArray[10].ToString();
                obj2.dx_rel_1_egreso = row.ItemArray[11].ToString();
                obj2.dx_rel_2_egreso = row.ItemArray[12].ToString();
                obj2.dx_rel_3_egreso = row.ItemArray[13].ToString();
                obj2.dx_complicacion = row.ItemArray[14].ToString();
                obj2.estado_salida = row.ItemArray[15].ToString();
                obj2.dx_causa_basica_muerte = row.ItemArray[16].ToString();
                obj2.fecha_egreso = Convert.ToDateTime(row.ItemArray[17].ToString());
                obj2.hora_egreso = row.ItemArray[18].ToString();
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AN_HISTORICO> GetRipsAnHistoricoById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "ManagmentRipsCorrectosAN";
            }
            else
            {
                cmd.CommandText = "ManagmentRipsIncorrectosAN";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AN_HISTORICO> list = new List<RIPS_AN_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AN_HISTORICO obj2 = new RIPS_AN_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_madre = row.ItemArray[2].ToString();
                obj2.num_id_madre = row.ItemArray[3].ToString();
                obj2.fecha_nacimiento_rn = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.hora_nacimiento = row.ItemArray[5].ToString();
                obj2.edad_gestacional = row.ItemArray[6].ToString();
                obj2.control_prenatal = row.ItemArray[7].ToString();
                obj2.sexo = row.ItemArray[8].ToString();
                obj2.peso = row.ItemArray[9].ToString();
                obj2.dx_recien_nacido = row.ItemArray[10].ToString();
                obj2.causa_muerte = row.ItemArray[11].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[12].ToString()))
                {
                    obj2.fecha_muerte = Convert.ToDateTime(row.ItemArray[12].ToString());
                }

                if (!string.IsNullOrEmpty(row.ItemArray[13].ToString()))
                {
                    obj2.hora_muerte = row.ItemArray[13].ToString();
                }
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AN_HISTORICO> GetRipsAnOportunidadById(int id, int modo)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd = conn.CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = "set arithabort on;";
            cmd.ExecuteNonQuery();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@IdRips", SqlDbType.Int);
            param[0].Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            if (modo == 1)
            {
                cmd.CommandText = "management_rips_an_oportunos";
            }
            else
            {
                cmd.CommandText = "management_rips_an_inoportunos";
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 3000;
            cmd.Parameters.AddRange(param);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AN_HISTORICO> list = new List<RIPS_AN_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AN_HISTORICO obj2 = new RIPS_AN_HISTORICO();

                obj2.num_factura = row.ItemArray[0].ToString();
                obj2.cod_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_madre = row.ItemArray[2].ToString();
                obj2.num_id_madre = row.ItemArray[3].ToString();
                obj2.fecha_nacimiento_rn = Convert.ToDateTime(row.ItemArray[4].ToString());
                obj2.hora_nacimiento = row.ItemArray[5].ToString();
                obj2.edad_gestacional = row.ItemArray[6].ToString();
                obj2.control_prenatal = row.ItemArray[7].ToString();
                obj2.sexo = row.ItemArray[8].ToString();
                obj2.peso = row.ItemArray[9].ToString();
                obj2.dx_recien_nacido = row.ItemArray[10].ToString();
                obj2.causa_muerte = row.ItemArray[11].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[12].ToString()))
                {
                    obj2.fecha_muerte = Convert.ToDateTime(row.ItemArray[12].ToString());
                }

                if (!string.IsNullOrEmpty(row.ItemArray[13].ToString()))
                {
                    obj2.hora_muerte = row.ItemArray[13].ToString();
                }
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_AF_HISTORICO> GetRipsAfHistoricoById(int id)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand("Select * from [RIPS_AF_HISTORICO] where id_rips=@id", conn);
            command.Parameters.AddWithValue("@id", id);
            command.CommandTimeout = 3000;
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_AF_HISTORICO> list = new List<RIPS_AF_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_AF_HISTORICO obj2 = new RIPS_AF_HISTORICO();
                obj2.codigo_prestador = row.ItemArray[0].ToString();
                obj2.nombre_prestador = row.ItemArray[1].ToString();
                obj2.tipo_id_prestador = row.ItemArray[2].ToString();
                obj2.num_id_prestador = row.ItemArray[3].ToString();
                obj2.num_factura = row.ItemArray[4].ToString();
                obj2.fecha_exp_factura = Convert.ToDateTime(row.ItemArray[5].ToString());
                obj2.fecha_inicio = Convert.ToDateTime(row.ItemArray[6].ToString());
                obj2.fecha_final = Convert.ToDateTime(row.ItemArray[7].ToString());
                obj2.cod_entidad_adm = row.ItemArray[8].ToString();
                obj2.nom_entidad_adm = row.ItemArray[9].ToString();
                obj2.num_contrato = row.ItemArray[10].ToString();
                obj2.plan_beneficios = row.ItemArray[11].ToString();
                obj2.num_poliza = row.ItemArray[12].ToString();
                if (!string.IsNullOrEmpty(row.ItemArray[13].ToString()))
                    obj2.copago = Convert.ToDecimal(row.ItemArray[13].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[14].ToString()))
                    obj2.valor_comision = Convert.ToDecimal(row.ItemArray[14].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[15].ToString()))
                    obj2.valor_descuentos = Convert.ToDecimal(row.ItemArray[15].ToString());
                if (!string.IsNullOrEmpty(row.ItemArray[16].ToString()))
                    obj2.valor_neto = Convert.ToDecimal(row.ItemArray[16].ToString());
                list.Add(obj2);
            }

            return list;
        }

        public List<RIPS_US_HISTORICO> GetRipsUsHistoricoById(int id)
        {
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand("Select * from [RIPS_US_HISTORICO] where id_rips=@id", conn);
            command.Parameters.AddWithValue("@id", id);
            command.CommandTimeout = 3000;
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            List<RIPS_US_HISTORICO> list = new List<RIPS_US_HISTORICO>();
            foreach (DataRow row in dt.Rows)
            {
                RIPS_US_HISTORICO obj2 = new RIPS_US_HISTORICO();
                obj2.tipo_id_usuario = row.ItemArray[0].ToString();
                obj2.num_id_usuario = row.ItemArray[1].ToString();
                obj2.cod_entidad_adm = row.ItemArray[2].ToString();
                obj2.tipo_usuario = row.ItemArray[3].ToString();
                obj2.primer_apellido = row.ItemArray[4].ToString();
                obj2.segundo_apellido = row.ItemArray[5].ToString();
                obj2.primer_nombre = row.ItemArray[6].ToString();
                obj2.segundo_nombre = row.ItemArray[7].ToString();
                obj2.edad = row.ItemArray[8].ToString();
                obj2.unidad_medida_edad = row.ItemArray[9].ToString();
                obj2.sexo = row.ItemArray[10].ToString();
                obj2.cod_departamento_residencia = row.ItemArray[11].ToString();
                obj2.cod_municipio_residencia = row.ItemArray[12].ToString();
                obj2.zona_residencia = row.ItemArray[13].ToString();
                list.Add(obj2);
            }
            return list;
        }

        #endregion

        #region PQRS

        public List<ecop_pqrs_a_quien_llamo> aQuienLlamoId(int? idPqr)
        {
            List<ecop_pqrs_a_quien_llamo> lista = new List<ecop_pqrs_a_quien_llamo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ecop_pqrs_a_quien_llamo.Where(x => x.id_ecop_pqr == idPqr).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<Ref_PQRS_Atributo> listaAtributosPqrs()
        {
            List<Ref_PQRS_Atributo> List = new List<Ref_PQRS_Atributo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List = db.Ref_PQRS_Atributo.ToList();
                    return List;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return List;
            }
        }
        public List<vw_ecop_PQRS> ConsultaPQRS()
        {
            List<vw_ecop_PQRS> lstResult = new List<vw_ecop_PQRS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ciudades> TotalCiudades()
        {
            List<Ref_ciudades> lista = new List<Ref_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_ciudades.ToList();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            return lista;
        }


        public List<Ref_ciudades> TotalCiudadesXRegional(int? regional)
        {
            List<Ref_ciudades> lista = new List<Ref_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_ciudades.Where(x => x.id_ref_regional == regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            return lista;
        }

        public Ref_ciudades CiudadesId(int? id)
        {
            Ref_ciudades lista = new Ref_ciudades();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_ciudades.Where(x => x.id_ref_ciudades == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }

            return lista;
        }
        public List<management_pqrs_tableroAuditorResult> ConsultaTableroAuditor(int? filtrado, int? idAuditor)
        {
            List<management_pqrs_tableroAuditorResult> lstResult = new List<management_pqrs_tableroAuditorResult>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    lstResult = db.management_pqrs_tableroAuditor(filtrado, idAuditor).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_ecop_PQRS_enrevision> ConsultaPQRSEnRevision()
        {
            List<vw_ecop_PQRS_enrevision> lstResult = new List<vw_ecop_PQRS_enrevision>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS_enrevision.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_PQRS_usuarios> GetusuariosPQRS()
        {
            List<Ref_PQRS_usuarios> lstResult = new List<Ref_PQRS_usuarios>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_usuarios.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public Ref_PQRS_usuarios GetUsuarioPqrs(string usuario)
        {
            Ref_PQRS_usuarios dato = new Ref_PQRS_usuarios();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.Ref_PQRS_usuarios.Where(x => x.usuario == usuario).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public Ref_PQRS_usuarios GetUsuarioPqrsId(int? idUsuario)
        {
            Ref_PQRS_usuarios dato = new Ref_PQRS_usuarios();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.Ref_PQRS_usuarios.Where(x => x.id_ref_PQRS_usuarios == idUsuario).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }
        public List<ecop_PQRS> GetPQRSId(int id)
        {
            List<ecop_PQRS> lstResult = new List<ecop_PQRS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_PQRS.Where(p => p.id_ecop_PQRS == id).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<ecop_PQRS_enrevision> GetPQRSIdEnrevision(int id)
        {
            List<ecop_PQRS_enrevision> lstResult = new List<ecop_PQRS_enrevision>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_PQRS_enrevision.Where(p => p.id_ecop_PQRA_enrevision == id).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<ecop_PQRS_enrevision> GetIdPQRSEnrevision(int id)
        {
            List<ecop_PQRS_enrevision> lstResult = new List<ecop_PQRS_enrevision>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_PQRS_enrevision.Where(p => p.id_ecop_PQRS == id).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_ecop_PQRS2> ConsultaPQRS2()
        {
            List<vw_ecop_PQRS2> lstResult = new List<vw_ecop_PQRS2>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS2.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_ecop_PQRS> ConsultaPQRSId(Int32 id_ecop_PQRS)
        {
            List<vw_ecop_PQRS> lstResult = new List<vw_ecop_PQRS>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS.Where(p => p.id_ecop_PQRS == id_ecop_PQRS).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_ecop_PQRS2> ConsultaPQRSId2(Int32 id_ecop_PQRS)
        {
            List<vw_ecop_PQRS2> lstResult = new List<vw_ecop_PQRS2>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS2.Where(p => p.id_ecop_PQRS == id_ecop_PQRS).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<management_vw_ecop_PQRS2Result> ConsultaPQRSId3(Int32 id_ecop_PQRS)
        {
            List<management_vw_ecop_PQRS2Result> lstResult = new List<management_vw_ecop_PQRS2Result>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_vw_ecop_PQRS2(id_ecop_PQRS).ToList();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_ecop_PQRS_DetalleG> ConsultaPQRSDetalle(Int32 Id_pqrs)
        {
            List<vw_ecop_PQRS_DetalleG> lstResult = new List<vw_ecop_PQRS_DetalleG>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS_DetalleG.Where(p => p.id_ecop_PQRS == Id_pqrs).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<management_vw_ecop_PQRS_DetalleGResult> ConsultaPQRSDetalle2(Int32 Id_pqrs)
        {
            List<management_vw_ecop_PQRS_DetalleGResult> lstResult = new List<management_vw_ecop_PQRS_DetalleGResult>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_vw_ecop_PQRS_DetalleG(Id_pqrs).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<ecop_pqrs_prestadores> ListadoPrestadoresIdPqrs(int? idPqrs)
        {
            List<ecop_pqrs_prestadores> listado = new List<ecop_pqrs_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.ecop_pqrs_prestadores.Where(x => x.id_pqrs == idPqrs).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;

        }
        public log_pqrs_reinicioConteo_asignacionAnalistas BuscarReinicioConteoPqrs(int? mes, int? año)
        {
            log_pqrs_reinicioConteo_asignacionAnalistas dato = new log_pqrs_reinicioConteo_asignacionAnalistas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.log_pqrs_reinicioConteo_asignacionAnalistas.Where(x => x.mes == mes && x.año == año).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<management_buscarAnalistaPqrsResult> AnalistaPqr(int ciudad, int regional)
        {

            List<management_buscarAnalistaPqrsResult> lista = new List<management_buscarAnalistaPqrsResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_buscarAnalistaPqrs(ciudad, regional).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return lista;
            }
        }

        public management_pqrs_detalleCasoResult DetallePqrsReporteCorreo(int? idPqr)
        {
            management_pqrs_detalleCasoResult dato = new management_pqrs_detalleCasoResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_pqrs_detalleCaso(idPqr).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<sis_usuario> GetListAuditorCiudad(Int32? regional, Int32? ciudad, ref MessageResponseOBJ MsgRes)
        {
            List<sis_usuario> result = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    var lista = from a in db.sis_auditor_regional
                                join b in db.sis_usuario
                                on a.id_auditor equals b.id_usuario
                                where a.id_regional == regional
                                && b.id_estado == 1

                                select b;

                    result = lista.ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }

        }


        public List<management_pqrs_busquedaAudirotesRegionalResult> BusquedaAuditoresPqrs(int? idRegional, int? cargo)
        {
            List<management_pqrs_busquedaAudirotesRegionalResult> lista = new List<management_pqrs_busquedaAudirotesRegionalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_pqrs_busquedaAudirotesRegional(idRegional, cargo).ToList();
                }
            }
            catch (Exception ex)
            {
                var errorr = ex.Message;
            }
            return lista;
        }





        public Int32? Getidauditor(string nombre)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            var user = db.sis_usuario.Where(l => l.nombre == nombre).FirstOrDefault();
            if (user != null)
            {
                return user.id_usuario;
            }
            else
            {
                return 0;
            }

        }

        public List<ecop_PQRS_Auditor> ConsultaPQRSAuditor(Int32 Id_pqrs)
        {
            List<ecop_PQRS_Auditor> lstResult = new List<ecop_PQRS_Auditor>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ecop_PQRS_Auditor.Where(p => p.id_ecop_PQRS == Id_pqrs).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public ecop_PQRS_Auditor TraerRespuestaAuditor(int? idPqrs)
        {
            ecop_PQRS_Auditor audi = new ecop_PQRS_Auditor();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    audi = db.ecop_PQRS_Auditor.Where(x => x.id_ecop_PQRS == idPqrs).OrderByDescending(x => x.id_ecop_PQRS_auditor).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return audi;
        }

        public List<management_pqrs_auditorListaResult> ListaPqrsAuditor(int idPqrs)
        {
            List<management_pqrs_auditorListaResult> list = new List<management_pqrs_auditorListaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrs_auditorLista(idPqrs).ToList();
                    return list;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<vw_ecop_PQRS_correo_direc> ConsultaPQRSCorreo()
        {
            List<vw_ecop_PQRS_correo_direc> lstResult = new List<vw_ecop_PQRS_correo_direc>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ecop_PQRS_correo_direc.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<GestionDocumentalPQRS> GetUrlDocumentosPqrs(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            List<GestionDocumentalPQRS> ListResult = new List<GestionDocumentalPQRS>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.GestionDocumentalPQRS.Where(p => p.id_gestion_documental_pqrs == id).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<GestionDocumentalPQRS2> GetUrlProyeccion(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            List<GestionDocumentalPQRS2> ListResult = new List<GestionDocumentalPQRS2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.GestionDocumentalPQRS2.Where(p => p.id_ecop_pqr == id && p.numero_caso != null && p.id_tipo_documental != 12 && p.id_tipo_documental != 13).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<management_pqrs_mirarArchivosResult> ArchivosPqrs(Int32 idPqr)
        {
            List<management_pqrs_mirarArchivosResult> ListResult = new List<management_pqrs_mirarArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.management_pqrs_mirarArchivos(idPqr).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public GestionDocumentalPQRS2 traerArchivoPqr(int idArchivo)
        {
            GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.GestionDocumentalPQRS2.Where(x => x.id_gestion_documental_pqrs == idArchivo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public GestionDocumentalPQRS2 traerArchivoPqrId(int idPqr)
        {
            GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.GestionDocumentalPQRS2.Where(x => x.id_ecop_pqr == idPqr).OrderByDescending(x => x.id_gestion_documental_pqrs).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public GestionDocumentalPQRS2 ExisteArchivoConceptoAuditor(int idPqr)
        {
            GestionDocumentalPQRS2 dato = new GestionDocumentalPQRS2();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.GestionDocumentalPQRS2.Where(x => x.id_ecop_pqr == idPqr && x.id_tipo_documental == 14).OrderByDescending(x => x.id_gestion_documental_pqrs).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<Ref_PQRS_correo_envio> ConsultaPQRSref_correo()
        {
            List<Ref_PQRS_correo_envio> lstResult = new List<Ref_PQRS_correo_envio>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_correo_envio.ToList();
                    lstResult = lstResult.Where(x => x.estado == "A").ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_PQRS_categorizacion> ConsultaPQRSCategorizacion()
        {
            List<Ref_PQRS_categorizacion> lstResult = new List<Ref_PQRS_categorizacion>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_categorizacion.Where(x => x.estado == 1).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<management_pqrs_tablero_controlResult> GestiontableroPQRS()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<management_pqrs_tablero_controlResult> result = new List<management_pqrs_tablero_controlResult>();
            db.CommandTimeout = 6000;
            result = db.management_pqrs_tablero_control().ToList();
            return result;
        }

        public List<management_pqrs_proyectadasCierreResult> DatosTableroPqrsProyectadasCierre()
        {
            List<management_pqrs_proyectadasCierreResult> lista = new List<management_pqrs_proyectadasCierreResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_pqrs_proyectadasCierre().ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_pqrs_tablero_control_proyectadasFinalesResult> DatosTableroPqrsProyectadasFinales()
        {
            List<management_pqrs_tablero_control_proyectadasFinalesResult> lista = new List<management_pqrs_tablero_control_proyectadasFinalesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_pqrs_tablero_control_proyectadasFinales().ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_pqrs_tablero_control_proyectadasResult> GestiontableroPQRSProyectadas(string numCaso, string numOpc, string numDocumento, DateTime? fechaInicial, DateTime? fechaFinal, int? idPqr)
        {
            List<management_pqrs_tablero_control_proyectadasResult> result = new List<management_pqrs_tablero_control_proyectadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    result = db.management_pqrs_tablero_control_proyectadas(numCaso, numOpc, numDocumento, fechaInicial, fechaFinal, idPqr).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<management_pqrs_TableroseguimientoResult> GestiontableeroSeguimientoPqrs(string usuario, string solucionador, int? tipoBusqueda, int? idUsuario)
        {
            List<management_pqrs_TableroseguimientoResult> result = new List<management_pqrs_TableroseguimientoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    result = db.management_pqrs_Tableroseguimiento(usuario, solucionador, tipoBusqueda, idUsuario).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<ref_solucionador> getSolucionadorTotal()
        {
            List<ref_solucionador> list = new List<ref_solucionador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_solucionador.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_solucionador> getSolucionador(int idCiudad)
        {
            List<ref_solucionador> list = new List<ref_solucionador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_solucionador.Where(x => x.id_ciudad == idCiudad || x.id_ciudad == null).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrsAuditor_reporteResult> ReporteAuditorPqrs(int idPqrs)
        {
            List<management_pqrsAuditor_reporteResult> list = new List<management_pqrsAuditor_reporteResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrsAuditor_reporte(idPqrs).OrderByDescending(x => x.id_ecop_PQRS_auditor).Take(1).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrs_datosReporte_conceptoResult> ReporteConceptoAuditor(int idPqrs)
        {
            List<management_pqrs_datosReporte_conceptoResult> list = new List<management_pqrs_datosReporte_conceptoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrs_datosReporte_concepto(idPqrs).Take(1).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_solucionador> getSolucionadorReg(int idRegional)
        {
            List<ref_solucionador> list = new List<ref_solucionador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_solucionador.Where(x => x.id_regional == idRegional || x.id_regional == 0).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_ref_solucionadorResult> getSolucionadorRegActivos(int idRegional)
        {
            List<management_ref_solucionadorResult> list = new List<management_ref_solucionadorResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_ref_solucionador().Where(x => x.id_regional == idRegional || x.id_regional == 0).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public ref_solucionador getSolucionadorNombre(string nombre, string auxsolucionador)
        {
            ref_solucionador dato = new ref_solucionador();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (!string.IsNullOrEmpty(auxsolucionador) && auxsolucionador != "OTRO")
                    {
                        dato = db.ref_solucionador.Where(x => x.nombre_solucionador == nombre && x.auxiliar_solucionador == auxsolucionador && x.correo_solucionador != "").FirstOrDefault();
                    }
                    else
                    {
                        dato = db.ref_solucionador.Where(x => x.nombre_solucionador == nombre && x.correo_solucionador != "").FirstOrDefault();
                    }

                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }


        public ref_solucionador TraerAuxSolucionador(string nombreAux)
        {
            ref_solucionador dato = new ref_solucionador();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_solucionador.Where(x => x.auxiliar_solucionador == nombreAux && x.auxiliar_solucionador != "OTRO").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<ref_solucionador> getSolucionadorRegional(int? idRegional)
        {
            List<ref_solucionador> dato = new List<ref_solucionador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_solucionador.Where(x => x.id_regional == idRegional).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<Management_PQRS_solucionadoresResult> getSolucionadorAux()
        {
            List<Management_PQRS_solucionadoresResult> list = new List<Management_PQRS_solucionadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Management_PQRS_solucionadores().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrs_observacionesAuditorResult> PqrsListaObservacionesAuditor(int idPqrs)
        {
            List<management_pqrs_observacionesAuditorResult> list = new List<management_pqrs_observacionesAuditorResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrs_observacionesAuditor(idPqrs).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrs_consolidadoMasivoResult> PqrsListaMasivos(int? idUsuario)
        {
            List<management_pqrs_consolidadoMasivoResult> list = new List<management_pqrs_consolidadoMasivoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrs_consolidadoMasivo(idUsuario).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrs_consolidadoMasivo_detalleResult> PqrsListaMasivosDetalle(int? idMasivo, int? idUsuario)
        {
            List<management_pqrs_consolidadoMasivo_detalleResult> list = new List<management_pqrs_consolidadoMasivo_detalleResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_pqrs_consolidadoMasivo_detalle((int)idMasivo, (int)idUsuario).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_pqrs_sinArchivoInicialResult> listadoPqrsInicialSinArchivo(int? idUsuario)
        {
            List<management_pqrs_sinArchivoInicialResult> lista = new List<management_pqrs_sinArchivoInicialResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_pqrs_sinArchivoInicial(idUsuario).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;

        }

        public ecop_PQRS buscarNumeroCasoPqrs(string numero_caso)
        {
            ecop_PQRS dato = new ecop_PQRS();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_PQRS.Where(x => x.numero_caso == numero_caso).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            return dato;
        }


        public List<management_pqrs_PorcentajeAuditoresResult> listadoPQRSAuditorPorcentaje(string auditor)
        {
            List<management_pqrs_PorcentajeAuditoresResult> lista = new List<management_pqrs_PorcentajeAuditoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_pqrs_PorcentajeAuditores(auditor).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_devolverFechaHabil_diasResult DevolverDiasHabiles(DateTime? fecha, int? tipoSolicitud)
        {
            management_devolverFechaHabil_diasResult dato = new management_devolverFechaHabil_diasResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_devolverFechaHabil_dias(fecha, tipoSolicitud).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            return dato;
        }


        #endregion

        #region GESTION DOCUMENTAL

        public List<Ref_gestion_tipo_documental> ConsultaGestionTipoDocumental(Int32 idproceso)
        {
            List<Ref_gestion_tipo_documental> lstResult = new List<Ref_gestion_tipo_documental>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_gestion_tipo_documental.Where(p => p.id_proceso == idproceso).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_consolidado_fac MD_CosolidadofAC(String numero_factura)
        {
            vw_md_consolidado_fac lstResult = new vw_md_consolidado_fac();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_consolidado_fac.Where(p => p.numero_factura == numero_factura).Single();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return lstResult;
        }

        public List<vw_md_consolidado_fac> MD_CosolidadofACDetalle(String numero_factura)
        {
            List<vw_md_consolidado_fac> lstResult = new List<vw_md_consolidado_fac>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_consolidado_fac.Where(p => p.numero_factura == numero_factura).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_md_consolidado_fac> MD_CosolidadofAC2(String factura)
        {
            List<vw_md_consolidado_fac> lstResult = new List<vw_md_consolidado_fac>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_consolidado_fac.Where(p => p.numero_factura == factura).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_gestion_tipo_documental> ConsultaCodigoGD(Ref_gestion_tipo_documental objBusqueda, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_gestion_tipo_documental> LstResult = new List<Ref_gestion_tipo_documental>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    LstResult = db.Ref_gestion_tipo_documental.Where(p => p.id_tipo_documental == objBusqueda.id_tipo_documental).ToList();
                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_g_documental_medicamentos> ConsultaFactura(String FacMedicamentos)
        {
            List<vw_g_documental_medicamentos> lstResult = new List<vw_g_documental_medicamentos>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_g_documental_medicamentos.Where(p => p.numero_factura == FacMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_g_documental_medicamentos> ConsultaDocumento(Decimal DocMedicamentos)
        {
            List<vw_g_documental_medicamentos> lstResult = new List<vw_g_documental_medicamentos>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_g_documental_medicamentos.Where(p => p.numero_documento == DocMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_fac_consolidado> ConsultaFactura2(String FacMedicamentos)
        {
            List<vw_fac_consolidado> lstResult = new List<vw_fac_consolidado>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_fac_consolidado.Where(p => p.numero_factura == FacMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_fac_consolidado> ConsultaProveedor(String FacMedicamentos)
        {
            List<vw_fac_consolidado> lstResult = new List<vw_fac_consolidado>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_fac_consolidado.Where(p => p.numero_factura == FacMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }


        public List<vw_fac_consolidado> ConsultaDocumento2(String DocMedicamentos)
        {
            List<vw_fac_consolidado> lstResult = new List<vw_fac_consolidado>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_fac_consolidado.Where(p => p.numero_documento == DocMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_fac_consolidado> ConsultaFormula(String ForMedicamentos)
        {
            List<vw_fac_consolidado> lstResult = new List<vw_fac_consolidado>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_fac_consolidado.Where(p => p.numero_factura == ForMedicamentos).ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }


        public vw_g_documental_medicamentos ConsultaIdGestionDocumental(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            vw_g_documental_medicamentos LstResult = new vw_g_documental_medicamentos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    LstResult = db.vw_g_documental_medicamentos.Where(p => p.id_gestion_documental__medicamentos == id_gestion_documental).Single();
                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumental2(Int32 id_gestion_documental, ref MessageResponseOBJ MsgRes)
        {
            List<vw_g_documental_medicamentos> LstResult = new List<vw_g_documental_medicamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_g_documental_medicamentos.Where(p => p.id_gestion_documental__medicamentos == id_gestion_documental).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_g_documental_medicamentos> ConsultaIdGestionDocumentalFormula(String formula, ref MessageResponseOBJ MsgRes)
        {
            List<vw_g_documental_medicamentos> LstResult = new List<vw_g_documental_medicamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_g_documental_medicamentos.Where(p => p.numero_formula == formula).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_gestion_documental_med_cad_dtll> ConsultaIdGestionDocumentalMDCalidad(Int32 id_indicadores_medicamentos, ref MessageResponseOBJ MsgRes)
        {
            List<vw_gestion_documental_med_cad_dtll> LstResult = new List<vw_gestion_documental_med_cad_dtll>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_gestion_documental_med_cad_dtll.Where(p => p.id_indicadores_medicamentos == id_indicadores_medicamentos).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public ecop_PQRS ConsultaPQRSbyNumCaso(string numcaso)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                ecop_PQRS ecop = db.ecop_PQRS.Where(l => l.numero_caso == numcaso).FirstOrDefault();
                return ecop;
            }
        }

        public ecop_PQRS LlamarPqrsById(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    return db.ecop_PQRS.Where(x => x.id_ecop_PQRS == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        public ecop_pqrs_envioCorreos LlamarPqrsCorreoById(int id)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    return db.ecop_pqrs_envioCorreos.Where(x => x.id_pqrs == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        public List<ecop_PQRS> ListadoPqrsMasivo(int idMasivo)
        {
            List<ecop_PQRS> listado = new List<ecop_PQRS>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.ecop_PQRS.Where(x => x.id_masivo == idMasivo).ToList();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return listado;
            }
        }

        public GestionDocumentalPQRS ConsultaGestorPQRSbyId(Int32 Id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                GestionDocumentalPQRS ecop = db.GestionDocumentalPQRS.Where(l => l.id_gestion_documental_pqrs == Id).FirstOrDefault();
                return ecop;
            }
        }

        public List<GestionDocumentalPQRS> ConsultanumcasoGestionDocumentalPQRS(string numcaso)
        {
            List<GestionDocumentalPQRS> list = new List<GestionDocumentalPQRS>();
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            list = db.GestionDocumentalPQRS.Where(l => l.numero_caso == numcaso).ToList();
            return list;

        }


        public List<ManagmentRefPuntosDispersacionResult> ConsultaListaPD(int opc, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentRefPuntosDispersacionResult> LstResult = new List<ManagmentRefPuntosDispersacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    LstResult = db.ManagmentRefPuntosDispersacion(opc).ToList();
                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<Managment_md_Ref_crono_auditoresResult> ConsultaListaCronoAuditores(int opc1, Int32? opc2, ref MessageResponseOBJ MsgRes)
        {
            List<Managment_md_Ref_crono_auditoresResult> LstResult = new List<Managment_md_Ref_crono_auditoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    LstResult = db.Managment_md_Ref_crono_auditores(opc1, opc2).ToList();
                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public vw_gestion_documental_med_cad_dtll ConsultaIdGestionDocumentalMDCalId(Int32 id_gestion_documental__medicamentos_cad, ref MessageResponseOBJ MsgRes)
        {
            vw_gestion_documental_med_cad_dtll lstResult = new vw_gestion_documental_med_cad_dtll();


            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_gestion_documental_med_cad_dtll.Where(p => p.id_gestion_documental__medicamentos_cad == id_gestion_documental__medicamentos_cad).Single();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return lstResult;
        }


        #endregion

        #region Gestion Documental

        public List<Ref_procesos> ConsultaProcesos(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_procesos> lstResult = new List<Ref_procesos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.Ref_procesos.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Ref_gestion_tipo_documental> ConsultaGestionTipoDocumental(Int32 idproceso, ref MessageResponseOBJ MsgRes)
        {
            List<Ref_gestion_tipo_documental> lstResult = new List<Ref_gestion_tipo_documental>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.Ref_gestion_tipo_documental.Where(l => l.id_proceso == idproceso).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<GestionDocumentalMedicamentos> ConsultaGestionMedCargue()
        {
            List<GestionDocumentalMedicamentos> lstResult = new List<GestionDocumentalMedicamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.GestionDocumentalMedicamentos.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_g_documental_medicamentos_masivo> GestionDocumentalmasivo()
        {
            List<vw_g_documental_medicamentos_masivo> lstResult = new List<vw_g_documental_medicamentos_masivo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_g_documental_medicamentos_masivo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<management_masivo_pqrsResult> GestionDocumentalmasivo2()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<management_masivo_pqrsResult> result = new List<management_masivo_pqrsResult>();
            result = db.management_masivo_pqrs().ToList();
            return result;
        }


        #endregion

        #region PROCESOS INTERNOS

        public List<indicador_regional> ConsultarIndicadorRegionalList(ref MessageResponseOBJ MsgRes)
        {
            List<indicador_regional> lstResult = new List<indicador_regional>();
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                lstResult = db.indicador_regional.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_visitas> ConsultaCronogramaVisitas(Int32? idcronograma, ref MessageResponseOBJ MsgRes)
        {
            List<vw_visitas> lstResult = new List<vw_visitas>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                if (idcronograma == null)
                {
                    lstResult = db.vw_visitas.ToList();
                }
                else
                {
                    lstResult = db.vw_visitas.Where(l => l.id_cronograma_visitas == idcronograma.Value).ToList();
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Management_Consulta_Cronograma_VisitasResult> ConsultaCronogramaVisitasProcedimiento(int tipoFiltro, string Auditor)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_Consulta_Cronograma_Visitas(tipoFiltro, Auditor).ToList();
            }
        }

        public List<cronograma_visita_detalle> ConsultaCronogramaVisitaDetalle(int idcronograma)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.cronograma_visita_detalle.Where(l => l.id_cronograma_visita == idcronograma).ToList();

        }

        public List<cronograma_visita_detalle_indicador> ConsultaCronogramaVisitaDetalleInd(int idcronograma)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<cronograma_visita_detalle_indicador> dato = new List<cronograma_visita_detalle_indicador>();
            try
            {
                dato = db.cronograma_visita_detalle_indicador.Where(l => l.id_cronograma_visitas == idcronograma).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public cronograma_visitas getvisitabyid(Int32 idvisita, ref MessageResponseOBJ MsgRta)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            return db.cronograma_visitas.Where(l => l.id_cronograma_visitas == idvisita).FirstOrDefault();

        }

        public List<indicadores> ConsultarIndicadores(int? idindicador, ref MessageResponseOBJ MsgRes)
        {
            List<indicadores> lstResult = new List<indicadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (idindicador != null)
                    {
                        if (idindicador != 13)
                        {
                            lstResult = db.indicadores.Where(l => l.id_indicador == idindicador.Value).ToList();
                        }
                        else
                        {
                            lstResult = db.indicadores.ToList();
                        }

                    }
                    else
                    {
                        lstResult = db.indicadores.ToList();
                    }
                }
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public item_capitulo Getitemcapbyid(Int32 IdItem)
        {
            item_capitulo obj = new item_capitulo();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    obj = db.item_capitulo.Where(l => l.id_item == IdItem).FirstOrDefault();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return obj;
            }

        }

        public List<capitulos> GetCapitulosList()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.capitulos.ToList();
            }
        }

        public List<capitulo_indicador> GetCapitulosByIndicador(Int32? idindicador, Int32 idregioanl, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext bd = new ECOPETROL_DataContexDataContext();
            List<capitulo_indicador> lstResult = new List<capitulo_indicador>();
            try
            {
                lstResult = bd.capitulo_indicador.Where(l => l.regional_id == idregioanl).ToList();
                if (idindicador != null)
                {
                    lstResult = lstResult.Where(l => l.indicador_id == idindicador).ToList();
                }
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ManagementCalidadDtllIndicadorResult> GetCapitulosEvaluadosByIndicador(Int32? idindicador, Int32 idregioanl, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext bd = new ECOPETROL_DataContexDataContext();
            List<ManagementCalidadDtllIndicadorResult> lstResult = new List<ManagementCalidadDtllIndicadorResult>();
            try
            {
                lstResult = bd.ManagementCalidadDtllIndicador(idindicador, idregioanl).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public capitulo_indicador getcapbyindicadorcap(int idcapitulo, int idindicador, int idregional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            return db.capitulo_indicador.Where(l => l.capitulo_id == idcapitulo && l.indicador_id == idindicador && l.regional_id == idregional).FirstOrDefault();
        }

        public List<item_capitulo> Getitemcapbyindcap(Int32 idregional, Int32 idindicador, Int32? idcap)
        {
            List<item_capitulo> lstResult = new List<item_capitulo>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                lstResult = db.item_capitulo.Where(l => l.regional_id == idregional && l.indicador_id == idindicador).ToList();
                if (idcap != null)
                {
                    lstResult = lstResult.Where(l => l.capitulo_id == idcap).ToList();
                }
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<cronograma_visita_detalle> Getdetalllescronograma(int idcronograma)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.cronograma_visita_detalle.Where(l => l.id_cronograma_visita == idcronograma).ToList();
        }


        public cronograma_visita_informeOperativo TraerArchivoInformeOperativo(int? idArchivo)
        {
            cronograma_visita_informeOperativo dato = new cronograma_visita_informeOperativo();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cronograma_visita_informeOperativo.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public capitulos Getcapitulobyid(Int32 idcapitulo)
        {
            capitulos obj = new capitulos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    obj = db.capitulos.Where(l => l.id_capitulo == idcapitulo).FirstOrDefault();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return obj;
            }
        }

        public Ref_RIPS_Prestadores getPrestador(double codprestador, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            Ref_RIPS_Prestadores prestador = db.Ref_RIPS_Prestadores.Where(l => l.codigo_habilitacion == codprestador.ToString()).FirstOrDefault();
            return prestador;
        }

        public List<ref_usuario_responsable> ConsultResponsablesbyusuario(Int32 idusuario, ref MessageResponseOBJ MsgRes)
        {
            List<ref_usuario_responsable> result = new List<ref_usuario_responsable>();

            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.ref_usuario_responsable.Where(i => i.usuario_id == idusuario).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return result;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<sis_usuario> LstResponsables()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                var usuarios = from a in db.sis_auditor_regional
                               join b in db.sis_usuario on a.id_auditor equals b.id_usuario
                               select b;

                return usuarios.Distinct().ToList();

            }
        }

        public List<calidad_ref_especialidad> GetRefEspecialidadList()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.calidad_ref_especialidad.ToList();
            }
        }


        public List<calidad_prestadores> getprestadoresList()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_prestadores.ToList();
        }

        public calidad_prestadores getPresadorById(int idprestador)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_prestadores.Where(l => l.id_prestador == idprestador).FirstOrDefault();
        }

        public List<calidad_ref_regimen> GetRefRegimentList()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.calidad_ref_regimen.ToList();
            }
        }


        public List<Ref_clase_persona> GetClasePersonaList()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_clase_persona.ToList();
            }
        }

        public List<vw_calidad_prestador_indicador> GetListIndicadoresPrestador(int id_prestador)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_calidad_prestador_indicador.Where(l => l.id_prestador == id_prestador).ToList();
            }
        }

        public List<Ref_calidad_municipios> GetMunicipiosDane()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var lista = from item in db.Ref_calidad_municipios
                        select item;
            return lista.ToList();
        }

        public List<vw_visitas> GetListVisitas(Int32? id_visita, Int32? id_prestador, string numcontrato)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<vw_visitas> listvisitas = new List<vw_visitas>();
            if (id_visita != null)
            {
                listvisitas = db.vw_visitas.Where(l => l.id_cronograma_visitas == id_visita).ToList();
            }

            if (id_prestador != null)
            {
                listvisitas = db.vw_visitas.Where(l => l.id_prestador == id_prestador).ToList();
            }

            if (!string.IsNullOrEmpty(numcontrato))
            {
                listvisitas = db.vw_visitas.Where(l => l.num_contrato == numcontrato).ToList();
            }
            return listvisitas;
        }



        public List<ref_cronograma_visitas_novedades> GetListTipoNovedad()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_cronograma_visitas_novedades.ToList();
        }

        public cronograma_visita_documento Getactavisita(int idvisita)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var Documento = db.cronograma_visita_documento.Where(l => l.id_cronograma_visita == idvisita).FirstOrDefault();

            if (Documento != null)
            {
                return Documento;
            }
            else
            {
                return null;
            }
        }

        public management_cronograma_visita_documento_idResult Getactavisita2(int idvisita)
        {
            management_cronograma_visita_documento_idResult dato = new management_cronograma_visita_documento_idResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_cronograma_visita_documento_id(idvisita).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_cronograma_visita_documento_sinRutaResult> GetactavisitaSinRuta()
        {
            List<management_cronograma_visita_documento_sinRutaResult> dato = new List<management_cronograma_visita_documento_sinRutaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_cronograma_visita_documento_sinRuta().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }
        //public cronograma_visita_documentos Getactavisitas(int idvisita)
        //{
        //    ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
        //    var documentoDocs = db.cronograma_visita_documentos.Where(l => l.id_cronograma_visita == idvisita).FirstOrDefault();
        //    if (documentoDocs != null)
        //    {
        //        return documentoDocs;
        //    }
        //    else
        //    {

        //        return null;

        //    }
        //}


        public List<vw_visitas_documentos> GetActasVisitas(int regional, int mes, int año)
        {
            List<vw_visitas_documentos> listado = new List<vw_visitas_documentos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.vw_visitas_documentos.Where(l => l.mes_fecha_final_visita == mes && l.año_fecha_final_visita == año && l.id_regional == regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return listado;
        }

        public List<ManagementConsultaGnralVisitasResult> GetConsultageneralVisitas(int regional, int prestador, DateTime fecha, string nit, string codsap)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<ManagementConsultaGnralVisitasResult> lista = db.ManagementConsultaGnralVisitas(prestador, regional, fecha).ToList();
            if (!string.IsNullOrEmpty(nit))
            {
                lista = lista.Where(l => l.no_id_prestador == nit).ToList();
            }
            if (!string.IsNullOrEmpty(codsap))
            {
                lista = lista.Where(l => l.cod_sap == codsap).ToList();
            }

            return lista;
        }

        public cronograma_visita_detalle Getresultadovisitaindicador(int idvisita, int idindicador)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.cronograma_visita_detalle.Where(l => l.id_cronograma_visita == idvisita && l.id_item == idindicador).FirstOrDefault();
        }

        public List<cronograma_visitas_calificaciones> GetCalificacionesVisita(int id_cronograma)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.cronograma_visitas_calificaciones.Where(l => l.cronograma_visita_id == id_cronograma).ToList();

        }

        public Management_get_info_visita_por_idResult GetInfoVisitaById(int idCronograma)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_get_info_visita_por_id(idCronograma).FirstOrDefault();
        }

        #endregion

        #region MEDICAMENTOS

        public List<ManagmentFacMedicamentosResult> CuentaFacMedicamentos(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentFacMedicamentosResult> lstResult = new List<ManagmentFacMedicamentosResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentFacMedicamentos(factura, formula, OPC).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Managment_md_tablero_ConciliacionesResult> CuentaFacTableroControlConciliaciones(ref MessageResponseOBJ MsgRes)
        {
            List<Managment_md_tablero_ConciliacionesResult> lstResult = new List<Managment_md_tablero_ConciliacionesResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Managment_md_tablero_Conciliaciones().ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Managment_md_tablero_Conciliaciones_detalleResult> CuentaFacTableroControlConciliacionesdtll(ref MessageResponseOBJ MsgRes)
        {
            List<Managment_md_tablero_Conciliaciones_detalleResult> lstResult = new List<Managment_md_tablero_Conciliaciones_detalleResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Managment_md_tablero_Conciliaciones_detalle().ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public vw_md_glosa_conciliacion ConsultaGlosaDtllId(Int32 id_md_glosa_detalle)
        {
            vw_md_glosa_conciliacion lstResult = new vw_md_glosa_conciliacion();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_glosa_conciliacion.Where(p => p.Id == id_md_glosa_detalle).FirstOrDefault();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Managment_md_tablerocontrolResult> CuentaFacTableroControl(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<Managment_md_tablerocontrolResult> lstResult = new List<Managment_md_tablerocontrolResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Managment_md_tablerocontrol(fecha_inicial, fecha_final).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ManagmentocargueconsolidadoResult> CuentaConsolidadoMD(String numero_factura, String numero_formula, DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentocargueconsolidadoResult> lstResult = new List<ManagmentocargueconsolidadoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Managmentocargueconsolidado(numero_factura, numero_formula, fecha_inicial, fecha_final).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<ManagmentFacMedicamentosDetalleResult> CuentaFacMedicamentosDetalle(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentFacMedicamentosDetalleResult> lstResult = new List<ManagmentFacMedicamentosDetalleResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ManagmentFacMedicamentosDetalle(factura, formula, OPC).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<vw_glosa_medicamentos> ConsultaGlosa(String formula)
        {
            List<vw_glosa_medicamentos> lstResult = new List<vw_glosa_medicamentos>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                lstResult = db.vw_glosa_medicamentos.Where(l => l.numero_formula == formula).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<md_indicadores_detalle> GetIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            List<md_indicadores_detalle> lstResult = new List<md_indicadores_detalle>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                lstResult = db.md_indicadores_detalle.Where(l => l.id_indicadores_medicamentos == id_indicadores_medicamentos).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<md_indicadores_detalle> GetIndicadoresDetalleID(Int32 id_md_ref_indicador, Int32 id_indicadores_medicamentos)
        {
            List<md_indicadores_detalle> ListResult = new List<md_indicadores_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_indicadores_detalle.Where(p => p.id_md_ref_indicador == id_md_ref_indicador && p.id_indicadores_medicamentos == id_indicadores_medicamentos).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_indicador_detalle_sin_cumplir> GetIndicadoresSinCumplir(Int32 id_indicadores_medicamentos)
        {
            List<vw_indicador_detalle_sin_cumplir> lstResult = new List<vw_indicador_detalle_sin_cumplir>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                lstResult = db.vw_indicador_detalle_sin_cumplir.Where(l => l.id_indicadores_medicamentos == id_indicadores_medicamentos).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<Managment_md_Ref_indicadorResult> DetalleRefIndicadores(Int32 id_indicadores_medicamentos, Int32 opc)
        {
            List<Managment_md_Ref_indicadorResult> ListResult = new List<Managment_md_Ref_indicadorResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_indicador(opc, id_indicadores_medicamentos).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<ManagmentReporIndicadorMDResult> ReporteIndicadores(Int32 id_indicadores_medicamentos)
        {
            List<ManagmentReporIndicadorMDResult> ListResult = new List<ManagmentReporIndicadorMDResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ManagmentReporIndicadorMD(id_indicadores_medicamentos).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public vw_total_md_detalle Total_DetalleIndMD(Int32 id_indicadores_medicamentos)
        {
            vw_total_md_detalle ListResult = new vw_total_md_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_total_md_detalle.Where(l => l.id_indicadores_medicamentos == id_indicadores_medicamentos).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_table_gestion_MD> ConsultaGestionMd()
        {
            List<vw_table_gestion_MD> lstResult = new List<vw_table_gestion_MD>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_table_gestion_MD.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_Ref_tipo_hallazgo> TipoHallazgo()
        {
            List<md_Ref_tipo_hallazgo> lstResult = new List<md_Ref_tipo_hallazgo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_tipo_hallazgo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Managment_md_ConsultasResult> CuentaConsultasMedicamentos(Int32 opc, DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            List<Managment_md_ConsultasResult> lstResult = new List<Managment_md_ConsultasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Managment_md_Consultas(opc, fecha_inicial, fecha_final).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public IEnumerable<vw_md_Ref_indicador_datalle> GetVwIndicadoresDetalle(Int32 id_indicadores_medicamentos)
        {
            List<vw_md_Ref_indicador_datalle> lstResult = new List<vw_md_Ref_indicador_datalle>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                lstResult = db.vw_md_Ref_indicador_datalle.Where(l => l.id_indicadores_medicamentos == id_indicadores_medicamentos).ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return lstResult;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<md_ref_puntos_dispensacion> PuntosDispercion()
        {
            List<md_ref_puntos_dispensacion> lstResult = new List<md_ref_puntos_dispensacion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_puntos_dispensacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_indicadores_medicamentos> IndicadoresProvvedor(String Proveeedor)
        {
            List<vw_indicadores_medicamentos> lstResult = new List<vw_indicadores_medicamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_indicadores_medicamentos.Where(l => l.nombre_auditado == Proveeedor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Managment_md_Ref_obligacionesResult> DetalleRefObligaciones(Int32 id_obligaciones_contractuales, Int32 opc)
        {
            List<Managment_md_Ref_obligacionesResult> ListResult = new List<Managment_md_Ref_obligacionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_obligaciones(opc, id_obligaciones_contractuales).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public vw_total_md_obligaciones_detalle Total_DetalleObligacionesMD(Int32 id_obligaciones_contractuales)
        {
            vw_total_md_obligaciones_detalle ListResult = new vw_total_md_obligaciones_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_total_md_obligaciones_detalle.Where(l => l.id_obligaciones_contractuales == id_obligaciones_contractuales).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_obligaciones_contractuales_detalle> GetObligacionesDetalleID(Int32 id_md_ref_obligaciones, Int32 id_obligaciones_contractuales)
        {
            List<md_obligaciones_contractuales_detalle> ListResult = new List<md_obligaciones_contractuales_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_obligaciones_contractuales_detalle.Where(p => p.id_md_ref_obligaciones == id_md_ref_obligaciones && p.id_obligaciones_contractuales == id_obligaciones_contractuales).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_obligaciones_contractuales> ObligacionesProveedor(String Proveedor)
        {
            List<vw_obligaciones_contractuales> lstResult = new List<vw_obligaciones_contractuales>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_obligaciones_contractuales.Where(l => l.nombre_auditado == Proveedor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_herramientas_tecnologicas> IndicadoresProvvedorHerramientas(Int32 Proveeedor)
        {
            List<vw_herramientas_tecnologicas> lstResult = new List<vw_herramientas_tecnologicas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_herramientas_tecnologicas.Where(l => l.nombre_auditado == Proveeedor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_tabla1> ref_tabla1()
        {
            List<md_ref_tabla1> lstResult = new List<md_ref_tabla1>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_tabla1.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_crono> ConsultaCronograma()
        {
            List<vw_md_crono> lstResult = new List<vw_md_crono>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_crono.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_md_crono_auditores> GetUsuarioCronoId(String usuario, ref MessageResponseOBJ MsgRes)
        {
            List<vw_md_crono_auditores> ListResult = new List<vw_md_crono_auditores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_crono_auditores.Where(p => p.usuario_auditor == usuario).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<Managment_md_Ref_General1Result> DetalleRefInterventoriaGeneral1(Int32 id_md_interventoria_general, Int32 opc)
        {
            List<Managment_md_Ref_General1Result> ListResult = new List<Managment_md_Ref_General1Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_General1(opc, id_md_interventoria_general).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<Managment_md_Ref_General2Result> DetalleRefInterventoriaGeneral2(Int32 id_md_interventoria_general, Int32 opc)
        {
            List<Managment_md_Ref_General2Result> ListResult = new List<Managment_md_Ref_General2Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_General2(opc, id_md_interventoria_general).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }
        public List<Managment_md_Ref_General3Result> DetalleRefInterventoriaGeneral3(Int32 id_md_interventoria_general, Int32 opc)
        {
            List<Managment_md_Ref_General3Result> ListResult = new List<Managment_md_Ref_General3Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_General3(opc, id_md_interventoria_general).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }
        public List<Managment_md_Ref_General4Result> DetalleRefInterventoriaGeneral4(Int32 id_md_interventoria_general, Int32 opc)
        {
            List<Managment_md_Ref_General4Result> ListResult = new List<Managment_md_Ref_General4Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_General4(opc, id_md_interventoria_general).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public vw_total_md_interventoria_detalle Total_DetalleInterventoriaGe(Int32 id_md_interventoria_general)
        {
            vw_total_md_interventoria_detalle ListResult = new vw_total_md_interventoria_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_total_md_interventoria_detalle.Where(l => l.id_md_interventoria_general == id_md_interventoria_general).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<md_ref_tabla2> ref_tabla2()
        {
            List<md_ref_tabla2> lstResult = new List<md_ref_tabla2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_tabla2.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_tabla3> ref_tabla3()
        {
            List<md_ref_tabla3> lstResult = new List<md_ref_tabla3>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_tabla3.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_tabla4> ref_tabla4()
        {
            List<md_ref_tabla4> lstResult = new List<md_ref_tabla4>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_tabla4.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_tabla1_categ> Tabla1Catg()
        {
            List<vw_tabla1_categ> lstResult = new List<vw_tabla1_categ>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tabla1_categ.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_detalle_T1> Tabla1Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            List<vw_md_detalle_T1> lstResult = new List<vw_md_detalle_T1>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_detalle_T1.Where(x => x.id_categoria == id_cat && x.id_herramienta_tec_med == id_herramienta_tec).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_detalle_T2> Tabla2Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            List<vw_md_detalle_T2> lstResult = new List<vw_md_detalle_T2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_detalle_T2.Where(x => x.id_categoria == id_cat && x.id_herramienta_tec_med == id_herramienta_tec).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_detalle_T3> Tabla3Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            List<vw_md_detalle_T3> lstResult = new List<vw_md_detalle_T3>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_detalle_T3.Where(x => x.id_categoria == id_cat && x.id_herramienta_tec_med == id_herramienta_tec).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_detalle_T4> Tabla4Detalle(Int32 id_cat, Int32 id_herramienta_tec)
        {
            List<vw_md_detalle_T4> lstResult = new List<vw_md_detalle_T4>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_detalle_T4.Where(x => x.id_herramienta_tec_med == id_herramienta_tec).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_total_md_interventoria_detalle Total_DetalleInterventoriaGeneralMD(Int32 id_md_interventoria_general)
        {
            vw_total_md_interventoria_detalle ListResult = new vw_total_md_interventoria_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_total_md_interventoria_detalle.Where(l => l.id_md_interventoria_general == id_md_interventoria_general).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_interventoria_general_detalle1> GetInterventoriaDetalle1ID(Int32 id_md_ref_inte_general1, Int32 id_md_interventoria_general)
        {
            List<md_interventoria_general_detalle1> ListResult = new List<md_interventoria_general_detalle1>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_interventoria_general_detalle1.Where(p => p.id_md_ref_inte_general1 == id_md_ref_inte_general1 && p.id_md_interventoria_general == id_md_interventoria_general).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<md_interventoria_general_detalle2> GetInterventoriaDetalle2ID(Int32 id_md_ref_inte_general2, Int32 id_md_interventoria_general)
        {
            List<md_interventoria_general_detalle2> ListResult = new List<md_interventoria_general_detalle2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_interventoria_general_detalle2.Where(p => p.id_md_ref_inte_general2 == id_md_ref_inte_general2 && p.id_md_interventoria_general == id_md_interventoria_general).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<md_interventoria_general_detalle3> GetInterventoriaDetalle3ID(Int32 id_md_ref_inte_general3, Int32 id_md_interventoria_general)
        {
            List<md_interventoria_general_detalle3> ListResult = new List<md_interventoria_general_detalle3>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_interventoria_general_detalle3.Where(p => p.id_md_ref_inte_general3 == id_md_ref_inte_general3 && p.id_md_interventoria_general == id_md_interventoria_general).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }


        public List<md_interventoria_general_detalle4> GetInterventoriaDetalle4ID(Int32 id_md_ref_inte_general4, Int32 id_md_interventoria_general)
        {
            List<md_interventoria_general_detalle4> ListResult = new List<md_interventoria_general_detalle4>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_interventoria_general_detalle4.Where(p => p.id_md_ref_inte_general4 == id_md_ref_inte_general4 && p.id_md_interventoria_general == id_md_interventoria_general).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_md_interventoria_general> InterventoriaGeneralProveedor(String Proveedor)
        {
            List<vw_md_interventoria_general> lstResult = new List<vw_md_interventoria_general>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_interventoria_general.Where(l => l.nombre_auditado == Proveedor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Managment_md_Ref_seguimiento_pendientesResult> DetalleRefSeguimientoPendientes(Int32 id_md_seguimiento_pendientes, Int32 opc)
        {
            List<Managment_md_Ref_seguimiento_pendientesResult> ListResult = new List<Managment_md_Ref_seguimiento_pendientesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_seguimiento_pendientes(opc, id_md_seguimiento_pendientes).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public vw_total_md_seguimiento_detalle Total_DetalleSeguimientoPendientesMD(Int32 id_md_seguimiento_pendientes)
        {
            vw_total_md_seguimiento_detalle ListResult = new vw_total_md_seguimiento_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_total_md_seguimiento_detalle.Where(l => l.id_md_seguimiento_pendientes == id_md_seguimiento_pendientes).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_seguimiento_pendientes_detalle> GetSeguimientoPendientesDetalleID(Int32 id_md_ref_seguimiento_pendientes, Int32 id_md_seguimiento_pendientes)
        {
            List<md_seguimiento_pendientes_detalle> ListResult = new List<md_seguimiento_pendientes_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_seguimiento_pendientes_detalle.Where(p => p.id_md_ref_seguimiento_pendiente == id_md_ref_seguimiento_pendientes && p.id_md_seguimiento_pendientes == id_md_seguimiento_pendientes).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_md_seguimiento_pendientes> SeguimientoPendientesProveedor(String Proveedor)
        {
            List<vw_md_seguimiento_pendientes> lstResult = new List<vw_md_seguimiento_pendientes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_seguimiento_pendientes.Where(l => l.nombre_auditado == Proveedor).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_gestionDocumentalCad> GestionDocumentalMedCalidad(Int32 id, Int32 id2)
        {
            List<vw_gestionDocumentalCad> lstResult = new List<vw_gestionDocumentalCad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_gestionDocumentalCad.Where(l => l.id_indicadores_medicamentos == id && l.id_md_indicador_detalle == id2).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_total_T1 totalesT1(Int32 id)
        {
            vw_md_total_T1 lstResult = new vw_md_total_T1();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_total_T1.Where(l => l.id_herramienta_tec_med == id).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_total_T2 totalesT2(Int32 id)
        {
            vw_md_total_T2 lstResult = new vw_md_total_T2();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_total_T2.Where(l => l.id_herramienta_tec_med == id).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_total_T3 totalesT3(Int32 id)
        {
            vw_md_total_T3 lstResult = new vw_md_total_T3();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_total_T3.Where(l => l.id_herramienta_tec_med == id).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_total_T4 totalesT4(Int32 id)
        {
            vw_md_total_T4 lstResult = new vw_md_total_T4();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_total_T4.Where(l => l.id_herramienta_tec_med == id).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw__md_herramientaT_sin_cumplir> GetHerramientasSincumplir(Int32 id_herramienta_tec_med, Int32 tabla)
        {
            List<vw__md_herramientaT_sin_cumplir> ListResult = new List<vw__md_herramientaT_sin_cumplir>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw__md_herramientaT_sin_cumplir.Where(p => p.id_herramienta_tec_med == id_herramienta_tec_med && p.tabla == tabla).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<ManagmentReportNotifiAuditoriaResult> ReportNotificacionAudi(Int32 id)
        {
            List<ManagmentReportNotifiAuditoriaResult> ListResult = new List<ManagmentReportNotifiAuditoriaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ManagmentReportNotifiAuditoria(id).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_ref_dispens_ambulatoria> RefDispersacionAmbulatoria()
        {
            List<md_ref_dispens_ambulatoria> lstResult = new List<md_ref_dispens_ambulatoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_dispens_ambulatoria.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_dispens_hospitalaria> RefDispersacionHospitalaria()
        {
            List<md_ref_dispens_hospitalaria> lstResult = new List<md_ref_dispens_hospitalaria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_dispens_hospitalaria.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Managment_md_Ref_ambulatorioResult> DetalleRefAmbulatorio(Int32 id_md_dispensacion_ambulatoria)
        {
            List<Managment_md_Ref_ambulatorioResult> ListResult = new List<Managment_md_Ref_ambulatorioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_ambulatorio(id_md_dispensacion_ambulatoria).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<Managment_md_Ref_hospitalarioResult> DetalleRefHospitalario(Int32 id_md_dispensacion_Hospitalaria)
        {
            List<Managment_md_Ref_hospitalarioResult> ListResult = new List<Managment_md_Ref_hospitalarioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_Ref_hospitalario(id_md_dispensacion_Hospitalaria).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_dispensacion_ambulatoria_detalle> GetAmbulatorioDetalleID(Int32 id_md_ref_dispens_ambulatoria, Int32 id_md_dispensacion_ambulatoria)
        {
            List<md_dispensacion_ambulatoria_detalle> ListResult = new List<md_dispensacion_ambulatoria_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_dispensacion_ambulatoria_detalle.Where(p => p.id_md_ref_dispens_ambulatoria == id_md_ref_dispens_ambulatoria && p.id_md_dispensacion_ambulatoria == id_md_dispensacion_ambulatoria).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<md_dispensacion_hospitalaria_detalle> GetHospitalarioDetalleID(Int32 id_md_ref_dispens_hospitalaria, Int32 id_md_dispensacion_hospitalaria)
        {
            List<md_dispensacion_hospitalaria_detalle> ListResult = new List<md_dispensacion_hospitalaria_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_dispensacion_hospitalaria_detalle.Where(p => p.id_md_ref_dispens_hospitalaria == id_md_ref_dispens_hospitalaria && p.id_md_dispensacion_hospitalaria == id_md_dispensacion_hospitalaria).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_dispensacion_ambulatoria> AmbulatorioProvvedor(String Proveeedor)
        {
            List<vw_dispensacion_ambulatoria> lstResult = new List<vw_dispensacion_ambulatoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_dispensacion_ambulatoria.Where(l => l.auditado == Convert.ToInt32(Proveeedor)).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_dispensacion_hospitalaria> hospitalarioProvvedor(String Proveeedor)
        {
            List<vw_dispensacion_hospitalaria> lstResult = new List<vw_dispensacion_hospitalaria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_dispensacion_hospitalaria.Where(l => l.auditado == Convert.ToInt32(Proveeedor)).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return lstResult;

                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public vw_md_total_ambulatoria Total_DetalleAmbulatoria(Int32 id_md_dispensacion_ambulatoria)
        {
            vw_md_total_ambulatoria ListResult = new vw_md_total_ambulatoria();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_total_ambulatoria.Where(l => l.id_md_dispensacion_ambulatoria == id_md_dispensacion_ambulatoria).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public vw_md_total_hospitalaria Total_DetalleHospitalaria(Int32 id_md_dispensacion_hospitalaria)
        {
            vw_md_total_hospitalaria ListResult = new vw_md_total_hospitalaria();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_total_hospitalaria.Where(l => l.id_md_dispensacion_hospitalaria == id_md_dispensacion_hospitalaria).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public md_control_gastos control_gastosMes(Int32 mes, String año)
        {
            md_control_gastos ListResult = new md_control_gastos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_control_gastos.Where(l => l.mes_ingresado == mes && l.año == año).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public vw_md_control_gasto control_gastosTotal(Int32 mes)
        {
            vw_md_control_gasto ListResult = new vw_md_control_gasto();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_control_gasto.FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<Managment_md_control_gastos_tableroResult> tableroControlGastos(int opc, int año)
        {
            List<Managment_md_control_gastos_tableroResult> ListResult = new List<Managment_md_control_gastos_tableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_control_gastos_tablero(opc, año).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<Managment_md_control_gastos_tablero2Result> tableroControlGastos2(int opc, int año)
        {
            List<Managment_md_control_gastos_tablero2Result> ListResult = new List<Managment_md_control_gastos_tablero2Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.Managment_md_control_gastos_tablero2(opc, año).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<md_prefacturas_cargue_base> GetPrefacturasList()
        {
            List<md_prefacturas_cargue_base> lista = new List<md_prefacturas_cargue_base>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.md_prefacturas_cargue_base.ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_validadorPrefacturasResult> GetPrefacturasListado(int? rol, string usuario)
        {
            List<management_validadorPrefacturasResult> lista = new List<management_validadorPrefacturasResult>();

            try

            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_validadorPrefacturas(rol, usuario).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_validadorCarguePrefacturasResult> GetPrefacturasListadoCargue(int? rol, string usuario)
        {
            List<management_validadorCarguePrefacturasResult> lista = new List<management_validadorCarguePrefacturasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    lista = db.management_validadorCarguePrefacturas(rol, usuario).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public management_prefacturasDatosCargueResult DatosPrefacturaIdCargue(int idCargue)
        {
            management_prefacturasDatosCargueResult dato = new management_prefacturasDatosCargueResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_prefacturasDatosCargue(idCargue).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }


        public List<management_consolidadoInicialPrefacturasResult> GetPrefacturasListadoConsolidadoInicial(int? idUsuario)
        {
            List<management_consolidadoInicialPrefacturasResult> lista = new List<management_consolidadoInicialPrefacturasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_consolidadoInicialPrefacturas(idUsuario).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public md_prefacturas_cargue_base GetPrefacturas(int id)
        {
            md_prefacturas_cargue_base dato = new md_prefacturas_cargue_base();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.md_prefacturas_cargue_base.Where(x => x.id_cargue_base == id).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public log_prefacturas_estadoValidacion GetLogEstadoValidacionPrefacturas(int? id)
        {
            log_prefacturas_estadoValidacion dato = new log_prefacturas_estadoValidacion();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.log_prefacturas_estadoValidacion.Where(x => x.id_cargue == id).OrderByDescending(x => x.id_log).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }



        public List<log_prefacturas_estadoValidacion> GetLogEstadoValidacionPrefacturasFases(int? id, int? fase)
        {
            List<log_prefacturas_estadoValidacion> dato = new List<log_prefacturas_estadoValidacion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    //dato = db.log_prefacturas_estadoValidacion.Where(x => x.id_cargue == id && x.fase == fase).OrderByDescending(x => x.id_log).ToList();
                    dato = db.log_prefacturas_estadoValidacion.Where(x => x.id_cargue == id).OrderByDescending(x => x.id_log).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }


        public log_control_validaciones_prefacturas GetLogPrefacturas(int? idCargue)
        {
            log_control_validaciones_prefacturas dato = new log_control_validaciones_prefacturas();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.log_control_validaciones_prefacturas.Where(x => x.id_cargue_base == idCargue).OrderByDescending(x => x.id_log).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }


        public List<md_prefacturas_detalle> GetPrefacturasById(string numeroPrefactura)
        {
            List<md_prefacturas_detalle> dato = new List<md_prefacturas_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.md_prefacturas_detalle.Where(x => x.remision_prefactura_fact == numeroPrefactura).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<md_prefacturas_detalle> GetPrefacturasByIdLote(int lote)
        {
            List<md_prefacturas_detalle> dato = new List<md_prefacturas_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.md_prefacturas_detalle.Where(x => x.Id_prefactura_cargue_base == lote).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public md_prefacturas_detalle GetPrefacturasID(int? id_detprefactura)
        {
            md_prefacturas_detalle dato = new md_prefacturas_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.md_prefacturas_detalle.Where(x => x.id_detalle_prefactura == id_detprefactura).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<ManagmentReportePrefacturasResult> GetRptPrefacturas(int idcargue)
        {
            List<ManagmentReportePrefacturasResult> list = new List<ManagmentReportePrefacturasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ManagmentReportePrefacturas(idcargue).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ManagementMedicamentosFacturacionResult> GetListMdFacturacion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ManagementMedicamentosFacturacion().ToList();
        }

        public List<managemente_medicamentos_facturacionResult> Getmedicamentos_facturacion(int mes, int año, int regional)
        {
            List<managemente_medicamentos_facturacionResult> ListResult = new List<managemente_medicamentos_facturacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.managemente_medicamentos_facturacion(mes, año, regional).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<ManagementMedicamentosFacturacionResult> GetMedicamentosFacturacionList(int? mesinicio, int? añoinicio, int? mesfinal, int? añofin, string Prestador, string regional)
        {
            List<ManagementMedicamentosFacturacionResult> lista = new List<ManagementMedicamentosFacturacionResult>();

            if (mesinicio == null || añoinicio == null || mesfinal == null || añofin == null || string.IsNullOrEmpty(Prestador) || string.IsNullOrEmpty(regional))
            {
                string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conexion);
                conn.Open();

                string query = "Select * from [vw_medicamentos_facturacion] where 0 = 0 ";

                if (mesinicio != null)
                    query += "and mes >= @mesinicio ";

                if (añoinicio != null)
                    query += "and año >= @añoinicio ";

                if (mesfinal != null)
                    query += "and mes <= @mesfinal ";

                if (añofin != null)
                    query += "and mes <= @añofinal ";

                if (!string.IsNullOrEmpty(Prestador))
                    query += "and prestador LIKE '%' + @prestador + '%' ";

                if (!string.IsNullOrEmpty(regional))
                    query += "and regional LIKE '%' + @regional + '%' ";

                SqlCommand command = new SqlCommand(query, conn);


                if (mesinicio != null)
                {
                    command.Parameters.Add("@mesinicio", SqlDbType.Int).Value = mesinicio;
                }

                if (añoinicio != null)
                {
                    command.Parameters.AddWithValue("@añoinicio", SqlDbType.Int).Value = añoinicio;
                }

                if (mesfinal != null)
                {
                    command.Parameters.Add("@mesfinal", SqlDbType.Int).Value = mesfinal;
                }

                if (añofin != null)
                {
                    command.Parameters.AddWithValue("@añofinal", SqlDbType.Int).Value = añofin;
                }

                if (!string.IsNullOrEmpty(Prestador))
                    command.Parameters.AddWithValue("@prestador", SqlDbType.VarChar).Value = Prestador;

                if (!string.IsNullOrEmpty(regional))
                    command.Parameters.AddWithValue("@regional", SqlDbType.VarChar).Value = regional;

                command.CommandTimeout = 3000;
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                foreach (DataRow row in dt.Rows)
                {
                    ManagementMedicamentosFacturacionResult obj2 = new ManagementMedicamentosFacturacionResult();
                    obj2.id_medicamentos_facturacion = Convert.ToInt32(row.ItemArray[0].ToString());
                    obj2.regional = row.ItemArray[1].ToString();
                    obj2.unis = row.ItemArray[2].ToString();
                    obj2.localidad = row.ItemArray[3].ToString();
                    obj2.tipo_identificacion = row.ItemArray[4].ToString();
                    obj2.identificacion_paciente = row.ItemArray[5].ToString();
                    obj2.cum = row.ItemArray[6].ToString();
                    obj2.concepto = row.ItemArray[7].ToString();
                    obj2.cantidad = Convert.ToInt32(row.ItemArray[8].ToString());
                    obj2.fecha_dispensacion = Convert.ToDateTime(row.ItemArray[9].ToString());
                    obj2.fecha_factura = Convert.ToDateTime(row.ItemArray[10].ToString());
                    obj2.numero_factura = row.ItemArray[11].ToString();
                    obj2.valor = Convert.ToDecimal(row.ItemArray[12].ToString());
                    obj2.nit_prestador = row.ItemArray[13].ToString();
                    obj2.prestador = row.ItemArray[14].ToString();
                    obj2.factura_sin_letras = row.ItemArray[15].ToString();
                    obj2.nombre_regional = row.ItemArray[16].ToString();
                    obj2.mes = Convert.ToInt32(row.ItemArray[17].ToString());
                    obj2.año = Convert.ToInt32(row.ItemArray[18].ToString());
                    obj2.fecha_digita = Convert.ToDateTime(row.ItemArray[19].ToString());
                    obj2.usuario_digita = row.ItemArray[20].ToString();
                    lista.Add(obj2);
                }
            }

            return lista;
        }

        public List<Managment_ReportePrefacturas_totalResult> GetPrefacturasTotal()
        {
            List<Managment_ReportePrefacturas_totalResult> list = new List<Managment_ReportePrefacturas_totalResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Managment_ReportePrefacturas_total().ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }
        public List<Managment_ReportePrefacturas_total_cerradasResult> GetPrefacturasTotalCerradas()
        {
            List<Managment_ReportePrefacturas_total_cerradasResult> list = new List<Managment_ReportePrefacturas_total_cerradasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Managment_ReportePrefacturas_total_cerradas().ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }
        public List<Managment_ReportePrefacturas_total_abiertasResult> GetPrefacturasTotalAbiertas()
        {
            List<Managment_ReportePrefacturas_total_abiertasResult> list = new List<Managment_ReportePrefacturas_total_abiertasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Managment_ReportePrefacturas_total_abiertas().ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }


        public List<Managment_ReportePrefacturas_cerrar_abiertasResult> GetPrefacturasCerrarAbiertas()
        {
            List<Managment_ReportePrefacturas_cerrar_abiertasResult> list = new List<Managment_ReportePrefacturas_cerrar_abiertasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Managment_ReportePrefacturas_cerrar_abiertas().ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }

        public List<management_prefacturas_consolidado_abiertasResult> GetPrefacturasAbiertas(int? cargueBase)
        {
            List<management_prefacturas_consolidado_abiertasResult> list = new List<management_prefacturas_consolidado_abiertasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    list = db.management_prefacturas_consolidado_abiertas(cargueBase).ToList();

                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }

        public List<management_prefacturas_consolidado_cerradasResult> GetPrefacturasCerradas(int? cargueBase)
        {
            List<management_prefacturas_consolidado_cerradasResult> list = new List<management_prefacturas_consolidado_cerradasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    list = db.management_prefacturas_consolidado_cerradas(cargueBase).ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }

        public List<Managment_ReportePrefacturas_cerrar_cerradasResult> GetPrefacturasCerrarCerradas()
        {
            List<Managment_ReportePrefacturas_cerrar_cerradasResult> list = new List<Managment_ReportePrefacturas_cerrar_cerradasResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.Managment_ReportePrefacturas_cerrar_cerradas().ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }
        }


        public List<md_prefacturas_cargue_base> ConsultaExistenciaPrefactura(int regional, string numPrefactura, int idPrestador)
        {
            List<md_prefacturas_cargue_base> lista = new List<md_prefacturas_cargue_base>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.md_prefacturas_cargue_base.Where(x => x.id_regional == regional && x.num_prefactura.Contains(numPrefactura) && x.id_prestador == idPrestador).ToList();
                    return lista;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return lista;
            }

        }

        public List<ref_referencia_pago> GetReferenciaPagoList()
        {
            List<ref_referencia_pago> list = new List<ref_referencia_pago>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_referencia_pago.ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return list;
            }

        }


        #endregion

        #region ANALISIS CASO 

        public List<analisis_caso_original> ConsultaEvolucionAnalisisCasoOriginal(Int32? idconcurrencia, Int32? idanalisiscaso, ref MessageResponseOBJ MsgRes)
        {
            List<analisis_caso_original> result = new List<analisis_caso_original>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (idanalisiscaso == null)
                    {
                        result = db.analisis_caso_original.Where(l => l.id_concurrencia == idconcurrencia).ToList();
                    }
                    else
                    {
                        result = db.analisis_caso_original.Where(l => l.id_concurrencia == idconcurrencia && l.id_analisis_caso_original == idanalisiscaso).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<analisis_caso_salud_publica> ConsultaEvolucionAnalisisSaludP(Int32 idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            List<analisis_caso_salud_publica> result = new List<analisis_caso_salud_publica>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (IdAnalisis == null)
                    {
                        result = db.analisis_caso_salud_publica.Where(l => l.id_concurrencia == idconcurrencia).ToList();
                    }
                    else
                    {
                        result = db.analisis_caso_salud_publica.Where(l => l.id_concurrencia == idconcurrencia && l.id_analisis_caso_salud_publica == IdAnalisis).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }


        public List<analisis_caso_alertas> ConsultaAnalisisCasoAlertas(Int32? idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            List<analisis_caso_alertas> result = new List<analisis_caso_alertas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (IdAnalisis == null)
                    {
                        result = db.analisis_caso_alertas.Where(l => l.id_concurrencia == idconcurrencia).ToList();
                    }
                    else
                    {
                        result = db.analisis_caso_alertas.Where(l => l.id_concurrencia == idconcurrencia && l.id_analisis_caso_alertas == IdAnalisis).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }


        public List<analisis_caso_muestreo> ConsultaAnalisisCasoMuestreo(Int32? idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            List<analisis_caso_muestreo> result = new List<analisis_caso_muestreo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (IdAnalisis == null)
                    {
                        result = db.analisis_caso_muestreo.Where(l => l.id_concurrencia == idconcurrencia).ToList();
                    }
                    else
                    {
                        result = db.analisis_caso_muestreo.Where(l => l.id_concurrencia == idconcurrencia && l.id_analisis_caso_muestreo == IdAnalisis).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }


        public List<ecop_concurrencia_eventos_en_asalud> ConsultaAnalisisEventosenSalud(Int32 idconcurrencia, Int32? IdAnalisis, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_eventos_en_asalud> result = new List<ecop_concurrencia_eventos_en_asalud>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    if (IdAnalisis == null)
                    {
                        result = db.ecop_concurrencia_eventos_en_asalud.Where(l => l.id_concurrencia == idconcurrencia).ToList();
                    }
                    else
                    {
                        result = db.ecop_concurrencia_eventos_en_asalud.Where(l => l.id_concurrencia == idconcurrencia && l.id_ecop_concurrencia_eventos_en_asalud == IdAnalisis).ToList();
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return null;
            }
        }

        public List<ecop_concurrencia_eventos_en_salud_detalle> ConsultaAnalisisEventosenSaludDetalle(ecop_concurrencia_eventos_en_salud_detalle OBJ, ref MessageResponseOBJ MsgRes)
        {
            List<ecop_concurrencia_eventos_en_salud_detalle> result = new List<ecop_concurrencia_eventos_en_salud_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    result = db.ecop_concurrencia_eventos_en_salud_detalle.Where(l => l.id_ecop_concurrencia_eventos_en_salud == OBJ.id_ecop_concurrencia_eventos_en_salud).ToList();



                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return null;
            }
        }

        public List<ecop_concurrencia_eventos_en_salud_detalle> GetAnalisisEventosenSaludDetalle(int idanalisis)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ecop_concurrencia_eventos_en_salud_detalle.Where(l => l.id_ecop_concurrencia_eventos_en_salud == idanalisis).ToList();
        }

        public List<ManagmentReporteAnalisisCasoSPResult> ReporteAnalisisCasoSP(Int32 idconcurrencia, Int32 idanalisis)
        {
            List<ManagmentReporteAnalisisCasoSPResult> Report = new List<ManagmentReporteAnalisisCasoSPResult>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                Report = db.ManagmentReporteAnalisisCasoSP(idconcurrencia, idanalisis).ToList();

            }
            return Report;
        }

        public List<ManagmentReporteAnalisisCasoOrgResult> ReporteAnalisisCasoOriginal(Int32 idConcurrencia, Int32 idAnalisis, ref MessageResponseOBJ MsgRes)
        {
            List<ManagmentReporteAnalisisCasoOrgResult> Result = new List<ManagmentReporteAnalisisCasoOrgResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    Result = db.ManagmentReporteAnalisisCasoOrg(idConcurrencia, idAnalisis).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return Result;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return Result;
            }
        }

        public List<ManagmentReporteAnalisisESResult> ReporteEventosSalud(Int32 IdConcurrencia, Int32 Idanalisis)
        {
            List<ManagmentReporteAnalisisESResult> Result = new List<ManagmentReporteAnalisisESResult>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                Result = db.ManagmentReporteAnalisisES(IdConcurrencia, Idanalisis).ToList();
                return Result;
            }
        }

        public List<vw_tablero_analisis_casos> ConsultaTableroAnalisisCasos(ref MessageResponseOBJ MsgRes)
        {
            List<vw_tablero_analisis_casos> result = new List<vw_tablero_analisis_casos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.vw_tablero_analisis_casos.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public GestionAnalisisDeCasos GetAnalisisGestion(Int32? idtipoanalisis, Int32? idanalsis, ref MessageResponseOBJ MsgRes)
        {
            GestionAnalisisDeCasos result = new GestionAnalisisDeCasos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.GestionAnalisisDeCasos.Where(l => l.id_tipo_analisis_caso == idtipoanalisis && l.id_analisis_caso == idanalsis).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }


        public List<vw_analisis_caso_alertas> GetIdAnalisisAlertas(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            List<vw_analisis_caso_alertas> ListResult = new List<vw_analisis_caso_alertas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_analisis_caso_alertas.Where(p => p.id_concurrencia == id_concurrencia).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<vw_analisis_caso_muestreo> GetIdAnalisisMuestras(Int32 id_concurrencia, ref MessageResponseOBJ MsgRes)
        {
            List<vw_analisis_caso_muestreo> ListResult = new List<vw_analisis_caso_muestreo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_analisis_caso_muestreo.Where(p => p.id_concurrencia == id_concurrencia).ToList();
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        #endregion

        #region URGENCIAS 
        public List<urg_cargue_base_ok> ConsultarUrgencias(int? idurgencia, DateTime? fechadesde, DateTime? fechahasta, int? regional, int? idusuario, ref MessageResponseOBJ MsgRes)
        {
            List<urg_cargue_base_ok> result = new List<urg_cargue_base_ok>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    TimeSpan ts2 = new TimeSpan(23, 59, 0);
                    TimeSpan ts1 = new TimeSpan(00, 00, 0);
                    if (idurgencia != null)
                    {
                        result = db.urg_cargue_base_ok.Where(l => l.id_urg_base_ok == idurgencia).ToList();
                    }
                    else
                    {

                        if (idusuario != null)
                        {
                            regional = db.sis_auditor_regional.Where(l => l.id_auditor == idusuario).FirstOrDefault().id_regional;
                        }

                        result = db.urg_cargue_base_ok.Where(l => l.fecha_digita.Value.Date >= fechadesde.Value.Date && l.fecha_digita.Value.Date <= fechahasta.Value.Date).ToList();

                        if (regional != 0)
                        {
                            if (regional == 1)
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("SUR")).ToList();
                            }
                            else if (regional == 2)
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("CARIBE")).ToList();
                            }
                            else if (regional == 3)
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("SANTANDERES")).ToList();
                            }
                            else if (regional == 4)
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("ORINOQUIA")).ToList();
                            }
                            else if (regional == 5)
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("CUB")).ToList();
                            }
                            else
                            {
                                result = result.Where(l => l.COORDINACION.ToUpper().Contains("MAGDALENA MEDIO")).ToList();
                            }
                        }


                        var consulta = from a in db.urg_cargue_base_ok.DefaultIfEmpty()
                                       join b in db.urg_auditoria_urgencias.DefaultIfEmpty()
                                        on a.id_urg_base_ok equals b.id_urg_base
                                       select a.id_urg_base_ok;

                        List<int> lista = consulta.ToList();
                        foreach (int item in lista)
                        {
                            result.Remove(result.Where(l => l.id_urg_base_ok == item).FirstOrDefault());
                        }
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<Ref_tipo_egreso> ConsultaTipoEgreso(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_tipo_egreso> ResultList = new List<Ref_tipo_egreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ResultList = db.Ref_tipo_egreso.ToList();
                }
                return ResultList;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return ResultList;
            }

        }

        public List<ref_urg_destino_paciente> ConsultaDestinoPaciente(ref MessageResponseOBJ MsgRes)
        {
            List<ref_urg_destino_paciente> ResultList = new List<ref_urg_destino_paciente>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ResultList = db.ref_urg_destino_paciente.ToList();
                }
                return ResultList;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return ResultList;
            }
        }

        public List<vw_tablero_urgencias_ok> ConsultaTablerUrgenciasOk()
        {
            List<vw_tablero_urgencias_ok> lstResult = new List<vw_tablero_urgencias_ok>();
            MessageResponseOBJ MsgRes = new MessageResponseOBJ();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tablero_urgencias_ok.ToList();

                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        #endregion

        #region Cierre Contable 

        public cierre_contable GetCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {

            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            cierre_contable cierre = db.cierre_contable.Where(l => l.id_cierre == idcierre).FirstOrDefault();
            return cierre;

        }

        public List<cierre_contable> GetListCierreContable(ref MessageResponseOBJ MsgRes)
        {
            List<cierre_contable> ResultList = new List<cierre_contable>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                ResultList = db.cierre_contable.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return ResultList;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ResultList;
            }
        }

        public List<vw_totales_cierre_contable> GetListTotalesCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            List<vw_totales_cierre_contable> List = new List<vw_totales_cierre_contable>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List = db.vw_totales_cierre_contable.Where(l => l.id_cierre == idcierre).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return List;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return List;
            }

        }

        public List<vw_causas_facturas> GetListCausasCierreContable(int idcierre, ref MessageResponseOBJ MsgRes)
        {
            List<vw_causas_facturas> List = new List<vw_causas_facturas>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List = db.vw_causas_facturas.Where(l => l.id_cierre == idcierre).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return List;
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return List;
            }
        }

        public cierre_contable_cargue_base traerCierreContable(int? mes, int? año, int? regional)
        {
            cierre_contable_cargue_base dato = new cierre_contable_cargue_base();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cierre_contable_cargue_base.Where(x => x.año == año && x.mes == mes && x.regional == regional).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<management_cierre_contable_tableroControlResult> TraerDatosCierreContable()
        {
            List<management_cierre_contable_tableroControlResult> lista = new List<management_cierre_contable_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cierre_contable_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }




        #endregion

        #region COHORTES

        public List<ref_cohortes> Get_refCohortes()
        {
            List<ref_cohortes> result = new List<ref_cohortes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_cohortes.ToList();

                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<ref_cohortes> Get_refCohortesSindh()
        {
            List<ref_cohortes> result = new List<ref_cohortes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_cohortes.Where(x => x.aplica_adh == false).ToList();

                }
                return result;
            }
            catch
            {
                return result;
            }
        }
        public List<ref_adh_modalidad_prestacion> Get_adherencia_modalidad_prestacion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adh_modalidad_prestacion.ToList();
        }

        public List<management_cohortesBeneficiarioResult> GetCohortesBeneficiario(string idDoc)
        {
            List<management_cohortesBeneficiarioResult> list = new List<management_cohortesBeneficiarioResult>();
            List<management_cohortesBeneficiarioResult> cohorte = new List<management_cohortesBeneficiarioResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    int idDocumento = int.Parse(idDoc);
                    list = db.management_cohortesBeneficiario(idDocumento).OrderByDescending(x => x.id_cohorte).ToList();
                    list = list.Where(x => x.descripcion != null).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<management_HospitalizacionEvitable_cohortesResult> HospitalizacionPrevenible_cohortes(string idDoc)
        {
            List<management_HospitalizacionEvitable_cohortesResult> list = new List<management_HospitalizacionEvitable_cohortesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_HospitalizacionEvitable_cohortes(idDoc).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ecop_directorioPPE_correos> GetEcop_DirectorioPPE_Correos(string regional)
        {
            List<ecop_directorioPPE_correos> list = new List<ecop_directorioPPE_correos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ecop_directorioPPE_correos.Where(x => x.regional.Contains(regional)).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<ecop_directorioPPE_correos> GetEcop_DirectorioPPE_CorreosDocumento(string documento)
        {
            List<ecop_directorioPPE_correos> list = new List<ecop_directorioPPE_correos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ecop_directorioPPE_correos.Where(x => x.documento_profesional.Equals(documento)).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<ref_cargue_vigilanciaCohortes_tipos> ListaTiposVCohorte()
        {
            List<ref_cargue_vigilanciaCohortes_tipos> lista = new List<ref_cargue_vigilanciaCohortes_tipos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cargue_vigilanciaCohortes_tipos.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        #endregion

        #region ADHERENCIA 

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio()
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    return db.ref_adh_tipo_criterio.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public List<ver_tipocriterio> get_ref_tipoCriterioGeneral()
        {
            List<ver_tipocriterio> list = new List<ver_tipocriterio>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ver_tipocriterio.ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }
        public List<ref_adh_grupo_tipocriterio> get_ref_grupoTipoCriterio()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adh_grupo_tipocriterio.ToList();
        }

        public List<adh_tipocriterio> get_adh_tipocriterio(int idadherencia)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.adh_tipocriterio.Where(l => l.id_tipo_cohorte == idadherencia).ToList();
        }

        public List<ref_adh_grupo_tipocriterio> get_ref_adh_grupotipocriterio()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adh_grupo_tipocriterio.ToList();
        }

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio_cohorte(int tipo_cohorte)
        {
            List<ref_adh_tipo_criterio> resul = new List<ref_adh_tipo_criterio>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    List<int> lista = db.adh_criterio.Where(l => l.id_tipo_cohorte == tipo_cohorte).Select(l => l.id_tipo_criterio).Distinct().ToList();
                    if (lista.Count > 0)
                    {
                        foreach (int i in lista)
                        {
                            ref_adh_tipo_criterio item = db.ref_adh_tipo_criterio.Where(l => l.id_tipo_criterio == i).FirstOrDefault();
                            resul.Add(item);
                        }
                    }
                    return resul;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<adh_criterio> getcriteriosbytipocohorte(int tipocohorte)
        {
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                return db.adh_criterio.Where(l => l.id_tipo_cohorte == tipocohorte).ToList();

            }
            catch
            {
                throw;
            }
        }

        public adh_criterio ConsultarCriterioById(int idcriterio)
        {
            adh_criterio result = new adh_criterio();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.adh_criterio.Where(l => l.id_adh_criterio == idcriterio).FirstOrDefault();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<vw_rptResultadosAdherencia> GetResultadosPrestador(Int32? idresultados)
        {

            List<vw_rptResultadosAdherencia> result = new List<vw_rptResultadosAdherencia>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                if (idresultados == null)
                {
                    result = db.vw_rptResultadosAdherencia.ToList();
                }
                else
                {
                    result = db.vw_rptResultadosAdherencia.Where(l => l.id_adh_resultados == idresultados).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<managmentReporteResultadosAdherenciaResult> GetResultadosAdherencia(Int32 idresultados)
        {
            List<managmentReporteResultadosAdherenciaResult> result = new List<managmentReporteResultadosAdherenciaResult>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.managmentReporteResultadosAdherencia(idresultados).ToList();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<managmentReporteResultadosAdherencia2Result> GetResultadosAdherencia2(Int32 idresultados)
        {
            List<managmentReporteResultadosAdherencia2Result> result = new List<managmentReporteResultadosAdherencia2Result>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.managmentReporteResultadosAdherencia2(idresultados).ToList();
                return result;
            }
            catch
            {
                return result;
            }
        }


        public List<Management_adh_cantidad_resultados_grupoResult> GetResultadosGrupoAdherencia(Int32 idresultados)
        {
            List<Management_adh_cantidad_resultados_grupoResult> result = new List<Management_adh_cantidad_resultados_grupoResult>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Management_adh_cantidad_resultados_grupo(idresultados).ToList();
                return result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<Ref_ips_cuentas> getprestadores()
        {
            List<Ref_ips_cuentas> result = new List<Ref_ips_cuentas>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Ref_ips_cuentas.ToList();
                return result;
            }
            catch
            {
                return result;
            }

        }
        public List<management_prestadoresHomologadosResult> getprestadoresHomologados()
        {
            List<management_prestadoresHomologadosResult> result = new List<management_prestadoresHomologadosResult>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.management_prestadoresHomologados().ToList();
                return result;
            }
            catch
            {
                return result;
            }

        }

        public List<Ref_ips_cuentas> getprestadoresEspecial(string nit, string prestador)
        {
            List<Ref_ips_cuentas> result = new List<Ref_ips_cuentas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.Ref_ips_cuentas.ToList();


                    if (!string.IsNullOrEmpty(nit))
                    {
                        result = result.Where(x => x.Nit.Contains(nit)).ToList();
                    }
                    if (!string.IsNullOrEmpty(prestador))
                    {
                        result = result.Where(x => x.Nombre.Contains(prestador)).ToList();
                    }

                    return result;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }

        }



        public Ref_ips_cuentas getprestadoresNit(string nit)
        {
            Ref_ips_cuentas result = new Ref_ips_cuentas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.Ref_ips_cuentas.Where(x => x.Nit == nit || x.nit_prestador_suis == nit).FirstOrDefault();
                    return result;

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }

        }

        public ref_cohortes getTipoCohorteById(int idtipocohorte)
        {
            ref_cohortes result = new ref_cohortes();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.ref_cohortes.Where(l => l.id_ref_cohortes == idtipocohorte).FirstOrDefault();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<adh_resultados> GetResultadosPrestadorv2(int idprestador, int profesional, int mes, int año)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                List<adh_resultados> resul = db.adh_resultados.Where(l => l.id_prestador == idprestador && l.id_profesional == profesional && l.mes_periodo_evaluacion == mes && l.año_periodo_evaluacion == Convert.ToInt32(año)).ToList();
                return resul;
            }
        }

        public List<ref_adherencia_unis> GetUnisByRegional(int idregional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adherencia_unis.Where(l => l.regional_id == idregional).ToList();
        }


        public List<Ref_odont_unis> unisRegional(int? idRegional)
        {
            List<Ref_odont_unis> lista = new List<Ref_odont_unis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_odont_unis.Where(x => x.id_regional == idRegional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_adherencia_ciudad> GetciudadByunis(int idunis)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adherencia_ciudad.Where(l => l.unis_id == idunis).ToList();
        }

        public List<ref_adherencia_prestador_ciudad> GetPrestadoresByciudad(int idciudad)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adherencia_prestador_ciudad.Where(l => l.id_ref_adherencia_ciudad == idciudad).ToList();

        }

        public List<ref_adherencia_profesional_prestador> GetProfesionalesByprestador(int idprestador)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adherencia_profesional_prestador.Where(l => l.prestador_id == idprestador).ToList();

        }
        #endregion

        #region ODONTOLOGIA 

        public List<Ref_odont_list_check_ortod> getcheckOrtod()
        {
            List<Ref_odont_list_check_ortod> result = new List<Ref_odont_list_check_ortod>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Ref_odont_list_check_ortod.ToList();

                return result;
            }
            catch
            {
                return result;
            }

        }

        public List<vw_odontologia_detallado_historia_clinica> getdetallehistoriaclinica()
        {
            List<vw_odontologia_detallado_historia_clinica> result = new List<vw_odontologia_detallado_historia_clinica>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.vw_odontologia_detallado_historia_clinica.ToList();

                return result;
            }
            catch
            {
                return result;
            }

        }

        public List<Ref_odont_check_porcentajes> getcheckPorcentaje()
        {
            List<Ref_odont_check_porcentajes> result = new List<Ref_odont_check_porcentajes>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Ref_odont_check_porcentajes.ToList();

                return result;
            }
            catch
            {
                return result;
            }

        }

        public List<Ref_odont_tipo_endodoncia> getListTipoEndodoncia()
        {
            List<Ref_odont_tipo_endodoncia> result = new List<Ref_odont_tipo_endodoncia>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Ref_odont_tipo_endodoncia.ToList();

                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ref_odont_parametros_auditados> getListParametrosAuditados()
        {

            List<Ref_odont_parametros_auditados> result = new List<Ref_odont_parametros_auditados>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                result = db.Ref_odont_parametros_auditados.ToList();

                return result;
            }
            catch
            {
                return result;
            }

        }

        public List<vw_odont_ortodoncia_report> ConsultaIdReporteOrtodoncia(Int32 id_tratamiento_ortodoncia, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_ortodoncia_report> LstResult = new List<vw_odont_ortodoncia_report>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_ortodoncia_report.Where(p => p.id_tratamiento_ortodoncia == id_tratamiento_ortodoncia).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<cargue_seguimiento_covid19> ConsultaIdSeguimientoCovid19(Int32? ID, ref MessageResponseOBJ MsgRes)
        {
            List<cargue_seguimiento_covid19> LstResult = new List<cargue_seguimiento_covid19>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.cargue_seguimiento_covid19.Where(p => p.id == ID).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_seguimiento_covid19_detalle> ConsultaIdSeguimientoCovid19Detalle(Int32 ID, ref MessageResponseOBJ MsgRes)
        {
            List<vw_seguimiento_covid19_detalle> LstResult = new List<vw_seguimiento_covid19_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_seguimiento_covid19_detalle.Where(p => p.id_cargue == ID).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_seguimiento_covid19_ultimo_detalle> ConsultaIdSeguimientoCovid19DetalleUltimo(Int32 ID, ref MessageResponseOBJ MsgRes)
        {
            List<vw_seguimiento_covid19_ultimo_detalle> LstResult = new List<vw_seguimiento_covid19_ultimo_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_seguimiento_covid19_ultimo_detalle.Where(p => p.id_cargue == ID).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_removible_report> ConsultaIdReporteRemovible(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_removible_report> LstResult = new List<vw_odont_removible_report>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_removible_report.Where(p => p.id_rehabilitacion_oral_protesis_removibles == id_rehabilitacion_oral_protesis_removibles).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_endodoncia_report> ConsultaIdReporteEndodoncia(Int32 id_tratamiento_endodoncia, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_endodoncia_report> LstResult = new List<vw_odont_endodoncia_report>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_endodoncia_report.Where(p => p.id_tratamiento_endodoncia == id_tratamiento_endodoncia).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_fija_report> ConsultaIdReporteProtesisFija(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_fija_report> LstResult = new List<vw_odont_fija_report>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_fija_report.Where(p => p.id_rehabilitacion_oral_protesis_fija == id_tratamiento_Protesis_Fija).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<cargue_seguimiento_covid19> ConsultaDocumentoPacienteCovid19(String id)
        {
            List<cargue_seguimiento_covid19> LstResult = new List<cargue_seguimiento_covid19>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.cargue_seguimiento_covid19.Where(p => p.documento == id).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<cargue_seguimiento_covid19> ConsultaCargueCovid19(ref MessageResponseOBJ MsgRes)
        {
            List<cargue_seguimiento_covid19> LstResult = new List<cargue_seguimiento_covid19>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.cargue_seguimiento_covid19.ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<cargue_seguimiento_covid19_detalle> ConsultaDetalleSeguimientoCovid19(Int32 id_cargue, ref MessageResponseOBJ MsgRes)
        {
            List<cargue_seguimiento_covid19_detalle> LstResult = new List<cargue_seguimiento_covid19_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.cargue_seguimiento_covid19_detalle.Where(p => p.id_cargue == id_cargue).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<odont_rehabilitacion_oral_protesis_fija_dtll> ConsultaIdReporteProtesisFijaDtll(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {

            List<odont_rehabilitacion_oral_protesis_fija_dtll> LstResult = new List<odont_rehabilitacion_oral_protesis_fija_dtll>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.odont_rehabilitacion_oral_protesis_fija_dtll.Where(p => p.id_odont_rehabilitacion_oral_protesis_fija == id_tratamiento_Protesis_Fija).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_reporte_removible_dtll> ConsultaIdReporteProtesisRemovibleDtll(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {

            List<vw_odont_reporte_removible_dtll> LstResult = new List<vw_odont_reporte_removible_dtll>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_reporte_removible_dtll.Where(p => p.id_rehabilitacion_oral_protesis_removibles == id_rehabilitacion_oral_protesis_removibles).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }


        public List<vw_odont_tableros_ortodoncia> ConsultaListadoTTOsOrodoncia(ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ortodoncia> LstResult = new List<vw_odont_tableros_ortodoncia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_tableros_ortodoncia.ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_tableros_ortodoncia_prof> ConsultaListadoTTOsOrodonciaProf(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_ortodoncia_prof.ToList();
            }
        }

        public List<vw_odont_tableros_ProtesisFija> ConsultaListadoTTOsPPF(ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisFija> LstResult = new List<vw_odont_tableros_ProtesisFija>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_tableros_ProtesisFija.ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_tableros_ProtesisFija_prof> ConsultaListadoTTOsProf(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_ProtesisFija_prof.ToList();
            }
        }

        public List<vw_seguimiento_covid19_diario> ConsultaListadoseguimientoCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_diario.ToList();
            }
        }

        public List<vw_seguimiento_covid19_descarga_diaria> ConsultaListadoseguimientodescargaCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_descarga_diaria.ToList();
            }
        }

        public List<vw_seguimiento_covid19_descarga_interdiaria> ConsultaListadoseguimientointerdiariodescargaCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_descarga_interdiaria.ToList();
            }
        }

        public List<vw_seguimiento_covid19_descarga_casos_cerrados> ConsultaListadoseguimientoCasosCerradosdescargaCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_descarga_casos_cerrados.ToList();
            }
        }


        public List<vw_seguimiento_covid19_interdiario> ConsultaListadoseguimientoInterdiarioCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_interdiario.ToList();
            }
        }

        public List<vw_seguimiento_covid19_casos_cerrados> ConsultaListadoseguimientoCerradosCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_casos_cerrados.ToList();
            }
        }


        public List<vw_seguimiento_covid19_general_detalle> Consultageneraldetalleseguimientocovid()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_covid19_general_detalle.ToList();
            }
        }

        public List<ref_covid19_tipificacion> ConsultaListadoTipicacionCovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_covid19_tipificacion.ToList();
            }
        }

        public List<ref_covid19_tipificacion7_detalle> ConsultaListadoTipicacion7Covid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_covid19_tipificacion7_detalle.ToList();
            }
        }
        public List<ref_covid19_estado_asalud> Consultaestadoasaludcovid19()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_covid19_estado_asalud.ToList();
            }
        }
        public List<vw_odont_tableros_ProtesisRemov> ConsultaListadoTTOsRemovible(ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisRemov> LstResult = new List<vw_odont_tableros_ProtesisRemov>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_tableros_ProtesisRemov.ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_tableros_ProtesisRemov_prof> ConsultaListadoTTOsRemoviblesProf(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {

                return db.vw_odont_tableros_ProtesisRemov_prof.ToList();
            }
        }

        public List<vw_odont_tableros_endodoncia> ConsultaListadoTTOsEndodoncia(ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_endodoncia> LstResult = new List<vw_odont_tableros_endodoncia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_tableros_endodoncia.ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_tableros_endodoncia_prof> ConsultaListadoEndodonciaProf(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_endodoncia_prof.ToList();
            }
        }

        public List<Ref_odont_acciones_mejora> GetListAccionesMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_acciones_mejora.ToList();
            }
        }

        public List<Ref_odont_estado_accion> GetListEstadosAccionesMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_estado_accion.ToList();
            }
        }

        public List<vw_odont_tbl_prestadores> GetprestadoresPlanMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tbl_prestadores.ToList();
            }
        }

        public List<vw_odont_planes_mejora> GetPlanesMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_planes_mejora.ToList();
            }
        }

        public List<odont_plan_mejora> GetPlanMejoraTra()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.odont_plan_mejora.ToList();
            }
        }

        public List<odont_plan_mejora_beneficiario> GetPlanMejoraBen()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.odont_plan_mejora_beneficiario.ToList();
            }
        }

        public List<vw_odont_planes_mejora> GetPlanMejoraTradtll(Int32 id_odont_plan_mejora, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_planes_mejora> LstResult = new List<vw_odont_planes_mejora>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_planes_mejora.Where(p => p.id_odont_plan_mejora == id_odont_plan_mejora).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_planes_mejora_ben> GetPlanMejoraBendtll(Int32 id_odont_plan_mejora_beneficiario, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_planes_mejora_ben> LstResult = new List<vw_odont_planes_mejora_ben>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_planes_mejora_ben.Where(p => p.id_odont_plan_mejora_beneficiario == id_odont_plan_mejora_beneficiario).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<odont_historia_clinica> GetHistoriaClinica()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.odont_historia_clinica.ToList();
            }
        }

        public List<Ref_odont_hc_calidad_formal> GetHistoriaClinicaRefCalidadFormal()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_hc_calidad_formal.ToList();
            }
        }

        public List<Ref_odont_hc_calidad_contenido> GetHistoriaClinicaRefContenido()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_hc_calidad_contenido.ToList();
            }
        }

        public List<odont_historia_clinica_paciente> GetHistoriaClinicaPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            List<odont_historia_clinica_paciente> LstResult = new List<odont_historia_clinica_paciente>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.odont_historia_clinica_paciente.Where(p => p.id_odont_historia_clinica == id_odont_historia_clinica).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_historia_clinica_detalle> GetVWHistoriaClinicaDetalle(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_historia_clinica_detalle> LstResult = new List<vw_odont_historia_clinica_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_historia_clinica_detalle.Where(p => p.id_odont_historia_clinica_paciente == id_odont_historia_clinica_paciente).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }
        public List<vw_odont_historia_clinica_detalle_contenido> GetVWHistoriaClinicaDetalleConten(Int32 id_odont_historia_clinica_paciente, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_historia_clinica_detalle_contenido> LstResult = new List<vw_odont_historia_clinica_detalle_contenido>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_historia_clinica_detalle_contenido.Where(p => p.id_odont_historia_clinica_paciente == id_odont_historia_clinica_paciente).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }
        public List<Ref_odont_prestadores> GetPrestadoresOdont()
        {
            List<Ref_odont_prestadores> lista = new List<Ref_odont_prestadores>();
            try
            {

                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_odont_prestadores.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<ref_odontologia_protesisfija_dientes> OdontoProtesisFijaDientes(int? tipo)
        {
            List<ref_odontologia_protesisfija_dientes> lista = new List<ref_odontologia_protesisfija_dientes>();
            try
            {

                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_odontologia_protesisfija_dientes.Where(x => x.tipo == tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<ref_odontologia_protesisfija_dientes> OdontoProtesisFijaDientesTotal()
        {
            List<ref_odontologia_protesisfija_dientes> lista = new List<ref_odontologia_protesisfija_dientes>();
            try
            {

                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_odontologia_protesisfija_dientes.OrderBy(x => x.diente).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public ref_odontologia_protesisfija_dientes TraerDienteId(int? id)
        {
            ref_odontologia_protesisfija_dientes dato = new ref_odontologia_protesisfija_dientes();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_odontologia_protesisfija_dientes.Where(x => x.diente == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<vw_odont_totales_hc> GetOdontogTotales(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_totales_hc> LstResult = new List<vw_odont_totales_hc>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_totales_hc.Where(p => p.id_odont_historia_clinica == id_odont_historia_clinica).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_detalle_plan_mejora> GetOdontogdetallePlanMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_detalle_plan_mejora.ToList();
            }
        }

        public List<vw_odont_tableros_ortodoncia> GetOdontogTablerosOrtodoncia()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_ortodoncia.ToList();
            }
        }

        public List<vw_odont_tableros_ProtesisFija> GetOdontogTablerosPT()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_ProtesisFija.ToList();
            }
        }

        public List<vw_odont_tableros_ProtesisRemov> GetOdontogTablerosPR()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_ProtesisRemov.ToList();
            }
        }

        public List<vw_odont_tableros_endodoncia> GetOdontogTablerosEndodoncia()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_endodoncia.ToList();
            }
        }

        public List<Ref_odont_parametros_auditados_rem> GetparametrosRem()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_parametros_auditados_rem.ToList();
            }
        }

        public List<Ref_odont_tratamiento_rem> GetTratamientosRem()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Ref_odont_tratamiento_rem.ToList();
            }
        }

        public List<vw_odont_tableros_plan_mejora> GetOdontogTablerosPlanMejora()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_tableros_plan_mejora.ToList();
            }
        }

        public List<vw_odont_consolidado_hc> GetConsolidadoHc(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_consolidado_hc.ToList();
            }

        }

        public List<vw_odont_consolidado_hc_prestador> GetConsolidadoHcporprestador(ref MessageResponseOBJ MsgRes)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_consolidado_hc_prestador.ToList();
            }
        }

        public List<vw_odont_remisiones_especialidades> GetRemisiones(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_odont_remisiones_especialidades.ToList();
        }

        public List<vw_odont_remisiones_verificadas> GetRemisionesVerificadas(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_odont_remisiones_verificadas.ToList();
        }

        public List<vw_odont_count_planes_mejora> GetListCountPlanesMejora(int idregional)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_count_planes_mejora.Where(l => l.id_regional == idregional).ToList();
            }
        }

        public List<vw_odont_count_acciones_mejora> GetListCountAccionesMejora(int idregional)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_odont_count_acciones_mejora.Where(l => l.id_regional == idregional).ToList();
            }
        }

        public List<vw_totales_odont> GetTotalPaciente(Int32 id_odont_historia_clinica, ref MessageResponseOBJ MsgRes)
        {
            List<vw_totales_odont> LstResult = new List<vw_totales_odont>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_totales_odont.Where(p => p.id_odont_historia_clinica == id_odont_historia_clinica).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_reportesTratamientosUnificados> GetReportTratamientosUnificados(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_reportesTratamientosUnificados.ToList();
        }

        public List<vw_prestadores_odontologiaUnificado> GetPrestadoresOdonUnificados(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_prestadores_odontologiaUnificado.ToList();
        }

        public List<vw_odont_porcentaje_d1_fija> Getporcentaje_d1_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_porcentaje_d1_fija> LstResult = new List<vw_odont_porcentaje_d1_fija>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_porcentaje_d1_fija.Where(p => p.id_odont_rehabilitacion_oral_protesis_fija == id_protesis_fija).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<vw_odont_porcentaje_d2_fija> Getporcentaje_d2_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_porcentaje_d2_fija> LstResult = new List<vw_odont_porcentaje_d2_fija>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.vw_odont_porcentaje_d2_fija.Where(p => p.id_odont_rehabilitacion_oral_protesis_fija == id_protesis_fija).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }


        #region FFMM

        public List<vw_ffmm_consulta_radicacion_ips> GetOdontogRadicacionIPS()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_ffmm_consulta_radicacion_ips.ToList();
            }
        }

        public List<ffmm_Cuentas_radicacion> GetRadicacionIPSFacturas(Int32 id_proveedor, Int32 id_factura, string prefijo, ref MessageResponseOBJ MsgRes)
        {
            List<ffmm_Cuentas_radicacion> LstResult = new List<ffmm_Cuentas_radicacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    LstResult = db.ffmm_Cuentas_radicacion.Where(p => p.ips_proveedor == id_proveedor && p.numero_factura == id_factura && p.prefijo_factura.Contains(prefijo)).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<ffmm_Cuentas_auditoria> GetIPSTotal(Int32 id_ref_ffmm_radicacion_Cuentas, ref MessageResponseOBJ MsgRes)
        {
            List<ffmm_Cuentas_auditoria> LstResult = new List<ffmm_Cuentas_auditoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.ffmm_Cuentas_auditoria.Where(p => p.id_ref_ffmm_radicacion_Cuentas == id_ref_ffmm_radicacion_Cuentas).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<ffmm_cuentas_glosas> GetIPSTotalGlosas(Int32 id_ref_ffmm_radicacion_Cuentas, ref MessageResponseOBJ MsgRes)
        {
            List<ffmm_cuentas_glosas> LstResult = new List<ffmm_cuentas_glosas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    LstResult = db.ffmm_cuentas_glosas.Where(p => p.id_ref_ffmm_radicacion_Cuentas == id_ref_ffmm_radicacion_Cuentas).ToList();

                    return LstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return LstResult;
            }
        }

        public List<managmentFFMMfacturasRadicadasLoteResult> GetRecepcionFacturasFFMM(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.managmentFFMMfacturasRadicadasLote().ToList();
        }

        public List<Management_FFMM_Glosas_PrestadoresResult> GetFFMMGlosasPrestadores(ref MessageResponseOBJ MsgRes)
        {
            List<Management_FFMM_Glosas_PrestadoresResult> lstResult = new List<Management_FFMM_Glosas_PrestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Management_FFMM_Glosas_Prestadores().OrderBy(p => p.departamento).ThenBy(p => p.muninombre).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<managmentFFMMfacturasRadicadasdtllResult> GetRecepcionFacturasDTLLFFMM(Int32 id_cargue_base, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.managmentFFMMfacturasRadicadasdtll(id_cargue_base).ToList();
        }

        public List<managmentFFMMfacturasRadicadasid_dtllResult> GetRecepcionFacturasDTLLFFMMId(Int32 id_cargue_dtll, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.managmentFFMMfacturasRadicadasid_dtll(id_cargue_dtll).ToList();
        }

        public List<Management_FFMM_Consultas_cuentasResult> GetConsultaFFMMCuentas(ref MessageResponseOBJ MsgRes)
        {
            List<Management_FFMM_Consultas_cuentasResult> lista = new List<Management_FFMM_Consultas_cuentasResult>();
            try
            {

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                lista = db.Management_FFMM_Consultas_cuentas().ToList();
                return lista;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return lista;
        }

        public List<management_CupsAuditoriaResult> ListaCupsAuditoria()
        {
            List<management_CupsAuditoriaResult> list = new List<management_CupsAuditoriaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_CupsAuditoria().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<Management_FFMM_Consultas_ConcurreniaResult> GetConsultaFFMMConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_Consultas_Concurrenia().ToList();
        }

        public List<Management_FFMM_Consultas_PADResult> GetConsultaFFMMPad(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_Consultas_PAD().ToList();
        }

        public List<Management_FFMM_consulta_cuentas_primeraResult> GetConsultaFFMMCuentasUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_cuentas_primera(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_cuentas_segundaResult> GetConsultaFFMMCuentasDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_cuentas_segunda(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_cuentas_terceraResult> GetConsultaFFMMCuentasTres(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_cuentas_tercera(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_cuentas_cuartaResult> GetConsultaFFMMCuentasCuatro(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_cuentas_cuarta(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_cuentas_quintaResult> GetConsultaFFMMCuentasCinco(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_cuentas_quinta(fecha_inicial, fecha_final).ToList();
        }

        public List<ref_auditor> listAuditor()
        {
            List<ref_auditor> list = new List<ref_auditor>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_auditor.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }

        public List<Management_FFMM_consulta_concurrencia_primeraResult> GetConsultaFFMMConcurrenciaUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_concurrencia_primera(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_concurrencia_segundaResult> GetConsultaFFMMConcurrenciaDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_concurrencia_segunda(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_concurrencia_terceroResult> GetConsultaFFMMConcurrenciaTercero(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_concurrencia_tercero(fecha_inicial, fecha_final).ToList();
        }
        public List<Management_FFMM_consulta_concurrencia_cuartoResult> GetConsultaFFMMConcurrenciaCuarto(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Management_FFMM_consulta_concurrencia_cuarto(fecha_inicial, fecha_final).ToList();
        }

        #endregion

        #endregion

        #region CONFIGURACION
        public sis_configuracion GetParametro(string parametro)
        {
            sis_configuracion result = new sis_configuracion();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                result = db.sis_configuracion.Where(l => l.nom_parametro.ToLower().Contains(parametro.ToLower()) && l.activo == true).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Radicacion Electronica

        public List<vw_prestadores_lotes> GetRecepcionFacturas(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_prestadores_lotes.Where(l => l.count_facturas_pendientes > 0).ToList();
        }

        public List<vw_analistas_lotes> GetRecepcionFacturasAnalistas(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_analistas_lotes.Where(l => l.count_facturas_pendientes > 0).ToList();
        }

        public List<managementFacturasanalistas_lotesResult> GetFacturaAnalistas(String usuario, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<managementFacturasanalistas_lotesResult> result = new List<managementFacturasanalistas_lotesResult>();
            result = db.managementFacturasanalistas_lotes(usuario).ToList();
            return result;
        }

        public List<managementFacturasanalistas_lotes_okResult> GetFacturaAnalistasok(int? idRol, string usuario, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            db.CommandTimeout = 3000;
            List<managementFacturasanalistas_lotes_okResult> result = new List<managementFacturasanalistas_lotes_okResult>();
            result = db.managementFacturasanalistas_lotes_ok(idRol, usuario).ToList();
            return result;
        }

        public List<Management_Lotes_totales_con_analistaResult> GetLotesAnalistaTotal(DateTime fecha_inicio, DateTime fecha_fin, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<Management_Lotes_totales_con_analistaResult> result = new List<Management_Lotes_totales_con_analistaResult>();
            result = db.Management_Lotes_totales_con_analista(fecha_inicio, fecha_fin).ToList();
            return result;

        }

        public List<Management_Lotes_totales_con_analistaDtllResult> GetLotesAnalistaTotalDtll(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<Management_Lotes_totales_con_analistaDtllResult> result = new List<Management_Lotes_totales_con_analistaDtllResult>();
            result = db.Management_Lotes_totales_con_analistaDtll(Id).ToList();
            return result;

        }
        public List<vw_prestadores_lotes> GetRecepcionFacturas2(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_prestadores_lotes.Where(l => l.count_facturas_pendientes == 0).ToList();
        }


        public List<managment_prestadores_facturasResult> GetFacturasByIdRecepcion(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            List<managment_prestadores_facturasResult> result = new List<managment_prestadores_facturasResult>();

            try
            {

                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    result = db.managment_prestadores_facturas(idrecepcion).ToList();
                }

                //    string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                //SqlConnection conn = new SqlConnection(conexion);
                //SqlCommand cmd = new SqlCommand();

                //cmd.Connection = conn;
                //cmd = conn.CreateCommand();
                //cmd.Connection.Open();
                //cmd.CommandText = "set arithabort on;";
                //cmd.ExecuteNonQuery();

                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@id_cargue", SqlDbType.Int);
                //param[0].Value = idrecepcion;

                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "managment_prestadores_facturas";
                //cmd.Connection = conn;
                //cmd.CommandTimeout = 3000;
                //cmd.Parameters.AddRange(param);

                //SqlDataReader reader = cmd.ExecuteReader();

                //DataTable dt = new DataTable();
                //dt.Load(reader);

                //foreach (DataRow row in dt.Rows)
                //{
                //    managment_prestadores_facturasResult obj = new managment_prestadores_facturasResult();
                //    obj.id_cargue_base = Convert.ToInt32(row["id_cargue_base"].ToString());
                //    obj.id_cargue_dtll = Convert.ToInt32(row["id_cargue_dtll"].ToString());
                //    if (!string.IsNullOrEmpty(row["idGestionDocumental"].ToString()))
                //    {
                //        obj.idGestionDocumental = Convert.ToInt32(row["idGestionDocumental"].ToString());
                //    }

                //    obj.codigo_prestador = row["codigo_prestador"].ToString();
                //    obj.nombre_prestador = row["nombre_prestador"].ToString();
                //    obj.num_id_prestador = row["num_id_prestador"].ToString();
                //    obj.num_factura = row["num_factura"].ToString();
                //    if (!string.IsNullOrEmpty(row["fecha_exp_factura"].ToString()))
                //        obj.fecha_exp_factura = Convert.ToDateTime(row["fecha_exp_factura"].ToString());
                //    if (!string.IsNullOrEmpty(row["fecha_inicio"].ToString()))
                //        obj.fecha_inicio = Convert.ToDateTime(row["fecha_inicio"].ToString());
                //    if (!string.IsNullOrEmpty(row["fecha_final"].ToString()))
                //        obj.fecha_final = Convert.ToDateTime(row["fecha_final"].ToString());
                //    if (!string.IsNullOrEmpty(row["valor_neto"].ToString()))
                //        obj.valor_neto = Convert.ToDecimal(row["valor_neto"].ToString());
                //    obj.estado_factura = Convert.ToInt32(row["estado_factura"].ToString());
                //    if (!string.IsNullOrEmpty(row["cargo_facturas"].ToString()))
                //        obj.cargo_facturas = Convert.ToInt32(row["cargo_facturas"].ToString());
                //    obj.factura_digital = row["factura_digital"].ToString();
                //    obj.count_soportes = Convert.ToInt32(row["count_soportes"].ToString());
                //    if (!string.IsNullOrEmpty(row["gestionada"].ToString()))
                //        obj.gestionada = Convert.ToBoolean(row["gestionada"].ToString());
                //    obj.observaciones = row["observaciones"].ToString();
                //    obj.indice = row["indice"].ToString();
                //    if (!string.IsNullOrEmpty(row["id_ref_regional"].ToString()))
                //        obj.id_ref_regional = Convert.ToInt32(row["id_ref_regional"].ToString());
                //    if (!string.IsNullOrEmpty(row["id_ref_ciudades"].ToString()))
                //        obj.id_ref_ciudades = Convert.ToInt32(row["id_ref_ciudades"].ToString());
                //    obj.ciudad = row["ciudad"].ToString();
                //    if (!string.IsNullOrEmpty(row["id_auditor_asignado"].ToString()))
                //        obj.id_auditor_asignado = Convert.ToInt32(row["id_auditor_asignado"].ToString());
                //    if (!string.IsNullOrEmpty(row["fecha_asignacion_auditor"].ToString()))
                //        obj.fecha_asignacion_auditor = Convert.ToDateTime(row["fecha_asignacion_auditor"].ToString());
                //    obj.nom_auditor = row["nom_auditor"].ToString();
                //    if (!string.IsNullOrEmpty(row["prestador"].ToString()))
                //        obj.prestador = Convert.ToInt32(row["prestador"].ToString());
                //    obj.factura_subsanada = row["factura_subsanada"].ToString();
                //    obj.subsanada_por = row["subsanada_por"].ToString();
                //    obj.count_soportes_zip = Convert.ToInt32(row["count_soportes_zip"].ToString());
                //    obj.factura_subsanada = row["factura_subsanada"].ToString();
                //    if (!string.IsNullOrEmpty(row["fecha_ultima_gestion"].ToString()))
                //        obj.fecha_ultima_gestion = Convert.ToDateTime(row["fecha_ultima_gestion"].ToString());
                //    if (!string.IsNullOrEmpty(row["dias_trascurridos"].ToString()))
                //        obj.dias_trascurridos = Convert.ToInt32(row["dias_trascurridos"].ToString());
                //    if (!string.IsNullOrEmpty(row["Id_auditor_automatico"].ToString()))
                //        obj.Id_auditor_automatico = Convert.ToInt32(row["Id_auditor_automatico"].ToString());

                //    if (!string.IsNullOrEmpty(row["id_af_factura_subsanada"].ToString()))
                //        obj.id_af_factura_subsanada = Convert.ToInt32(row["id_af_factura_subsanada"].ToString());
                //    result.Add(obj);
                //}

                return result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<managment_prestadores_facturas2Result> GetFacturasByIdRecepcion2(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            List<managment_prestadores_facturas2Result> result = new List<managment_prestadores_facturas2Result>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    result = db.managment_prestadores_facturas2(idrecepcion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }
        public List<ver_evaluacion_archivos> TraerArchivosVisitasDispensacion(int idEvaluacion)
        {
            List<ver_evaluacion_archivos> lista = new List<ver_evaluacion_archivos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ver_evaluacion_archivos.Where(x => x.id_evaluacion == idEvaluacion).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }
        public ver_evaluacion_pdfs TraerPDFEvaluacionVisitas(int idEvaluacion)
        {

            ver_evaluacion_pdfs lista = new ver_evaluacion_pdfs();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ver_evaluacion_pdfs.Where(x => x.id_evaluacion == idEvaluacion).OrderByDescending(x => x.id_pdf).FirstOrDefault();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_facturas_sinDocumentacionResult> ListaFacturasIncompletas()
        {
            List<management_facturas_sinDocumentacionResult> lista = new List<management_facturas_sinDocumentacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_facturas_sinDocumentacion().ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }


        public List<managmentprestadoresFacturas_analistasResult> prestadoresFacturas_analistas()
        {
            List<managmentprestadoresFacturas_analistasResult> lista = new List<managmentprestadoresFacturas_analistasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lista = db.managmentprestadoresFacturas_analistas().ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<managmentprestadoresFacturas_auditoresResult> prestadoresFacturas_auditores()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturas_auditoresResult> result = db.managmentprestadoresFacturas_auditores().ToList();
            return result;
        }


        public List<managment_prestadores_facturasResult> GetFactura(int idrecepcion, int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<managment_prestadores_facturasResult> result = new List<managment_prestadores_facturasResult>();
            result = db.managment_prestadores_facturas(idrecepcion).Where(l => l.id_cargue_dtll == iddetalle).ToList();
            return result;
        }

        public managment_prestadores_facturas_GDResult GetFacturaGD(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            managment_prestadores_facturas_GDResult result = new managment_prestadores_facturas_GDResult();
            result = db.managment_prestadores_facturas_GD(iddetalle).FirstOrDefault();
            return result;

        }

        public managment_prestadores_facturas_GD_zipResult GetFacturaGD2(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            managment_prestadores_facturas_GD_zipResult result = new managment_prestadores_facturas_GD_zipResult();
            result = db.managment_prestadores_facturas_GD_zip(iddetalle).FirstOrDefault();
            return result;

        }

        public List<managmentprestadoresfacturasestadoResult> GetFacturasByEstado(int idestado, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasestadoResult> result = new List<managmentprestadoresfacturasestadoResult>();
            result = db.managmentprestadoresfacturasestado(idestado).ToList();
            return result;
        }

        public List<managmentprestadoresfacturasaceptadasResult> GetFacturasAceptadas(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresfacturasaceptadasResult> result = new List<managmentprestadoresfacturasaceptadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 90000;
                    result = db.managmentprestadoresfacturasaceptadas(idestado, id_usuario).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public List<managmentprestadoresfacturasaceptadasOKResult> GetFacturasAceptadas2(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasaceptadasOKResult> result = new List<managmentprestadoresfacturasaceptadasOKResult>();
            result = db.managmentprestadoresfacturasaceptadasOK().ToList();
            return result;
        }

        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasgestionadas3Result> result = new List<managmentprestadoresfacturasgestionadas3Result>();
            try
            {
                string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd = conn.CreateCommand();
                cmd.Connection.Open();
                cmd.CommandText = "set arithabort on;";
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "managmentprestadoresfacturasgestionadas3";
                cmd.Connection = conn;
                cmd.CommandTimeout = 3600;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        managmentprestadoresfacturasgestionadas3Result obj = new managmentprestadoresfacturasgestionadas3Result();
                        obj.id_cargue_base = Convert.ToInt32(reader["id_cargue_base"]);
                        obj.id_cargue_dtll = Convert.ToInt32(reader["id_cargue_dtll"]);
                        if (!string.IsNullOrEmpty(reader["prestador"].ToString()))
                            obj.prestador = Convert.ToInt32(reader["prestador"]);
                        obj.nombre_prestador = reader["nombre_prestador"].ToString();
                        obj.num_id_prestador = reader["num_id_prestador"].ToString();
                        obj.num_factura = reader["num_factura"].ToString();
                        if (!string.IsNullOrEmpty(reader["valor_neto"].ToString()))
                            obj.valor_neto = Convert.ToDecimal(reader["valor_neto"]);
                        if (!string.IsNullOrEmpty(reader["estado_factura"].ToString()))
                            obj.estado_factura = Convert.ToInt32(reader["estado_factura"]);
                        if (!string.IsNullOrEmpty(reader["id_ref_ciudades"].ToString()))
                            obj.id_ref_ciudades = Convert.ToInt32(reader["id_ref_ciudades"]);
                        obj.ciudad = reader["ciudad"].ToString();
                        if (!string.IsNullOrEmpty(reader["id_ref_regional"].ToString()))
                            obj.id_ref_regional = Convert.ToInt32(reader["id_ref_regional"]);
                        obj.nombre_regional = reader["nombre_regional"].ToString();
                        if (!string.IsNullOrEmpty(reader["id_auditor_asignado"].ToString()))
                            obj.id_auditor_asignado = Convert.ToInt32(reader["id_auditor_asignado"]);
                        obj.nom_auditor = reader["nom_auditor"].ToString();
                        if (!string.IsNullOrEmpty(reader["id_analista_gestiona"].ToString()))
                            obj.id_analista_gestiona = Convert.ToInt32(reader["id_analista_gestiona"]);
                        obj.nom_analitica = reader["nom_analitica"].ToString();
                        obj.multiusuario = reader["multiusuario"].ToString();
                        obj.id_diagnostico = reader["id_diagnostico"].ToString();
                        obj.diagnostico = reader["diagnostico"].ToString();
                        if (!string.IsNullOrEmpty(reader["fecha_recepcion_fac"].ToString()))
                            obj.fecha_recepcion_fac = Convert.ToDateTime(reader["fecha_recepcion_fac"]);
                        if (!string.IsNullOrEmpty(reader["fecha_aprobacion"].ToString()))
                            obj.fecha_aprobacion = Convert.ToDateTime(reader["fecha_aprobacion"]);
                        obj.estado_des = reader["estado_des"].ToString();
                        obj.count_soportes = Convert.ToInt32(reader["count_soportes"]);
                        obj.count_soportes_zip = Convert.ToInt32(reader["count_soportes_zip"]);
                        if (!string.IsNullOrEmpty(reader["fecha_exp_factura"].ToString()))
                            obj.fecha_exp_factura = Convert.ToDateTime(reader["fecha_exp_factura"]);
                        obj.cod_sap = reader["cod_sap"].ToString();
                        obj.tipoGastos = reader["tipoGastos"].ToString();
                        obj.ruta = reader["ruta"].ToString();
                        obj.valorGlosa = reader["valorGlosa"].ToString();
                        obj.MotivosGlosa = reader["MotivosGlosa"].ToString();
                        obj.Observaciones = reader["Observaciones"].ToString();
                        obj.editable = reader["editable"].ToString();
                        obj.motivos_rechazo = reader["motivos_rechazo"].ToString();
                        obj.estado_aprobada = Convert.ToInt32(reader["estado_aprobada"]);
                        if (!string.IsNullOrEmpty(reader["id_factura_nueva"].ToString()))
                            obj.id_factura_nueva = Convert.ToInt32(reader["id_factura_nueva"]);
                        if (!string.IsNullOrEmpty(reader["Fecha_nueva_recepción_nueva"].ToString()))
                            obj.Fecha_nueva_recepción_nueva = Convert.ToDateTime(reader["Fecha_nueva_recepción_nueva"]);
                        obj.Numero_factura_nueva = reader["Numero_factura_nueva"].ToString();
                        if (!string.IsNullOrEmpty(reader["valor_factura_nueva"].ToString()))
                            obj.valor_factura_nueva = Convert.ToDecimal(reader["valor_factura_nueva"]);
                        if (!string.IsNullOrEmpty(reader["estado_fac_nueva"].ToString()))
                            obj.estado_fac_nueva = Convert.ToInt32(reader["estado_fac_nueva"]);
                        obj.homologacion_nit = reader["homologacion_nit"].ToString();
                        obj.homologacion_sap = reader["homologacion_sap"].ToString();
                        obj.homologacion_razonSocial = reader["homologacion_razonSocial"].ToString();
                        obj.numero_contrato = reader["numero_contrato"].ToString();
                     
                        result.Add(obj);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }

        }


        public List<managmentprestadoresfacturasgestionadas3_numFacturaResult> BuscarFactura_numFactura(string numFactura)
        {
            List<managmentprestadoresfacturasgestionadas3_numFacturaResult> lista = new List<managmentprestadoresfacturasgestionadas3_numFacturaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.managmentprestadoresfacturasgestionadas3_numFactura(numFactura).ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;

        }

        public List<managmentprestadoresfacturasgestionadasFechasResult> GetGestionFacturasFechas(DateTime fechaIni, DateTime fechaFin)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasgestionadasFechasResult> result = new List<managmentprestadoresfacturasgestionadasFechasResult>();
            result = db.managmentprestadoresfacturasgestionadasFechas(fechaIni, fechaFin).ToList();
            return result;
        }
        /*Obtener los datos del procedimiento otra forma*/
        public List<managmentprestadoresfacturasgestionadasCompletaResult> GetGestionFacturasv3(String numFac, String nit, String prestador, String sap, int? estado, int? idDetalle)
        {
            List<managmentprestadoresfacturasgestionadasCompletaResult> lista = new List<managmentprestadoresfacturasgestionadasCompletaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lista = db.managmentprestadoresfacturasgestionadasCompleta(numFac, nit, prestador, sap, estado, idDetalle).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }

        }

        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturasv2(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            List<managmentprestadoresfacturasgestionadas3Result> lista = new List<managmentprestadoresfacturasgestionadas3Result>();

            try
            {
                if (!string.IsNullOrEmpty(estado) || regional != null || !string.IsNullOrEmpty(prestador) || !string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(numFac))
                {
                    string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(conexion);
                    conn.Open();

                    string query = " Select * from [vw_prestadoresfacturasgestionadas2023] where 0 = 0 ";

                    if (idDetalle != null)
                        query += " and id_cargue_dtll = @idDetalle ";

                    if (!string.IsNullOrEmpty(estado))
                        query += " and estado_factura = @estadofactura ";

                    if (regional != null)
                        query += " and id_ref_regional = @regional ";

                    if (!string.IsNullOrEmpty(prestador))
                        query += " and (nombre_prestador like '%' + @prestador + '%' or homologacion_razonSocial like '%'+ @prestador + '%')";

                    if (!string.IsNullOrEmpty(nit))
                        query += " and num_id_prestador = @nit or homologacion_nit = @nit";

                    if (!string.IsNullOrEmpty(numFac))
                        query += " and num_factura LIKE '%' + @numFac + '%'";

                    query += " order by id_cargue_base desc";

                    SqlCommand command = new SqlCommand(query, conn);

                    if (idDetalle != null)
                    {
                        command.Parameters.AddWithValue("@idDetalle", SqlDbType.Int).Value = idDetalle;
                    }

                    if (!string.IsNullOrEmpty(estado))
                    {
                        command.Parameters.Add("@estadofactura", SqlDbType.Int).Value = estado;
                    }

                    if (regional != null)
                    {
                        command.Parameters.AddWithValue("@regional", SqlDbType.Int).Value = regional;
                    }

                    if (!string.IsNullOrEmpty(prestador))
                        command.Parameters.AddWithValue("@prestador", SqlDbType.VarChar).Value = prestador;

                    if (!string.IsNullOrEmpty(nit))
                        command.Parameters.AddWithValue("@nit", SqlDbType.VarChar).Value = nit;

                    if (!string.IsNullOrEmpty(numFac))
                        command.Parameters.AddWithValue("@numFac", SqlDbType.VarChar).Value = numFac;

                    command.CommandTimeout = 3000;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        managmentprestadoresfacturasgestionadas3Result obj2 = new managmentprestadoresfacturasgestionadas3Result();
                        obj2.id_cargue_base = Convert.ToInt32(row["id_cargue_base"].ToString());
                        obj2.id_cargue_dtll = Convert.ToInt32(row["id_cargue_dtll"].ToString());
                        obj2.prestador = Convert.ToInt32(row["prestador"].ToString());
                        obj2.nombre_prestador = row["nombre_prestador"].ToString();
                        obj2.num_id_prestador = row["num_id_prestador"].ToString();
                        obj2.num_factura = row["num_factura"].ToString();
                        obj2.valor_neto = Convert.ToDecimal(row["valor_neto"].ToString());
                        if (!string.IsNullOrEmpty(row["estado_factura"].ToString()))
                        {
                            obj2.estado_factura = Convert.ToInt32(row["estado_factura"].ToString());
                        }
                        if (!string.IsNullOrEmpty(row["id_ref_ciudades"].ToString()))
                        {
                            obj2.id_ref_ciudades = Convert.ToInt32(row["id_ref_ciudades"].ToString());
                        }

                        //obj2.id_ref_ciudades = Convert.ToInt32(row["id_ref_ciudades"].ToString());
                        obj2.ciudad = row["ciudad"].ToString();
                        if (!string.IsNullOrEmpty(row["id_ref_regional"].ToString()))
                        {
                            obj2.id_ref_regional = Convert.ToInt32(row["id_ref_regional"].ToString());
                        }
                        obj2.nombre_regional = row["nombre_regional"].ToString();
                        if (!string.IsNullOrEmpty(row["id_auditor_asignado"].ToString()))
                        {
                            obj2.id_auditor_asignado = Convert.ToInt32(row["id_auditor_asignado"].ToString());
                        }
                        obj2.nom_auditor = row["nom_auditor"].ToString();
                        if (!string.IsNullOrEmpty(row["id_analista_gestiona"].ToString()))
                        {
                            obj2.id_analista_gestiona = Convert.ToInt32(row["id_analista_gestiona"].ToString());
                        }
                        obj2.nom_analitica = row["nom_analitica"].ToString();
                        obj2.multiusuario = row["multiusuario"].ToString();

                        obj2.id_diagnostico = row["id_diagnostico"].ToString();
                        obj2.diagnostico = row["diagnostico"].ToString();
                        obj2.fecha_recepcion_fac = Convert.ToDateTime(row["fecha_recepcion_fac"].ToString());
                        if (!string.IsNullOrEmpty(row["fecha_aprobacion"].ToString()))
                        {
                            obj2.fecha_aprobacion = Convert.ToDateTime(row["fecha_aprobacion"].ToString());
                        }
                        obj2.estado_des = row["estado_des"].ToString();
                        obj2.count_soportes = Convert.ToInt32(row["count_soportes"].ToString());
                        obj2.count_soportes_zip = Convert.ToInt32(row["count_soportes_zip"].ToString());
                        if (!string.IsNullOrEmpty(row["fecha_exp_factura"].ToString()))
                        {
                            obj2.fecha_exp_factura = Convert.ToDateTime(row["fecha_exp_factura"].ToString());
                        }
                        obj2.cod_sap = row["cod_sap"].ToString();
                        obj2.tipoGastos = row["tipoGastos"].ToString();
                        obj2.ruta = row["ruta"].ToString();
                        obj2.valorGlosa = row["valorGlosa"].ToString();
                        obj2.MotivosGlosa = row["MotivosGlosa"].ToString();
                        obj2.Observaciones = row["Observaciones"].ToString();
                        obj2.editable = row["editable"].ToString();
                        obj2.motivos_rechazo = row["motivos_rechazo"].ToString();
                        obj2.estado_aprobada = Convert.ToInt32(row["estado_aprobada"].ToString());
                        if (!string.IsNullOrEmpty(row["id_factura_nueva"].ToString()))
                        {
                            obj2.id_factura_nueva = Convert.ToInt32(row["id_factura_nueva"].ToString());
                        }
                        if (!string.IsNullOrEmpty(row["Fecha_nueva_recepción_nueva"].ToString()))
                        {
                            obj2.Fecha_nueva_recepción_nueva = Convert.ToDateTime(row["Fecha_nueva_recepción_nueva"].ToString());
                        }
                        obj2.Numero_factura_nueva = row["Numero_factura_nueva"].ToString();
                        if (!string.IsNullOrEmpty(row["valor_factura_nueva"].ToString()))
                        {
                            obj2.valor_factura_nueva = Convert.ToDecimal(row["valor_factura_nueva"].ToString());
                        }
                        if (!string.IsNullOrEmpty(row["estado_fac_nueva"].ToString()))
                        {
                            obj2.estado_fac_nueva = Convert.ToInt32(row["estado_fac_nueva"].ToString());
                        }
                        obj2.homologacion_nit = row["homologacion_nit"].ToString();
                        obj2.homologacion_sap = row["homologacion_sap"].ToString();
                        obj2.homologacion_razonSocial = row["homologacion_razonSocial"].ToString();
                        obj2.numero_contrato = row["numero_contrato"].ToString();

                        lista.Add(obj2);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }



        public List<managmentprestadoresfacturasgestionadas2Result> GetGestionFacturasV3Filtrada(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac, string rol, int? idUsuario)
        {
            List<managmentprestadoresfacturasgestionadas2Result> lista = new List<managmentprestadoresfacturasgestionadas2Result>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.managmentprestadoresfacturasgestionadas2(idDetalle, fechainicial, fechafinal, estado, regional, prestador, nit, numFac, rol, idUsuario).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidad()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasgestionadasTrazabilidadResult> result = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();
            db.CommandTimeout = 3000;
            result = db.managmentprestadoresfacturasgestionadasTrazabilidad().ToList();
            return result;
        }



        /*Obtener tablero de facturas gestionadas trazabilidad*/
        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidadV2(DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            List<managmentprestadoresfacturasgestionadasTrazabilidadResult> lista = new List<managmentprestadoresfacturasgestionadasTrazabilidadResult>();

            try
            {
                if (!string.IsNullOrEmpty(estado) || regional != null || !string.IsNullOrEmpty(prestador) || !string.IsNullOrEmpty(nit) || !string.IsNullOrEmpty(numFac))
                {
                    string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(conexion);
                    conn.Open();

                    string query = "Select * from [vw_de_prueba_completa] where 0 = 0 ";

                    if (!string.IsNullOrEmpty(estado))
                        query += "and estado_factura = @estadofactura ";

                    if (regional != null)
                        query += "and id_ref_regional = @regional ";

                    if (!string.IsNullOrEmpty(prestador))
                        query += "and nombre_prestador like '%@prestador%' or homologacion_razonSocial like '%@prestador%'";

                    if (!string.IsNullOrEmpty(nit))
                        query += "and num_id_prestador = @nit or homologacion_nit = @nit ";

                    if (!string.IsNullOrEmpty(numFac))
                        query += "and num_factura LIKE '%' + @numFac + '%'";

                    if (fechainicial != null)
                        query += "and fecha_recepcion_fac >= @fechaInicial ";

                    if (fechafinal != null)
                        query += "and fecha_recepcion_fac <= @fechaFinal ";

                    query += "order by id_cargue_dtll desc ";

                    SqlCommand command = new SqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(estado))
                    {
                        command.Parameters.Add("@estadofactura", SqlDbType.Int).Value = estado;
                    }

                    if (regional != null)
                    {
                        command.Parameters.AddWithValue("@regional", SqlDbType.Int).Value = regional;
                    }

                    if (!string.IsNullOrEmpty(prestador))
                        command.Parameters.AddWithValue("@prestador", SqlDbType.VarChar).Value = prestador;

                    if (!string.IsNullOrEmpty(nit))
                        command.Parameters.AddWithValue("@nit", SqlDbType.VarChar).Value = nit;

                    if (!string.IsNullOrEmpty(numFac))
                        command.Parameters.AddWithValue("@numFac", SqlDbType.VarChar).Value = numFac;

                    if (fechainicial != null)
                    {
                        command.Parameters.AddWithValue("@fechaInicial", SqlDbType.DateTime).Value = fechainicial;
                    }
                    if (fechafinal != null)
                    {
                        command.Parameters.AddWithValue("@fechaFinal", SqlDbType.DateTime).Value = fechafinal;
                    }

                    command.CommandTimeout = 3000;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        managmentprestadoresfacturasgestionadasTrazabilidadResult obj2 = new managmentprestadoresfacturasgestionadasTrazabilidadResult();
                        obj2.id_cargue_base = Convert.ToInt32(row["id_cargue_base"].ToString());

                        obj2.id_cargue_dtll = Convert.ToInt32(row["id_cargue_dtll"].ToString());

                        obj2.prestador = Convert.ToInt32(row["prestador"].ToString());

                        obj2.nombre_prestador = row["nombre_prestador"].ToString();

                        obj2.num_id_prestador = row["num_id_prestador"].ToString();

                        obj2.num_factura = row["num_factura"].ToString();

                        obj2.valor_neto = Convert.ToDecimal(row["valor_neto"].ToString());

                        /*null*/
                        if (!string.IsNullOrEmpty(row["estado_factura"].ToString()))
                            obj2.estado_factura = Convert.ToInt32(row["estado_factura"].ToString());

                        obj2.id_ref_ciudades = Convert.ToInt32(row["id_ref_ciudades"].ToString());

                        obj2.ciudad = row["ciudad"].ToString();
                        /*null*/
                        if (!string.IsNullOrEmpty(row["id_ref_regional"].ToString()))
                            obj2.id_ref_regional = Convert.ToInt32(row["id_ref_regional"].ToString());

                        obj2.nombre_regional = row["nombre_regional"].ToString();
                        /*null*/
                        if (!string.IsNullOrEmpty(row["id_auditor_asignado"].ToString()))
                            obj2.id_auditor_asignado = Convert.ToInt32(row["id_auditor_asignado"].ToString());

                        obj2.nom_auditor = row["nom_auditor"].ToString();
                        /*null*/
                        if (!string.IsNullOrEmpty(row["id_analista_gestiona"].ToString()))
                            obj2.id_analista_gestiona = Convert.ToInt32(row["id_analista_gestiona"].ToString());

                        obj2.nom_analitica = row["nom_analitica"].ToString();

                        if (!string.IsNullOrEmpty(row["fecha_asignacion_analista"].ToString()))
                            obj2.fecha_asignacion_analista = Convert.ToDateTime(row["fecha_asignacion_analista"].ToString());

                        obj2.multiusuario = row["multiusuario"].ToString();

                        obj2.id_diagnostico = row["id_diagnostico"].ToString();
                        obj2.diagnostico = row["diagnostico"].ToString();

                        if (!string.IsNullOrEmpty(row["fecha_recepcion_fac"].ToString()))
                            obj2.fecha_recepcion_fac = Convert.ToDateTime(row["fecha_recepcion_fac"].ToString());
                        /*null*/
                        if (!string.IsNullOrEmpty(row["fecha_aprobacion"].ToString()))
                            obj2.fecha_aprobacion = Convert.ToDateTime(row["fecha_aprobacion"].ToString());
                        obj2.estado_des = row["estado_des"].ToString();
                        obj2.count_soportes = Convert.ToInt32(row["count_soportes"].ToString());
                        obj2.count_soportes_zip = Convert.ToInt32(row["count_soportes_zip"].ToString());
                        /*null*/
                        if (!string.IsNullOrEmpty(row["fecha_exp_factura"].ToString()))
                            obj2.fecha_exp_factura = Convert.ToDateTime(row["fecha_exp_factura"].ToString());
                        obj2.tipoGastos = row["tipoGastos"].ToString();
                        obj2.ruta = row["ruta"].ToString();
                        obj2.valorGlosa = row["valorGlosa"].ToString();
                        obj2.MotivosGlosa = row["MotivosGlosa"].ToString();
                        obj2.Observaciones = row["Observaciones"].ToString();
                        obj2.editable = row["editable"].ToString();
                        obj2.motivos_rechazo = row["motivos_rechazo"].ToString();
                        obj2.estado_aprobada = Convert.ToInt32(row["estado_aprobada"].ToString());
                        if (!string.IsNullOrEmpty(row["dias_procesamiento"].ToString()))
                            obj2.dias_procesamiento = Convert.ToInt32(row["dias_procesamiento"].ToString());
                        obj2.contabilizado_factura = row["contabilizado_factura"].ToString();
                        obj2.contabilizado_documento = row["contabilizado_documento"].ToString();
                        if (!string.IsNullOrEmpty(row["contabilizado_fecha"].ToString()))
                            obj2.contabilizado_fecha = Convert.ToDateTime(row["contabilizado_fecha"].ToString());
                        if (!string.IsNullOrEmpty(row["fecha_asignacion_auditor"].ToString()))
                            obj2.fecha_asignacion_auditor = Convert.ToDateTime(row["fecha_asignacion_auditor"].ToString());
                        obj2.cod_sap = row["cod_sap"].ToString();
                        obj2.nit_prestador_suis = row["nit_prestador_suis"].ToString();
                        obj2.nombre_prestador_suis = row["nombre_prestador_suis"].ToString();
                        if (!string.IsNullOrEmpty(row["estado_fac_nueva"].ToString()))
                            obj2.estado_fac_nueva = Convert.ToInt32(row["estado_fac_nueva"].ToString());
                        obj2.homologacion_nit = row["homologacion_nit"].ToString();
                        obj2.homologacion_sap = row["homologacion_sap"].ToString();
                        obj2.homologacion_razonSocial = row["homologacion_razonSocial"].ToString();

                        lista.Add(obj2);
                    }
                }


            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;

        }

        public List<managmentprestadores_estados_factura_totalResult> GetTotalFacturas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadores_estados_factura_totalResult> result = new List<managmentprestadores_estados_factura_totalResult>();
            db.CommandTimeout = 3600;
            result = db.managmentprestadores_estados_factura_total().ToList();
            return result;
        }

        public List<view_ref_estado_facturas> GetEstadoFacturas()
        {
            List<view_ref_estado_facturas> lista = new List<view_ref_estado_facturas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.view_ref_estado_facturas.ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<vw_analistas_recepcion> GetListAnalistas()
        {
            List<vw_analistas_recepcion> lista = new List<vw_analistas_recepcion>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.vw_analistas_recepcion.ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public vw_analistas_recepcion TraerAnalista(int idUsuario)
        {
            vw_analistas_recepcion obj = new vw_analistas_recepcion();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj = db.vw_analistas_recepcion.Where(x => x.id_usuario == idUsuario).FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return obj;
            }
        }

        public List<managmentprestadoresFacturasResult> GetFacturasByEstadoAceptadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresFacturasResult> result = new List<managmentprestadoresFacturasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 1000;
                    result = db.managmentprestadoresFacturas(idestado).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }


        public List<managmentprestadoresFacturas_devueltasResult> GetFacturasByEstadoDevueltas(int idestado, int? id, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresFacturas_devueltasResult> result = new List<managmentprestadoresFacturas_devueltasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 1000;
                    result = db.managmentprestadoresFacturas_devueltas(idestado, id).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public List<managmentprestadoresFacturas_rangoResult> GetFacturasByEstadoAceptadasRango(int rango, Int32 id_regional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturas_rangoResult> result = new List<managmentprestadoresFacturas_rangoResult>();
            result = db.managmentprestadoresFacturas_rango().Where(l => l.RangoDias == rango && l.id_ref_regional == id_regional).ToList();
            return result;
        }

        public List<managmentprestadoresFacturasAuditorResult> GetFacturasByAuditor(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturasAuditorResult> result = new List<managmentprestadoresFacturasAuditorResult>();
            result = db.managmentprestadoresFacturasAuditor(idestado, id_usuario).ToList();
            return result;
        }

        public List<managmentprestadoresFacturasAuditorOKResult> GetFacturasByAuditor2(ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturasAuditorOKResult> result = new List<managmentprestadoresFacturasAuditorOKResult>();
            result = db.managmentprestadoresFacturasAuditorOK().ToList();
            return result;
        }

        public List<managmentprestadoresFacturasAuditorOKCompletaResult> GetFacturasByAuditor3(int idAuditor)
        {
            List<managmentprestadoresFacturasAuditorOKCompletaResult> lista = new List<managmentprestadoresFacturasAuditorOKCompletaResult>();
            try
            {
                string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd = conn.CreateCommand();
                cmd.Connection.Open();
                cmd.CommandText = "set arithabort on;";
                cmd.ExecuteNonQuery();

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@idAuditor", SqlDbType.Int);
                param[0].Value = idAuditor;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "managmentprestadoresFacturasAuditorOKCompleta";
                cmd.Connection = conn;
                cmd.CommandTimeout = 3000;
                cmd.Parameters.AddRange(param);

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                List<managmentprestadoresFacturasAuditorOKCompletaResult> Result = new List<managmentprestadoresFacturasAuditorOKCompletaResult>();
                foreach (DataRow row in dt.Rows)
                {
                    managmentprestadoresFacturasAuditorOKCompletaResult obj = new managmentprestadoresFacturasAuditorOKCompletaResult();
                    obj.id_cargue_base = Convert.ToInt32(row["id_cargue_base"].ToString());
                    obj.id_cargue_dtll = Convert.ToInt32(row["id_cargue_dtll"].ToString());
                    //if (!string.IsNullOrEmpty(row["prestador"].ToString()))
                    //    obj.prestador = Convert.ToInt32(row["prestador"].ToString());


                    obj.nombre_prestador = row["nombre_prestador"].ToString();
                    obj.num_id_prestador = row["num_id_prestador"].ToString();
                    obj.num_factura = row["num_factura"].ToString();
                    if (!string.IsNullOrEmpty(row["valor_neto"].ToString()))
                        obj.valor_neto = Convert.ToDecimal(row["valor_neto"].ToString());
                    if (!string.IsNullOrEmpty(row["estado_factura"].ToString()))
                        obj.estado_factura = Convert.ToInt32(row["estado_factura"].ToString());
                    if (!string.IsNullOrEmpty(row["id_ref_ciudades"].ToString()))
                        obj.id_ref_ciudades = Convert.ToInt32(row["id_ref_ciudades"].ToString());
                    obj.ciudad = row["ciudad"].ToString();
                    if (!string.IsNullOrEmpty(row["id_ref_regional"].ToString()))
                        obj.id_ref_regional = Convert.ToInt32(row["id_ref_regional"].ToString());
                    obj.nombre_regional = row["nombre_regional"].ToString();
                    if (!string.IsNullOrEmpty(row["id_auditor_asignado"].ToString()))
                        obj.id_auditor_asignado = Convert.ToInt32(row["id_auditor_asignado"].ToString());
                    obj.nom_auditor = row["nom_auditor"].ToString();
                    if (!string.IsNullOrEmpty(row["id_analista_gestiona"].ToString()))
                        obj.id_analista_gestiona = Convert.ToInt32(row["id_analista_gestiona"].ToString());
                    obj.nom_analitica = row["nom_analitica"].ToString();
                    obj.estado_des = row["estado_des"].ToString();
                    obj.count_soportes = Convert.ToInt32(row["count_soportes"].ToString());
                    obj.count_soportes_zip = Convert.ToInt32(row["count_soportes_zip"].ToString());
                    if (!string.IsNullOrEmpty(row["fecha_exp_factura"].ToString()))
                        obj.fecha_exp_factura = Convert.ToDateTime(row["fecha_exp_factura"].ToString());
                    obj.ruta = row["ruta"].ToString();
                    if (!string.IsNullOrEmpty(row["fecha_ultima_gestion"].ToString()))
                        obj.fecha_ultima_gestion = Convert.ToDateTime(row["fecha_ultima_gestion"].ToString());
                    if (!string.IsNullOrEmpty(row["dias_trascurridos"].ToString()))
                        obj.dias_trascurridos = Convert.ToInt32(row["dias_trascurridos"].ToString());

                    if (!string.IsNullOrEmpty(row["version"].ToString()))
                    {
                        obj.version = Convert.ToInt32(row["version"]);
                    }
                    Result.Add(obj);
                }

                return Result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<managmentprestadoresFacturasAuditorEnSubsanacionResult> GetFacturasByAuditorEnSubsanacion(int idAuditor)
        {
            List<managmentprestadoresFacturasAuditorEnSubsanacionResult> lista = new List<managmentprestadoresFacturasAuditorEnSubsanacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.managmentprestadoresFacturasAuditorEnSubsanacion(idAuditor).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<managmentprestadoresFacturasAprobadasResult> GetFacturasAprobadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturasAprobadasResult> result = new List<managmentprestadoresFacturasAprobadasResult>();
            result = db.managmentprestadoresFacturasAprobadas(idestado).ToList();
            return result;
        }

        public List<managmentprestadoresFacturasReporteResult> GetFacturasByEstadoReporte(int idestado, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresFacturasReporteResult> result = new List<managmentprestadoresFacturasReporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managmentprestadoresFacturasReporte(idestado).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }
        }


        public List<managmentprestadoresFacturasReporte_fisResult> GetFacturasByEstadoReporte_fis(int idestado, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresFacturasReporte_fisResult> result = new List<managmentprestadoresFacturasReporte_fisResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managmentprestadoresFacturasReporte_fis(idestado).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }
        }
        public List<managmentRechazoFacturasReporteResult> GetFacturasByRechazoReporte(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            List<managmentRechazoFacturasReporteResult> result = new List<managmentRechazoFacturasReporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managmentRechazoFacturasReporte(id_dtll).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }

        }

        public List<managmentRechazoLoteFacturasReporteResult> GetLoteFacturasByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            List<managmentRechazoLoteFacturasReporteResult> result = new List<managmentRechazoLoteFacturasReporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managmentRechazoLoteFacturasReporte(id_lote).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }
        }


        public List<managmentRechazoLoteDtllFacturasReporteResult> GetLoteFacturasdtllByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            List<managmentRechazoLoteDtllFacturasReporteResult> result = new List<managmentRechazoLoteDtllFacturasReporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managmentRechazoLoteDtllFacturasReporte(id_lote).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }
        }


        public List<managment_prestadores_soportes_clinicosResult> GetSoportesClinicosList(int idcargue, int detalle)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managment_prestadores_soportes_clinicosResult> result = new List<managment_prestadores_soportes_clinicosResult>();
            result = db.managment_prestadores_soportes_clinicos(idcargue, detalle).ToList();
            return result;
        }

        public List<managment_prestadores_documentosResult> GetSoportesList(int detalle)
        {

            List<managment_prestadores_documentosResult> lista = new List<managment_prestadores_documentosResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.managment_prestadores_documentos(detalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;

            //ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            //List<managment_prestadores_documentosResult> result = new List<managment_prestadores_documentosResult>();
            //result = db.managment_prestadores_documentos(detalle).ToList();
            //return result;
        }

        public List<managment_ffmm_documentosResult> GetSoportesListFFMM(int detalle)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managment_ffmm_documentosResult> result = new List<managment_ffmm_documentosResult>();
            result = db.managment_ffmm_documentos(detalle).ToList();
            return result;
        }

        public management_prestadores_get_soporteResult Getsoporteclinico(int idsoportee)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            management_prestadores_get_soporteResult result = new management_prestadores_get_soporteResult();
            result = db.management_prestadores_get_soporte(idsoportee).FirstOrDefault();
            return result;
        }

        public List<ref_rechazos_Fac> Getref_rechazos_Fac(ref MessageResponseOBJ MsgRes)
        {
            List<ref_rechazos_Fac> result = new List<ref_rechazos_Fac>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_rechazos_Fac.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<vw_auditores_totales> GetAuditorTotales(ref MessageResponseOBJ MsgRes)
        {
            List<vw_auditores_totales> result = new List<vw_auditores_totales>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.vw_auditores_totales.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<vw_auditores_totales_pqrs> GetAuditorTotalesPqrs(ref MessageResponseOBJ MsgRes)
        {
            List<vw_auditores_totales_pqrs> result = new List<vw_auditores_totales_pqrs>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.vw_auditores_totales_pqrs.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public vw_auditores_totales GetAuditorNombre(string nombre)
        {
            vw_auditores_totales dato = new vw_auditores_totales();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.vw_auditores_totales.Where(x => x.nombre.ToUpper().Contains(nombre.ToUpper())).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public vw_auditores_totales GetAuditorId(int? id)
        {
            vw_auditores_totales dato = new vw_auditores_totales();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.vw_auditores_totales.Where(x => x.id_usuario == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public vw_auditores_totales_pqrs GetAuditorNombrePqrs(string nombre)
        {
            vw_auditores_totales_pqrs dato = new vw_auditores_totales_pqrs();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.vw_auditores_totales_pqrs.Where(x => x.nombre.ToUpper().Contains(nombre.ToUpper())).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<ref_tipo_gasto_facturas> Getref_tipo_gasto_facturas(ref MessageResponseOBJ MsgRes)
        {
            List<ref_tipo_gasto_facturas> result = new List<ref_tipo_gasto_facturas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_tipo_gasto_facturas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                return result;
            }
        }

        public List<managment_prestadores_list_rechazosResult> GetFacturasByRechazoList(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            List<managment_prestadores_list_rechazosResult> result = new List<managment_prestadores_list_rechazosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.managment_prestadores_list_rechazos(id_dtll).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return result;
            }

        }

        public List<ManagmentDetalleFacturasDevueltasResult> GetDetalleFacturadevuletas(int id_detalle)
        {
            List<ManagmentDetalleFacturasDevueltasResult> result = new List<ManagmentDetalleFacturasDevueltasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ManagmentDetalleFacturasDevueltas(id_detalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public List<ManagmentDetalleFacturasDevueltas_fisResult> GetDetalleFacturadevuletas_FIS(int id_detalle)
        {
            List<ManagmentDetalleFacturasDevueltas_fisResult> result = new List<ManagmentDetalleFacturasDevueltas_fisResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ManagmentDetalleFacturasDevueltas_fis(id_detalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public ecop_firma_digital_cod_barras GetDtll_codBarras(Int32? idDetalle)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ecop_firma_digital_cod_barras.Where(l => l.id_firma_digital == idDetalle).FirstOrDefault();
        }


        public List<management_ecop_firma_digital_sami_todoResult> ListadoFirmasSinRuta()
        {
            List<management_ecop_firma_digital_sami_todoResult> lista = new List<management_ecop_firma_digital_sami_todoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_ecop_firma_digital_sami_todo().ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public ecop_firma_digital_sami traerDatosFirma(int? idUsuario)
        {
            ecop_firma_digital_sami dato = new ecop_firma_digital_sami();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_firma_digital_sami.Where(x => x.id_usuario == idUsuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public Management_consulta_QRResult GetConsultaQr(Int32? idDetalle)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            Management_consulta_QRResult result = new Management_consulta_QRResult();
            result = db.Management_consulta_QR(idDetalle).FirstOrDefault();

            return result;
        }

        public List<managmentprestadoresFacturasOBSResult> GetConsultaObsFactura(Int32? id_af)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresFacturasOBSResult> result = new List<managmentprestadoresFacturasOBSResult>();

            result = db.managmentprestadoresFacturasOBS(id_af).ToList();

            return result;
        }

        public List<managmentprestadoresfacturasDEV_RECHResult> GetConsultaRechDevFactura()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasDEV_RECHResult> result = new List<managmentprestadoresfacturasDEV_RECHResult>();

            result = db.managmentprestadoresfacturasDEV_RECH().ToList();

            return result;
        }

        public List<managmentprestadoresfacturasDEV_RECHV2Result> GetConsultaRechDevFacturaV2(int? idfactura)
        {
            List<managmentprestadoresfacturasDEV_RECHV2Result> result = new List<managmentprestadoresfacturasDEV_RECHV2Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    result = db.managmentprestadoresfacturasDEV_RECHV2(idfactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public List<getfacturabynumfactura_idprestadorResult> ValidarEvistenciaFactura(int idfactura, string numnuevofactura, string numidprestador)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<getfacturabynumfactura_idprestadorResult> result = new List<getfacturabynumfactura_idprestadorResult>();

            result = db.getfacturabynumfactura_idprestador(numnuevofactura, numidprestador).ToList();
            var obj = result.Where(l => l.id_af == idfactura).FirstOrDefault();
            if (obj != null)
            {
                result.Remove(obj);
            }

            return result;
        }

        public List<ecop_gestion_factura_digital> GetConsultaGestionFactura(Int32? idDetalle)
        {
            List<ecop_gestion_factura_digital> ListResult = new List<ecop_gestion_factura_digital>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ecop_gestion_factura_digital.Where(l => l.id_cargue_dtll == idDetalle).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<factura_devolucion> GetConsultaGestionDevolucion(Int32? idDetalle)
        {
            List<factura_devolucion> ListResult = new List<factura_devolucion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.factura_devolucion.Where(l => l.id_af == idDetalle).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<managmentprestadoresfacturasACEP_ASIGResult> GetConsultaAcep_AsigFactura()
        {
            List<managmentprestadoresfacturasACEP_ASIGResult> result = new List<managmentprestadoresfacturasACEP_ASIGResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    result = db.managmentprestadoresfacturasACEP_ASIG().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public List<managmentprestadoresNumeroFacturaResult> GetConsultaNumeroFactura(String numeroFac)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresNumeroFacturaResult> result = new List<managmentprestadoresNumeroFacturaResult>();

            result = db.managmentprestadoresNumeroFactura(numeroFac).ToList();

            return result;
        }

        public List<factura_devolucion> GetfacturadevolucionByIdFactura(int idfactura)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            return db.factura_devolucion.Where(l => l.id_af == idfactura).ToList();
        }

        public List<management_reportelotecontabilizadoResult> ConsultaReporteLote(Int32 ID)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<management_reportelotecontabilizadoResult> result = new List<management_reportelotecontabilizadoResult>();

            result = db.management_reportelotecontabilizado(ID).ToList();

            return result;
        }


        public List<management_prestadores_regionalResult> GetPrestadoresRegional(int idRegional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<management_prestadores_regionalResult> result = new List<management_prestadores_regionalResult>();
            result = db.management_prestadores_regional(idRegional).ToList();
            return result;
        }

        public List<vw_ref_estado_factura_total_rango> GetRecepcionFacturasRango(Int32 opc)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            db.CommandTimeout = 3600;
            return db.vw_ref_estado_factura_total_rango.Where(l => l.RangoDias == opc).ToList();
        }

        public List<vw_factura_digital_gasto_total> GetGatosFactura(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_factura_digital_gasto_total.Where(l => l.id_cargue_dtll == id).ToList();

        }

        public List<managementprestadores_alertas_activasResult> GetConsultaAlertasactivas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managementprestadores_alertas_activasResult> result = new List<managementprestadores_alertas_activasResult>();

            result = db.managementprestadores_alertas_activas().ToList();

            return result;
        }

        public List<managmentprestadoresfacturasgestionadasdtllResult> GetListFacturasByNumFactura(string numfactura)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<managmentprestadoresfacturasgestionadasdtllResult> result = db.managmentprestadoresfacturasgestionadasdtll(numfactura).ToList();
            return result;
        }

        public List<managmentprestadoresfacturasgestionadasdtllCompletaResult> GetListFacturasByNumFacturaCompleta(String numFac, String nit, String prestador, String sap, int? idFactura)
        {
            List<managmentprestadoresfacturasgestionadasdtllCompletaResult> result = new List<managmentprestadoresfacturasgestionadasdtllCompletaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    result = db.managmentprestadoresfacturasgestionadasdtllCompleta(numFac, idFactura, nit, prestador, sap).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }

        public ManagementPrestadoresFacturasByIdDtllResult GetInfoFacturaById(int idcarguedtll)
        {
            ManagementPrestadoresFacturasByIdDtllResult result = new ManagementPrestadoresFacturasByIdDtllResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ManagementPrestadoresFacturasByIdDtll(idcarguedtll).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return result;
            }
        }


        #endregion

        #region OTROS
        public List<vw_md_tablero_interventoria_general> Getinterventoriagneral()
        {
            List<vw_md_tablero_interventoria_general> lstResult = new List<vw_md_tablero_interventoria_general>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_tablero_interventoria_general.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }
        public List<vw_md_tablero_intenventoria_general_detalle1> Detalleinterventoria1(Int32 ID)
        {
            List<vw_md_tablero_intenventoria_general_detalle1> ListResult = new List<vw_md_tablero_intenventoria_general_detalle1>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_tablero_intenventoria_general_detalle1.Where(l => l.id_md_interventoria_general == ID).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_md_tablero_interventoria_general_detalle2> Detalleinterventoria2(Int32 ID)
        {
            List<vw_md_tablero_interventoria_general_detalle2> ListResult = new List<vw_md_tablero_interventoria_general_detalle2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_tablero_interventoria_general_detalle2.Where(l => l.id_md_interventoria_general == ID).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_md_tablero_interventoria_general_detalle3> Detalleinterventoria3(Int32 ID)
        {
            List<vw_md_tablero_interventoria_general_detalle3> ListResult = new List<vw_md_tablero_interventoria_general_detalle3>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_tablero_interventoria_general_detalle3.Where(l => l.id_md_interventoria_general == ID).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public List<vw_md_tablero_interventoria_general_detalle4> Detalleinterventoria4(Int32 ID)
        {
            List<vw_md_tablero_interventoria_general_detalle4> ListResult = new List<vw_md_tablero_interventoria_general_detalle4>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.vw_md_tablero_interventoria_general_detalle4.Where(l => l.id_md_interventoria_general == ID).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }


        public List<vw_md_datos_comunicado> GetDatosComunicado()
        {
            List<vw_md_datos_comunicado> lstResult = new List<vw_md_datos_comunicado>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_datos_comunicado.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }


        public List<md_comunicaciones> TraerdocumentoComunicados(Int32 ID)
        {
            List<md_comunicaciones> ListResult = new List<md_comunicaciones>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.md_comunicaciones.Where(l => l.id_md_comunicaciones == ID).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }

        }

        public GestionDocumentalMedicamentosCad Traerimagenindicacioes(Int32 ID)
        {
            GestionDocumentalMedicamentosCad lstResult = new GestionDocumentalMedicamentosCad();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.GestionDocumentalMedicamentosCad.Where(p => p.id_gestion_documental__medicamentos_cad == ID).Single();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return lstResult;

        }


        public List<vw_md_consolidado_sin_auditor> Getfactursinauditor()
        {
            List<vw_md_consolidado_sin_auditor> lstResult = new List<vw_md_consolidado_sin_auditor>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_consolidado_sin_auditor.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<md_ref_puntos_control> GetpuntoControl()
        {
            List<md_ref_puntos_control> lstResult = new List<md_ref_puntos_control>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_puntos_control.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public ManagmentIngresarIncapacidadResult GetAsignarAuditorConsolidado(String factura, ref MessageResponseOBJ MsgRes)
        {

            ManagmentIngresarIncapacidadResult result = new ManagmentIngresarIncapacidadResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ManagmentIngresarIncapacidad(factura).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return result;
        }

        //Codigo facturas rechazadas
        public List<managmentprestadoresFacturasRechazadasResult> GetFacturasRechazadas(string factura, ref MessageResponseOBJ MsgRes)
        {
            List<managmentprestadoresFacturasRechazadasResult> result = new List<managmentprestadoresFacturasRechazadasResult>();
            try
            {

                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                result = db.managmentprestadoresFacturasRechazadas(factura).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return result;
        }

        public List<management_insumos_capacidad_resolutiva_listaResult> ListaInsumosCapacidadResolutiva()
        {
            List<management_insumos_capacidad_resolutiva_listaResult> result = new List<management_insumos_capacidad_resolutiva_listaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.management_insumos_capacidad_resolutiva_lista().ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return result;

        }

        #endregion

        #region FACTURACION
        public contratacion_cargue_base getcarguecontratacion(int mes, int año)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            contratacion_cargue_base reg = db.contratacion_cargue_base.Where(l => l.periodo_mes == mes && l.periodo_año == año).FirstOrDefault();
            return reg;
        }

        public ecop_gestion_facturas_aprobadas GetFacturasAprobadas(int id_cargue_dtll)
        {
            ecop_gestion_facturas_aprobadas dato = new ecop_gestion_facturas_aprobadas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_gestion_facturas_aprobadas.Where(l => l.id_cargue_dtll == id_cargue_dtll).OrderByDescending(x => x.id_gestion_factura_aprobadas).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        #endregion

        #region GESTION INTERNA


        public List<ref_gestion_interna> GetGestionInternaList()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_gestion_interna.ToList();
        }

        public ref_gestion_interna GetGestionInternaById(int idgestion)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_gestion_interna.Where(l => l.id_ref_gestion_interna == idgestion).FirstOrDefault();
        }

        public List<vw_odont_historia_clinica> ListHistoriaClinica(int id_historia)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            return db.vw_odont_historia_clinica.Where(l => l.id_odont_historia_clinica == id_historia).ToList();
        }

        public List<vw_odont_historia_clinica> GetListHistoriaClinicaXOdontologo(string nomodontologo)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_odont_historia_clinica.Where(l => l.nom_odontologo_auditado.Contains(nomodontologo)).ToList();
        }

        #endregion

        #region GASTOS POR SERVICIO
        public gasto_por_servicio_cargue_base getcarguebase(int mes, int año, string regional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.gasto_por_servicio_cargue_base.Where(l => l.mes == mes && l.año == año && l.tipoCargue == regional).FirstOrDefault();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 19 de julio de 2022
        /// Metodo que obtiene el listado de cargues de gasto por servicio
        /// </summary>
        /// <returns></returns>
        public List<vw_consulta_gasto_por_servicio> ObtenerListadoCarguesGastoPorServicio()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                db.CommandTimeout = 5000;
                return db.vw_consulta_gasto_por_servicio.ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 1 de agosto de 2022
        /// Metodo para obtener el consolidado de gasto por servicio
        /// </summary>
        /// <param name="idCargueBase"></param>
        /// <returns></returns>
        public List<Management_gasto_x_servicio_consolidadoResult> ObtenerConsolidadoGastoPorServicioPorIdCargueBase(int idCargueBase)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                db.CommandTimeout = 600;
                return db.Management_gasto_x_servicio_consolidado(idCargueBase).ToList();
            }
        }


        /// <summary>
        /// Autor: Kevin Suarez
        /// Fecha: 12 de agosto de 2022
        /// </summary>
        /// 
        public List<management_gastoxservicio_consultaResult> ObtenerGastoPorsercicioConsulta(DateTime? fechaInicio, DateTime? fechaFin, string factura, int cedula, string servicio, string tiga, DateTime? fechaInicioPre, DateTime? fechaFinPre)
        {
            List<management_gastoxservicio_consultaResult> list = new List<management_gastoxservicio_consultaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    list = db.management_gastoxservicio_consulta(fechaInicio, fechaFin, factura, cedula, servicio, tiga, fechaInicioPre, fechaFinPre).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_gastoxservicio_consulta2Result> ObtenerGastoPorsercicioConsulta2(DateTime? fechaInicio, DateTime? fechaFin, string factura, string cedula, string servicio, string tiga, DateTime? fechaInicioPre, DateTime? fechaFinPre)
        {
            List<management_gastoxservicio_consulta2Result> list = new List<management_gastoxservicio_consulta2Result>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    list = db.management_gastoxservicio_consulta2(fechaInicio, fechaFin, factura, cedula, servicio, tiga, fechaInicioPre, fechaFinPre).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        #endregion

        #region SEGUIMIENTO ENTREGABLES

        public List<seguimiento_entregables_periodo> GetListEntregablesPeriodo(int id_seg_entregable)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable == id_seg_entregable).ToList();
        }


        public seguimiento_entregables_periodo GetEntregablePeriodoById(int id_ent_periodo)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_entregables_periodo.Where(l => l.id_seg_entregable_periodo == id_ent_periodo).FirstOrDefault();

        }

        public List<ref_periodicidad_entregables> GetListPeriodicidadEntregables()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_periodicidad_entregables.ToList();
            }
        }

        public List<vw_seguimiento_entregables> GetListEntregables(int? periodicidad)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            if (periodicidad != null)
            {
                return db.vw_seguimiento_entregables.Where(l => l.periodicidad_entrega == periodicidad.Value).ToList();
            }
            else
            {
                return db.vw_seguimiento_entregables.ToList();
            }
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntrega(int id_dtll)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_dtll_entrega.Where(l => l.id_seg_dtll_entrega == id_dtll).FirstOrDefault();
        }

        public seguimiento_dtll_entrega GetseguimientoDtllEntregaPresentado(int? id_dtll)
        {
            seguimiento_dtll_entrega dato = new seguimiento_dtll_entrega();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    //dato = db.seguimiento_dtll_entrega.Where(l => l.id_seg_dtll_entrega == id_dtll).FirstOrDefault();
                    dato = db.seguimiento_dtll_entrega.Where(l => l.id_estado_entregable == 3 && (l.id_seg_dtll_entrega == id_dtll || l.id_dtll_ant == id_dtll)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<seguimiento_dtll_entrega> GetListseguimientoDtllEntrega(int id_seg_periodo)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_dtll_entrega.Where(l => l.id_seg_entregable_periodo == id_seg_periodo).ToList();
        }

        public List<seguimiento_entregables_documentos> GetSeguimientoEntregableDocs(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_entregables_documentos.Where(l => l.id_seg_dtll_entrega == id).ToList();
        }


        public seguimiento_entregables_documentos traerDocumentoEntregableId(int idDocumento)
        {
            seguimiento_entregables_documentos dato = new seguimiento_entregables_documentos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.seguimiento_entregables_documentos.Where(x => x.id_documento_seg_entregable == idDocumento).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<managmentSeguimiento_entregables_documentosResult> GetSeguimientoEntregableDocs2(ref MessageResponseOBJ MsgRes)
        {
            List<managmentSeguimiento_entregables_documentosResult> result = new List<managmentSeguimiento_entregables_documentosResult>();
            try
            {
                ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

                result = db.managmentSeguimiento_entregables_documentos().ToList();

                return result;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;

            }
            return result;

        }

        public seguimiento_entregables GetSeguimientoEntregable(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.seguimiento_entregables.Where(l => l.id_seg_entregables == id).FirstOrDefault();

        }


        public List<ref_cobertura_seguimiento_entregable> GetCoberturaSegEntregable()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_cobertura_seguimiento_entregable.ToList();

        }

        public List<ref_seguimiento_entregable_usuario_gestion> GetUsuariosSegGestion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_seguimiento_entregable_usuario_gestion.ToList();
        }

        public List<vw_seguimiento_entregables_competencias> GetSeguimientoEntregablesCompetencias()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_seguimiento_entregables_competencias.ToList();
        }

        public List<ref_proceso_entregable> Getprocesoentregable()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_proceso_entregable.ToList();

        }

        /// <summary>
        /// Autor: Alexis Quiñones Casstillo
        /// Fecha: 27/diciembre/2022
        /// Metodo para obtener el listado de tipo de seguimiento entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_seguimiento_entregables_tipo_entregable> GetListTipoEntregable()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_seguimiento_entregables_tipo_entregable.ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo para obtener el listado de estado de entregables
        /// </summary>
        /// <returns></returns>
        public List<ref_estado_entregable> GetListEstadoEntregable()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.ref_estado_entregable.ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Catillo
        /// Fecha: 28 de diciembre de 2022
        /// Metodo utilizado para consultar el listado de alertas enviadas
        /// 
        /// </summary>
        /// <returns></returns>
        public List<seguimiento_entregables_alerta_diaria> GetListNotificacionesEnviadasSeguimientoEntregables(DateTime? fecha)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                if (fecha != null)
                {
                    return db.seguimiento_entregables_alerta_diaria.Where(l => l.fecha_envio_notificacion.Date == fecha.Value.Date).ToList();
                }
                else
                {
                    return db.seguimiento_entregables_alerta_diaria.ToList();
                }
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de entregables para mostrar en el tablero de gestion 
        /// </summary>
        /// <param name="periodicidad"></param>
        /// <param name="tipoEntregable"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_gestionResult> GetListSeguimientoEntregableGestion(int? periodicidad, int? tipoEntregable)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_seguimiento_entregables_gestion(periodicidad, tipoEntregable).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 4 de enero de 2023
        /// Metodo para consultar el listado de periodos entregados y por entregar de un entregable
        /// </summary>
        /// <param name="idSeguimientoEntregable"></param>
        /// <returns></returns>
        public List<vw_seguimiento_entregables> GetListEntregablesPorIdEntregable(int? idSeguimientoEntregable)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.vw_seguimiento_entregables.Where(l => l.id_seg_entregables == idSeguimientoEntregable).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 12 de enero de 2023
        /// Metodo utilizado para retornar los datos ingresados en la evaluacion de calidad de un entregable filtrado por el id periodo entregable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public seguimiento_entregables_periodo_eval_calidad ConsultarEvaluacionPorIdPeriodoSegEntregable(int id)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.seguimiento_entregables_periodo_eval_calidad.Where(l => l.seguimiento_entregables_periodo_id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 16 de enero de 2022
        /// Metodo para obtener el listado de oportunidad  de seguimiento entregables
        /// </summary>
        /// <param name="personaResponsable"></param>
        /// <param name="tipoEntregable"></param>
        /// <param name="periodicidad"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_seguimiento_entregables_indicadoresResult> GetListadoOportunidadSeguimientoEntregables(string personaResponsable, int? tipoEntregable, int? periodicidad, int? año)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_seguimiento_entregables_indicadores(personaResponsable, tipoEntregable, periodicidad, año).ToList();
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXPersonaResult> GetListadoIndicadoresXPersonaSeguimientoEntregables(int mesInicial, int mesFinal, int año, string responsable)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_SeguimientoEntregables_IndicadorXPersona(mesInicial, mesFinal, año, responsable).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXComponenteResult> GetListadoIndicadoresXComponenteSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_SeguimientoEntregables_IndicadorXComponente(mesInicial, mesFinal, año, idProceso).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 31 de enero de 2023
        /// </summary>
        /// <param name="mesInicial"></param>
        /// <param name="mesFinal"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorXCompyPeridicidadResult> GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables(int mesInicial, int mesFinal, int año, int? idProceso, int? idPeriodicidad)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_SeguimientoEntregables_IndicadorXCompyPeridicidad(mesInicial, mesFinal, año, idProceso, idPeriodicidad).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 3 de febrero de 2023
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="idProceso"></param>
        /// <param name="año"></param>
        /// <returns></returns>
        public List<Management_SeguimientoEntregables_IndicadorVencimientoResult> GetIndicadorDiasVencimientSegEntregables(string responsable, int? idProceso, int? año)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_SeguimientoEntregables_IndicadorVencimiento(responsable, idProceso, año).ToList();
            }
        }
        #endregion

        #region CONTACT CENTER

        public List<ref_contact_clasificacion_contacto> GetListClasificacionContacto()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_clasificacion_contacto.ToList();
        }

        public List<ref_contact_tipificacion> GetListTipificacion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_tipificacion.ToList();
        }

        public List<ref_contact_tipo_servicio> GetListTipoServicio()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_tipo_servicio.ToList();
        }

        public List<ref_contact_tipo_solicitud> GetListTipoSolicitud()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_tipo_solicitud.ToList();
        }

        public List<ref_contact_tipoSolicitudBitacora> GetListTipoSolicitudBitacora()
        {
            List<ref_contact_tipoSolicitudBitacora> lista = new List<ref_contact_tipoSolicitudBitacora>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_contact_tipoSolicitudBitacora.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<Ref_cie10> GetCie10Bycodigo(string term)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.Ref_cie10.Where(l => l.id_cie10.Contains(term) || l.des.ToLower().Contains(term.ToLower())).ToList();
        }


        public List<ref_cie10_mortNat> GetCie10MorNatBycodigo(string term)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_cie10_mortNat.Where(l => l.codigo.Contains(term) || l.descripcion.ToLower().Contains(term.ToLower())).ToList();
        }

        public List<ref_contact_estado_solicitud> GetListEstadoSolicitud()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_estado_solicitud.ToList();
        }

        public List<ref_contact_medio_notificacion> GetListMediosNotificacion()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_contact_medio_notificacion.ToList();
        }


        public contact_center GetContactCenterById(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.contact_center.Where(l => l.id_contact_center == id).FirstOrDefault();
        }

        public management_contact_centerResult GetContactCenterCensoIdContact(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.management_contact_center(null, null).Where(l => l.id_contact_center == id).FirstOrDefault();
        }

        public management_contact_centerResult GetContactCenterCensoIdCenso(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.management_contact_center(null, null).Where(l => l.id_censo == id).FirstOrDefault();
        }

        public management_contact_centerResult GetContactCenterCensoIdConcurrencia(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.management_contact_center(null, null).Where(l => l.id_concurrencia == id).FirstOrDefault();
        }

        public List<contact_center_dtll> GetListBitacoraByIngreso(int id_contact_center, int? censo, int? idConcurrencia)
        {
            List<contact_center_dtll> lista = new List<contact_center_dtll>();
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            try
            {
                lista = db.contact_center_dtll.Where(l => (l.id_contact_center == id_contact_center && l.id_contact_center != 0) || (l.id_censo == censo && l.id_censo != 0) || (l.id_concurrencia == idConcurrencia && l.id_concurrencia != 0)).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<vw_contact_center> GetListContactCenter(int? estado)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            if (estado == null)
            {
                return db.vw_contact_center.ToList();
            }
            else
            {
                return db.vw_contact_center.Where(l => l.id_estado_solicitud == estado.Value).ToList();
            }
        }

        public List<management_contact_centerResult> ListaTableroContactCenter(DateTime? fechaIni, DateTime? fechaFin)
        {
            List<management_contact_centerResult> lista = new List<management_contact_centerResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_contact_center(fechaIni, fechaFin).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }
        public List<ref_contact_solicitante> GetlistSolicitantesbytipo(string term, int tipo)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();

            List<ref_contact_solicitante> lista = db.ref_contact_solicitante.Where(l => l.id_tipo_solicitante == tipo).ToList();
            lista = lista.Where(l => l.nombre_razon_social.ToUpper().Contains(term.ToUpper())).ToList();
            return lista;
        }


        public List<management_contact_center_camposObligatoriosResult> ListaCamposObligatoriosCC(int? idContact, int? idConcurrencia, int? idCenso)
        {
            List<management_contact_center_camposObligatoriosResult> lista = new List<management_contact_center_camposObligatoriosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_contact_center_camposObligatorios(idContact, idConcurrencia, idCenso).ToList();
                }
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }
            return lista;
        }


        public List<management_contact_center_seguimientoResult> ListaTableroContactCenterSeguimiento(DateTime? fechaIni, DateTime? fechaFin)
        {
            List<management_contact_center_seguimientoResult> lista = new List<management_contact_center_seguimientoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_contact_center_seguimiento(fechaIni, fechaFin).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }
        #endregion

        #region Insumos

        public bool ValidarExistenciaQuejasValidas(int mes, int año)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var quejascargadas = db.calidad_quejas_validas.Where(l => l.mes == mes && l.año == año).FirstOrDefault();
            if (quejascargadas != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidarExistenciaOportunidadRIPS(int mes, int año)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var quejascargadas = db.calidad_oportunidad_rips.Where(l => l.mes == mes && l.año == año).FirstOrDefault();
            if (quejascargadas != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<vw_calidad_quejas_validas> GetListCalidadQuejasValidas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_quejas_validas.ToList();
        }


        public List<calidad_quejas_validas_base_zip> GetListBasesCargadasQuejasValidas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_quejas_validas_base_zip.ToList();
        }

        public List<vw_calidad_oportunidad_rips_indicador> GetListCalidadOportunidadRips()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_oportunidad_rips_indicador.ToList();
        }

        public List<vw_calidad_de_rips_indicador> GetListCalidadCalidadRips()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_de_rips_indicador.ToList();
        }

        public List<vw_calidad_oportunidad_citas_medicina_gnral_indicador> GetListCalidadOporCitasMedicas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_oportunidad_citas_medicina_gnral_indicador.ToList();
        }

        public List<vw_calidad_oportunidad_odontologia_gnral_indicador> GetListCalidadOporOdontologia()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_oportunidad_odontologia_gnral_indicador.ToList();
        }

        public List<vw_calidad_citas_cumplidas_indicador> GetListCalidadCitasCumplidas()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_citas_cumplidas_indicador.ToList();
        }

        public List<calidad_evento_adverso> GetListCalidadEventoAdverso()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_evento_adverso.ToList();
        }

        public List<calidad_gestor_documental_insumos> GetListGestorDocumentalInsumos()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_gestor_documental_insumos.ToList();
        }

        public calidad_gestor_documental_insumos GetDocumentoById(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_gestor_documental_insumos.Where(l => l.id_calidad_gestor_documental_insumos == id).FirstOrDefault();
        }

        public vw_calidad_gestor_documental_insumos VwGetDocumentoById(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_gestor_documental_insumos.Where(l => l.id_calidad_gestor_documental_insumos == id).FirstOrDefault();
        }
        public calidad_quejas_validas_base_zip GetArchivoById(int id)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.calidad_quejas_validas_base_zip.Where(l => l.id_calidad_quejas_validas_base_zip == id).FirstOrDefault();
        }

        public List<ref_calidad_insumos_tipo_documental> GetListInsumoTipoDocumental()
        {
            List<ref_calidad_insumos_tipo_documental> list = new List<ref_calidad_insumos_tipo_documental>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_calidad_insumos_tipo_documental.OrderBy(x => x.descripcion.Contains("Otro")).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<vw_calidad_quejas_validas_prestadores> GetPrestadoresQuejasValidas(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_quejas_validas_prestadores.Where(l => l.prestador.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> GetPrestadoresOportunidadRips(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_oportunidad_calidad_rips_indicador_prestadores.Where(l => l.nom_prestador.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_oportunidad_calidad_rips_indicador_prestadores> GetCodPrestadoresOportunidadRips(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_oportunidad_calidad_rips_indicador_prestadores.Where(l => l.codigo_prestador.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_opor_citas_y_odont_prestadores> GetPrestadoresCitasmedicasyodontologia(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_opor_citas_y_odont_prestadores.Where(l => l.Prestador.ToUpper().Contains(term.ToUpper()) || l.Nit_prestador.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_opor_citas_y_odon_profesionales> GetProfesionalesCitasmedicasyodontologia(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_opor_citas_y_odon_profesionales.Where(l => l.profesional.ToUpper().Contains(term.ToUpper()) || l.documento_profesional.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_eventos_adversos_prestadores> GetprestadoresEventosAdversos(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_eventos_adversos_prestadores.Where(l => l.prestador.ToUpper().Contains(term.ToUpper()) || l.documento_prestador.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public List<vw_calidad_citas_cumplidas_profesionales> GetProfesionalesCitasCumplidas(string term, ref MessageResponseOBJ MsgRes)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.vw_calidad_citas_cumplidas_profesionales.Where(l => l.profesional.ToUpper().Contains(term.ToUpper())).ToList();
        }

        public bool ValidarExistenciaIndicadorCalidad(int mes, int año)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            var lista = db.insumos_capacidad_resolutiva.Where(l => l.mes == mes && l.año == año).FirstOrDefault();
            if (lista != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public base_beneficiarios getUltimoPeriodoBeneficiarios()
        {
            base_beneficiarios list = new base_beneficiarios();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.base_beneficiarios.OrderByDescending(x => x.periodo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return list;
        }


        public base_beneficiarios TraerBeneficiarioCedula(string cedula)
        {
            base_beneficiarios list = new base_beneficiarios();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.base_beneficiarios.Where(x => x.Numero_identificacion == cedula).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return list;
        }


        public List<management_baseBeneficiarios_tableroControlResult> TraerListadoBaseBeneficiarios()
        {
            List<management_baseBeneficiarios_tableroControlResult> listado = new List<management_baseBeneficiarios_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_baseBeneficiarios_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> TraerListadoBaseBeneficiariosConsolidado(string periodo)
        {
            List<management_baseBeneficiarios_consolidadoDetallePeriodoResult> listado = new List<management_baseBeneficiarios_consolidadoDetallePeriodoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_baseBeneficiarios_consolidadoDetallePeriodo(periodo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }



        public List<ref_adherencia_ciudad> GetCiudad()
        {
            List<ref_adherencia_ciudad> list = new List<ref_adherencia_ciudad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_adherencia_ciudad.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return list;
            }
        }

        public List<ref_adherencia_prestador> traerPrestadores()
        {
            List<ref_adherencia_prestador> list = new List<ref_adherencia_prestador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_adherencia_prestador.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var excepcion = ex.Message;
                return list;
            }
        }

        public List<management_traerPrestadoresResult> traerPrestadoresId(string id)
        {
            List<management_traerPrestadoresResult> list = new List<management_traerPrestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_traerPrestadores(id).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var excepcion = ex.Message;
                return list;
            }
        }

        public List<ref_adherencia_ciudad> getCiudadesUnis(int idUnis)
        {
            List<ref_adherencia_ciudad> list = new List<ref_adherencia_ciudad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_adherencia_ciudad.Where(x => x.unis_id == idUnis).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<base_beneficiarios_vip> listadoBBVIP()
        {
            List<base_beneficiarios_vip> lista = new List<base_beneficiarios_vip>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.base_beneficiarios_vip.ToList();
                }
            }catch(Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #endregion

        #region verificacion


        public List<ref_ver_tipoCriterio> Get_refTipoCriterio()
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    return db.ref_ver_tipoCriterio.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<ref_verificacion_farmaceutico> Get_refVerificacionFarmaceutita()
        {
            List<ref_verificacion_farmaceutico> result = new List<ref_verificacion_farmaceutico>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_verificacion_farmaceutico.ToList();
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public ver_evaluacion_archivos DescargarArchivoEvaluacionVisitas(int idArchivo)
        {
            ver_evaluacion_archivos datos = new ver_evaluacion_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.ver_evaluacion_archivos.Where(x => x.id_img == idArchivo).FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return datos;
            }
        }

        public ver_evaluacion_archivos BuscarExistenciaArchivo(int? idEvaluacion, int? idCriterio, int? idVerificacion, string nombre)
        {
            ver_evaluacion_archivos dato = new ver_evaluacion_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ver_evaluacion_archivos.FirstOrDefault(x => x.id_evaluacion == idEvaluacion && x.id_tipoCriterio == idCriterio && x.id_verification == idVerificacion && x.nombre == nombre);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }
        public ref_verificacion_farmaceutico Get_refVerificacionFarmaceutitaById(int idTipoVer)
        {
            ref_verificacion_farmaceutico result = new ref_verificacion_farmaceutico();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_verificacion_farmaceutico.Where(x => x.id_veriticacion == idTipoVer).FirstOrDefault();
                }
                return result;
            }
            catch
            {
                return result;
            }
        }


        public List<management_verificacionListaResult> getTipoCriterioId(int idTipo)
        {

            List<management_verificacionListaResult> list = new List<management_verificacionListaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_verificacionLista().Where(x => x.id_tipo_verificacion == idTipo).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }
        public List<management_verificacionListaResult> getTotalDatosDispen()
        {

            List<management_verificacionListaResult> list = new List<management_verificacionListaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_verificacionLista().ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }


        public List<ver_tipocriterio> get_ref_tipoCriterio(int idVerificacion)
        {
            List<ver_tipocriterio> list = new List<ver_tipocriterio>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ver_tipocriterio.Where(x => x.id_tipo_verificacion == idVerificacion).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<ver_tipocriterio> get_ver_tipocriterio(int idVerificacion)
        {
            List<ver_tipocriterio> list = new List<ver_tipocriterio>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ver_tipocriterio.Where(x => x.id_tipo_verificacion == idVerificacion).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }


        public List<ref_ver_grupo_tpocriterio> get_ver_grupoTipoCritero()
        {
            List<ref_ver_grupo_tpocriterio> list = new List<ref_ver_grupo_tpocriterio>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_ver_grupo_tpocriterio.ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<ver_criterio> getcriteriosbytipoverificacion(int tipoverificacion)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    return db.ver_criterio.Where(l => l.id_tipo_verificacion == tipoverificacion).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public ver_criterio ConsultarCriterioById2(int idcriterio)
        {
            ver_criterio result = new ver_criterio();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ver_criterio.Where(x => x.id_ver_criterio == idcriterio).FirstOrDefault();
                    return result;
                }
            }
            catch
            {
                return result;
            }

        }
        public List<ref_verificacionFarmaceutica_tipos> getTiposVerificacion()
        {
            List<ref_verificacionFarmaceutica_tipos> list = new List<ref_verificacionFarmaceutica_tipos>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_verificacionFarmaceutica_tipos.ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<ver_puntoDispensacion> getPuntoDispensacionList()
        {
            List<ver_puntoDispensacion> list = new List<ver_puntoDispensacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ver_puntoDispensacion.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_dispensacion_archivosRepositorioResult> MostrarArchivosEvaluacionVisitasMD(int? idEvaluacion)
        {
            List<management_dispensacion_archivosRepositorioResult> lista = new List<management_dispensacion_archivosRepositorioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_dispensacion_archivosRepositorio(idEvaluacion).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacionl()
        {
            List<management_dispensacion_evaluacionRelacionResult> list = new List<management_dispensacion_evaluacionRelacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacionId(int Id)
        {
            List<management_dispensacion_evaluacionRelacionResult> list = new List<management_dispensacion_evaluacionRelacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion().Where(x => x.id_evaluacion == Id).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }
        public List<management_dispensacion_evaluacionRelacion_totalResult> getDispensacionEvaluacionTotal()
        {
            List<management_dispensacion_evaluacionRelacion_totalResult> list = new List<management_dispensacion_evaluacionRelacion_totalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion_total().ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<management_dispensacion_evaluacionRelacion_totalIdResult> getDispensacionEvaluacionTotalId(int id)
        {
            List<management_dispensacion_evaluacionRelacion_totalIdResult> list = new List<management_dispensacion_evaluacionRelacion_totalIdResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion_totalId(id).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return list;
            }
        }


        public List<ref_tipoCriterio> ListImpactosEvaluacion()
        {
            List<ref_tipoCriterio> lista = new List<ref_tipoCriterio>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tipoCriterio.ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_dispensacion_evaluacionRelacion_hallazgoResult> getEvolucionHallazgos()
        {
            List<management_dispensacion_evaluacionRelacion_hallazgoResult> list = new List<management_dispensacion_evaluacionRelacion_hallazgoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion_hallazgo().Where(x => x.id_evaluacion != null).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalIdHallazgoId(int id)
        {
            List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> list = new List<management_dispensacion_evaluacionRelacion_total_hallazgoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion_total_hallazgo().Where(x => x.id_evaluacion == id).ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }


        public List<ref_evaluacion_estadoTotal> getEstadosEvaluacionHallazgos()
        {
            List<ref_evaluacion_estadoTotal> list = new List<ref_evaluacion_estadoTotal>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_evaluacion_estadoTotal.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_evaluacion_tipoHallazgo> getTipoHallazgoEvaluacion()
        {
            List<ref_evaluacion_tipoHallazgo> list = new List<ref_evaluacion_tipoHallazgo>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_evaluacion_tipoHallazgo.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ver_evaluacion_hallazgo> ExisteHallazgoSubsanado(int idTotal, int id_tipoCriterio)
        {
            List<ver_evaluacion_hallazgo> list = new List<ver_evaluacion_hallazgo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ver_evaluacion_hallazgo.Where(x => x.id_total == idTotal && x.id_tipoCriterio == id_tipoCriterio).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }

        public List<ref_evaluacion_cumplimiento> getCumplimientoEvaluacion()
        {
            List<ref_evaluacion_cumplimiento> list = new List<ref_evaluacion_cumplimiento>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_evaluacion_cumplimiento.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_evaluacion_tipoSoporte> getTipoSoporteEvaluacion()
        {
            List<ref_evaluacion_tipoSoporte> list = new List<ref_evaluacion_tipoSoporte>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_evaluacion_tipoSoporte.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalHallazgo()
        {
            List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> list = new List<management_dispensacion_evaluacionRelacion_total_hallazgoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_dispensacion_evaluacionRelacion_total_hallazgo().ToList();
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        #endregion

        #region CUIDADOS PALIATIVOS POR DAVID FONSECA

        public List<Ref_ffmm_unidad_cp> GetUnidad(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_unidad_cp> lstResult = new List<Ref_ffmm_unidad_cp>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_unidad_cp.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_unidad_satelite> lstResult = new List<Ref_ffmm_unidad_satelite>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_unidad_satelite.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_ffmm_tipo_visita_cp> GetTipoVisita(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_tipo_visita_cp> lstResult = new List<Ref_ffmm_tipo_visita_cp>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_tipo_visita_cp.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_ffmm_departamento> GetDepartamentos(ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_departamento> lstResult = new List<vw_ffmm_departamento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ffmm_departamento.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_ffmm_municipio> GetMunicipios(ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_municipio> lstResult = new List<vw_ffmm_municipio>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ffmm_municipio.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_ffmm_municipio> GetMunicipiosFM(int idDepartamento, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_municipio> lstResult = new List<vw_ffmm_municipio>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ffmm_municipio.Where(x => x.cod_departamento == idDepartamento).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<vw_ffmm_ips> GetIPS(ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_ips> lstResult = new List<vw_ffmm_ips>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ffmm_ips.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_tipo_documental> GetTipoIdentificacion(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_tipo_documental> lstResult = new List<Ref_tipo_documental>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_documental.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_ffmm_sexo> GetSexo(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_sexo> lstResult = new List<Ref_ffmm_sexo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_sexo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_unidad_cp> GetSitioAdscripcion(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_unidad_cp> lstResult = new List<Ref_ffmm_unidad_cp>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_unidad_cp.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_tipo_afiliacion> GetTipoAfiliacion(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_tipo_afiliacion> lstResult = new List<Ref_ffmm_tipo_afiliacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_tipo_afiliacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_estado> GetEstado(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_estado> lstResult = new List<Ref_ffmm_estado>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_estado.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_fuerza> GetFuerza(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_fuerza> lstResult = new List<Ref_ffmm_fuerza>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ffmm_fuerza.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ffmm_sino> GetSiNo(ref MessageResponseOBJ MsgRes)
        {
            List<Ref_ffmm_sino> lstResult = new List<Ref_ffmm_sino>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_sino.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }
        public List<vw_cie10> GetCie10(ref MessageResponseOBJ MsgRes)
        {
            List<vw_cie10> lstResult = new List<vw_cie10>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_cie10.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<management_traerIpsoPrestadoresResult> traerPrestadoresId(int tipo, int nit)
        {
            List<management_traerIpsoPrestadoresResult> list = new List<management_traerIpsoPrestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_traerIpsoPrestadores(tipo, nit).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_ffmm_ips_prestadores> existenciaIpsPrestadores(int nit)
        {
            List<ref_ffmm_ips_prestadores> list = new List<ref_ffmm_ips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_ffmm_ips_prestadores.Where(x => x.nit == nit).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<ref_ffmm_ips_prestadores> existenciaIpsPrestadoresNombre(String nombre)
        {
            List<ref_ffmm_ips_prestadores> list = new List<ref_ffmm_ips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_ffmm_ips_prestadores.Where(x => x.nombre.Contains(nombre)).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestador(int idCiudad, int tipo)
        {
            List<ref_ffmm_ips_prestadores> lstResult = new List<ref_ffmm_ips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ref_ffmm_ips_prestadores.Where(x => x.tipo == tipo && x.cod_municipio == idCiudad).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lstResult;
        }

        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestadorSinTipo(int idCiudad)
        {
            List<ref_ffmm_ips_prestadores> lstResult = new List<ref_ffmm_ips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ref_ffmm_ips_prestadores.Where(x => x.cod_municipio == idCiudad).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lstResult;
        }
        public List<ref_ffmm_ips_prestadores> ObtenerIpsPrestadorCompleto()
        {
            List<ref_ffmm_ips_prestadores> lstResult = new List<ref_ffmm_ips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ref_ffmm_ips_prestadores.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lstResult;
        }

        public List<management_contratos_prestadoresResult> ObtenerIpsPrestadorTablero()
        {
            List<management_contratos_prestadoresResult> lstResult = new List<management_contratos_prestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_contratos_prestadores().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lstResult;
        }
        public List<management_facturas_pagosResult> ListFacturasPago()
        {
            List<management_facturas_pagosResult> list = new List<management_facturas_pagosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_facturas_pagos().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        #endregion

        #region Glosas
        public List<vw_ffmm_glosas> GetFFMMGlosas(ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_glosas> lstResult = new List<vw_ffmm_glosas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ffmm_glosas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        #endregion

        #region AuditoriaFacturas 
        public List<ffmm_Cuentas_auditoria> GetCuentasAuditoria(ref MessageResponseOBJ MsgRes)
        {
            List<ffmm_Cuentas_auditoria> lstResult = new List<ffmm_Cuentas_auditoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ffmm_Cuentas_auditoria.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public ffmm_Cuentas_auditoria ultimoPagoyConciliacion()
        {
            ffmm_Cuentas_auditoria list = new ffmm_Cuentas_auditoria();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ffmm_Cuentas_auditoria.OrderByDescending(x => x.id_ffmm_Cuentas_auditoria).FirstOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public ffmm_glosas GetGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {

            ffmm_glosas objeto = new ffmm_glosas();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    objeto = db.ffmm_glosas.Where(p => p.id_glosa == id_glosa).First();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return objeto;
        }



        public ffmm_cuentas_glosas GetCuentasGlosas(int id_glosa, ref MessageResponseOBJ MsgRes)
        {
            ffmm_cuentas_glosas objeto = new ffmm_cuentas_glosas();
            ffmm_glosas objeto2 = new ffmm_glosas();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    objeto2 = db.ffmm_glosas.Where(p => p.id_glosa == id_glosa).First();

                    objeto = db.ffmm_cuentas_glosas.Where(p => p.id_ffmm_Cuentas_auditoria == objeto2.id_ffmm_cuentas_auditoria).First();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return objeto;
        }


        #endregion

        #region OTROS II
        public List<management_unionFuerzasgradosResult> getUnionFuerzas(int idFuerza)
        {
            List<management_unionFuerzasgradosResult> list = new List<management_unionFuerzasgradosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_unionFuerzasgrados(idFuerza).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<facturacion_sap_cargue> validarCargueFacturaSap(int? mes, int? año)
        {
            List<facturacion_sap_cargue> list = new List<facturacion_sap_cargue>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.facturacion_sap_cargue.Where(x => x.mes == mes && x.año == año).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_facturacionSAP_listaResult> ListarFacturacionSAP()
        {
            List<management_facturacionSAP_listaResult> list = new List<management_facturacionSAP_listaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_facturacionSAP_lista().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }
        public List<management_facturacionSAP_listaDetalleResult> ListarFacturacionSAPDetalle(int año, string mes)
        {
            List<management_facturacionSAP_listaDetalleResult> list = new List<management_facturacionSAP_listaDetalleResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_facturacionSAP_listaDetalle(año, mes).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }

        public List<management_facturacionSAP_listaREPORTEResult> ListarFacturacionSAPReporte(DateTime fechaIni, DateTime fechaFin)
        {
            List<management_facturacionSAP_listaREPORTEResult> list = new List<management_facturacionSAP_listaREPORTEResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_facturacionSAP_listaREPORTE(fechaIni, fechaFin).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }

        //public List<medicamentos_dispen_cargue> ExistenciaMedicamentosDispen(int año, int mes)
        //{
        //    List<medicamentos_dispen_cargue> list = new List<medicamentos_dispen_cargue>();
        //    try
        //    {
        //        using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
        //        {
        //            list = db.medicamentos_dispen_cargue.Where(x => x.año == año && x.mes == mes).ToList();
        //            return list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        return list;
        //    }

        //}

        public List<management_medicamentosDispen_listResult> ListaMedicamentosDispensacion(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_medicamentosDispen_listResult> list = new List<management_medicamentosDispen_listResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_list(fechaInicio, fechaFin).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_medicamentosDispen_consolidadoIdResult> ListaMedicamentosDispensacionConsolidado(int? idCargue)
        {
            List<management_medicamentosDispen_consolidadoIdResult> list = new List<management_medicamentosDispen_consolidadoIdResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 300;
                    list = db.management_medicamentosDispen_consolidadoId(idCargue).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_medicamentosDispen_reporteResult> ListaMedicamentosDispensacionReporte(int idCargue)
        {
            List<management_medicamentosDispen_reporteResult> list = new List<management_medicamentosDispen_reporteResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_reporte(idCargue).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_listaMedicDspensacionResult> ListaMedicamentosDispensacionPrestadores(int mes, int año)
        {
            List<management_listaMedicDspensacionResult> list = new List<management_listaMedicDspensacionResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    list = db.management_listaMedicDspensacion(mes, año).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_listaMedicDspensacion_agrupacionResult> ListaMedicamentosDispensacionPrestadoresAgrupacion(int mes, int año)
        {
            List<management_listaMedicDspensacion_agrupacionResult> list = new List<management_listaMedicDspensacion_agrupacionResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_listaMedicDspensacion_agrupacion(mes, año).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_medicamentosDispen_consultaResult> ListaMedicamentosDispenConsulta(DateTime fechaIni, DateTime fechaFin)
        {
            List<management_medicamentosDispen_consultaResult> list = new List<management_medicamentosDispen_consultaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_consulta(fechaIni, fechaFin).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_medicamentosDispen_consulta_armadaResult> ListaMedicamentosDispenConsultaArmada(DateTime fechaIni, DateTime fechaFin, string documento, string familiar, string formula)
        {
            List<management_medicamentosDispen_consulta_armadaResult> list = new List<management_medicamentosDispen_consulta_armadaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_consulta_armada(fechaIni, fechaFin, documento, familiar, formula).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_medicamentosDispen_consulta_filtros_docResult> ListaMedicamentosDispenConsultaFiltroDoc(string documento)
        {
            List<management_medicamentosDispen_consulta_filtros_docResult> list = new List<management_medicamentosDispen_consulta_filtros_docResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_consulta_filtros_doc(documento).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_medicamentosDispen_consulta_filtros_familiarResult> ListaMedicamentosDispenConsultaFiltroFamiliar(string familia)
        {
            List<management_medicamentosDispen_consulta_filtros_familiarResult> list = new List<management_medicamentosDispen_consulta_filtros_familiarResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_consulta_filtros_familiar(familia).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }
        public List<management_medicamentosDispen_consulta_filtros_formulaResult> ListaMedicamentosDispenConsultaFiltroFormu(string formula)
        {
            List<management_medicamentosDispen_consulta_filtros_formulaResult> list = new List<management_medicamentosDispen_consulta_filtros_formulaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_medicamentosDispen_consulta_filtros_formula(formula).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public md_Lupe_cargue_base UltimoCargueLupe(int? idPrestador)
        {
            md_Lupe_cargue_base objeto = new md_Lupe_cargue_base();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    objeto = db.md_Lupe_cargue_base.Where(x => x.id_prestador == idPrestador).OrderByDescending(x => x.id_lupe_cargue_base).FirstOrDefault();
                    return objeto;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return objeto;
            }
        }

        public List<management_lupe_carguesResult> listadoCargueLupe()
        {
            List<management_lupe_carguesResult> list = new List<management_lupe_carguesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_lupe_cargues().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return list;
            }
        }

        public List<management_lupe_cargues_intermediacionesResult> listadoCargueLupeIntermediaciones(int idLupe)
        {
            List<management_lupe_cargues_intermediacionesResult> list = new List<management_lupe_cargues_intermediacionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_lupe_cargues_intermediaciones(idLupe).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return list;
            }
        }

        public List<management_listadoPrefacturasCruzanResult> listadoSiCruzanPrefacturasLupe(int idBase)
        {
            List<management_listadoPrefacturasCruzanResult> list = new List<management_listadoPrefacturasCruzanResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 4000;
                    list = db.management_listadoPrefacturasCruzan(idBase).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_listadoPrefacturasNoCruzanResult> listadoNoCruzanPrefacturasLupe(int idBase)
        {
            List<management_listadoPrefacturasNoCruzanResult> list = new List<management_listadoPrefacturasNoCruzanResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 4000;
                    list = db.management_listadoPrefacturasNoCruzan(idBase).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public management_prefacturas_buscarEnHistoricoPrefacturasResult BuscarHistoricoPrefacturas(string num_documento_beneficiario, string cum,
            string nombre_comercial_medicacmento, string num_unidades_entregadas, DateTime fecha_despacho_formula, string vlr_unitario_und_entregada)
        {
            management_prefacturas_buscarEnHistoricoPrefacturasResult lista = new management_prefacturas_buscarEnHistoricoPrefacturasResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_prefacturas_buscarEnHistoricoPrefacturas(num_documento_beneficiario, cum, nombre_comercial_medicacmento, num_unidades_entregadas, fecha_despacho_formula, vlr_unitario_und_entregada).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }
        #endregion

        #region ANALISTAS

        public int ValidaExisteAnalista(int regional, int unis, int analista)
        {
            var respuesta = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    respuesta = db.ref_analisis_configuracion.Where(x => x.regional == regional && x.unis == unis && x.id_analista == analista).Count();
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return respuesta;
            }
        }

        public ref_cuentas_medicas_analista TraerAnalistasIngresados(ref_cuentas_medicas_analista obj)
        {
            ref_cuentas_medicas_analista list = new ref_cuentas_medicas_analista();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_cuentas_medicas_analista.Where(x => x.id_usuario == obj.id_usuario && x.id_regional == obj.id_regional && x.id_prestador == obj.id_prestador).FirstOrDefault();
                    return list;
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return list;
            }
        }


        #endregion ANALISTAS

        #region OTROS III
        public List<ManagmentRipsHomologacionFacResult> ConsultaHomologacionFac(String num_factura, String tipo_id_prestador, String num_id_prestador)
        {
            List<ManagmentRipsHomologacionFacResult> list = new List<ManagmentRipsHomologacionFacResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ManagmentRipsHomologacionFac(num_factura, tipo_id_prestador, num_id_prestador).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ManagmentRipsHomologacionFacDTLLResult> ConsultaHomologacionFacdtll(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            List<ManagmentRipsHomologacionFacDTLLResult> list = new List<ManagmentRipsHomologacionFacDTLLResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ManagmentRipsHomologacionFacDTLL(num_factura, tipo_id_prestador, num_id_prestador, id_rips).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<rips_homologacion> Traerhomologacion_dtll(rips_homologacion obj)
        {
            List<rips_homologacion> list = new List<rips_homologacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.rips_homologacion.Where(x => x.id_rips == obj.id_rips && x.num_facturas == obj.num_facturas).ToList();
                    return list;
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return list;
            }
        }

        public List<ManagmentRipsHomologacionFacDTLLFinalResult> ConsultaHomologacionFacdtllFinal(String num_factura, Int32 id_rips)
        {
            List<ManagmentRipsHomologacionFacDTLLFinalResult> list = new List<ManagmentRipsHomologacionFacDTLLFinalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ManagmentRipsHomologacionFacDTLLFinal(num_factura, id_rips).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<ManagmentRipsHomologacionFacFinalResult> ConsultaHomologacionFacFinal(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            List<ManagmentRipsHomologacionFacFinalResult> list = new List<ManagmentRipsHomologacionFacFinalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ManagmentRipsHomologacionFacFinal(num_factura, tipo_id_prestador, num_id_prestador, id_rips).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        public List<management_gastoServicio_nombreServicioResult> ConsultarNombreServicioGXS(string nombre)
        {
            List<management_gastoServicio_nombreServicioResult> list = new List<management_gastoServicio_nombreServicioResult>();
            {
                try
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        list = db.management_gastoServicio_nombreServicio(nombre).ToList();
                        return list;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return list;
                }
            }
        }

        public List<management_informacionUsuarios_prestadoresResult> UsuariosPrestadores(string nit, string nombre, string cedula)
        {
            List<management_informacionUsuarios_prestadoresResult> lista = new List<management_informacionUsuarios_prestadoresResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_informacionUsuarios_prestadores(nit, nombre, cedula).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }
        public List<management_informacionUsuarios_prestadoresDetalleResult> UsuariosPrestadoresDetalle(int idUsuario)
        {
            List<management_informacionUsuarios_prestadoresDetalleResult> lista = new List<management_informacionUsuarios_prestadoresDetalleResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_informacionUsuarios_prestadoresDetalle(idUsuario).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public sis_usuario datosUsuarioId(int idUsuario)
        {
            sis_usuario datos = new sis_usuario();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.sis_usuario.Where(x => x.id_usuario == idUsuario).FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return datos;
            }

        }


        public sis_usuario datosUsuarioNombre(string nombre)
        {
            sis_usuario datos = new sis_usuario();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.sis_usuario.Where(x => x.nombre == nombre).FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return datos;
            }

        }


        public sis_usuario datosUsuarioRol(int? idRol)
        {
            sis_usuario datos = new sis_usuario();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.sis_usuario.Where(x => x.id_rol == idRol && x.estado_usuario == "1").FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return datos;
            }

        }




        public sis_usuario datosUsuarioUser(string usuario)
        {
            sis_usuario datos = new sis_usuario();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.sis_usuario.Where(x => x.usuario == usuario).FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return datos;
            }

        }

        public List<management_regional_usuarioResult> ListadoRegionalesUsuarioId(int? idUsuario)
        {
            List<management_regional_usuarioResult> lista = new List<management_regional_usuarioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_regional_usuario(idUsuario).ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_usuarios_regionalIdResult> listadoRegionalesUsuarioReg(int? idRegional)
        {
            List<management_usuarios_regionalIdResult> lista = new List<management_usuarios_regionalIdResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_usuarios_regionalId(idRegional).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }


        public List<sis_auditor_regional> listadoRegionalesUsuario(int idUsuario)
        {
            List<sis_auditor_regional> lista = new List<sis_auditor_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.sis_auditor_regional.Where(x => x.id_auditor == idUsuario).OrderByDescending(x => x.id_auditor_regional).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<sis_auditor_regional_pqrs> listadoRegionalesUsuarioPqrs(int idUsuario)
        {
            List<sis_auditor_regional_pqrs> lista = new List<sis_auditor_regional_pqrs>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.sis_auditor_regional_pqrs.Where(x => x.id_usuario == idUsuario).OrderByDescending(x => x.id_registro).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<sis_auditor_regional_pqrs> listadoCoordinadoresPqrs()
        {
            List<sis_auditor_regional_pqrs> listado = new List<sis_auditor_regional_pqrs>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.sis_auditor_regional_pqrs.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public sis_auditor_regional GetRegionalAuditor(int? idUsuario)
        {
            sis_auditor_regional dato = new sis_auditor_regional();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.sis_auditor_regional.Where(x => x.id_auditor == idUsuario).OrderByDescending(x => x.id_auditor_regional).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            return dato;
        }

        public ref_solucionador UltimaRegionalUsuario(string nombre)
        {
            ref_solucionador lista = new ref_solucionador();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_solucionador.Where(x => x.nombre_solucionador == nombre).OrderByDescending(x => x.id_regional).FirstOrDefault();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_existeLoteAsignado_FacturaResult> ExisteLoteAsignado(int idFac)
        {
            List<management_existeLoteAsignado_FacturaResult> lista = new List<management_existeLoteAsignado_FacturaResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_existeLoteAsignado_Factura(idFac).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
                return lista;
            }
        }
        public management_prestadoresRegionalIdPrResult PrestadorRegional(int idPrestador)
        {
            management_prestadoresRegionalIdPrResult dato = new management_prestadoresRegionalIdPrResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_prestadoresRegionalIdPr(idPrestador).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }
        public List<vw_sis_auditor_regional> UsuariosxRegional(int idRegional)
        {
            List<vw_sis_auditor_regional> dato = new List<vw_sis_auditor_regional>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.vw_sis_auditor_regional.Where(x => x.id_regional == idRegional).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<ref_cuentas_medicas_analista> getAnalistasAsignadosprestador(int idPrestador)
        {
            List<ref_cuentas_medicas_analista> result = new List<ref_cuentas_medicas_analista>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ref_cuentas_medicas_analista.Where(x => x.id_prestador == idPrestador && x.activo == 1).ToList();
                    return result;
                }
            }
            catch
            {
                return result;
            }
        }

        public ref_cuentas_medicas_analista ListadoActualizarAnalistas(int idPrestador, int idAnalista)
        {
            ref_cuentas_medicas_analista listado = new ref_cuentas_medicas_analista();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.ref_cuentas_medicas_analista.Where(x => x.id_prestador == idPrestador && x.id_usuario == idAnalista).FirstOrDefault();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }


        public List<management_facturacion_tableroControlResult> ListadoMedicamentosFacturas(DateTime fechaIni, DateTime fechaFin, string identificacion)
        {
            List<management_facturacion_tableroControlResult> listado = new List<management_facturacion_tableroControlResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_facturacion_tableroControl(Convert.ToDateTime(fechaIni), fechaFin, identificacion).ToList();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }

        public List<management_facturacion_consolidado_listaResult> ListadoMedicamentosFacturasConsolidadoLista(DateTime fechaIni, DateTime fechaFin)
        {
            List<management_facturacion_consolidado_listaResult> listado = new List<management_facturacion_consolidado_listaResult>();
            string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    SqlConnection conn = new SqlConnection(conexion);
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = conn;
                    cmd = conn.CreateCommand();
                    cmd.Connection.Open();
                    cmd.CommandText = "set arithabort on;";
                    cmd.ExecuteNonQuery();

                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@fechaIni", SqlDbType.DateTime);
                    param[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime);
                    param[0].Value = fechaIni;
                    param[1].Value = fechaFin;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "management_facturacion_consolidado_lista";
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 3000;
                    cmd.Parameters.AddRange(param);

                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    foreach (DataRow row in dt.Rows)
                    {
                        management_facturacion_consolidado_listaResult obj = new management_facturacion_consolidado_listaResult();
                        if (!string.IsNullOrEmpty(row["mes"].ToString()))
                        {
                            obj.mes = Convert.ToInt32(row["mes"].ToString());
                        }

                        obj.descripcion = Convert.ToString(row["descripcion"].ToString());

                        if (!string.IsNullOrEmpty(row["año"].ToString()))
                        {
                            obj.año = Convert.ToInt32(row["año"].ToString());
                        }

                        obj.numero_factura = Convert.ToString(row["numero_factura"].ToString());

                        if (!string.IsNullOrEmpty(row["fecha_factura"].ToString()))
                        {
                            obj.fecha_factura = Convert.ToDateTime(row["fecha_factura"].ToString());
                        }

                        if (!string.IsNullOrEmpty(row["valorFactura"].ToString()))
                        {
                            obj.valorFactura = Convert.ToDecimal(row["valorFactura"].ToString());
                        }


                        listado.Add(obj);
                    }

                    return listado;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }

        public managemenet_prestadores_traerDatosFacturasResult ListadoFacturasIdAf(int id_af)
        {
            managemenet_prestadores_traerDatosFacturasResult listado = new managemenet_prestadores_traerDatosFacturasResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.managemenet_prestadores_traerDatosFacturas(id_af).FirstOrDefault();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }


        public managemenet_prestadores_traerDatosFacturas_idDetalleResult ListadoFacturasIdDetalle(int? idDetalle)
        {
            managemenet_prestadores_traerDatosFacturas_idDetalleResult listado = new managemenet_prestadores_traerDatosFacturas_idDetalleResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    listado = db.managemenet_prestadores_traerDatosFacturas_idDetalle(idDetalle).FirstOrDefault();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }

        public managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult ListadoFacturasIdDetalleAuditor(int? idDetalle)
        {
            managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult listado = new managemenet_prestadores_traerDatosFacturasAuditor_idDetalleResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    listado = db.managemenet_prestadores_traerDatosFacturasAuditor_idDetalle(idDetalle).FirstOrDefault();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return listado;
            }
        }
        public List<ref_componente_hospitalario> GetComponentesHospitalarios()
        {
            List<ref_componente_hospitalario> list = new List<ref_componente_hospitalario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_componente_hospitalario.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }


        public List<ref_analista_lote> TraerAnalistaLoteExistente(int idlote)
        {
            List<ref_analista_lote> lista = new List<ref_analista_lote>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_analista_lote.Where(x => x.id_lote_facturas == idlote).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public vw_calidad_gestor_documental_insumos TarerArchivoInsumosId(int id)
        {
            vw_calidad_gestor_documental_insumos dato = new vw_calidad_gestor_documental_insumos();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.vw_calidad_gestor_documental_insumos.Where(x => x.id_calidad_gestor_documental_insumos == id).FirstOrDefault();
                    return dato;
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public management_fisRips_prestadorTieneContratoResult UnPrestadorTeieneNegociacion(string nit)
        {
            management_fisRips_prestadorTieneContratoResult dato = new management_fisRips_prestadorTieneContratoResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisRips_prestadorTieneContrato(nit).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        #endregion

        #region INVENTARIO FACTURAS CONTABILIZADAS

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo para consultar el estado de facturas medianto su estado
        /// </summary>
        /// <param name="idEstado"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadasResult> ConsultarInventarioFacturasPorEstado(int idEstado, DateTime? fechainicio, DateTime? fechafinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<Management_inventario_facturas_contabilizadasResult> result = new List<Management_inventario_facturas_contabilizadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    result = db.Management_inventario_facturas_contabilizadas(fechainicio, fechafinal, regional, idEstado).ToList();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Datos consultados correctamente";
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de consultar los datos: " + ex.Message;
                return result;
            }
        }


        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el estado de facturas 
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_cordinacionResult> ConsultarInventarioFacturasCordinacion(int mes, int año, int regional, ref MessageResponseOBJ MsgRes)
        {
            List<Management_inventario_facturas_contabilizadas_cordinacionResult> result = new List<Management_inventario_facturas_contabilizadas_cordinacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 10000;
                    result = db.Management_inventario_facturas_contabilizadas_cordinacion(mes, año, regional).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Datos consultados correctamente";
                    return result;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de consultar los datos: " + ex.Message;
                return result;
            }
        }

        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el inventario de facturas contabilizadas pero consolidado por mes, año y regional
        /// </summary>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_consolidadoResult> ConsultarInventarioFacturasConsolidado()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                db.CommandTimeout = 80000;
                return db.Management_inventario_facturas_contabilizadas_consolidado().ToList();
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar la gestion realizada a una factura contabilizada que fue cargada a traves del id Detalle
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_gestion consultarGestionFacturaContabilizadaporIdDetalle(int idDetalle)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.inventario_facturas_contabilizadas_gestion.Where(l => l.inventario_cargue_dtll_id == idDetalle).FirstOrDefault();
            }
        }

        public inventario_facturas_contabilizadas_gestion consultarGestionFacturaContabilizadaporIdGestion(int idGestion)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.inventario_facturas_contabilizadas_gestion.Where(l => l.id_gestion_archivo == idGestion).FirstOrDefault();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para consultar los archivos cargados por id
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <param name=""></param>
        public inventario_facturas_contabilizadas_gestor_documental ConsultarRegistroArchivoCargadoPorId(int idArchivo, ref MessageResponseOBJ MsgRes)
        {

            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.inventario_facturas_contabilizadas_gestor_documental.Where(l => l.id_documento == idArchivo).FirstOrDefault();
            }
        }

        public List<inventario_facturas_contabilizadas_gestor_documental> ConsultarRegistroArchivoCargadoPorIdLista(int? mes, int? año, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<inventario_facturas_contabilizadas_gestor_documental> lista = new List<inventario_facturas_contabilizadas_gestor_documental>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.inventario_facturas_contabilizadas_gestor_documental.Where(l => l.mes == mes && l.año == año && l.id_regional == regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 02 de diciembre de 2022
        /// Metodo utilizado para validar la existencia de un periodo que vayana a cargar
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_carguebase ConsultarExistenciaPeriodoCargado(int mes, int año, int regional)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.inventario_facturas_contabilizadas_carguebase.Where(l => l.mes == mes && l.año == año && l.regional == regional).FirstOrDefault();
            }
        }

        public List<management_inventario_facturas_contabilizadas_reporteResult> ReporteInventarioContabilizadas(int estado)
        {
            List<management_inventario_facturas_contabilizadas_reporteResult> list = new List<management_inventario_facturas_contabilizadas_reporteResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.management_inventario_facturas_contabilizadas_reporte(estado).OrderByDescending(x => x.factura).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        #endregion

        public md_prefactura_contador TraerDatosContadorPrefacturas(int idDetallePrefactura)
        {
            md_prefactura_contador dato = new md_prefactura_contador();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.md_prefactura_contador.Where(x => x.id_detalle == idDetallePrefactura).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }
        //public List<management_prefacturas_notificacionOPLResult> ListaDatoaReportePrefacturasaOPL(int? idCargue)
        //{
        //    List<management_prefacturas_notificacionOPLResult> lista = new List<management_prefacturas_notificacionOPLResult>();
        //    try
        //    {
        //        using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
        //        {
        //            lista = db.management_prefacturas_notificacionOPL(idCargue).ToList();
        //            return lista;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        return lista;
        //    }
        //}

        public List<management_prefacturas_notificacionOPLNoPasanResult> ListaDatoaReportePrefacturasaOPLNoPasan(int? idCargue)
        {
            List<management_prefacturas_notificacionOPLNoPasanResult> lista = new List<management_prefacturas_notificacionOPLNoPasanResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_prefacturas_notificacionOPLNoPasan(idCargue).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_prefacturas_notificacionOPLPasanResult> ListaDatoaReportePrefacturasaOPLPasan(int? idCargue)
        {
            List<management_prefacturas_notificacionOPLPasanResult> lista = new List<management_prefacturas_notificacionOPLPasanResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_prefacturas_notificacionOPLPasan(idCargue).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_prefacturas_listaMedicamentosReguladosResult> ListaMedicamentosRegulados()
        {
            List<management_prefacturas_listaMedicamentosReguladosResult> lista = new List<management_prefacturas_listaMedicamentosReguladosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_prefacturas_listaMedicamentosRegulados().ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }

        public List<management_prefacturas_tableroCierreResult> TableroInformacionCierrePrefacturas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_prefacturas_tableroCierreResult> lista = new List<management_prefacturas_tableroCierreResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_prefacturas_tableroCierre(fechaInicio, fechaFin).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }
        public List<management_prefacturas_tableroAhorroResult> TableroInformacionAhorroPrefacturas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<management_prefacturas_tableroAhorroResult> lista = new List<management_prefacturas_tableroAhorroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_prefacturas_tableroAhorro(fechaInicio, fechaFin).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_prefacturas_reporteCierreResult> ReportePrefacturasCerradas(int? idCargue)
        {
            List<management_prefacturas_reporteCierreResult> lista = new List<management_prefacturas_reporteCierreResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_prefacturas_reporteCierre(idCargue).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;

        }

        public management_prefacturas_existeBeneficiarioResult PrefacturasExisteBeneficiario(string numeroBeneficiario, DateTime fechaDespachoFormula)
        {
            management_prefacturas_existeBeneficiarioResult dato = new management_prefacturas_existeBeneficiarioResult();
            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_prefacturas_existeBeneficiario(numeroBeneficiario, fechaDespachoFormula).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        //public management_prefacturas_numeroFacturaResult PrefacturasExisteFactura(string numeroBeneficiario, string numeroUnidades, DateTime fechaDespachoFormula, string vlrUnidades
        //    , string cum, string nombreComercial)
        //{
        //    management_prefacturas_numeroFacturaResult dato = new management_prefacturas_numeroFacturaResult();
        //    try
        //    {
        //        using (var db = new ECOPETROL_DataContexDataContext())
        //        {
        //            dato = db.management_prefacturas_numeroFactura(numeroBeneficiario, numeroUnidades, fechaDespachoFormula, vlrUnidades, cum, nombreComercial).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }

        //    return dato;
        //}

        public management_prefacturas_regionalBeneficiarioResult PrefacturasRegionalBeneficiario(string numeroBeneficiario, DateTime fechaDespachoFormula, string nombreEspecial)
        {
            management_prefacturas_regionalBeneficiarioResult dato = new management_prefacturas_regionalBeneficiarioResult();
            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_prefacturas_regionalBeneficiario(numeroBeneficiario, fechaDespachoFormula, nombreEspecial).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public string PrefacturasExisteFactura(string numeroBeneficiario, int numeroUnidades, DateTime fechaDespachoFormula, decimal vlrUnidades
            , string cum, string nombreComercial)
        {
            string resultado = "";

            try
            {
                using (var db = new ECOPETROL_DataContexDataContext())
                {
                    resultado = db.medicamentos_facturacion_dtll.Where(x => x.identificacion_paciente == numeroBeneficiario && x.cantidad == numeroUnidades
                    && x.fecha_dispensacion == fechaDespachoFormula
                    && x.valor == vlrUnidades
                    && (x.cum == cum || x.concepto == nombreComercial)
                    ).Select(x => x.numero_factura).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return resultado;
        }


        public int TraerUltimoCargueLogAprobacion()
        {
            var respuesta = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    respuesta = db.log_prefacturas_aprobacionMasiva.OrderByDescending(x => x.id_log).Select(x => x.id_log).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }


        public int TraerUltimoCargueLogDesaprobacion()
        {
            var respuesta = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    respuesta = db.log_prefacturas_desaprobacionMasiva.OrderByDescending(x => x.id_log).Select(x => x.id_log).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }




        public List<management_Validador_datosCorreosResult> ListadoCorreosValidadorOPL(int? idRegional)
        {
            List<management_Validador_datosCorreosResult> lista = new List<management_Validador_datosCorreosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_Validador_datosCorreos(idRegional).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }
        public ecop_concurrencia traerDatosBeneficiarioConcurrencia(string documentoBen)
        {
            ecop_concurrencia dato = new ecop_concurrencia();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_concurrencia.Where(x => x.id_afi == documentoBen).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<ecop_concurrencia_evolucion_procedimientos> traerDatosEvolucionProcedimientos(int id_evolucion)
        {
            List<ecop_concurrencia_evolucion_procedimientos> dato = new List<ecop_concurrencia_evolucion_procedimientos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ecop_concurrencia_evolucion_procedimientos.Where(x => x.id_evolucion == id_evolucion).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<management_egresos_categorizacionResult> listadoEgresosCategorizacion(int idConcurrencia)
        {
            List<management_egresos_categorizacionResult> lista = new List<management_egresos_categorizacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_egresos_categorizacion(idConcurrencia).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return lista;
            }
        }


        public List<management_alertas_epidemiologicas_tableroControlResult> ListadoAlertasEpidemiologicas()
        {
            List<management_alertas_epidemiologicas_tableroControlResult> lista = new List<management_alertas_epidemiologicas_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_alertas_epidemiologicas_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<vw_reporte_alertas_epidemiologia> ListadoAlertasEpidemiologicasReporte()
        {
            List<vw_reporte_alertas_epidemiologia> lista = new List<vw_reporte_alertas_epidemiologia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.vw_reporte_alertas_epidemiologia.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<vw_reporte_alertas_epidemiologia_gestiones> ListadoAlertasEpidemiologicasReporteGestiones()
        {
            List<vw_reporte_alertas_epidemiologia_gestiones> lista = new List<vw_reporte_alertas_epidemiologia_gestiones>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.vw_reporte_alertas_epidemiologia_gestiones.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_alertas_epidemiologicas_tableroControl_idGestionResult TraerDatosGestionId(int? idRegistro)
        {
            management_alertas_epidemiologicas_tableroControl_idGestionResult dato = new management_alertas_epidemiologicas_tableroControl_idGestionResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    dato = db.management_alertas_epidemiologicas_tableroControl_idGestion(idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> ListadoAlertasEpidemiologicasGestionadas()
        {
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> lista = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lista = db.management_alertas_epidemiologicas_tableroControl_gestionadas().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }



        public List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> ListadoAlertasEpidemiologicasGestionadasTotal()
        {
            List<management_alertas_epidemiologicas_tableroControl_gestionadasResult> lista = new List<management_alertas_epidemiologicas_tableroControl_gestionadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    lista = db.management_alertas_epidemiologicas_tableroControl_gestionadas().Where(x => x.id_registro != null).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_alertas_epidemiologicas_gestionesResult TraerGestionAlertasEpidemiologicas(int? idRegistro)
        {
            management_alertas_epidemiologicas_gestionesResult dato = new management_alertas_epidemiologicas_gestionesResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    dato = db.management_alertas_epidemiologicas_gestiones(idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<alerta_epidemiologica_gestion_archivos> ListadoArchivosEpidemiologicaId(int? idRegistro, int? tipo)
        {
            List<alerta_epidemiologica_gestion_archivos> lista = new List<alerta_epidemiologica_gestion_archivos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.alerta_epidemiologica_gestion_archivos.Where(x => x.id_gestion == idRegistro && x.tipo_Archivo == tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public alerta_epidemiologica_gestion_archivos TraerArchivoEpidemiologicoId(int? id)
        {
            alerta_epidemiologica_gestion_archivos dato = new alerta_epidemiologica_gestion_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.alerta_epidemiologica_gestion_archivos.FirstOrDefault(x => x.id_registro == id);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<ref_AE_tiposDemoras> ListadoAE()
        {
            List<ref_AE_tiposDemoras> listado = new List<ref_AE_tiposDemoras>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.ref_AE_tiposDemoras.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }



        public List<ref_AE_tiposDemoras_detallado> ListadoAEDetallado()
        {
            List<ref_AE_tiposDemoras_detallado> listado = new List<ref_AE_tiposDemoras_detallado>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.ref_AE_tiposDemoras_detallado.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<management_alertaEpidemiologica_reporteIdResult> TraerGestionAnalisis(int? idGestion)
        {
            List<management_alertaEpidemiologica_reporteIdResult> dato = new List<management_alertaEpidemiologica_reporteIdResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_alertaEpidemiologica_reporteId(idGestion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;

        }

        public List<management_alertaEpidemiologica_reporteId_demorasResult> TraerGestionAnalisis_demoras(int? idGestion)
        {
            List<management_alertaEpidemiologica_reporteId_demorasResult> dato = new List<management_alertaEpidemiologica_reporteId_demorasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_alertaEpidemiologica_reporteId_demoras(idGestion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;

        }

        public List<management_alertas_epidemiologicas_descargableArchivos_documentosResult> TraerDocumentosArchivosGestionAE(int? regional, int? localidad, int? unis, DateTime? fechaAlertaIni, DateTime? fechaAlertaFin,
    string documentoPaciente, int? idConcurrencia, DateTime? fechaIngresoIni, DateTime? fechaIngresoFin, int? conEgreso)
        {
            List<management_alertas_epidemiologicas_descargableArchivos_documentosResult> dato = new List<management_alertas_epidemiologicas_descargableArchivos_documentosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_alertas_epidemiologicas_descargableArchivos_documentos(regional, localidad, unis, fechaAlertaIni, fechaAlertaFin, documentoPaciente, idConcurrencia, fechaIngresoIni, fechaIngresoFin, conEgreso).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;

        }

        public List<management_alertas_epidemiologicas_descargableArchivosResult> TraerArchivosGestionAE(string documentoPaciente)
        {
            List<management_alertas_epidemiologicas_descargableArchivosResult> dato = new List<management_alertas_epidemiologicas_descargableArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_alertas_epidemiologicas_descargableArchivos(documentoPaciente).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;

        }

        public alerta_epidemiologica_gestion_gestionAnalisis TraerGestionAEId(int? idGestion)
        {
            alerta_epidemiologica_gestion_gestionAnalisis dato = new alerta_epidemiologica_gestion_gestionAnalisis();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    dato = db.alerta_epidemiologica_gestion_gestionAnalisis.FirstOrDefault(x => x.id_gestionAnalisis == idGestion);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> TraerGestionAEIdDemoras(int? idGestion)
        {
            List<alerta_epidemiologica_gestion_gestionAnalisis_demoras> lista = new List<alerta_epidemiologica_gestion_gestionAnalisis_demoras>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.alerta_epidemiologica_gestion_gestionAnalisis_demoras.Where(x => x.id_gestionAnalisis == idGestion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult> TraerGestionAEIdDemorasDetallado(int? idGestion)
        {
            List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult> lista = new List<management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetalladoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_alertas_epidemiologicas_alerta_epidemiologica_gestion_gestionAnalisisDetallado(idGestion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_evoluciones_listadoIdConcurrenciaResult> TraerEvolucionesIdConcurrencia(int? idConcurrencia)
        {
            List<management_evoluciones_listadoIdConcurrenciaResult> lista = new List<management_evoluciones_listadoIdConcurrenciaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_evoluciones_listadoIdConcurrencia(idConcurrencia).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        #region INVENTARIO ALTO COSTO

        public List<management_inventario_altoCosto_tableroResult> ListadoInventarioAltoCosto()
        {
            List<management_inventario_altoCosto_tableroResult> lista = new List<management_inventario_altoCosto_tableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_inventario_altoCosto_tablero().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_inventario_altoCostoCancer_atc> listadoInventarioATC()
        {
            List<ref_inventario_altoCostoCancer_atc> lista = new List<ref_inventario_altoCostoCancer_atc>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_inventario_altoCostoCancer_atc.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<inventario_altoCosto_gestiones> listaInvAltoCostoGestionadas(int? idDetalle)
        {
            List<inventario_altoCosto_gestiones> lista = new List<inventario_altoCosto_gestiones>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.inventario_altoCosto_gestiones.Where(x => x.id_detalle == idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;

        }

        public inventario_altoCosto_gestiones DatoInvAltoCostoGestionID(int? idGestion)
        {
            inventario_altoCosto_gestiones dato = new inventario_altoCosto_gestiones();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.inventario_altoCosto_gestiones.Where(x => x.id_gestion == idGestion).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public inventario_altoCosto_gestiones DatoUltimoInvAltoCostoGestionID(int? idDetalle)
        {
            inventario_altoCosto_gestiones dato = new inventario_altoCosto_gestiones();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.inventario_altoCosto_gestiones.Where(x => x.id_detalle == idDetalle).OrderByDescending(x => x.id_gestion).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_inventario_altoCosto_verArchivosResult> ListadoArchivosGestionados(int? idGestion)
        {
            List<management_inventario_altoCosto_verArchivosResult> archivos = new List<management_inventario_altoCosto_verArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    archivos = db.management_inventario_altoCosto_verArchivos(idGestion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return archivos;
        }

        public inventario_altoCosto_archivos traerArchivoAltoCostoIdArchivo(int? idArchivo)
        {
            inventario_altoCosto_archivos dato = new inventario_altoCosto_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.inventario_altoCosto_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_inventario_altoCosto_tableroGestionesResult> ListaAltoCostoGestiones(int? idDetalle)
        {
            List<management_inventario_altoCosto_tableroGestionesResult> archivos = new List<management_inventario_altoCosto_tableroGestionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    archivos = db.management_inventario_altoCosto_tableroGestiones(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return archivos;
        }


        public List<ref_cargue_cuentas_altoCosto> listadoCargueGsdRastreo()
        {
            List<ref_cargue_cuentas_altoCosto> lista = new List<ref_cargue_cuentas_altoCosto>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cargue_cuentas_altoCosto.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_cargue_cuentas_altoCosto_estados> listadoEstadosCuentaAltoCosto()
        {
            List<ref_cargue_cuentas_altoCosto_estados> lista = new List<ref_cargue_cuentas_altoCosto_estados>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cargue_cuentas_altoCosto_estados.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_rastreosResult> ListadoDatosRastreoTotal(int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosResult> lista = new List<management_cuentasAltoCosto_rastreosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_rastreos(tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_cuentasAltoCosto_gestionesResult> listadoGestionesAltoCosto(int? idRegistro, int? tipo)
        {
            List<management_cuentasAltoCosto_gestionesResult> lista = new List<management_cuentasAltoCosto_gestionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_gestiones(idRegistro, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_repositorioResult> CuentasAltoCostoRepositorio(int? idRegistro, int? tipo)
        {
            List<management_cuentasAltoCosto_repositorioResult> lista = new List<management_cuentasAltoCosto_repositorioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_repositorio(idRegistro, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_cuentas_altocosto_tipoSoporte> tipoSoporteCAC()
        {
            List<ref_cuentas_altocosto_tipoSoporte> lista = new List<ref_cuentas_altocosto_tipoSoporte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cuentas_altocosto_tipoSoporte.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_rastreosConfirmadosResult> ListadoDatosCuentasAltoCostoConfirmados(int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosConfirmadosResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmadosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_cuentasAltoCosto_rastreosConfirmados(tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public cargue_cuentas_altoCosto_archivos TraerArchivoRepositorio(int? idArchivo)
        {
            cargue_cuentas_altoCosto_archivos lista = new cargue_cuentas_altoCosto_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.cargue_cuentas_altoCosto_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult> ListadoDatosCuentasAltoCostoConfirmadosConArchivos()
        {
            List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_rastreosConfirmados_conArchivo().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult> ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada()
        {
            List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_rastreosConfirmados_conArchivoCompleta().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult> ListadoObservacionesCuentasAltoCostoGestionadas(int? idRegistro, int? tipo)
        {
            List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult> lista = new List<management_cuentasAltoCosto_rastreosConfirmados_observacionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_rastreosConfirmados_observaciones(idRegistro, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_consolidadoArchivosResult> ListaArchivosPorDocumentoCAC(string documento, int? tipo)
        {
            List<management_cuentasAltoCosto_consolidadoArchivosResult> lista = new List<management_cuentasAltoCosto_consolidadoArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_consolidadoArchivos(documento, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasAltoCosto_documentosArchivosResult> DocumentosConArchivos(int? año, string mes, string regional, int? tipo, string documento)
        {
            List<management_cuentasAltoCosto_documentosArchivosResult> lista = new List<management_cuentasAltoCosto_documentosArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasAltoCosto_documentosArchivos(año, mes, regional, tipo, documento).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        #endregion

        #region CONTRATOS

        public List<management_contratos_listadoResult> listadoContratos()
        {
            List<management_contratos_listadoResult> lista = new List<management_contratos_listadoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_contratos_listado().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public contratos_detalle MostrarDatosContratoId(int? idContrato)
        {
            contratos_detalle dato = new contratos_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.contratos_detalle.Where(x => x.id_contrato == idContrato).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public contratos_detalle MostrarDetallePContrato(string sap)
        {
            contratos_detalle dato = new contratos_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.contratos_detalle.Where(x => x.documento_SAP.Contains(sap)).OrderByDescending(x => x.id_contrato).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        #endregion

        #region CARGUE MASIVO RIPS DEPURADOS

        public rips_depurados_carguebase ConsultarCargueBaseRipsDepurados(string tipoRips, int Mes, int Año)
        {
            using (ANALITICA_DataContexDataContext db = new ANALITICA_DataContexDataContext())
            {
                return db.rips_depurados_carguebase.Where(l => l.tipo_archivo_rips == tipoRips && l.id_mes == Mes && l.id_año == Año).FirstOrDefault();
            }
        }
        #endregion


        #region REEMBOLSO

        public List<management_reembolsos_tableroResult> listadoReembolsosIngresados(int? idRegional)
        {
            List<management_reembolsos_tableroResult> lista = new List<management_reembolsos_tableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_reembolsos_tablero(idRegional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_reembolsos_tablero_gestionadosResult> listadoReembolsosGestionados(int? idRegional)
        {
            List<management_reembolsos_tablero_gestionadosResult> lista = new List<management_reembolsos_tablero_gestionadosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_reembolsos_tablero_gestionados(idRegional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_unis_reembolsos> UnisReembolsos()
        {
            List<ref_unis_reembolsos> lista = new List<ref_unis_reembolsos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_unis_reembolsos.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_reembolsos_gestionResult> listadoReembolsosIngresadosGestiones(int? idReembolso)
        {
            List<management_reembolsos_gestionResult> lista = new List<management_reembolsos_gestionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_reembolsos_gestion(idReembolso).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentas_reembolso_ArchivosResult> listadoReembolsosArchivos(int? idReembolso)
        {
            List<management_cuentas_reembolso_ArchivosResult> lista = new List<management_cuentas_reembolso_ArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentas_reembolso_Archivos(idReembolso).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public cuentas_reembolsos TraerDatosReembolso(int? idReembolso)
        {
            cuentas_reembolsos dato = new cuentas_reembolsos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cuentas_reembolsos.Where(x => x.id_reembolso == idReembolso).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public cuentas_reembolsos_archivos TrarArchivoReembolso(int? idArchivo)
        {
            cuentas_reembolsos_archivos dato = new cuentas_reembolsos_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cuentas_reembolsos_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        #endregion REEMBOLSO

        #region NORIPS

        public List<management_usuariosAnalistas_noripsResult> ListadoAnalistas()
        {
            List<management_usuariosAnalistas_noripsResult> lista = new List<management_usuariosAnalistas_noripsResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_usuariosAnalistas_norips().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<Total_Departamento> TraerDepartamentos()
        {
            List<Total_Departamento> lista = new List<Total_Departamento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Total_Departamento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public Total_Departamento TraerDepartamentoId(int? id)
        {
            Total_Departamento dato = new Total_Departamento();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.Total_Departamento.Where(x => x.id_dep == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }
        public List<management_total_departamentosResult> TraerDepartamentosRegional(int? regional)
        {
            List<management_total_departamentosResult> dato = new List<management_total_departamentosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_total_departamentos(regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<ref_cuentasmedicas_notips_motivos> ListaMotivosNoRips()
        {
            List<ref_cuentasmedicas_notips_motivos> lista = new List<ref_cuentasmedicas_notips_motivos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cuentasmedicas_notips_motivos.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasMedicas_ripsNoEsaludResult> TableroRipsNoEsalud(DateTime? fechaInicio, DateTime? fechaFin, int? regional)
        {
            List<management_cuentasMedicas_ripsNoEsaludResult> lista = new List<management_cuentasMedicas_ripsNoEsaludResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 5000;
                    lista = db.management_cuentasMedicas_ripsNoEsalud(fechaInicio, fechaFin, regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_cuentasMedicas_ripsNoEsalud_archivosResult> TableroRepositorioRipsNoEsalud(int? idMed)
        {
            List<management_cuentasMedicas_ripsNoEsalud_archivosResult> lista = new List<management_cuentasMedicas_ripsNoEsalud_archivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_cuentasMedicas_ripsNoEsalud_archivos(idMed).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public cuentas_medicas_norips_Archivos traerArchivoNoRips(int idArchivo)
        {
            cuentas_medicas_norips_Archivos dato = new cuentas_medicas_norips_Archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cuentas_medicas_norips_Archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult> TraerTodosLosArchivosRipsNoEsalud(DateTime? fechaInicio, DateTime? fechaFin, int? regional)
        {
            List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult> dato = new List<management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_cuentasMedicas_ripsNoEsalud_TodosArchivos(fechaInicio, fechaFin, regional).ToList();
                    return dato;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return dato;
            }
        }

        public List<vw_recep_facturas_cargue_base> GetHistoricoRadicacionById(int idcargue)
        {
            List<vw_recep_facturas_cargue_base> result = new List<vw_recep_facturas_cargue_base>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    result = db.vw_recep_facturas_cargue_base.Where(l => l.id_cargue_base == idcargue).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public List<ManagmentFacturasV2Result> GetFacturasByRecepcionV2(int idrecepcion)
        {
            List<ManagmentFacturasV2Result> result = new List<ManagmentFacturasV2Result>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    result = db.ManagmentFacturasV2(idrecepcion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }

        public List<management_baseBeneficiarios_xDocumentoResult> BusquedaBeneficiarioCedula(string documento)
        {
            List<management_baseBeneficiarios_xDocumentoResult> lista = new List<management_baseBeneficiarios_xDocumentoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_baseBeneficiarios_xDocumento(documento).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #endregion NORIPS

        #region CAMPAÑA

        public List<management_campana_tableroControlResult> ListadoCampanas()
        {
            List<management_campana_tableroControlResult> lista = new List<management_campana_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_campana_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public ref_campanas_permisosEdicion traerPermisosEdicionCampana(int? idUsuario)
        {
            ref_campanas_permisosEdicion dato = new ref_campanas_permisosEdicion();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_campanas_permisosEdicion.Where(x => x.id_usuario == idUsuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public creacion_campana TraerCampanaGeneralId(int? id)
        {
            creacion_campana dato = new creacion_campana();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.creacion_campana.Where(x => x.id_campana == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public creacion_campana TraerCampanaVersionGeneralId(int? id)
        {


            creacion_campana dato = new creacion_campana();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.creacion_campana.Where(x => x.id_campana == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;



        }



        public List<creacion_campana_detalle> TraerCampanaGeneraDetallelId(int? id)
        {
            List<creacion_campana_detalle> lista = new List<creacion_campana_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.creacion_campana_detalle.Where(x => x.id_campana == id && x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<creacion_campana_camposSimples> TraerCampanaCamposSimplesIdCampana(int? id)
        {
            List<creacion_campana_camposSimples> lista = new List<creacion_campana_camposSimples>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.creacion_campana_camposSimples.Where(x => x.id_campana == id && x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<creacion_campana_listas> TraerCampanaCamposListaIdCampana(int? id)
        {
            List<creacion_campana_listas> lista = new List<creacion_campana_listas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.creacion_campana_listas.Where(x => x.id_campana == id && x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<ref_campana_tipoPregunta> TraerTipoPreguntaCampaña()
        {
            List<ref_campana_tipoPregunta> lista = new List<ref_campana_tipoPregunta>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_campana_tipoPregunta.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public creacion_campana_camposSimples TraerCampanaCamposSimplesIdDetalle(int? id)
        {
            creacion_campana_camposSimples dato = new creacion_campana_camposSimples();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.creacion_campana_camposSimples.Where(x => x.id_detalle == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<creacion_campana_listas> TraerCampanaCamposListaIdDetalle(int? id)
        {
            List<creacion_campana_listas> lista = new List<creacion_campana_listas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.creacion_campana_listas.Where(x => x.id_detalle == id).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public creacion_campana_detalle TraerDatosPreguntaCampana(int? id)
        {
            creacion_campana_detalle dato = new creacion_campana_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.creacion_campana_detalle.Where(x => x.id_detalle == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<creacion_campana_detalle> ConsultaDtllPreguntasCampana(int? idcampana)
        {

            List<creacion_campana_detalle> dato = new List<creacion_campana_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.creacion_campana_detalle.Where(x => x.id_campana == idcampana && x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;

        }

        public List<ref_campaña_tipoFecha> TraertiposFechaCmpana()
        {
            List<ref_campaña_tipoFecha> lista = new List<ref_campaña_tipoFecha>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_campaña_tipoFecha.ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;

            }
            return lista;
        }

        public List<management_campana_reporteIdResult> ListadoCampanasId(int? idCampana)
        {
            List<management_campana_reporteIdResult> lista = new List<management_campana_reporteIdResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_campana_reporteId(idCampana).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        #endregion CAMPAÑA

        #region ALERTAS DISPENSACION

        public List<management_alertasDispensacion_tableroControlResult> ListadoAlertasDispensacion(DateTime? fecha_prescripcion, string numero_formula, string documento_beneficiario, string id_prescriptor, string nombre_comercial)
        {
            List<management_alertasDispensacion_tableroControlResult> lista = new List<management_alertasDispensacion_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 1000;
                    lista = db.management_alertasDispensacion_tableroControl(fecha_prescripcion, numero_formula, documento_beneficiario, id_prescriptor, nombre_comercial).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertasDispensacion_tableroControl_idResult> TraerRegistroAlerta(int? idRegistro)
        {
            List<management_alertasDispensacion_tableroControl_idResult> lista = new List<management_alertasDispensacion_tableroControl_idResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_tableroControl_id(idRegistro).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;

        }


        public List<ref_alertas_dispensacion_GestionTramite> ListadoTipotramite()
        {
            List<ref_alertas_dispensacion_GestionTramite> lista = new List<ref_alertas_dispensacion_GestionTramite>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_alertas_dispensacion_GestionTramite.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertasDispensacion_buscarNombreComercialResult> TraerNombreComercial(string nombre_comercial)
        {
            List<management_alertasDispensacion_buscarNombreComercialResult> lista = new List<management_alertasDispensacion_buscarNombreComercialResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_buscarNombreComercial(nombre_comercial).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_alertasDispensacion_tableroControl_gestionadasResult> ListadoAlertasDispensacionGestionadas()
        {
            List<management_alertasDispensacion_tableroControl_gestionadasResult> lista = new List<management_alertasDispensacion_tableroControl_gestionadasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_tableroControl_gestionadas().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult> ListadoAlertasDispensacionGestionadasDetallada()
        {
            List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult> lista = new List<management_alertasDispensacion_tableroControl_gestionadasDetalladaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_tableroControl_gestionadasDetallada().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult> ListadoAlertasDispensacionGestionadasArchivos(int? idDetalle)
        {
            List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult> lista = new List<management_alertasDispensacion_tableroControl_gestionadasArchivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_tableroControl_gestionadasArchivos(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertasDispensacion_tableroControl_respuestasResult> ListadoAlertasDispensacionGestiones(int? idDetalle)
        {
            List<management_alertasDispensacion_tableroControl_respuestasResult> lista = new List<management_alertasDispensacion_tableroControl_respuestasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_tableroControl_respuestas(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public alertas_dispensacion_detalle TraerDatosgestionTramite(int? idDetalle)
        {
            alertas_dispensacion_detalle datos = new alertas_dispensacion_detalle();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.alertas_dispensacion_detalle.FirstOrDefault(x => x.id_detalle == idDetalle);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return datos;
        }

        public List<management_alertasDispensacion_datosTramiteResult> TraerDatosAMTramite(int? idDetalle)
        {
            List<management_alertasDispensacion_datosTramiteResult> lista = new List<management_alertasDispensacion_datosTramiteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_datosTramite(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_alertasDispensacion_datosTramite_respuestasResult> TraerDatosAMTramiteRespuestas(int? idDetalle)
        {
            List<management_alertasDispensacion_datosTramite_respuestasResult> lista = new List<management_alertasDispensacion_datosTramite_respuestasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_alertasDispensacion_datosTramite_respuestas(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public alertas_dispensacion_detalle_archivos TraerArchivoAlertasDispen(int? idArchivo)
        {
            alertas_dispensacion_detalle_archivos dato = new alertas_dispensacion_detalle_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.alertas_dispensacion_detalle_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        #endregion

        #region MortalidadNatalidad

        public List<management_tiposBeneficiarioResult> TraerTiposBeneficiario()
        {
            List<management_tiposBeneficiarioResult> lista = new List<management_tiposBeneficiarioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_tiposBeneficiario().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public mortalidad_registros TraerDatosMortalidad(int? idMortalidad)
        {
            mortalidad_registros dato = new mortalidad_registros();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.mortalidad_registros.Where(x => x.id_mortalidad == idMortalidad).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return dato;
        }


        public mortalidad_registros TraerDatosMortalidadIdentificacion(string identificacion)
        {
            mortalidad_registros dato = new mortalidad_registros();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.mortalidad_registros.Where(x => x.numero_documento == identificacion).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return dato;
        }

        public List<management_TableroMortalidadResult> TraerMortalidadesTablero(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro)
        {
            List<management_TableroMortalidadResult> lista = new List<management_TableroMortalidadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_TableroMortalidad(año, trimestre, mes, unis, regional, documento, fechaFiltro).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public natalidad_registros TraerDatosNatalidad(int? idNatalidad)
        {
            natalidad_registros dato = new natalidad_registros();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.natalidad_registros.Where(x => x.id_natalidad == idNatalidad).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return dato;
        }

        public List<management_TableroNatalidadResult> TraerNatalidadesTablero(int? año, int? trimestre, int? mes, int? unis, int? regional, string documento, DateTime? fechaFiltro)
        {
            List<management_TableroNatalidadResult> lista = new List<management_TableroNatalidadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_TableroNatalidad(año, trimestre, mes, unis, regional, documento, fechaFiltro).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }
        #endregion

        #region EVENTOS EN SALUD

        public List<ref_eventosSalud_fuenteReporte> ListaEventosSaludFuenteReporte()
        {
            List<ref_eventosSalud_fuenteReporte> lista = new List<ref_eventosSalud_fuenteReporte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_fuenteReporte.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_ambitoOcurrencia> ListaEventosSaludAmbitoOcurrencia()
        {
            List<ref_eventosSalud_ambitoOcurrencia> lista = new List<ref_eventosSalud_ambitoOcurrencia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_ambitoOcurrencia.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_severidadDesenlace> ListaEventosSaludSeveridadDesenlace()
        {
            List<ref_eventosSalud_severidadDesenlace> lista = new List<ref_eventosSalud_severidadDesenlace>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_severidadDesenlace.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_ProbabilidadRepeticion> ListaEventosSaludProbabilidadRepeticion()
        {
            List<ref_eventosSalud_ProbabilidadRepeticion> lista = new List<ref_eventosSalud_ProbabilidadRepeticion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_ProbabilidadRepeticion.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_conceptoAuditoria> ListaEventosSaludConceptoAuditoria()
        {
            List<ref_eventosSalud_conceptoAuditoria> lista = new List<ref_eventosSalud_conceptoAuditoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_conceptoAuditoria.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_seguimiento> ListaEventosSaludSeguimiento()
        {
            List<ref_eventosSalud_seguimiento> lista = new List<ref_eventosSalud_seguimiento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_seguimiento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_eventosSalud_categoriaEvento> ListaEventosSaludCategoria()
        {
            List<ref_eventosSalud_categoriaEvento> lista = new List<ref_eventosSalud_categoriaEvento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_categoriaEvento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }
        public List<ref_eventosSalud_subCategoriaEvento> ListaEventosSaludSubCategoria()
        {
            List<ref_eventosSalud_subCategoriaEvento> lista = new List<ref_eventosSalud_subCategoriaEvento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_subCategoriaEvento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_eventosSalud_subCategoriaEvento> EventosSaludTraerSubCategoriaId(int? idCategoria)
        {
            List<ref_eventosSalud_subCategoriaEvento> dato = new List<ref_eventosSalud_subCategoriaEvento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_eventosSalud_subCategoriaEvento.Where(x => x.id_categoria == idCategoria).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<ref_eventosSalud_resultadoNegativo> ListaEventosSaludResNegativoIdCategoriaClasificacion(int? idCategoria, int? idClasificacin)
        {
            List<ref_eventosSalud_resultadoNegativo> lista = new List<ref_eventosSalud_resultadoNegativo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_resultadoNegativo.Where(x => x.id_categoria == idCategoria && x.id_clasificacion == idClasificacin).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_eventosSalud_resultadoNegativo> ListaEventosSaludResNegativo()
        {
            List<ref_eventosSalud_resultadoNegativo> lista = new List<ref_eventosSalud_resultadoNegativo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_resultadoNegativo.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_eventosSalud_clasificacionEvento> ListaEventosSaludClasificacion()
        {
            List<ref_eventosSalud_clasificacionEvento> lista = new List<ref_eventosSalud_clasificacionEvento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_eventosSalud_clasificacionEvento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_eventosSalud_tableroResult> ListadoEventosEnSaludTablero(int? mes, int? año)
        {
            List<management_eventosSalud_tableroResult> lista = new List<management_eventosSalud_tableroResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_eventosSalud_tablero(mes, año).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public eventos_salud_registros TraerDatosEventosSaludId(int? idEvento)
        {
            eventos_salud_registros dato = new eventos_salud_registros();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.eventos_salud_registros.Where(x => x.id_evento == idEvento).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        #endregion

        #region vistas

        public List<Ref_regional> listadoRegionalesIndice(string indice)
        {
            List<Ref_regional> lista = new List<Ref_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Ref_regional.Where(x => x.indice == indice).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<cronograma_visita_documento> ObtenerListadoDocumentosVisitas()
        {

            List<cronograma_visita_documento> dato = new List<cronograma_visita_documento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.cronograma_visita_documento.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;

        }

        public List<management_cronograma_visita_traerByteResult> ObtenerListadoDocumentosVisitasSinRuta()
        {
            List<management_cronograma_visita_traerByteResult> dato = new List<management_cronograma_visita_traerByteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    dato = db.management_cronograma_visita_traerByte().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;

        }

        #endregion

        #region ENCUESTA SAMI

        public List<management_encuesta_tipoPreguntaResult> listaEncuestaSAMI()
        {
            List<management_encuesta_tipoPreguntaResult> lista = new List<management_encuesta_tipoPreguntaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_encuesta_tipoPregunta().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_encuesta_sami_datosResult> listaRespuestasSAMI()
        {
            List<management_encuesta_sami_datosResult> lista = new List<management_encuesta_sami_datosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_encuesta_sami_datos().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_encuesta_sami_datos_detalleResult> listaRespuestasSAMIDetalle()
        {
            List<management_encuesta_sami_datos_detalleResult> lista = new List<management_encuesta_sami_datos_detalleResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_encuesta_sami_datos_detalle().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_encuesta_sami_datos_promediosResult> listaRespuestasSAMIPromedios()
        {
            List<management_encuesta_sami_datos_promediosResult> lista = new List<management_encuesta_sami_datos_promediosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_encuesta_sami_datos_promedios().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public encuesta_sami TraerEncuestaEsteMes()
        {
            encuesta_sami dato = new encuesta_sami();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;

                    dato = db.encuesta_sami
                        .Where(x => x.fecha_digita.Value.Year == year && x.fecha_digita.Value.Month == month)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        #endregion ENCUESTA SAMI

        #region FIS PRESTADORES

        public management_fisPrestadoresResult TraerPrestadorId(int? idPrestador)
        {
            management_fisPrestadoresResult lista = new management_fisPrestadoresResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores(idPrestador).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_fisPrestadores_sedesResult> TraerPrestadorSedesId(int? idPrestador)
        {
            List<management_fisPrestadores_sedesResult> lista = new List<management_fisPrestadores_sedesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_sedes(idPrestador).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_fisPrestadores_tableroControlResult> TableroControlPrestadores(string nit, string sap)
        {
            List<management_fisPrestadores_tableroControlResult> lista = new List<management_fisPrestadores_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_tableroControl(nit, sap).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_fisPrestadores_tableroControl_detalladoResult> TableroControlPrestadoresDetallado(string nit, string sap)
        {
            List<management_fisPrestadores_tableroControl_detalladoResult> lista = new List<management_fisPrestadores_tableroControl_detalladoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_tableroControl_detallado(nit, sap).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_fisPrestadores_tableroControl_archivosResult> TraerArchivosPrestadorId(int? idPrestador)
        {
            List<management_fisPrestadores_tableroControl_archivosResult> lista = new List<management_fisPrestadores_tableroControl_archivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_tableroControl_archivos(idPrestador).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public int TraerprestadorSedeCodHabilitacion(string codigo_sede, string codigo_habilitacion, int? idPrestador)
        {
            var respuesta = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores dato = db.fis_rips_prestadores.FirstOrDefault(x => x.codigo_habilitacion == codigo_habilitacion && x.codigo_sede == codigo_sede && x.id_prestador == idPrestador);

                    fis_rips_prestadores_sedes sede = db.fis_rips_prestadores_sedes.FirstOrDefault(x => x.codigo_habilitacion_sede == codigo_habilitacion && x.codigo_otra_sede == codigo_sede && x.id_prestador_sede == idPrestador);

                    respuesta = sede != null || dato != null ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return respuesta;
        }

        public int ExisteTigaContrato(int? idTiga, int? idContrato)
        {
            var respuesta = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    fis_rips_prestadores_contrato_tigas dato = db.fis_rips_prestadores_contrato_tigas.FirstOrDefault(x => x.id_tiga == idTiga && x.id_contrato == idContrato);
                    respuesta = dato != null ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return respuesta;
        }


        public fis_rips_prestadores_archivos ArchivoPrestadorId(int? idArchivo)
        {
            fis_rips_prestadores_archivos dato = new fis_rips_prestadores_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_prestadores_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }


        public List<fis_rips_prestadores> ConsultaPrestadoresFis(string nit, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores> lstResult = new List<fis_rips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.fis_rips_prestadores.Where(l => l.nit == nit).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<fis_rips_prestadores> ConsultaPrestadoresFisCodHab(string codHabilitacion, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores> lstResult = new List<fis_rips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.fis_rips_prestadores.Where(l => l.codigo_habilitacion == codHabilitacion).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<fis_rips_prestadores> ConsultaPrestadoresFisSAP(decimal sap, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores> lstResult = new List<fis_rips_prestadores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.fis_rips_prestadores.Where(l => l.codigo_SAP == sap).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<fis_rips_tigas> TraerTigas()
        {
            List<fis_rips_tigas> lista = new List<fis_rips_tigas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_tigas.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<fis_rips_tigas> TraerTigasCod(string codigo)
        {
            List<fis_rips_tigas> lista = new List<fis_rips_tigas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_tigas.Where(x => x.codigo.Contains(codigo)).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_tigas_detallados> TraerTigasDetallados()
        {
            List<ref_tigas_detallados> lista = new List<ref_tigas_detallados>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tigas_detallados.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_tigas_detallados> TraerTigasDetalladosCod(string detallado)
        {
            List<ref_tigas_detallados> lista = new List<ref_tigas_detallados>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tigas_detallados.Where(x => (Convert.ToString(x.tiga_detallado).Contains(detallado) || x.descripcion_tiga_detallado.Contains(detallado)) && x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public management_fisPrestadores_contratosResult TraerDatosContrato(int? idCOntrato)
        {
            management_fisPrestadores_contratosResult dato = new management_fisPrestadores_contratosResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisPrestadores_contratos(idCOntrato).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_fisPrestadores_contratos_tigasResult> TraerDatosContratoTigas(int? idCOntrato)
        {
            List<management_fisPrestadores_contratos_tigasResult> lista = new List<management_fisPrestadores_contratos_tigasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_tigas(idCOntrato).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<fis_rips_regional> TraerregionalesFis()
        {
            List<fis_rips_regional> lista = new List<fis_rips_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_regional.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_fis_departamento_regionalResult> TraerDepartamentosFis(int? idRegional)
        {
            List<management_fis_departamento_regionalResult> lista = new List<management_fis_departamento_regionalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_departamento_regional(idRegional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<ref_fis_departamentos> TraerDepartamentosFisTodos()
        {
            List<ref_fis_departamentos> lista = new List<ref_fis_departamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_fis_departamentos.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<ref_fis_municipios> TraerMunicipiosFis(int? idDepartamento)
        {
            List<ref_fis_municipios> lstResult = new List<ref_fis_municipios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.ref_fis_municipios.Where(x => x.id_departamento == idDepartamento).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<fis_rips_ciudad> TraerCiudadesFis(int? idDepartamento)
        {
            List<fis_rips_ciudad> lista = new List<fis_rips_ciudad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_ciudad.Where(x => x.id_departamento == idDepartamento).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public fis_rips_cups TraerCupsCodigo(string codigo)
        {
            fis_rips_cups dato = new fis_rips_cups();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_cups.Where(x => x.codigo_cups == codigo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fisPrestadores_contratos_tableroControlResult> DatosTableroControlContratos()
        {
            List<management_fisPrestadores_contratos_tableroControlResult> lista = new List<management_fisPrestadores_contratos_tableroControlResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_tableroControl().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<management_fisPrestadores_contratos_tableroControl_porVencerResult> DatosTableroControlContratosxVencer()
        {
            List<management_fisPrestadores_contratos_tableroControl_porVencerResult> lista = new List<management_fisPrestadores_contratos_tableroControl_porVencerResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_tableroControl_porVencer().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_fis_cupsResult> TraerCUpsTablero()
        {
            List<management_fis_cupsResult> lista = new List<management_fis_cupsResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_cups().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public fis_rips_cups TraerCupsId(int? idCups)
        {
            fis_rips_cups dato = new fis_rips_cups();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_cups.Where(x => x.id_cups == idCups).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<fis_rips_cups> TraerListadoCupsCodigo(string codigo)
        {
            List<fis_rips_cups> lista = new List<fis_rips_cups>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_cups.Where(x => x.codigo_cups.Contains(codigo) || x.descripcion.Contains(codigo)).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<fis_rips_cups> TraerListadoCups()
        {
            List<fis_rips_cups> lista = new List<fis_rips_cups>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_cups.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_fis_refTipoConsultasResult> ListadoTipoConsultaJson()
        {
            List<management_fis_refTipoConsultasResult> lista = new List<management_fis_refTipoConsultasResult>();
            {
                try
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        lista = db.management_fis_refTipoConsultas().ToList();
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                return lista;
            }
        }


        public List<fis_rips_prestadores_contratos> ConsultaContratoPrestadoresFis(string numContrato, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores_contratos> lstResult = new List<fis_rips_prestadores_contratos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.fis_rips_prestadores_contratos.Where(l => l.num_contrato == numContrato).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public fis_rips_prestadores_contratos TraerDatosContratoPrestadorId(int? idContrato)
        {
            fis_rips_prestadores_contratos obj = new fis_rips_prestadores_contratos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj = db.fis_rips_prestadores_contratos.FirstOrDefault(x => x.id_contrato == idContrato);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return obj;

        }

        public fis_rips_prestadores_contratos TraerDatosContratoPrestadorIdPrestador(int? idPrestador)
        {
            fis_rips_prestadores_contratos obj = new fis_rips_prestadores_contratos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj = db.fis_rips_prestadores_contratos.FirstOrDefault(x => x.id_prestador == idPrestador);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return obj;

        }

        public fis_rips_prestadores PrestadorxNit(string nit)
        {
            fis_rips_prestadores obj = new fis_rips_prestadores();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    obj = db.fis_rips_prestadores.FirstOrDefault(x => x.nit == nit);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return obj;
        }

        public List<fis_rips_cargue_LoteConsultas> ConsultaCUVFIS(string codCuv, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_cargue_LoteConsultas> lstResult = new List<fis_rips_cargue_LoteConsultas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.fis_rips_cargue_LoteConsultas.Where(l => l.codigo_cuv == codCuv).ToList();
                    return lstResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return lstResult;
            }
        }

        public List<management_fis_cargueRips_consultaResult> ListadoRipsConsulta(string codCuv)
        {
            List<management_fis_cargueRips_consultaResult> lista = new List<management_fis_cargueRips_consultaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.management_fis_cargueRips_consulta(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }

        public List<management_fis_cargueRips_hospitalizacionResult> ListadoRipsHospitalizacion(string codCuv)
        {
            List<management_fis_cargueRips_hospitalizacionResult> lista = new List<management_fis_cargueRips_hospitalizacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_hospitalizacion(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_medicamentosResult> ListadoRipsMedicamentos(string codCuv)
        {
            List<management_fis_cargueRips_medicamentosResult> lista = new List<management_fis_cargueRips_medicamentosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_medicamentos(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_otrosServiciosResult> ListadoRipsOtrosServicios(string codCuv)
        {
            List<management_fis_cargueRips_otrosServiciosResult> lista = new List<management_fis_cargueRips_otrosServiciosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_otrosServicios(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_procedimientosResult> ListadoRipsProcedimientos(string codCuv)
        {
            List<management_fis_cargueRips_procedimientosResult> lista = new List<management_fis_cargueRips_procedimientosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_procedimientos(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_recienNacidoResult> ListadoRipsRecienNacido(string codCuv)
        {
            List<management_fis_cargueRips_recienNacidoResult> lista = new List<management_fis_cargueRips_recienNacidoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_recienNacido(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_transaccionResult> ListadoRipsTransaccion(string codCuv)
        {
            List<management_fis_cargueRips_transaccionResult> lista = new List<management_fis_cargueRips_transaccionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_transaccion(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }


        public List<management_fis_cargueRips_urgenciasResult> ListadoRipsUrgencias(string codCuv)
        {
            List<management_fis_cargueRips_urgenciasResult> lista = new List<management_fis_cargueRips_urgenciasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;

                    lista = db.management_fis_cargueRips_urgencias(codCuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }

        public List<management_fis_cargueRips_usuariosResult> ListadoRipsUsuarios(int? idFactura)
        {
            List<management_fis_cargueRips_usuariosResult> lista = new List<management_fis_cargueRips_usuariosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 600;
                    lista = db.management_fis_cargueRips_usuarios(idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;

            }
            return lista;
        }

        public management_prestadores_traerCUVResult TraerCuv(int? idDetalle)
        {
            management_prestadores_traerCUVResult dato = new management_prestadores_traerCUVResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_prestadores_traerCUV(idDetalle).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public management_fis_traerDatosContrato_nitResult TraerDatosContratoNit(string filtrado)
        {
            management_fis_traerDatosContrato_nitResult dato = new management_fis_traerDatosContrato_nitResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_traerDatosContrato_nit(filtrado).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fis_traerDatosContrato_nitResult> TraerListaDatosContratoNit(string filtrado)
        {
            List<management_fis_traerDatosContrato_nitResult> dato = new List<management_fis_traerDatosContrato_nitResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_traerDatosContrato_nit(filtrado).ToList();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public management_fis_traerFechas_cuvResult TraerLimiteFechasFactura(int? factura)
        {
            management_fis_traerFechas_cuvResult dato = new management_fis_traerFechas_cuvResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_traerFechas_cuv(factura).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fis_facturasCuvResult> TraerListadoCupsFacturas(int? idDetalle)
        {
            List<management_fis_facturasCuvResult> lista = new List<management_fis_facturasCuvResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_facturasCuv(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_fis_facturasCuv_conBeneficiariosResult> TraerListadoCupsFacturasConBenficiarios(int? idDetalle)
        {
            List<management_fis_facturasCuv_conBeneficiariosResult> lista = new List<management_fis_facturasCuv_conBeneficiariosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fis_facturasCuv_conBeneficiarios(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_fis_facturasCuv_conBeneficiarios_idRegistroResult TraerCupsRegistro(int? idFactura, int? idRegistro)
        {
            management_fis_facturasCuv_conBeneficiarios_idRegistroResult dato = new management_fis_facturasCuv_conBeneficiarios_idRegistroResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_facturasCuv_conBeneficiarios_idRegistro(idFactura, idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }


        public List<management_fisBuscarCupsResult> TraerListaCupsIdFactura(string cups, int? idFactura)
        {
            List<management_fisBuscarCupsResult> lista = new List<management_fisBuscarCupsResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisBuscarCups(cups, idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<ref_fisFacturas_conceptoGeneral> ListadoConceptosGeneral()
        {
            List<ref_fisFacturas_conceptoGeneral> lista = new List<ref_fisFacturas_conceptoGeneral>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_fisFacturas_conceptoGeneral.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_fisFacturas_conceptoEspecifico> ListadoConceptosEspecifico(int? idGeneral)
        {
            List<ref_fisFacturas_conceptoEspecifico> lista = new List<ref_fisFacturas_conceptoEspecifico>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_fisFacturas_conceptoEspecifico.Where(x => x.id_general == idGeneral).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_fisFacturas_conceptoAplicacion> ListadoConceptosAplicacion(int? especifico)
        {
            List<ref_fisFacturas_conceptoAplicacion> lista = new List<ref_fisFacturas_conceptoAplicacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_fisFacturas_conceptoAplicacion.Where(x => x.id_especifico == especifico).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_fisFacturas_glosaResult> ListaGlosas(int? idDetalle)
        {
            List<management_fisFacturas_glosaResult> lista = new List<management_fisFacturas_glosaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisFacturas_glosa(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var errro = ex.Message;
            }
            return lista;
        }

        public List<management_fis_facturasCuvGlosas_reporteResult> ListaReporteCupsGlosas(int? idFactura)
        {
            List<management_fis_facturasCuvGlosas_reporteResult> lista = new List<management_fis_facturasCuvGlosas_reporteResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_facturasCuvGlosas_reporte(idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var errro = ex.Message;
            }
            return lista;
        }


        public List<ref_cie10_fis> listadoCie10FIS()
        {
            List<ref_cie10_fis> lista = new List<ref_cie10_fis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cie10_fis.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_cie10_fis> listadoCie10FISCodigo(string cie10)
        {
            List<ref_cie10_fis> lista = new List<ref_cie10_fis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cie10_fis.Where(x => x.codigo.Contains(cie10) || x.descripcion.Contains(cie10)).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_cie10_fis> listadoCie10FISDescripcion(string descripcion)
        {
            List<ref_cie10_fis> lista = new List<ref_cie10_fis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_cie10_fis.Where(x => x.descripcion.Contains(descripcion)).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public ref_cie10_fis TraerCie10Codigo(string codigo)
        {
            ref_cie10_fis dato = new ref_cie10_fis();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_cie10_fis.FirstOrDefault(x => x.codigo == codigo);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public ref_cie10_fis TraerCie10Descripcion(string descripcion)
        {
            ref_cie10_fis dato = new ref_cie10_fis();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_cie10_fis.FirstOrDefault(x => x.descripcion == descripcion);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_fisGlosasSubsanadas_prestadorResult> ListaGlosasSubsanadasPrestador(int? idDetalle)
        {
            List<management_fisGlosasSubsanadas_prestadorResult> lista = new List<management_fisGlosasSubsanadas_prestadorResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisGlosasSubsanadas_prestador(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var errro = ex.Message;
            }
            return lista;
        }

        public management_fisFactura_ultimoDiagnosticoResult TraerUltimoDiagnosticoCie100FacturaFis(string cuv)
        {
            management_fisFactura_ultimoDiagnosticoResult dato = new management_fisFactura_ultimoDiagnosticoResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisFactura_ultimoDiagnostico(cuv).FirstOrDefault();
                }
            }
            catch
            {

            }
            return dato;
        }

        public management_fisFactura_ultimoDiagnostico_relacionadoResult TraerUltimoDiagnosticoCie100FacturaFisRelacionado(string cuv)
        {
            management_fisFactura_ultimoDiagnostico_relacionadoResult dato = new management_fisFactura_ultimoDiagnostico_relacionadoResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisFactura_ultimoDiagnostico_relacionado(cuv).FirstOrDefault();
                }
            }
            catch
            {

            }
            return dato;
        }

        public List<management_fisFacturas_buscarExistenciaCuvResult> ListadoCuvExistentesCodigo(string cuv)
        {
            List<management_fisFacturas_buscarExistenciaCuvResult> lista = new List<management_fisFacturas_buscarExistenciaCuvResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisFacturas_buscarExistenciaCuv(cuv).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public fis_rips_facturas_glosa ListadoGlosaSinLevantar(int? idFactura)
        {
            fis_rips_facturas_glosa dato = new fis_rips_facturas_glosa();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_facturas_glosa.FirstOrDefault(x => x.id_factura == idFactura && x.estado == 1);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fis_rips_usuariosSinTigaResult> TraerUsuariosSinTiga(int? idAf)
        {
            List<management_fis_rips_usuariosSinTigaResult> lista = new List<management_fis_rips_usuariosSinTigaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_rips_usuariosSinTiga(idAf).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }

        public List<management_fis_rips_usuariosSinFechaPrestacionResult> TraerUsuariosSinFechaPrestacion(int? idAf)
        {
            List<management_fis_rips_usuariosSinFechaPrestacionResult> lista = new List<management_fis_rips_usuariosSinFechaPrestacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_rips_usuariosSinFechaPrestacion(idAf).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }


        public List<management_fis_rips_ParaActualizar_tigasResult> TraerListadoCUPSActualziarTigas(int? idAf, DateTime? fechaIni,
            DateTime? fechaFin, string codPrestador, string numFactura, string regional)
        {
            //Listado para darle actualizacion luego

            List<management_fis_rips_ParaActualizar_tigasResult> lista = new List<management_fis_rips_ParaActualizar_tigasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fis_rips_ParaActualizar_tigas(idAf, fechaIni, fechaFin, codPrestador, numFactura, regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }


        public fis_rips_facturas_glosa TraerGlosaRips(int? idGlosa)
        {
            fis_rips_facturas_glosa dato = new fis_rips_facturas_glosa();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_facturas_glosa.FirstOrDefault(x => x.id_glosa == idGlosa);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public ref_tigas_detallados TraerTigasDetallados(string tiga)
        {
            ref_tigas_detallados dato = new ref_tigas_detallados();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.ref_tigas_detallados.FirstOrDefault(x => x.tiga_detallado == Convert.ToInt32(tiga));
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<management_fisPrestadores_contratos_negociacionesResult> TraerNegociacionesContrato(int? idContrato)
        {
            List<management_fisPrestadores_contratos_negociacionesResult> lista = new List<management_fisPrestadores_contratos_negociacionesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_negociaciones(idContrato).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }


        public List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult> TraerNegociacionesPrestador(int? idPrestador)
        {
            List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult> lista = new List<management_fisPrestadores_contratos_negociaciones_idPrestadorResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_negociaciones_idPrestador(idPrestador).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }



        public List<management_fisPrestadores_contratos_negociaciones_detalleResult> TraerNegociacionesContratoDetalle(int? idMasivo)
        {
            List<management_fisPrestadores_contratos_negociaciones_detalleResult> lista = new List<management_fisPrestadores_contratos_negociaciones_detalleResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisPrestadores_contratos_negociaciones_detalle(idMasivo).ToList();
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return lista;

        }

        public fis_rips_cargue_registrosCompletos TraerRegistroRipsId(int? idRegistro)
        {
            fis_rips_cargue_registrosCompletos dato = new fis_rips_cargue_registrosCompletos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_registro == idRegistro);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public fis_rips_cargue_registrosCompletos TraerRegistroRipsIdFactura(int? idFactura)
        {
            fis_rips_cargue_registrosCompletos dato = new fis_rips_cargue_registrosCompletos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_cargue_registrosCompletos.FirstOrDefault(x => x.id_factura == idFactura);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<management_fis_buscarExietenciaBeneficiarioResult> BuscarBeneficiarioEnFis(DateTime? fecha, string tipoId, string identificacion)
        {
            List<management_fis_buscarExietenciaBeneficiarioResult> listado = new List<management_fis_buscarExietenciaBeneficiarioResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_fis_buscarExietenciaBeneficiario(fecha, tipoId, identificacion).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public management_fis_buscarFechaAtencion_idUsuarioResult BuscarFechaAtencionUsuario(int? idUsuario, int? idTransaccion)
        {
            management_fis_buscarFechaAtencion_idUsuarioResult listado = new management_fis_buscarFechaAtencion_idUsuarioResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.management_fis_buscarFechaAtencion_idUsuario(idUsuario, idTransaccion).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<fis_rips_facturas_glosa> TraerGlosasMantenidas(int? idFactura)
        {
            List<fis_rips_facturas_glosa> lista = new List<fis_rips_facturas_glosa>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_facturas_glosa.Where(x => x.id_factura == idFactura && x.estado == 3).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }



        public List<management_fis_glosaTieneNotaCreditoResult> TraerGlosasConNoptaCredito(int? idFactura)
        {
            List<management_fis_glosaTieneNotaCreditoResult> lista = new List<management_fis_glosaTieneNotaCreditoResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_glosaTieneNotaCredito(idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_prestadoresFacturasGestionadas_fisResult> TraerListadoFacturas()
        {
            List<management_prestadoresFacturasGestionadas_fisResult> lista = new List<management_prestadoresFacturasGestionadas_fisResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_prestadoresFacturasGestionadas_fis().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public fis_rips_facturas TraerFacturaIdAf(int? idAf)
        {
            fis_rips_facturas dato = new fis_rips_facturas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_facturas.FirstOrDefault(x => x.id_factura == idAf);
                }
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }
            return dato;
        }

        public fis_rips_facturas TraerFacturaIdAfAnalista(int? idAf)
        {
            fis_rips_facturas dato = new fis_rips_facturas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_facturas.FirstOrDefault(x => x.id_factura == idAf && x.tipo_ingreso == 1);
                }
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }
            return dato;
        }

        public fis_rips_facturas TraerFacturaIdAfAuditor(int? idAf)
        {
            fis_rips_facturas dato = new fis_rips_facturas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.fis_rips_facturas.FirstOrDefault(x => x.id_factura == idAf && x.tipo_ingreso == 2);
                }
            }
            catch (Exception ex)
            {
                var eror = ex.Message;
            }
            return dato;
        }


        public List<management_fisRips_TableroDetallesCargueResult> ListadoDetallesMasivo(DateTime? fechaIni, DateTime? fechaFin, int? regional)
        {
            List<management_fisRips_TableroDetallesCargueResult> listado = new List<management_fisRips_TableroDetallesCargueResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    listado = db.management_fisRips_TableroDetallesCargue(fechaIni, fechaFin, regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return listado;
        }

        public List<fis_rips_sinJson_detalle> TraerListadoCargueMasivo(int? idDetalle)
        {
            List<fis_rips_sinJson_detalle> listado = new List<fis_rips_sinJson_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.fis_rips_sinJson_detalle.Where(x => x.id_detalle == idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<fis_rips_sinJson_detalle> TraerListadoCargueMasivoTotal()
        {
            List<fis_rips_sinJson_detalle> listado = new List<fis_rips_sinJson_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.fis_rips_sinJson_detalle.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public management_fisBuscarCuv_idFacturaResult FisBuscarCuv(int? idDetalle)
        {
            management_fisBuscarCuv_idFacturaResult dato = new management_fisBuscarCuv_idFacturaResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisBuscarCuv_idFactura(idDetalle).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public List<management_fisMasivo_buscarFacturasResult> FisBuscarFacturas(int? idDetalle)
        {
            List<management_fisMasivo_buscarFacturasResult> lista = new List<management_fisMasivo_buscarFacturasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fisMasivo_buscarFacturas(idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_fisMasivo_buscarUsuariosResult> FisBuscarFacturasUsuarios(int? idDetalle, string numFactura)
        {
            List<management_fisMasivo_buscarUsuariosResult> lista = new List<management_fisMasivo_buscarUsuariosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fisMasivo_buscarUsuarios(idDetalle, numFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_fisMasivo_buscarServiciosResult> FisBuscarFacturasServicios(int? idDetalle, string numDocumento)
        {
            List<management_fisMasivo_buscarServiciosResult> lista = new List<management_fisMasivo_buscarServiciosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fisMasivo_buscarServicios(idDetalle, numDocumento).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<fis_rips_cargueMasivo_ivm_registros> ExisteIVM()
        {
            List<fis_rips_cargueMasivo_ivm_registros> lista = new List<fis_rips_cargueMasivo_ivm_registros>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_cargueMasivo_ivm_registros.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public management_fis_devolverIVAResult DevolverIvaTotalIdFactura(int? idFactura)
        {
            management_fis_devolverIVAResult dato = new management_fis_devolverIVAResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_devolverIVA(idFactura).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }


        public List<management_fis_tableroCargueContableSapResult> TraerGlosasTerminadasDocumentoSAP()
        {
            List<management_fis_tableroCargueContableSapResult> lista = new List<management_fis_tableroCargueContableSapResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_tableroCargueContableSap().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_fis_reporteHesResult> TraerDatosReporteHES(DateTime? fechaIni, DateTime? fechaFin, string regional)
        {
            List<management_fis_reporteHesResult> lista = new List<management_fis_reporteHesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3600;
                    lista = db.management_fis_reporteHes(fechaIni, fechaFin, regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<fis_rips_cargueMasivo_pedidos_registros> ListadoDatosFacturasContables()
        {
            List<fis_rips_cargueMasivo_pedidos_registros> listado = new List<fis_rips_cargueMasivo_pedidos_registros>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    listado = db.fis_rips_cargueMasivo_pedidos_registros.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return listado;
        }

        public List<management_fis_facturasCuv_tableroEliminarResult> TraerListadoDetallesId(int? idFactura, string numFactura)
        {
            List<management_fis_facturasCuv_tableroEliminarResult> lista = new List<management_fis_facturasCuv_tableroEliminarResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_facturasCuv_tableroEliminar(idFactura, numFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_fis_facturasCuv_tableroEliminar_conteoResult DevolverConteoFacturas(int? idFactura, string numFactura)
        {
            management_fis_facturasCuv_tableroEliminar_conteoResult dato = new management_fis_facturasCuv_tableroEliminar_conteoResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_facturasCuv_tableroEliminar_conteo(idFactura, numFactura).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fisSinJson_DescargarDatosResult> TraerListadoDetallesSinJson(int? idLote)
        {
            List<management_fisSinJson_DescargarDatosResult> lista = new List<management_fisSinJson_DescargarDatosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fisSinJson_DescargarDatos(idLote).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_fisFacturas_sumaGlosasValidacionResult BuscarSumatoriaGlosas(int? idRegistro, int? idFactura, int? idUsuario, string cups, decimal? valorNuevo)
        {
            management_fisFacturas_sumaGlosasValidacionResult dato = new management_fisFacturas_sumaGlosasValidacionResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisFacturas_sumaGlosasValidacion(idFactura, idUsuario, cups, valorNuevo, idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public int HayDetallesIdFactura(int? idFactura)
        {
            var existe = 0;
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 3000;
                    fis_rips_sinJson_detalle det = db.fis_rips_sinJson_detalle.FirstOrDefault(x => x.id_detalle == idFactura);
                    existe = det != null ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return existe;
        }

        public List<management_fis_cargarMasivoDetalles_idsFacturasResult> listadoFacturasCargueMasivo()
        {
            List<management_fis_cargarMasivoDetalles_idsFacturasResult> lista = new List<management_fis_cargarMasivoDetalles_idsFacturasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_cargarMasivoDetalles_idsFacturas().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_fisFacturas_glosa_idGlopsaResult traerGlosaId(int? idFactuea, int? idGlosa)
        {
            management_fisFacturas_glosa_idGlopsaResult dato = new management_fisFacturas_glosa_idGlopsaResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fisFacturas_glosa_idGlopsa(idFactuea, idGlosa).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;

            }
            return dato;
        }

        public management_fis_TraerValoresFacturaResult TraerValoreFactura(int? idFactura)
        {
            management_fis_TraerValoresFacturaResult dato = new management_fis_TraerValoresFacturaResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_TraerValoresFactura(idFactura).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                var erorr = ex.Message;
            }
            return dato;
        }

        public List<fis_rips_cargue_usuarios> listadoUsuariosFisIdFactura(int? idFactura)
        {
            List<fis_rips_cargue_usuarios> lista = new List<fis_rips_cargue_usuarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_cargue_usuarios.Where(x => x.id_factura == idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<management_fis_listadoPrestadoresExistentesResult> ListadoprestadoresFISeXISTENTES()
        {
            List<management_fis_listadoPrestadoresExistentesResult> lista = new List<management_fis_listadoPrestadoresExistentesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_listadoPrestadoresExistentes().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public List<management_fis_contratosExistentesResult> ListadoContratosFISExistntes()
        {
            List<management_fis_contratosExistentesResult> lista = new List<management_fis_contratosExistentesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_contratosExistentes().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<fis_rips_prestadores_contrato_tigas> listadoTIGAScontratoFIS()
        {
            List<fis_rips_prestadores_contrato_tigas> lista = new List<fis_rips_prestadores_contrato_tigas>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.fis_rips_prestadores_contrato_tigas.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<management_fis_tableroActualizarContratos_gestionAnalistaResult> ListadoGestionContratos()
        {
            List<management_fis_tableroActualizarContratos_gestionAnalistaResult> lista = new List<management_fis_tableroActualizarContratos_gestionAnalistaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lista = db.management_fis_tableroActualizarContratos_gestionAnalista().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }


        public management_fis_traerNumfactura_idResult TraerNumFactura_idAf(int? idDetalle)
        {
            management_fis_traerNumfactura_idResult dato = new management_fis_traerNumfactura_idResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_fis_traerNumfactura_id(idDetalle).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public List<management_fis_facturasActuaRutaAprobadoResult> listadoFacturasActualizarAprobadas()
        {
            List<management_fis_facturasActuaRutaAprobadoResult> lista = new List<management_fis_facturasActuaRutaAprobadoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_facturasActuaRutaAprobado().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }


        public List<management_fis_validacionExistentesTigasdetalles_contratoResult> ListaTigasSinContrato(int? idFactura)
        {
            List<management_fis_validacionExistentesTigasdetalles_contratoResult> lista = new List<management_fis_validacionExistentesTigasdetalles_contratoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_fis_validacionExistentesTigasdetalles_contrato(idFactura).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_facturas_novedades> TiposNovedadesFacturas()
        {
            List<ref_facturas_novedades> lista = new List<ref_facturas_novedades>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_facturas_novedades.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<management_facturasNovedades_buscarResult> TraerDatosNovedadesFacturas(int? idFactura, int? novedad)
        {
            List<management_facturasNovedades_buscarResult> lista = new List<management_facturasNovedades_buscarResult>();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_facturasNovedades_buscar(idFactura, novedad).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public factura_novedades BuscarNovedadId(int? idNovedad)
        {
            factura_novedades dato = new factura_novedades();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.factura_novedades.FirstOrDefault(x => x.id_novedad == idNovedad);
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        #endregion FIS PRESTADORES

        #region CHATBOT
        public List<Management_chatbot_ref_proyectosResult> ChatBotProyectos()
        {
            List<Management_chatbot_ref_proyectosResult> lista = new List<Management_chatbot_ref_proyectosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_proyectos().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<Management_chatbot_ref_procesosResult> ChatBotProcesos(int? idProyecto)
        {
            List<Management_chatbot_ref_procesosResult> lista = new List<Management_chatbot_ref_procesosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_procesos(idProyecto).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<Management_chatbot_ref_subprocesosResult> ChatBotSubProcesos(int? idProceso)
        {
            List<Management_chatbot_ref_subprocesosResult> lista = new List<Management_chatbot_ref_subprocesosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_subprocesos(idProceso).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<Management_chatbot_ref_preguntasResult> ChatBotPreguntas(int? idSubProceso)
        {
            List<Management_chatbot_ref_preguntasResult> lista = new List<Management_chatbot_ref_preguntasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_preguntas(idSubProceso).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<Management_chatbot_ref_respuestasResult> ChatBotRespuestas(int? idPregunta)
        {
            List<Management_chatbot_ref_respuestasResult> lista = new List<Management_chatbot_ref_respuestasResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_respuestas(idPregunta).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public List<Management_chatbot_ref_respuestas_archivosResult> ChatBotRespuestasArchivos(int? idRespuesta)
        {
            List<Management_chatbot_ref_respuestas_archivosResult> lista = new List<Management_chatbot_ref_respuestas_archivosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.Management_chatbot_ref_respuestas_archivos(idRespuesta).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }

        public chatbot_ref_respuestas_archivos TraerArchivoChatBot(int? idArchivo)
        {
            chatbot_ref_respuestas_archivos dato = new chatbot_ref_respuestas_archivos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_respuestas_archivos.Where(x => x.id_archivo == idArchivo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return dato;
        }

        public chatbot_ref_respuestas TraerRespuestaId(int? idRespuesta)
        {
            chatbot_ref_respuestas dato = new chatbot_ref_respuestas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_respuestas.Where(x => x.id_chatbot_ref_respuesta == idRespuesta).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public chatbot_ref_preguntas TraerPreguntaId(int? idPregunta)
        {
            chatbot_ref_preguntas dato = new chatbot_ref_preguntas();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_preguntas.Where(x => x.id_chatbot_ref_pregunta == idPregunta).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        public chatbot_ref_subprocesos TraerSubProcesoId(int? idSub)
        {
            chatbot_ref_subprocesos dato = new chatbot_ref_subprocesos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_subprocesos.Where(x => x.id_chatbot_ref_subproceso == idSub).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }


        public chatbot_ref_procesos TraerProcesoId(int? idProceso)
        {
            chatbot_ref_procesos dato = new chatbot_ref_procesos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_procesos.Where(x => x.id_chatbot_ref_proceso == idProceso).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }


        public chatbot_ref_proyectos TraerProyectoId(int? idProyecto)
        {
            chatbot_ref_proyectos dato = new chatbot_ref_proyectos();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.chatbot_ref_proyectos.Where(x => x.id_chatbot_ref_proyecto == idProyecto).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        #endregion CHATBOT


        public management_traerRegionalFacturaResult TraerRegionalFacturas(int? idCargue)
        {
            management_traerRegionalFacturaResult dato = new management_traerRegionalFacturaResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_traerRegionalFactura(idCargue).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;
        }

        #region VIGILANCIA COHORTES

        public List<management_VigilanciaCohortes_TableroInicialResult> ListadoVigilanciaCohortesInicial(int? mes, int? año,
         string regional, string localidad, string municipio, int? tipo)
        {
            List<management_VigilanciaCohortes_TableroInicialResult> lista = new List<management_VigilanciaCohortes_TableroInicialResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    lista = db.management_VigilanciaCohortes_TableroInicial(mes, año, regional, localidad, municipio, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_VigilanciaCohortes_TableroGestionadosResult> ListadoVigilanciaCohortesGestionados(int? mes, int? año,
            string regional, string localidad, string municipio, int? tipo)
        {
            List<management_VigilanciaCohortes_TableroGestionadosResult> lista = new List<management_VigilanciaCohortes_TableroGestionadosResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.CommandTimeout = 6000;
                    lista = db.management_VigilanciaCohortes_TableroGestionados(mes, año, regional, localidad, municipio, tipo).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_BuscarLocalidad_descripcionRegionalResult> LocalidadesXRegional(string regional)
        {
            List<management_BuscarLocalidad_descripcionRegionalResult> lista = new List<management_BuscarLocalidad_descripcionRegionalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lista = db.management_BuscarLocalidad_descripcionRegional(regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_BuscarCiudad_descripcionLocalidadResult> CiudadesXLocalidad(string localidad)
        {
            List<management_BuscarCiudad_descripcionLocalidadResult> lista = new List<management_BuscarCiudad_descripcionLocalidadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lista = db.management_BuscarCiudad_descripcionLocalidad(localidad).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_VigilanciaCohortes_gestionesResult TraerGestionVigilanciaCohortes(int? idLote, int? idRegistro)
        {
            management_VigilanciaCohortes_gestionesResult lista = new management_VigilanciaCohortes_gestionesResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_VigilanciaCohortes_gestiones(idLote, idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public management_VigilanciaCohortes_detalleParaGestionarResult DetallesVigilanciaCohortes(int? idLote, int? idRegistro)
        {
            management_VigilanciaCohortes_detalleParaGestionarResult lista = new management_VigilanciaCohortes_detalleParaGestionarResult();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_VigilanciaCohortes_detalleParaGestionar(idLote, idRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_vigilancoaCohortes_busquedaLocalidadesResult> GRPCBusquedaLocalidades(string regional)
        {
            List<management_vigilancoaCohortes_busquedaLocalidadesResult> lista = new List<management_vigilancoaCohortes_busquedaLocalidadesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lista = db.management_vigilancoaCohortes_busquedaLocalidades(regional).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<management_vigilancoaCohortes_busquedaCiudadesResult> GRPCBusquedaCiudades(string localidad)
        {
            List<management_vigilancoaCohortes_busquedaCiudadesResult> lista = new List<management_vigilancoaCohortes_busquedaCiudadesResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lista = db.management_vigilancoaCohortes_busquedaCiudades(localidad).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #endregion VIGILANCIA COHORTES


        #region ADICIONALES
        public List<management_facturas_creacionreporteAprobacionResult> listadoFacturasReporteAprobacion()
        {
            List<management_facturas_creacionreporteAprobacionResult> lista = new List<management_facturas_creacionreporteAprobacionResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_facturas_creacionreporteAprobacion().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }
        #endregion ADICIONALES

    }
}