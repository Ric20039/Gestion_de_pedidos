using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_de_pedidos.Models
{
    [Table("producto")]
    public class Producto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("precio_unitario")]
        public double PrecioUnitario { get; set; }

        [Column("stock")]
        public int Stock { get; set; }
    }
}
