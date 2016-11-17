using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Traveler.Models
{
    public class City
    {
        public int CityID { get; set; }

        public string Name { get; set; }

        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
        
        [ScriptIgnore]
        public virtual ICollection<Travel> Travels { get; set; }
    }
}