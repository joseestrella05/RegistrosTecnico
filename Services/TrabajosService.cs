using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Components.Pages.TrabajoPage;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class TrabajoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> ExisteDescripcion(String descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AnyAsync(T => T.Descripcion == descripcion);
    }

    public async Task<bool> ExisteId(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AnyAsync(t => t.TrabajoId == id);
    }

    private async Task<bool> Insertar(Trabajos trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        await AfectarCobro(trabajo.TrabajosDetalles.ToArray());
        await contexto.Trabajos.AddAsync(trabajo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajos trabajos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajoOriginal = await contexto.Trabajos
        .Include(t => t.TrabajosDetalles)
        .AsNoTracking()  
        .FirstOrDefaultAsync(t => t.TrabajoId == trabajos.TrabajoId);

        await AfectarCobro(trabajoOriginal.TrabajosDetalles.ToArray(), false);

        await AfectarCobro(trabajos.TrabajosDetalles.ToArray());

        contexto.Update(trabajos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajos)
    {
        if (!await ExisteId(trabajos.TrabajoId))
            return await Insertar(trabajos);

        return await Modificar(trabajos);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajo = contexto.Trabajos.Find(id);

        await AfectarCobro(trabajo.TrabajosDetalles.ToArray(), false);

        contexto.TrabajosDetalles.RemoveRange(trabajo.TrabajosDetalles);
        contexto.Trabajos.Remove(trabajo);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(T => T.TrabajosDetalles)
            .FirstOrDefaultAsync(t => t.TrabajoId == id);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(T => T.TrabajosDetalles)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    private async Task AfectarCobro(TrabajosDetalles[] detalles, bool resta = true)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var item in detalles)
        {
            var Articulo = await contexto.Articulos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
            if (resta)
            {
                Articulo.Existencia -= item.Cantidad;
            }
            else
            {
                Articulo.Existencia += item.Cantidad;
            }

        }
    }
}
