using Keycloak.Models;
using Keycloak.Net.Models.Users;
using Keycloak.Services.Keycloak;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Keycloak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKeycloakServices _keycloakServices;

        public HomeController(ILogger<HomeController> logger,IKeycloakServices keycloakServices)
        {
            _logger = logger;
            _keycloakServices = keycloakServices;
        }

        public IActionResult Index()
        {
            return View();
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
