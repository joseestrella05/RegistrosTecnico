using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models;

public class TrabajoDestalle
{
    [Key] 
    public int DetalleId { get; set; }
    public int TrabajoId { get; set; }
    public int ArticuloId { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public double Costo { get; set; }

    [ForeignKey("TrabajoId")]
    [InverseProperty("TrabajoDetalle")]
    public virtual Trabajos Trabajos { get; set; } = null!;
}
