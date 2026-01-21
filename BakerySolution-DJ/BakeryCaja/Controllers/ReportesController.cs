using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeryCaja.Controllers
{
    public class ReportesController : Controller
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        // VER HISTORIAL DE TURNOS (Z)
        public IActionResult Index()
        {
            // Seguridad: Solo Admin
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Access");
            }

            // Traemos los turnos, incluyendo los datos del Usuario (Cajero)
            // Ordenamos del más reciente al más antiguo
            var turnos = _context.Shifts
                .Include(s => s.User)
                .OrderByDescending(s => s.StartTime)
                .ToList();

            return View(turnos);
        }
    }
}