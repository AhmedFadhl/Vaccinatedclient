using Vaccinatedclient.Models;

namespace Vaccinatedclient.multi_Models
{
    public class listvaccine
    {
        public List<vaccine>? vaccines { get; set; }
        public List<vaccine_kids>? kid_Vaccines { get; set; }
        public IEnumerable<IGrouping<string, IEnumerable<vaccine>>> vaccine { get; set; }

    }
}