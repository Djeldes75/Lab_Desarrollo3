using System;
using System.Collections.Generic;

namespace BakeryCaja.Models
{
    public class Shift
    {
        public int Id { get; set; }

        // El cajero que abrió el turno
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        // Horarios
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }

        // Dinero y Cuadre
        public decimal InitialCash { get; set; } // Fondo inicial
        public decimal TotalSales { get; set; } // Lo que vendió el sistema
        public decimal? ReportedTotal { get; set; } // Lo que contó el cajero al cerrar

        public bool IsClosed { get; set; } = false;

        // Listas de movimientos (Relaciones)
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
    }
}