using Curso.ComercioElectronico.Domain;

public interface IProductoRepository : IRepository<Producto, int>
{
    Task<bool> ExisteNombre(string nombre);
    Task<bool> ExisteNombre(string nombre, int idExcluir);
    // IQueryable<Producto> GetByText(int limit = 10, int offset = 0, string campo = "", string parametro = "");
}