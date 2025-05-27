namespace Gestion_de_pedidos.Models
{
    // Models/Producto.cs
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float PrecioUnitario { get; set; }
        public int Stock { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
    }

}
