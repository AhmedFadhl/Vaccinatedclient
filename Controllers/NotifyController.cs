using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class notifyController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {
            List<notify>? dataList = new List<notify>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "notify"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<notify>>(sections.Result);
                }
            }

            return View(dataList);
        }

  

        public ActionResult add_notify()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_notify(notify home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url + "notify");

            using (var httpClient = new HttpClient())
            {               
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<notify>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete_notify(string id)
        {
            try
            {
                notify? home = new notify();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}notify/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<notify>(sections.Result);
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

        public ActionResult Edit_notify(int id)
        {
            notify? notify = new notify();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}notify/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    notify = JsonConvert.DeserializeObject<notify>(sections.Result);
                }
            }
            return View(notify);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_notify(string id, notify home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "notify/", id);
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    
                    var model = JsonConvert.DeserializeObject<notify>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
