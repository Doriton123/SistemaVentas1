using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaVentas.Models
{
    public class Producto
    {
        [Key]
        public int CodigoProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int CodigoCategoria { get; set; }

        // Navegación
        [ForeignKey("CodigoCategoria")]
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}

