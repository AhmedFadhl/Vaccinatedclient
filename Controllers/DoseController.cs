using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class DoseController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {
            List<dose>? dataList = new List<dose>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Dose"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<dose>>(sections.Result);
                }
            }

            return View(dataList);
        }



        public ActionResult add_dose()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_dose(dose home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Dose");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<dose>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete_dose(string id)
        {
            try
            {
                dose? home = new dose();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}dose/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<dose>(sections.Result);
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

        public ActionResult Edit_dose(int id)
        {
            dose? dose = new dose();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}dose/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dose = JsonConvert.DeserializeObject<dose>(sections.Result);
                }
            }
            return View(dose);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_dose(string id, dose home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "dose/", id);
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<dose>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Index));

        }




    }
}
