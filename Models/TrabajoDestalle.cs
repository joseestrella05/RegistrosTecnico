using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models;

public class TrabajosDetalles
{
    [Key]
    public int DetalleId { get; set; }

    [Required(ErrorMessage = "campo obligarior")]
    public int TrabajoId { get; set; }

    [Required(ErrorMessage = "campo obligarior")]
    public int ArticuloId { get; set; }

    [Required(ErrorMessage = "campo obligarior")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "campo obligarior")]
    public double? Costo { get; set; }

    [Required(ErrorMessage = "campo obligarior")]
    public double? Precio { get; set; }

}
