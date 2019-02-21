using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Capstone.DAL;
using Capstone.Menus;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            IParkDAO parkDAO = new ParksSqlDAO(connectionString);
            ICampgroundDAO campgroundDAO = new CampgroundSqlDAO(connectionString);
            ISiteDAO siteDAO = new SitesSqlDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationsSqlDAO(connectionString);
            MainMenuCLI mainMenu = new MainMenuCLI();
            ParkInfoMenuCLI parkInfoMenu = new ParkInfoMenuCLI();
            ParkCampgroundsMenuCLI parkCampgrounds = new ParkCampgroundsMenuCLI();
            ReservationMenuCLI reservationMenu = new ReservationMenuCLI();

            NPSystemController nPSystemController = new NPSystemController(parkDAO, campgroundDAO, siteDAO, reservationDAO, mainMenu, parkInfoMenu, parkCampgrounds, reservationMenu);
            nPSystemController.Run();

        }
    }
}
