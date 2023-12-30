namespace Vaccinatedclient.Models
{
    public class hospital_type
    {
        public int? ID { get; set; }
        public string? type { get; set; }
        public string? desc { get; set; }
        public ICollection<hospital> hospitals { get; } = new List<hospital>(); // Collection navigation containing dependents

    }
}
