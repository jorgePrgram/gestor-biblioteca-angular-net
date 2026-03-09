using LibrarySystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace LibrarySystem.Persistence.Seeders;

public class UserDataSeeder
{
    private readonly IServiceProvider service;

    public UserDataSeeder(IServiceProvider service)
    {
        this.service = service;
    }

    public async Task SeedAsync()
    {
        var userManager = service.GetRequiredService<UserManager<LibrarySystemUserIdentity>>();
        var roleManager=service.GetRequiredService<RoleManager<IdentityRole>>();
        //Creating roles
        var adminRole =new IdentityRole(Roles.Administrator);
        var clienteRole=new IdentityRole(Roles.Cliente);

        if(!await roleManager.RoleExistsAsync(Roles.Administrator))
            await roleManager.CreateAsync(adminRole);

        if(!await roleManager.RoleExistsAsync(Roles.Cliente))
            await roleManager.CreateAsync(clienteRole);

        //Admin user
        var adminUser = new LibrarySystemUserIdentity()
        {
 
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync("admin@gmail.com") is null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin1234*");
            if (result.Succeeded)
            {
                // Obtenemos el registro del usuario
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                // Aqui agregamos el Rol de Administrador para el usuario Admin
                if (adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Roles.Administrator);
            }
        }
    }


}
