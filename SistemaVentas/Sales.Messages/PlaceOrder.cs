using NServiceBus;
using System;

namespace Sales.Messages
{
    public class PlaceOrder : ICommand
    {
        public Guid OrderId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }

        public string TipoDocumento { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
    }
}