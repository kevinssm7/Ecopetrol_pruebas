using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class interventoria
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

        private List<md_Ref_proveedor> _Listproveedor;
        public List<md_Ref_proveedor> Listproveedor
        {
            get
            {
                if (_Listproveedor == null)
                {
                    return _Listproveedor = new List<md_Ref_proveedor>();
                }
                else
                {
                    return _Listproveedor;
                }

            }

            set
            {
                _Listproveedor = value;
            }
        }

        private List<vw_md_interventoria_general> _ListMDInterventoriaGeneral;
        public List<vw_md_interventoria_general> ListMDInterventoriaGeneral
        {
            get
            {
                if (_ListMDInterventoriaGeneral == null)
                {
                    return _ListMDInterventoriaGeneral = new List<vw_md_interventoria_general>();
                }
                else
                {
                    return _ListMDInterventoriaGeneral;
                }

            }

            set
            {
                _ListMDInterventoriaGeneral = value;
            }
        }

        private md_interventoria_general _OBJInterventoriaGeneral;
        public md_interventoria_general OBJInterventoriaGeneral
        {
            get
            {
                if (_OBJInterventoriaGeneral == null)
                {
                    return _OBJInterventoriaGeneral = new md_interventoria_general();
                }
                else
                {
                    return _OBJInterventoriaGeneral;
                }

            }

            set
            {
                _OBJInterventoriaGeneral = value;
            }
        }

        private List<Managment_md_Ref_General1Result> _ListaGeneral1OK;
        public List<Managment_md_Ref_General1Result> ListaGeneral1OK
        {
            get
            {
                if (_ListaGeneral1OK == null)
                {
                    return _ListaGeneral1OK = new List<Managment_md_Ref_General1Result>();
                }
                else
                {
                    return _ListaGeneral1OK;
                }

            }

            set
            {
                _ListaGeneral1OK = value;
            }
        }

        private List<Managment_md_Ref_General2Result> _ListaGeneral2OK;
        public List<Managment_md_Ref_General2Result> ListaGeneral2OK
        {
            get
            {
                if (_ListaGeneral2OK == null)
                {
                    return _ListaGeneral2OK = new List<Managment_md_Ref_General2Result>();
                }
                else
                {
                    return _ListaGeneral2OK;
                }

            }

            set
            {
                _ListaGeneral2OK = value;
            }
        }


        private List<Managment_md_Ref_General3Result> _ListaGeneral3OK;
        public List<Managment_md_Ref_General3Result> ListaGeneral3OK
        {
            get
            {
                if (_ListaGeneral3OK == null)
                {
                    return _ListaGeneral3OK = new List<Managment_md_Ref_General3Result>();
                }
                else
                {
                    return _ListaGeneral3OK;
                }

            }

            set
            {
                _ListaGeneral3OK = value;
            }
        }


        private List<Managment_md_Ref_General4Result> _ListaGeneral4OK;
        public List<Managment_md_Ref_General4Result> ListaGeneral4OK
        {
            get
            {
                if (_ListaGeneral4OK == null)
                {
                    return _ListaGeneral4OK = new List<Managment_md_Ref_General4Result>();
                }
                else
                {
                    return _ListaGeneral4OK;
                }

            }

            set
            {
                _ListaGeneral4OK = value;
            }
        }


        private md_interventoria_general_detalle1 _OBJDetallleG1;
        public md_interventoria_general_detalle1 OBJDetallleG1
        {
            get
            {
                if (_OBJDetallleG1 == null)
                {
                    return _OBJDetallleG1 = new md_interventoria_general_detalle1();
                }
                else
                {
                    return _OBJDetallleG1;
                }

            }

            set
            {
                _OBJDetallleG1 = value;
            }
        }

        private md_interventoria_general_detalle2 _OBJDetallleG2;
        public md_interventoria_general_detalle2 OBJDetallleG2
        {
            get
            {
                if (_OBJDetallleG2 == null)
                {
                    return _OBJDetallleG2 = new md_interventoria_general_detalle2();
                }
                else
                {
                    return _OBJDetallleG2;
                }

            }

            set
            {
                _OBJDetallleG2 = value;
            }
        }

        private md_interventoria_general_detalle3 _OBJDetallleG3;
        public md_interventoria_general_detalle3 OBJDetallleG3
        {
            get
            {
                if (_OBJDetallleG3 == null)
                {
                    return _OBJDetallleG3 = new md_interventoria_general_detalle3();
                }
                else
                {
                    return _OBJDetallleG3;
                }

            }

            set
            {
                _OBJDetallleG3 = value;
            }
        }


        private md_interventoria_general_detalle4 _OBJDetallleG4;
        public md_interventoria_general_detalle4 OBJDetallleG4
        {
            get
            {
                if (_OBJDetallleG4 == null)
                {
                    return _OBJDetallleG4 = new md_interventoria_general_detalle4();
                }
                else
                {
                    return _OBJDetallleG4;
                }

            }

            set
            {
                _OBJDetallleG4 = value;
            }
        }

        private List<Managment_md_Ref_General1Result> _Lista1G1;
        public List<Managment_md_Ref_General1Result> Lista1G1
        {
            get
            {
                if (_Lista1G1 == null)
                {
                    return _Lista1G1 = new List<Managment_md_Ref_General1Result>();
                }
                else
                {
                    return _Lista1G1;
                }

            }

            set
            {
                _Lista1G1 = value;
            }
        }

        private List<Managment_md_Ref_General1Result> _Lista2G1;
        public List<Managment_md_Ref_General1Result> Lista2G1
        {
            get
            {
                if (_Lista2G1 == null)
                {
                    return _Lista2G1 = new List<Managment_md_Ref_General1Result>();
                }
                else
                {
                    return _Lista2G1;
                }

            }

            set
            {
                _Lista2G1 = value;
            }
        }

        private List<Managment_md_Ref_General2Result> _Lista1G2;
        public List<Managment_md_Ref_General2Result> Lista1G2
        {
            get
            {
                if (_Lista1G2 == null)
                {
                    return _Lista1G2 = new List<Managment_md_Ref_General2Result>();
                }
                else
                {
                    return _Lista1G2;
                }

            }

            set
            {
                _Lista1G2 = value;
            }
        }

        private List<Managment_md_Ref_General2Result> _Lista2G2;
        public List<Managment_md_Ref_General2Result> Lista2G2
        {
            get
            {
                if (_Lista2G2 == null)
                {
                    return _Lista2G2 = new List<Managment_md_Ref_General2Result>();
                }
                else
                {
                    return _Lista2G2;
                }

            }

            set
            {
                _Lista2G2 = value;
            }
        }


        private List<Managment_md_Ref_General3Result> _Lista1G3;
        public List<Managment_md_Ref_General3Result> Lista1G3
        {
            get
            {
                if (_Lista1G3 == null)
                {
                    return _Lista1G3 = new List<Managment_md_Ref_General3Result>();
                }
                else
                {
                    return _Lista1G3;
                }

            }

            set
            {
                _Lista1G3 = value;
            }
        }

        private List<Managment_md_Ref_General3Result> _Lista2G3;
        public List<Managment_md_Ref_General3Result> Lista2G3
        {
            get
            {
                if (_Lista2G3 == null)
                {
                    return _Lista2G3 = new List<Managment_md_Ref_General3Result>();
                }
                else
                {
                    return _Lista2G3;
                }

            }

            set
            {
                _Lista2G3 = value;
            }
        }



        private List<Managment_md_Ref_General4Result> _Lista1G4;
        public List<Managment_md_Ref_General4Result> Lista1G4
        {
            get
            {
                if (_Lista1G4 == null)
                {
                    return _Lista1G4 = new List<Managment_md_Ref_General4Result>();
                }
                else
                {
                    return _Lista1G4;
                }

            }

            set
            {
                _Lista1G4 = value;
            }
        }

        private List<Managment_md_Ref_General4Result> _Lista2G4;
        public List<Managment_md_Ref_General4Result> Lista2G4
        {
            get
            {
                if (_Lista2G4 == null)
                {
                    return _Lista2G4 = new List<Managment_md_Ref_General4Result>();
                }
                else
                {
                    return _Lista2G4;
                }

            }

            set
            {
                _Lista2G4 = value;
            }
        }



        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA AUDITORIA")]
        public DateTime? fecha_auditoria { get; set; }
        public DateTime? fecha_auditoriaOK { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITOR")]
        public String nombre_auditor { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "CIUDAD")]
        public int ciudad { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public int id_regional { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NOMBRE AUDITADO")]
        public String nombre_auditado { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO")]
        public Decimal? resultado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "HALLAZGOS")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,5000}$", ErrorMessage = "Maximo 5000 caracteres")]
        public String hallazgos { get; set; }

        public int peso { get; set; }
        public Int32 valor { get; set; }
        public String observaciones { get; set; }

        public Int32 id_md_interventoria_general { get; set; }
        public Int32 id_md_interventoria_general_detalle { get; set; }

        public Int32 id_md_ref_general1 { get; set; }
        public Int32 id_md_ref_general2 { get; set; }
        public Int32 id_md_ref_general3 { get; set; }
        public Int32 id_md_ref_general4 { get; set; }


        #endregion


        #region FUNCIONES

        public List<sis_usuario> Usuarios()
        {
            return BusClass.GetSisUsuario();
        }

        public void ConsultaLista()
        {
            Listproveedor = BusClass.GetMD_Ref_proveedor();
        }

        public Int32 InsertarInterventoriaG(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarInterventoriaGeneral(OBJInterventoriaGeneral, ref MsgRes);
        }

        public List<vw_md_interventoria_general> InterventoriaGeneralProveedor(String Proveedor)
        {
            _ListMDInterventoriaGeneral = BusClass.InterventoriaGeneralProveedor(Proveedor);

            return _ListMDInterventoriaGeneral;
        }

        public List<Managment_md_Ref_General1Result> ListaInterventoriaGeneral1(Int32 id_md_interventoria_general)
        {
            ListaGeneral1OK = BusClass.DetalleRefInterventoriaGeneral1(id_md_interventoria_general, 2);
            return ListaGeneral1OK;
        }
        public List<Managment_md_Ref_General2Result> ListaInterventoriaGeneral2(Int32 id_md_interventoria_general)
        {
            ListaGeneral2OK = BusClass.DetalleRefInterventoriaGeneral2(id_md_interventoria_general, 2);
            return ListaGeneral2OK;
        }
        public List<Managment_md_Ref_General3Result> ListaInterventoriaGeneral3(Int32 id_md_interventoria_general)
        {
            ListaGeneral3OK = BusClass.DetalleRefInterventoriaGeneral3(id_md_interventoria_general, 2);
            return ListaGeneral3OK;
        }
        public List<Managment_md_Ref_General4Result> ListaInterventoriaGeneral4(Int32 id_md_interventoria_general)
        {
            ListaGeneral4OK = BusClass.DetalleRefInterventoriaGeneral4(id_md_interventoria_general, 2);
            return ListaGeneral4OK;
        }

        public Int32 InsertarGeneral1Detalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGeneral1Detalle(OBJDetallleG1, ref MsgRes);
        }
        public Int32 InsertarGeneral2Detalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGeneral2Detalle(OBJDetallleG2, ref MsgRes);
        }
        public Int32 InsertarGeneral3Detalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGeneral3Detalle(OBJDetallleG3, ref MsgRes);
        }
        public Int32 InsertarGeneral4Detalle(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGeneral4Detalle(OBJDetallleG4, ref MsgRes);
        }


        public List<Managment_md_Ref_General1Result> DetalleRefGeneral1(Int32 id_md_interventoria_general)
        {


            List<Managment_md_Ref_General1Result> list1 = new List<Managment_md_Ref_General1Result>();
            List<Managment_md_Ref_General1Result> list3 = new List<Managment_md_Ref_General1Result>();

            Lista1G1 = BusClass.DetalleRefInterventoriaGeneral1(id_md_interventoria_general, 1);
            Lista2G1 = BusClass.DetalleRefInterventoriaGeneral1(id_md_interventoria_general, 2);

            foreach (var item in Lista2G1)
            {
                Managment_md_Ref_General1Result Obj = new Managment_md_Ref_General1Result();

                Obj.id_md_ref_general1 = item.id_md_ref_general1;
                Obj.descripcion_general1 = item.descripcion_general1;
                Obj.peso = item.peso;

                foreach (var item2 in Lista1G1)
                {
                    if (Obj.id_md_ref_general1 == item2.id_md_ref_general1)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_md_interventoria_general = id_md_interventoria_general;
                        Obj.Nro = item2.Nro;
                    }

                }
                list1.Add(Obj);
            }

            return list1;
        }

        public List<Managment_md_Ref_General2Result> DetalleRefGeneral2(Int32 id_md_interventoria_general)
        {


            List<Managment_md_Ref_General2Result> list1 = new List<Managment_md_Ref_General2Result>();
            List<Managment_md_Ref_General2Result> list3 = new List<Managment_md_Ref_General2Result>();

            Lista1G2 = BusClass.DetalleRefInterventoriaGeneral2(id_md_interventoria_general, 1);
            Lista2G2 = BusClass.DetalleRefInterventoriaGeneral2(id_md_interventoria_general, 2);

            foreach (var item in Lista2G2)
            {
                Managment_md_Ref_General2Result Obj = new Managment_md_Ref_General2Result();

                Obj.id_md_ref_general2 = item.id_md_ref_general2;
                Obj.descripcion_general2 = item.descripcion_general2;
                Obj.peso = item.peso;

                foreach (var item2 in Lista1G2)
                {
                    if (Obj.id_md_ref_general2 == item2.id_md_ref_general2)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_md_interventoria_general = id_md_interventoria_general;
                        Obj.Nro = item2.Nro;
                    }

                }
                list1.Add(Obj);
            }

            return list1;
        }

        public List<Managment_md_Ref_General3Result> DetalleRefGeneral3(Int32 id_md_interventoria_general)
        {


            List<Managment_md_Ref_General3Result> list1 = new List<Managment_md_Ref_General3Result>();
            List<Managment_md_Ref_General3Result> list3 = new List<Managment_md_Ref_General3Result>();

            Lista1G3 = BusClass.DetalleRefInterventoriaGeneral3(id_md_interventoria_general, 1);
            Lista2G3 = BusClass.DetalleRefInterventoriaGeneral3(id_md_interventoria_general, 2);

            foreach (var item in Lista2G3)
            {
                Managment_md_Ref_General3Result Obj = new Managment_md_Ref_General3Result();

                Obj.id_md_ref_general3 = item.id_md_ref_general3;
                Obj.descripcion_general3 = item.descripcion_general3;
                Obj.peso = item.peso;

                foreach (var item2 in Lista1G3)
                {
                    if (Obj.id_md_ref_general3 == item2.id_md_ref_general3)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_md_interventoria_general = id_md_interventoria_general;
                        Obj.Nro = item2.Nro;
                    }

                }
                list1.Add(Obj);
            }

            return list1;
        }

        public List<Managment_md_Ref_General4Result> DetalleRefGeneral4(Int32 id_md_interventoria_general)
        {


            List<Managment_md_Ref_General4Result> list1 = new List<Managment_md_Ref_General4Result>();
            List<Managment_md_Ref_General4Result> list3 = new List<Managment_md_Ref_General4Result>();

            Lista1G4 = BusClass.DetalleRefInterventoriaGeneral4(id_md_interventoria_general, 1);
            Lista2G4 = BusClass.DetalleRefInterventoriaGeneral4(id_md_interventoria_general, 2);

            foreach (var item in Lista2G4)
            {
                Managment_md_Ref_General4Result Obj = new Managment_md_Ref_General4Result();

                Obj.id_md_ref_general4 = item.id_md_ref_general4;
                Obj.descripcion_general4 = item.descripcion_general4;
                Obj.peso = item.peso;

                foreach (var item2 in Lista1G4)
                {
                    if (Obj.id_md_ref_general4 == item2.id_md_ref_general4)
                    {
                        Obj.valor = item2.valor;
                        Obj.resultado = item2.resultado;
                        Obj.observaciones = item2.observaciones;
                        Obj.id_md_interventoria_general = id_md_interventoria_general;
                        Obj.Nro = item2.Nro;
                    }

                }
                list1.Add(Obj);
            }

            return list1;
        }



        public vw_total_md_interventoria_detalle Total_DetalleInterventoriaGeneralMD(Int32 id_md_interventoria_general)
        {
            return BusClass.Total_DetalleInterventoriaGeneralMD(id_md_interventoria_general);

        }


        public void ActualizarInterventoriaGeneralMD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarInterventoriaGeneralMD(OBJInterventoriaGeneral, ref MsgRes);
        }


        public List<md_interventoria_general_detalle1> GetInterventoriaDetalle1ID(Int32 id_md_ref_inte_general1, Int32 id_md_interventoria_general)
        {
            return BusClass.GetInterventoriaDetalle1ID(id_md_ref_inte_general1, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle2> GetInterventoriaDetalle2ID(Int32 id_md_ref_inte_general2, Int32 id_md_interventoria_general)
        {
            return BusClass.GetInterventoriaDetalle2ID(id_md_ref_inte_general2, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle3> GetInterventoriaDetalle3ID(Int32 id_md_ref_inte_general3, Int32 id_md_interventoria_general)
        {
            return BusClass.GetInterventoriaDetalle3ID(id_md_ref_inte_general3, id_md_interventoria_general);
        }

        public List<md_interventoria_general_detalle4> GetInterventoriaDetalle4ID(Int32 id_md_ref_inte_general4, Int32 id_md_interventoria_general)
        {
            return BusClass.GetInterventoriaDetalle4ID(id_md_ref_inte_general4, id_md_interventoria_general);
        }


        public void ActualizarInterventoriaGeneralDetalle1MD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarInterventoriaGeneralDetalle1MD(OBJDetallleG1, ref MsgRes);
        }
        public void ActualizarInterventoriaGeneralDetalle2MD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarInterventoriaGeneralDetalle2MD(OBJDetallleG2, ref MsgRes);
        }
        public void ActualizarInterventoriaGeneralDetalle3MD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarInterventoriaGeneralDetalle3MD(OBJDetallleG3, ref MsgRes);
        }
        public void ActualizarInterventoriaGeneralDetalle4MD(ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarInterventoriaGeneralDetalle4MD(OBJDetallleG4, ref MsgRes);
        }

        #endregion
    }
}