using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.Configuration;
using System.Data.SqlClient;


namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class RipsFis
    {
        #region Propiedades
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


        #region Metodos


        public List<Management_FisRips_Correctos_ACResult> FisRipsCorrectos_AC(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AC(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_Correctos_AFResult> FisRipsCorrectos_AF(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AF(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_Correctos_AHResult> FisRipsCorrectos_AH(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AH(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_Correctos_AMResult> FisRipsCorrectos_AM(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AM(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_Correctos_ANResult> FisRipsCorrectos_AN(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AN(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Correctos_APResult> FisRipsCorrectos_AP(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AP(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Correctos_ATResult> FisRipsCorrectos_AT(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AT(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_Correctos_AUResult> FisRipsCorrectos_AU(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_AU(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Correctos_USResult> FisRipsCorrectos_US(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsCorrectos_US(regional, mes, año, ref MsgRes);
        }


        public List<ECOPETROL_COMMON.ENUM.reporterips> ConsultaRipsFisEvaluacion(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {

            return BusClass.ConsultaRipsFisEvaluacion(regional, mes, año, ref MsgRes);
        }



        public List<Management_FisRips_Incorrectos_ACResult> FisRipsErrores_AC(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AC(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_AHResult> FisRipsErrores_AH(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AH(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_AMResult> FisRipsErrores_AM(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AM(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_ANResult> FisRipsErrores_AN(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AN(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_APResult> FisRipsErrores_AP(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AP(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_ATResult> FisRipsErrores_AT(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AT(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_AUResult> FisRipsErrores_AU(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_AU(regional, mes, año, ref MsgRes);
        }

        public List<Management_FisRips_Incorrectos_USResult> FisRipsErrores_US(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsErrores_US(regional, mes, año, ref MsgRes);
        }




        public List<Management_FisRips_SinOportunidad_ACResult> FisRipsInoportuno_AC(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AC(regional, mes, año, ref MsgRes);
        }



        public List<Management_FisRips_SinOportunidad_AHResult> FisRipsInoportuno_AH(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AH(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_SinOportunidad_APResult> FisRipsInoportuno_AP(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AP(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_SinOportunidad_ANResult> FisRipsInoportuno_AN(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AN(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_SinOportunidad_AUResult> FisRipsInoportuno_AU(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AU(regional, mes, año, ref MsgRes);
        }


        public List<Management_FisRips_SinOportunidad_AMResult> FisRipsInoportuno_AM(int regional, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.FisRipsInoportuno_AM(regional, mes, año, ref MsgRes);
        }


        #endregion









    }





}