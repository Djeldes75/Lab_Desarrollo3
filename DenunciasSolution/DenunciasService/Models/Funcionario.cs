using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DenunciasService.Models
{
    /// <summary>
    /// Entidad que representa el reporte de irregularidad.
    /// </summary>
    [Table("tblFuncionariosCurruptos")]
    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido.")]
        [StringLength(20)]
        public string tipodocumento { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]

        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "El documento solo puede contener números y guiones.")]
        public string documento { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string apellidos { get; set; } = string.Empty;

        [StringLength(100)]
        public string? posicion { get; set; }

        [StringLength(100)]
        public string? empresa { get; set; }

        [StringLength(2000)]
        public string? descripcion { get; set; }

        public DateTime fechaingreso { get; set; } = DateTime.Now;

        public bool estado { get; set; } = true;
    }
}