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

        public List<string> Users { get; set; }
    }
}