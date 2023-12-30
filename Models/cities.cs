namespace Vaccinatedclient.Models
{
   public class cities
    {
        public int? ID { get; set; }
        public string? name { get; set; }
        public string? desc { get; set; }
        public ICollection<hospital> hospitals { get; } = new List<hospital>(); // Collection navigation containing dependents


    }
}