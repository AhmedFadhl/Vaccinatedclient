using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{

    public class CitiesController : Controller
    {
        URI uri = new URI();   

        public IActionResult cities()
        {
            List<cities>? dataList = new List<cities>();
            using (var httpClient = new HttpClient())
            {
                
                using (var response = httpClient.GetAsync(uri.url + "city"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<cities>>(sections.Result);
                }
            }
            return View(dataList);
        }

        public ActionResult add_city()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_city(cities home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "city");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<cities>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(cities));
        }



        public ActionResult Delete_city(string id)
        {
            try
            {
                cities? home = new cities();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}city/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<cities>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(cities));
            }
            catch
            {
                return View("cities");
            }
        }



        [HttpGet]

        public ActionResult Edit_cities(int id)
        {
            cities? city = new cities();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}city/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    city = JsonConvert.DeserializeObject<cities>(sections.Result);
                }
            }
            return View(city);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_city(string id, cities home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "city/", id);
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<cities>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(cities));

        }

    }
}