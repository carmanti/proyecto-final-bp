using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdenController : ControllerBase
{

    private readonly IOrdenAppService ordenAppService;

    public OrdenController(IOrdenAppService ordenAppService)
    {
        this.ordenAppService = ordenAppService;
    }

    [HttpGet]
    public ICollection<OrdenDto> GetAll()
    {

        return ordenAppService.GetAll();
    }

    [HttpPost]
    public async Task<OrdenDto> CreateAsync(OrdenCrearActualizarDto orden)
    {

        return await ordenAppService.CreateAsync(orden);

    }

    [HttpPut]
    public async Task UpdateAsync(int id, OrdenCrearActualizarDto orden)
    {

        await ordenAppService.UpdateAsync(id, orden);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int ordenId)
    {

        return await ordenAppService.DeleteAsync(ordenId);

    }

}