using AutoMapper;
using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Enums;
using LibrarySystem.Repositories.Interfaces;
using LibrarySystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Services.Implementations;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository pedidoRepository;
    private readonly IEjemplarRepository ejemplarRepository;
    private readonly IClienteRepository clienteRepository;
    private readonly ILogger<PedidoService> logger;
    private readonly IMapper mapper;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IEjemplarRepository ejemplarRepository,
        IClienteRepository clienteRepository,
        ILogger<PedidoService> logger,
        IMapper mapper)
    {
        this.pedidoRepository = pedidoRepository;
        this.ejemplarRepository = ejemplarRepository;
        this.clienteRepository = clienteRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(PedidoRequestDto request, string userId)
    {
        var response = new BaseResponseGeneric<int>();

        await pedidoRepository.ExecuteTransactionAsync(async () =>
        {
            // =========================
            // 1. Validar cliente
            // =========================
            var cliente = await clienteRepository.GetByUserIdAsync(userId);

            if (cliente is null || cliente.UserId != userId)
            {
                response.ErrorMessage = "La cantidad de libros y códigos no coincide.";
                return;
            }
            // =========================
            // 2. Validar cantidades
            // =========================
            if (request.LibrosIds.Count != request.CodigosBarra.Count)
            {
                response.ErrorMessage = "La cantidad de libros y códigos no coincide.";
                return;
            }

            // =========================
            // 3. Validar duplicados
            // =========================
            if (request.LibrosIds.Count != request.LibrosIds.Distinct().Count())
            {
                response.ErrorMessage = "No puedes enviar el mismo libro dos veces.";
                return;
            }
               

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                FechaPedido = DateTime.Now,
                Status = true,
                PedidoEjemplares = new List<PedidoEjemplar>()
            };

            for (int i = 0; i < request.LibrosIds.Count; i++)
            {
                var libroId = request.LibrosIds[i];
                var codigoBarra = request.CodigosBarra[i];

                var ejemplar = await ejemplarRepository.GetByCodigoBarraAsync(codigoBarra);

                if (ejemplar is null)
                    throw new Exception($"El código {codigoBarra} no existe.");

                if (ejemplar.LibroId != libroId)
                    throw new Exception($"El código {codigoBarra} no pertenece al libro.");

                if (ejemplar.Estado != EstadoEjemplar.Disponible)
                    throw new Exception($"El ejemplar {codigoBarra} no está disponible.");

                var yaTienePrestamo = await pedidoRepository
                    .ExistePrestamoActivoAsync(cliente.Id, libroId);

                if (yaTienePrestamo)
                    throw new Exception("Ya tienes este libro prestado.");

                pedido.PedidoEjemplares.Add(new PedidoEjemplar
                {
                    EjemplarId = ejemplar.Id,
                    FechaReserva=DateTime.Now,
                });

                ejemplar.Estado = EstadoEjemplar.Reservado;
            }

            await pedidoRepository.AddAsync(pedido);

            response.Success = true;
            response.Data = pedido.Id;
        });

        return response;
    }





    public async Task<BaseResponseGeneric<PedidoResponseDto>> GetAsync(int id, string userId, bool isAdmin)
    {
        var response = new BaseResponseGeneric<PedidoResponseDto>();

        try
        {
            // Obtener pedido con cliente y libros
            var pedido = await pedidoRepository.GetByIdWithEjemplaresAsync(id);

            if (pedido is null)
            {
                response.ErrorMessage = "No se encontró el pedido.";
                return response;
            }
            if(!isAdmin && pedido.Cliente.UserId != userId)
            {
                response.ErrorMessage = "No tienes acceso a este pedido";
                return response;
            }

            // Mapear a DTO
            response.Data = mapper.Map<PedidoResponseDto>(pedido);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener el pedido.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> DevolverEjemplarAsync(int pedidoId, string codigoBarra)
    {
        var response = new BaseResponse();

        try
        {
            var pedido = await pedidoRepository.GetByIdWithEjemplaresAsync(pedidoId);

            if (pedido is null)
            {
                response.ErrorMessage = "Pedido no encontrado.";
                return response;
            }

         

            var pedidoEjemplar = pedido.PedidoEjemplares
                .FirstOrDefault(pe => pe.Ejemplar.CodigoBarra == codigoBarra);

            if (pedidoEjemplar is null)
            {
                response.ErrorMessage = "El ejemplar no pertenece a este pedido.";
                return response;
            }

            if (pedidoEjemplar.Ejemplar.Estado != EstadoEjemplar.Prestado)
            {
                response.ErrorMessage = "Solo se pueden devolver ejemplares prestados.";
                return response;
            }

            // ?? REGLAS DE NEGOCIO
            pedidoEjemplar.Ejemplar.Estado = EstadoEjemplar.Disponible;

            pedidoEjemplar.FechaDevolucion = DateTime.Now;

            await ejemplarRepository.UpdateAsync(pedidoEjemplar.Ejemplar);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al devolver el ejemplar.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponseGeneric<List<int>>> GetLibrosPrestadosAsync(
       string userId,
       bool isAdmin,
       int? clienteId)
    {
        var response = new BaseResponseGeneric<List<int>>();

        try
        {
            int idCliente;

            if (isAdmin)
            {
                // Admin debe especificar clienteId
                if (!clienteId.HasValue)
                {
                    response.ErrorMessage = "El administrador debe especificar un clienteId.";
                    return response;
                }

                // Validar que el cliente existe
                var clienteAdmin = await clienteRepository.GetAsync(clienteId.Value);
                if (clienteAdmin is null)
                {
                    response.ErrorMessage = "El cliente especificado no existe.";
                    return response;
                }

                idCliente = clienteId.Value;
            }
            else
            {
                // Usuario normal solo puede ver sus propios libros
                if (clienteId.HasValue && clienteId.Value != 0)
                {
                    // Intentando acceder a otro cliente sin ser admin
                    response.ErrorMessage = "No tienes permisos para acceder a este cliente.";
                    return response;
                }

                var cliente = await clienteRepository.GetByUserIdAsync(userId);
                if (cliente is null)
                {
                    response.ErrorMessage = "No tienes un perfil de cliente asociado.";
                    return response;
                }

                idCliente = cliente.Id;
            }

            // Obtener libros prestados
            var libros = await pedidoRepository.GetLibrosPrestadosAsync(idCliente);

            response.Data = libros;
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al obtener libros prestados para userId: {UserId}", userId);
            response.ErrorMessage = "Ocurrió un error al obtener los libros prestados.";
            return response;
        }
    }

    public async Task<BaseResponseGeneric<List<PedidoResponseDto>>> GetPedidosAsync(string userId, bool isAdmin, int? clienteId)
    {
        var response= new BaseResponseGeneric<List<PedidoResponseDto>>();

        try
        {
            List<Pedido> pedidos;

            if (isAdmin)
            {
                if (clienteId.HasValue)
                {
                    pedidos=await pedidoRepository.GetByClienteWhitEjemplaresAsync(clienteId.Value);
                }
                else
                {
                    pedidos=await pedidoRepository.GetAllWithEjemplaresAsync();
                }
            }
            else
            {
                var cliente= await clienteRepository.GetByUserIdAsync(userId);
                if(cliente is null)
                {
                    response.ErrorMessage = "Cliente no encontrado";
                    return response;
                }
                pedidos=await pedidoRepository.GetByClienteWhitEjemplaresAsync(cliente.Id);

                
            }
            response.Data = mapper.Map<List<PedidoResponseDto>>(pedidos);
            response.Success = true;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error obteniendo pedidos");
            response.ErrorMessage = "Error al obtener pedidos.";
        }
        return response;
    }


    public async Task<BaseResponse> ConfirmarPrestamoAsync(
    int pedidoId,
    string codigoBarra)
    {
        var response = new BaseResponse();

        try
        {
            

            var pedido = await pedidoRepository.GetByIdWithEjemplaresAsync(pedidoId);

            if (pedido is null)
            {
                response.ErrorMessage = "Pedido no encontrado.";
                return response;
            }

            var pedidoEjemplar = pedido.PedidoEjemplares
                .FirstOrDefault(pe => pe.Ejemplar.CodigoBarra == codigoBarra);

            if (pedidoEjemplar is null)
            {
                response.ErrorMessage = "El ejemplar no pertenece a este pedido.";
                return response;
            }

            if (pedidoEjemplar.Ejemplar.Estado != EstadoEjemplar.Reservado)
            {
                response.ErrorMessage = "El ejemplar no está en estado reservado.";
                return response;
            }


            // Cambio real de estado
            pedidoEjemplar.Ejemplar.Estado = EstadoEjemplar.Prestado;
            pedidoEjemplar.FechaPrestamo = DateTime.Now;

            await ejemplarRepository.UpdateAsync(pedidoEjemplar.Ejemplar);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al confirmar el préstamo.";
            logger.LogError(ex, "Error confirmando préstamo");
        }

        return response;
    }




}