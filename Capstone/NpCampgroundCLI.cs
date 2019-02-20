using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class NpCampgroundCLI
    {
        private string ConnectionString { get;}

        private IParkSqlDAO parkDAO;
        private ICampgroundSqlDAO campgroundDAO;
        private ISiteSqlDAO siteDAO;
        private IReservationSqlDAO reservationDAO;

        public NpCampgroundCLI(IParkSqlDAO parkDAO, ICampgroundSqlDAO campgroundDAO, ISiteSqlDAO siteDAO, IReservationSqlDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
        }

        public void Run()
        {
            // Main menu
            while (true)
            {
                Console.WriteLine("Select from parks to view details");

                // Get a list of parks
                IList<Park> parks = parkDAO.GetAllParks();

                // Loop through parks list returned and print out all the available parks
                foreach(Park park in parks)
                {
                    Console.WriteLine(park.ToString());
                }
                // Option for user to quit
                Console.WriteLine("(Q) Quit");

                // Read in user choice
                string choice = Console.ReadLine();
                if (choice.ToLower() == "q") break; // Quit if q

                // Get information on park choice
                Park parkChoice = parkDAO.GetParkInfo(int.Parse(choice));
                Console.WriteLine("Park Information");
                //Print info for chosen park
                parkChoice.Display(parkChoice);

                //Call command menu
                CampgroundMenu();

            }
        }

        public void CampgroundMenu()
        {

            while (true)
            {
                Console.WriteLine("Select a Command");
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");

                string choice = Console.ReadLine();

                if (choice == "3") break;

                if (choice == "1")
                {
                    IList<Campground> campgrounds = campgroundDAO.GetAllCampgrounds(int.Parse(choice));
                    foreach(Campground campground in campgrounds)
                    {
                        Console.WriteLine(campground.ToString());
                    }
                }


            }
        }
    }
}
