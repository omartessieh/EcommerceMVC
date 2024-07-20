using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.DTO;
using EcommerceMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly DatabaseContext _context;

        public UserAuthenticationService(DatabaseContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._context = context;
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Country = model.Country,
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = true,
                Governorate = model.Governorate,
                City = model.City,
                Street = model.Street,
                Building = model.Building,
                Floor = model.Floor
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                return status;
            }

            //if (!await roleManager.RoleExistsAsync(model.Role))
            //    await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();

        }

        //public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        //{
        //    var status = new Status();
        //    var user = await userManager.FindByNameAsync(username);
        //    if (user == null)
        //    {
        //        status.Message = "User does not exist";
        //        status.StatusCode = 0;
        //        return status;
        //    }
        //    var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        status.Message = "Password has updated successfully";
        //        status.StatusCode = 1;
        //    }
        //    else
        //    {
        //        status.Message = "Some error occcured";
        //        status.StatusCode = 0;
        //    }
        //    return status;
        //}
    }
}