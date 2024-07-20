using EcommerceMVC.Models.DTO;
using EcommerceMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebsiteRepository _websiteRepository;

        public CategoryController(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string title)
        {
            try
            {
                var carousel = await _websiteRepository.GetCarousels();
                var category = await _websiteRepository.GetCategories();

                var viewModel = new HomeViewModel
                {
                    Carousel = carousel,
                    Category = category
                };

                ViewData["Title"] = title;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}