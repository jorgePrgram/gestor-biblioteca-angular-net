using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;

namespace LibrarySystem.Services.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteResponseDto>();
        CreateMap<ClienteRequestDto, Cliente>();
    }
  

}