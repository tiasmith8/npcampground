using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using System.Linq;

namespace Capstone
{
    public class NpCampgroundCLI
    {
        // Properties
        private string ConnectionString { get; } //Save database connection information(server name, db name)
        private string ParkChoice { get; set; } //Holds which park the user chooses
        private int CampgroundChoice { get; set; } //Holds the chosen campground number

        // Variables
        private IList<Campground> Campgrounds = new List<Campground>();
        private IList<Site> Sites = new List<Site>();

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

        // Starting point of the program. First shows all parks in the system.
        public void Run()
        {
            // Account for can't parse string that isn't a number
            int tryParseInt;

            // Main menu - run until user types in Q
            while (true)
            {
                // Get a list of parks: Call method to query db table (park) for all entries
                IList<Park> parks = parkDAO.GetAllParks();
                int numberOfParks = 0;

                do //using a do to print the initial menu once and then check for valid input
                {
                    Console.WriteLine("Select from parks to view details");

                    // Loop through parks list returned and print out all the available parks
                    foreach (Park park in parks)
                    {
                        Console.WriteLine(park.ToString());
                        numberOfParks++; //Keep track of how many parks there are to validate input
                    }
                    // Option for user to quit
                    Console.WriteLine("(Q) Quit");

                    // Read in user choice
                    this.ParkChoice = Console.ReadLine();

                    if (this.ParkChoice.ToLower() == "q") return; // Quit if q

                    Console.Clear();

                } // while input is not valid, continue to prompt user to choose a valid park
                while ((int.TryParse(this.ParkChoice, out tryParseInt) == false) || int.Parse(ParkChoice) <= 0 || int.Parse(ParkChoice) > numberOfParks);

                // Save the chosen park here
                Park parkChoice = parkDAO.GetParkInfo(int.Parse(this.ParkChoice));
                Console.WriteLine("Park Information");

                // Print info for chosen park
                parkChoice.Display(parkChoice);

                // Call command menu (select campground/reservation for here)
                CampgroundMenu();
            }
        }

        // 2nd menu
        public void CampgroundMenu()
        {
            // create list of campgrounds
            this.Campgrounds = campgroundDAO.GetAllCampgrounds(int.Parse(this.ParkChoice));

            // Saves park name to a variable to be used in menu options
            string parkName = parkDAO.GetParkInfo(int.Parse(this.ParkChoice)).Name;

            // Run until user chooses 3 to return to main menu
            while (true)
            {
                Console.WriteLine("Select an Option:");
                Console.WriteLine($"1) View Campgrounds for {parkName}");
                Console.WriteLine($"2) Search for a Reservation at {parkName}");
                Console.WriteLine($"3) View All Upcoming Reservations for {parkName}");
                Console.WriteLine("4) Return to Previous Screen");

                string choice = Console.ReadLine();

                if (choice == "4") { Console.Clear(); break; }//Return to previous screen

                // View campgrounds in chosen park
                else if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Park Campgrounds");
                    Console.WriteLine($"{parkName} National Park Campgrounds");

                    //Displays the campsites for the chosen park
                    DisplayCampgroundInformation();
                }

                // Search for a reservation
                else if (choice == "2")
                {
                    Console.Clear();

                    // Then call another menu to choose campground and available dates
                    ReservationMenu();
                }

                // Choice to view all upcoming reservations for the chosen park
                // chosen park id: parkChoice
                else if (choice == "3")
                {
                    UpcomingReservationsMenu();
                }
            }
        }

        private void UpcomingReservationsMenu()
        {
            Console.Clear();
            Console.WriteLine($"Upcoming Reservations for: {parkDAO.GetParkInfo(int.Parse(this.ParkChoice)).Name} National Park Campgrounds");
            Console.WriteLine();
            // Make a call that returns a list of reservations
            IList<Reservation> upcomingReservations = siteDAO.GetReservationsNext30Days(int.Parse(this.ParkChoice));

            // Header
            Console.WriteLine($"ReservationID\t {"Name", 5}{"ArrivalDate", 37} {"Departure Date", 26}");

            //Now print the list
            foreach(Reservation reservation in upcomingReservations)
            {   
                // Reservation Id, Name, FromDate, ToDate of Rerservation
                Console.WriteLine($"{reservation.Reservation_Id}\t{reservation.Name, 35}\t" +
                    $"{reservation.From_Date}\t{reservation.To_Date}" );
            }
        }

