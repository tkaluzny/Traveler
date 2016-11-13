namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Traveler.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Traveler.Models.ApplicationDbContext context)
        {
            context.Countries.AddOrUpdate(
                c => c.Name,
                new Models.Country { CountryID = 1, Name = "Polska" },
                new Models.Country { CountryID = 2, Name = "Anglia" }
                );

            context.Cities.AddOrUpdate(
                c => c.Name,
                new Models.City { CityID = 1, CountryID = 1, Name = "Warszawa" },
                new Models.City { CityID = 2, CountryID = 1, Name = "Kraków" },
                new Models.City { CityID = 3, CountryID = 1, Name = "Lublin" },
                new Models.City { CityID = 4, CountryID = 1, Name = "Opole" },
                new Models.City { CityID = 5, CountryID = 2, Name = "Manchester" },
                new Models.City { CityID = 6, CountryID = 2, Name = "Londyn" }
                );
        }
    }
}
