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

        private IList<Campground> Campgrounds = new List<Campground>();

        private string ParkChoice { get; set; }

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
                this.ParkChoice = Console.ReadLine();
                if (ParkChoice.ToLower() == "q") break; // Quit if q

                // After user selects which park to get details on,
                // Get information on that 1 park choice
                Park parkChoice = parkDAO.GetParkInfo(int.Parse(ParkChoice));
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
                else if (choice == "1")
                {   //Get a list of all campgrounds
                    this.Campgrounds = campgroundDAO.GetAllCampgrounds(int.Parse(ParkChoice));
                    //Loop to print the campgrounds list
                    Console.WriteLine();
                    Console.WriteLine("Park Campgrounds");
                    Console.WriteLine($"{parkDAO.GetParkInfo(int.Parse(ParkChoice)).Name} National Park Campgrounds");

                    DisplayCampgroundInformation(Campgrounds);
                }
                else if (choice == "2")
                {
                    //Then call another menu to choose campground and available dates
                    ReservationMenu();
                }
            }
        }

        /// <summary>
        /// Reserve a campground or return to previous menu
        /// </summary>
        public void ReservationMenu()
        {
            Console.WriteLine("\nSelect a Command: ");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            string reservationChoice = Console.ReadLine();

            while (true)
            {   //Search for a reservation
                if (reservationChoice == "1")
                {
                    Console.WriteLine("Search for Campground Reservation");
                    Console.WriteLine("\t\tName     Open        Close       Daily Fee");
                    //loop through campgrounds again
                    //Console.WriteLine($"{parkDAO.GetParkInfo(int.Parse(choice)).Name} National Park Campgrounds");
                    DisplayCampgroundInformation(Campgrounds);

                    Console.WriteLine("Which campground (enter 0 to cancel)?: ");
                    string campgroundChoice = Console.ReadLine();

                    Console.WriteLine("What is the arrival date?: ");
                    string arrivalDateChoice = Console.ReadLine();

                    Console.WriteLine("What is the departure date?: ");
                    string departureDateChoice = Console.ReadLine();

                    Console.WriteLine("Results Matching Your Search Criteria");
                    Console.WriteLine("Site No.     Max Occup. Accessible? Max RV Length  Utility  Cost");
                    //Call SiteSqlDao method to view all campsites at campground
                    //siteDAO.GetAllCampsitesByCampgroundIdAndAvailability(campgroundChoice, arrivalDateChoice, departureDateChoice);
                }

                else if(reservationChoice == "2")
                {
                    break;
                }

            }
        }

        public void DisplayCampgroundInformation(IList<Campground> campgrounds)
        {
            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine(campground.ToString());
            }
        }
    }
}
