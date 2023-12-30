using System.ComponentModel.DataAnnotations.Schema;

namespace Vaccinatedclient.Models
{
    public class parents
    {
        public int? ID { get; set; }
        public string? name { get; set; }
        public int id_card_number { get; set; }
        public DateTime? pirth_date { get; set; }
        public int marital_status { get; set; }
        public string? user_name { get; set; }
        public string? nationality { get; set; }
        public string? pirth_place { get; set; }
        public int? gender { get; set; }
        public string? gender_string { get; set; }
        public string? password { get; set; }
        public int? mobile_number { get; set; }
        public int? phone_number { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public string? image_path { get; set; }
        [NotMapped]
        public IFormFile? picture { get; set; }
        public List<kids> kids { get; } = new();


    }
}
