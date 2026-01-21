using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using BakeryCaja.Models;

namespace BakeryCaja.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context) { _context = context; }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Login", "Access");
            return View(_context.Users.ToList());
        }

        [HttpPost]
        public IActionResult CrearUsuario(string username, string password, string role)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Login", "Access");
            _context.Users.Add(new User { Username = username, Password = password, Role = role, Branch = "Principal" });
            Log("CREAR", $"Usuario creado: {username} ({role})");
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditarUsuario(int id, string username, string password, string role)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Login", "Access");
            var user = _context.Users.Find(id);
            if (user != null)
            {
                Log("EDITAR", $"Usuario editado: {user.Username} -> {username}");
                user.Username = username; user.Password = password; user.Role = role;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarUsuario(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Login", "Access");
            var user = _context.Users.Find(id);
            if (user != null)
            {
                Log("ELIMINAR", $"Usuario eliminado: {user.Username}");
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private void Log(string accion, string detalles)
        {
            _context.AuditLogs.Add(new AuditLog { Usuario = HttpContext.Session.GetString("UserName"), Accion = accion, Detalles = detalles });
        }
    }
}
