using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;
namespace RegistrosTecnico.Services;

public class PrioridadService(IDbContextFactory<Contexto> DbFactory)
{
	public async Task<bool> Guardar(Prioridades prioridad)
	{

		if (!await Existe(prioridad.PrioridadId))
			return await Insertar(prioridad);
		else
			return await Modificar(prioridad);
	}

	public async Task<bool> Insertar(Prioridades prioridad)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prioridades.Add(prioridad);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Modificar(Prioridades prioridad)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(prioridad);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Existe(int prioridaId)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
			.AnyAsync(p => p.PrioridadId == prioridaId);

	}

	public async Task<bool> Existe(string? descripcion, int? prioridadId = null)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
			.AnyAsync(p => p.Descripcion.Equals(descripcion));
	}


	public async Task<bool> Existe(int prioridadId, string? descripcion)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
			.AnyAsync(p => p.PrioridadId != prioridadId && p.Descripcion.Equals(descripcion));
	}

	public async Task<bool> Eliminar(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var prioridades = await contexto.Prioridades
			.Where(p => p.PrioridadId == id)
			.ExecuteDeleteAsync();
		return prioridades > 0;
	}

	public async Task<Prioridades?> Buscar(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
			.AsNoTracking()
			.FirstOrDefaultAsync(P => P.PrioridadId == id);
	}

	public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}
}
