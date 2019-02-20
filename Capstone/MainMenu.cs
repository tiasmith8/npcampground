using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class MainMenu
    {
        private string ConnectionString { get;}

        public MainMenu(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void Run()
        {
            // Main menu
            while (true)
            {
                Console.WriteLine("Select from parks to view details");

                //Object to pull from park table to C# object
                ParkSqlDAO parkDAO = new ParkSqlDAO(ConnectionString);
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
                CampgroundMenu campgroundMenu = new CampgroundMenu(int.Parse(choice));
                campgroundMenu.Run();

            }
        }
    }
}
