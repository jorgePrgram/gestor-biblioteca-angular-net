using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Repositories.Interfaces;
using LibrarySystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GenreService> logger;

    public GenreService(IGenreRepository repository, IMapper mapper,ILogger<GenreService> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request)
    {
       var response= new BaseResponseGeneric<int>(); 
        try
        {
            var entity=mapper.Map<Genre>(request);
            response.Data=await repository.AddAsync(entity);
            response.Success=true;
        }
        catch (Exception ex) {
            response.ErrorMessage = "Ocurrio un error al añadir la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            await repository.DeleteAsync(id);   
            response.Success=true;  
        }
        catch (Exception ex) {
            response.ErrorMessage = "Ocurrio un error al eliminar informacion";
            logger.LogError(ex,"{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAllAsync()
    {
        var response=new BaseResponseGeneric<ICollection<GenreResponseDto>>();
        try
        {
            var entities=await repository.GetAllAsync();    
            response.Data=mapper.Map<ICollection<GenreResponseDto>>(entities);
            response.Success=true;
        }
        catch (Exception ex) {
            response.ErrorMessage = "Ocurrio un error al obtener informacion";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response ;
    }

    public async Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id)
    {
        var response=new BaseResponseGeneric<GenreResponseDto>();

        try
        {
            var entity=await repository.GetAsync(id);   
            response.Data = mapper.Map<GenreResponseDto> (entity);
            response.Success=true;
        }
        catch (Exception ex) {
            response.ErrorMessage = "Ocurrio un error al obtener informacion";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request)
    {
       var response= new BaseResponse();
        try
        {
            var entity=await repository.GetAsync (id);
            if (entity == null)
            {
                response.ErrorMessage = "No se encontro el registro";
                return response;
            }
            mapper.Map(request,entity);
            await repository.UpdateAsync(entity);
            response.Success=true;

        }
        catch (Exception ex) {
            response.ErrorMessage = "Ocurrió un error al actualizar la información.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}