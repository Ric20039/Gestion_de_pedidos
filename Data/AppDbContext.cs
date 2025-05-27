using Gestion_de_pedidos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        // MÉTODOS PARA CLIENTE USANDO SOLO Cliente
        public List<Cliente> ObtenerClientes()
        {
            return Cliente.FromSqlRaw("EXEC sp_obtener_clientes").ToList();
        }

        public void InsertarCliente(Cliente cliente)
        {
            Database.ExecuteSqlRaw("EXEC sp_insertar_cliente @p0, @p1, @p2, @p3, @p4",
                cliente.Nombre, cliente.Apellido1, cliente.Apellido2, cliente.Ciudad, cliente.Categoria);
        }

        public void ActualizarCliente(Cliente cliente)
        {
            Database.ExecuteSqlRaw("EXEC sp_actualizar_cliente @p0, @p1, @p2, @p3, @p4, @p5",
                cliente.Id, cliente.Nombre, cliente.Apellido1, cliente.Apellido2, cliente.Ciudad, cliente.Categoria);
        }

        public void EliminarCliente(int id)
        {
            Database.ExecuteSqlRaw("EXEC sp_eliminar_cliente @p0", id);
        }
    }
}
