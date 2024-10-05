using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class TiposTecnicoServices
{
    private readonly Contexto _context;
    public TiposTecnicoServices(Contexto context) => _context = context;

    public async Task<bool> Existe(int id)
    {
        return await _context.TiposTecnicos.AnyAsync(t => t.TipoId == id);
    }

    public async Task<bool> Insertar(TiposTecnicos tecnicos)
    {
        _context.TiposTecnicos.Add(tecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tecnicos)
    {
        _context.TiposTecnicos.Update(tecnicos);
        return await _context.SaveChangesAsync() > 0;
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
        var Tipo = await _context.TiposTecnicos.
            Where(t => t.TipoId == id).
            ExecuteDeleteAsync();
        return Tipo > 0;
    }

    public async Task<TiposTecnicos> Buscar(int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == id);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> cristerio)
    {
        return _context.TiposTecnicos.AsNoTracking().Where(cristerio).ToList();
    }

    public async Task<List<TiposTecnicos>> ListarTipoTecnicos()
    {
        return await _context.Tecnicos.AsNoTracking().Select(t => t.TipoTecnico).ToListAsync();
    }

    public async Task<bool> ExisteTipoTecnicoDescripcion(string descripcion)
    {
        return await _context.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
    }


}

