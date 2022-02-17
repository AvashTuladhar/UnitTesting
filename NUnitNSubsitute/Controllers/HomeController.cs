using Microsoft.AspNetCore.Mvc;
using Practise.Models;
using Practise.Services;
using System.Diagnostics;

namespace Practise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServices userServices;

        public HomeController(ILogger<HomeController> logger,IUserServices userServices)
        {
            _logger = logger;
            this.userServices = userServices;
        }

        public IActionResult Index()
        {
            return View(userServices.GetAllUsers());
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