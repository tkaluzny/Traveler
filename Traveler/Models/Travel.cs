using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Traveler.Models
{
    public class Travel
    {
        public int TravelID { get; set; }

        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Display(Name="Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public string UserID { get; set; }
        
        public virtual ICollection<City> Cities { get; set; }
        [Display(Name = "Zdjęcia")]
        public virtual ICollection<Image> Photos { get; set; }
    }
}