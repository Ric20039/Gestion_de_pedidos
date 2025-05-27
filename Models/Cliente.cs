namespace Gestion_de_pedidos.Models
{
    // Models/Cliente.cs
        public class Cliente
        {
            public int Id { get; set; } // Asegúrate de incluir el Id para update/delete
            public string? Nombre { get; set; }
            public string? Apellido1 { get; set; }
            public string? Apellido2 { get; set; }
            public string? Ciudad { get; set; }
            public int? Categoria { get; set; }
        }
   

}
