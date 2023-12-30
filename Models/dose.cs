namespace Vaccinatedclient.Models
{
    public class dose
    {
        public int ID { get; set; }
        public string? name { get; set; }
        public string? desc { get; set; }
        public ICollection<vaccine>? vaccines { get; } = new List<vaccine>(); // Collection navigation containing dependents

    }
}
