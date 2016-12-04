using System.Collections.Generic;

namespace Traveler.Models
{
    public class Place
    {
        public int PlaceID { get; set; }

        public int CityID { get; set; }
        public virtual City City { get; set; }

        public int TravelID { get; set; }
        public virtual Travel Travel { get; set; }

        public string Description { get; set; }

        public virtual List<Photo> Photos { get; set; }
    }
}