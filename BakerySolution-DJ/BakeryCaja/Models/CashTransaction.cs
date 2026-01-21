using System;

namespace BakeryCaja.Models
{
    public class CashTransaction
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }
        public virtual Shift? Shift { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string Type { get; set; } = "Entrada";
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}