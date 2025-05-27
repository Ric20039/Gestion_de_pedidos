using Gestion_de_pedidos.Models;
using Microsoft.EntityFrameworkCore;
using Gestion_de_pedidos.Models;

namespace Gestion_de_pedidos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Comercial> Comercial { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
}
