using Microsoft.EntityFrameworkCore;

    namespace Gestion_de_pedidos.Models
    {
        [Keyless]
        public class PedidoResultado
        {
            public double Cantidad { get; set; }
            public DateTime Fecha { get; set; }
            public string Nombre_Cliente { get; set; }
            public string Nombre_Comercial { get; set; }
        }
    }

