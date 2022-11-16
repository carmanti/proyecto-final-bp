public interface ITipoProductoAppService
{
    ICollection<TipoProductoDto> GetAll();
    Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProducto);
    Task UpdateAsync(int id, TipoProductoCrearActualizarDto tipoProducto);
    Task<bool> DeleteAsync(int tipoProductoId);
}