using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vaccinatedclient.Models;
using Vaccinatedclient.multi_Models;

namespace Vaccinatedclient.Controllers
{
    public class ListvaccineController:Controller
    {
          URI uri = new URI();
        public IActionResult Index(int id)
        {
            List<listvaccine>? model = new List<listvaccine>();
            listvaccine? dataList = new listvaccine();
            List<vaccine>? vaccines=new List<vaccine>();
            kids? kids=new kids();
            List<vaccine_kids>? vaccine_Kids=new List<vaccine_kids>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "vaccine"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vaccines = JsonConvert.DeserializeObject<List<vaccine>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "kid_vaccine/detiles"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vaccine_Kids = JsonConvert.DeserializeObject<List<vaccine_kids>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + $"Kids/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    kids = JsonConvert.DeserializeObject<kids>(sections.Result);
                }
                

            }
            dataList.kid_Vaccines=vaccine_Kids;
            dataList.vaccines=vaccines;
            return View(dataList);
        }





    }
}