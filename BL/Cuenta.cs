using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cuenta
    {
        public static ML.Result GetCuentasByIdPersona(int IdPersona)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var cuentas = context.Cuentas
                        .Include("Banco")
                        .Where(c => c.IdPersona == IdPersona)
                        .ToList();

                    if (cuentas != null)
                    {
                        foreach (var e in cuentas)
                        {
                            ML.Cuenta cuenta = new ML.Cuenta
                            {
                                IdCuenta = e.IdCuenta,
                                NumeroCuenta = e.NumeroCuenta.Value,
                                Persona = new ML.Persona { IdPersona = e.IdPersona.Value },
                                Banco = new ML.Banco { IdBanco = e.IdBanco.Value, Nombre = e.Banco.Nombre }
                            };

                            result.Objects.Add(cuenta);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Cuentas registradas para esta Persona.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Persona persona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var newCuenta = new DL.Cuenta
                    {
                        NumeroCuenta = persona.Cuenta.NumeroCuenta,
                        IdPersona = persona.IdPersona,
                        IdBanco = persona.Cuenta.Banco.IdBanco
                    };

                    context.Cuentas.Add(newCuenta);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdCuenta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var cuentaToDelete = context.Cuentas.Find(IdCuenta);
                    if (cuentaToDelete != null)
                    {
                        context.Cuentas.Remove(cuentaToDelete);
                        context.SaveChanges();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró la cuenta a eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
