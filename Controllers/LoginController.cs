using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{

    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor context;


        URI uri = new URI();


        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }
        public IActionResult Login()
        {
            context.HttpContext.Session.SetString("user", "");
            context.HttpContext.Session.SetString("pass", "");
            context.HttpContext.Session.SetString("token", "");
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDto users)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Auth/login");
            User? user_data = new User();
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    string a = apiResponse.Result.ToString();
                    if (a != "User not found." && a != "Wrong password.")
                    {
                        user_data = JsonConvert.DeserializeObject<User>(apiResponse.Result);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Check the username or the password");
                        UserDto user = new UserDto();
                        return View(user);
                    }
                }
                if (user_data != null)
                {
                    context.HttpContext.Session.SetString("user", users.user_name);
                    context.HttpContext.Session.SetString("pass", users.Password);
                    context.HttpContext.Session.SetString("token", user_data.token);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction(nameof(Login));

                }



            }

        }





        [HttpPost]
        public ActionResult Register(UserDto users)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Auth/Register");
            User? user_data = new User();
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    if (apiResponse.Result == "did not add user successfully")
                    {
                        user_data = JsonConvert.DeserializeObject<User>(apiResponse.Result);
                    }
                }

                return RedirectToAction(nameof(Login));
            }
        }
    }
}