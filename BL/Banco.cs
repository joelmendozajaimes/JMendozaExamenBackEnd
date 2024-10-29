using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Banco
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.JMendozaExamenBackEndEntities context = new DL.JMendozaExamenBackEndEntities())
                {
                    var bancos = context.Bancoes.ToList();
                    if (bancos != null)
                    {
                        foreach (var e in bancos)
                        {
                            ML.Banco banco = new ML.Banco();
                            banco.IdBanco = e.IdBanco;
                            banco.Nombre = e.Nombre;

                            result.Objects.Add(banco);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Banco.";
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
