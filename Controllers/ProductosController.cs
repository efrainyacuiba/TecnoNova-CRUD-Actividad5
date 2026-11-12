using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;
using TecnoNovaCRUD.Data;
using TecnoNovaCRUD.Models;

namespace TecnoNovaCRUD.Controllers;

public class ProductosController : Controller
{
    private readonly ProductoDAO _dao;

    public ProductosController(ProductoDAO dao)
    {
        _dao = dao;
    }

    // READ: muestra los productos.
    public IActionResult Index()
    {
        return View(_dao.ObtenerTodos());
    }

    // CREATE: muestra formulario.
    [HttpGet]
    public IActionResult Create()
    {
        CargarCategorias();
        return View(new Producto());
    }

    // CREATE: guarda producto.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            CargarCategorias();
            return View(producto);
        }
        try
        {
            _dao.Insertar(producto);
            TempData["Mensaje"] = "Producto agregado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            ModelState.AddModelError(nameof(producto.Sku), "Ese código SKU ya existe.");
            CargarCategorias();
            return View(producto);
        }
    }

    // UPDATE: muestra el producto actual.
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _dao.ObtenerPorId(id);
        if (producto is null) return NotFound();
        CargarCategorias();
        return View(producto);
    }

    // UPDATE: guarda los cambios.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Producto producto)
    {
        if (id != producto.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            CargarCategorias();
            return View(producto);
        }
        try
        {
            if (!_dao.Actualizar(producto)) return NotFound();
            TempData["Mensaje"] = "Producto actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            ModelState.AddModelError(nameof(producto.Sku), "Ese código SKU ya existe.");
            CargarCategorias();
            return View(producto);
        }
    }

    // DELETE: pide confirmación antes de borrar.
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = _dao.ObtenerPorId(id);
        if (producto is null) return NotFound();
        return View(producto);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_dao.Eliminar(id)) return NotFound();
        TempData["Mensaje"] = "Producto eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private void CargarCategorias()
    {
        ViewBag.Categorias = new SelectList(
            _dao.ObtenerCategorias(), "Id", "Nombre");
    }
}
