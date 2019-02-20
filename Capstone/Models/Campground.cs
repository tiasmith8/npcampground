using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    //Represents a row in the database campground table
    public class Campground
    {
        public int CampgroundId { get; set; }
        public string Name { get; set; }
        public DateTime OpenMonth { get; set; }
        public DateTime ClosedMonth { get; set; }
        public decimal DailyFee { get; set; }

        //Display the campground information
        public override string ToString()
        {
            return CampgroundId.ToString().PadRight(6) + Name.PadRight(15) + 
                OpenMonth + "    " + ClosedMonth + "    " + DailyFee;
        }

    }
}
