using LibrarySystem.Dto.Request;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibrarySystem.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase

{
    private readonly IUserService service;

    public UsersController(IUserService service)
    {
        this.service = service;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Result([FromBody] RegisterRequestDto request)
    {
        var response = await service.RegisterAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await service.LoginAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("RequestTokenToResetPassword")]
    public async Task<IActionResult> ResquestTokenToResetPassword(ResetPasswordRequestDto request)
    {
        var response = await service.RequestTokenResetPasswordAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(NewPasswordRequestDto reques)
    {
        var response = await service.ResetPasswordAsync(reques);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("ChangePassword")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto request)
    {
        //Get authenticated user email
        var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;
        var response = await service.ChangePasswordAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }


}
