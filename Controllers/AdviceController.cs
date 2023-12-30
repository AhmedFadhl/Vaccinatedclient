using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class AdviceController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {
            List<advices>? dataList = new List<advices>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "advice"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<advices>>(sections.Result);
                }
            }

            return View(dataList);
        }

  

        public ActionResult add_advice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_advice(advices home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "advice");

            using (var httpClient = new HttpClient())
            {               
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<advices>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete_advice(string id)
        {
            try
            {
                advices? home = new advices();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}advice/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<advices>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }



        [HttpGet]

        public ActionResult Edit_advice(int id)
        {
            advices? advices = new advices();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}advice/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    advices = JsonConvert.DeserializeObject<advices>(sections.Result);
                }
            }
            return View(advices);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_advice(string id, advices home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "advice/", id);
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    
                    var model = JsonConvert.DeserializeObject<advices>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
