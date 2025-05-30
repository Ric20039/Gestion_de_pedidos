using System.ComponentModel.DataAnnotations.Schema;
using Gestion_de_pedidos.Models;

[Table("detalle_pedido")]
public class DetallePedido
{
    public int Id { get; set; }

    [Column("id_pedido")]
    public int IdPedido { get; set; }

    [ForeignKey("IdPedido")]
    public Pedido Pedido { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [ForeignKey("IdProducto")]
    public Producto Producto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }
}
