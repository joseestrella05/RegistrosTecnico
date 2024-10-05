﻿using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;
namespace RegistrosTecnico.Services;

public class PrioridadService
{
	private readonly Contexto _context;
	public PrioridadService(Contexto contexto) => _context = contexto;


	public async Task<bool> Guardar(Prioridades prioridad)
	{

		if (!await Existe(prioridad.PrioridadId))
			return await Insertar(prioridad);
		else
			return await Modificar(prioridad);
	}

	public async Task<bool> Insertar(Prioridades prioridad)
	{
		_context.Prioridades.Add(prioridad);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Modificar(Prioridades prioridad)
	{
		_context.Update(prioridad);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> Existe(int prioridaId)
	{
		return await _context.Prioridades
			.AnyAsync(p => p.PrioridadId == prioridaId);

	}

	public async Task<bool> Existe(string? descripcion, int? prioridadId = null)
	{
		return await _context.Prioridades
			.AnyAsync(p => p.Descripcion.Equals(descripcion));
	}


	public async Task<bool> Existe(int prioridadId, string? descripcion)
	{

		return await _context.Prioridades
			.AnyAsync(p => p.PrioridadId != prioridadId && p.Descripcion.Equals(descripcion));
	}

	public async Task<bool> Eliminar(int id)
	{
		var prioridades = await _context.Prioridades
			.Where(p => p.PrioridadId == id)
			.ExecuteDeleteAsync();
		return prioridades > 0;
	}

	public async Task<Prioridades?> Buscar(int id)
	{
		return await _context.Prioridades
			.AsNoTracking()
			.FirstOrDefaultAsync(P => P.PrioridadId == id);
	}

	public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
	{
		return await _context.Prioridades
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}
}
