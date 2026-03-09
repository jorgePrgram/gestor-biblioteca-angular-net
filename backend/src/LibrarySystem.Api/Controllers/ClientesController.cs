using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace LibrarySystem.Api.Controllers;


[ApiController]
[Route("/api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService service;

    public ClientesController(IClienteService service)
    {
        this.service = service;
    }  

    [HttpGet]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<ClienteResponseDto>>>> Get()
    {
        var clientes = await service.GetAsync();
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponseGeneric<int>>> Post([FromBody] ClienteRequestDto request)
    {
        var response=await service.AddAsync(request);
        return Ok(response);
    }

  



}
