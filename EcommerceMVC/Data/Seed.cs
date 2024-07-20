using EcommerceMVC.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace EcommerceMVC.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                string adminUserEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "0000000",
                        First_Name = "Admin",
                        Last_Name = "Admin",
                        Country = "Country",
                        Governorate = "Governorate",
                        City = "City",
                        Street = "Street",
                        Building = "Building",
                        Floor = 0,
                        Created_at = DateTime.Now,
                    };

                    await userManager.CreateAsync(newAdminUser, "Admin@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);

                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "0000000",
                        First_Name = "User",
                        Last_Name = "User",
                        Country = "Country",
                        Governorate = "Governorate",
                        City = "City",
                        Street = "Street",
                        Building = "Building",
                        Floor = 8,
                        Created_at = DateTime.Now,
                    };

                    await userManager.CreateAsync(newAppUser, "User@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}