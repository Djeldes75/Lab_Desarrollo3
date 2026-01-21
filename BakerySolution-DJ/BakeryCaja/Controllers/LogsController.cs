using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using System.Linq;

namespace BakeryCaja.Controllers
{
    public class LogsController : Controller
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Access");
            }

            var logs = _context.AuditLogs.OrderByDescending(l => l.Fecha).ToList();
            return View(logs);
        }
    }
}