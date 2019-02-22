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
            
            // Instantiate menu class for entire program and send it the data access objects
            NpCampgroundCLI cli = new NpCampgroundCLI(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            //Console menu entry point
            cli.Run();

        }
    }
}


//TODO
/* Catch incorrect input to menus (not 1, 2, 3) - In progress
 * Create unit tests for entire application - not possible
 * Add user authentication/login
 * Look at Bonus
 */
