using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vaccinatedclient.Models
{
    public class kids
    {
        public int ID { get; set; }
        public string? name { get; set; }
        public DateTime? pirth_date { get; set; }
        public string? pirth_place { get; set; }
        public string? blood { get; set; }
        public int age_in_days { get; set; }
        public int host_id { get; set; }
        public hospital? hospital { get; set; }
        public int? father_id { get; set; }
        public int? mother_id { get; set; }
        public parents? father { get; set; }
        public parents? mother { get; set; }
        public List<vaccine>? Vaccines { get; } = new();
        public virtual IEnumerable<SelectListItem>? fathers_list { get; set; }
        public virtual IEnumerable<SelectListItem>? mothers_list { get; set; }
        public virtual IEnumerable<SelectListItem>? hospital_list { get; set; }
    }
}
