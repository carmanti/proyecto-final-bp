namespace Curso.ComercioElectronico.Application;

public interface IProductoAppService
{
    Task<ProductoDto> GetByIdAsync(int id);
    ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0);
    ListaPaginada<ProductoDto> GetListAsync(ProductoListInput input);
    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto);
    Task UpdateAsync(int id, ProductoCrearActualizarDto producto);
    Task<bool> DeleteAsync(int productoId);

}

public class ProductoListInput
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
    public int? TipoProductoId { get; set; }
    public int? MarcaId { get; set; }
    public string ValorBuscar { get; set; }

}
public class ListaPaginada<T> where T : class
{
    public ICollection<T> Lista { get; set; }
    public long Total { get; set; }


}
