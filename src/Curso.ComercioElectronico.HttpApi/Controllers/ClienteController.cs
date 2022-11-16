using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{

    private readonly IClienteAppService clienteAppService;

    public ClienteController(IClienteAppService clienteAppService)
    {
        this.clienteAppService = clienteAppService;
    }

    [HttpGet]
    public ICollection<ClienteDto> GetAll()
    {

        return clienteAppService.GetAll();
    }

    [HttpPost]
    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteCrearActualizarDto)
    {

        return await clienteAppService.CreateAsync(clienteCrearActualizarDto);

    }


    [HttpPut]
    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto cliente)
    {

        await clienteAppService.UpdateAsync(id, cliente);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid clienteId)
    {

        return await clienteAppService.DeleteAsync(clienteId);

    }

}