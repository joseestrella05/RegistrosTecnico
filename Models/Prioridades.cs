using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnico.Models;

public class Prioridades
{
	[Key]
	public int PrioridadId { get; set; }
	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numero")]
	[Required(ErrorMessage = "El Campo Descripción es obligatorio")]
	public string? Descripcion { get; set; }

	public int Tiempo { get; set; }
}
