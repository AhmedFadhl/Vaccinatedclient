using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vaccinatedclient.Models
{
    public class hospital
    {
        public int? ID { get; set; }
        public string? name { get; set; }
        [ForeignKey("hospitaltypefk")]
        public int type_id { get; set; }
        public int city_id { get; set; }
        public string? latitude { get; set; }
        public string? longtitude { get; set; }
        public ICollection<kids>? kids { get; } = new List<kids>(); // Collection navigation containing dependents
        public hospital_type? hospital_Type { get; set; } = null!; // Required reference navigation to principal
        public cities? city { get; set; } = null!; // Required reference navigation to principal
        public virtual IEnumerable<SelectListItem>? hospital_types { get; set; }
        public virtual IEnumerable<SelectListItem>? cities { get; set; }
        public string? address { get; set; }
        public double phone_number { get; set; }

    }
}
