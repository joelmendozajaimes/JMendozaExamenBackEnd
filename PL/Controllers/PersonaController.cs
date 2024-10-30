using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Persona persona = new ML.Persona();
            persona.TipoPersonaFiscal = new ML.TipoPersonaFiscal();
            persona.Pais = new ML.Pais();
            persona.Ocupacion = new ML.Ocupacion();
            persona.Sexo = new ML.Sexo();

            ML.Result result = BL.Persona.GetAll();

            if (result.Correct)
            {
                persona.Personas = result.Objects;
            }
            else
            {
                ViewBag.Message = "No hay registros en Persona. Error: " + result.ErrorMessage;
            }

            return View(persona);
        }

        [HttpGet]
        public ActionResult Form(int? IdPersona, string tipoPersona)
        {
            ML.Persona persona = new ML.Persona();
            ML.Result result = new ML.Result();

            if(IdPersona == 0 ||  IdPersona == null)
            {
                result = BL.Ocupacion.GetAll();
                persona.Ocupacion = new ML.Ocupacion();
                persona.Ocupacion.Ocupaciones = result.Objects;

                result = BL.Pais.GetAll();
                persona.Pais = new ML.Pais();
                persona.Pais.Paises = result.Objects;

                result = BL.Sexo.GetAll();
                persona.Sexo = new ML.Sexo();
                persona.Sexo.Sexos = result.Objects;

                result = BL.TipoPersonaFiscal.GetAll();
                persona.TipoPersonaFiscal = new ML.TipoPersonaFiscal();
                persona.TipoPersonaFiscal.Descripcion = tipoPersona;
                persona.TipoPersonaFiscal.TiposPersonaFiscal = result.Objects;

                persona.Cuenta = new ML.Cuenta();
                persona.Cuenta.Cuentas = new List<object>();

                result = BL.Banco.GetAll();
                persona.Cuenta.Banco = new ML.Banco();
                persona.Cuenta.Banco.Bancos = result.Objects;
            }
            else
            {
                result = BL.Persona.GetById(IdPersona.Value);

                if (result.Correct)
                {
                    persona = (ML.Persona)result.Object;
                    if (persona.TipoPersonaFiscal.Descripcion == "Moral")
                    {
                        result = BL.Ocupacion.GetAll();
                        persona.Ocupacion = new ML.Ocupacion();
                        persona.Ocupacion.Ocupaciones = result.Objects;

                        result = BL.Pais.GetAll();
                        persona.Pais.Paises = result.Objects;

                        result = BL.Sexo.GetAll();
                        persona.Sexo = new ML.Sexo();
                        persona.Sexo.Sexos = result.Objects;

                        result = BL.TipoPersonaFiscal.GetAll();
                        persona.TipoPersonaFiscal.TiposPersonaFiscal = result.Objects;
                    }
                    else
                    {
                        persona = (ML.Persona)result.Object;

                        result = BL.Ocupacion.GetAll();
                        persona.Ocupacion.Ocupaciones = result.Objects;

                        result = BL.Pais.GetAll();
                        persona.Pais.Paises = result.Objects;

                        result = BL.Sexo.GetAll();
                        persona.Sexo.Sexos = result.Objects;

                        result = BL.TipoPersonaFiscal.GetAll();
                        persona.TipoPersonaFiscal.TiposPersonaFiscal = result.Objects;
                    }

                    result = BL.Cuenta.GetCuentasByIdPersona(persona.IdPersona);
                    persona.Cuenta = new ML.Cuenta();
                    persona.Cuenta.Cuentas = result.Objects;

                    result = BL.Banco.GetAll();
                    persona.Cuenta.Banco = new ML.Banco();
                    persona.Cuenta.Banco.Bancos = result.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al traer la información de la Persona. " + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }

            return View(persona);
        }

        [HttpPost]
        public ActionResult Form(ML.Persona persona)
        {
            ML.Result result = new ML.Result();
            ViewBag.IsCuenta = false;
            if (persona.IdPersona == 0)
            {
                result = BL.Persona.Add(persona);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha insertado la persona.";
                }
                else
                {
                    ViewBag.Message = "No se ha insertado la persona. " + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Persona.Update(persona);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado la persona.";
                }
                else
                {
                    ViewBag.Message = "No se ha actualizado la persona. " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdPersona)
        {
            ML.Result result = new ML.Result();
            ViewBag.IsCuenta = false;
            if (IdPersona != 0)
            {
                result = BL.Persona.Delete(IdPersona);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha eliminado la persona.";
                }
                else
                {
                    ViewBag.Message = "No se ha eliminado la persona. " + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "No se seleccionó ninguna persona.";
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult DeleteCuenta(int IdCuenta, int IdPersona, string tipoPersona)
        {
            ViewBag.IsCuenta = true;

            ViewBag.IdPersona = IdPersona;

            ViewBag.TipoPersona = tipoPersona;

            ML.Result result = new ML.Result();
            if (IdCuenta != 0)
            {
                result = BL.Cuenta.Delete(IdCuenta);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha eliminado la cuenta.";
                }
                else
                {
                    ViewBag.Message = "No se ha eliminado la cuenta. " + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "No se seleccionó ninguna cuenta.";
            }
            return PartialView("Modal");
        }

        public ActionResult AddCuenta(ML.Persona persona)
        {
            ViewBag.IsCuenta = true;

            ViewBag.IdPersona = persona.IdPersona;

            ViewBag.TipoPersona = persona.TipoPersonaFiscal.Descripcion;

            ML.Result result = new ML.Result();

            result = BL.Cuenta.Add(persona);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha insertado la cuenta.";
            }
            else
            {
                ViewBag.Message = "No se ha insertado la cuenta. " + result.ErrorMessage;
            }


            return PartialView("Modal");
        }
    }
}