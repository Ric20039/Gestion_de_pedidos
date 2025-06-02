using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class ClienteController : Controller
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    // Mostrar todos los clientes
    public IActionResult Index()
    {
        var clientes = _context.Cliente.FromSqlRaw("EXEC sp_obtener_clientes").ToList();
        ViewBag.ClienteForm = new Cliente(); // Asegurarse de enviar un formulario vacío
        return View(clientes);
    }

    [HttpPost]
    public IActionResult Insertar(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_insertar_cliente @p0, @p1, @p2, @p3, @p4",
                cliente.Nombre, cliente.Apellido1, cliente.Apellido2, cliente.Ciudad, cliente.Categoria
            );
            return RedirectToAction("Index");
        }

        var clientes = _context.Cliente.FromSqlRaw("EXEC sp_obtener_clientes").ToList();
        ViewBag.ClienteForm = cliente;
        return View("Index", clientes);
    }

    [HttpPost]
    public IActionResult Actualizar(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_actualizar_cliente @p0, @p1, @p2, @p3, @p4, @p5",
                cliente.Id, cliente.Nombre, cliente.Apellido1, cliente.Apellido2, cliente.Ciudad, cliente.Categoria
            );
            return RedirectToAction("Index");
        }

        var clientes = _context.Cliente.FromSqlRaw("EXEC sp_obtener_clientes").ToList();
        ViewBag.ClienteForm = cliente;
        return View("Index", clientes);
    }

    public IActionResult Editar(int id)
    {
        var cliente = _context.Cliente.FirstOrDefault(c => c.Id == id);
        var clientes = _context.Cliente.ToList();

        ViewBag.ClienteForm = cliente;
        return View("Index", clientes);
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        _context.Database.ExecuteSqlRaw("EXEC sp_eliminar_cliente @p0", id);
        return RedirectToAction("Index");
    }

    // ✅ Nueva acción para buscar cliente por nombre
    [HttpPost]
    public IActionResult BuscarPorNombre(string nombreBusqueda)
    {
        if (string.IsNullOrWhiteSpace(nombreBusqueda))
        {
            return RedirectToAction("Index");
        }

        var clientes = _context.Cliente
            .FromSqlRaw("EXEC sp_buscar_clientes_por_nombre @p0", nombreBusqueda)
            .ToList();

        ViewBag.ClienteForm = new Cliente(); // Dejar el formulario limpio
        ViewBag.ResultadoBusqueda = $"Se encontraron {clientes.Count} cliente(s) con el nombre '{nombreBusqueda}'";

        return View("Index", clientes);
    }
    public async Task<IActionResult> ClientesPorCategoria()
    {
        var resultado = await _context.ClientesPorCategoriaDTO
            .FromSqlRaw("EXEC sp_ClientesPorCategoria")
            .ToListAsync();

        ViewBag.Titulo = "Clientes por Categoría";
        return View("ClientesPorCategoria", resultado); // Crearás esta vista a continuación
    }
    public async Task<IActionResult> ClientesPorCiudad()
    {
        var clientes = await _context.ClientesPorCiudadDTO
            .FromSqlRaw("EXEC sp_ClientesPorCiudad")
            .ToListAsync();

        ViewBag.Titulo = "Clientes por Ciudad";
        return View("ClientesPorCiudad", clientes); // Usa una vista distinta
    }
}
