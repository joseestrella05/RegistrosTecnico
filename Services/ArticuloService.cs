using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Models;
using RegistrosTecnico.DAL;
using System.Linq.Expressions;
using System.Linq;

namespace RegistrosTecnico.Services;

public class ArticuloService (IDbContextFactory<Contexto> DbFactory)
{


    public async Task<bool> ExisteId(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos.AnyAsync(a => a.ArticuloId == id);
    }
    private async Task<bool> Insertar(Articulos articulo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        await contexto.Articulos.AddAsync(articulo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Articulos articulos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(articulos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Articulos articulos)
    {
        if (!await ExisteId(articulos.ArticuloId))
            return await Insertar(articulos);

        return await Modificar(articulos);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos.
            Where(a => a.ArticuloId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<Articulos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .FirstOrDefaultAsync(A => A.ArticuloId == id);
    }

    public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}




