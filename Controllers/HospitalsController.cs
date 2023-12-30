using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class HospitalsController : Controller
    {
        URI uri = new URI();
        public IActionResult hospital_type()
        {
            List<hospital_type>? dataList = new List<hospital_type>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "hospital_type/type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<hospital_type>>(sections.Result);
                }
            }
            return View(dataList);
        }

        public ActionResult add_hospital_type()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_hospital_type(hospital_type home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "hospital_type/type");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<hospital_type>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(hospital_type));
        }



        public ActionResult Delete_hospital_type(string id)
        {
            try
            {
                hospital_type? home = new hospital_type();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}hospital_type/type/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<hospital_type>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(hospital_type));
            }
            catch
            {
                return View("hospital_type");
            }
        }



        [HttpGet]

        public ActionResult Edit_hospital_type(int id)
        {
            hospital_type? hospital_type = new hospital_type();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}hospital_type/type/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    hospital_type = JsonConvert.DeserializeObject<hospital_type>(sections.Result);
                }
            }
            return View(hospital_type);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_hospital_type(string id, hospital_type home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "hospital_type/type/", id);
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<hospital_type>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(hospital_type));

        }




















        public IActionResult hospital()
        {
            List<hospital>? dataList = new List<hospital>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Hospital"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<hospital>>(sections.Result);
                }
            }

            return View(dataList);
        }








        public ActionResult add_hospital()
        {

            List<hospital_type>? dataList = new List<hospital_type>();
            List<cities>? dataList2 = new List<cities>();
            hospital hospital = new hospital();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "hospital_type/type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<hospital_type>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "city"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList2 = JsonConvert.DeserializeObject<List<cities>>(sections.Result);
                }
            }

            var model = new hospital();
            if (dataList!=null)
            {        
            model.hospital_types = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.type
            });
            }
            if(dataList2!=null){

            model.cities = dataList2.ToList().Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.name
            });
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_hospital(hospital home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "hospital");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<hospital>(apiResponse.Result);
                }



            }

            return RedirectToAction(nameof(hospital));
        }






        [HttpGet]

        public ActionResult Edit_hospital(int id)
        {
            hospital? hospital = new hospital();
            List<hospital_type>? hospital_Type = new List<hospital_type>();
            List<cities>? datalist = new List<cities>();

            using (var httpClient = new HttpClient())
            {

                using (var response = httpClient.GetAsync($"{uri.url}hospital/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    hospital = JsonConvert.DeserializeObject<hospital>(sections.Result);
                }
                using (var response = httpClient.GetAsync($"{uri.url}hospital_type/type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    hospital_Type = JsonConvert.DeserializeObject<List<hospital_type>>(sections.Result);
                }
                using (var response = httpClient.GetAsync($"{uri.url}city"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    datalist = JsonConvert.DeserializeObject<List<cities>>(sections.Result);
                }
                if(hospital!=null){

                

                if(hospital_Type!=null){
                hospital.hospital_types = hospital_Type.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.type
                });
                }
                if(datalist!=null){
                hospital.cities = datalist.ToList().Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.name
                });
                }
            }
            }
            return View(hospital);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_hospital(string id, hospital home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "hospital/", id);
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<hospital>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(hospital));

        }


        public ActionResult Delete_hospital(string id)
        {
            try
            {
                hospital? home = new hospital();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}hospital/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<hospital>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(hospital_type));
            }
            catch
            {
                return View("hospital");
            }
        }





    }
}
