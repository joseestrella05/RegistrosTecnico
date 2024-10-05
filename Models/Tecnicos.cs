using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numero")]
    [Required(ErrorMessage = "El Nombres obligatorio")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Favor de introducir el sueldo por hora del técnico.")]
    [Range(0, 200000, ErrorMessage = "Favor de introducir un sueldo mayor que 1 y menor que 200000.")]
    public float SueldoHora { get; set; }

    [ForeignKey("TipoTecnico")]
    [Required(ErrorMessage = "Selecionar un tipo")]
    public int TipoId { get; set; }

    [ForeignKey("TipoId")]
    public TiposTecnicos TipoTecnico { get; set; }
}
