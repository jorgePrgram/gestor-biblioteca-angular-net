using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
namespace LibrarySystem.Persistence.Configurations;

public class ClienteConfiguration: IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente", "Biblioteca");
    }
}