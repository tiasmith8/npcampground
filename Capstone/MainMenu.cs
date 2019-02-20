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
            while (true)
            {
                Console.WriteLine("Select from parks to view details");

                ParkSqlDAO parkDAO = new ParkSqlDAO(ConnectionString);
                IList<Park> parks = parkDAO.GetAllParks();

                foreach(Park park in parks)
                {
                    Console.WriteLine(park.ToString());
                }
                Console.WriteLine("(Q) Quit");

                string choice = Console.ReadLine();
                if (choice.ToLower() == "q") break;
            }
        }
    }
}
