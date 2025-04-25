using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NewRestaurantAPI.Models;
using NewRestaurantAPI.Services;
using System.Diagnostics;

namespace NewRestaurantAPI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)// For logging when the category name is derived from a specific controller.
        {
            _logger = logger;
            
        }

        public IActionResult Index() //This will show the homepage in relation to project.
        {
            return View();
        }

        public IActionResult Privacy() //This will show the privacy notice.
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetUserName() //This is for authentication of the user. This will identify them.
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                return Content(username);
            }
            return Content("No user");
        }
        [Authorize]
        public IActionResult Restricted()
        {
            return Content("This is restricted.");
        }

        

    }
}
