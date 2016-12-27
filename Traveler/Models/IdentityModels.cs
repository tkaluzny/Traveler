using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Traveler.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
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

        public UserData ToUser()
        {
            UserData u = new UserData();
            u.Nick = UserName;
            u.ID = Id;
            u.Name = Name;
            u.Surname = Surname;
            u.Male = Male;
            u.Age = Age;
            u.City = City;
            u.Country = Country;
            u.Avatar = Avatar;
            return u;

        }
        public void FromUser(UserData u)
        {
            UserName = u.Nick;
            Id = u.ID;
            Name = u.Name;
            Surname = u.Surname;
            Male = u.Male;
            Age = u.Age;
            City = u.City;
            Country = u.Country;
            Avatar = u.Avatar;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Traveler.Models.Travel> Travels { get; set; }

        public System.Data.Entity.DbSet<Traveler.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<Traveler.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<Traveler.Models.Photo> Photos { get; set; }

        public System.Data.Entity.DbSet<Traveler.Models.Place> Places { get; set; }

        public System.Data.Entity.DbSet<Traveler.Models.Comment> Comments { get; set; }
    }
}