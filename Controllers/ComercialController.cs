using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_de_pedidos.Controllers
{
    public class ComercialController : Controller
    {
        private readonly AppDbContext _context;

        public ComercialController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var comerciales = await _context.Comercial
                .FromSqlRaw("EXEC sp_ObtenerComerciales")
                .ToListAsync();

            return View(comerciales);
        }

        public async Task<IActionResult> VerClientes(int id)
        {
            var clientes = await _context.Cliente
                .FromSqlRaw("EXEC sp_ObtenerClientesPorComercial @p0", id)
                .ToListAsync();

            ViewBag.ComercialId = id;
            return View("ClientesPorComercial", clientes);
        }

    }
}
