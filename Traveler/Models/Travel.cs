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
    }
}