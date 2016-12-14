using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Traveler.Models;
using System.Linq;
using System.Web.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace Traveler.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            UserData model = new UserData { };

            model = db.UserData.Where(u => u.Nick == User.Identity.Name).FirstOrDefault();
            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult Edit()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var gender = new SelectListItem { Text = "Kobieta", Value = "0" };
            list.Add(gender);
            gender = new SelectListItem { Text = "Meżczyzna", Value = "1" };
            list.Add(gender);
            ViewData["Male"] = list;
            UserData model = new UserData { };

            model = db.UserData.Where(u => u.Nick == User.Identity.Name).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Nick,Name,Surname,Male,Age,City,Country,Avatar")]UserData user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string dirPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "uploads");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                user.Avatar = user.Avatar + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(dirPath, user.Avatar);
                FileStream writeStream = new FileStream(filePath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(writeStream);
                byte[] buff = new byte[file.ContentLength];
                file.InputStream.Read(buff, 0, file.ContentLength);
                bw.Write(buff);
                bw.Close();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
             
            }
            return View(user);
        }
        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

#endregion
    }
}