using Curso.ComercioElectronico.Domain;

public interface ITipoProdcutoRepository : IRepository<TipoProducto, int>
{
    Task<bool> ExisteNombreTipoProducto(string nombre);

    Task<bool> ExisteNombreTipoProducto(string nombre, int idExcluir);
}