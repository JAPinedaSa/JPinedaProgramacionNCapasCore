using System;
using System.Collections.Generic;

namespace DL;

public partial class Dependiente
{
    public int IdDependiente { get; set; }

    public string IdEmpleado { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string? EstadoCivil { get; set; }

    public string Genero { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Rfc { get; set; }

    public int IdDependienteTipo { get; set; }

    public virtual DependienteTipo IdDependienteTipoNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;


    // PROPIEDADES PARA LA RELACION CON OTRAS ENTIDADES

    public string NombreDependienteTipo { get; set; }
    public string NombreEmpleado { get; set; }
}
