using EcommerceMVC.Models.View;
using EcommerceMVC.Repositories.Abstract;
using EcommerceMVC.Repositories.Implementation;
using EcommerceMVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceMVC.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceMVC.Data;

namespace EcommerceMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;

        public ProductsController(IProductRepository productRepository, UserManager<ApplicationUser> userManager, DatabaseContext context)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, int? subcategoryId, string title, string? subtitle, string? sortOrder)
        {
            try
            {
                List<ProductView> products;

                int categoryId = id;
                string pageTitle = title;

                ViewBag.CategoryId = categoryId;
                ViewData["Title"] = pageTitle;
                ViewData["Subtitle"] = subtitle;

                if (subcategoryId.HasValue)
                {
                    products = await _productRepository.GetProductsBySubCategoryAsync(subcategoryId.Value);
                }
                else
                {
                    products = await _productRepository.GetProductsByCategoryAsync(id);
                }

                var subCategories = await _productRepository.GetSubCategoriesByCategoryAsync(id);

                var viewModel = new ProductsViewModel
                {
                    SubCategoryView = subCategories,
                    ProductView = products
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id, string Title, string? title)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var productdetails = await _productRepository.GetProductDetailsAsync(id);
            var colors = await _productRepository.GetProductColorsAsync(id);
            var sizes = await _productRepository.GetProductSizesAsync(id);
            var images = await _productRepository.GetProductImagesAsync(id);
            var favorites = await _productRepository.GetFavoritesAsync(userId, id);
            var orderDetails = await _productRepository.GetOrderDetailsAsync(id);

            int sumqty = 0;
            int orderqty = 0;
            int stockqty = productdetails?.Qty ?? 0;

            if (orderDetails.Any())
            {
                sumqty = orderDetails.Sum(i => i.Qty);
                orderqty = stockqty - sumqty;
            }
            else
            {
                orderqty = stockqty;
            }

            if (productdetails == null)
            {
                return NotFound();
            }

            var viewModel = new ProductsViewModel
            {
                ProductDetail = productdetails,
                ProductColor = colors,
                ProductSize = sizes,
                ProductImage = images,
                Favorite = favorites,
                OrderDetail = orderDetails,
                OrderQty = orderqty
            };

            ViewData["Title"] = Title;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return BadRequest("Search string is empty.");
            }

            var searchResults = await _context.SearchViews.Where(e => e.Title.Contains(searchString)).ToListAsync();

            return Ok(searchResults);
        }
    }
}
