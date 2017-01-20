using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}