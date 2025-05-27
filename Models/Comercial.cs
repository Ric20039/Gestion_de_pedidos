namespace Gestion_de_pedidos.Models
{
    // Models/Comercial.cs
    public class Comercial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Ciudad { get; set; }
        public float Comision { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }

}
