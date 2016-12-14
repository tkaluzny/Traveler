using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class UserData
    {
        public virtual string ID { get; set; }
        public string Nick { get; set; }
        [Display(Name = "Imie:")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko:")]
        public string Surname { get; set; }
        [Display(Name = "Płeć:")]
        public byte Male { get; set; }
        [Display(Name = "Wiek:")]
        public byte Age { get; set; }
        [Display(Name = "Miasto:")]
        public string City { get; set; }
        [Display(Name = "Panstwo:")]
        public string Country { get; set; }
        public string Avatar { get; set; }
    }
}