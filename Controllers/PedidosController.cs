using Microsoft.AspNetCore.Mvc;
using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_de_pedidos.Controllers
{
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedido.ToListAsync();
            return View("~/Views/Pedidos/Index.cshtml", pedidos);
        }
    }
}
