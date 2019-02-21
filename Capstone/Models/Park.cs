using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    //Model for the park table
    public class Park
    {   //Park object Properties - maps to table columns
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        //Display the park information
        public override string ToString()
        {
            return ParkId.ToString() + ") " + Name;
        }

        /// <summary>
        /// Prints "Park Information Screen"
        /// </summary>
        /// <param name="parkChoice"></param>
        public void Display(Park parkChoice)
        {
            Console.WriteLine(Name + " National Park");
            Console.WriteLine($"{"Location", -1} {parkChoice.Location, 25}");
            Console.WriteLine($"Established: {parkChoice.EstablishDate, 25}");
            Console.WriteLine($"Area: {parkChoice.Area, 25}");
            Console.WriteLine($"Annual Visitors: {parkChoice.Visitors, 25}");
            Console.WriteLine("\n" + Description + "\n");
        }
    }
}
