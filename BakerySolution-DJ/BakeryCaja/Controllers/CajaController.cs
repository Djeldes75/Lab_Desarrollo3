using Microsoft.AspNetCore.Mvc;
using BakeryCaja.Data;
using BakeryCaja.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BakeryCaja.Controllers
{
    public class CajaController : Controller
    {
        private readonly AppDbContext _context;

        public CajaController(AppDbContext context)
        {
            _context = context;
        }

        // --- PANTALLA PRINCIPAL ---
        public IActionResult Index()
        {
            var turnoActual = _context.Shifts
                .Include(s => s.Orders)
                .FirstOrDefault(s => !s.IsClosed);

            if (turnoActual == null) return View("AbrirCaja");

            var productosEnCero = _context.Products.Where(p => p.Stock == 0).ToList();
            if (productosEnCero.Any())
            {
                foreach (var p in productosEnCero) p.Stock = 50;
                _context.SaveChanges();
            }

            var movimientos = _context.CashTransactions.Where(t => t.ShiftId == turnoActual.Id).ToList();

            decimal entradas = movimientos.Where(t => t.Type == "Entrada").Sum(t => t.Amount);
            decimal salidas = movimientos.Where(t => t.Type == "Salida").Sum(t => t.Amount);

            ViewBag.TurnoId = turnoActual.Id;
            ViewBag.MontoInicial = turnoActual.InitialCash;
            ViewBag.Ventas = turnoActual.TotalSales;
            ViewBag.Entradas = entradas;
            ViewBag.Salidas = salidas;
            ViewBag.TotalCaja = (turnoActual.InitialCash + turnoActual.TotalSales + entradas) - salidas;

            var productos = _context.Products.Where(p => p.IsActive).ToList();
            return View("Terminal", productos);
        }

        //ABRIR CAJA
        [HttpPost]
        public IActionResult AbrirTurno(decimal montoInicial)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var nuevoTurno = new Shift
            {
                UserId = userId,
                InitialCash = montoInicial,
                StartTime = DateTime.Now,
                IsClosed = false
            };
            _context.Shifts.Add(nuevoTurno);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //COBRAR
        [HttpPost]
        public IActionResult ProcesarVenta(string jsonDetalle, decimal totalVenta, string metodoPago, string cliente)
        {
            var turno = _context.Shifts.FirstOrDefault(s => !s.IsClosed);
            if (turno == null) return RedirectToAction("Index");

            var items = JsonSerializer.Deserialize<List<CarritoItem>>(jsonDetalle);
            string desc = "";
            if (items != null) foreach (var item in items)
                {
                    desc += $"{item.cantidad}x {item.nombre}, ";
                    var p = _context.Products.Find(item.id);
                    if (p != null) p.Stock = Math.Max(0, p.Stock - item.cantidad);
                }

            if (string.IsNullOrEmpty(cliente)) cliente = "Consumidor Final";

            _context.Orders.Add(new Order
            {
                ShiftId = turno.Id,
                Date = DateTime.Now,
                TotalAmount = totalVenta,
                PaymentMethod = metodoPago,
                Description = desc,
                CustomerName = cliente
            });

            turno.TotalSales += totalVenta;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RegistrarMovimiento(string tipo, decimal monto, string motivo)
        {
            var turno = _context.Shifts.FirstOrDefault(s => !s.IsClosed);
            if (turno != null)
            {
                var movimiento = new CashTransaction
                {
                    ShiftId = turno.Id,
                    Date = DateTime.Now,
                    Type = tipo,
                    Amount = monto,
                    Reason = motivo
                };
                _context.CashTransactions.Add(movimiento);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CerrarTurno()
        {
            var turno = _context.Shifts.FirstOrDefault(s => !s.IsClosed);
            if (turno != null)
            {
                var movimientos = _context.CashTransactions.Where(t => t.ShiftId == turno.Id).ToList();
                decimal entradas = movimientos.Where(t => t.Type == "Entrada").Sum(t => t.Amount);
                decimal salidas = movimientos.Where(t => t.Type == "Salida").Sum(t => t.Amount);

                turno.IsClosed = true;
                turno.EndTime = DateTime.Now;
                turno.ReportedTotal = (turno.InitialCash + turno.TotalSales + entradas) - salidas;

                _context.SaveChanges();
            }
            return RedirectToAction("Logout", "Access");
        }

        public class CarritoItem
        {
            public int id { get; set; }
            public string nombre { get; set; } = string.Empty;
            public decimal precio { get; set; }
            public int cantidad { get; set; }
        }
    }
}