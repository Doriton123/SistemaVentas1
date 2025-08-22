using System.ComponentModel.DataAnnotations;

namespace TiendaVentas.Models
{
    public class Categoria
    {
        [Key]
        public int CodigoCategoria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Relación con productos
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
