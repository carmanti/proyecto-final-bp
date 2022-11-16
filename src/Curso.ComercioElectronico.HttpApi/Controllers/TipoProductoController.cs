using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TipoProductoController : ControllerBase
{
    private readonly ITipoProductoAppService tipoProductoAppService;
    public TipoProductoController(ITipoProductoAppService tipoProductoAppService)
    {
        this.tipoProductoAppService = tipoProductoAppService;
    }
    // private readonly ITipoProductoAppService 
    [HttpGet]
    public ICollection<TipoProductoDto> GetAll()
    {
        return tipoProductoAppService.GetAll();
    }
    [HttpPost]
    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipo)
    {
        return await tipoProductoAppService.CreateAsync(tipo);
    }
    [HttpPut]
    public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tipo)
    {
        await tipoProductoAppService.UpdateAsync(id, tipo);
    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int tipoId)
    {
        return await tipoProductoAppService.DeleteAsync(tipoId);
    }
}