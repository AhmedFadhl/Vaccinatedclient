using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vaccinatedclient.Models
{
    public class vaccine
    {
        public int? ID { get; set; }
        public string? name { get; set; }
        public int days_to_take { get; set; }
        public string? desc { get; set; }
        public int dose_id { get; set; }
        public dose? dose { get; set; }
        public List<kids>? kids { get; } = new();
        public virtual IEnumerable<SelectListItem>? dose_list { get; set; }


    }
}
