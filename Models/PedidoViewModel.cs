namespace Gestion_de_pedidos.Models
{
    public class PedidoViewModel
    {
        public Pedido PedidoNuevo { get; set; } = new Pedido();
        public List<PedidoResultado> Pedidos { get; set; } = new List<PedidoResultado>();
    }
}
