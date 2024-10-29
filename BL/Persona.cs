using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Persona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var personas = context.Personas
                          .Include("TipoPersonaFiscal")
                          .Include("Pai")
                          .Include("Ocupacion")
                          .Include("Sexo")
                          .ToList();

                    if (personas != null)
                    {
                        foreach (var e in personas)
                        {
                            ML.Persona persona = new ML.Persona();
                            persona.TipoPersonaFiscal = new ML.TipoPersonaFiscal();
                            persona.Pais = new ML.Pais();
                            persona.Ocupacion = new ML.Ocupacion();
                            persona.Sexo = new ML.Sexo();

                            persona.IdPersona = e.IdPersona;
                            persona.Nombre = e.Nombre;
                            persona.ApellidoPaterno = e.ApellidoPaterno;
                            persona.ApellidoMaterno = e.ApellidoMaterno;
                            persona.TipoPersonaFiscal.IdTipoPersonaFiscal = e.IdTipoPersonaFiscal.Value;
                            persona.TipoPersonaFiscal.Descripcion = e.TipoPersonaFiscal.Descripcion;
                            persona.RFC = e.RFC;
                            persona.CURP = e.CURP;
                            persona.Pais.IdPais = e.IdPais.Value;
                            persona.Pais.Nombre = e.Pai.Nombre;
                            persona.Estatus = e.Estatus.Value;

                            if (e.Ocupacion != null)
                            {
                                persona.Ocupacion = new ML.Ocupacion
                                {
                                    IdOcupacion = e.IdOcupacion.HasValue ? e.IdOcupacion.Value : (int?)null,
                                    Nombre = e.Ocupacion.Nombre
                                };
                            }
                            else
                            {
                                persona.Ocupacion = null;
                            }

                            if (e.Sexo != null)
                            {
                                persona.Sexo = new ML.Sexo
                                {
                                    IdSexo = e.IdSexo.HasValue ? e.IdSexo.Value : (int?)null,
                                    Nombre = e.Sexo.Nombre
                                };
                            }
                            else
                            {
                                persona.Sexo = null; 
                            }

                            result.Objects.Add(persona);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Persona.";
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
                using(DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    DL.Persona newPersona = new DL.Persona
                    {
                        Nombre = persona.Nombre,
                        ApellidoPaterno = persona.ApellidoPaterno,
                        ApellidoMaterno = persona.ApellidoMaterno,
                        IdTipoPersonaFiscal = persona.TipoPersonaFiscal.IdTipoPersonaFiscal,
                        RFC = persona.RFC,
                        CURP = persona.CURP,
                        IdPais = persona.Pais.IdPais,
                        IdOcupacion = persona.Ocupacion?.IdOcupacion,
                        IdSexo = persona.Sexo?.IdSexo,
                        Estatus = persona.Estatus
                    };

                    context.Personas.Add(newPersona);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Persona persona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var existingPersona = context.Personas.SingleOrDefault(p => p.IdPersona == persona.IdPersona);
                    if (existingPersona != null)
                    {
                        existingPersona.Nombre = persona.Nombre;
                        existingPersona.ApellidoPaterno = persona.ApellidoPaterno;
                        existingPersona.ApellidoMaterno = persona.ApellidoMaterno;
                        existingPersona.IdTipoPersonaFiscal = persona.TipoPersonaFiscal.IdTipoPersonaFiscal;
                        existingPersona.RFC = persona.RFC;
                        existingPersona.CURP = persona.CURP;
                        existingPersona.IdPais = persona.Pais.IdPais;
                        existingPersona.IdOcupacion = persona.Ocupacion?.IdOcupacion;
                        existingPersona.IdSexo = persona.Sexo?.IdSexo;
                        existingPersona.Estatus = persona.Estatus;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Persona no encontrada.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdPersona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var personaToDelete = context.Personas.SingleOrDefault(p => p.IdPersona == IdPersona);
                    if (personaToDelete != null)
                    {
                        context.Personas.Remove(personaToDelete);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Persona no encontrada.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdPersona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var e = context.Personas
                           .Include("TipoPersonaFiscal")
                           .Include("Pai")
                           .Include("Ocupacion")
                           .Include("Sexo")
                           .SingleOrDefault(p => p.IdPersona == IdPersona);

                    if (e != null)
                    {
                        ML.Persona persona = new ML.Persona
                        {
                            TipoPersonaFiscal = new ML.TipoPersonaFiscal { IdTipoPersonaFiscal = e.TipoPersonaFiscal.IdTipoPersonaFiscal, Descripcion = e.TipoPersonaFiscal.Descripcion },
                            Pais = new ML.Pais { IdPais = e.Pai.IdPais, Nombre = e.Pai.Nombre },
                            Ocupacion = e.Ocupacion != null ? new ML.Ocupacion
                            {
                                IdOcupacion = e.Ocupacion.IdOcupacion, 
                                Nombre = e.Ocupacion.Nombre
                            } : null,
                            Sexo = e.Sexo != null ? new ML.Sexo
                            {
                                IdSexo = e.Sexo.IdSexo,
                                Nombre = e.Sexo.Nombre
                            } : null,
                            IdPersona = e.IdPersona,
                            Nombre = e.Nombre,
                            ApellidoPaterno = e.ApellidoPaterno,
                            ApellidoMaterno = e.ApellidoMaterno,
                            RFC = e.RFC,
                            CURP = e.CURP,
                            Estatus = e.Estatus.Value
                        };

                        result.Object = persona;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Persona no encontrada.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
