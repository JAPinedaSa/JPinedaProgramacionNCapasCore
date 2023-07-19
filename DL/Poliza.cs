using System;
using System.Collections.Generic;

namespace DL;

public partial class Poliza
{
    public int IdPoliza { get; set; }

    public string? Nombre { get; set; }

    public byte? IdSubPoliza { get; set; }

    public string? NumeroPoliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual SubPoliza? IdSubPolizaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    //Propiedades Para la Navegacion con otras Entidades.
    public string NombreUsuario { get; set; }
    public string NombreSubPoliza { get; set; }
}
