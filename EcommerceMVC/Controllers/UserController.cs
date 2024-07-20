using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EcommerceMVC.Repositories.Abstract;
using EcommerceMVC.Models.View;
using EcommerceMVC.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EcommerceMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(DatabaseContext context, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Username = user.UserName,
                Email = user.Email,
                Country = user.Country,
                Governorate = user.Governorate,
                City = user.City,
                Street = user.Street,
                Building = user.Building,
                PhoneNumber = user.PhoneNumber,
                Floor = user.Floor
            };

            return View(editMV);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EditProfileViewModel editVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            bool isChanged = false;

            if (user.First_Name != editVM.First_Name)
            {
                user.First_Name = editVM.First_Name;
                isChanged = true;
            }
            if (user.Last_Name != editVM.Last_Name)
            {
                user.Last_Name = editVM.Last_Name;
                isChanged = true;
            }
            if (user.Email != editVM.Email)
            {
                user.Email = editVM.Email;
                isChanged = true;
            }
            if (user.UserName != editVM.Username)
            {
                user.UserName = editVM.Username;
                isChanged = true;
            }
            if (user.Country != editVM.Country)
            {
                user.Country = editVM.Country;
                isChanged = true;
            }
            if (user.Governorate != editVM.Governorate)
            {
                user.Governorate = editVM.Governorate;
                isChanged = true;
            }
            if (user.City != editVM.City)
            {
                user.City = editVM.City;
                isChanged = true;
            }
            if (user.Street != editVM.Street)
            {
                user.Street = editVM.Street;
                isChanged = true;
            }
            if (user.Building != editVM.Building)
            {
                user.Building = editVM.Building;
                isChanged = true;
            }
            if (user.PhoneNumber != editVM.PhoneNumber)
            {
                user.PhoneNumber = editVM.PhoneNumber;
                isChanged = true;
            }
            if (user.Floor != editVM.Floor)
            {
                user.Floor = editVM.Floor;
                isChanged = true;
            }

            if (!isChanged)
            {
                TempData["emsg"] = "No Data changed";
                return RedirectToAction("Index", "User");
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return View("Error");
            }
            else
            {
                TempData["smsg"] = "Update successfully";
                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Favorite()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            var favorite = await _userRepository.GetFavoriteAsync(userId);

            return View(favorite);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> OrderDetail()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            var orderDetails = await _userRepository.GetOrderDetailsAsync(userId);

            return View(orderDetails);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            var cartItems = await _userRepository.GetCartItemAsync(userId);

            var cartViewModel = new CartViewModel
            {
                CartItems = cartItems,
                PaymentAmount = cartItems.Sum(item => item.OrderPrice),
                Shipping = cartItems.Count() * 10,
                CheckTotalQuantity = cartItems.Sum(item => item.OrderQty),
                Total = cartItems.Sum(item => item.OrderPrice) + (cartItems.Count() * 10)
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cartitem cartitem)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _userRepository.AddToCartAsync(cartitem, userId);
                return Json(new { success = "Product added to Cart." });
            }
            catch (ApplicationException ex)
            {
                return Json(new { error = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { error = "An error occurred while adding to cart." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddFavorite(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _userRepository.AddFavoriteAsync(id, userId);
                return RedirectToAction("ProductDetails", "Products", new { id = id });
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("Login", "UserAuthentication");
            }
            catch (Exception)
            {
                return RedirectToAction("ProductDetails", "Products", new { id = id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFavorite(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _userRepository.RemoveFavoriteAsync(id, userId);
                return RedirectToAction("ProductDetails", "Products", new { id = id });
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("Login", "UserAuthentication");
            }
            catch (Exception)
            {
                return RedirectToAction("ProductDetails", "Products", new { id = id });
            }
        }

        ////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> RemoveItem(int product_id, int cartitem_id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            await _userRepository.RemoveItem(userId, product_id, cartitem_id);

            return RedirectToAction("Cart", "User");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateQtyCartItem(int orderQty, int productId, int cartItemId, int stockQty)
        {
            var orderDetails = await _context.OrderDetail
                .Where(p => p.Product_id == productId)
                .ToListAsync();

            int sumQty = orderDetails.Sum(i => i.Qty);
            int availableQty = orderDetails.Any() ? stockQty - sumQty : stockQty;

            if (orderQty <= 0)
            {
                return Json(new { success = false, message = "Quantity should be greater than 0" });
            }
            else if (orderQty > availableQty)
            {
                return Json(new { success = false, message = "Not enough quantity available" });
            }
            else
            {
                var cartItem = await _context.Cartitem.FindAsync(cartItemId);

                if (cartItem != null)
                {
                    cartItem.Qty = orderQty;
                    await _userRepository.UpdateQtyCartItem(cartItem);

                    return Json(new { success = true, message = "Qty Updated." });
                }
                else
                {
                    return Json(new { success = false, message = "Cart item not found" });
                }
            }
        }

        public async Task<IActionResult> Checkout_Click()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            await _userRepository.Checkout_Click(userId);

            return RedirectToAction("Cart", "User");
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemCount()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(0);
                }

                var count = await _userRepository.GetCartItemCountAsync(userId);
                return Json(count);
            }
            catch (Exception)
            {
                return Json(0);
            }
        }
    }
}