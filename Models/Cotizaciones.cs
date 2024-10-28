using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models;

public class Cotizaciones
{
    [Key]
    public int CotizacionId { get; set; }
    
    [Required]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required]
    public int ClienteID { get; set; }

    [Required]
    public string? Oservacion { get; set;}

    [Required]
    public double Monto { get; set; }

    [InverseProperty("Cotizaciones")]
    public virtual ICollection<CotizacionDetalles> ContizacionDetalles { get; set; } = new List<CotizacionDetalles>();

    [ForeignKey("Clientes")]
    [InverseProperty("Cotizaciones")]
    public virtual Clientes Cliente { get; set; } = null!;

}
