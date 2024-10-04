using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnico.Models;

public class Clientes
{
    [Key]
    public int ClientesId { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Favor de introducir el nombre del Cliente.")]
    public string? Nombres { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
    public string WhatsApp { get; set; }



}
