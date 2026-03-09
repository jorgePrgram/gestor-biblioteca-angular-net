namespace LibrarySystem.Persistence;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibrarySystem.Entities;

public class AppDbContext
    : IdentityDbContext<LibrarySystemUserIdentity>

{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoEjemplar> PedidoEjemplares { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Ejemplar> Ejemplares { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<PedidoEjemplar>()
    .HasKey(pl => new { pl.PedidoId, pl.EjemplarId });


        modelBuilder.Entity<PedidoEjemplar>()
            .HasOne(pl => pl.Pedido)
            .WithMany(p => p.PedidoEjemplares)
            .HasForeignKey(pl => pl.PedidoId);

        modelBuilder.Entity<PedidoEjemplar>()
            .HasOne(pl => pl.Ejemplar)
            .WithMany(l => l.PedidoEjemplares)
            .HasForeignKey(pl => pl.EjemplarId);
    }
}