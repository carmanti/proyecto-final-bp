namespace Curso.ComercioElectronico.Domain;

public interface IClienteRepository : IRepository<Cliente, Guid>
{


    Task<bool> ExisteNombreCliente(string nombre);

    Task<bool> ExisteNombreCliente(string nombre, Guid idExcluir);


}