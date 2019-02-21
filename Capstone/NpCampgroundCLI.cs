using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class NpCampgroundCLI
    {
        private string ConnectionString { get; } //Save database connection information(server name, db name)

        private IParkSqlDAO parkDAO;
        private ICampgroundSqlDAO campgroundDAO;
        private ISiteSqlDAO siteDAO;
        private IReservationSqlDAO reservationDAO;

        /// <summary>
        /// Sets up menu cli. - Constructor
        /// </summary>
        /// <param name="parkDAO"></param>
        /// <param name="campgroundDAO"></param>
        /// <param name="siteDAO"></param>
        /// <param name="reservationDAO"></param>
        public NpCampgroundCLI(IParkSqlDAO parkDAO, ICampgroundSqlDAO campgroundDAO, ISiteSqlDAO siteDAO, IReservationSqlDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
        }

        public void Run()
        {
            // Main menu - run until user types in Q
            while (true)
            {
                Console.WriteLine("Select from parks to view details");

                // Get a list of parks: Call method to query db table: park for all entries
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

                // After user selects which park to get details on,
                // Get information on that 1 park choice
                Park parkChoice = parkDAO.GetParkInfo(int.Parse(choice));
                Console.WriteLine("Park Information");
                //Print info for chosen park
                parkChoice.Display(parkChoice);

                //Call command menu (select campground/reservation for here)
                CampgroundMenu();

            }
        }

        public void CampgroundMenu()
        {
            //Run until user chooses 3 to return to main menu
            while (true)
            {
                Console.WriteLine("Select a Command");
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");

                string choice = Console.ReadLine();

                if (choice == "3") break;//Return to previous screen

                //View campgrounds in chosen park
                if (choice == "1")
                {   //Get a list of all campgrounds
                    IList<Campground> campgrounds = campgroundDAO.GetAllCampgrounds(int.Parse(choice));
                    //Loop to print the campgrounds list
                    foreach(Campground campground in campgrounds)
                    {
                        Console.WriteLine(campground.ToString());
                    }
                }
                //Then call another menu to choose campground and available dates

            }
        }
    }
}
