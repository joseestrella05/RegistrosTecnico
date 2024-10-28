using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RegistrosTecnico.Models;
using System.Net;

namespace RegistrosTecnico.DAL;

public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
{
    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Trabajos> Trabajos { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet<Articulos> Articulos { get; set; }
    public DbSet<TrabajosDetalles> TrabajosDetalles { get; set; }
    public virtual DbSet<Cotizaciones> Cotizaciones { get; set; }
    public virtual DbSet<CotizacionDetalles> CotizacionDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
        {
            new Articulos() {ArticuloId = 1, Descripcion = "Cable Lan",
                Costo = 100, Precio = 40,Existencia = 20},
            
        });
    }

}
