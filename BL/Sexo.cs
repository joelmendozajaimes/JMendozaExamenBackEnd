using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sexo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using(DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var sexos = context.Sexoes.ToList();
                    if (sexos != null)
                    {
                        foreach (var e in sexos)
                        {
                            ML.Sexo sexo = new ML.Sexo();
                            sexo.IdSexo = e.IdSexo;
                            sexo.Nombre = e.Nombre;

                            result.Objects.Add(sexo);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Sexo.";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
