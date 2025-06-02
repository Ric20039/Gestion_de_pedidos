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

        [HttpPost]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Pedido.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // En caso de error, recargar con los datos
            var pedidos = _context.PedidoResultado.FromSqlRaw("EXEC sp_obtener_pedidos").ToList();

            var viewModel = new PedidoViewModel
            {
                PedidoNuevo = pedido,
                Pedidos = pedidos
            };

            return View("Index", viewModel);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var pedidos = _context.PedidoResultado.FromSqlRaw("EXEC sp_obtener_pedidos").ToList();

            var viewModel = new PedidoViewModel
            {
                PedidoNuevo = new Pedido(),
                Pedidos = pedidos
            };

            return View(viewModel);
        }


    }
}
