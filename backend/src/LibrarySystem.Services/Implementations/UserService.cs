using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;
using LibrarySystem.Entities;
using LibrarySystem.Persistence;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security;

namespace LibrarySystem.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<LibrarySystemUserIdentity> userManager;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IClienteService clienteService;
    private readonly ILogger<UserService> logger;
    private readonly IEmailService emailService;

    public UserService(UserManager<LibrarySystemUserIdentity> userManager, IJwtTokenService jwtTokenService, IClienteService clienteService, ILogger<UserService> logger,
        IEmailService emailService)
    {
        this.userManager = userManager;
        this.jwtTokenService = jwtTokenService;
        this.clienteService = clienteService;
        this.logger = logger;
        this.emailService = emailService;
    }

    public async Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordRequestDto request)
    {
        var response= new BaseResponse();
        try
        {
            var user=await userManager.FindByEmailAsync(email);
            if (user is null)
                throw new SecurityException("Usuario no existe");
            var result = await userManager.ChangePasswordAsync(
                user,
                request.OldPassword, request.NewPassword
                );
            response.Success = result.Succeeded;
            if(!result.Succeeded)
            {
                response.ErrorMessage=string.Join(", ", result.Errors.Select(e=>e.Description));
            }
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error cambiando password");
            response.ErrorMessage = "Error al cambiar contraseña";
        }
        return response;
    }

    public async Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var response= new BaseResponseGeneric<LoginResponseDto>();
        try
        {
            var user = await userManager.FindByEmailAsync(request.UserName);
            if (user ==null)
            {
                response.ErrorMessage = "Credenciales incorrectas";
                return response;
            }
            var validPassword=await userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
            {
                response.ErrorMessage = "Credenciales incorrectas";
                return response;
            }
            response.Success = true;
            response.Data= await jwtTokenService.GenerateTokenAsync(user);
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error en login");
            response.ErrorMessage = "Error al iniciar sesion";
        }
        return response;
        
    }

    public async Task<BaseResponseGeneric<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        var response= new BaseResponseGeneric<RegisterResponseDto>();

        try
        {
            var user = new LibrarySystemUserIdentity
            {
                UserName=request.Email,
                Email=request.Email,
                EmailConfirmed=true,
                FirstName=request.FirstName,
                LastName=request.LastName,
            };
            var result=await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) { 
                response.ErrorMessage=string.Join(", ", result.Errors.Select(e=>e.Description));
                return response;
            }
            await userManager.AddToRoleAsync(user, Roles.Cliente);

            var clienteRequest = new ClienteRequestDto
            {
                Nombre = request.FirstName,
                Apellido = request.LastName,
                Email = request.Email,
                Edad= request.Edad,
                Dni= request.DocumentNumber,

                UserId=user.Id,
            };

            var clienteResult= await clienteService.AddAsync(clienteRequest);
            if (!clienteResult.Success) {
                await userManager.DeleteAsync(user);
                response.ErrorMessage=clienteResult.ErrorMessage;
                return response;
            }

            var token = await jwtTokenService.GenerateTokenAsync(user);
            response.Success = true;
            response.Data = new RegisterResponseDto
            {
                UserId = user.Id,
                Token=token.Token,
                ExpirationDate =token.ExpirationDate,
            };
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error al registrar usuario");
            response.ErrorMessage = ex.Message.Contains("cliente")
       ? ex.Message
       : "Error al registrar usuario";
        }
        return response;

    }

    public async Task<BaseResponse> RequestTokenResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var response=new BaseResponse();
        try {
            var user=await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new SecurityException("Ususario no exite");
            }
            var token= await userManager.GeneratePasswordResetTokenAsync(user);

            await emailService.SendEmailAsync(
                request.Email,
                "Reset Passord",
                $"Token: {token}"
                );
            response.Success = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error solicitando reset password");
            response.ErrorMessage = "Error al solicitar reset de contraseña";
        }

        return response;
    }

    public async Task<BaseResponse> ResetPasswordAsync(NewPasswordRequestDto request)
    {
        var response= new BaseResponse();

        try {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new SecurityException("Usuario no existe");
            var result = await userManager.ResetPasswordAsync(
                user,
                request.Token,
                request.NewPassword
                );

            response.Success = result.Succeeded;
            if (!result.Succeeded) { 
                response.ErrorMessage = string.Join(", ",
                    result.Errors.Select(e => e.Description));
                 }
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error reset password");
            response.ErrorMessage = "Error al resetear contraseña";
        }

        return response;
    }
}