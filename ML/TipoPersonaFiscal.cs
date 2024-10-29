using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class TipoPersonaFiscal
    {
        public int IdTipoPersonaFiscal { get; set; }
        [Display(Name = "Tipo de Persona Fiscal")]
        public string Descripcion { get; set; }
        public List<object> TiposPersonaFiscal { get; set; }
    }
}
