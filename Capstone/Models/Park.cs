using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    //Model for the park table
    public class Park
    {
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
            return ParkId.ToString() + "\n" + Name + " " + Location;
        }
    }
}
