using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class OrdenAppService : IOrdenAppService
{
    private readonly IOrdenRepository repository;
    private readonly IUnitOfWork unitOfWork;

    //private readonly IUnitOfWork unitOfWork;

    public OrdenAppService(IOrdenRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        //this.unitOfWork = unitOfWork;
    }

    public async Task<OrdenDto> CreateAsync(OrdenCrearActualizarDto OrdenDto)
    {

        //Reglas Validaciones... 
        var existeNombreOrden = await repository.ExisteNombreOrden(OrdenDto.Codigo);
        if (existeNombreOrden)
        {
            throw new ArgumentException($"Ya existe una Orden con el nombre {OrdenDto.Codigo}");
        }

        //Mapeo Dto => Entidad
        var orden = new Orden();
        orden.Codigo = OrdenDto.Codigo;
        orden.ClienteId = OrdenDto.ClienteId;
        orden.Codigo = OrdenDto.Codigo;
        orden.Estado = OrdenDto.Estado;
        orden.fecha = OrdenDto.fecha;
        orden.FechaAnulacion = OrdenDto.FechaAnulacion;
        orden.Items = OrdenDto.Items;
        orden.Observaciones = OrdenDto.Observaciones;
        orden.Total = OrdenDto.Total;

        //Persistencia objeto
        orden = await repository.AddAsync(orden);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var ordenCreada = new OrdenDto();
        ordenCreada.Codigo = orden.Codigo;
        ordenCreada.Id = orden.Id;
        ordenCreada.ClienteId = orden.ClienteId;
        ordenCreada.Cliente = orden.Cliente;
        ordenCreada.Estado = orden.Estado;
        ordenCreada.fecha = orden.fecha;
        ordenCreada.FechaAnulacion = orden.FechaAnulacion;
        ordenCreada.Items = orden.Items;
        ordenCreada.Observaciones = orden.Observaciones;
        ordenCreada.Total = orden.Total;


        return ordenCreada;
    }

    public async Task UpdateAsync(int id, OrdenCrearActualizarDto OrdenDto)
    {
        var orden = await repository.GetByIdAsync(id);
        if (orden == null)
        {
            throw new ArgumentException($"La Orden con el id: {id}, no existe");
        }

        var existeNombreOrden = await repository.ExisteNombreOrden(OrdenDto.Codigo, id);
        if (existeNombreOrden)
        {
            throw new ArgumentException($"Ya existe una Orden con el nombre {OrdenDto.Codigo}");
        }

        //Mapeo Dto => Entidad
        orden.Codigo = OrdenDto.Codigo;
        orden.ClienteId = OrdenDto.ClienteId;
        orden.Estado = OrdenDto.Estado;
        orden.fecha = OrdenDto.fecha;
        orden.FechaAnulacion = OrdenDto.FechaAnulacion;
        orden.Items = OrdenDto.Items;
        orden.Observaciones = OrdenDto.Observaciones;
        orden.Total = OrdenDto.Total;

        //Persistencia objeto
        await repository.UpdateAsync(orden);
        await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int OrdenId)
    {
        //Reglas Validaciones... 
        var orden = await repository.GetByIdAsync(OrdenId);
        if (orden == null)
        {
            throw new ArgumentException($"La Orden con el id: {OrdenId}, no existe");
        }

        repository.Delete(orden);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<OrdenDto> GetAll()
    {
        var ordenList = repository.GetAll();

        var ordenListDto = from m in ordenList
                           select new OrdenDto()
                           {
                               Id = m.Id,
                               Codigo = m.Codigo,
                               ClienteId = m.ClienteId,
                               Estado = m.Estado,
                               fecha = m.fecha,
                               FechaAnulacion = m.FechaAnulacion,
                               Items = m.Items,
                               Observaciones = m.Observaciones,
                               Total = m.Total,

                           };

        return ordenListDto.ToList();
    }


}
