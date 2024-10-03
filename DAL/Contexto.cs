using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Models;

namespace RegistrosTecnico.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options){}

    public DbSet<Tecnicos> Tecnicos { get; set; }
}
