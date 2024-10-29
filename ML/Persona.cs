using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        public ML.TipoPersonaFiscal TipoPersonaFiscal { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public ML.Pais Pais { get; set; }
        public ML.Ocupacion Ocupacion { get; set; }
        public ML.Sexo Sexo { get; set; }
        public bool Estatus { get; set; }
        public List<object> Personas { get; set; }
        public ML.Cuenta Cuenta { get; set; }
    }
}
