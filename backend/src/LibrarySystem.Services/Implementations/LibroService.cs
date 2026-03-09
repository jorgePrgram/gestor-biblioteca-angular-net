using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Repositories.Implementations;
using LibrarySystem.Repositories.Interfaces;
using LibrarySystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Services.Implementations;

public class LibroService : ILibroService
{
    private readonly ILibroRepository repository;
    private readonly ILogger<LibroService> logger;
    private readonly IMapper mapper;
    private readonly IEjemplarRepository ejemplarRepository;


    public LibroService(ILibroRepository repository, IEjemplarRepository ejemplarRepository, ILogger<LibroService> logger, IMapper mapper)
    {
        this.repository = repository;
        this.ejemplarRepository = ejemplarRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(LibroRequestDto request)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
         
            var entity = mapper.Map<Libro>(request);
            response.Data = await repository.AddAsync(entity);
            response.Success = true;
        }
        catch (Exception ex)
        {
           
            response.ErrorMessage = "Ocurrió un error al añadir la información.";
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
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al eliminar el registro.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<LibroResponseDto>>> GetAllAsync()
    {
        var response = new BaseResponseGeneric<ICollection<LibroResponseDto>>();
        try
        {
            var entities = await repository.GetAllAsync();
            response.Data = mapper.Map<ICollection<LibroResponseDto>>(entities);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener los datos.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<LibroResponseDto>> GetAsync(int id)
    {
        var response = new BaseResponseGeneric<LibroResponseDto>();
        try
        {
            var entity = await repository.GetAsync(id);
            response.Data = mapper.Map<LibroResponseDto>(entity);
            response.Success = entity != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener los datos.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<LibroResponseDto>>> GetByGenreAsync(int genreId)
    {
        var response = new BaseResponseGeneric<ICollection<LibroResponseDto>>();
        try
        {
            var entities= await repository.GetByGenreAsync(genreId);
            response.Data = mapper.Map<ICollection<LibroResponseDto>>(entities);
            response.Success = true;
        }catch(Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al filtrar por género.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<EjemplarResponseDto>>> GetDisponiblesByLibroIdAsync(int libroId)
    {
        var response = new BaseResponseGeneric<ICollection<EjemplarResponseDto>>();

        var ejemplares=await ejemplarRepository.GetDisponiblesByLibroIdAsync(libroId);

        response.Data=  mapper.Map<ICollection<EjemplarResponseDto>>(ejemplares);
        response.Success = true;

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, LibroRequestDto request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await repository.GetAsync(id);
            if (entity is not null)
            {
                mapper.Map(request, entity);
                await repository.UpdateAsync(entity);
                response.Success = true;
                return response;
            }

            response.ErrorMessage = "No se encontró el registro.";

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al actualizar la información.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }






}