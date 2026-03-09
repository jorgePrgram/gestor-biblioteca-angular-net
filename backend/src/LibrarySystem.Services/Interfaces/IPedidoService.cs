using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using System.Threading.Tasks;

namespace LibrarySystem.Services.Interfaces;

public interface IPedidoService
{
    Task<BaseResponseGeneric<int>> AddAsync(PedidoRequestDto request, string userId);
    Task<BaseResponseGeneric<PedidoResponseDto>> GetAsync(int id, string userId, bool isAdmin);

    Task<BaseResponse> DevolverEjemplarAsync(int pedidoId, string codigoBarra);
    Task<BaseResponseGeneric<List<int>>> GetLibrosPrestadosAsync(string userId, bool isAdmin, int? clienteId);

    Task<BaseResponseGeneric<List<PedidoResponseDto>>> GetPedidosAsync(string userId, bool isAdmin, int? clienteId);

    Task<BaseResponse> ConfirmarPrestamoAsync(int pedidoId, string codigoBarra);

    }