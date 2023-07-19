using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public string NumeroEmpleado { get; set; }

        public string RFC { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string FechaNacimiento { get; set; }

        public string NSS { get; set; }

        public string FechaRegistro { get; set; }

        public string Foto { get; set; }

        public Empresa Empresa { get; set; }

        public string Action { get; set; }

        public List<object> Empleados { get; set; }


    }
}
