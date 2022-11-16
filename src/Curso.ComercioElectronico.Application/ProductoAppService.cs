using System;
using Curso.ComercioElectronico.Application;

public class ProductoAppService : IProductoAppService
{
    private readonly IProductoRepository repository;
    private readonly IUnitOfWork unitOfWork;
    //Para realizar la consulta se iyecta IProductoRepository
    public ProductoAppService(IProductoRepository repository, IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.repository = repository;
    }

    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
    {
        //Reglas Validaciones... 
        var existeNombreProducto = await repository.ExisteNombre(productoDto.Nombre);
        if (existeNombreProducto)
        {
            throw new ArgumentException($"Ya existe un Producto con el nombre {productoDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        var producto = new Producto();
        producto.Nombre = productoDto.Nombre;
        producto.Precio = productoDto.Precio;
        producto.Observaciones = productoDto.Observaciones;
        producto.Codigo = productoDto.Codigo;
        producto.FechaIngreso = productoDto.FechaIngreso;
        producto.FechaCaducidad = productoDto.FechaCaducidad;
        producto.MarcaId = productoDto.MarcaId;
        producto.TipoProductoId = productoDto.TipoProductoId;



        //Persistencia objeto
        producto = await repository.AddAsync(producto);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var productoCreado = new ProductoDto();
        productoCreado.Nombre = producto.Nombre;
        productoCreado.Id = producto.Id;
        productoCreado.Precio = producto.Precio;
        productoCreado.Observaciones = producto.Observaciones;
        productoCreado.Codigo = producto.Codigo;
        productoCreado.FechaIngreso = producto.FechaIngreso;
        productoCreado.FechaCaducidad = producto.FechaCaducidad;
        productoCreado.MarcaId = producto.MarcaId;
        productoCreado.TipoProductoId = producto.TipoProductoId;
        // productoCreado.Marca = producto.Marca.Nombre;
        // productoCreado.TipoProducto = producto.TipoProducto.Nombre;

        return productoCreado;
    }

    public async Task<bool> DeleteAsync(int productoId)
    {
        var producto = await repository.GetByIdAsync(productoId);
        if (producto == null)
        {
            throw new ArgumentException($"La producto con el id: {productoId}, no existe");
        }

        repository.Delete(producto);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
    {

        var listaProductos = repository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);
        var total = listaProductos.Count();
        // var listaProductos = repository.GetAll().Skip(offset).Take(limit);
        var listaProductoDto = listaProductos.Skip(offset).Take(limit)
        .Select(
            x => new ProductoDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                FechaCaducidad = x.FechaCaducidad,
                Marca = x.Marca.Nombre,
                MarcaId = x.MarcaId,
                Observaciones = x.Observaciones,
                Precio = x.Precio,
                TipoProducto = x.TipoProducto.Nombre,
                TipoProductoId = x.TipoProductoId,
                Codigo = x.Codigo
            }
            );
        var lista = new ListaPaginada<ProductoDto>();
        lista.Total = total;
        lista.Lista = listaProductoDto.ToList();
        return lista;
    }

    // public async Task<Producto> GetByText(int limit = 10, int offset = 0, string nombre = "", string codigo = "")
    // {
    //     var consulta = repository.GetAll();
    //     consulta = consulta.Where(x => x.Nombre == nombre);
    //     var existeNombreProducto = await repository.ExisteNombre(nombre);
    //     if (!existeNombreProducto)
    //     {
    //         throw new ArgumentException($"No existe");
    //     }
    //     var listaProductoDto = consulta
    //     .Select(
    //         x => new ProductoDto()
    //         {
    //             Id = x.Id,
    //             FechaCaducidad = x.FechaCaducidad,
    //             FechaIngreso = x.FechaIngreso,
    //             MarcaId = x.MarcaId,
    //             Nombre = x.Nombre,
    //             Precio = x.Precio,
    //             Observaciones = x.Observaciones,
    //             TipoProductoId = x.TipoProductoId

    //         }
    //     );
    //     return Task.FromResult(listaProductoDto.SingleOrDefault());
    // }

    public Task<ProductoDto> GetByIdAsync(int id)
    {
        var consulta = repository.GetAllIncluding();
        consulta = consulta.Where(x => x.Id == id);

        var listaProductoDto = consulta
                                .Select(
                                    x => new ProductoDto()
                                    {
                                        Id = x.Id,
                                        Nombre = x.Nombre,
                                        FechaCaducidad = x.FechaCaducidad,
                                        Marca = x.Marca.Nombre,
                                        MarcaId = x.MarcaId,
                                        Observaciones = x.Observaciones,
                                        Precio = x.Precio,
                                        TipoProducto = x.TipoProducto.Nombre,
                                        TipoProductoId = x.TipoProductoId,
                                        Codigo = x.Codigo
                                    }
                                    );

        return Task.FromResult(listaProductoDto.SingleOrDefault());
    }

    public ListaPaginada<ProductoDto> GetListAsync(ProductoListInput input)
    {
        var listaProductos = repository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);
        // var listaProductos = repository.GetAll().Skip(offset).Take(limit);
        if (input.TipoProductoId.HasValue)
        {
            listaProductos = listaProductos.Where(x => x.TipoProductoId == input.TipoProductoId);
        }
        if (input.MarcaId.HasValue)
        {
            listaProductos = listaProductos.Where(x => x.MarcaId == input.MarcaId);
        }
        if (!string.IsNullOrEmpty(input.ValorBuscar))
        {
            listaProductos = listaProductos.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            x.Codigo.StartsWith(input.ValorBuscar)
            );
        }
        var total = listaProductos.Count();
        listaProductos = listaProductos.Skip(input.Offset).Take(input.Limit);
        var consultaListaProductoDto = listaProductos.Skip(input.Offset).Take(input.Limit).Select(
            x => new ProductoDto()
            {
                Id = x.Id,
                FechaCaducidad = x.FechaCaducidad,
                FechaIngreso = x.FechaIngreso,
                Marca = x.Marca.Nombre,
                MarcaId = x.MarcaId,
                Observaciones = x.Observaciones,
                Precio = x.Precio,
                TipoProducto = x.TipoProducto.Nombre,
                TipoProductoId = x.TipoProductoId,
                Codigo = x.Codigo,
                Nombre = x.Nombre
            }
        );
        var lista = new ListaPaginada<ProductoDto>();
        lista.Total = total;
        lista.Lista = consultaListaProductoDto.ToList();
        return lista;
    }

    public async Task UpdateAsync(int id, ProductoCrearActualizarDto productoDto)
    {
        var producto = await repository.GetByIdAsync(id);
        if (producto == null)
        {
            throw new ArgumentException($"La producto con el id: {id}, no existe");
        }

        var existeNombreMarca = await repository.ExisteNombre(productoDto.Nombre, id);
        if (existeNombreMarca)
        {
            throw new ArgumentException($"Ya existe un producto con el nombre {productoDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        producto.Nombre = productoDto.Nombre;
        producto.Precio = productoDto.Precio;
        producto.Observaciones = productoDto.Observaciones;
        producto.Codigo = productoDto.Codigo;
        producto.FechaCaducidad = productoDto.FechaCaducidad;
        producto.MarcaId = productoDto.MarcaId;
        producto.TipoProductoId = productoDto.TipoProductoId;

        //Persistencia objeto
        await repository.UpdateAsync(producto);
        await unitOfWork.SaveChangesAsync();

        return;
    }

}
