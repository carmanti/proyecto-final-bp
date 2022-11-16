using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ProductoDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    [Required]
    public decimal Precio { get; set; }

    [StringLength(DominioConstantes.OBSERVACIONES_MAXIMO)]
    public string Observaciones { get; set; }

    [Required]
    public string Codigo { get; set; }

    [Column(TypeName = "Date")]
    public DateTime FechaIngreso { get; set; }

    [Column(TypeName = "Date")]
    public DateTime FechaCaducidad { get; set; }

    // [Required]
    // public int stock { get; set; }

    // [Required]
    // public bool Activo { get; set; }

    //Relaciones  otras entidades
    // [ForeignKey("Marca")]
    [Required]
    public int MarcaId { get; set; }
    public string Marca { get; set; }

    [Required]
    public int TipoProductoId { get; set; }
    public string TipoProducto { get; set; }

}
