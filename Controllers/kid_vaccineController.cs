using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;
using Vaccinatedclient.multi_Models;

namespace Vaccinatedclient.Controllers
{
    public class kid_vaccineController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {
            List<vaccine_kids>? dataList = new List<vaccine_kids>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "kid_vaccine/detiles"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<vaccine_kids>>(sections.Result);
                }

            }
            return View(dataList);
        }



        public ActionResult add_vaccine_kid()
        {

            List<kids>? kids = new List<kids>();
            List<vaccine>? vaccines = new List<vaccine>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "kids"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    kids = JsonConvert.DeserializeObject<List<kids>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "vaccine"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vaccines = JsonConvert.DeserializeObject<List<vaccine>>(sections.Result);
                }
            }
            var model = new kid_vaccine();
            if (kids != null)
            {

                model.kids = kids.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });
            }
            if (vaccines != null)
            {

                model.vaccine = vaccines.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });

            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_vaccine_kid(kid_vaccine home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "kid_vaccine");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<kid_vaccine>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit_kid_vaccine(int kidid, int vaccineid)
        {

            var model = new kid_vaccine();
            List<kids>? kids = new List<kids>();
            List<vaccine>? vaccines = new List<vaccine>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "kids"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    kids = JsonConvert.DeserializeObject<List<kids>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "vaccine"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vaccines = JsonConvert.DeserializeObject<List<vaccine>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + $"kid_vaccine/{kidid}/{vaccineid}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<kid_vaccine>(sections.Result);
                }
            }

            if (model != null)
            {
                if (kids != null)
                {

                    model.kids = kids.ToList().Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.name
                    });
                }
                if (vaccines != null)
                {

                    model.vaccine = vaccines.ToList().Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.name
                    });
                }
            }


            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit_kid_vaccine(int kidid, int vaccineid, kid_vaccine home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, $"kid_vaccine/{home.kids_Id}/{vaccineid}");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<kids>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }




        public ActionResult Delete_kids_vaccine(int kidid, int vaccineid)
        {
            try
            {
                kids? home = new kids();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}kid_vaccine/{kidid}/{vaccineid}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<kids>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }




    }
}
