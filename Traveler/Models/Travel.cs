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
        
        public virtual List<Place> Places { get; set; }
        public virtual List<Photo> Photos { get; set; }
    }
}