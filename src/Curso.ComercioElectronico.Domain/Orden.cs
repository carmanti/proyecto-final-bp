using System.Net.Cache;
using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

public class Orden
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Codigo { get; set; }

    [Required]
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<OrdenItem> Items { get; set; }

    [StringLength(DominioConstantes.OBSERVACIONES_MAXIMO)]
    public string Observaciones { get; set; }

    [Required]
    public DateTime fecha { get; set; }

    public DateTime FechaAnulacion { get; set; }

    [Required]
    public decimal Total { get; set; }

    //Agregar mas propiedades

    public OrdenEstado Estado { get; set; }

    public void AgregarItem(OrdenItem item)
    {
        item.Orden = this;
        Items.Add(item);
    }
}

public enum OrdenEstado
{
    Registrado = 1,
    Anulada = 0,
    Procesado = 2,
    Entregada = 3
}
