using Microsoft.EntityFrameworkCore;
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
        await _contexto.Trabajos.AddAsync(trabajo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajos trabajos)
    {
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
        return await _contexto.Trabajos.
            Where(p => p.TrabajoId == id).ExecuteDeleteAsync() > 0;
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridades)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TrabajoId == id);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Cliente)
            .Include(p => p.Prioridades)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
