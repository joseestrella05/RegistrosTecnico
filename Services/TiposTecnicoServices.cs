using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class TiposTecnicoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos.AnyAsync(t => t.TipoId == id);
    }

    public async Task<bool> Insertar(TiposTecnicos tecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TiposTecnicos.Add(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TiposTecnicos.Update(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnicos tecnicos)
    {
        if (!await Existe(tecnicos.TipoId))
            return await Insertar(tecnicos);
        else
            return await Modificar(tecnicos);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Tipo = await contexto.TiposTecnicos.
            Where(t => t.TipoId == id).
            ExecuteDeleteAsync();
        return Tipo > 0;
    }

    public async Task<TiposTecnicos> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == id);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> cristerio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return contexto.TiposTecnicos
            .AsNoTracking()
            .Where(cristerio).ToList();
    }

    public async Task<List<TiposTecnicos>> ListarTipoTecnicos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking().Select(t => t.TipoTecnico)
            .ToListAsync();
    }

    public async Task<bool> ExisteTipoTecnicoDescripcion(string descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AnyAsync(t => t.Descripcion == descripcion);
    }


}

