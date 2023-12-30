using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class VaccinesController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {

            List<vaccine>? dataList = new List<vaccine>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "vaccine"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<vaccine>>(sections.Result);
                }
            }
            return View(dataList);
        }


        public ActionResult add_vaccine()
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


            var model = new vaccine();
            if (dataList != null)
            {


                model.dose_list = dataList.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_vaccine(vaccine home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "vaccine");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<vaccine>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public ActionResult Edit_vaccine(int id)
        {
            List<dose>? dataList = new List<dose>();
            var model = new vaccine();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "dose"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<dose>>(sections.Result);

                }

                using (var response = httpClient.GetAsync(uri.url + $"vaccine/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<vaccine>(sections.Result);
                }
            }

            if (model != null)
            {
                if (dataList != null)
                {


                    model.dose_list = dataList.ToList().Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.name
                    });
                }


            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit_vaccine(int id, vaccine home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, $"vaccine/{id}");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<vaccine>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }




        public ActionResult Delete_kid(string id)
        {
            try
            {
                kids? home = new kids();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}kids/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<kids>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(vaccine));
            }
            catch
            {
                return View("vaccine");
            }
        }







    }
}
