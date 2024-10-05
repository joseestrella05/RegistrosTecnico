using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Models;
using System.Linq.Expressions;

namespace RegistrosTecnico.Services;

public class ClientesServices
{
    readonly Contexto _context;

    public ClientesServices(Contexto context) => _context = context;

    public async Task<bool> Existe(int clientesId)
    {
        return await _context.Clientes.AnyAsync(c => c.ClientesId == clientesId);
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
        _context.Clientes.Add(clientes);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Clientes clientes)
    {
        _context.Clientes.Update(clientes);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var Clientes = await _context.Clientes.Where(c => c.ClientesId == id).ExecuteDeleteAsync();
        return Clientes > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        return await _context.Clientes.
            AsNoTracking().
            FirstOrDefaultAsync(c => c.ClientesId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(criterio).ToListAsync();
    }


    public async Task<Clientes?> BuscarNombres(string nombre)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombres == nombre);
    }


}
