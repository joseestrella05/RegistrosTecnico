﻿using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Models;

namespace RegistrosTecnico.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options){}

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Trabajos> Trabajos { get; set; }
	public DbSet<Prioridades> Prioridades { get; set; }
}
