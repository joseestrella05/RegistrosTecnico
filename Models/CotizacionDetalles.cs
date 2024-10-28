using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models;

public class CotizacionDetalles
{
    [Key]
    public int DetalleId { get; set; }

    public int CotizacionId { get; set; }

    [ForeignKey("Articulo")]
    public int ArticuloId { get; set; }
    public virtual Articulos? Articulos { get; set; }

    public int Cantidad { get; set; }

    public double? Precio { get; set; }

    [ForeignKey("CotizacionId")]
   
    public virtual Cotizaciones Cotizaciones { get; set; } = null!;

   
}
