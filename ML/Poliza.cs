using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Poliza
    {
        public int IdPoliza { get; set; }

        public string Nombre { get; set; }

        public byte IdSubPoliza { get; set; }

        public string NumeroPoliza { get; set; }

        public string FechaCreacion { get; set; }

        public string FechaModificacion { get; set; }

        public int IdUsuario { get; set; }

        public SubPoliza SubPoliza { get; set; }

        public Usuario Usuario { get; set; }
        public List<object> Polizas { get; set; }

    }
}
