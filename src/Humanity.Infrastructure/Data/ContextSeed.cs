using Humanity.Domain.Entities;
using Humanity.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Infrastructure.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Administrator));
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Member));
        }

        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed SuperAdmin that is only one who can manage other users roles while app is running.
            var defaultUser = new User
            {
                UserName = "Admin",
                Email = "super.administrator@gmail.com",
                FirstName = "Arnold",
                LastName = "Schwarzenegger",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Ab.35*");
                    await userManager.AddToRoleAsync(defaultUser, RoleNames.Administrator);
                    await userManager.AddToRoleAsync(defaultUser, RoleNames.Member);
                }
            }
        }
    }
}
