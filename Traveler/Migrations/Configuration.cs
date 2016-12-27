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
                new Models.Country { CountryID = 3, Name = "Hiszpania" },
                new Models.Country { CountryID = 4, Name = "Niemcy" },
                new Models.Country { CountryID = 5, Name = "Francja" },
                new Models.Country { CountryID = 6, Name = "W³ochy"}
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
                new Models.City { CityID = 9, CountryID = 3, Name = "Valencia" },
                new Models.City { CityID = 10, CountryID = 3, Name = "Sewilla" },
                new Models.City { CityID = 11, CountryID = 4, Name = "Berlin" },
                new Models.City { CityID = 12, CountryID = 4, Name = "Dortmund" },
                new Models.City { CityID = 13, CountryID = 4, Name = "Frankfurt" },
                new Models.City { CityID = 14, CountryID = 4, Name = "Dosseldorf" },
                new Models.City { CityID = 15, CountryID = 5, Name = "Pary¿" },
                new Models.City { CityID = 17, CountryID = 5, Name = "Marsylia" },
                new Models.City { CityID = 18, CountryID = 5, Name = "Lyon" },
                new Models.City { CityID = 19, CountryID = 5, Name = "Nicea" },
                new Models.City { CityID = 20, CountryID = 5, Name = "Monaco" },
                new Models.City { CityID = 21, CountryID = 6, Name = "Rzym" },
                new Models.City { CityID = 22, CountryID = 6, Name = "Mediolan" },
                new Models.City { CityID = 23, CountryID = 6, Name = "Neapol" },
                new Models.City { CityID = 24, CountryID = 6, Name = "Bolonia" },
                new Models.City { CityID = 25, CountryID = 5, Name = "Rennes" },
                new Models.City { CityID = 27, CountryID = 5, Name = "Bordoux" },
                new Models.City { CityID = 26, CountryID = 5, Name = "Reims" }
                );
        }
    }
}