        /// <summary>
        /// Reserve a campground or return to previous menu
        /// </summary>
        public void ReservationMenu()
        {
            string reservationChoice = string.Empty;

            do
            { 
                DisplayCampgroundInformation();

                Console.WriteLine("\nSelect an Option: ");
                Console.WriteLine("1) Search for Available Reservation");
                Console.WriteLine("2) Return to Previous Screen");
                reservationChoice = Console.ReadLine();

                //Search for a reservation
                if (reservationChoice == "1")
                {
                    Console.WriteLine("Search for Campground Reservation");

                    Console.WriteLine($"{parkDAO.GetParkInfo(int.Parse(this.ParkChoice)).Name} National Park Campgrounds");

                    Console.WriteLine("Which campground (enter 0 to cancel)?: ");

                    //Need to check for invalid input
                    int tryParse = 0;
                    string input = Console.ReadLine();
                    bool isValid = int.TryParse(input, out tryParse);

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid input");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    } //If user doesn't enter a number
                    this.CampgroundChoice = int.Parse(input);

                    //If an integer was read in, but it's not in the list then break
                    if (!Campgrounds.Any(var => var.CampgroundId == CampgroundChoice))
                    {
                        Console.WriteLine("Site does not exist!");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    } 

                    else if (CampgroundChoice == 0) break;

                    DateTime arrivalDateChoice = ValidateInput.GetDate("What is the arrival date?");
                    DateTime departureDateChoice = ValidateInput.GetDate("What is the departure date?");

                    Console.WriteLine("Results Matching Your Search Criteria");
                    Console.WriteLine($"{"Site No.",-5}{"Max Occup.",10}{"Accessible?",10}{"Max RV Length",10}{"Utility",10}{"Total Cost",10}");

                    //Call SiteDao method to view all campsites at campground
                    // Returns list of sites. Display list + cost
                    this.Sites = siteDAO.GetAvailableSites(this.CampgroundChoice, arrivalDateChoice, departureDateChoice);

                    if (this.Sites.Count == 0)
                    {
                        Console.WriteLine("No sites available for given dates, choose new dates [y/n]");
                        string choice = Console.ReadLine();

                        if (choice.ToLower() == "y")
                        {
                            Console.Clear();
                            ReservationMenu();
                        }
                        else break;
                    }

                    // Display available sites and pass in number of days in reservation to calculate total cost
                    int numOfSites = DisplayCamgroundSites((departureDateChoice - arrivalDateChoice).Days);

                    Console.WriteLine("Which site should be reserved (enter 0 to cancel)? ");
                    string siteReservation = Console.ReadLine();

                    //Check for valid site info
                    if (siteReservation == "0" || int.TryParse(siteReservation, out tryParse) || int.Parse(siteReservation) < 0 || int.Parse(siteReservation) > numOfSites)
                        break;

                    Console.WriteLine("What name should the reservation be made under? ");
                    string reservationName = Console.ReadLine();

                    int reservationID = reservationDAO.CreateReservation(int.Parse(siteReservation), reservationName, arrivalDateChoice, departureDateChoice, DateTime.Now);
                    Console.WriteLine($"\nThe reservation has been made and the confirmation ID is {reservationID}");
                }

                else if (reservationChoice == "2")
                {
                    Console.Clear();
                    break;
                }
            } while (reservationChoice != "1" || reservationChoice != "2");
        }

        /// <summary>
        /// Display campgrounds based on chosen park.
        /// </summary>
        public void DisplayCampgroundInformation()
        {
            Console.WriteLine($"{"Site No.",-5}{"Name",8}{"Open",20}{"Close",16}{"Daily Fee",16}");
            foreach (Campground campground in this.Campgrounds)
            {
                Console.WriteLine(campground.ToString());
            }
        }

        public int DisplayCamgroundSites(int days)
        {
            decimal fee = siteDAO.GetSiteFee();

            foreach(Site site in Sites)
            {
                Console.WriteLine($"{site.ToString()} {fee*days:C2}");
            }
            return Sites.Count; //return number of sites
        }    
    }
}
