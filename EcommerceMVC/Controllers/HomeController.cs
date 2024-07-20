using EcommerceMVC.Models;
using EcommerceMVC.Repositories.Abstract;
using EcommerceMVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebsiteRepository _websiteRepository;

        public HomeController(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}