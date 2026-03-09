using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;

namespace LibrarySystem.Services.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile() { 
        CreateMap<Genre, GenreResponseDto>();
        CreateMap<GenreRequestDto, Genre>();
    }
}