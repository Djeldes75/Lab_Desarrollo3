using System;

namespace BakeryCaja.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Detalles { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}