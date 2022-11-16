namespace Curso.ComercioElectronico.Domain;

public interface IOrdenRepository : IRepository<Orden, int>
{


    Task<bool> ExisteNombreOrden(string nombre);

    Task<bool> ExisteNombreOrden(string nombre, int idExcluir);


}