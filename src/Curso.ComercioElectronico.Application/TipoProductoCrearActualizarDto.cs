using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

public class TipoProductoCrearActualizarDto
{

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }


}