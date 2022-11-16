

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{

    private readonly IProductoAppService productoAppService;

    public ProductoController(IProductoAppService productoAppService)
    {
        this.productoAppService = productoAppService;
    }

    [HttpGet]
    public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
    {

        return productoAppService.GetAll(limit, offset);
    }

    [HttpGet("{id}")]
    public async Task<ProductoDto> GetByIdAsync(int id)
    {

        return await productoAppService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto)
    {

        return await productoAppService.CreateAsync(producto);

    }

    [HttpPut]
    public async Task UpdateAsync(int id, ProductoCrearActualizarDto producto)
    {

        await productoAppService.UpdateAsync(id, producto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int productoId)
    {

        return await productoAppService.DeleteAsync(productoId);

    }

    [HttpGet("busqueda")]
    public ListaPaginada<ProductoDto> GetListAsync([FromQuery] ProductoListInput input)
    {
        return productoAppService.GetListAsync(input);

    }
    // public virtual IQueryable<Producto> GetByText(int limit = 10, int offset = 0, string campo = "", string parametro = "")
    // {
    //     return productoAppService.GetByText(limit, offset, campo, parametro);

    // }
}