using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public string NumeroEmpleado { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? Nss { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Foto { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual ICollection<Dependiente> Dependientes { get; set; } = new List<Dependiente>();

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public string NombreEmpresa { get; set; }
}
