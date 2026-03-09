using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Enums;

namespace LibrarySystem.Services.Profiles;

public class LibroProfile : Profile
{
    public LibroProfile()
    {
        CreateMap<Libro, LibroResponseDto>()
             .ForMember(dest => dest.GenreNombre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.StockDisponible,
                opt => opt.MapFrom(src =>
                    src.Ejemplares.Count(e => e.Estado == EstadoEjemplar.Disponible)
                ))

            .ForMember(dest => dest.EstadoDescripcion,
                opt => opt.MapFrom(src =>
                    src.Ejemplares.Any(e => e.Estado == EstadoEjemplar.Disponible)
    ? "Disponible"
    : "No disponible"
                ));

        CreateMap<LibroRequestDto, Libro>()
             .ForMember(x => x.Genre, o => o.Ignore())
         .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));



        CreateMap<Ejemplar, EjemplarResponseDto>();


    }
}