using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoPersonaFiscal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var tiposPersonaFiscal = context.TipoPersonaFiscals.ToList();
                    if (tiposPersonaFiscal != null)
                    {
                        foreach (var e in tiposPersonaFiscal)
                        {
                            ML.TipoPersonaFiscal tipoPersonaFiscal = new ML.TipoPersonaFiscal();
                            tipoPersonaFiscal.IdTipoPersonaFiscal = e.IdTipoPersonaFiscal;
                            tipoPersonaFiscal.Descripcion = e.Descripcion;

                            result.Objects.Add(tipoPersonaFiscal);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Tipo Persona Fiscal.";
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
