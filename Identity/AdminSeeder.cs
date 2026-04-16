using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medicos.Backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Medicos.Backend.Identity
{
    public class AdminSeeder
    {
        
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider, IConfiguration config)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        string adminEmail = config["AdminUser:Email"];
        string adminPassword = config["AdminUser:Password"];

        var existingUser = await userManager.FindByEmailAsync(adminEmail);

        if (existingUser != null)
            return; // ya existe

        var adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (!result.Succeeded)
            throw new Exception("Error creando usuario Admin inicial");

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    }
}