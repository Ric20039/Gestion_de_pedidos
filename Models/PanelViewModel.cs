namespace Gestion_de_pedidos.Models;

public class PanelViewModel
{
    public int TotalClientes { get; set; }
    public int TotalComerciales { get; set; }
    public int TotalPedidos { get; set; }
    public int TotalProductos { get; set; }
    public List<PedidoResumen> UltimosPedidos { get; set; }
}

public class PedidoResumen
{
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; }
    public string Comercial { get; set; }
    public int CantidadProductos { get; set; }
}
