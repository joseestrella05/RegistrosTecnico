using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class CotizacionServices(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .AnyAsync(c => c.CotizacionId == cotizacionId);
    }

    private async Task<bool> Insertar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Cotizaciones.Add(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Cotizaciones.Update(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Cotizaciones cotizacion)
    {

        if (!await Existe(cotizacion.CotizacionId))

            return await Insertar(cotizacion);
        else

            return await Modificar(cotizacion);
    }

    public async Task<Cotizaciones> Buscar(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones.Include(d => d.Cliente)
            .Include(d => d.ContizacionDetalles)
            .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);
    }

    public async Task<bool> Eliminar(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cotizacion = await contexto.Cotizaciones
            .Include(c => c.ContizacionDetalles)
            .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);

        if (cotizacion == null) return false;

        contexto.CotizacionDetalles.RemoveRange(cotizacion.ContizacionDetalles);
        contexto.Cotizaciones.Remove(cotizacion);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }
    public async Task<Cotizaciones> BuscarConDetalles(int Id)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.ContizacionDetalles)
            .ThenInclude(td => td.ArticuloId)
            .FirstOrDefaultAsync(t => t.CotizacionId == Id);
    }

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.Cliente)
            .Include(d => d.ContizacionDetalles)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<Cotizaciones?> RemoverConId(int id)
    {

        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.ContizacionDetalles)
            .FirstOrDefaultAsync(c => c.CotizacionId == id);
    }



}
