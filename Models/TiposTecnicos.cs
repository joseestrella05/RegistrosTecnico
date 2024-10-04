using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnico.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten numero")]
    [Required(ErrorMessage = "La descripcion es obligatoria")]
    public string Descripcion { get; set; }
}
