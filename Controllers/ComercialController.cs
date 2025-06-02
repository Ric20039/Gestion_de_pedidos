using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class ComercialController : Controller
{
    private readonly AppDbContext _context;

    public ComercialController(AppDbContext context)
    {
        _context = context;
    }

    // Mostrar todos los comerciales
    public IActionResult Index()
    {
        var comerciales = _context.Comercial.FromSqlRaw("EXEC sp_obtener_comerciales").ToList();
        ViewBag.ComercialForm = new Comercial(); // formulario vacío
        return View(comerciales);
    }

    [HttpPost]
    public IActionResult Insertar(Comercial comercial)
    {
        if (comercial.Id == 0)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_insertar_comercial @p0, @p1, @p2, @p3, @p4",
                    comercial.Nombre, comercial.Apellido1, comercial.Apellido2,
                    comercial.Comision, comercial.Ciudad
                );
                return RedirectToAction("Index");
            }
        }
        else
        {
            // si tiene ID, actualiza
            return Actualizar(comercial);
        }

        var comerciales = _context.Comercial.FromSqlRaw("EXEC sp_obtener_comerciales").ToList();
        ViewBag.ComercialForm = comercial;
        return View("Index", comerciales);
    }

    [HttpPost]
    public IActionResult Actualizar(Comercial comercial)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_actualizar_comercial @p0, @p1, @p2, @p3, @p4, @p5",
                comercial.Id, comercial.Nombre, comercial.Apellido1, comercial.Apellido2,
                comercial.Ciudad, comercial.Comision
            );

            return RedirectToAction("Index");
        }

        var comerciales = _context.Comercial.FromSqlRaw("EXEC sp_obtener_comerciales").ToList();
        ViewBag.ComercialForm = comercial;
        return View("Index", comerciales);
    }

    public IActionResult Editar(int id)
    {
        var comercial = _context.Comercial.FirstOrDefault(c => c.Id == id);
        var comerciales = _context.Comercial.ToList();

        ViewBag.ComercialForm = comercial;
        return View("Index", comerciales);
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        _context.Database.ExecuteSqlRaw("EXEC sp_eliminar_comercial @p0", id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ClientesPorComercial(int IdComercial)
    {
        var clientes = _context.Cliente
            .FromSqlRaw("EXEC sp_ObtenerClientesPorComercial @p0", IdComercial)
            .ToList();

        var comercial = _context.Comercial.FirstOrDefault(c => c.Id == IdComercial);
        ViewBag.NombreComercial = comercial != null
            ? $"{comercial.Nombre} {comercial.Apellido1} {comercial.Apellido2}"
            : "Desconocido";

        return View("ClientesPorComercial", clientes);
    }
    public async Task<IActionResult> PedidosPorCliente(int clienteId)
    {
        var pedidos = await _context.Pedido
         .Where(p => p.Id_Cliente == clienteId)
         .Include(p => p.Comercial)
         .Include(p => p.Detalles)
             .ThenInclude(d => d.Producto)
         .ToListAsync();


        if (pedidos == null || pedidos.Count == 0)
        {
            ViewBag.ClienteId = clienteId;
            return View("PedidosPorCliente", new List<Pedido>()); // Pasamos lista vacía si no hay pedidos
        }

        return View("PedidosPorCliente", pedidos);
    }
}
