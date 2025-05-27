using Microsoft.AspNetCore.Mvc;
using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using System.Linq;

namespace Gestion_de_pedidos.Controllers
{
    public class ComercialController : Controller
    {
        private readonly AppDbContext _context;

        public ComercialController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var comerciales = _context.Comercial.ToList(); // obtiene todos los comerciales
            return View("~/Views/Comercial/Index.cshtml",comerciales);
        }
    }
}
