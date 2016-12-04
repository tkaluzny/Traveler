using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public string Name { get; set; }
        public virtual Place place { get; set; }
    }
}