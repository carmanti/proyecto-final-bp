namespace Curso.ComercioElectronico.Application;


public interface IOrdenAppService
{

    ICollection<OrdenDto> GetAll();

    Task<OrdenDto> CreateAsync(OrdenCrearActualizarDto orden);

    Task UpdateAsync(int id, OrdenCrearActualizarDto orden);

    Task<bool> DeleteAsync(int ordenId);
}