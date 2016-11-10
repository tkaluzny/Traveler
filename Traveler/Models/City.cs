using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class City
    {
        public int CityID { get; set; }

        public string Name { get; set; }

        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
        
        public virtual ICollection<Travel> Travels { get; set; }
    }
}