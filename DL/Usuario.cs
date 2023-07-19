using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string? Correo { get; set; }

    public int IdRol { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public string Password { get; set; } = null!;

    public string? Imagen { get; set; }

    public bool? Estatus { get; set; }

    public virtual ICollection<Aseguradora> Aseguradoras { get; set; } = new List<Aseguradora>();

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();

    //Propiedades de Navegacion con otras entidades ()


    public string Calle { get; set; }
    public string NumeroInterior { get; set; }
    public string NumeroExterior { get; set; }
    public int IdColonia { get; set; }
    public string Colonia { get; set; }
    public int IdMunicipio { get; set; }
    public string Municipio { get; set; }
    public int IdEstado { get; set; }
    public string Estado { get; set; }
    public int IdPais { get; set; }
    public string Pais { get; set; }
}
