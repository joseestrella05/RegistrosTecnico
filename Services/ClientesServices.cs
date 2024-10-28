using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class ClientesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int clientesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClientesId == clientesId);
    }

    public async Task<bool> Guardar(Clientes clientes)
    {
        if (!await Existe(clientes.ClientesId))
            return await Insertar(clientes);
        else
            return await Modificar(clientes);
    }

    public async Task<bool> Insertar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(clientes);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Update(clientes);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Clientes = await contexto.Clientes.Where(c => c.ClientesId == id).ExecuteDeleteAsync();
        return Clientes > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.
            AsNoTracking().
            FirstOrDefaultAsync(c => c.ClientesId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .Where(criterio).ToListAsync();
    }


    public async Task<Clientes?> BuscarNombres(string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombres == nombre);
    }


}
