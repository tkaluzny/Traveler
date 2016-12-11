using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int TravelID { get; set; }
        public virtual Travel travel { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public string UserID { get; set; }
        public DateTime Date { get; set; }
    }
}