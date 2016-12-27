using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class Search
    {
        public string Query { get; set; }

        public List<Country> Countries { get; set; }

        public List<City> Cities { get; set; }

        public List<UserData> Users = new List<UserData>();
        public List<PlacePhoto> CountriesPhoto = new List<PlacePhoto>();
        public List<PlacePhoto> CitiesPhoto = new List<PlacePhoto>();
    }
    public class PlacePhoto
    {
        public int idPlace { get; set; }
        public Photo photo { get; set; }
    }
   
}