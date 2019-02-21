using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Capstone.DAL;

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

            // Save the connection string pulled from key(Project), Value(db connection info) pair
            string connectionString = configuration.GetConnectionString("Project");

            // Instantiate objects communicating with DB here and pass to menu class
            IParkSqlDAO         parkDAO        = new ParkSqlDAO(connectionString);
            ICampgroundSqlDAO   campgroundDAO  = new CampgroundSqlDAO(connectionString);
            ISiteSqlDAO         siteDAO        = new SiteSqlDAO(connectionString);
            IReservationSqlDAO  reservationDAO = new ReservationSqlDAO(connectionString);
            
            // Instantiate menu class for entire program
            NpCampgroundCLI cli = new NpCampgroundCLI(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            //Console menu entry point
            cli.Run();
        }
    }
}


//TODO
/* Delete MainMenu.cs since we have NpCampgroundCLI.cs now and don't need it (same code) - done
 * Fix spacing/padding for printing campgrounds and parks in Models->Campground ToString method
 * Fix spacing/padding in Models-> Park
 * Create ToString method overrides for Reservation model and Site model
 * Convert number months to name values
 * Create integration unit tests for the sqldao classes
 * Create unit tests for entire application
 * Fill out IReservation interface based on UML (methods)
 * Fill out ISite based on UML (methods)
 * Continue ICampground interface definition
 * Add user authentication/login
 * Look at Bonus
 */
