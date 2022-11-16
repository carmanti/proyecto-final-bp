using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

public class OrdenItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int ProductoId { get; set; }
    public virtual Producto Producto { get; set; }

    [Required]
    public int OrdenId { get; set; }
    public virtual Orden Orden { get; set; }

    [Required]
    public long Cantidad { get; set; }

    public decimal Precio { get; set; }

    [StringLength(DominioConstantes.OBSERVACIONES_MAXIMO)]
    public string Observaciones { get; set; }



}
