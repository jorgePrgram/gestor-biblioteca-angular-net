using LibrarySystem.Dto.Request;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers;


[ApiController]
[Route("api/genres")]
public class GenresController: ControllerBase
{
    private readonly IGenreService service;

    public GenresController(IGenreService service)
    {
        this.service = service;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await service.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response= await service.GetAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(GenreRequestDto request)
    {
        var response= await service.AddAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response= await service.DeleteAsync(id);    
        return response.Success ?  Ok(response) : BadRequest(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(int id, GenreRequestDto request) {
        var response = await service.UpdateAsync(id, request);
        return response.Success ? Ok(response) : BadRequest(response);

    } 

    

}
