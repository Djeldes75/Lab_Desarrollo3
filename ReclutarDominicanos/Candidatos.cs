using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReclutarDominicanos
{
    public class Candidato
    {
        public int Id { get; set; }
        public int TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNac { get; set; }
        public DateTime FechaIng { get; set; }
        public decimal Peso { get; set; }
        public decimal Estatura { get; set; }
        public int CantHijos { get; set; }
        public string CondFisica { get; set; }
        public int Estado { get; set; }
        public string FormAcad { get; set; }
    }

}
