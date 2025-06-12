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
    public class Cronograma
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


        public List<md_ref_puntos_dispensacion> _lisPuntosDispersacion;
        public List<md_ref_puntos_dispensacion> lisPuntosDispersacion
        {
            get
            {
                if (_lisPuntosDispersacion == null)
                {
                    _lisPuntosDispersacion = BusClass.PuntosDispercion();

                    _lisPuntosDispersacion = _lisPuntosDispersacion.OrderBy(X => X.nombre_esm).ToList();

                    return _lisPuntosDispersacion;
                }
                else
                {
                    return _lisPuntosDispersacion;
                }

            }

            set
            {
                _lisPuntosDispersacion = value;
            }
        }

        private List<md_ref_tipo_visita> _LstMdTipoVisita;
        public List<md_ref_tipo_visita> LstMdTipoVisita
        {
            get
            {
                if (_LstMdTipoVisita == null)
                {
                    _LstMdTipoVisita = BusClass.GetMdTipoVisita();
                }

                return _LstMdTipoVisita;
            }
            set { _LstMdTipoVisita = value; }
        }

        private List<vw_md_crono_auditores> _ListCronoAuditor;
        public List<vw_md_crono_auditores> ListCronoAuditor
        {
            get
            {
                if (_ListCronoAuditor == null)
                {
                    return _ListCronoAuditor = new List<vw_md_crono_auditores>();
                }
                else
                {
                    return _ListCronoAuditor;
                }

            }

            set
            {
                _ListCronoAuditor = value;
            }
        }

        private List<vw_md_crono_auditores> _CronoAuditores;
        public List<vw_md_crono_auditores> CronoAuditores
        {
            get
            {
                if (_CronoAuditores == null)
                {
                    return _CronoAuditores = BusClass.GetRefCronoAuditor();
                }
                else
                {
                    return _CronoAuditores;
                }

            }

            set
            {
                _CronoAuditores = value;
            }
        }


        private md_crono_visita _OBJCrono;
        public md_crono_visita OBJCrono
        {
            get
            {
                if (_OBJCrono == null)
                {
                    return _OBJCrono = new md_crono_visita();
                }
                else
                {
                    return _OBJCrono;
                }

            }

            set
            {
                _OBJCrono = value;
            }
        }

        private List<vw_md_crono> _LstCronoVi;
        public List<vw_md_crono> LstCronoVi
        {
            get
            {
                if (_LstCronoVi == null)
                {
                    if (SesionVar.ROL == "1")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();
                    }
                    else if (grupo_auditor == "GRUPO 1")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 1").ToList();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

                    }
                    else if (grupo_auditor == "GRUPO 2")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 2").ToList();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

                    }
                    else if (grupo_auditor == "GRUPO 3")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 3").ToList();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

                    }
                    else if (grupo_auditor == "GRUPO 4")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 4").ToList();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

                    }
                    else if (grupo_auditor == "GRUPO 5")
                    {
                        _LstCronoVi = BusClass.ConsultaCronograma();
                        _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 5").ToList();
                        _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

                    }
                    return _LstCronoVi;
                }
                else
                {
                    return _LstCronoVi;
                }

            }

            set
            {
                _LstCronoVi = value;
            }
        }

        private List<Managment_md_Ref_crono_auditoresResult> _LstCronoAuditoresGrupo;
        public List<Managment_md_Ref_crono_auditoresResult> LstCronoAuditoresGrupo
        {
            get
            {
                if (_LstCronoAuditoresGrupo == null)
                {
                    _LstCronoAuditoresGrupo = new List<Managment_md_Ref_crono_auditoresResult>();
                }

                return _LstCronoAuditoresGrupo;
            }
            set

            { _LstCronoAuditoresGrupo = value; }


        }




        public int id_cronograma_visitas { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA VISITA")]
        public DateTime? fecha_visita { get; set; }
        public DateTime? fecha_visitaOK { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "PUNTOS DISPENSACION")]
        public Int32? id_puntos_dispensacion { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO VISITA")]
        public String tipo_visita { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "AUDITOR")]
        public String auditor_grupo { get; set; }


        public Int32 id_usuario { get; set; }

        public String usuario { get; set; }


        public String grupo_auditor { get; set; }




        #endregion

        #region FUNCIONES


        public void InsertarCronoVisitas(ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCronoVisitas(OBJCrono, ref MsgRes);
        }


        public void ConsultaCronoUsuario(String usuario)
        {
            vw_md_crono_auditores OBJAuditor = new vw_md_crono_auditores();
            OBJAuditor.usuario_auditor = usuario;
            ListCronoAuditor = BusClass.GetUsuarioCronoId(usuario, ref MsgRes);


            foreach (var item in ListCronoAuditor)
            {
                id_usuario = item.id_auditor;
                usuario = item.usuario_auditor;
                grupo_auditor = Convert.ToString(item.descripcion_grupo);

            }

        }


        public List<Managment_md_Ref_crono_auditoresResult> ConsultaListaCronoAuditoresGrupo(int opc1, Int32? opc2, ref MessageResponseOBJ MsgRes)
        {

            List<Managment_md_Ref_crono_auditoresResult> listadoauditores = BusClass.ConsultaListaCronoAuditores(opc1, opc2, ref MsgRes);

            return listadoauditores;


        }

        public List<vw_md_crono> listaCronograma()
        {

            if (SesionVar.ROL == "1")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();
            }
            else if (grupo_auditor == "GRUPO 1")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 1").ToList();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

            }
            else if (grupo_auditor == "GRUPO 2")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 2").ToList();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

            }
            else if (grupo_auditor == "GRUPO 3")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 3").ToList();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

            }
            else if (grupo_auditor == "GRUPO 4")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 4").ToList();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

            }
            else if (grupo_auditor == "GRUPO 5")
            {
                _LstCronoVi = BusClass.ConsultaCronograma();
                _LstCronoVi = _LstCronoVi.Where(x => x.grupo_auditor == "GRUPO 5").ToList();
                _LstCronoVi = _LstCronoVi.OrderBy(x => x.dias_restantes).ToList();

            }

            return _LstCronoVi;
        }

        #endregion
    }
}