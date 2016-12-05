using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Traveler.Models
{
    public class Place
    {
        public int PlaceID { get; set; }

        [Required]
        public int CityID { get; set; }
        public virtual City City { get; set; }

        [Required]
        public int TravelID { get; set; }
        public virtual Travel Travel { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual List<Photo> Photos { get; set; }
    }
}