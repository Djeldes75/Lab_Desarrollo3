using System.ComponentModel.DataAnnotations;

namespace BakeryCaja.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty; // Evita errores de nulos

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Cajero"; // Puede ser Admin o Cajero o Panadero

        public string Branch { get; set; } = "Principal"; // La sucursal
    }
}