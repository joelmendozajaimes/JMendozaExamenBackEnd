using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Sexo
    {
        public int? IdSexo { get; set; }
        [Display(Name = "Sexo")]
        public string Nombre { get; set; }
        public List<object> Sexos { get; set; }
    }
}
