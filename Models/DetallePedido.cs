namespace Gestion_de_pedidos.Models
{
    // Models/DetallePedido.cs
    public class DetallePedido
    {
        public int Id { get; set; }

        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }

        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }

}
