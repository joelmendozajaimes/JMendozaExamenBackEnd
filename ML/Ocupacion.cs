using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ocupacion
    {
        public int? IdOcupacion { get; set; }
        [Display(Name = "Ocupación")]
        public string Nombre { get; set; }
        public List<object> Ocupaciones { get; set; }
    }
}
