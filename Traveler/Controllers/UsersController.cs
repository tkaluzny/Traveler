using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Show/user@example.com
        public ActionResult Show(string id)
        {
            UserViewModel model = new UserViewModel { };

            IQueryable<UserData> foundUsers = db.UserData.Where(u => u.Nick == id);

            if (foundUsers.Count() > 0)
            {
                model.User = foundUsers.FirstOrDefault();
                model.Travels = db.Travels.Where(t => t.UserID == id).ToList();

                return View(model);
            }

            return View("~/Views/Shared/AccessDeniedError.cshtml");
        }
    }
}
