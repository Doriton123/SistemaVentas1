using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVentas.Models;

namespace TiendaVentas.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Venta
                .Include(v => v.Producto)
                .ThenInclude(p => p.Categoria)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();
            return View(ventas);
        }

        // GET: Ultima Venta por Categoria
        public async Task<IActionResult> UltimaVentaCategoria()
        {
            var ultimaVenta = await _context.Venta
                .Include(v => v.Producto)
                .ThenInclude(p => p.Categoria)
                .OrderByDescending(v => v.Fecha)
                .FirstOrDefaultAsync();
            return View(ultimaVenta);
        }

        // GET: Filtro de Productos Vendidos por Categoría (2019)
        public async Task<IActionResult> VentasPorCategoria(int? categoriaId = null)
        {
            // Obtener las categorías que tuvieron ventas en 2019 para el dropdown
            var categorias2019 = await _context.Venta
                .Where(v => v.Fecha.Year == 2019)
                .Include(v => v.Producto)
                .ThenInclude(p => p.Categoria)
                .Select(v => v.Producto.Categoria)
                .Distinct()
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            // Crear lista simple para el ViewBag
            ViewBag.Categorias = categorias2019.Select(c => new SelectListItem
            {
                Value = c.CodigoCategoria.ToString(),
                Text = c.Nombre,
                Selected = categoriaId.HasValue && c.CodigoCategoria == categoriaId.Value
            }).ToList();

            // Lista vacía de productos por defecto
            List<Producto> productosVendidos = new List<Producto>();

            // Solo buscar productos si se seleccionó una categoría
            if (categoriaId.HasValue)
            {
                productosVendidos = await _context.Venta
                    .Where(v => v.Fecha.Year == 2019 && v.Producto.CodigoCategoria == categoriaId.Value)
                    .Include(v => v.Producto)
                    .ThenInclude(p => p.Categoria)
                    .Select(v => v.Producto)
                    .Distinct()
                    .OrderBy(p => p.Nombre)
                    .ToListAsync();

                // Pasar la categoría seleccionada a la vista
                ViewBag.CategoriaSeleccionada = categorias2019
                    .FirstOrDefault(c => c.CodigoCategoria == categoriaId.Value)?.Nombre;
            }

            return View(productosVendidos);
        }

        // GET: Productos
        public async Task<IActionResult> Productos()
        {
            var productos = await _context.Producto
                .Include(p => p.Categoria)
                .OrderBy(p => p.Categoria.Nombre)
                .ThenBy(p => p.Nombre)
                .ToListAsync();
            return View(productos);
        }

        // GET: Categorias
        public async Task<IActionResult> Categorias()
        {
            var categorias = await _context.Categoria
                .Include(c => c.Producto)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
            return View(categorias);
        }
    }
}