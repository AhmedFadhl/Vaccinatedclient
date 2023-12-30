using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class KidsController : Controller
    {
        URI uri = new URI();
        public IActionResult get_kids()
        {
            List<kids>? dataList = new List<kids>();
            List<kids>? kids = new List<kids>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Kids"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<kids>>(sections.Result);
                }
            }
            if (dataList != null)
            {
                foreach (var item in dataList)
                {
                    double s = DateTime.Now.Subtract(Convert.ToDateTime(item.pirth_date)).TotalDays;
                    item.age_in_days=(int)s;
                    kids.Add(item);
                }
            }
            return View(dataList);
        }



        public ActionResult add_kid()
        {

            List<parents>? dataList = new List<parents>();
            List<hospital>? hospitals = new List<hospital>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Parent"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<parents>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "hospital"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    hospitals = JsonConvert.DeserializeObject<List<hospital>>(sections.Result);
                }
            }
            List<parents> father = new List<parents>();
            List<parents> mother = new List<parents>();

            var model = new kids();
            if (dataList != null)
            {

                foreach (var item in dataList)
                {
                    if (item.gender == 1)
                    {
                        father.Add(item);

                    }
                    else if (item.gender == 2)
                    {
                        mother.Add(item);
                    }
                }
            }

            model.fathers_list = father.ToList().Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.name
            });
            if (hospitals != null)
            {

                model.hospital_list = hospitals.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });
            }
            model.mothers_list = mother.ToList().Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.name
            });

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_kid(kids home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Kids");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<kids>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(get_kids));
        }

        [HttpGet]
        public ActionResult Edit_kid(int id)
        {

            List<parents>? dataList = new List<parents>();
            List<hospital>? hospitals = new List<hospital>();
            var model = new kids();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Parent"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<parents>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "hospital"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    hospitals = JsonConvert.DeserializeObject<List<hospital>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + $"Kids/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<kids>(sections.Result);
                }
            }
            List<parents> father = new List<parents>();
            List<parents> mother = new List<parents>();

            if (dataList != null)
            {

                foreach (var item in dataList)
                {
                    if (item.gender == 1)
                    {
                        father.Add(item);

                    }
                    else if (item.gender == 2)
                    {
                        mother.Add(item);
                    }
                }
            }
            if (model != null)
            {

                model.fathers_list = father.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });
                if (hospitals != null)
                {

                    model.hospital_list = hospitals.ToList().Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.name
                    });
                }
                model.mothers_list = mother.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit_kid(int id, kids home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, $"Kids/{id}");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<kids>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(get_kids));
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
                return RedirectToAction(nameof(get_kids));
            }
            catch
            {
                return View("get_kids");
            }
        }




    }
}
