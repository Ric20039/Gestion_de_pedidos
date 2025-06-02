using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gestion_de_pedidos.Models;
using Gestion_de_pedidos.Data; // Asegúrate de que este namespace sea correcto

namespace Gestion_de_pedidos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Index()
    {
        var model = new PanelViewModel
        {
            TotalClientes = _context.Cliente.Count(),
            TotalComerciales = _context.Comercial.Count(),
            TotalPedidos = _context.Pedido.Count(),
            TotalProductos = _context.Producto.Count(),

            UltimosPedidos = _context.Pedido
                .OrderByDescending(p => p.Fecha)
                .Take(5)
                .Select(p => new PedidoResumen
                {
                    Fecha = p.Fecha,
                    Cliente = p.Cliente.Nombre + " " + p.Cliente.Apellido1,
                    Comercial = p.Comercial.Nombre + " " + p.Comercial.Apellido1,
                    CantidadProductos = _context.DetallePedido
                        .Where(dp => dp.IdPedido == p.Id)
                        .Sum(dp => dp.Cantidad)
                }).ToList()
        };

        return View(model);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
