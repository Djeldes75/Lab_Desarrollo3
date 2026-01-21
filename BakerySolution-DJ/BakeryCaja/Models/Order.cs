using System;
using System.ComponentModel.DataAnnotations;

namespace BakeryCaja.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; } = "Consumidor Final";
        public Shift Shift { get; set; }
    }
}