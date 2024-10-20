using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnico.Models;

public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }

    [StringLength(250)]
    [Required(ErrorMessage = "Campo obligatario")]
    [RegularExpression("^[a-zA-ZÀ-ÿ\\s]+$", ErrorMessage = "Solo se permiten letras.")]
    public string? Descripcion { get; set; }

    public DateTime? Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El sueldo por hora debe ser mayor a 0.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
    public double Monto { get; set; }

    [ForeignKey("Tecnicos")]
    [Required(ErrorMessage = "Debe de elegir un tecnico")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Debe de elegir un tecnico")]
    public int TecnicoId { get; set; }
    public Tecnicos? Tecnicos { get; set; }
    [ForeignKey("TrabajoId")]
    public ICollection<TrabajosDetalles> TrabajosDetalles { get; set; } = [];

    [Required(ErrorMessage = "Debe de elegir un cliente")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Debe de elegir un cliente")]
    public int? ClienteId { get; set; }
    public Clientes? Cliente { get; set; }

    [Required(ErrorMessage = "Debe de elegir un cliente")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Debe de elegir un prioridad")]
    [ForeignKey("Prioridad")]
    public int? PrioridadId { get; set; }
    public Prioridades? Prioridad { get; set; }



}
