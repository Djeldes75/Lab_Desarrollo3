using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using BakeryCaja.Models;
using Microsoft.AspNetCore.Http;

namespace BakeryCaja.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;

        public AccessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mostrar pantalla de Login
        public IActionResult Login()
        {
            // VALIDACIÓN DE SEGURIDAD:
            // Ya no creamos usuarios "hardcodeados" aquí.
            // Si la base de datos está vacía, deberás insertar el primer usuario manualmente
            // o usar un "Seeder" en la configuración inicial, pero nunca aquí en el Login.

            // Si ya hay sesión iniciada, redirigir según su rol
            var role = HttpContext.Session.GetString("UserRole");
            if (role != null)
            {
                return RedirigirPorRol(role);
            }

            return View();
        }

        // POST: Procesar el Login (BLINDADO)
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // 1. Buscamos primero en la BD por nombre (SQL suele ser insensible a mayúsculas)
            var candidatos = _context.Users
                .Where(u => u.Username == username)
                .ToList(); // Traemos los posibles candidatos a memoria

            // 2. Verificamos ESTRICTAMENTE en memoria (C# sí distingue mayúsculas)
            // StringComparison.Ordinal asegura que 'Isaac' sea diferente de 'isaac'
            var user = candidatos.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.Ordinal) &&
                u.Password.Equals(password, StringComparison.Ordinal));

            if (user != null)
            {
                // ¡Credenciales exactas confirmadas!
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("UserRole", user.Role);

                return RedirigirPorRol(user.Role);
            }

            // Si falla (por contraseña o por mayúsculas/minúsculas)
            ViewBag.Error = "ERROR_LOGIN";
            return View();
        }

        // Semáforo de Redirección
        private IActionResult RedirigirPorRol(string role)
        {
            if (role == "Admin") return RedirectToAction("Index", "Admin");
            if (role == "Panadero") return RedirectToAction("Index", "Cocina");
            return RedirectToAction("Index", "Caja");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
