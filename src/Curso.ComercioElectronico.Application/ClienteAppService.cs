using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository repository;

    public ClienteAppService(IClienteRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {
        var existeNombreCliente = await repository.ExisteNombreCliente(clienteDto.Nombres);
        if (existeNombreCliente)
        {

            var msg = $"Ya existe un cliente con el nombre {clienteDto.Nombres}";


            throw new ArgumentException(msg);
        }

        //Mapeo Dto => Entidad
        var cliente = new Cliente();
        cliente.Id = Guid.NewGuid();
        cliente.Nombres = clienteDto.Nombres;
        cliente.Cedula = clienteDto.Cedula;
        cliente.Direccion = clienteDto.Direccion;
        cliente.Telefono = clienteDto.Telefono;
        cliente.Edad = clienteDto.Edad;
        cliente.MayorEdad = clienteDto.MayorEdad;
        cliente.Observaciones = clienteDto.Observaciones;

        //Persistencia objeto
        cliente = await repository.AddAsync(cliente);
        // await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var clienteCreado = new ClienteDto();
        clienteCreado.Id = cliente.Id;
        clienteCreado.Nombres = cliente.Nombres;
        clienteCreado.Cedula = cliente.Cedula;
        clienteCreado.Direccion = cliente.Direccion;
        clienteCreado.Telefono = cliente.Telefono;
        clienteCreado.Edad = cliente.Edad;
        clienteCreado.MayorEdad = cliente.MayorEdad;
        clienteCreado.Observaciones = cliente.Observaciones;

        return clienteCreado;
    }

    public async Task<bool> DeleteAsync(Guid clienteId)
    {
        var cliente = await repository.GetByIdAsync(clienteId);
        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {clienteId}, no existe");
        }

        repository.Delete(cliente);
        // await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<ClienteDto> GetAll(string buscar, int limit = 10, int offset = 0)
    {
        throw new NotImplementedException();
    }

    public ICollection<ClienteDto> GetAll()
    {
        var clienteList = repository.GetAll();

        var clienteListDto = from m in clienteList
                             select new ClienteDto()
                             {
                                 Id = m.Id,
                                 Nombres = m.Nombres
                             };

        return clienteListDto.ToList();
    }


    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto clienteDto)
    {
        var cliente = await repository.GetByIdAsync(id);
        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {id}, no existe");
        }

        // var cliente = new Cliente();
        cliente.Id = Guid.NewGuid();
        cliente.Nombres = clienteDto.Nombres;
        cliente.Cedula = clienteDto.Cedula;
        cliente.Direccion = clienteDto.Direccion;
        cliente.Telefono = clienteDto.Telefono;
        cliente.Edad = clienteDto.Edad;
        cliente.MayorEdad = clienteDto.MayorEdad;
        cliente.Observaciones = clienteDto.Observaciones;

        await repository.UpdateAsync(cliente);

        return;
    }
}
