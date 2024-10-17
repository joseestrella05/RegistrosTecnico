using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RegistrosTecnico.Models;

namespace RegistrosTecnico.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options){}

    public virtual DbSet<Tecnicos> Tecnicos { get; set; }
    public virtual DbSet<TiposTecnicos> TiposTecnicos { get; set; }
    public virtual DbSet<Clientes> Clientes { get; set; }
    public virtual DbSet<Trabajos> Trabajos { get; set; }
	public virtual DbSet<Prioridades> Prioridades { get; set; }

    public virtual DbSet<TrabajoDestalle> TrabajoDestalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulos>().HasData(
            new List<Articulos>()
            {
                new()
                {
                    ArticuloId = 1,
                    Depcripcion = "Tornillo",
                    Costo = 100,
                    Precio = 150,
                    Existencia = 2900,
                },
                new()
                {
                     ArticuloId = 2,
                    Depcripcion = "Cable de red",
                    Costo = 120,
                    Precio = 200,
                    Existencia = 4999,
                }
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
