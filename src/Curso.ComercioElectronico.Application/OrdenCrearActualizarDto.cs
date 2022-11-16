using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application
{
    public class OrdenCrearActualizarDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        // public virtual Cliente Cliente { get; set; }
        [Required]
        public string Codigo { get; set; }

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
    }
}