using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Vaccinatedclient.Models;
using Vaccinatedclient.multi_Models;

namespace Vaccinatedclient.Controllers
{
    public class HomeController : Controller
    {
        URI uri = new URI();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

         

            var pass = HttpContext.Session.GetString("pass");
            var user = HttpContext.Session.GetString("user");

            if (pass != null && user != null && (pass != "" && user != ""))
            {
                   homevm? dataList = new homevm();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "home"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<homevm>(sections.Result);
                }
            }

                return View(dataList);
            }
            else
            {
                return RedirectToAction("Login", "Login");
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