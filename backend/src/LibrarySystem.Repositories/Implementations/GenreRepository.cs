using LibrarySystem.Entities;
using LibrarySystem.Persistence;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context)
    {
    }
}