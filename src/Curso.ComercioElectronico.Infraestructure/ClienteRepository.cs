using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ClienteRepository : EfRepository<Cliente, Guid>, IClienteRepository
{
    public ClienteRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombreCliente(string nombres)
    {

        var resultado = await this._context.Set<Cliente>()
                       .AnyAsync(x => x.Nombres.ToUpper() == nombres.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombreCliente(string nombres, Guid idExcluir)
    {

        var query = this._context.Set<Cliente>()
                       .Where(x => x.Id != idExcluir)
                       .Where(x => x.Nombres.ToUpper() == nombres.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }




}