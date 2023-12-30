using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vaccinatedclient.Models
{
    public class kid_vaccine
    {
        public int kids_Id { get; set; }
        public int vaccines_Id { get; set; }
        public virtual IEnumerable<SelectListItem>? kids { get; set; }
        public virtual IEnumerable<SelectListItem>? vaccine { get; set; }

        
        public Boolean taken { get; set; } = false;
    }
}
