using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOPETROL_COMMON.ENTIDADES;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.ConsultaAfiliado
{
    [SessionExpireFilter]
    public class ConsultaAfiliadoController : Controller
    {
        public ActionResult BusquedaUsuario()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }

        public ActionResult BusquedaUsuarioEvolucion()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }
        public ActionResult BusquedaUsuarioReporteEvento()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }
        public ActionResult BusquedaUsuarioReporteGLosa()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }
        public ActionResult BusquedaUsuarioCalidad()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }
        public ActionResult BusquedaUsuarioSeguimientoIndividual()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }

        public ActionResult BusquedaUsuarioGlosa()
        {
            Models.ConsultaAfiliado.ConsultaAfiliado Model = new Models.ConsultaAfiliado.ConsultaAfiliado();
            return View(Model);
        }


        [HttpPost]
        public ActionResult BusquedaUsuario(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult BusquedaUsuarioEvolucion(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult BusquedaUsuarioReporteEvento(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult BusquedaUsuarioReporteGLosa(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult BusquedaUsuarioCalidad(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult BusquedaUsuarioSeguimientoIndividual(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult BusquedaUsuarioGlosa(Models.ConsultaAfiliado.ConsultaAfiliado Model)
        {
            if (Model.Items == "2")
            {
                ModelState.Remove("numeroIdentificacion");
                ModelState.Remove("IdSeleccionado");
            }
            if (ModelState.IsValid)
            {
                if (Model.id_concurrencia == 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_afi = Model.numeroIdentificacion;
                    obj.afi_tipo_doc = Model.IdSeleccionado;
                    Model.LstConcurrencia = Model.ConsultaAfiliadoConcurrenia(obj);
                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }


                if (Model.id_concurrencia != 0)
                {
                    ecop_concurrencia obj = new ecop_concurrencia();
                    obj.id_concurrencia = Model.id_concurrencia;
                    Model.LstConcurrencia = Model.ConsultaIdConcurrenia(obj);

                    if (Model.LstConcurrencia.Count == 0)
                    {
                        ModelState.AddModelError("", "ERROR: *---No se encuentran registros----*");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "ERROR: *---Debe ingresar los campos obligatorios--- *");
            }
            return View(Model);
        }
    }
}