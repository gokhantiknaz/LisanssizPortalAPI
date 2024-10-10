
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Humanity.WebApi.StartupTasks;

public class AuthenticationStartupTask(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var roleNames = new string[] { RoleNames.Administrator, RoleNames.Member, RoleNames.User };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new Role(roleName));
            }
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var administratorUser = new User
        {
            UserName = "gokhantiknaz@gmail.com",
            Email = "gokhantiknaz@gmail.com",
            FirstName = "Gökhan",
            LastName = "TIKNAZOGLU",
            RefreshToken=""
        };

        await CheckCreateUserAsync(administratorUser, "1234qqqQ!.!", RoleNames.Administrator, RoleNames.User);

        async Task CheckCreateUserAsync(User user, string password, params string[] roles)
        {
            var dbUser = await userManager.FindByEmailAsync(user.Email);
            if (dbUser == null)
            {
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
