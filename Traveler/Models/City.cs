using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Traveler.Models
{
    public class City
    {
        public int CityID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Państwo")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
    }
}