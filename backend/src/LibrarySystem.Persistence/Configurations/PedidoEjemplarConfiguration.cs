using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Persistence.Configurations;

public class PedidoEjemplarConfiguration : IEntityTypeConfiguration<PedidoEjemplar>
{
    public void Configure(EntityTypeBuilder<PedidoEjemplar> builder)
    {
        builder.ToTable("PedidoEjemplares", "Biblioteca");
    }
}