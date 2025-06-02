using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_de_pedidos.Models
{
    // Models/Pedido.cs
    public class Pedido
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public int Id_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Id_Comercial")]
        public int Id_Comercial { get; set; }
        public Comercial Comercial { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
    }
   

}
