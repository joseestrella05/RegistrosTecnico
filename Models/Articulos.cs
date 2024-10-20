using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(100)]
        [RegularExpression("^[a-zA-ZÀ-ÿ\\s]+$", ErrorMessage = "Solo se permiten letras.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public double? Costo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public double? Precio { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int? Existencia { get; set; }

    }

}
