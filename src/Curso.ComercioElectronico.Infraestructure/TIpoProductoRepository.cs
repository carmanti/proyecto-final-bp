using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using Curso.ComercioElectronico.Infraestructure;

public class TipoProductoRepository : EfRepository<TipoProducto, int>, ITipoProdcutoRepository
{
    public TipoProductoRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombreTipoProducto(string nombre)
    {
        var resultado = await this._context.Set<TipoProducto>()
                       .AnyAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombreTipoProducto(string nombre, int idExcluir)
    {
        var query = this._context.Set<TipoProducto>()
                               .Where(x => x.Id != idExcluir)
                               .Where(x => x.Nombre.ToUpper() == nombre.ToUpper())
                               ;

        var resultado = await query.AnyAsync();
        return resultado;
    }
}