using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Vaccinatedclient.Models;

namespace Vaccinatedclient.Controllers
{
    public class parentsController : Controller
    {
        URI uri = new URI();
        public IActionResult Index()
        {
            List<parents>? dataList = new List<parents>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Parent"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<parents>>(sections.Result);
                }
            }

            return View(dataList);
        }



        public ActionResult add_parent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_parent(parents home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Parent");

                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                byte[] data;
                if (home.picture != null){


                    using (var br = new BinaryReader(home.picture.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)home.picture.OpenReadStream().Length);
                    }
                ByteArrayContent bytes = new ByteArrayContent(data);
                multiContent.Add(bytes, "picture", home.picture.FileName);
                }
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.address), "address");
                multiContent.Add(new StringContent(home.id_card_number.ToString()), "id_card_number");
                multiContent.Add(new StringContent(home.gender.ToString()), "gender");
                multiContent.Add(new StringContent(home.pirth_date.ToString()), "pirth_date");
                multiContent.Add(new StringContent(home.email), "email");
                multiContent.Add(new StringContent(home.password), "password");
                multiContent.Add(new StringContent(home.nationality), "nationality");
                multiContent.Add(new StringContent(home.user_name), "user_name");
                multiContent.Add(new StringContent(home.phone_number.ToString()), "phone_number");
                multiContent.Add(new StringContent(home.mobile_number.ToString()), "mobile_number");
                multiContent.Add(new StringContent(home.marital_status.ToString()), "marital_status");
                multiContent.Add(new StringContent(home.pirth_place.ToString()), "pirth_place");
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<parents>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete_parent(string id)
        {
            try
            {
                parents? home = new parents();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}Parent/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<parents>(sections.Result);
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

        public ActionResult Edit_parents(int id)
        {
            parents? parents = new parents();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}Parent/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    parents = JsonConvert.DeserializeObject<parents>(sections.Result);
                }
            }
            return View(parents);

        }


        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_parents(string id, parents home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Parent/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;

                MultipartFormDataContent multiContent = new MultipartFormDataContent();




                if (home.picture != null)
                {
                    using (var br = new BinaryReader(home.picture.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)home.picture.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    multiContent.Add(bytes, "picture", home.picture.FileName);
                }
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.address), "address");
                multiContent.Add(new StringContent(home.id_card_number.ToString()), "id_card_number");
                multiContent.Add(new StringContent(home.gender.ToString()), "gender");
                multiContent.Add(new StringContent(home.pirth_date.ToString()), "pirth_date");
                multiContent.Add(new StringContent(home.email), "email");
                multiContent.Add(new StringContent(home.password), "password");
                multiContent.Add(new StringContent(home.nationality), "nationality");
                multiContent.Add(new StringContent(home.user_name), "user_name");
                multiContent.Add(new StringContent(home.phone_number.ToString()), "phone_number");
                multiContent.Add(new StringContent(home.ID.ToString()), "ID");
                multiContent.Add(new StringContent(home.mobile_number.ToString()), "mobile_number");
                multiContent.Add(new StringContent(home.marital_status.ToString()), "marital_status");
                multiContent.Add(new StringContent(home.pirth_place.ToString()), "pirth_place");
                multiContent.Add(new StringContent(home.image_path), "image_path");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<parents>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Index));

        }






    }
}
