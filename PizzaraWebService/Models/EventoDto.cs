using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaraWebService.Models
{
public class EventoDto
    {
        public string IdJuego { get; set; }
        public int Carrera { get; set; }
        public int Inning { get; set; }
        public string Abre { get; set; }
        public string Cierra { get; set; }
        public string Pelotero { get; set; }
        public DateTime? FechaEvento { get; set; }
    }

}
