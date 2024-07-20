using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using EcommerceMVC.Repositories.Abstract;
using EcommerceMVC.Repositories.Implementation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceMVC.Views.Shared.ViewComponents
{
    public class CartItemCountViewComponent : ViewComponent
    {
        private readonly CartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartItemCountViewComponent(CartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = HttpContext.User as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                return View(0);
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return View(0);
            }

            var count = await _cartRepository.GetCartItemCountAsync(userId);
            return View(count);
        }
    }
}