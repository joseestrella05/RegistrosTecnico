using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Components.Pages.TrabajoPage;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class TrabajoService(Contexto contexto)
{
    private readonly Contexto _contexto = contexto;
    public async Task<bool> ExisteDescripcion(String descripcion)
    {
        return await _contexto.Trabajos.AnyAsync(T => T.Descripcion == descripcion);
    }

    public async Task<bool> ExisteId(int id)
    {
        return await _contexto.Trabajos.AnyAsync(t => t.TrabajoId == id);
    }

    private async Task<bool> Insertar(Trabajos trabajo)
    {
        await AfectarCobro(trabajo.TrabajosDetalles.ToArray());
        await _contexto.Trabajos.AddAsync(trabajo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajos trabajos)
    {
        var trabajoOriginal = await _contexto.Trabajos
        .Include(t => t.TrabajosDetalles)
        .AsNoTracking() // 
        .FirstOrDefaultAsync(t => t.TrabajoId == trabajos.TrabajoId);

        await AfectarCobro(trabajoOriginal.TrabajosDetalles.ToArray(), false);

        await AfectarCobro(trabajos.TrabajosDetalles.ToArray());

        _contexto.Update(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajos)
    {
        if (!await ExisteId(trabajos.TrabajoId))
            return await Insertar(trabajos);

        return await Modificar(trabajos);
    }

    public async Task<bool> Eliminar(int id)
    {
        var trabajo = _contexto.Trabajos.Find(id);

        await AfectarCobro(trabajo.TrabajosDetalles.ToArray(), false);

        _contexto.TrabajosDetalles.RemoveRange(trabajo.TrabajosDetalles);
        _contexto.Trabajos.Remove(trabajo);

        var cantidad = await _contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(T => T.TrabajosDetalles)
            .FirstOrDefaultAsync(t => t.TrabajoId == id);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
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

        foreach (var item in detalles)
        {
            var Articulo = await _contexto.Articulos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
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
