using LibrarySystem.Dto.Request;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers;



[ApiController]
[Route("api/libros")]
public class LibrosController : ControllerBase
{
    private readonly ILibroService service;


    public LibrosController(ILibroService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await service.GetAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LibroRequestDto request)
    {
        var response = await service.AddAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, LibroRequestDto request)
    {
        var response = await service.UpdateAsync(id, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await service.DeleteAsync(id);

        return Ok(response);
    }

    [HttpGet("genre/{genreId:int}")]
    public async Task<IActionResult> GetByGenre(int genreId)
    {
        var result = await service.GetByGenreAsync(genreId);
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Cliente")]
    [HttpGet("{libroId}/ejemplares-disponibles")]
    public async Task<IActionResult> GetEjemplaresDisponibles(int libroId)
    {
        var response = await service.GetDisponiblesByLibroIdAsync(libroId);
        return Ok(response);
    }




}
