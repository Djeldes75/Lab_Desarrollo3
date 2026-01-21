using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using BakeryCaja.Models;

namespace BakeryCaja.Controllers
{
    public class CocinaController : Controller
    {
        private readonly AppDbContext _context;

        public CocinaController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Mostrar la lista de productos y su stock actual
        public IActionResult Index()
        {
            // Validar sesión (Seguridad básica por ahora)
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Access");
            }

            var productos = _context.Products.ToList();
            return View(productos);
        }

        // 2. Acción de HORNEAR (Sumar Stock)
        [HttpPost]
        public IActionResult Hornear(int id, int cantidad)
        {
            var producto = _context.Products.Find(id);
            if (producto != null && cantidad > 0)
            {
                producto.Stock += cantidad; // Sumamos la cantidad horneada
                _context.SaveChanges();
            }
            // Recargamos la página para ver el cambio
            return RedirectToAction("Index");
        }
    }
}
