using System.ComponentModel.DataAnnotations.Schema;

[Table("producto")]
public class Producto
{
    [Column("id")] // ← CAMBIADO de "id_producto" a "id"
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
