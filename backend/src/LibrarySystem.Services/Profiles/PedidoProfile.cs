using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Enums;

namespace LibrarySystem.Services.Profiles;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, PedidoResponseDto>()
             .ForMember(dest => dest.ClienteNombre,
                opt => opt.MapFrom(src => src.Cliente.Nombre))

            .ForMember(dest => dest.Ejemplares,
                opt => opt.MapFrom(src =>
                    src.PedidoEjemplares
                ))

            .ForMember(dest => dest.EstadoPedido,
                opt => opt.MapFrom(src =>
                    src.PedidoEjemplares.All(pl => pl.Ejemplar.Estado == EstadoEjemplar.Disponible)
                        ? "Devuelto"
                        : "Pendiente"
                ));

        CreateMap<PedidoEjemplar, PedidoEjemplarResponseDto>()
                .ForMember(dest => dest.EjemplarId,
                    opt => opt.MapFrom(src => src.EjemplarId))

                .ForMember(dest => dest.CodigoBarra,
                    opt => opt.MapFrom(src => src.Ejemplar.CodigoBarra))

                .ForMember(dest => dest.NombreLibro,
                    opt => opt.MapFrom(src => src.Ejemplar.Libro.Nombre))

                .ForMember(dest => dest.Estado,
                    opt => opt.MapFrom(src => (int)src.Ejemplar.Estado));
    }


}