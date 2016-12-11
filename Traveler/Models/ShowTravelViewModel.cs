using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class ShowTravelViewModel
    {
        public Travel travel { get; set; }
        public Comment comment { get; set; }
        public List<Comment> Comments { get; set; }
    }
}