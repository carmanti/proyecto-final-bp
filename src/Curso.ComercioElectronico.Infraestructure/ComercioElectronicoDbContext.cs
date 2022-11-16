using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ComercioElectronicoDbContext : DbContext, IUnitOfWork
{

    //Agregar sus entidades
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<TipoProducto> TipoProductos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    // public DbSet<OrdenItem> OrdenItems { get; set; }

    public string DbPath { get; set; }

    //Configurar para usar BDD SqLite
    public ComercioElectronicoDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "curso.comercio-electronico.db");

    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>().Property(e => e.Precio).HasConversion<double>();
        modelBuilder.Entity<OrdenItem>().Property(e => e.Precio).HasConversion<double>();
    }

}



