using Gestion_de_pedidos.Data;
using Gestion_de_pedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class ProductosController : Controller
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    // Mostrar todos los productos
    public IActionResult Index()
    {
        var productos = _context.Producto.FromSqlRaw("EXEC sp_obtener_productos").ToList();
        ViewBag.ProductoForm = new Producto(); // Formulario vacío
        return View(productos);
    }

    [HttpPost]
    public IActionResult Insertar(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_insertar_producto @p0, @p1, @p2, @p3",
                producto.Nombre, producto.Descripcion, producto.PrecioUnitario, producto.Stock
            );
            return RedirectToAction("Index");
        }

        var productos = _context.Producto.FromSqlRaw("EXEC sp_obtener_productos").ToList();
        ViewBag.ProductoForm = producto;
        return View("Index", productos);
    }

    [HttpPost]
    public IActionResult Actualizar(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_actualizar_producto @p0, @p1, @p2, @p3, @p4",
                producto.Id, producto.Nombre, producto.Descripcion, producto.PrecioUnitario, producto.Stock
            );
            return RedirectToAction("Index");
        }

        var productos = _context.Producto.FromSqlRaw("EXEC sp_obtener_productos").ToList();
        ViewBag.ProductoForm = producto;
        return View("Index", productos);
    }

    public IActionResult Editar(int id)
    {
        var producto = _context.Producto.FirstOrDefault(p => p.Id == id);
        var productos = _context.Producto.FromSqlRaw("EXEC sp_obtener_productos").ToList();

        ViewBag.ProductoForm = producto;
        return View("Index", productos);
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        _context.Database.ExecuteSqlRaw("EXEC sp_eliminar_producto @p0", id);
        return RedirectToAction("Index");
    }

    // ✅ Buscar productos por nombre
    [HttpPost]
    public IActionResult BuscarPorNombre(string nombreBusqueda)
    {
        if (string.IsNullOrWhiteSpace(nombreBusqueda))
        {
            return RedirectToAction("Index");
        }

        var productos = _context.Producto
            .FromSqlRaw("EXEC sp_buscar_producto_por_nombre @p0", nombreBusqueda)
            .ToList();

        ViewBag.ProductoForm = new Producto();
        ViewBag.ResultadoBusqueda = $"Se encontraron {productos.Count} producto(s) con el nombre '{nombreBusqueda}'";

        return View("Index", productos);
    }
}
