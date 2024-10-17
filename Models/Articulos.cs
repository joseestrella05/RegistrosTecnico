using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosTecnico.Models
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }
        public string Depcripcion { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }

        [InverseProperty("Articulos")]
        public virtual ICollection<Trabajos> Trabajos { get; set; } = new List<Trabajos>();

    }

}
