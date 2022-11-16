public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProdcutoRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public TipoProductoAppService(ITipoProdcutoRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {
        //Reglas Validaciones... 
        var existeNombreTipoProducto = await repository.ExisteNombreTipoProducto(tipoProductoDto.Nombre);
        if (existeNombreTipoProducto)
        {
            throw new ArgumentException($"Ya existe una marca con el nombre {tipoProductoDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        var tipoProducto = new TipoProducto();
        tipoProducto.Nombre = tipoProductoDto.Nombre;

        //Persistencia objeto
        tipoProducto = await repository.AddAsync(tipoProducto);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var tipoCreada = new TipoProductoDto();
        tipoCreada.Nombre = tipoProducto.Nombre;
        tipoCreada.Id = tipoProducto.Id;

        //TODO: Enviar un correo electronica... 

        return tipoCreada;
    }

    public async Task<bool> DeleteAsync(int tipoProductoId)
    {
        var tipoProducto = await repository.GetByIdAsync(tipoProductoId);
        if (tipoProducto == null)
        {
            throw new ArgumentException($"La marca con el id: {tipoProductoId}, no existe");
        }
        repository.Delete(tipoProducto);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public ICollection<TipoProductoDto> GetAll()
    {
        var tipoProductoList = repository.GetAll();

        var tipoProductoListDto = from m in tipoProductoList
                                  select new TipoProductoDto()
                                  {
                                      Id = m.Id,
                                      Nombre = m.Nombre
                                  };

        return tipoProductoListDto.ToList();
    }

    public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tipoProductoDto)
    {
        var tipoProducto = await repository.GetByIdAsync(id);
        if (tipoProducto == null)
        {
            throw new ArgumentException($"El tipo producto con id: {id} no existe");
        }

        var existeNombreTipoProducto = await repository.ExisteNombreTipoProducto(tipoProductoDto.Nombre, id);
        if (existeNombreTipoProducto)
        {
            throw new ArgumentException($"Ya existe un tipo Producto con el nombre {tipoProductoDto.Nombre}");
        }

        tipoProducto.Nombre = tipoProductoDto.Nombre;
        await repository.UpdateAsync(tipoProducto);
        await unitOfWork.SaveChangesAsync();

        return;

    }
}