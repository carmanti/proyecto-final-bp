using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ClienteCrearActualizarDto
{

    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres { get; set; }

    [Required]
    public string Direccion { get; set; }

    [Required]
    public string Telefono { get; set; }

    [Required]
    public string Cedula { get; set; }

    public int Edad { get; set; }

    [Required]
    public bool MayorEdad { get; set; }

    public Referencia referencia { get; set; }

    [StringLength(DominioConstantes.OBSERVACIONES_MAXIMO)]
    public string Observaciones { get; set; }
}