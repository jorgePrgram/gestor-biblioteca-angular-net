using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Repositories.Interfaces;
using LibrarySystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Services.Implementations;

public class ClienteService:IClienteService
{
    private readonly IClienteRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<ClienteService> logger;

    public ClienteService(IClienteRepository repository, ILogger<ClienteService> logger, IMapper mapper)
    {
        this.repository = repository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BaseResponseGeneric<ICollection<ClienteResponseDto>>> GetAsync()
    {
        var response = new BaseResponseGeneric<ICollection<ClienteResponseDto>>();
        try
        {
            var entities=await repository.GetAllAsync();
            Console.WriteLine($"Clientes encontrados: {entities.Count}");
            foreach (var c in entities)
            {
                Console.WriteLine($"Cliente: {c.Nombre} {c.Apellido}, Edad: {c.Edad}");
            }
            response.Data = mapper.Map<ICollection<ClienteResponseDto>>(entities);
            response.Success = true;
        }
        catch (Exception ex)
        {
            
            response.Success = false;
            /*
            response.ErrorMessage = "Ocurrió un error al obtener los clientes.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        */
            response.ErrorMessage = ex.Message; // <--- así ves el error exacto
            logger.LogError(ex, "Error real al obtener clientes");
        }

        return response;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(ClienteRequestDto request)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            var entity = mapper.Map<Cliente>(request);
            response.Data = await repository.AddAsync(entity) ;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ErrorMessage = "Ocurrió un error al registrar el cliente.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, ClienteRequestDto request)
    {
        var response = new BaseResponse();

        try
        {
            var entity = await repository.GetAsync(id);

            if (entity is null)
            {
                response.Success = false;
                response.ErrorMessage = "No se encontró el cliente.";
                return response;
            }
            var updatedCliente=mapper.Map<Cliente>(request);

            updatedCliente.Id = id;
            await repository.UpdateAsync(updatedCliente);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ErrorMessage = "Ocurrió un error al actualizar el cliente.";
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
            response.Success = false;
            response.ErrorMessage = "Ocurrió un error al eliminar el cliente.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}