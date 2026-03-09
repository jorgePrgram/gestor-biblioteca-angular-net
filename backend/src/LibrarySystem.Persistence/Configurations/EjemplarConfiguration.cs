using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Persistence.Configurations;

public class EjemplarConfiguration : IEntityTypeConfiguration<Ejemplar>
{
    public void Configure(EntityTypeBuilder<Ejemplar> builder)
    {
        builder.ToTable("Ejemplares","Biblioteca");
    }
}