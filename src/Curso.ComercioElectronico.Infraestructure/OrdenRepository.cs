
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class OrdenRepository : EfRepository<Orden, int>, IOrdenRepository
{
    public OrdenRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombreOrden(string nombre)
    {

        var resultado = await this._context.Set<Orden>()
                       .AnyAsync(x => x.Codigo.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombreOrden(string nombre, int idExcluir)
    {

        var query = this._context.Set<Orden>()
                       .Where(x => x.Id != idExcluir)
                       .Where(x => x.Codigo.ToUpper() == nombre.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }

}