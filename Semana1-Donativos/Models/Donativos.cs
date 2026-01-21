using System;

namespace Semana1_Donativos.Models
{
    public class Donativo
    {
        public int ID { get; set; }
        public string Operativo { get; set; } = "";
        public string Pais { get; set; } = "";
        public int Lote { get; set; }
        public string Descripcion { get; set; } = "";
        public int Cantidad { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Estado { get; set; } = "";
    }
}
