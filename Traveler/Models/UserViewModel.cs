using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.Models
{
    public class UserViewModel
    {
        public UserData User{ get; set; }

        public List<Travel> Travels { get; set; }
    }
}