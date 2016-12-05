using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }

        [Required]
        [Display(Name = "Ścieżka do pliku")]
        public string FileName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Dotyczy miejsca")]
        public int PlaceID { get; set; }
        public virtual Place place { get; set; }
    }
}