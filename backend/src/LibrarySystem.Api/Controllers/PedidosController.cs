using LibrarySystem.Dto.Request;
using LibrarySystem.Entities;
using LibrarySystem.Repositories.Implementations;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibrarySystem.Api.Controllers;

[ApiController]
[Route("api/pedidos")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        this.pedidoService = pedidoService;
    }
    // 1. CREAR PEDIDO
    [HttpPost]
    [Authorize(Roles = Roles.Cliente)]
    public async Task<IActionResult> Post([FromBody] PedidoRequestDto request)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);  
    
        var response = await pedidoService.AddAsync(request, userId);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response); 
    }

    // 2. OBTENER PEDIDO POR ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin=User.IsInRole(Roles.Administrator);
        var response = await pedidoService.GetAsync(id, userId, isAdmin);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPut("{pedidoId}/libros/{codigoBarra}/devolver")]
    public async Task<IActionResult> DevolverLibro(int pedidoId, string codigoBarra)
    {
       

        var response = await pedidoService.DevolverEjemplarAsync(pedidoId, codigoBarra);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }



    // GET api/pedidos?clienteId=13
    [HttpGet("libros-prestados")]
    public async Task<IActionResult> GetLibrosPrestados([FromQuery] int? clienteId)
    {
        // Obtener info del usuario logueado
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Administrator");

        // Llamar al service
        var response = await pedidoService.GetLibrosPrestadosAsync(userId, isAdmin, clienteId);

        // Manejar la respuesta
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos([FromQuery] int? clienteId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Administrator");

        var response = await pedidoService
            .GetPedidosAsync(userId, isAdmin, clienteId);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPut("{pedidoId}/confirmar-prestamo")]
    public async Task<IActionResult> ConfirmarPrestamo(
     int pedidoId,
     string codigoBarra)
    {

        var response = await pedidoService
            .ConfirmarPrestamoAsync(pedidoId, codigoBarra);

        return response.Success ? Ok(response) : BadRequest(response);
    }



}
