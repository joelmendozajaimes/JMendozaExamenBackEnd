using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Ocupacion
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var ocupaciones = context.Ocupacions.ToList();
                    if (ocupaciones != null)
                    {
                        foreach (var e in ocupaciones)
                        {
                            ML.Ocupacion ocupacion = new ML.Ocupacion();
                            ocupacion.IdOcupacion = e.IdOcupacion;
                            ocupacion.Nombre = e.Nombre;

                            result.Objects.Add(ocupacion);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Ocupación.";
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
