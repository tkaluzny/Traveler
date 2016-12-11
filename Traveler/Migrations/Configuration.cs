namespace Traveler.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Traveler.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Traveler.Models.ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@example.com",
                    PasswordHash = passwordHash.HashPassword("Password@123"),
                    SecurityStamp = "8733aaf3-c0b0-43d4-b0c4-11ab1d276627"
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    PasswordHash = passwordHash.HashPassword("Password@123"),
                    SecurityStamp = "8733aaf3-c0b0-43d4-b0c4-11ab1d276627"
                });

            context.Countries.AddOrUpdate(
                c => c.Name,
                new Models.Country { CountryID = 1, Name = "Polska" },
                new Models.Country { CountryID = 2, Name = "Anglia" },
                new Models.Country { CountryID = 3, Name = "Hiszpania" }
                );

            context.Cities.AddOrUpdate(
                c => c.Name,
                new Models.City { CityID = 1, CountryID = 1, Name = "Warszawa" },
                new Models.City { CityID = 2, CountryID = 1, Name = "Kraków" },
                new Models.City { CityID = 3, CountryID = 1, Name = "Lublin" },
                new Models.City { CityID = 4, CountryID = 1, Name = "Opole" },
                new Models.City { CityID = 5, CountryID = 2, Name = "Manchester" },
                new Models.City { CityID = 6, CountryID = 2, Name = "Londyn" },
                new Models.City { CityID = 7, CountryID = 3, Name = "Barcelona" },
                new Models.City { CityID = 8, CountryID = 3, Name = "Madryt" },
                new Models.City { CityID = 9, CountryID = 3, Name = "Valencia" }
                );
        }
    }
}
