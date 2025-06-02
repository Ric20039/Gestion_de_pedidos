using Microsoft.AspNetCore.Mvc;

namespace Gestion_de_pedidos.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
